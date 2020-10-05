using Coldairarrow.Entity.Primary;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Primary
{
    public interface IFractionTypesBusiness
    {
        Task<PageResult<FractionTypesDTO>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<FractionTypes> GetTheDataAsync(string id);
        Task AddDataAsync(FractionTypes data);
        Task UpdateDataAsync(FractionTypes data);
        Task DeleteDataAsync(List<string> ids);
    }

    [Map(typeof(FractionTypes))]
    public class FractionTypesDTO : FractionTypes
    {
        public string FractionTypeName { get => FractionType.Value == 1 ? "判断题" : FractionType.Value == 2 ? "单选题" : FractionType.Value == 3 ? "多选题" : FractionType.Value == 4 ? "不定项" : FractionType.Value == 5 ? "不定项子题" : ""; }
        public string IsHiddenName { get => IsHidden.Value ? "隐藏" : "不隐藏"; }
    }
}