using Coldairarrow.Business.Primary;
using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Primary
{
    [Route("/Primary/[controller]/[action]")]
    public class FractionsController : BaseApiController
    {
        #region DI

        public FractionsController(IFractionsBusiness fractionsBus)
        {
            _fractionsBus = fractionsBus;
        }

        IFractionsBusiness _fractionsBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<FractionsDTO>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _fractionsBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<Fractions> GetTheData(IdInputDTO input)
        {
            return await _fractionsBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(FractionsEditDTO data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _fractionsBus.AddDataAsync(data);
            }
            else
            {
                await _fractionsBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _fractionsBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}