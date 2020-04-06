using PetaPoco.Repository.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PetaPoco.Repository
{
    public class CrudRepositoryServiceCollection : List<Abstractions.ICrudRepositoryService>, Abstractions.ICrudRepositoryServiceCollection
    {

    }
}
