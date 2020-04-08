using System;
using System.Collections.Generic;
using System.Text;

namespace PetaPoco.Repository.Abstractions
{
    public interface IRepositoryConfiguration
    {
        Abstractions.IDatabaseFactory DatabaseFactory { get; set; }
        Abstractions.ICrudRepositoryServiceCollection CrudServiceCollection { get; set; }
    }
}
