namespace Coldairarrow.Util
{
    /// <summary>
    /// 通用条件查询DTO
    /// </summary>
    public class ConditionDTO
    {
        public string Condition { get; set; }
        public string Keyword { get; set; }
        public bool All { get; set; }
        public string SchedulesId { get; set; }
        /// <summary>
        /// 题目类型 4不定项父题目
        /// </summary>
        public int FractionType { get; set; }
    }
}
