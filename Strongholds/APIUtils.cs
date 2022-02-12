using Newtonsoft.Json;
using System.Text;
using System.Net;

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

        public async Task<bool> CanGetSuccessfully(string url)
        {
            var response = await Client.GetAsync(url);
            return response.IsSuccessStatusCode;
        }

        public async Task<HttpResponseMessage> PostQueryString(string url, string property)
        {
            //TODO: does the API endpoint get the username from url or content?
            StringContent content = new StringContent(nameof(property) + "=" + property);

            return Client.PostAsync(url, content).Result;
        }

        public async Task<HttpResponseMessage> PostModel(string url, Object model)
        {
            // Serialise model
            //var json = JsonConvert.SerializeObject(model);
            string json = "{ 'contact_name':'sdfsd'}";

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Yeet the data
            var response= await Client.PostAsync(url, data);

            var status = response.StatusCode;

            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
            return response;
        }

    }
}
