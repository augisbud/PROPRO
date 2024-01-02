using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using InvoicesBackend.Exceptions;
using InvoicesBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace InvoicesBackend.Services
{
    public class AuthenticationService(IConfiguration configuration, IMapper mapper, UserManager<IdentityUser> userManager) : IAuthenticationService
    {
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IConfiguration _configuration = configuration;

        public async Task<TokenDTO> Register(RegisterRequestDTO request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not null || await _userManager.FindByNameAsync(request.UserName) is not null)
            {
                throw new UserAlreadyExistsException(request.Email, request.UserName);
            }

            var user = _mapper.Map<IdentityUser>(request);

            var result = await _userManager.CreateAsync(user, request.Password);

            if(!result.Succeeded)
            {
                throw new UserCreationFailedException(request.UserName, result.Errors);
            }

            return await Login(new(request.UserName, request.Password));
        }

        public async Task<TokenDTO> Login(LoginRequestDTO request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new UnauthorizedAccessException();
            }

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, request.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            return GetToken(authClaims);
        }

        private TokenDTO GetToken(IEnumerable<Claim> authClaims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                authClaims,
                expires: DateTime.Now.AddSeconds(31556926),
                signingCredentials: credentials
            );

            return new(new JwtSecurityTokenHandler().WriteToken(Sectoken));
        }
    }
}