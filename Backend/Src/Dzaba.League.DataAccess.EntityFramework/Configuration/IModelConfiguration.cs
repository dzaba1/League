using Microsoft.EntityFrameworkCore;

namespace Dzaba.League.DataAccess.EntityFramework.Configuration
{
    public interface IModelConfiguration
    {
        void Configure(ModelBuilder modelBuilder);
    }
}
