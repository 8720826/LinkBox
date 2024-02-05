using FluentMigrator.Runner;
using LinkBox.Migrator.Migrations;
using LinkBox.Template;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkBox.Migrator
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMigrate(this IServiceCollection services,string path)
        {

            var serviceProvider = services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    // Set the connection string
                    .WithGlobalConnectionString($"Data Source = {path}")
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(InitTables).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);

            using (var scope = serviceProvider.CreateScope())
            {
                // Instantiate the runner
                var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
                // Execute the migrations
                runner.MigrateUp();
            }
            return services;
        }

        public static IServiceCollection AddTemplate(this IServiceCollection services)
        {

            var serviceProvider = services.AddLogging().BuildServiceProvider(false);

            using (var scope = serviceProvider.CreateScope())
            {

                var service = serviceProvider.GetRequiredService<ITemplateService>();

                service.Reset("index.html");
                service.Reset("index.js");
                service.Reset("index.css");

            }
            return services;
        }

    }
}
