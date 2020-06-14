using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnApp.WebAPI.Models
{
    /// <summary>
    /// Yandex translate model.
    /// </summary>
    public class YandexModel
    {
        /// <summary>
        /// Code of language.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Language.
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// Words array.
        /// </summary>
        public string[] Text { get; set; }
    }
}
