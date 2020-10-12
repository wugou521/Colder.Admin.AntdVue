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
        public string FractionTypeName { get => FractionType.GetDescription(); }
        //public string IsHiddenName { get => IsHidden.Value ? "隐藏" : "不隐藏"; }
        public string ScheduleName { get; set; }
    }
}