using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using MySqlProviderServices = MySql.Data.MySqlClient.MySqlClientFactory;
using SqlProviderServices = System.Data.Entity.SqlServer.SqlProviderServices;


namespace WinstonChurchill.Backend.Repository
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))] // MySQL
    public partial class dbContext : DbContext
    {
        public dbContext(DbConnection dbConnection, DbCompiledModel compileModel) : base(dbConnection, compileModel, true)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = false;

            Database.SetInitializer<dbContext>(null);
        }

    }
}


