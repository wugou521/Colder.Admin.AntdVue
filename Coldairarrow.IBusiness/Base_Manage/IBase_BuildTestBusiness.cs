using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_BuildTestBusiness
    {
        Task<PageResult<Base_BuildTest>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<Base_BuildTest> GetTheDataAsync(string id);
        Task AddDataAsync(Base_BuildTest data);
        Task UpdateDataAsync(Base_BuildTest data);
        Task DeleteDataAsync(List<string> ids);
    }
}