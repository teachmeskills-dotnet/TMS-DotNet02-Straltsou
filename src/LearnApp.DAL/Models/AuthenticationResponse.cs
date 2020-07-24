using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LearnApp.DAL.Models
{
    public class AuthenticationResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public AuthenticationResponse(ApplicationUser user, string jwtToken, string refreshToken)
        {
            Id = user.Id;
            Username = user.Login;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
