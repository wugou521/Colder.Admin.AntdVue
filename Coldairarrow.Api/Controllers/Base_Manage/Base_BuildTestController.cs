using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    [Route("/Base_Manage/[controller]/[action]")]
    public class Base_BuildTestController : BaseApiController
    {
        #region DI

        public Base_BuildTestController(IBase_BuildTestBusiness base_BuildTestBus)
        {
            _base_BuildTestBus = base_BuildTestBus;
        }

        IBase_BuildTestBusiness _base_BuildTestBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<Base_BuildTest>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _base_BuildTestBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<Base_BuildTest> GetTheData(IdInputDTO input)
        {
            return await _base_BuildTestBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(Base_BuildTest data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _base_BuildTestBus.AddDataAsync(data);
            }
            else
            {
                await _base_BuildTestBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _base_BuildTestBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}