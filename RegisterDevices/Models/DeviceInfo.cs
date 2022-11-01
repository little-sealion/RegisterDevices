using System;
using System.ComponentModel.DataAnnotations;

namespace RegisterDevices.Models
{
    public class DeviceInfo
    {
        [Key]
        public string DeviceId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public string  AssetId { get; set; }
    }
}
