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
        public FractionsBusiness(IDbAccessor db, IMapper mapper)
            : base(db)
        {
            _mapper = mapper;
        }

        #region 外部接口

        public async Task<PageResult<FractionsDTO>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            Expression<Func<Fractions, Schedules, Fractions, FractionsDTO>> select = (a, b, c) => new FractionsDTO
            {
                ParentName = c.Title,
                SchedulesName = b.Name
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

        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        [Transactional]
        public async Task UploadFractionsAsync(string filepath)
        {
            try
            {
                filepath = Path.Combine(System.Environment.CurrentDirectory, "wwwroot", filepath.Substring(filepath.IndexOf("Upload")).Replace("/", "\\"));
                var data = AsposeOfficeHelper.ReadExcel(filepath);
                string scheduleId = "";
                string parentId = "";
                foreach (System.Data.DataRow item in data.Rows)
                {
                    string scheduleName = item[0].ToString();
                    string parentName = item[1].ToString();
                    string type = item[2].ToString();
                    string title = item[3].ToString();
                    string description = item[4].ToString();
                    string answer = item[5].ToString();
                    string a = item[6].ToString();
                    string b = item[7].ToString();
                    string c = item[8].ToString();
                    string d = item[9].ToString();
                    string analysis = item[10].ToString();


                    //var fraction=new Fractions { 
                        
                    //}
                }
            }
            catch (Exception)
            {

                throw;
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