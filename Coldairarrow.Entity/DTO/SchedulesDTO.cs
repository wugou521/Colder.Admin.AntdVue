using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.Entity.DTO
{
    [Map(typeof(Schedules))]
    public class SchedulesDTO : Schedules
    {
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassesName { get; set; }
    }
}
