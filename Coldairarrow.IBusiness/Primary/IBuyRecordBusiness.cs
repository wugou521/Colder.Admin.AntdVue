using Coldairarrow.Entity.DTO;
using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Primary
{
    public interface IBuyRecordBusiness
    {
        Task<PageResult<BuyRecordDTO>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<BuyRecord> GetTheDataAsync(string id);
        Task AddDataAsync(BuyRecord data);
        Task UpdateDataAsync(BuyRecord data);
        Task DeleteDataAsync(List<string> ids);
    }
}