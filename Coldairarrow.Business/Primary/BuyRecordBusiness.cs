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
    public class BuyRecordBusiness : BaseBusiness<BuyRecord>, IBuyRecordBusiness, ITransientDependency
    {
        public BuyRecordBusiness(IDbAccessor db)
            : base(db)
        {
        }

        #region 外部接口

        public async Task<PageResult<BuyRecordDTO>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            Expression<Func<BuyRecord, Classes, User, BuyRecordDTO>> select = (a, b, c) => new BuyRecordDTO
            {
                ClassesName = b.ClassName,
                Phone = c.Phone
            };
            select = select.BuildExtendSelectExpre();

            var q = from a in GetIQueryable().AsExpandable()
                    join b in Db.GetIQueryable<Classes>() on a.ClassesId equals b.Id into ab
                    from b in ab.DefaultIfEmpty()
                    join c in Db.GetIQueryable<User>() on a.UserId equals c.Id into ac
                    from c in ac.DefaultIfEmpty()
                    select @select.Invoke(a, b, c);

            var search = input.Search;
            if (!search.Keyword.IsNullOrEmpty())
            {
                var keyword = $"%{search.Keyword}%";
                q = q.Where(x =>
                      EF.Functions.Like(x.Phone, keyword)
                      || EF.Functions.Like(x.ClassesName, keyword));
            }

            return await q.GetPageResultAsync(input);
        }

        public async Task<BuyRecord> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(BuyRecord data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(BuyRecord data)
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