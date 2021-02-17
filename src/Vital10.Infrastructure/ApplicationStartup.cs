using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Vital10.Infrastructure.Database;
using Vital10.Infrastructure.Logging;
using Vital10.Infrastructure.Processing;
using Serilog;
using Vital10.Infrastructure.Domain;

namespace Vital10.Infrastructure
{
    public class ApplicationStartup
    {
        public static IServiceProvider Initialize(
            IServiceCollection services,
            string connectionString,
            ILogger logger)
        {
            var serviceProvider = CreateAutofacServiceProvider(
                services, 
                connectionString, 
                logger);

            return serviceProvider;
        }

        private static IServiceProvider CreateAutofacServiceProvider(
            IServiceCollection services,
            string connectionString,
            ILogger logger)
        {
            // Create a Autofac container builder
            var container = new ContainerBuilder();

            // Read service collection to Autofac
            container.Populate(services);

            // Use and configure Autofac
            container.RegisterModule(new LoggingModule(logger));
            container.RegisterModule(new DataAccessModule(connectionString));
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new DomainModule());

            // build the Autofac container
            var buildContainer = container.Build();

            return new AutofacServiceProvider(buildContainer);
        }
    }
}