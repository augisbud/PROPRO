using InvoicesBackend.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JwtInDotnetCore.Controllers
{
    [Route("authentication")]
    [ApiController]
    public class AuthController(IConfiguration config) : ControllerBase
    {
        private readonly IConfiguration _config = config;

        /// <summary>
        /// Authenticate via email and password
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenDTO))]
        public IActionResult Authenticate([FromBody] LoginRequest loginRequest)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("Jwt:Key")!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            return Ok(new TokenDTO(new JwtSecurityTokenHandler().WriteToken(Sectoken)));
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenDTO))]
        public IActionResult Register([FromBody] RegisterRequest registerRequest)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("Jwt:Key")!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            return Ok(new TokenDTO(new JwtSecurityTokenHandler().WriteToken(Sectoken)));
        }
    }
}