namespace Dzaba.AspNet.Contracts
{
    public class UserInfo<T>
    {
        public NamedLink<T> User { get; set; }
        public TokenData TokenData { get; set; }
    }
}
