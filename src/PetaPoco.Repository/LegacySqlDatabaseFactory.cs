using System;

namespace PetaPoco.Repository
{
    /// <summary>
    /// this factory uses the base <see cref="PetaPoco.Providers.SqlServerDatabaseProvider"/> which does not support INSERT on tables that have triggers
    /// </summary>
    public class LegacySqlDatabaseFactory : Abstractions.IDatabaseFactory
    {
        private readonly string _connectionString;

        public LegacySqlDatabaseFactory(string connectionString)
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
