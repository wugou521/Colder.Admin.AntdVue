using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Primary
{
    /// <summary>
    /// SystemConfig
    /// </summary>
    [Table("SystemConfig")]
    public class SystemConfig
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// Logo地址
        /// </summary>
        public String LogoUrl { get; set; }

        /// <summary>
        /// SEO关键字
        /// </summary>
        public String SeoKeyword { get; set; }

        /// <summary>
        /// ICP备案号
        /// </summary>
        public String ICP { get; set; }

        /// <summary>
        /// 公安网备
        /// </summary>
        public String ICP2 { get; set; }

        /// <summary>
        /// 幻灯片地址集合
        /// </summary>
        public String Slides { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

    }
}