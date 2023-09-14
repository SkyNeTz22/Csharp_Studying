using System;
using System.Collections.Generic;

namespace ShopApp.MVVM.Model;

public partial class Inventory
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int CurrentStock { get; set; }

    public string Location { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<HistoricalInventory> HistoricalInventories { get; set; } = new List<HistoricalInventory>();

    public virtual ICollection<OrderList> OrderLists { get; set; } = new List<OrderList>();

    public virtual Product Product { get; set; } = null!;
}
