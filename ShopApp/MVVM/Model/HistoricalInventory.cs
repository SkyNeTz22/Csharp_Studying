using System;
using System.Collections.Generic;

namespace ShopApp.MVVM.Model;

public partial class HistoricalInventory
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int InventoryId { get; set; }

    public int Sold { get; set; }

    public int AllTimeStock { get; set; }

    public virtual Inventory Inventory { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
