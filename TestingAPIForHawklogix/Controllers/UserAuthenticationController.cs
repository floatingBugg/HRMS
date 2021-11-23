using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Web.Helper;
using Web.Model.Common;

namespace Web_API.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IJwtAuthService _jwtAuth;
        private IConfiguration _config;
        Logger _logger;
        private IWebHostEnvironment _hostEnvironment;
        public UserAuthenticationController(IJwtAuthService jwtAuth, IConfiguration config, IWebHostEnvironment environment)
        {
            _jwtAuth = jwtAuth;
            _config = config;
            _hostEnvironment = environment;
            _logger = new Logger(_hostEnvironment);
        }
       
        [HttpPost("auth/userAuth")]
        public IActionResult Login([FromBody] UserCredential login)
        {
            try
            {
                IActionResult response = Unauthorized();
                string generatedToken = _jwtAuth.Authentication(login);

                if (!string.IsNullOrEmpty(generatedToken))
                {
                    response = Ok(new { token = generatedToken });
                }

                return response;
            }
            catch(Exception ex)
            {
                _logger.LogExceptions(ex);
                return null;
            }
        }
   
        [HttpPost("auth/register")]
        public IActionResult Register([FromBody] RegisterCredential register)
        {
            try
            {
                IActionResult response = Unauthorized();
                string generatedToken = _jwtAuth.Register(register);

                if (!string.IsNullOrEmpty(generatedToken))
                {
                    response = Ok(new { token = generatedToken });
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);
                return null;
            }
        }

    }
}
