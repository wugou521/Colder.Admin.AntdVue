using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Primary
{
    /// <summary>
    /// User
    /// </summary>
    [Table("User")]
    public class User
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// UserName
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        public String Phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public String Email { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// CreatorId
        /// </summary>
        public String CreatorId { get; set; }

        /// <summary>
        /// IsLock
        /// </summary>
        public Boolean? IsLock { get; set; }

    }
}