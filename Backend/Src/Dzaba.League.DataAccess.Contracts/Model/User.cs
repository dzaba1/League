using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Dzaba.AspNet.PocoToDto;

namespace Dzaba.League.DataAccess.Contracts.Model
{
    public class User : IdentityUser<long>, INamedEntity<long>
    {
        [NotMapped]
        public string Name
        {
            get
            {
                return UserName;
            }
            set
            {
                UserName = value;
            }
        }
    }
}
