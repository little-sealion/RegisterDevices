using System.Net.Http.Json;
using FluentAssertions;
using RegisterDevices.Models;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Diagnostics;
using Newtonsoft.Json;


namespace RegisterDevices.Test;

public class ControllerTest : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Startup> _factory;

    public ControllerTest(WebApplicationFactory<Startup> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();

    }


    [Fact]
    public async void PostDeviceShouldReturnCreated()
    {
        var payload = new RegisterDevicesRequest
        {
            correlationId = "84f84dc5-d0a7-440f-92e0-c926ad8aa709",
            devices = new DeviceInfoWithoutAssetId[]
            {
                new DeviceInfoWithoutAssetId
                {
                    id = "DVID000001",
                    type = "device type 1",
                    Name = "device 1 name",
                    location = "location 1"
                }
            }
        };
        var response = await _client.PostAsJsonAsync("/api/RegisterDevices", payload);
        var content = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        content.Should().Contain("DVID000001");
    }

    [Fact]
    public async void Post1000DeviceShouldReturnCreatedWithin10Minutes()
    {


        using var reader = new StreamReader("./SampleData/SampleData.json");
        var requestContent = reader.ReadToEnd() ?? "";
        var payload = JsonConvert.DeserializeObject<RegisterDevicesRequest>(requestContent);
        var stopWatch = new Stopwatch();

        stopWatch.Start();
        var response = await _client.PostAsJsonAsync("/api/RegisterDevices", payload);
        stopWatch.Stop();

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("DVID001000");
        stopWatch.Elapsed.Should().BeLessThan(TimeSpan.FromMinutes(10));
    }
}
