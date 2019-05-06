namespace Dzaba.AspNet.Contracts
{
    public class NamedLink<T> : Link<T>
    {
        public string Name { get; set; }
    }
}
