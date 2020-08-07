using LearnApp.BLL.Interfaces;
using LearnApp.DAL.Models;
using LearnApp.DAL.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace LearnApp.WebAPI.Controllers
{
    /// <summary>
    /// Controller for login and registration.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        private readonly IRepository<ApplicationUser> _repository;

        /// <summary>
        /// Constructor which resolves services below.
        /// </summary>
        /// <param name="context">Database context.</param>
        /// <param name="jwtAuthenticationManager">Jwt authentication manager.</param>
        /// <param name="repository">User repository.</param>
        public AccountController(ApplicationDbContext context,
            IJwtAuthenticationManager jwtAuthenticationManager,
            IRepository<ApplicationUser> repository)
        {
            _context = context;
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _repository = repository;
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
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(response);
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
        /// Register new user by the incoming authentication parameters.
        /// </summary>
        /// <param name="parameters">Authentication parameters - Login(email), password.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] AuthenticationParameters parameters)
        {
            var user = _context.Users.SingleOrDefault(x => x.Login == parameters.Username && x.Password == parameters.Password);
            if (user != null)
            {
                return BadRequest(new { message = "User with current login already exists." });
            }

            user = new ApplicationUser { Login = parameters.Username, Password = parameters.Password };
            _repository.CreateEntity(user);
            _repository.SaveChanges();

            return Ok();

        }

        /// <summary>
        /// Establish the refresh token info into the cookie response.
        /// </summary>
        /// <param name="token">Refresh token.</param>
        private void SetTokenCookie(string token)
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