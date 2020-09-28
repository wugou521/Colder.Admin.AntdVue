using Coldairarrow.Business.Primary;
using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Primary
{
    [Route("/Primary/[controller]/[action]")]
    public class UserController : BaseApiController
    {
        #region DI

        public UserController(IUserBusiness userBus)
        {
            _userBus = userBus;
        }

        IUserBusiness _userBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<User>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _userBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<User> GetTheData(IdInputDTO input)
        {
            return await _userBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(User data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _userBus.AddDataAsync(data);
            }
            else
            {
                await _userBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _userBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}