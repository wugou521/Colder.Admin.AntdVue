using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Primary
{
    public interface IMenusBusiness
    {
        Task<PageResult<MenusDTO>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<Menus> GetTheDataAsync(string id);
        Task AddDataAsync(Menus data);
        Task UpdateDataAsync(Menus data);
        Task DeleteDataAsync(List<string> ids);
    }

    [Map(typeof(Menus))]
    public class MenusDTO : Menus
    {
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName { get => Status.Value ? "上架" : "下架"; }
    }
}