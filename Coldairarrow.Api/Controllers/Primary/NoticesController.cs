using Coldairarrow.Business.Primary;
using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Primary
{
    [Route("/Primary/[controller]/[action]")]
    public class NoticesController : BaseApiController
    {
        #region DI

        public NoticesController(INoticesBusiness noticesBus)
        {
            _noticesBus = noticesBus;
        }

        INoticesBusiness _noticesBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<Notices>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _noticesBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<Notices> GetTheData(IdInputDTO input)
        {
            return await _noticesBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(Notices data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _noticesBus.AddDataAsync(data);
            }
            else
            {
                await _noticesBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _noticesBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}