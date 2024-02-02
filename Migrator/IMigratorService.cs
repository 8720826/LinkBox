using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkBox.Migrator
{
    public interface IMigratorService
    {
        string MigrateUp();



        string MigrateDown(long version);

        string GetVersion();

    }
}
