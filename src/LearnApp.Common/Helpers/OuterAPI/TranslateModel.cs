using System.Collections.Generic;

namespace LearnApp.Common.Helpers.OuterAPI
{
    /// <summary>
    /// Translate model.
    /// </summary>
    public class TranslateModel
    {
        /// <summary>
        /// List of found words or sentences.
        /// </summary>
        public List<Outputs> Outputs { get; set; }
    }

    /// <summary>
    /// Searching value.
    /// </summary>
    public class Outputs
    {
        /// <summary>
        /// Specific found word or sentence.
        /// </summary>
        public string Output { get; set; }
    }
}
