using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace MobileApi.Data.Database
{
    /// <summary>
    /// Класс для работы с БД
    /// </summary>
    public class Db
    {
        private readonly MySqlConnectionProvider _sqlConnectionProvider;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="sqlConnectionProvider">Провайдер соединения до MySql</param>
        public Db(MySqlConnectionProvider sqlConnectionProvider)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
        }

        /// <summary>
        /// Выполнение запроса без получения результата
        /// </summary>
        /// <param name="sqlText">Текст запроса</param>
        /// <param name="parameters">Параметры запроса</param>
        public async Task ExecuteNonQueryAsync(string sqlText, params MySqlParameter[] parameters)
        {
            await using var connection = await CreateConnectionAsync();
            await using var command = CreateCommand(connection, sqlText, parameters);
            await command.ExecuteNonQueryAsync();
        }
        
        /// <summary>
        /// Выполнение запроса с получением одного результата
        /// </summary>
        /// <param name="sqlText">Текст запроса</param>
        /// <param name="binding">Функция для преобразования результата выполнения запроса в объект</param>
        /// <param name="parameters">Параметры запроса</param>
        /// <typeparam name="T">Тип возвращаемого объекта</typeparam>
        public async Task<T> ExecuteReaderAsync<T>(string sqlText, Func<IDataReader, T> binding, params MySqlParameter[] parameters)
        {
            await using var connection = await CreateConnectionAsync();
            await using var command = CreateCommand(connection, sqlText, parameters);
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return binding(reader);
            
            return default;
        }
        
        /// <summary>
        /// Выполнение запроса с получением результата в виде списка указанного типа
        /// </summary>
        /// <param name="sqlText">Текст запроса</param>
        /// <param name="binding">Функция для преобразования результата выполнения запроса в объект</param>
        /// <param name="parameters">Параметры запроса</param>
        /// <typeparam name="T">Тип возвращаемых объектов</typeparam>
        public async Task<List<T>> ExecuteReaderListAsync<T>(string sqlText, Func<IDataReader, T> binding, params MySqlParameter[] parameters)
        {
            await using var connection = await CreateConnectionAsync();
            await using var command = CreateCommand(connection, sqlText, parameters);
            await using var reader = await command.ExecuteReaderAsync();

            var result = new List<T>();
            while (await reader.ReadAsync())
            {
                result.Add(binding(reader));
            }

            return result;
        }

        private static DbCommand CreateCommand(MySqlConnection connection, string sqlText, params MySqlParameter[] @params)
        {
            var command = connection.CreateCommand();
            command.CommandText = sqlText;
            command.Parameters.AddRange(@params);
            return command;
        }

        private async Task<MySqlConnection> CreateConnectionAsync()
        {
            var connection = _sqlConnectionProvider.GetConnection();
            await connection.OpenAsync();

            return connection;
        }
    }
}