using System;
using System.Collections.Generic;
using System.Text;

namespace LearnApp.Common.Config
{
    /// <summary>
    /// Application settings for JWT creation.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Security key which serves for creating JSON web token.
        /// </summary>
        public string SecretEncryptionKey { get; set; }
    }
}
