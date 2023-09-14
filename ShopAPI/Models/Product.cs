﻿using System;
using System.Collections.Generic;

namespace ShopAPI.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool ProductStatus { get; set; }

    public int Stock { get; set; }

    public double Price { get; set; }

    public string Description { get; set; } = null!;

    public string Origin { get; set; } = null!;

    public int Warrancy { get; set; }

    public int? BrandId { get; set; }

    public int? CategoryId { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<HistoricalInventory> HistoricalInventories { get; set; } = new List<HistoricalInventory>();

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
