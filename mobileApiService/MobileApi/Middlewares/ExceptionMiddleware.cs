using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.AspNetCore.Http;

namespace MobileApi.Middlewares
{
    public class ExceptionMiddleware    
    {    
        private readonly RequestDelegate _next;    
    
        public ExceptionMiddleware(RequestDelegate next)    
        {    
            _next = next;
        }
    
        public async Task Invoke(HttpContext context)    
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex);
            }
            
        }
        private Task HandleExceptionMessageAsync(HttpContext context, Exception exception)  
        {  
            var statusCode = (int)HttpStatusCode.InternalServerError;
            
            var result = JsonSerializer.Serialize(new  
            {  
                statusCode = statusCode,
                errorMessage = exception.Message
            });
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        } 
    }    
}