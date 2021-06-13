using System;
using System.Collections;
using System.Data;
using MySqlConnector;

namespace MobileApi.Data.Database
{
    /// <summary>
    /// Класс расширение для работы с БД
    /// </summary>
    public static class DataBaseExtensions
    {
        /// <summary>
        /// Считывание поля из ридера по имени и автоматическое приведение к указанному типу
        /// </summary>
        /// <param name="reader">Данные</param>
        /// <param name="propertyName">Название поля ридера</param>
        /// <typeparam name="T">Тип, к которому необходимо выполнить преобразования</typeparam>
        public static T Get<T>(this IDataReader reader, string propertyName)
        {
            return (T) Convert.ChangeType(reader[propertyName], typeof(T));
        }
        
        /// <summary>
        /// Получение типа объекта в PostgreSQL на базе типа переданного объекта
        /// </summary>
        /// <param name="obj">Объект для которого нужно подобрать тип</param>
        /// <exception cref="InvalidCastException">Не удалось сопоставить тип в PostgreSQL и переданным объектом</exception>
        public static MySqlDbType GetMySqlTypeByObject(this object obj)
        {
            switch (obj)
            {
                case Enum _:
                    return MySqlDbType.Int32;
                case Guid _:
                    return MySqlDbType.Guid;
                case byte _:
                    return MySqlDbType.Byte;
                case short _:
                    return MySqlDbType.Byte;
                case int _:
                    return MySqlDbType.Int32;
                case long _:
                    return MySqlDbType.Int64;
                case double _:
                    return MySqlDbType.Double;
                case decimal _:
                    return MySqlDbType.Decimal;
                case bool _:
                    return MySqlDbType.Bool;
                case DateTime _:
                    return MySqlDbType.DateTime;
                case TimeSpan _:
                    return MySqlDbType.Timestamp;
                case string _:
                    return MySqlDbType.String;
                // case IEnumerable enumerable:
                //     var enumerator = enumerable.GetEnumerator();
                //     enumerator.MoveNext();
                //     var itemType = enumerator.Current.GetPostgreSqlTypeByObject();
                //     var type = MySqlDbType. | itemType;
                //     return type; 
                default:
                    throw new InvalidCastException($"Не удалось сопоставить тип {obj.GetType()} с типом в PostgreSQL");
            }
        }
    }
}