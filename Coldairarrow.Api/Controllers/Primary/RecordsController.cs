using Coldairarrow.Business.Primary;
using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Primary
{
    [Route("/Primary/[controller]/[action]")]
    public class RecordsController : BaseApiController
    {
        #region DI

        public RecordsController(IRecordsBusiness recordsBus)
        {
            _recordsBus = recordsBus;
        }

        IRecordsBusiness _recordsBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<Records>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _recordsBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<Records> GetTheData(IdInputDTO input)
        {
            return await _recordsBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(Records data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _recordsBus.AddDataAsync(data);
            }
            else
            {
                await _recordsBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _recordsBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}