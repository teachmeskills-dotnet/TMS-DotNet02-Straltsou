using System.Collections.Generic;

namespace LearnApp.Common.Helpers.OuterAPI.ImageModel
{
    /// <summary>
    /// Generalized model for image.
    /// </summary>
    public class ImageModel
    {
        /// <summary>
        /// List of images by the context.
        /// </summary>
        public List<ImageResult> Results { get; set; }

        /// <summary>
        /// Total images amount.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Total pages amount.
        /// </summary>
        public int Total_pages { get; set; }
    }
}
