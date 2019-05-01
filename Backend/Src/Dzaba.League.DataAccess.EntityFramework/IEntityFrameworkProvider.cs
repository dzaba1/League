using Microsoft.EntityFrameworkCore;

namespace Dzaba.League.DataAccess.EntityFramework
{
    public interface IEntityFrameworkProvider
    {
        void Register(string connectionString, DbContextOptionsBuilder builder);
    }
}