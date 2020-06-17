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
            return new UnitOfWork(_databaseFactory);
        }
    }
}
