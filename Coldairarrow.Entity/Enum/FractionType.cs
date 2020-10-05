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
        TrueOrFalse = 1,
        /// <summary>
        /// 单选题
        /// </summary>
        SingleChoice = 2,
        /// <summary>
        /// 多选题
        /// </summary>
        Multiple = 3,
        /// <summary>
        /// 不定项
        /// </summary>
        Indefinite = 4,
        /// <summary>
        /// 不定项子试题
        /// </summary>
        IndefiniteChild = 5
    }
}
