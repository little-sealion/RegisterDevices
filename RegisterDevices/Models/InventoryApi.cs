using System;
namespace RegisterDevices.Models
{
    public class InventoryApi
    {
        public Uri BaseUrl { get; set; } = new Uri("http://tech-assessment.vnext.com.au/api/devices/");
        public string GetRequestApiKey { get; set; }
        public string PostRequestApiKey { get; set; }
    }
}
