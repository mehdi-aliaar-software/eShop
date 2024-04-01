using _0_Framework.Domain;
using IM.Application.Contracts.Inventory;

namespace IM.Domain.InventoryAgg
{
    public interface IInventoryRepository: IRepository<long,Inventory>
    {
        EditInventory GetDetails(long id);
        Inventory GetBy(long productId);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
    }
}
