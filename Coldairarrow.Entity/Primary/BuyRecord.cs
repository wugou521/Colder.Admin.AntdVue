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
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public Int32 UserId { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// ExpireTime
        /// </summary>
        public DateTime? ExpireTime { get; set; }

        /// <summary>
        /// CreatorId
        /// </summary>
        public String CreatorId { get; set; }

    }
}