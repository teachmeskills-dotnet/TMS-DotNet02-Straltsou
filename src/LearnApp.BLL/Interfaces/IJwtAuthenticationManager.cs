using LearnApp.Common.Helpers;
using LearnApp.DAL.Models;

namespace LearnApp.BLL.Interfaces
{
    /// <summary>
    /// Interface of authentication and authorization manager based on JSON web token implementation.
    /// </summary>
    public interface IJwtAuthenticationManager
    {
        /// <summary>
        /// Generate and attaches to response the JWT and refresh token based on giving parameters and IP address.
        /// </summary>
        /// <param name="parameters">Authentication parameters - login, password.</param>
        /// <param name="ipAddress">IP address of current user.</param>
        /// <returns></returns>
        AuthenticationResponse Authenticate(AuthenticationParameters parameters, string ipAddress);

        /// <summary>
        /// Registering a new user by the giving request parameters.
        /// </summary>
        /// <param name="parameters">Authentication request parameters.</param>
        /// <param name="origin">IP address of current user.</param>
        /// <returns></returns>
        bool Register(AuthenticationParameters parameters, string origin);

        /// <summary>
        /// Generate a new refresh token based on giving last updated refresh token.
        /// </summary>
        /// <param name="token">Refresh token.</param>
        /// <param name="ipAddress">IP address of current user.</param>
        /// <returns></returns>
        AuthenticationResponse RefreshToken(string token, string ipAddress);

        /// <summary>
        /// Verificate user by the giving token from email message.
        /// </summary>
        /// <param name="token">Verification token.</param>
        /// <returns></returns>
        bool VerifyEmail(string token);
    }
}
