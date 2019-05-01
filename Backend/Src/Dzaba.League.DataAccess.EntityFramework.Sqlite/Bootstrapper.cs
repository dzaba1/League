using Dzaba.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Dzaba.League.DataAccess.EntityFramework.Sqlite
{
    public static class Bootstrapper
    {
        public static void RegisterSqlite(this IServiceCollection container)
        {
            Require.NotNull(container, nameof(container));

            container.AddTransient<IEntityFrameworkProvider, SqliteProvider>();
        }
    }
}
