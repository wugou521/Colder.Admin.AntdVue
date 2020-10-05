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
    public class AnswerRecordDetailsBusiness : BaseBusiness<AnswerRecordDetails>, IAnswerRecordDetailsBusiness, ITransientDependency
    {
        public AnswerRecordDetailsBusiness(IDbAccessor db)
            : base(db)
        {
        }

        #region 外部接口

        public async Task<PageResult<AnswerRecordDetails>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<AnswerRecordDetails>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<AnswerRecordDetails, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<AnswerRecordDetails> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(AnswerRecordDetails data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(AnswerRecordDetails data)
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