using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework_First.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public DateTime RegisteredDate { get; set; }
        public int UserFlag { get; set; }
        public Boolean Deleted { get; set; }
    }
}
