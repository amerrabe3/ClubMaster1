using System.ComponentModel.DataAnnotations;

namespace ClubMaster3.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
} 