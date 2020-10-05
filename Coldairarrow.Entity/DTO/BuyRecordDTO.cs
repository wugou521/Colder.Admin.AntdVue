using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.Entity.DTO
{
    [Map(typeof(BuyRecord))]
    public class BuyRecordDTO : BuyRecord
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string ClassesName { get; set; }
    }
}
