using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileApi.Core.Repository
{
    public interface IObjectRepository
    {
        Task<List<string>> GetObjectsAsync(string name);
        Task<object> CreateGeoObject();
        object GetObjectById(int objectId);
    }
}