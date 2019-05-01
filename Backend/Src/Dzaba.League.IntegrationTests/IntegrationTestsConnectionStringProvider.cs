using Dzaba.League.DataAccess.Contracts;
using Dzaba.League.DataAccess.EntityFramework.Sqlite;
using System;
using System.IO;

namespace Dzaba.League.IntegrationTests
{
    internal sealed class IntegrationTestsConnectionStringProvider : IConnectionStringProvider
    {
        public static readonly string DbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "leagueTests.db");

        public string GetConnectionString()
        {
            return SqliteUtils.CreateConnectionString(DbPath);
        }
    }
}
