using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Primary
{
    /// <summary>
    /// BuyRecord
    /// </summary>
    [Table("BuyRecord")]
    public class BuyRecord
    {

        /// <summary>
        /// Id
        /// </summary>
        public String Id { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public String UserId { get; set; }

        /// <summary>
        /// 课程ID
        /// </summary>
        public String ClassesId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpireTime { get; set; }

        /// <summary>
        /// 创建人编号
        /// </summary>
        public String CreatorId { get; set; }

    }
}