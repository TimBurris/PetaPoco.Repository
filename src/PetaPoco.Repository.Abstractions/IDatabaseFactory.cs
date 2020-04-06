using System;

namespace PetaPoco.Repository.Abstractions
{
    public interface IDatabaseFactory
    {
        IDatabase Invoke();
    }
}
