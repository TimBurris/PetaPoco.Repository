using System.Collections.Generic;
using System.Linq;

namespace PetaPoco.Repository
{
    public abstract class RepositoryBase : Abstractions.IRepository
    {
        private readonly Abstractions.IDatabaseFactory _databaseFactory;
        private Abstractions.IUnitOfWork _unitOfWork;

        /// <summary>
        /// Default constructor which will use <see cref="Configuration.DefaultConfiguration"/> for databaseFactory
        /// </summary>
        public RepositoryBase()
        {

        }

        public RepositoryBase(Abstractions.IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public virtual void AssignUnitOfWork(Abstractions.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// exposes the current assigned unit of work, primarily for information purposes
        /// </summary>
        protected Abstractions.IUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }

        protected virtual PetaPoco.IDatabase GetDatabase()
        {
            //even if there is a unitofwork, db could be null if unitof work has ended)
            var uowDb = _unitOfWork?.Db;
            if (uowDb == null)
            {
                return (_databaseFactory ?? Configuration.DefaultConfiguration.DatabaseFactory).Invoke();
            }
            else
            {
                //open a shared connection so that when this database gets "disposed" it will not dispsose the entire thing, it will just  decrement the connection count, so that it's not ACTUALLY disposed until the final unitofwork close up shop
                uowDb.OpenSharedConnection();

                return uowDb;
            }
        }

        /// <summary>
        /// wraps up the process of starting an IDatabase instace, executing the query by builing a very simple where clause with one filter, and closing out the IDatabase instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName">the name of the column to use in the where clause</param>
        /// <param name="value">the value to compare columnName to
        /// <returns></returns>
        protected virtual IEnumerable<T> QueryWithSingleFilter<T>(string columnName, object value)
        {

            string sql = $"WHERE {columnName} = @0";

            return this.Query<T>(sql, value);
        }

        /// <summary>
        /// wraps up the process of starting an IDatabase instace, executing the query by builing a very simple where IN clause with one filter, and closing out the IDatabase instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName">the name of the column to use in the where clause</param>
        /// <param name="values">the values to compare columnName to
        /// <returns></returns>
        public IEnumerable<T> QueryWithInFilter<T, TKey>(string columnName, IEnumerable<TKey> values)
        {
            if (values?.Any() != true)
            {
                return Enumerable.Empty<T>();
            }

            var sql = $"WHERE {columnName} IN(@0)";

            return this.Query<T>(sql, values);
        }

        /// <summary>
        /// wraps up the process of starting an IDatabase instace, executing the query, and closing out the IDatabase instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected virtual IEnumerable<T> Query<T>(string sql, params object[] args)
        {
            using (var db = this.GetDatabase())
            {
                return db.Query<T>(sql, args)
                    .ToList();
            }
        }

        /// <summary>
        /// wraps up the process of starting an IDatabase instace, executing the query, and closing out the IDatabase instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected virtual IEnumerable<T> Query<T>(PetaPoco.Sql sql)
        {
            using (var db = this.GetDatabase())
            {
                return db.Query<T>(sql)
                    .ToList();
            }
        }
    }

    public abstract class RepositoryBase<T> : RepositoryBase
    {
        /// <summary>
        /// Default constructor which will use <see cref="Configuration.DefaultConfiguration"/> for databaseFactory
        /// </summary>
        public RepositoryBase()
        {

        }

        public RepositoryBase(Abstractions.IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        protected T EntityType { get; }

        /// <summary>
        /// wraps up the process of starting an IDatabase instace, executing the query by builing a very simple where clause with one filter, and closing out the IDatabase instance
        /// </summary>
        /// <param name="columnName">the name of the column to use in the where clause</param>
        /// <param name="value">the value to compare columnName to
        /// <returns></returns>
        protected virtual IEnumerable<T> QueryWithSingleFilter(string columnName, object value)
        {
            return this.QueryWithSingleFilter<T>(columnName, value);
        }

        /// <summary>
        /// wraps up the process of starting an IDatabase instace, executing the query, and closing out the IDatabase instance
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected virtual IEnumerable<T> Query(string sql, params object[] args)
        {
            return this.Query<T>(sql, args);
        }

        /// <summary>
        /// wraps up the process of starting an IDatabase instace, executing the query, and closing out the IDatabase instance
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected virtual IEnumerable<T> Query(PetaPoco.Sql sql)
        {
            return this.Query<T>(sql);
        }
    }
}

