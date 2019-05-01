using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dzaba.League.DataAccess.EntityFramework.Configuration
{
    public abstract class EntityConfiguration<T> : IModelConfiguration
        where T : class
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<T>();
            Configure(builder);
        }

        protected abstract void Configure(EntityTypeBuilder<T> builder);
    }
}
