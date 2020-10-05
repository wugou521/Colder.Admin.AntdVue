using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Primary
{
    /// <summary>
    /// Schedules
    /// </summary>
    [Table("Schedules")]
    public class Schedules
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 父级编号
        /// </summary>
        public String ParentId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public Int32? Level { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public Boolean? Type { get; set; }

        /// <summary>
        /// 课程路径
        /// </summary>
        public String Path { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public String IconUrl { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public Boolean? Kind { get; set; }

        /// <summary>
        /// 创建人编号
        /// </summary>
        public String CreatorId { get; set; }

        /// <summary>
        /// 题目数量
        /// </summary>
        public Int32? FracitonCount { get; set; }

        /// <summary>
        /// ClassesId
        /// </summary>
        public String ClassesId { get; set; }

    }
}