using NUnit.Framework;
using System;
using System.IO;

namespace Dzaba.League.IntegrationTests
{
    public class IntegrationTestFixture
    {
        protected IServiceProvider Container { get; private set; }

        [TearDown]
        public void CleanUp()
        {
            if (File.Exists(IntegrationTestsConnectionStringProvider.DbPath))
            {
                File.Delete(IntegrationTestsConnectionStringProvider.DbPath);
            }
        }

        [SetUp]
        public void SetupBase()
        {
            CleanUp();
            Container = Bootstrapper.GetTestContainer();
        }
    }
}
