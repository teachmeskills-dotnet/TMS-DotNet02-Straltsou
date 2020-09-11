using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LearnApp.DAL.Models
{
    /// <summary>
    /// Application user.
    /// </summary>
    public class ApplicationUser
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Login of user.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password of user.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Verification token for email service.
        /// </summary>
        public string VerificationToken { get; set; }

        /// <summary>
        /// Date of verification.
        /// </summary>
        public DateTime? Verified { get; set; }

        /// <summary>
        /// User is verified.
        /// </summary>
        public bool IsVerified => Verified.HasValue;

        /// <summary>
        /// Date of account creation.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Date of account update.
        /// </summary>
        public DateTime? Updated { get; set; }

        /// <summary>
        /// Role of user.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// List of saved cards related to specific user.
        /// </summary>
        public ICollection<Card> Cards { get; set; }

        /// <summary>
        /// List of refreshed tokens pinned to this user.
        /// </summary>
        [JsonIgnore]
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
