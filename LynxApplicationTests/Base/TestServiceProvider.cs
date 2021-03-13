using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Application;
using Lynx.Interfaces;
using Lynx.Infrastructure;
using LynxApplicationTests.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LynxApplicationTests.Base;

namespace LynxApplicationTests
{
    public class TestServiceProvider : IDisposable
    {
        private readonly ServiceProvider p_ServiceProvider;

        public string DatabaseName { get; private set; }

        public static TestServiceProvider InSQLContext(Action<ServiceCollection> additionalServices = null)
        {
            return new TestServiceProvider(null, additionalServices);
        }

        public static TestServiceProvider InMemoryContext(Action<ServiceCollection> additionalServices = null)
        {
            string _dbName = $"db-{Guid.NewGuid().ToString().Substring(0, 8)}";

            return new TestServiceProvider(_dbName, additionalServices);
        }

        private TestServiceProvider(string dbName, Action<ServiceCollection> additionalServices = null)
        {
            DatabaseName = dbName;

            ServiceCollection _services = new ServiceCollection();

            _services.AddScoped<IDateTime, TestDateTime>();
            _services.AddScoped<IJsonSerializer, TestJsonSerializer>();

            _services.AddApplication(includeValidators: true);
            _services.AddLogging();


            _services.AddInfrastructureUseInMemory(DatabaseName);
            _services.AddInfrastructure(null);

            if (additionalServices != null)
            {
                additionalServices.Invoke(_services);
            }

            p_ServiceProvider = _services.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return p_ServiceProvider.GetService<T>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                var _dbContext = GetService<DbContext>();

                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                }

                p_ServiceProvider.Dispose();

            }
        }
    }
}
