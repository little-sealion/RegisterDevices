using System;
using Microsoft.Extensions.DependencyInjection;
using RegisterDevices.Models;

namespace RegisterDevices.Test.SetUp
{
    public class IntegrationTestBase:IClassFixture<IntegrationTestFactory<Startup,DeviceContext>>
    {
        public readonly IntegrationTestFactory<Startup, DeviceContext> Factory;

        public IntegrationTestBase(IntegrationTestFactory<Startup,DeviceContext> factory)
        {
            Factory = factory;
        }
    }
}

