using System.ComponentModel.DataAnnotations;

namespace WearHouse.Models.Requests
{
    public class UpdateCategoryRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
