using System;
using System.Collections.Generic;
using System.Text;

namespace PetaPoco.Repository
{
    public class DefaultSqlDatabaseFactory : Abstractions.IDatabaseFactory
    {
        private readonly string _connectionString;

        public DefaultSqlDatabaseFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDatabase Invoke()
        {
            return new PetaPoco.Database(_connectionString, new Providers.SqlServerDatabaseProvider());
        }
    }
}
