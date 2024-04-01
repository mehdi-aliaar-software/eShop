using System.Security.Cryptography.X509Certificates;
using _0_Framework.Application;
using DM.Application.Contracts.CustomerDiscount;
using DM.Domain.CustomerDiscountAgg;

namespace DM.Application
{
    public class CustomerDiscountApplication: ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public CustomerDiscountApplication()
        {
            
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate= command.EndDate.ToGeorgianDateTime();
            var operation= new OperationResult();
            if (_customerDiscountRepository.Exists(x=>x.ProductId==command.ProductId && 
                                                      x.StartDate== startDate &&
                                                      x.EndDate==endDate))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var customerDiscount = new CustomerDiscount(command.ProductId, command.DiscountRate, startDate, endDate,
                command.Reason);
            _customerDiscountRepository.Create(customerDiscount);
            _customerDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            try
            {
                var startDate1 = command.StartDate.ToGeorgianDateTime();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            var operation=new OperationResult();
            var customerDiscount = _customerDiscountRepository.GetBy(command.Id);
            if (customerDiscount == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if (_customerDiscountRepository.Exists(x => x.ProductId == command.ProductId &&
                                                        x.StartDate == startDate &&
                                                        x.EndDate == endDate && x.Id!=command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            customerDiscount.Edit(command.ProductId,command.DiscountRate,startDate,endDate, command.Reason);
            _customerDiscountRepository.SaveChanges();
            return operation.Succeeded();

        }

        public EditCustomerDiscount GetDetails(long id)
        {
            var result=_customerDiscountRepository.GetDetails(id);
            return result;
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var result = _customerDiscountRepository.Search(searchModel);
            return result;
        }
    }
}
