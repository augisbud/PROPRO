using InvoicesBackend.Models;
using InvoicesBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace JwtInDotnetCore.Controllers
{
    [Route("authentication")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        /// <summary>
        /// Authenticate via email and password
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenDTO))]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequestDTO requestDTO)
        {
            try
            {
                return Ok(await _authenticationService.Login(requestDTO));
            }
            catch
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDTO))]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO requestDTO)
        {
            try
            {
                return Ok(await _authenticationService.Register(requestDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDTO(ex.Message));
            }
        }
    }
}