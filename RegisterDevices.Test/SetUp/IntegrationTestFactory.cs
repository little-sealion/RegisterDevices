//using System;
//using DotNet.Testcontainers.Builders;
//using DotNet.Testcontainers.Configurations;
//using DotNet.Testcontainers.Containers;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.AspNetCore.TestHost;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using RegisterDevices.Models;

//namespace RegisterDevices.Test.SetUp
//{
//    public class IntegrationTestFactory<TProgram,TDbContext>:WebApplicationFactory<TProgram>,IAsyncLifetime
//        where TProgram : class where TDbContext: DbContext
//    {
//        private readonly TestcontainerDatabase _container;

//        public IntegrationTestFactory()
//        {
//            _container = new TestcontainersBuilder<MsSqlTestcontainer>()
//                .WithDatabase(new MsSqlTestcontainerConfiguration
//                { Password = "localdevpassword#123"})
//                .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
//                .WithCleanUp(true)
//                .Build();
//        }

//        protected override void ConfigureWebHost(IWebHostBuilder builder)
//        {
//            builder.ConfigureTestServices(services =>
//            {
//                services.RemoveDbContext<TDbContext>();
//                services.AddDbContext<TDbContext>(options => { options.UseSqlServer(_container.ConnectionString); });

//            });
//        }

//        public async Task InitializeAsync()
//        {
//            await _container.StartAsync();
//        }

//        async Task IAsyncLifetime.DisposeAsync()
//        {
//            await _container.StopAsync();
//        }
//    }
//}

