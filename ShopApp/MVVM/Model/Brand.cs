using System.Collections.Generic;

namespace ShopApp.MVVM.Model;

public partial class Brand
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool BrandStatus { get; set; }

    public bool Deleted { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
