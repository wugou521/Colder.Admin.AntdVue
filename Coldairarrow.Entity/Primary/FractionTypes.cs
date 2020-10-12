using Coldairarrow.Entity.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Primary
{
    /// <summary>
    /// FractionTypes
    /// </summary>
    [Table("FractionTypes")]
    public class FractionTypes
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 试题类型
        /// </summary>
        public FractionType FractionType { get; set; }

        /// <summary>
        /// 试题分数
        /// </summary>
        public Decimal? FractionCount { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public Boolean? IsHidden { get; set; }

        /// <summary>
        /// 错题扣分
        /// </summary>
        public Decimal? WrongCount { get; set; }

        /// <summary>
        /// 少选得分
        /// </summary>
        public Decimal? LessCount { get; set; }

        /// <summary>
        /// ScheduleId
        /// </summary>
        public String ScheduleId { get; set; }

    }
}