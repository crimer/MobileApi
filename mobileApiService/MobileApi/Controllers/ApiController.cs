using System;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace MobileApi.Controllers
{
    public class ApiController : ControllerBase
    {
        /// <summary>
        /// Преобразование необобщённого Result в ответ API
        /// </summary>
        /// <param name="result">Результат работы метода для пребразования</param>
        protected IActionResult ApiResponse(Result result)
        {
            if (result.IsSuccess)
                return Ok();
            
            return BadRequest(string.Join(Environment.NewLine, result.Errors.ConvertAll(error => error.Message)));
        }
        
        /// <summary>
        /// Преобразование обощённого Result в ответ API
        /// </summary>
        /// <param name="result">Результат работы метода для пребразования</param>
        protected IActionResult ApiResponse<T>(Result<T> result)
        {
            if (result.IsSuccess)
                return Ok();
            
            return BadRequest(string.Join(Environment.NewLine, result.Errors.ConvertAll(error => error.Message)));
        }
        
        /// <summary>
        /// Преобразование обощённого Result в ответ API
        /// </summary>
        /// <param name="result">Результат работы метода для пребразования</param>
        protected IActionResult ApiResponseResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
                return Ok(result.Value);
            
            return BadRequest(string.Join(Environment.NewLine, result.Errors.ConvertAll(error => error.Message)));
        }
    }
}