using System;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RegisterDevices.Models;

namespace RegisterDevices.Services
{
    public class GetInventoryIdService:IGetInventoryIdService
    {
        private readonly InventoryApi _inventoryApi;
        public const string HttpClientName = "InventoryHttpClient";
        private readonly HttpClient _httpClient;

        public GetInventoryIdService(IHttpClientFactory httpClientFactory,InventoryApi inventoryApi)
        {
            _inventoryApi = inventoryApi;
            _httpClient = httpClientFactory.CreateClient(HttpClientName);
        }

        public async Task<SingleDeviceResponse> GetSingleInventoryId(string deviceId)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri(_inventoryApi.BaseUrl + deviceId);
            request.Method = HttpMethod.Get;
            request.Headers.Add("x-functions-key", _inventoryApi.GetRequestApiKey);

            var response = await _httpClient.SendAsync(request); 
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<SingleDeviceResponse>(content);
            return dto;
        }

        public async Task<MultipleDeviceResponse> GetMultipleInventoryId(string[] deviceIdArray)
        {
            var data = new
            {
                deviceIds = deviceIdArray
            };
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri(_inventoryApi.BaseUrl);
            request.Method = HttpMethod.Post;
            request.Content = JsonContent.Create(data);
            request.Headers.Add("x-functions-key", _inventoryApi.PostRequestApiKey);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<MultipleDeviceResponse>(content);
            return dto;
        }

    }
}
