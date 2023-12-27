namespace InvoicesBackend.Models
{
    public class ErrorDTO(string message) 
    {
        public string Message { get; set; } = message;
    }
}