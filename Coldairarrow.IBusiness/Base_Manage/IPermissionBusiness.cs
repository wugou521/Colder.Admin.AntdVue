using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IPermissionBusiness
    {
        Task<string[]> GetUserActionIds(string userId);
        Task<List<string>> GetUserPermissionValuesAsync(string userId);
        Task<List<Base_ActionDTO>> GetUserMenuListAsync(string userId);
    }
}
