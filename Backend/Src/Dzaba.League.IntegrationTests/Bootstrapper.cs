using Microsoft.Extensions.DependencyInjection;
using System;

namespace Dzaba.League.IntegrationTests
{
    internal static class Bootstrapper
    {
        public static IServiceProvider GetRealContainer()
        {
            var collection = new ServiceCollection();
            return new Startup(null).ConfigureServices(collection);
        }
    }
}
