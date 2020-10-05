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
    public class SystemConfigBusiness : BaseBusiness<SystemConfig>, ISystemConfigBusiness, ITransientDependency
    {
        readonly IMapper _mapper;
        public SystemConfigBusiness(IDbAccessor db,IMapper mapper)
            : base(db)
        {
            _mapper = mapper;
        }

        #region 外部接口

        public async Task<PageResult<SystemConfigDTO>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<SystemConfig>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<SystemConfig, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).ProjectTo<SystemConfigDTO>(_mapper.ConfigurationProvider).GetPageResultAsync(input);
        }

        public async Task<SystemConfig> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(SystemConfigEditDTO data)
        {
            data.Slides = HandlerSlideList(data.SlidesList);
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(SystemConfigEditDTO data)
        {
            data.Slides = HandlerSlideList(data.SlidesList);
            await UpdateAsync(data);
        }

        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        #endregion

        #region 私有成员
        string HandlerSlideList(List<string> slides)
        {
            return string.Join(",", slides);
        }
        #endregion
    }
}