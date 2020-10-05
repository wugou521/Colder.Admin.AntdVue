using Coldairarrow.Business.Primary;
using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Primary
{
    [Route("/Primary/[controller]/[action]")]
    public class MenusController : BaseApiController
    {
        #region DI

        public MenusController(IMenusBusiness menusBus)
        {
            _menusBus = menusBus;
        }

        IMenusBusiness _menusBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<MenusDTO>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _menusBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<Menus> GetTheData(IdInputDTO input)
        {
            return await _menusBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(Menus data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _menusBus.AddDataAsync(data);
            }
            else
            {
                await _menusBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _menusBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}