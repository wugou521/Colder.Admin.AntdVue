using Coldairarrow.Business.Primary;
using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Primary
{
    [Route("/Primary/[controller]/[action]")]
    public class AnswerRecordsController : BaseApiController
    {
        #region DI

        public AnswerRecordsController(IAnswerRecordsBusiness answerRecordsBus)
        {
            _answerRecordsBus = answerRecordsBus;
        }

        IAnswerRecordsBusiness _answerRecordsBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<AnswerRecords>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _answerRecordsBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<AnswerRecords> GetTheData(IdInputDTO input)
        {
            return await _answerRecordsBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(AnswerRecords data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _answerRecordsBus.AddDataAsync(data);
            }
            else
            {
                await _answerRecordsBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _answerRecordsBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}