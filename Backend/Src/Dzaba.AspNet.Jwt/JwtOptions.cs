using System;

namespace Dzaba.AspNet.Jwt
{
    public class JwtOptions
    {
        public bool RequireHttpsMetadata { get; set; }
        public bool SaveToken { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string Key { get; set; }
        public TimeSpan Expires { get; set; }
    }
}
