using _0_Framework.Domain;
using DM.Application.Contracts.ColleagueDiscount;
using DM.Application.Contracts.CustomerDiscount;

namespace DM.Domain.ColleagueDiscountAgg
{
    public interface IColleagueDiscountRepository:IRepository<long, ColleagueDiscount>
    {
        EditColleagueDiscount GetDetails(long id);
        List<ColleagueDiscountViewModel> Search (ColleagueDiscountSearchModel searchModel);
    }
}
