using AutoMapper;
using AutoMapper.QueryableExtensions;
using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Primary
{
    public class FractionsBusiness : BaseBusiness<Fractions>, IFractionsBusiness, ITransientDependency
    {
        readonly IMapper _mapper;
        public FractionsBusiness(IDbAccessor db, IMapper mapper, ISchedulesBusiness schedulesBusiness)
            : base(db)
        {
            _mapper = mapper;
            _schedulesBusiness = schedulesBusiness;
        }

        readonly ISchedulesBusiness _schedulesBusiness;

        #region 外部接口

        public async Task<PageResult<FractionsDTO>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            Expression<Func<Fractions, Schedules, Fractions, FractionsDTO>> select = (a, b, c) => new FractionsDTO
            {
                ParentName = c.Title,
                SchedulesName = b.Name + " " + b.Title
            };

            var search = input.Search;

            select = select.BuildExtendSelectExpre();

            var q = from a in GetIQueryable().AsExpandable()
                    join b in Db.GetIQueryable<Schedules>() on a.SchedulesId equals b.Id into ab
                    from b in ab.DefaultIfEmpty()
                    join c in GetIQueryable() on a.ParentId equals c.Id into ac
                    from c in ac.DefaultIfEmpty()
                    select @select.Invoke(a, b, c);

            q = q.WhereIf(!search.SchedulesId.IsNullOrEmpty(), row => row.SchedulesId == search.SchedulesId);
            q = q.WhereIf(!search.FractionType.IsNullOrEmpty() && search.FractionType != 0, row => row.Type == search.FractionType);
            q = q.WhereIf(!search.Keyword.IsNullOrEmpty(), row => row.Title.Contains(search.Keyword));

            return await q.GetPageResultAsync(input);
        }

        public async Task<FractionsViewDTO> GetTheDataAsync(string id)
        {
            var data = await GetEntityAsync(id);
            data.Answer = data.Answer.Replace("A", "1").Replace("B", "2").Replace("C", "3").Replace("D", "4");
            return _mapper.Map<FractionsViewDTO>(data);
        }

        public async Task AddDataAsync(FractionsEditDTO data)
        {
            if (data.AnswerList != null && data.AnswerList.Count > 0)
            {
                data.Answer = HandlerListAnswer(data.AnswerList);
            }
            data.Answer = ChangeAnswerType(data.Answer, 0);
            await InsertAsync(_mapper.Map<Fractions>(data));
        }

        public async Task UpdateDataAsync(FractionsEditDTO data)
        {
            if (data.AnswerList != null && data.AnswerList.Count > 0)
            {
                data.Answer = HandlerListAnswer(data.AnswerList);
            }
            data.Answer = ChangeAnswerType(data.Answer, 0);
            await UpdateAsync(_mapper.Map<Fractions>(data));
        }

        [Transactional]
        public async Task DeleteDataAsync(List<string> ids)
        {
            Dictionary<string, int> schedules = new Dictionary<string, int>();
            foreach (var item in ids)
            {
                var data = await GetTheDataAsync(item);
                var schedule = Db.GetIQueryable<Schedules>().SingleOrDefault(row => row.Id == data.SchedulesId);
                var parentSchedule = await _schedulesBusiness.GetTheParentDataAsync(schedule.ParentId, 1);
                if (schedules.ContainsKey(schedule.Id))
                {
                    schedules[schedule.Id]++;
                }
                else
                {
                    schedules.Add(schedule.Id, 1);
                }
                if (schedules.ContainsKey(parentSchedule.Id))
                {
                    schedules[parentSchedule.Id]++;
                }
                else
                {
                    schedules.Add(parentSchedule.Id, 1);
                }
            }
            await DeleteAsync(ids);
            foreach (var item in schedules)
            {
                var data = await Db.GetEntityAsync<Schedules>(item.Key);
                data.FracitonCount -= item.Value;
                await Db.UpdateAsync(data);
            }
        }

        [Transactional]
        public async Task<List<KeyValuePair<string, string>>> UploadFractionsAsync(string filepath)
        {
            try
            {
                filepath = Path.Combine(System.Environment.CurrentDirectory, "wwwroot", filepath.Substring(filepath.IndexOf("Upload")).Replace("/", "\\"));
                var data = AsposeOfficeHelper.ReadExcel(filepath);
                string scheduleId = "";
                string topScheduleId = "";
                string topScheduleName = "";
                string parentId = "";
                string scheduleName = "";
                string parentName = "";
                string classesId = "";
                List<KeyValuePair<string, string>> fractionTitles = new List<KeyValuePair<string, string>>();
                Dictionary<string, int> schedules = new Dictionary<string, int>();
                var fractionTypes = Db.GetIQueryable<FractionTypes>().ToList();
                if (fractionTypes.Count <= 0)
                {
                    throw new Exception("题目类型数据为空");
                }
                foreach (System.Data.DataRow item in data.Rows)
                {
                    string title = item[4].ToString();
                    try
                    {
                        bool topChange = false;
                        if (topScheduleName.IsNullOrEmpty() || topScheduleName != item[0].ToString().Trim())
                        {
                            topScheduleName = item[0].ToString().Trim();
                            var schedule = Db.GetIQueryable<Schedules>().FirstOrDefault(row => row.Title == topScheduleName);
                            topScheduleId = schedule.Id;
                            topChange = true;
                        }

                        //小节是必有的 如果为空则是第一次循环 如果不相等则重新获取
                        if (scheduleName.IsNullOrEmpty() || scheduleName != item[1].ToString().Trim() || topChange)
                        {
                            scheduleName = item[1].ToString().Trim();
                            var schedule = await _schedulesBusiness.GetChildListDataAsync(topScheduleId, scheduleName);
                            if (schedule != null)
                            {
                                scheduleId = schedule.Id;
                                classesId = schedule.ClassesId;
                            }
                            else
                            {
                                fractionTitles.Add(new KeyValuePair<string, string>(title, $"小节{scheduleName}名称不存在"));
                                continue;
                            }
                        }

                        string parentTile = item[2].ToString().Trim();
                        if (!parentTile.IsNullOrEmpty())
                        {
                            if (parentName != parentTile)
                            {
                                parentName = parentTile;
                                var singleFraction = GetIQueryable().FirstOrDefault(row => row.Title == parentName);
                                if (singleFraction != null)
                                {
                                    parentId = singleFraction.Id;
                                }
                                else
                                {
                                    fractionTitles.Add(new KeyValuePair<string, string>(title, $"父题{parentName}不存在"));
                                }
                            }
                        }
                        else
                        {
                            parentId = null;
                        }
                        string type = item[3].ToString();
                        if (type.IsNullOrEmpty())
                        {
                            fractionTitles.Add(new KeyValuePair<string, string>(title, $"题目类型错误"));
                            continue;
                        }
                        string description = item[5].ToString();
                        string answer = item[6].ToString();
                        string a = item[7].ToString();
                        string b = item[8].ToString();
                        string c = item[9].ToString();
                        string d = item[10].ToString();
                        string analysis = item[11].ToString();

                        var fraction = new Fractions
                        {
                            Id = IdHelper.GetId(),
                            SchedulesId = scheduleId,
                            Title = title,
                            Description = description,
                            Answer = answer,
                            Type = type == "判断题" ? 1 : type == "单选题" ? 2 : type == "多选题" ? 3 : type == "不定项父题" ? 4 : type == "不定项子题" ? 5 : 0,
                            A = a,
                            B = b,
                            C = c,
                            D = d,
                            Analysis = analysis,
                            CreateTime = DateTime.Now
                        };
                        fraction.FractionCount = fractionTypes.FirstOrDefault(row => row.FractionType == fraction.Type && row.ScheduleId == scheduleId)?.FractionCount;
                        if (fraction.Type == 5)
                        {
                            fraction.ParentId = parentId.IsNullOrEmpty() ? null : parentId;
                        }
                        else if (fraction.Type == 4)
                        {
                            parentId = fraction.Id;
                        }
                        else
                        {
                            parentId = null;
                        }
                        await InsertAsync(fraction);
                        //更新每个章节题目数量
                        if (schedules.ContainsKey(scheduleId))
                        {
                            schedules[scheduleId]++;
                        }
                        else
                        {
                            schedules.Add(scheduleId, 1);
                        }
                        //更新顶级
                        if (schedules.ContainsKey(topScheduleId))
                        {
                            schedules[topScheduleId]++;
                        }
                        else
                        {
                            schedules.Add(topScheduleId, 1);
                        }
                    }
                    catch (Exception ex)
                    {
                        fractionTitles.Add(new KeyValuePair<string, string>(title, $"插入试题错误：{ex.Message}"));
                    }
                }
                foreach (var item in schedules)
                {
                    var sche = Db.GetIQueryable<Schedules>().FirstOrDefault(row => row.Id == item.Key);
                    sche.FracitonCount += item.Value;
                    await Db.UpdateAsync<Schedules>(sche);
                }
                return fractionTitles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 私有成员
        private string HandlerListAnswer(List<string> listAnswer)
        {
            if (listAnswer != null && listAnswer.Count > 0)
            {
                return string.Join(",", listAnswer);
            }
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string ChangeAnswerType(string answer, int type)
        {
            if (type != 1)
            {
                return answer.Replace("1", "A").Replace("2", "B").Replace("3", "C").Replace("4", "D");
            }
            else
            {
                return answer.Replace("1", "正确").Replace("2", "错误");
            }
        }
        #endregion
    }
}