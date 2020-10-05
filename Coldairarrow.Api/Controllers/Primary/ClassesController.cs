using Coldairarrow.Business.Primary;
using Coldairarrow.Entity.DTO;
using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Primary
{
    [Route("/Primary/[controller]/[action]")]
    public class ClassesController : BaseApiController
    {
        #region DI

        public ClassesController(IClassesBusiness classesBus)
        {
            _classesBus = classesBus;
        }

        IClassesBusiness _classesBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<ClassesDTO>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _classesBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<Classes> GetTheData(IdInputDTO input)
        {
            return await _classesBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(Classes data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _classesBus.AddDataAsync(data);
            }
            else
            {
                await _classesBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _classesBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}