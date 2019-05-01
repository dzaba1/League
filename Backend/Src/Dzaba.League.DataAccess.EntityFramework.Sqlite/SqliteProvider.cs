using Dzaba.Utils;
using Microsoft.EntityFrameworkCore;

namespace Dzaba.League.DataAccess.EntityFramework.Sqlite
{
    internal sealed class SqliteProvider : IEntityFrameworkProvider
    {
        public void Register(string connectionString, DbContextOptionsBuilder optionsBuilder)
        {
            Require.NotWhiteSpace(connectionString, nameof(connectionString));
            Require.NotNull(optionsBuilder, nameof(optionsBuilder));

            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
