using Coldairarrow.Entity.DTO;
using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Primary
{
    public interface ISchedulesBusiness
    {
        Task<List<SchedulesTreeDTO>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<Schedules> GetTheDataAsync(string id);
        Task AddDataAsync(Schedules data);
        Task UpdateDataAsync(Schedules data);
        Task DeleteDataAsync(List<string> ids);
    }

    public class SchedulesTreeDTO : TreeModel
    {
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassesName { get; set; }

        /// <summary>
        /// 题目数量
        /// </summary>
        public Int32? FracitonCount { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public String IconUrl { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 课程路径
        /// </summary>
        public String Path { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public String Title { get; set; }

        public object children { get => Children; }
        public string title { get => Text; }
        public string value { get => Id; }
        public string key { get => Id; }
    }
}