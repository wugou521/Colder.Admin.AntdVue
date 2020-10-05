using AutoMapper;
using AutoMapper.QueryableExtensions;
using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Primary
{
    public class MenusBusiness : BaseBusiness<Menus>, IMenusBusiness, ITransientDependency
    {
        readonly IMapper _mapper;
        public MenusBusiness(IDbAccessor db, IMapper mapper)
            : base(db)
        {
            _mapper = mapper;
        }

        #region 外部接口

        public async Task<PageResult<MenusDTO>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<Menus>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Menus, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).ProjectTo<MenusDTO>(_mapper.ConfigurationProvider).GetPageResultAsync(input);
        }

        public async Task<Menus> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(Menus data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(Menus data)
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