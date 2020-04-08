using PetaPoco.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetaPoco.Repository
{
    public class Configuration : Abstractions.IRepositoryConfiguration
    {
        public IDatabaseFactory DatabaseFactory { get; set; }
        public ICrudRepositoryServiceCollection CrudServiceCollection { get; set; }

        public static Abstractions.IRepositoryConfiguration DefaultConfiguration { get; set; }

        public static void InitDefaultWithSqlServer(string connectionString)
        {
            InitDefault(new DefaultSqlDatabaseFactory(connectionString), new CrudRepositoryServiceCollection());
        }
        public static void InitDefault(Abstractions.IDatabaseFactory databaseFactory)
        {
            InitDefault(databaseFactory, new CrudRepositoryServiceCollection());
        }

        public static void InitDefault(Abstractions.IDatabaseFactory databaseFactory, Abstractions.ICrudRepositoryServiceCollection crudServiceCollection)
        {
            DefaultConfiguration = new Configuration()
            {
                DatabaseFactory = databaseFactory,
                CrudServiceCollection = crudServiceCollection,
            };
        }

    }
}
