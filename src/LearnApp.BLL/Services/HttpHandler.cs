using Flurl;
using Flurl.Http;
using LearnApp.BLL.Interfaces;
using LearnApp.Common.Config;
using LearnApp.Common.Helpers.OuterAPI;
using LearnApp.Common.Helpers.OuterAPI.ImageModel;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnApp.BLL.Services
{
    /// <summary>
    /// Class which serves as a handler for user requests.
    /// </summary>
    public class HttpHandler : IHttpHandler
    {
        private readonly IOptions<ApiConfig> _options;

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
        public async Task<T> GetJsonResultAsync<T>(string host, object[] segments, object parameters, object headers = null)
        {
            return await host
                .WithHeaders(headers)
                .AppendPathSegments(segments)
                .SetQueryParams(parameters)
                .GetAsync()
                .ReceiveJson<T>();
        }

        /// <inheritdoc/>
        public async Task<TranslateModel> GetTranslateModelAsync(string input)
        {
            if(input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var host = "https://systran-systran-platform-for-language-processing-v1.p.rapidapi.com/";
            var segments = new List<string>
            {
                "translation",
                "text",
                "translate"
            };
            object[] path = segments.ToArray();

            var headers = new { x_rapidapi_host = "systran-systran-platform-for-language-processing-v1.p.rapidapi.com", 
                x_rapidapi_key = _options.Value.TranslateAPI };

            var parameters = new { source = "en", target = "ru", input = input };

            return await GetJsonResultAsync<TranslateModel>(host, path, parameters, headers);
        }

        /// <inheritdoc/>
        public async Task<ImageModel> GetUnsplashModelAsync(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var host = "https://api.unsplash.com/";
            var segments = new List<string>
            {
                "search",
                "photos"
            };
            object[] path = segments.ToArray();
            var parameters = new { client_id = _options.Value.UnsplashAPI, query = input };

            return await GetJsonResultAsync<ImageModel>(host, path, parameters);
        }

        /// <inheritdoc/>
        public async Task<List<ContextModel>> GetContextModelAsync(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var response = await "https://api.datamuse.com/"
                .AppendPathSegment("words")
                .SetQueryParams(new { ml = input, qe = "ml", max = "8", md = "d" })
                .GetAsync()
                .ReceiveJson<List<ContextModel>>();
            return response;
        }
    }
}
