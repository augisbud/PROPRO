using System.ComponentModel.DataAnnotations;

namespace InvoicesBackend.Models
{
    public class RegisterRequestDTO(string email, string userName, string password)
    {
        [Required]
        public string Email { get; set; } = email;
        [Required]
        public string UserName { get; set; } = userName;
        [Required]
        public string Password { get; set; } = password;
    }
}
