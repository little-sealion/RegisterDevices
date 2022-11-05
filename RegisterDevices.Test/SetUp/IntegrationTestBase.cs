using System;
using Microsoft.Extensions.DependencyInjection;
using RegisterDevices.Models;

namespace RegisterDevices.Test.SetUp
{
    public class IntegrationTestBase:IClassFixture<IntegrationTestFactory<Startup,DeviceContext>>
    {
        public readonly IntegrationTestFactory<Startup, DeviceContext> Factory;
        public readonly DeviceContext DbContext;
        public IntegrationTestBase(IntegrationTestFactory<Startup,DeviceContext> factory)
        {
            Factory = factory;
            var scope = factory.Services.CreateScope();
            DbContext = scope.ServiceProvider.GetRequiredService<DeviceContext>();
        }
    }
}

