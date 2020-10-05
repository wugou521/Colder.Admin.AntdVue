using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Primary
{
    /// <summary>
    /// Notices
    /// </summary>
    [Table("Notices")]
    public class Notices
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public String Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public String Actor { get; set; }

        /// <summary>
        /// 简述
        /// </summary>
        public String Resume { get; set; }

    }
}