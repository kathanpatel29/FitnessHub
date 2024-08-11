using System.ComponentModel.DataAnnotations;

namespace FitnessHub.Models
{
    public class EditUserViewModel
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        public string Role { get; set; } // Admin or User

        // Add any additional fields as needed
    }
}
