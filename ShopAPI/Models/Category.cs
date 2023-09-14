using System;
using System.Collections.Generic;

namespace ShopAPI.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool CategoryStatus { get; set; }

    public bool Deleted { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
