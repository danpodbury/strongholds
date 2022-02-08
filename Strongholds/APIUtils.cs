namespace Strongholds
{
    public class APIUtils
    {
        private readonly IHttpClientFactory _clientFactory;
        private HttpClient Client => _clientFactory.CreateClient();
        public APIUtils(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<string> GetResultFromAsync(string url)
        {
            var response = await Client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return "request was not successful";

            // Deserializing the response received from web api and storing into a list.
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetResultFromAsync(string url, string failureString)
        {
            try
            {
                return await GetResultFromAsync(url);
            }
            catch
            {
                return failureString;
            }

        }

        public async Task<HttpResponseMessage> PostQueryString(string url, string username)
        {
            //TODO: does the API endpoint get the username from url or content?
            StringContent content = new StringContent(nameof(username) + "=" + username);

            return Client.PostAsync(url, content).Result;
        }

    }
}
