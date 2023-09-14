using System;

namespace ShopApp.MVVM.Model;

public partial class Cart
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int InventoryId { get; set; }

    public double Price { get; set; }

    public int Items { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual Inventory Inventory { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
