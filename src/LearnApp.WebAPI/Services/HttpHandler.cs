using Flurl;
using Flurl.Http;
using System.Threading.Tasks;

namespace LearnApp.WebAPI.Services
{
    /// <summary>
    /// Class which serves as a hendler for user requests.
    /// </summary>
    public class HttpHandler
    {
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
    }
}
