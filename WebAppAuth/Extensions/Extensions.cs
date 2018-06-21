using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace WebAppAuth
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
        /// Получение отображаемого описания enum
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            if (enumValue == null) return string.Empty;

            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .GetDescription();
        }

        /// <summary>
        /// List to DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iList"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this List<T> iList)
        {
            var dataTable = new DataTable();
            var propertyDescriptorCollection =
                TypeDescriptor.GetProperties(typeof (T));
            for (var i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                var propertyDescriptor = propertyDescriptorCollection[i];
                var type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>))
                    type = Nullable.GetUnderlyingType(type);


                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            var values = new object[propertyDescriptorCollection.Count];
            foreach (var iListItem in iList)
            {
                for (var i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        /// <summary>
        /// Получение MD5 хэша строки
        /// </summary>
        /// <returns></returns>
        public static string GetMd5Hash(this string input)
        {
            byte[] hash;
            using (var md5Hash = new System.Security.Cryptography.MD5CryptoServiceProvider())
                hash = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();

            foreach (byte t in hash)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        /// Ограничение с условием
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition,
            Expression<Func<T, bool>> predicate)
        {
            return condition ? query.Where(predicate) : query;
        }


        /// <summary>
        /// Ограничение с условием
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> enumerable, bool condition,
            Func<T, bool> predicate)
        {
            return condition ? enumerable.Where(predicate) : enumerable;
        }
    }
}