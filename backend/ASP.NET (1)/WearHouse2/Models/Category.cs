
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WearHouse.Models
{
    public class Category

    {
        [Key]
        public int CategoryId { get; set; }

        [MaxLength(255)]
        [NotNull]
        public string Name { get; set; }
    }
}
