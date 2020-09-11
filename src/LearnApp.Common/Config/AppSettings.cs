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

        /// <summary>
        /// Data from whom email message sent.
        /// </summary>
        public string EmailFrom { get; set; }

        /// <summary>
        /// Email service host.
        /// </summary>
        public string SmtpHost { get; set; }

        /// <summary>
        /// Email service port.
        /// </summary>
        public int SmtpPort { get; set; }

        /// <summary>
        /// Current processing user.
        /// </summary>
        public string SmtpUser { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string SmtpPass { get; set; }
    }
}
