namespace LearnApp.WebAPI.Models
{
    /// <summary>
    /// Class which serve as the secret data model.
    /// </summary>
    public class ApiConfig
    {
        /// <summary>
        /// Yandex translate API key.
        /// </summary>
        public string YandexAPI { get; set; }

        /// <summary>
        /// Unsplash service API key.
        /// </summary>
        public string UnsplashAPI { get; set; }
    }
}
