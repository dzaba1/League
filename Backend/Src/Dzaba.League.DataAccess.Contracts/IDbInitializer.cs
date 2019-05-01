using System.Threading.Tasks;

namespace Dzaba.League.DataAccess.Contracts
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
    }
}
