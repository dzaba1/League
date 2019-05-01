using Microsoft.AspNetCore.Identity;

namespace Dzaba.League.DataAccess.Contracts.Model
{
    public class Role : IdentityRole<long>, INamedEntity<long>
    {
    }
}
