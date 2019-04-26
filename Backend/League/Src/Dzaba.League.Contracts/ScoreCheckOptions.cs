using System;

namespace Dzaba.League.Contracts
{
    [Flags]
    public enum ScoreCheckOptions
    {
        OneWinnerRestAreLoosers = 1,
        OneLooserRestAreWinners = 2,
        OnlyWinners = 4,
        DrawOnExAequoWin = 8
    }
}
