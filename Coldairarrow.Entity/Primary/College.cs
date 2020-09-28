using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Primary
{
    /// <summary>
    /// College
    /// </summary>
    [Table("College")]
    public class College
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// CollegeName
        /// </summary>
        public String CollegeName { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public String CreateTime { get; set; }

        /// <summary>
        /// CreatorId
        /// </summary>
        public String CreatorId { get; set; }

    }
}