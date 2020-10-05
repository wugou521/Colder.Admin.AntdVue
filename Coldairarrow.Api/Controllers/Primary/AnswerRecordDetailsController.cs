using Coldairarrow.Business.Primary;
using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Primary
{
    [Route("/Primary/[controller]/[action]")]
    public class AnswerRecordDetailsController : BaseApiController
    {
        #region DI

        public AnswerRecordDetailsController(IAnswerRecordDetailsBusiness answerRecordDetailsBus)
        {
            _answerRecordDetailsBus = answerRecordDetailsBus;
        }

        IAnswerRecordDetailsBusiness _answerRecordDetailsBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<AnswerRecordDetails>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _answerRecordDetailsBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<AnswerRecordDetails> GetTheData(IdInputDTO input)
        {
            return await _answerRecordDetailsBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(AnswerRecordDetails data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _answerRecordDetailsBus.AddDataAsync(data);
            }
            else
            {
                await _answerRecordDetailsBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _answerRecordDetailsBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}