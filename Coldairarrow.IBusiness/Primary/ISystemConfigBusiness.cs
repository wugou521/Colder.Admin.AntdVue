using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Coldairarrow.Business.Primary
{
    public interface ISystemConfigBusiness
    {
        Task<PageResult<SystemConfigDTO>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<SystemConfig> GetTheDataAsync(string id);
        Task AddDataAsync(SystemConfigEditDTO data);
        Task UpdateDataAsync(SystemConfigEditDTO data);
        Task DeleteDataAsync(List<string> ids);
    }

    [Map(typeof(SystemConfig))]
    public class SystemConfigEditDTO : SystemConfig
    {
        /// <summary>
        /// 幻灯片集合
        /// </summary>
        public List<string> SlidesList { get; set; }
    }

    [Map(typeof(SystemConfig))]
    public class SystemConfigDTO : SystemConfig
    {
        /// <summary>
        /// 幻灯片集合
        /// </summary>
        public List<string> SlidesList { get => Slides.Split(',').ToList(); }
    }
}