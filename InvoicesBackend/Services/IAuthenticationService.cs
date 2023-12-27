using InvoicesBackend.Models;

namespace InvoicesBackend.Services
{
    public interface IAuthenticationService
    {
        Task<TokenDTO> Register(RegisterRequestDTO request);
        Task<TokenDTO> Login(LoginRequestDTO request);
    }
}