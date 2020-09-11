using LearnApp.DAL.Models;

namespace LearnApp.DAL.DTO
{
    /// <summary>
    /// Card data transfer object.
    /// </summary>
    public class CardDto
    {
        /// <summary>
        /// Card identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Remembered word.
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// User identifier. 
        /// </summary>
        public int ApplicationUserId { get; set; }

        /// <summary>
        /// Navigational property for EF Core. 
        /// </summary>
        public ApplicationUser User { get; set; }

        /// <summary>
        /// Remembered definition.
        /// </summary>
        public string[] Definition { get; set; }
    }
}
