using System;
using System.Collections.Generic;

namespace ShopAPI.Models;

public partial class HistoricalInventory
{
    public int Id { get; set; }

    public int Sold { get; set; }

    public int AllTimeStock { get; set; }

    public int? ProductId { get; set; }

    public int? InventoryId { get; set; }

    public virtual Inventory? Inventory { get; set; }

    public virtual Product? Product { get; set; }
}
