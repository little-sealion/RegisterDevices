using System;
using System.Collections.Generic;

namespace RegisterDevices.Models
{
    public class RegisterDevicesRequest
    {
        public string correlationId { get; }
        public IEnumerable<DeviceInfoWithoutAssetId> devices { get;}
    }
}
