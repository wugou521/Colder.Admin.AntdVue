using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Coldairarrow.Business.Primary
{
    public interface IFractionsBusiness
    {
        Task<PageResult<FractionsDTO>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<FractionsViewDTO> GetTheDataAsync(string id);
        Task AddDataAsync(FractionsEditDTO data);
        Task UpdateDataAsync(FractionsEditDTO data);
        Task DeleteDataAsync(List<string> ids);
        Task<List<KeyValuePair<string, string>>> UploadFractionsAsync(string filepath);
    }

    [Map(typeof(Fractions))]
    public class FractionsDTO : Fractions
    {
        public List<string> AnswerList
        {
            get
            {
                if (Type != 1)
                {
                    return Answer.IsNullOrEmpty() ? new List<string>() : Answer.Replace("1", "A").Replace("2", "B").Replace("3", "C").Replace("4", "D").Split(',').ToList();
                }
                else
                {
                    return Answer.IsNullOrEmpty() ? new List<string>() : Answer.Replace("1", "正确").Replace("2", "错误").Replace("A", "正确").Replace("B", "错误").Split(',').ToList();
                }
            }
        }
        public string ParentName { get; set; }
        public string SchedulesName { get; set; }
        public string TypeName { get => Type == 1 ? "判断题" : Type == 2 ? "单选题" : Type == 3 ? "多选题" : Type == 4 ? "不定项" : Type == 5 ? "不定项子试题" : ""; }
    }

    [Map(typeof(Fractions))]
    public class FractionsViewDTO : Fractions
    {
        public List<string> AnswerList
        {
            get
            {
                if (Type != 1 && Type != 2)
                {
                    return Answer.IsNullOrEmpty() ? new List<string>() : Answer.Replace("A", "1").Replace("B", "2").Replace("C", "3").Replace("D", "4").Split(',').ToList();
                }
                else
                {
                    return null;
                }
            }
        }
    }

    [Map(typeof(Fractions))]
    public class FractionsEditDTO : Fractions
    {
        public List<string> AnswerList { get; set; }
    }

    public class UploadFractionsDTO
    {
        public string Filepath { get; set; }
    }
}