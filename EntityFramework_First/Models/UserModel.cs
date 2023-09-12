using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework_First.Models
{
    [Table("Users")]
    public partial class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Pass { get; set; }

        public DateTime? RegisteredDate { get; set; }

        public int UserFlag { get; set; }

        public bool Deleted { get; set; }
    }
}
