using _0_Framework.Application;
using IM.Application.Contracts.Inventory;
using IM.Domain.InventoryAgg;

namespace IM.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public OperationResult Create(CreateInventory command)
        {
            var operation = new OperationResult();
            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var inventory = new Inventory(command.ProductId, command.unitPrice);

            _inventoryRepository.Create(inventory);
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.Id);
            if (inventory == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            //var inventory = new Inventory(command.ProductId, command.unitPrice);

            inventory.Edit(command.ProductId, command.unitPrice);
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            const long operatorId = 1;  // until we set up the users specs.
            inventory.Increase(command.Count, operatorId, command.Description);
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            const long operatorId = 1;  // until we set up the users specs.
            //const long orderId = 0; // for store-keeper
            long orderId = command.OrderId > 0 ? command.OrderId : 0; // 0 = for store-keeper
            inventory.Reduce(command.Count, operatorId, command.Description, orderId);
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            var operation = new OperationResult();
            long operatorId = 1; // until we set up the users specs.
            foreach (var item in command)
            {
                var inventory = _inventoryRepository.Get(item.ProductId);
                inventory.Reduce(item.Count, operatorId, item.Description, item.OrderId);
            }

            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditInventory GetDetails(long id)
        {
            var result = _inventoryRepository.GetDetails(id);
            return result;
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var result = _inventoryRepository.Search(searchModel);
            return result;
        }
    }
}
