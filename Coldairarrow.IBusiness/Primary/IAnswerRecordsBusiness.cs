using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Primary
{
    public interface IAnswerRecordsBusiness
    {
        Task<PageResult<AnswerRecords>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<AnswerRecords> GetTheDataAsync(string id);
        Task AddDataAsync(AnswerRecords data);
        Task UpdateDataAsync(AnswerRecords data);
        Task DeleteDataAsync(List<string> ids);
    }
}