namespace ShopApp.MVVM.Model;

public partial class Notification
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Message { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
