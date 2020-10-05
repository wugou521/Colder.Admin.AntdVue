using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Primary
{
    public interface IAnswerRecordDetailsBusiness
    {
        Task<PageResult<AnswerRecordDetails>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<AnswerRecordDetails> GetTheDataAsync(string id);
        Task AddDataAsync(AnswerRecordDetails data);
        Task UpdateDataAsync(AnswerRecordDetails data);
        Task DeleteDataAsync(List<string> ids);
    }
}