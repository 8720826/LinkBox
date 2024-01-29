using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.Migrator
{
    public class MigratorService: IMigratorService
    {
        private IMigrationRunner runner;

        public MigratorService(IMigrationRunner runner)
        {
            this.runner = runner;
        }

        public string MigrateUp()
        {
            var errs = ConsoleHook(() => runner.MigrateUp());
            var result = String.IsNullOrEmpty(errs) ? "Success" : errs;

            return result;
        }

        // Migrate down *to* the version.
        // If you want to migrate down the first migration, 
        // use any version # prior to that first migration.
        public string MigrateDown(long version)
        {
            var errs = ConsoleHook(() => runner.MigrateDown(version));
            var result = String.IsNullOrEmpty(errs) ? "Success" : errs;

            return result;
        }


        public string GetVersion()
        {
            var item = runner.MigrationLoader.LoadMigrations().FirstOrDefault().Value;
            return $"{item.Version}";
        }

        private string ConsoleHook(Action action)
        {
            var saved = Console.Out;
            var sb = new StringBuilder();
            var tw = new StringWriter(sb);
            Console.SetOut(tw);

            try
            {
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            tw.Close();

            // Restore the default console out.
            Console.SetOut(saved);

            var errs = sb.ToString();

            return errs;
        }
    }
}
