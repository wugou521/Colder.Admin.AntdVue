using Coldairarrow.Entity.DTO;
using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Primary
{
    public class ClassesBusiness : BaseBusiness<Classes>, IClassesBusiness, ITransientDependency
    {
        public ClassesBusiness(IDbAccessor db)
            : base(db)
        {
        }

        #region 外部接口

        public async Task<PageResult<ClassesDTO>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            Expression<Func<Classes, College, ClassesDTO>> select = (a, b) => new ClassesDTO
            {
                CollegeName = b.CollegeName
            };
            var search = input.Search;
            select = select.BuildExtendSelectExpre();
            var q = from a in GetIQueryable().AsExpandable()
                    join b in Db.GetIQueryable<College>() on a.CollegeId equals b.Id into ab
                    from b in ab.DefaultIfEmpty()
                    select @select.Invoke(a, b);

            if (!search.Keyword.IsNullOrEmpty())
            {
                var keyword = $"%{search.Keyword}%";
                q = q.Where(x =>
                      EF.Functions.Like(x.ClassName, keyword)
                      || EF.Functions.Like(x.CollegeName, keyword));
            }

            return await q.GetPageResultAsync(input);
        }

        public async Task<Classes> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(Classes data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(Classes data)
        {
            await UpdateAsync(data);
        }

        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        #endregion

        #region 私有成员

        #endregion
    }
}