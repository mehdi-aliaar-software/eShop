using DM.Application.Contracts.CustomerDiscount;
using DM.Domain.CustomerDiscountAgg;
using System.Linq.Expressions;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using SM.Infrastructure.EfCore;

namespace DM.Infrastructure.EfCore.Repository
{
    public class CustomerDiscountRepository: RepositoryBase<long, CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopContext _shopContext;

        public CustomerDiscountRepository(DiscountContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }


        public EditCustomerDiscount GetDetails(long id)
        {
            var result = _context.CustomerDiscounts
                .Select(x => new EditCustomerDiscount
                {
                    Id = x.Id,
                    DiscountRate = x.DiscountRate,
                    StartDate = x.StartDate.ToFarsi(),
                    EndDate = x.EndDate.ToFarsi(),
                    ProductId = x.ProductId,
                    Reason = x.Reason
                    
                })
                .FirstOrDefault(x => x.Id == id);
            return result;
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var products=_shopContext.Products.Select(x=>new {x.Id,x.Name}).ToList();

            var query = _context.CustomerDiscounts
                .Select(x => new CustomerDiscountViewModel
                {
                    Id = x.Id,
                    DiscountRate = x.DiscountRate,
                    StartDate = x.StartDate.ToFarsi(),
                    StartDateGr = x.StartDate,
                    EndDate = x.EndDate.ToFarsi(),
                    EndDateGr = x.EndDate,
                    ProductId = x.ProductId,
                    Reason = x.Reason,
                    CreationDate = x.CreationDate.ToFarsi()

                });
            if (searchModel.ProductId>0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }
            if (!string.IsNullOrWhiteSpace(searchModel.StartDate) )
            {
                var startDate = searchModel.StartDate.ToGeorgianDateTime(); // searchModel.StartDate to datetime
                query = query.Where(x => x.StartDateGr >= startDate);
            }
            if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
            {
                var endDate = searchModel.EndDate.ToGeorgianDateTime(); // searchModel.EndDate to datetime
                query = query.Where(x => x.EndDateGr <= endDate);
            }

            var discounts = query.OrderByDescending(x => x.Id).ToList();

            discounts.ForEach(discount=> 
                discount.Product = products.FirstOrDefault(x=>x.Id== discount.ProductId)?.Name);
            return discounts;
        }
    }
}
