using PetaPoco.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetaPoco.Repository
{
    public class UnitOfWorkProvider : Abstractions.IUnitOfWorkProvider
    {
        private readonly IDatabaseFactory _databaseFactory;

        /// <summary>
        /// Default constructor which will use <see cref="Configuration.DefaultConfiguration"/> for databaseFactory
        /// </summary>
        public UnitOfWorkProvider()
        {

        }

        public UnitOfWorkProvider(Abstractions.IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        public Abstractions.IUnitOfWork GetUnitOfWork()
        {
            return GetUnitOfWork(enlistRepositories: null);
        }

        public IUnitOfWork GetUnitOfWork(params IRepository[] enlistRepositories)
        {
            var uow = new UnitOfWork(_databaseFactory);

            if (enlistRepositories != null)
            {
                foreach (var r in enlistRepositories)
                {
                    r.AssignUnitOfWork(uow);
                }
            }

            return uow;
        }
    }
}
