using System;
using System.Collections.Generic;
using System.Text;

namespace LearnApp.DAL.Models
{
    /// <summary>
    /// Card model for saving the information from client.
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Remembered word.
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// Remembered definition.
        /// </summary>
        public string Definition { get; set; }
    }
}
