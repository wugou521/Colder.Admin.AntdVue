using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Primary
{
    public interface IRecordsBusiness
    {
        Task<PageResult<Records>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<Records> GetTheDataAsync(string id);
        Task AddDataAsync(Records data);
        Task UpdateDataAsync(Records data);
        Task DeleteDataAsync(List<string> ids);
    }
}