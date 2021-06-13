using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MobileApi.Core.Application;
using MobileApi.Extensions;
using MobileApi.Middlewares;

namespace MobileApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private AppConfig _config;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppConfig>(options => _configuration.GetSection("Config").Bind(options));
            _config = _configuration.GetSection("Config").Get<AppConfig>();
            
            SetupJwtServices(services, _config);
            
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);;
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "MobileApi", Version = "v1"}); });

            services.ConfigureDependencies();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            
            app.UseAuthentication();
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MobileApi v1"));

            // global cors policy
            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            
            app.UseRouting();
            app.UseAuthorization();
            
            app.UseMiddleware<ExceptionMiddleware>();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            //app.UseMiddleware<JwtMiddleware>();
        }
        
        private void SetupJwtServices(IServiceCollection services, AppConfig config)
        {
            var key = config.JWTSecretKey;    
            var issuer = "http://mysite.com";  
  
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  
                .AddJwtBearer(options =>  
                {
                    options.TokenValidationParameters = new TokenValidationParameters  
                    {  
                        ValidateIssuer = true,  
                        ValidateAudience = true,  
                        ValidateIssuerSigningKey = true,  
                        ValidIssuer = issuer,  
                        ValidAudience = issuer,  
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))  
                    };  
  
                    options.Events = new JwtBearerEvents  
                    {  
                        OnAuthenticationFailed = context =>  
                        {  
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))  
                            {  
                                context.Response.Headers.Add("Token-Expired", "true");  
                            }  
                            return Task.CompletedTask;  
                        }  
                    };  
                });  
        }
    }
}