using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MobileApi.Controllers.Object.Dto;
using MobileApi.Core.Repository;

namespace MobileApi.Controllers.Object
{
    [ApiController]
    [Route("api/object")]
    public class ObjectApiController : ApiController
    {
        private readonly IObjectRepository _repository;

        public ObjectApiController(IObjectRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet("getAllObjects")]
        public async Task<IActionResult> GetAllObjets([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Переданны некорректные данные");
            
            var t = await _repository.GetObjectsAsync(name);
            return Ok(t);
        }
        
        /// <summary>
        /// Метод создания объекта теплоснабжения (дома)
        /// </summary>
        /// <param name="name">Название объеката</param>
        [HttpPost("createObject")]
        public async Task<IActionResult> CreateObject([FromBody] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Переданны некорректные данные");
            
            var t = await _repository.GetObjectsAsync(name);
            return Ok(t);
        }
        
        /// <summary>
        /// Метод создания геообъекта
        /// </summary>
        /// <param name="geoObjectDto">Модель геообъекта</param>
        [HttpPost("createGeoObject")]
        public async Task<IActionResult> CreateGeoObject([FromBody] CreateGeoObjectDto geoObjectDto)
        {
            if (geoObjectDto == null)
                return BadRequest("Переданны некорректные данные");
            
            var t = await _repository.CreateGeoObject();
            return Ok(t);
        }

        /// <summary>
        /// Получение одного объекта
        /// </summary>
        /// <param name="objectId">Идентификатор объекта</param>
        /// <returns>Объект</returns>
        [HttpGet("getObject")]
        public async Task<IActionResult> GetObject([FromQuery] int objectId)
        {
            if (objectId <= 0)
                return BadRequest("Некорректный идентификатор объекта");

            var objectData = _repository.GetObjectById(objectId);
            return Ok(objectData);
        }
    }
}