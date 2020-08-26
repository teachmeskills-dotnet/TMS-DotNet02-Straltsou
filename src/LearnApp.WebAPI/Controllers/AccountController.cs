using LearnApp.BLL.Interfaces;
using LearnApp.Common.Helpers;
using LearnApp.DAL.Models;
using LearnApp.DAL.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using BC = BCrypt.Net.BCrypt;

namespace LearnApp.WebAPI.Controllers
{
    /// <summary>
    /// Controller which responsible for any interaction with account.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        private readonly IRepository<ApplicationUser> _repository;
        private readonly IEmailService _emailService;

        /// <summary>
        /// Constructor which resolves services below.
        /// </summary>
        /// <param name="context">Database context.</param>
        /// <param name="jwtAuthenticationManager">Jwt authentication manager.</param>
        /// <param name="repository">User repository.</param>
        public AccountController(ApplicationDbContext context,
            IJwtAuthenticationManager jwtAuthenticationManager,
            IRepository<ApplicationUser> repository,
            IEmailService emailService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _jwtAuthenticationManager = jwtAuthenticationManager ?? throw new ArgumentNullException(nameof(jwtAuthenticationManager));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        /// <summary>
        /// Authenticate the incoming data of user.
        /// </summary>
        /// <param name="parameters">Authentication parameters - login, password.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] AuthenticationParameters parameters)
        {
            var response = _jwtAuthenticationManager.Authenticate(parameters, IpAddress());

            if (response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect. No verification by email." });
            }

            return Ok(response);
        }

        /// <summary>
        /// Register new user by the incoming authentication parameters.
        /// </summary>
        /// <param name="parameters">Authentication parameters - Login(email), password.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] AuthenticationParameters parameters)
        {
            var registerSucceded = _jwtAuthenticationManager.Register(parameters, "https://learn-application.herokuapp.com"); //Request.Headers["origin"]
            if (!registerSucceded)
            {
                return BadRequest(new { message = "User with current login already exists." });
            }

            return Ok(new { message = "Registration successful, please check your email for verification instructions" });
        }

        /// <summary>
        /// Update the new refresh and JWT tokens with last updated refresh token help.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] string refreshToken)
        {
            //var refreshToken = Request.Cookies["refreshToken"];

            var response = _jwtAuthenticationManager.RefreshToken(refreshToken, IpAddress());

            if (response == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            return Ok(response);
        }

        /// <summary>
        /// Receives verification token from the email confirmation message and assign verification status.
        /// </summary>
        /// <param name="token">Verification token.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("verify-email")]
        public IActionResult VerifyEmail(string token)
        {
            var result = _jwtAuthenticationManager.VerifyEmail(token);
            if(!result)
            {
                return BadRequest(new { message = "Incorrect verification token."});
            }
            return Ok();
        }

        /// <summary>
        /// Establish the refresh token info into the cookie response.
        /// </summary>
        /// <param name="token">Refresh token.</param>
        private void SetTokenCookie(string token) // Use this method for transfer tokens in cookie.
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                IsEssential = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        /// <summary>
        /// Establish the IP address from the incoming request.
        /// </summary>
        /// <returns></returns>
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