using System;

namespace Dzaba.AspNet.Jwt
{
    public class TokenData
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
