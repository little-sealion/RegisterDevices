using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return device.DeviceId;
        }

        public async Task<string> InsertDevices(IEnumerable<DeviceInfo> devices)
        {
            _context.Devices.AddRange(devices);
            await _context.SaveChangesAsync();
            return devices.Last().DeviceId;
        }

   
    }
}
