namespace LearnApp.BLL.Interfaces
{
    /// <summary>
    /// Service for sending email approval for registration.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Send a confirmation letter to the current registering subject.
        /// </summary>
        /// <param name="to">Whom to send email.</param>
        /// <param name="subject">Receiver.</param>
        /// <param name="html">Html message data.</param>
        /// <param name="from">From whom email sent.</param>
        void Send(string to, string subject, string html, string from = null);
    }
}
