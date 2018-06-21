using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace OhDataManager.Extensions
{
    /// <summary>
    /// Расширения
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Получение отображаемого имени enum
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum enumValue)
        {
            if (enumValue == null) return string.Empty;

            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .GetName();
        }

        /// <summary>
        /// Получение названия таблицы
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetTableName<T>()
        {
            var type = typeof (T);
            var tableAttribute = type.GetCustomAttribute<TableAttribute>();

            return tableAttribute != null ? $"{tableAttribute.Schema}.\"{tableAttribute.Name}\"" : type.Name;
        }
    }
}