namespace Dzaba.League.Contracts
{
    public sealed class EloOptions
    {
        public double ExponentialFactor { get; set; }
        public double K { get; set; }
        public double Win { get; set; }
        public double Loss { get; set; }
        public double Draw { get; set; }
        public double Initial { get; set; }
    }
}
