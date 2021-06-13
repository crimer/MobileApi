using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MobileApi.Core.Repository;
using MobileApi.Data.Database;
using MySqlConnector;

namespace MobileApi.Data.Repository
{
    public class MySqlObjectRepository : IObjectRepository
    {
        private readonly Db _db;

        public MySqlObjectRepository(Db db)
        {
            _db = db;
        }
        
        public Task<List<string>> GetObjectsAsync(string w)
        {
            string query = $@"SELECT * FROM objects WHERE Id = {w}";
            return _db.ExecuteReaderListAsync<string>(query, ReaderToObject,
                new MySqlParameter("Count", w));
        }

        public async Task<object> CreateGeoObject()
        {
            string query = $@"SELECT * FROM objects WHERE Id = ";
            return _db.ExecuteReaderListAsync<string>(query, ReaderToObject);
        }

        public object GetObjectById(int objectId)
        {
            string query = $@"SELECT id AS Id, FROM objects WHERE Id = ";
            //return _db.ExecuteReaderListAsync<string>(query, ReaderToObject);
            return new {data = "data"};
        }

        private string ReaderToObject(IDataReader reader)
        {
            return "t1";
        }
    }
}