using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace APICoreTemplate.Client.V1
{
    internal class NSwagHttpClient : IDisposable
    {
        public const int NoConcurrencyLimit = -1;
        private readonly HttpClient _httpClient;
        private readonly SemaphoreSlim _semaphore;

        public NSwagHttpClient(int concurrencyLimit)
        {
            if (concurrencyLimit < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(concurrencyLimit), "Concurrency limit must be greater than 0.");
            }
            _httpClient = new HttpClient();
            _semaphore = new SemaphoreSlim(concurrencyLimit);
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        {
            await _semaphore.WaitAsync(cancellationToken);
            try
            {
                return await _httpClient.SendAsync(request, completionOption, cancellationToken);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public void Authorization(string Bearer)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Bearer);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
