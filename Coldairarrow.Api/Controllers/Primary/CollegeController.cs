using Coldairarrow.Business.Primary;
using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Primary
{
    [Route("/Primary/[controller]/[action]")]
    public class CollegeController : BaseApiController
    {
        #region DI

        public CollegeController(ICollegeBusiness collegeBus)
        {
            _collegeBus = collegeBus;
        }

        ICollegeBusiness _collegeBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<College>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _collegeBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<College> GetTheData(IdInputDTO input)
        {
            return await _collegeBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(College data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _collegeBus.AddDataAsync(data);
            }
            else
            {
                await _collegeBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _collegeBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}