﻿using LearnApp.DAL.Models;

namespace LearnApp.BLL
{
    /// <summary>
    /// Authentication response which contain information about JwtToken,RefreshToken,user id etc.
    /// </summary>
    public class AuthenticationResponse
    {
        /// <summary>
        /// User ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Username or login.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// JSON web token.
        /// </summary>
        public string JwtToken { get; set; }

        /// <summary>
        /// Refresh token.
        /// </summary>
        //[JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        /// <summary>
        /// Constructor which accepts parameters below.
        /// </summary>
        /// <param name="user">Application user.</param>
        /// <param name="jwtToken">JWT.</param>
        /// <param name="refreshToken">Refresh token.</param>
        public AuthenticationResponse(ApplicationUser user, string jwtToken, string refreshToken)
        {
            Id = user.Id;
            Username = user.Login;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
