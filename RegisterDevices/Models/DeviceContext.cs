using System;
using Microsoft.EntityFrameworkCore;

namespace RegisterDevices.Models
{
    public class DeviceContext:DbContext
    {
        public DeviceContext(DbContextOptions<DeviceContext> options):base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<DeviceInfo> Devices { get; set; }
    }
}
