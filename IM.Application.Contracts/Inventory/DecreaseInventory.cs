﻿namespace IM.Application.Contracts.Inventory;

public class DecreaseInventory
{
    public long InventoryId { get; set; }
    public long Count { get; set; }
    public string Description { get; set; }
    public long OrderId { get; set; }

}