using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.Entity.DTO
{
    /// <summary>
    /// 
    /// </summary>
    [Map(typeof(Classes))]
    public class ClassesDTO : Classes
    {
        /// <summary>
        /// 
        /// </summary>
        public string CollegeName { get; set; }
    }
}
