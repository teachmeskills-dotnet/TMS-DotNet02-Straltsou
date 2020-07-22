using LearnApp.Common.Config;
using LearnApp.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace LearnApp.WebAPI.Controllers
{
    /// <summary>
    /// Controller for login and registration.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private List<ApplicationUser> people = new List<ApplicationUser>
        {
            new ApplicationUser { Login="admin@gmail.com", Password="12345", Role = "admin" },
            new ApplicationUser { Login="qwerty@gmail.com", Password="55555", Role = "user" }
        };

        [HttpPost]
        public IActionResult GetToken([FromBody] AuthParameters parameters)
        {
            var identity = GetIdentity(parameters);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthenticationOptions.ISSUER,
                audience: AuthenticationOptions.AUDIENCE,
                claims: identity.Claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(AuthenticationOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Ok(response);
        }


        private ClaimsIdentity GetIdentity(AuthParameters parameters)
        {
            ApplicationUser user = people.FirstOrDefault(u => u.Login == parameters.Username && u.Password == parameters.Password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };

                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            return null;
        }

    }
}