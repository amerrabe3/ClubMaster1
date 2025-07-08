using System.ComponentModel.DataAnnotations;

namespace ClubMaster3.Models
{
    public class User
    {
            public int Id { get; set; }

            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            public string Role { get; set; } // "Admin", "Coach", "User", etc.

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            [Required]
            [Phone]
            public string PhoneNumber { get; set; }
        }


    }

