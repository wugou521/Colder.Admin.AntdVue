using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Coldairarrow.Entity.Enum
{
    /// <summary>
    /// 试题类型枚举
    /// </summary>
    [Description("试题类型枚举")]
    public enum FractionType
    {
        /// <summary>
        /// 判断题
        /// </summary>
        [Description("判断题")]
        判断题 = 1,
        /// <summary>
        /// 单选题
        /// </summary>
        [Description("单选题")]
        单选题 = 2,
        /// <summary>
        /// 多选题
        /// </summary>
        [Description("多选题")]
        多选题 = 3,
        /// <summary>
        /// 不定项父题
        /// </summary>
        [Description("不定项父题")]
        不定项父题 = 4,
        /// <summary>
        /// 不定项子题
        /// </summary>
        [Description("不定项子题")]
        不定项子题 = 5
    }
}
