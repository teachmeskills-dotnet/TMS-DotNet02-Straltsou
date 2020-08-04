using System;
using System.Collections.Generic;
using System.Text;

namespace LearnApp.DAL.DTO
{
    /// <summary>
    /// Card data transfer object.
    /// </summary>
    public class CardDto
    {
        /// <summary>
        /// Remembered word.
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// User identifier. 
        /// </summary>
        public int ApplicationUserId { get; set; }

        /// <summary>
        /// Remembered definition.
        /// </summary>
        public string[] Definition { get; set; }
    }
}
