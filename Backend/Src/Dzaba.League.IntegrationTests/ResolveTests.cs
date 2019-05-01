using Dzaba.League.DataAccess.Contracts;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Dzaba.League.IntegrationTests
{
    [TestFixture]
    public class ResolveTests
    {
        private IServiceProvider container;

        [OneTimeSetUp]
        public void Setup()
        {
            container = Bootstrapper.GetRealContainer();
        }

        public static IEnumerable<Type> GetTransientServices()
        {
            yield return typeof(IDbInitializer);
        }

        [TestCaseSource(nameof(GetTransientServices))]
        public void GetRequiredService_WhenCalled_ThenItReturnsDifferentObjects(Type type)
        {
            var s1 = container.GetRequiredService(type);
            var s2 = container.GetRequiredService(type);
            s1.Should().NotBeSameAs(s2);
        }
    }
}
