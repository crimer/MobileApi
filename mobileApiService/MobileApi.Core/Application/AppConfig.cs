namespace MobileApi.Core.Application
{
    public class AppConfig
    {
        public string MySqlConnection { get; set; }
        
        /// <summary>
        /// Секретный ключ для валидации JWT 
        /// </summary>
        public string JWTSecretKey { get; set; }
    }
}