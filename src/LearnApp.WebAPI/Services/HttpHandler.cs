using Flurl;
using Flurl.Http;
using LearnApp.WebAPI.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnApp.WebAPI.Services
{
    /// <summary>
    /// Class which serves as a hendler for user requests.
    /// </summary>
    public class HttpHandler
    {
        private readonly IOptions<ApiConfig> _options;

        /// <summary>
        /// Default constuctor.
        /// </summary>
        public HttpHandler() { }

        /// <summary>
        /// Constructor which serve as transmitter of secret data.
        /// </summary>
        /// <param name="options">Secret data, keys.</param>
        public HttpHandler(IOptions<ApiConfig> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Get generalized JSON model.
        /// </summary>
        /// <typeparam name="T">Generic model.</typeparam>
        /// <param name="host">Host title.</param>
        /// <param name="segments">Endpoint set.</param>
        /// <param name="parameters">Set of query params.</param>
        /// <returns>JSON model.</returns>
        public async Task<T> GetJsonResultAsync<T>(string host, object[] segments, object parameters)
        {
            return await host
                .AppendPathSegments(segments)
                .SetQueryParams(parameters)
                .GetAsync()
                .ReceiveJson<T>();
        }

        /// <summary>
        /// Returns JSON Yandex model through connection to API.
        /// </summary>
        /// <param name="input">Incoming update.</param>
        /// <returns>JSON model.</returns>
        public async Task<YandexModel> GetYandexModel(string input)
        {
            var host = "https://translate.yandex.net/";
            var segments = new List<string>
            {
                "api",
                "v1.5",
                "tr.json",
                "translate"
            };
            object[] path = segments.ToArray();
            var parameters = new { key = _options.Value.YandexAPI, text = input, lang = "en-ru" };

            return await GetJsonResultAsync<YandexModel>(host, path, parameters);
        }

        /// <summary>
        /// Returns JSON Unsplash model through connection to API.
        /// </summary>
        /// <param name="input">Incoming update.</param>
        /// <returns>JSON model.</returns>
        public async Task<ImageModel> GetUnsplashModel(string input)
        {
            var host = "https://api.unsplash.com/";
            var segments = new List<string>
            {
                "photos",
                "random"
            };
            object[] path = segments.ToArray();
            var parameters = new { client_id = _options.Value.UnsplashAPI, query = input };

            return await GetJsonResultAsync<ImageModel>(host, path, parameters);
        }

        /// <summary>
        /// Get JSON model from Datamuse API.
        /// </summary>
        /// <param name="userInput">User text input.</param>
        /// <returns>JSON model.</returns>
        public async Task<List<ContextModel>> GetContextModelAsync(string input)
        {
            var response = await "https://api.datamuse.com/"
                .AppendPathSegment("words")
                .SetQueryParams(new { ml = input, qe = "ml", max = "7", md = "d" })
                .GetAsync()
                .ReceiveJson<List<ContextModel>>();
            return response;
        }
    }
}
