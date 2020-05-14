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
            try
            {
                return new PetaPoco.Database(_connectionString, new Providers.SqlServerDatabaseProvider());
            }
            catch (ArgumentException ex)
            {
                if (ex.Message != null && ex.Message.Contains("Could not load the SqlServerDatabaseProvider DbProviderFactory"))
                {
                    throw new ApplicationException("Error initialiting the SqlServer provider, be sure that you have included System.Data.SqlClient in your project", ex);
                }
                throw;
            }
        }
    }
}
