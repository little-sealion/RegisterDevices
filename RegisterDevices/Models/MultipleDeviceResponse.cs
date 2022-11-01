using System;
using System.Collections.Generic;

namespace RegisterDevices.Models
{
    public class MultipleDeviceResponse
    {
        public IEnumerable<SingleDeviceResponse> devices { get; set; }
    }
}
