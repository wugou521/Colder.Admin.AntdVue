using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Primary
{
    /// <summary>
    /// AnswerRecordDetails
    /// </summary>
    [Table("AnswerRecordDetails")]
    public class AnswerRecordDetails
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 答案总表
        /// </summary>
        public String FractionRecordId { get; set; }

        /// <summary>
        /// 题目编号
        /// </summary>
        public String FractionId { get; set; }

        /// <summary>
        /// 所选答案
        /// </summary>
        public String Answer { get; set; }

        /// <summary>
        /// 是否正确
        /// </summary>
        public Boolean? IsRright { get; set; }

    }
}