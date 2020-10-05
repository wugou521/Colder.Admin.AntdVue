using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Primary
{
    /// <summary>
    /// Menus
    /// </summary>
    [Table("Menus")]
    public class Menus
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Boolean? Status { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public String Content { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public String Actor { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

    }
}