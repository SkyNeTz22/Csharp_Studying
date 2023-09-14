using System;
using System.Collections.Generic;

namespace ShopAPI.Models;

public partial class Recovery
{
    public int Id { get; set; }

    public string Token { get; set; } = null!;

    public DateTime Expiry { get; set; }

    public int? UserId { get; set; }

    //public virtual User? User { get; set; }
}
