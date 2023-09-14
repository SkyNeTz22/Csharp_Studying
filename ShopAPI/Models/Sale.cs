using System;
using System.Collections.Generic;

namespace ShopAPI.Models;

public partial class Sale
{
    public int Id { get; set; }

    public double AmountPurchased { get; set; }

    public DateTime DateCreated { get; set; }

    public bool Enabled { get; set; }

    public int? OrderId { get; set; }

    public virtual Order? Order { get; set; }
}
