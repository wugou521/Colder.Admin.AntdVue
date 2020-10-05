using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Primary
{
    /// <summary>
    /// AnswerRecords
    /// </summary>
    [Table("AnswerRecords")]
    public class AnswerRecords
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public String UserId { get; set; }

        /// <summary>
        /// 小节编号
        /// </summary>
        public String SchedulesId { get; set; }

        /// <summary>
        /// 一级课程编号
        /// </summary>
        public String SchedulesOneId { get; set; }

        /// <summary>
        /// 总题目数量
        /// </summary>
        public Int32? FractionCount { get; set; }

        /// <summary>
        /// 回答题目数量
        /// </summary>
        public Int32? FractionAnswerCount { get; set; }

        /// <summary>
        /// 正确率
        /// </summary>
        public Decimal? RightRate { get; set; }

        /// <summary>
        /// 正确数量
        /// </summary>
        public Decimal? RightFraction { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime? CreateTime { get; set; }

    }
}