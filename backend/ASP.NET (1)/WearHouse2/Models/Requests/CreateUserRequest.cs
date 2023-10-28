using System.ComponentModel.DataAnnotations;

namespace WearHouse.Models.Requests
{
    public class CreateUserRequest
    {

        [Required] 
        public string Name { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required] 
        public string Password { get; set; }

    }
}
