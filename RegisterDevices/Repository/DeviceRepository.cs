using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegisterDevices.Models;

namespace RegisterDevices.Repository
{
    public class DeviceRepository:IDeviceRepository
    {
        private readonly DeviceContext _context;
        public DeviceRepository(DeviceContext context)
        {
            _context = context;
        }

        public async Task<string> InsertDevice(DeviceInfo device)
        {

            await insertDevice(device);
            return $"device with deviceId:{device.DeviceId} has been inserted";
        }

        public async Task<string> InsertDevices(IEnumerable<DeviceInfo> devices)
        {
            foreach (var device in devices)
            {
                await insertDevice(device);
            }
            return $"devices from deviceId:{devices.First().DeviceId} to deviceId:{devices.Last().DeviceId} has been inserted";
        } 
   
        private Task<int> insertDevice(DeviceInfo device)
        {
           return _context.Database.ExecuteSqlRawAsync($"InsertOrUpdateDevice N'{device.DeviceId}',N'{device.Name}',N'{device.Location}',N'{device.Type}',N'{device.AssetId}'");
        }
    }
}
