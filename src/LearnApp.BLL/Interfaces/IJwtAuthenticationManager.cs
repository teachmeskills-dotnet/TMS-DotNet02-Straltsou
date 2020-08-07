using LearnApp.DAL.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace LearnApp.BLL.Interfaces
{
    /// <summary>
    /// Interface of authentication manager based on JSON web token implementation.
    /// </summary>
    public interface IJwtAuthenticationManager
    {
        /// <summary>
        /// Generate the JWT and refresh tokens based on giving parameters and IP address.
        /// </summary>
        /// <param name="parameters">Authentication parameters.</param>
        /// <param name="ipAddress">IP address of current user.</param>
        /// <returns></returns>
        AuthenticationResponse Authenticate(AuthenticationParameters parameters, string ipAddress);

        /// <summary>
        /// Generate a new refresh token based on giving last updated refresh token.
        /// </summary>
        /// <param name="token">Refresh token.</param>
        /// <param name="ipAddress">IP address of current user.</param>
        /// <returns></returns>
        AuthenticationResponse RefreshToken(string token, string ipAddress);
    }
}
