using Coldairarrow.Entity.DTO;
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
    public class SchedulesBusiness : BaseBusiness<Schedules>, ISchedulesBusiness, ITransientDependency
    {
        public SchedulesBusiness(IDbAccessor db)
            : base(db)
        {
        }

        #region 外部接口

        public async Task<List<SchedulesTreeDTO>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            Expression<Func<Schedules, Classes, SchedulesDTO>> select = (a, b) => new SchedulesDTO
            {
                ClassesName = b.ClassName
            };
            select = select.BuildExtendSelectExpre();

            var search = input.Search;

            var q = from a in GetIQueryable().AsExpandable()
                    join b in Db.GetIQueryable<Classes>() on a.ClassesId equals b.Id into ab
                    from b in ab.DefaultIfEmpty()
                    select @select.Invoke(a, b);

            if (!search.Keyword.IsNullOrEmpty())
            {
                var keyword = $"%{search.Keyword}%";
                if (search.Condition == "ClassesName")
                {
                    q = q.Where(x => EF.Functions.Like(x.ClassesName, keyword));
                }
                else if (search.Condition == "Name")
                {
                    q = q.Where(x => EF.Functions.Like(x.Name, keyword));
                }
                else if (search.Condition == "Title")
                {
                    q = q.Where(x => EF.Functions.Like(x.Title, keyword));
                }
            }
            List<SchedulesTreeDTO> treeList = await q.Select(row => new SchedulesTreeDTO
            {
                Id = row.Id,
                ParentId = row.ParentId,
                Name = row.Name,
                Text = row.Name,
                Title = row.Title,
                IconUrl = row.IconUrl,
                ClassesName = row.ClassesName,
                CreateTime = row.CreateTime,
                FracitonCount = row.FracitonCount ?? 0,
                Value = row.Id
            }).ToListAsync();

            return TreeHelper.BuildTree(treeList);
        }

        public async Task<Schedules> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(Schedules data)
        {
            await InitData(data);
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(Schedules data)
        {
            await InitData(data);
            await UpdateAsync(data);
        }

        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        #endregion

        #region 私有成员
        /// <summary>
        /// 数据处理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task InitData(Schedules data)
        {
            if (data.ClassesId.IsNullOrEmpty() && data.ParentId.IsNullOrEmpty())
            {
                throw new Exception("班级和父级必选一个");
            }
            if (!data.ClassesId.IsNullOrEmpty())
            {
                data.Path = data.Id ;
                data.Level = 1;
            }
            else
            {
                var parent = await GetTheDataAsync(data.ParentId);
                data.Path = Path.Combine(parent.Path, data.Id);
                data.Level = parent.Level + 1;
            }
            data.Path += "\\";
            data.Kind = false;
            data.FracitonCount = 0;
        }
        #endregion
    }
}