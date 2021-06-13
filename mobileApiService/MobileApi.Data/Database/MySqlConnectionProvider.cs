using System;
using Microsoft.Extensions.Options;
using MobileApi.Core.Application;
using MySqlConnector;

namespace MobileApi.Data.Database
{
    public class MySqlConnectionProvider : IDisposable
    {
        private readonly string _connectionString;
        private MySqlConnection _mySqlConnection;

        public MySqlConnectionProvider(IOptions<AppConfig> config)
        {
            _connectionString = config.Value.MySqlConnection;
        }

        public MySqlConnection GetConnection()
        {
            _mySqlConnection = new MySqlConnection(_connectionString);
            return _mySqlConnection;
        }

        public void Dispose()
        {
            _mySqlConnection.Dispose();
        }
    }
}