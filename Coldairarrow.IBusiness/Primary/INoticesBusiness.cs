using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Primary
{
    public interface INoticesBusiness
    {
        Task<PageResult<Notices>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<Notices> GetTheDataAsync(string id);
        Task AddDataAsync(Notices data);
        Task UpdateDataAsync(Notices data);
        Task DeleteDataAsync(List<string> ids);
    }
}