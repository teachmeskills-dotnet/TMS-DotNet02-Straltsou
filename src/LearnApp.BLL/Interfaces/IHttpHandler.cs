using Flurl;
using Flurl.Http;
using LearnApp.Core.Models;
using LearnApp.DAL.Models.ImageModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearnApp.BLL.Interfaces
{
    /// <summary>
    /// Interface which serves as a handler for user requests.
    /// </summary>
    public interface IHttpHandler
    {
        /// <summary>
        /// Returns JSON Yandex model through connection to API.
        /// </summary>
        /// <param name="input">Incoming update.</param>
        /// <returns>JSON model.</returns>
        public Task<YandexModel> GetYandexModelAsync(string input);

        /// <summary>
        /// Returns JSON Unsplash model through connection to API.
        /// </summary>
        /// <param name="input">Incoming update.</param>
        /// <returns>JSON model.</returns>
        public Task<ImageModel> GetUnsplashModelAsync(string input);

        /// <summary>
        /// Get JSON model from Datamuse API.
        /// </summary>
        /// <param name="userInput">User text input.</param>
        /// <returns>JSON model.</returns>

        public Task<List<ContextModel>> GetContextModelAsync(string input);
    }
}
