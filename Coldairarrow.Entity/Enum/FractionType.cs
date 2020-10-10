using System;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.Entity.Enum
{
    /// <summary>
    /// 试题类型枚举
    /// </summary>
    public enum FractionType
    {
        /// <summary>
        /// 判断题
        /// </summary>
        判断题 = 1,
        /// <summary>
        /// 单选题
        /// </summary>
        单选题 = 2,
        /// <summary>
        /// 多选题
        /// </summary>
        多选题 = 3,
        /// <summary>
        /// 不定项
        /// </summary>
        不定项父试题 = 4,
        /// <summary>
        /// 不定项子试题
        /// </summary>
        不定项子试题 = 5
    }
}
