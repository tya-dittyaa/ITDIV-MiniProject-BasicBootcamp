using System.ComponentModel.DataAnnotations;

namespace WearHouse.Models.Requests
{
    public class CreateCategoryRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
