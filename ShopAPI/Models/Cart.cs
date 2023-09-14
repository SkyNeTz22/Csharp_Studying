using System;
using System.Collections.Generic;

namespace ShopAPI.Models;

public partial class Cart
{
    public int Id { get; set; }

    public double Price { get; set; }

    public int Items { get; set; }

    public DateTime DateCreated { get; set; }

    public int? UserId { get; set; }

    public int? InventoryId { get; set; }

    public virtual Inventory? Inventory { get; set; }

    public virtual User? User { get; set; }
}
