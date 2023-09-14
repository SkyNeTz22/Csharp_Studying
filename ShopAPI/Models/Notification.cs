using System;
using System.Collections.Generic;

namespace ShopAPI.Models;

public partial class Notification
{
    public int Id { get; set; }

    public string Message { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
