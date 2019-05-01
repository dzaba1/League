using Dzaba.League.DataAccess.Contracts;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Dzaba.League.IntegrationTests
{
    [TestFixture]
    public class DbInitalizerTests : IntegrationTestFixture
    {
        private IDbInitializer CreateSut()
        {
            return Container.GetRequiredService<IDbInitializer>();
        }

        [Test]
        public async Task InitializeAsync_WhenCalled_ThenItCreatesTheDb()
        {
            var sut = CreateSut();
            await sut.InitializeAsync();
        }
    }
}
