using System.Collections.Generic;
using System.Linq;

namespace PetaPoco.Repository
{
    public abstract class ReadRepositoryBase<T, TPrimaryKeyType> : RepositoryBase<T>, Abstractions.IReadRepository<T, TPrimaryKeyType> where T : class
    {
        /// <summary>
        /// Default constructor which will use <see cref="Configuration.DefaultConfiguration"/> for databaseFactory
        /// </summary>
        public ReadRepositoryBase()
        {

        }
        public ReadRepositoryBase(Abstractions.IDatabaseFactory databaseFactory)
                    : base(databaseFactory)
        {

        }

        public T FindById(TPrimaryKeyType entityId)
        {
            using (var db = this.GetDatabase())
            {
                return db.SingleOrDefault<T>(entityId);
            }
        }

        public IEnumerable<T> FindAllByIds(IEnumerable<TPrimaryKeyType> entityIds)
        {
            if (entityIds == null || !entityIds.Any())
                return new List<T>();

            using (var db = this.GetDatabase())
            {
                string primaryKeyColumn = db.Provider.EscapeSqlIdentifier(PetaPoco.Core.PocoData.ForType(typeof(T), db.DefaultMapper).TableInfo.PrimaryKey);
                string sql = $"WHERE [{primaryKeyColumn}] IN(@0)";

                return db
                    .Fetch<T>(sql, entityIds)
                    .ToList();
            }
        }

        public virtual IEnumerable<T> GetAll()
        {
            using (var db = this.GetDatabase())
            {
                return db.Query<T>(sql: string.Empty).ToList();
            }
        }
    }

}
