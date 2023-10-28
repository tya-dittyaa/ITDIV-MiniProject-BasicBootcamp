using System.ComponentModel.DataAnnotations;

namespace WearHouse2.Models.Requests
{
    public class LoginUserRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
