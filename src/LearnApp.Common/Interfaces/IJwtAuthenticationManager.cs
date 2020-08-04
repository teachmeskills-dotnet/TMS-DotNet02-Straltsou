using System.Collections.Generic;
using System.Security.Claims;

namespace LearnApp.Common.Interfaces
{
    /// <summary>
    /// Interface of authentication manager based on JSON web token implementation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Z"></typeparam>
    public interface IJwtAuthenticationManager<T,Z>
        where T: class
        where Z : class

    {
        /// <summary>
        /// Generate the JWT and refresh tokens based on giving parameters and IP address.
        /// </summary>
        /// <param name="parameters">Authentication parameters.</param>
        /// <param name="ipAddress">IP address of current user.</param>
        /// <returns></returns>
        T Authenticate(Z parameters, string ipAddress);

        /// <summary>
        /// Generate a new refresh token based on giving last updated refresh token.
        /// </summary>
        /// <param name="token">Refresh token.</param>
        /// <param name="ipAddress">IP address of current user.</param>
        /// <returns></returns>
        T RefreshToken(string token, string ipAddress);
    }
}
