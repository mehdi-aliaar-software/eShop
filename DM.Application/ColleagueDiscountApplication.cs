using DM.Application.Contracts.ColleagueDiscount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using DM.Application.Contracts.CustomerDiscount;
using DM.Domain.ColleagueDiscountAgg;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DM.Application
{
    public class ColleagueDiscountApplication: IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operation=new OperationResult();
            if (_colleagueDiscountRepository.Exists(x=>x.ProductId==command.ProductId && x.DiscountRate==command.DiscountRate))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var colleagueDiscount = new ColleagueDiscount(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.Create(colleagueDiscount);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();   
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.GetBy(command.Id);
            if (colleagueDiscount == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_colleagueDiscountRepository.Exists(x => 
                    x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id!=command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            colleagueDiscount.Edit(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.GetBy(id);
            if (colleagueDiscount == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

           colleagueDiscount.Remove();
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.GetBy(id);
            if (colleagueDiscount == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            colleagueDiscount.Restore();
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            var result = _colleagueDiscountRepository.GetDetails(id);
            return result;
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            var result=_colleagueDiscountRepository.Search(searchModel);
            return result;
        }
    }
}
