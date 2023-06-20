using System;

namespace PetaPoco.Repository.Abstractions
{
    public interface IDatabaseFactory
    {
        /// <summary>
        /// generates a new instance of <see cref="IDatabase"/>
        /// </summary>
        /// <returns></returns>
        IDatabase Invoke();

        /// <summary>
        /// Event raised anytime a new Database is instantiated
        /// </summary>

        event EventHandler<DatabaseInstantiatedEventArgs> DatabaseInstantiated;
    }
}
