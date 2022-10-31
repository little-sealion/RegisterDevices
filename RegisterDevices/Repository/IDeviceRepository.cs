using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RegisterDevices.Models;

namespace RegisterDevices.Repository
{
    public interface IDeviceRepository
    {
         Task<string> InsertDevice(DeviceInfo device);
         Task<string> InsertDevices(IEnumerable<DeviceInfo> devices);
    }
}
