using EntityFramework_First.Models;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new ShopContext())
        {
            var users = context.Users.ToList(); // Retrieve all users
            foreach (var user in users)
            {
                Console.WriteLine($"User: {user.FirstName} {user.LastName}");
            }
        }
    }
}
