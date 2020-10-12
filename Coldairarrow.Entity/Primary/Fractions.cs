using Coldairarrow.Entity.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Primary
{
    /// <summary>
    /// Fractions
    /// </summary>
    [Table("Fractions")]
    public class Fractions
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
        /// 课程编号
        /// </summary>
        public String SchedulesId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 题目标题
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// 题目描述
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// 题目答案
        /// </summary>
        public String Answer { get; set; }

        /// <summary>
        /// 题目类型
        /// </summary>
        public FractionType Type { get; set; }

        /// <summary>
        /// 题目分数
        /// </summary>
        public Decimal? FractionCount { get; set; }

        /// <summary>
        /// 选项A
        /// </summary>
        public String A { get; set; }

        /// <summary>
        /// 选项B
        /// </summary>
        public String B { get; set; }

        /// <summary>
        /// 选项C
        /// </summary>
        public String C { get; set; }

        /// <summary>
        /// 选项D
        /// </summary>
        public String D { get; set; }

        /// <summary>
        /// Deleted
        /// </summary>
        public Boolean? Deleted { get; set; }

        /// <summary>
        /// Analysis
        /// </summary>
        public String Analysis { get; set; }

    }
}