using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Primary
{
    public interface ICollegeBusiness
    {
        Task<PageResult<College>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<College> GetTheDataAsync(string id);
        Task AddDataAsync(College data);
        Task UpdateDataAsync(College data);
        Task DeleteDataAsync(List<string> ids);
    }
}