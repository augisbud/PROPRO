using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JwtInDotnetCore.Controllers
{
    [Route("authenticate")]
    [ApiController]
    public class LoginController(IConfiguration config) : ControllerBase
    {
        private IConfiguration _config = config;

        [HttpPost]
        public IActionResult Post()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            var token =  new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(token);
        }
    }
}