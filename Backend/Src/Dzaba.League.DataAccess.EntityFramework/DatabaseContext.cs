using Dzaba.League.DataAccess.Contracts.Model;
using Dzaba.League.DataAccess.EntityFramework.Configuration;
using Dzaba.Utils;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Dzaba.League.DataAccess.EntityFramework
{
    internal sealed class DatabaseContext : IdentityDbContext<User, Role, long>
    {
        private readonly IModelConfiguration[] modelConfigurations;

        public DatabaseContext(DbContextOptions<DatabaseContext> options,
            IEnumerable<IModelConfiguration> modelConfigurations)
            : base(options)
        {
            Require.NotNull(modelConfigurations, nameof(modelConfigurations));

            this.modelConfigurations = modelConfigurations.ToArray();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var configuration in modelConfigurations)
            {
                configuration.Configure(builder);
            }
        }
    }
}
