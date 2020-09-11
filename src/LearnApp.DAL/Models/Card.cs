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

        /// <summary>
        /// User identifier. 
        /// </summary>
        public int ApplicationUserId { get; set; }

        /// <summary>
        /// Navigational property for EF Core. 
        /// </summary>
        public ApplicationUser User { get; set; }
    }
}
