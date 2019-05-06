using System;

namespace Dzaba.AspNet.Contracts
{
    public class TokenData
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
