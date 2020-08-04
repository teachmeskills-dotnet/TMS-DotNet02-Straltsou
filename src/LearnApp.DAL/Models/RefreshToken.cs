using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace LearnApp.DAL.Models
{
    /// <summary>
    /// Refresh token class.
    /// </summary>
    [Owned]
    public class RefreshToken
    {
        /// <summary>
        /// Token id for database.
        /// </summary>
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// Token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Expiration date.
        /// </summary>
        public DateTime Expires { get; set; }

        /// <summary>
        /// Is expiered already.
        /// </summary>
        public bool IsExpired => DateTime.UtcNow >= Expires;

        /// <summary>
        /// Date of creation.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Created by current IP.
        /// </summary>
        public string CreatedByIp { get; set; }

        /// <summary>
        /// Revoke date.
        /// </summary>
        public DateTime? Revoked { get; set; }

        /// <summary>
        /// Revoked by current IP.
        /// </summary>
        public string RevokedByIp { get; set; }

        /// <summary>
        /// New token which replaced the last one..
        /// </summary>
        public string ReplacedByToken { get; set; }

        /// <summary>
        /// Token is active.
        /// </summary>
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
