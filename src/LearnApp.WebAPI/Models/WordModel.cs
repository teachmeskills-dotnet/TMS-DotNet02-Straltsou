﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnApp.WebAPI.Models
{
    /// <summary>
    /// Root JSON object of context word.
    /// </summary>
    public class WordModel
    {
        /// <summary>
        /// Exact word.
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// Word id.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Special tags for word.
        /// </summary>
        public string[] Tags { get; set; }

        /// <summary>
        /// Definition set of word.
        /// </summary>
        public string[] Defs { get; set; }
    }
}
