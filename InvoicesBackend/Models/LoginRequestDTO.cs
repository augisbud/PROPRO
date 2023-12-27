using System.ComponentModel.DataAnnotations;

namespace InvoicesBackend.Models
{
    public class LoginRequestDTO(string userName, string password)
    {
        [Required]
        public string UserName { get; set; } = userName;
        [Required]
        public string Password { get; set; } = password;
    }
}
