using System;
using System.Collections.Generic;

namespace ShopAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Pass { get; set; }

    public DateTime? RegisteredDate { get; set; }

    public int UserFlag { get; set; }

    public bool Deleted { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Recovery> Recoveries { get; set; } = new List<Recovery>();
}
