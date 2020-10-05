using Coldairarrow.Entity.DTO;
using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Primary
{
    public interface IClassesBusiness
    {
        Task<PageResult<ClassesDTO>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<Classes> GetTheDataAsync(string id);
        Task AddDataAsync(Classes data);
        Task UpdateDataAsync(Classes data);
        Task DeleteDataAsync(List<string> ids);
    }
}