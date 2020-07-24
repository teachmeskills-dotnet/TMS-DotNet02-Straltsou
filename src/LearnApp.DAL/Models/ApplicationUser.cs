using System;
using System.Collections.Generic;
using System.Text;
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
        /// Password of user.
        /// </summary>
        public string Password { get; set; }

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
