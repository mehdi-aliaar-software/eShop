using _0_Framework.Domain;
using DM.Application.Contracts.CustomerDiscount;

namespace DM.Domain.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository:IRepository<long,CustomerDiscount>
    {
        EditCustomerDiscount GetDetails(long id);

        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
    }
}
