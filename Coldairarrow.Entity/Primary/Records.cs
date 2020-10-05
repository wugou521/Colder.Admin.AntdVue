using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Primary
{
    /// <summary>
    /// Records
    /// </summary>
    [Table("Records")]
    public class Records
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public String UserId { get; set; }

        /// <summary>
        /// 小节编号
        /// </summary>
        public String ScheduleId { get; set; }

        /// <summary>
        /// 一级目录编号
        /// </summary>
        public String ScheduleOneId { get; set; }

        /// <summary>
        /// 题目编号
        /// </summary>
        public String FractionId { get; set; }

        /// <summary>
        /// 记录类型
        /// </summary>
        public Int32? Type { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

    }
}