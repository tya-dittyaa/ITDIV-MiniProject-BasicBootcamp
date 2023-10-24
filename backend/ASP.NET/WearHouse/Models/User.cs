using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WearHouse.Models
{
    public class User
    {
        [Key]
        public string UserId { get; set; }

        [MaxLength(255)]
        [NotNull]
        public string Name { get; set; }

        [MaxLength(255)]
        [NotNull]
        public string Email { get; set; }

        [MaxLength(255)]
        [NotNull]
        public string Password { get; set; }
    }
}
