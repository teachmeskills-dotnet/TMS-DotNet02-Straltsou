using LearnApp.Common.Interfaces;
using LearnApp.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LearnApp.WebAPI.Controllers
{
    /// <summary>
    /// Controller for login and registration.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJwtAuthenticationManager<AuthenticationResponse, AuthenticationParameters> _jwtAuthenticationManager;

        public AccountController(IJwtAuthenticationManager<AuthenticationResponse, AuthenticationParameters> jwtAuthenticationManager)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] AuthenticationParameters parameters)
        {
            var response = _jwtAuthenticationManager.Authenticate(parameters, IpAddress());

            if (response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public IActionResult Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = _jwtAuthenticationManager.RefreshToken(refreshToken, IpAddress());

            if (response == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            SetTokenCookie(response.RefreshToken);

            return Ok(response);
        }


        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return Request.Headers["X-Forwarded-For"];
            }
            else
            {
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }
    }
}