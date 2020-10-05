using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Primary
{
    /// <summary>
    /// Classes
    /// </summary>
    [Table("Classes")]
    public class Classes
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 学院ID
        /// </summary>
        public String CollegeId { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        public String ClassName { get; set; }

        /// <summary>
        /// 班级图片路径
        /// </summary>
        public String IconUrl { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

    }
}