using PetaPoco.Repository.Abstractions;

namespace PetaPoco.Repository
{
    public class UnitOfWork : Abstractions.IUnitOfWork
    {
        private readonly Transaction _petaTransaction;
        private readonly IDatabase _db;
        private readonly IDatabaseFactory _databaseFactory;

        /// <summary>
        /// Default constructor which will use <see cref="Configuration.DefaultConfiguration"/> for databaseFactory
        /// </summary>
        public UnitOfWork()
            : this(databaseFactory: null)
        {

        }

        public UnitOfWork(Abstractions.IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;

            var db = this.GetDatabase() as Database;
            _petaTransaction = new Transaction(db);
            _db = db;
        }

        protected virtual PetaPoco.IDatabase GetDatabase()
        {
            return (_databaseFactory ?? Configuration.DefaultConfiguration.DatabaseFactory).Invoke();
        }

        public void Dispose()
        {
            _petaTransaction.Dispose();
        }

        public IDatabase Db
        {
            get { return _db; }
        }

        public void Commit()
        {
            _petaTransaction.Complete();
        }
    }
}
