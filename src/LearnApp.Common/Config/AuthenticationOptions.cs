using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearnApp.Common.Config
{
    /// <summary>
    /// Options class for JSON web token.
    /// </summary>
    public class AuthenticationOptions
    {
        public const string ISSUER = "MyAuthServer";

        public const string AUDIENCE = "MyAuthClient";

        const string KEY = "mysupersecret_secretkey!123";

        public const int LIFETIME = 5;

        /// <summary>
        /// Get encrypted key.
        /// </summary>
        /// <returns></returns>
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
