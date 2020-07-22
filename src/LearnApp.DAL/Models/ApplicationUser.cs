using System;
using System.Collections.Generic;
using System.Text;

namespace LearnApp.DAL.Models
{
    /// <summary>
    /// Application user.
    /// </summary>
    public class ApplicationUser
    {
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
    }
}
