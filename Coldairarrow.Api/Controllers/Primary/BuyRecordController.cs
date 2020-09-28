using Coldairarrow.Business.Primary;
using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Primary
{
    [Route("/Primary/[controller]/[action]")]
    public class BuyRecordController : BaseApiController
    {
        #region DI

        public BuyRecordController(IBuyRecordBusiness buyRecordBus)
        {
            _buyRecordBus = buyRecordBus;
        }

        IBuyRecordBusiness _buyRecordBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<BuyRecord>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _buyRecordBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<BuyRecord> GetTheData(IdInputDTO input)
        {
            return await _buyRecordBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(BuyRecord data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _buyRecordBus.AddDataAsync(data);
            }
            else
            {
                await _buyRecordBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _buyRecordBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}