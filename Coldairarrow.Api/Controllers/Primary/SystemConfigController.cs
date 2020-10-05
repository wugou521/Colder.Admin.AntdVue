using Coldairarrow.Business.Primary;
using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Primary
{
    [Route("/Primary/[controller]/[action]")]
    public class SystemConfigController : BaseApiController
    {
        #region DI

        public SystemConfigController(ISystemConfigBusiness systemConfigBus)
        {
            _systemConfigBus = systemConfigBus;
        }

        ISystemConfigBusiness _systemConfigBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<SystemConfigDTO>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _systemConfigBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<SystemConfig> GetTheData(IdInputDTO input)
        {
            return await _systemConfigBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(SystemConfigEditDTO data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _systemConfigBus.AddDataAsync(data);
            }
            else
            {
                await _systemConfigBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _systemConfigBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}