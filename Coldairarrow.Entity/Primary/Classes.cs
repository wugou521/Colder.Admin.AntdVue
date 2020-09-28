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
        /// CollegeId
        /// </summary>
        public String CollegeId { get; set; }

        /// <summary>
        /// ClassName
        /// </summary>
        public String ClassName { get; set; }

        /// <summary>
        /// IconUrl
        /// </summary>
        public String IconUrl { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime? CreateTime { get; set; }

    }
}