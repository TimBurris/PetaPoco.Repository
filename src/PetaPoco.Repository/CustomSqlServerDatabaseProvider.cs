using System.Data;

namespace PetaPoco.Repository
{
    /// <summary>
    /// Derives from <see cref="PetaPoco.Providers.SqlServerDatabaseProvider"/> and customized to support INSERT against tables which might have triggers.  The base PetaPoco implementation fails because of their method of handling OUTPUT.  Our cusomt implementation uses  "output into" and creating a table var to capture the ids
    /// </summary>
    public class CustomSqlServerDatabaseProvider : PetaPoco.Providers.SqlServerDatabaseProvider
    {
        public override string GetInsertOutputClause(string primaryKeyName)
        {
            return base.GetInsertOutputClause(primaryKeyName) + " INTO @ids";
        }

        public override object ExecuteInsert(Database db, IDbCommand cmd, string primaryKeyName)
        {
            //use sql_variant so that it support all types (i hope)
            cmd.CommandText = $@"DECLARE @ids TABLE (colid sql_variant);
{cmd.CommandText}
SELECT colid from @ids";
            return base.ExecuteInsert(db, cmd, primaryKeyName);
        }
    }
}
