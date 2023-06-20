using System;

namespace PetaPoco.Repository.Abstractions
{
    public class DatabaseInstantiatedEventArgs : EventArgs
    {
        public DatabaseInstantiatedEventArgs(IDatabase database)
        {
            this.Database = database;
        }

        public IDatabase Database { get; }
    }
}

