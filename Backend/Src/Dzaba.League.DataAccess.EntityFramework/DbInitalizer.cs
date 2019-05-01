using Dzaba.League.DataAccess.Contracts;
using Dzaba.Utils;
using System;
using System.Threading.Tasks;

namespace Dzaba.League.DataAccess.EntityFramework
{
    internal sealed class DbInitalizer : IDbInitializer
    {
        private readonly Func<DatabaseContext> dbContextFactory;

        public DbInitalizer(Func<DatabaseContext> dbContextFactory)
        {
            Require.NotNull(dbContextFactory, nameof(dbContextFactory));

            this.dbContextFactory = dbContextFactory;
        }

        public async Task InitializeAsync()
        {
            using (var dbContext = dbContextFactory())
            {
                await dbContext.Database.EnsureCreatedAsync();
            }
        }
    }
}
