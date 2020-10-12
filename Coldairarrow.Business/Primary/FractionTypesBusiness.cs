using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class FractionTypesBusiness : BaseBusiness<FractionTypes>, IFractionTypesBusiness, ITransientDependency
    {
        readonly IMapper _mapper;
        public FractionTypesBusiness(IDbAccessor db, IMapper mapper)
            : base(db)
        {
            _mapper = mapper;
        }

        #region 外部接口

        public async Task<PageResult<FractionTypesDTO>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            Expression<Func<FractionTypes, Schedules, FractionTypesDTO>> select = (a, b) => new FractionTypesDTO
            {
                ScheduleName = b.Name
            };

            var search = input.Search;
            select = select.BuildExtendSelectExpre();

            var q = from a in GetIQueryable().AsExpandable()
                    join b in Db.GetIQueryable<Schedules>() on a.ScheduleId equals b.Id into ab
                    from b in ab.DefaultIfEmpty()
                    select @select.Invoke(a, b);

            if (!search.Keyword.IsNullOrEmpty())
            {
                var keyword = $"%{search.Keyword}%";
                q = q.Where(x =>
                      EF.Functions.Like(x.ScheduleId, keyword)
                      || EF.Functions.Like(x.FractionTypeName, keyword));
            }

            return await q.GetPageResultAsync(input);
        }

        public async Task<FractionTypes> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(FractionTypes data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(FractionTypes data)
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