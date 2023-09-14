using System;
using System.Collections.Generic;

namespace ShopApp.MVVM.Model;

public partial class Order
{
    public int Id { get; set; }

    public string RefCode { get; set; } = null!;

    public int UserId { get; set; }

    public string DeliveryAddress { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;

    public double AmountPurchased { get; set; }

    public int OrderStatus { get; set; }

    public bool? Paid { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    //public virtual User User { get; set; } = null!;
}
