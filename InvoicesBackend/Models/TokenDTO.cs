namespace InvoicesBackend.Models
{
    public class TokenDTO(string token) 
    {
        public string Token { get; set; } = token;
    }
}