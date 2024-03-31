using DM.Application.Contracts.CustomerDiscount;
using _0_Framework.Domain;
using System.Collections.Generic;

namespace DM.Domain.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository:IRepository<long,CustomerDiscount>
    {
        EditCustomerDiscount GetDetail(long id);

        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
    }
}
