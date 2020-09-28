using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Primary
{
    public interface IUserBusiness
    {
        Task<PageResult<User>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<User> GetTheDataAsync(string id);
        Task AddDataAsync(User data);
        Task UpdateDataAsync(User data);
        Task DeleteDataAsync(List<string> ids);
    }
}