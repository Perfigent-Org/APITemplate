using APICoreTemplate.Dashboard.ClientApi;
using System;

namespace APICoreTemplate.Client.V1
{
    public interface IApiClient
    {
        IDBClient DB { get; }
        IUserClient Users { get; }
        void Authorization(string apiToken);
    }

    public class ApiClient : IApiClient, IDisposable
    {
        public const string DefaultBaseUrl = "https://localhost:44370/";
        public IDBClient DB { get; }
        public IUserClient Users { get; }

        private readonly NSwagHttpClient _httpClient;
        public ApiClient(string baseUrl, int concurrencyLimit)
        {
            if (concurrencyLimit < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(concurrencyLimit), "Concurrency limit must be greater than 0.");
            }

            // Share an HTTP client between all resources to limit concurrency across entire client
            _httpClient = new NSwagHttpClient(concurrencyLimit);

            DB = new DBClient(_httpClient) { BaseUrl = baseUrl };
            Users = new UserClient(_httpClient) { BaseUrl = baseUrl };
        }

        public ApiClient(int concurrencyLimit) : this(DefaultBaseUrl, concurrencyLimit) { }

        public void Authorization(string apiToken)
        {
            _httpClient.Authorization(apiToken);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}