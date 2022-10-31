using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegisterDevices.Models;
using RegisterDevices.Repository;
using RegisterDevices.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RegisterDevices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterDevicesController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IGetInventoryIdService _getInventoryIdService;

        public RegisterDevicesController(IDeviceRepository deviceRepository, IGetInventoryIdService getInventoryIdService)
        {
            _deviceRepository = deviceRepository;
            _getInventoryIdService = getInventoryIdService;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] RegisterDevicesRequest request)
        {
            var devicesWithoutAssetId = request.devices;
            var deviceIds = devicesWithoutAssetId.Select(device => device.id).ToArray();
            string result;

            if(deviceIds.Length == 1)
            {
                var theDevice = devicesWithoutAssetId.First();
                var response = await _getInventoryIdService.GetSingleInventoryId(deviceIds[0]);
                
                result = await _deviceRepository.InsertDevice
                    (
                        new DeviceInfo
                        {DeviceId = theDevice.id,Name=theDevice.Name,Location = theDevice.location,
                        Type=theDevice.type,AssetId=response.assetId
                        });
            }
            else
            {
                var response = await _getInventoryIdService.GetMultipleInventoryId(deviceIds);
                var query1 = from o in devicesWithoutAssetId
                             select new DeviceInfo
                             {
                                 DeviceId = o.id,
                                 Name = o.Name,
                                 Location = o.location,
                                 Type = o.type
                             };

                var query2 = from o in response.devices
                             select new DeviceInfo
                             {
                                 DeviceId = o.deviceId,
                                 AssetId = o.assetId
                             };
                
                var assetIds = response.devices;
                var devices = query1.Concat(query2);
                result = await _deviceRepository.InsertDevices(devices);
            }

            return Created(string.Empty, result);
            
        }

    }
}
