using Coldairarrow.Business.Primary;
using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Primary
{
    [Route("/Primary/[controller]/[action]")]
    public class SchedulesController : BaseApiController
    {
        #region DI

        public SchedulesController(ISchedulesBusiness schedulesBus)
        {
            _schedulesBus = schedulesBus;
        }

        ISchedulesBusiness _schedulesBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<List<SchedulesTreeDTO>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _schedulesBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<Schedules> GetTheData(IdInputDTO input)
        {
            return await _schedulesBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(Schedules data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _schedulesBus.AddDataAsync(data);
            }
            else
            {
                await _schedulesBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _schedulesBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}