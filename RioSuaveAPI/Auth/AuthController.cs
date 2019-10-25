using Microsoft.AspNetCore.Mvc;
using RioSuaveLib;
using RioSuaveLib.JWT;

namespace RioSuaveAPI.Auth
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        public AuthController(AuthService authService, JwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<JwtDTO> PostAsync([FromBody] LoginCredentials credentials)
        {
            if (!_authService.CanAuthenticate(credentials.Username, credentials.Password))
                return Unauthorized();

            var jwt = _jwtService.Generate();

            return jwt;
        }

        private readonly AuthService _authService;
        private readonly JwtService _jwtService;
    }
}
