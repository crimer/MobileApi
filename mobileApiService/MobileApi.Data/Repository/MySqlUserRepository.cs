using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.Extensions.Logging;
using MobileApi.Core.Repository;
using MobileApi.Core.Services.User.Models;
using MobileApi.Data.Database;

namespace MobileApi.Data.Repository
{
    public class MySqlUserRepository : IUserRepository
    {
        private readonly Db _db;
        private readonly ILogger<MySqlUserRepository> _logger;

        public MySqlUserRepository(Db db, ILogger<MySqlUserRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        
        public async Task<Result<List<UserProfile>>> GetUserProfile()
        {
            try
            {
                string query = $@"SELECT * FROM users";
                var data = await _db.ExecuteReaderListAsync<UserProfile>(query, ReaderToObject);
                return Result.Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError($"При получении информации о пользователе произошла ошибка: {e}");
                return Result.Fail($"При получении информации о пользователе произошла ошибка: {e}");
            }
        }
        
        private UserProfile ReaderToObject(IDataReader reader)
        {
            return new UserProfile()
            {
                Id = reader.Get<int>("id"),
                Name = reader.Get<string>("name"),
                Role = reader.Get<string>("role"),
                Mail = reader.Get<string>("mail"),
                Tel = reader.Get<string>("telephone"),
            };
        }
    }
}