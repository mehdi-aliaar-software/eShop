namespace IM.Application.Contracts.Inventory;

public class InventoryViewModel
{
    public long Id { get; set; }
    public string Product { get; set; }
    public long ProductId { get; private set; }
    public double unitPrice { get; private set; }
    public bool InStock { get; set; }
    public long CurrentCount { get; set; }
}