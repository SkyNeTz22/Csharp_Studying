using System;
using System.Collections.Generic;

namespace ShopAPI.Models;

public partial class OrderList
{
    public int Id { get; set; }

    public double Price { get; set; }

    public double Total { get; set; }

    public int? InventoryId { get; set; }

    public virtual Inventory? Inventory { get; set; }
}
