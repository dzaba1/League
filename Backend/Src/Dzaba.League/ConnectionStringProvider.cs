using Dzaba.League.DataAccess.Contracts;
using Dzaba.Utils;
using Microsoft.Extensions.Configuration;

namespace Dzaba.League
{
    internal sealed class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly IConfiguration configuration;

        public ConnectionStringProvider(IConfiguration configuration)
        {
            Require.NotNull(configuration, nameof(configuration));

            this.configuration = configuration;
        }

        public string GetConnectionString()
        {
            return configuration.GetConnectionString("Default");
        }
    }
}
