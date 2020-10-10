using Coldairarrow.Business.Primary;
using Coldairarrow.Entity.Enum;
using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Primary
{
    [Route("/Primary/[controller]/[action]")]
    public class FractionTypesController : BaseApiController
    {
        #region DI

        public FractionTypesController(IFractionTypesBusiness fractionTypesBus)
        {
            _fractionTypesBus = fractionTypesBus;
        }

        IFractionTypesBusiness _fractionTypesBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<FractionTypesDTO>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _fractionTypesBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<FractionTypes> GetTheData(IdInputDTO input)
        {
            return await _fractionTypesBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(FractionTypes data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _fractionTypesBus.AddDataAsync(data);
            }
            else
            {
                await _fractionTypesBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _fractionTypesBus.DeleteDataAsync(ids);
        }


        [HttpPost]
        public List<SelectOption> GetFractionTypeList()
        {
            return EnumHelper.ToOptionList(typeof(FractionType));
        }

        #endregion
    }
}