using Microsoft.Extensions.DependencyInjection;
using MobileApi.Core.Repository;
using MobileApi.Core.Services.Auth;
using MobileApi.Core.Services.Object;
using MobileApi.Core.Services.User;
using MobileApi.Data.Database;
using MobileApi.Data.Repository;

namespace MobileApi.Extensions
{
    public static class DependencyExtension
    {
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            // Репозитории
            services.AddSingleton<IObjectRepository, MySqlObjectRepository>();
            services.AddSingleton<IUserRepository, MySqlUserRepository>();

            //Сервисы
            services.AddSingleton<AuthService>();
            services.AddSingleton<UserService>();
            services.AddSingleton<ObjectService>();

            // Инфраструктура и работа с БД
            services.AddSingleton<Db>();
            services.AddSingleton<MySqlConnectionProvider>();
        }
    }
}