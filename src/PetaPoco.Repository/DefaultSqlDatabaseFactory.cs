using System;

namespace PetaPoco.Repository
{

    /// <summary>
    /// The default factory which utilizes <see cref="CustomSqlServerDatabaseProvider"/>
    /// </summary>
    [Obsolete("This class is deprecated, please use DefaultMsSqlDatabaseFactory instead; but it uses Microsoft.Data.Sql so you'll need to change your nuget and you'll need to trust server certificates")]
    public class DefaultSqlDatabaseFactory : Abstractions.IDatabaseFactory
    {
        private readonly string _connectionString;

        /// <summary>
        /// Event raised anytime a new Database is instantiated
        /// </summary>
        public event EventHandler<Abstractions.DatabaseInstantiatedEventArgs> DatabaseInstantiated;

        public DefaultSqlDatabaseFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDatabase Invoke()
        {
            try
            {
                var db = new PetaPoco.Database(_connectionString, new CustomSqlServerDatabaseProvider()); //I have switched from the base  provider to our custom provider because supporting triggers should be supported by default.  i'm providing a LegacySqlDatabaseFactory for anyone who does not want that functionality
                this.DatabaseInstantiated?.Invoke(this, new Abstractions.DatabaseInstantiatedEventArgs(db));
                return db;
            }
            catch (ArgumentException ex)
            {
                if (ex.Message != null && ex.Message.Contains("Could not load the SqlServerDatabaseProvider DbProviderFactory"))
                {
                    throw new ApplicationException("Error initializing the SqlServer provider, be sure that you have included System.Data.SqlClient in your project", ex);
                }
                throw;
            }
        }
    }
}
