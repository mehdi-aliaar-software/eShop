using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using IM.Application.Contracts.Inventory;
using IM.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using SM.Infrastructure.EfCore;

namespace IM.Infrastructure.EfCore.Repository
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly InventoryContext _context;
        private readonly ShopContext _shopContext;

        public InventoryRepository(InventoryContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public EditInventory GetDetails(long id)
        {
            var result = _context
                .Inventory
                .Select(x => new EditInventory
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    unitPrice = x.unitPrice
                }).FirstOrDefault(x => x.Id == id);
            return result;
        }

        public Inventory Get(long productId)
        {
            var result = _context.Inventory.FirstOrDefault(x => x.ProductId == productId);
            return result;
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _context
                .Inventory
                .Select(x => new InventoryViewModel
                {
                    Id = x.Id,
                    UnitPrice = x.unitPrice,
                    ProductId = x.ProductId,
                    InStock = x.InStock,
                    CreationDate = x.CreationDate.ToFarsi(),
                    CurrentCount = x.CalculateCurrentCount()

                });
            if (searchModel.ProductId > 0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }

            if (searchModel.InStock)
            {
                query = query.Where(x => !x.InStock);
            }

            var inventory = query.OrderByDescending(x => x.Id).ToList();

            inventory.ForEach(x => { x.Product = products.FirstOrDefault(y => y.Id == x.ProductId)?.Name; }
            );

            return inventory;

        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            var inventory = _context.Inventory.FirstOrDefault(x => x.Id == inventoryId);
            var result = inventory
                .Operations
                .Select(x => new InventoryOperationViewModel
                {
                    Id = x.Id ,  ///*x.opertionid*/
                    Count = x.Count,
                    OrderId = x.OrderId,
                    CurrentCount = x.CurrentCount,
                    Description = x.Description,
                    Operation = x.Operation,
                    OperationDate = x.OperationDate.ToFarsi(),
                    OperatorId = x.OperatorId,
                    Operator = "مدیرسیستم",
                   
                }).ToList();
            return result ;
        }
    }
}