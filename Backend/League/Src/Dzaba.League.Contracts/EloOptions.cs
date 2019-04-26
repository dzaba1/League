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

        public static readonly EloOptions Default = new EloOptions
        {
            Draw = 0.5,
            ExponentialFactor = 400,
            Initial = 1000,
            K = 32,
            Loss = 0,
            Win = 1
        };
    }
}
