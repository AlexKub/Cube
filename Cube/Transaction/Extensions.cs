using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PJT.Transactions;
using System.Net;
using System.IO;

namespace PJT
{
    public static class Extensions
    {
        /// <summary>
        /// Проверка, что свойство загружено
        /// </summary>
        /// <typeparam name="T">Тип свойства</typeparam>
        /// <param name="clientObject"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool IsPropertyAvailable<T>(this T clientObject, Expression<Func<T, object>> property) where T : ClientObject
        {
            var expression = (MemberExpression)property.Body;
            string propName = expression.Member.Name;
            return clientObject.IsPropertyAvailable(propName);
        }

        /// <summary>
        /// Получение ответа в виде строки
        /// </summary>
        /// <param name="response">Ответ на запрос</param>
        /// <returns>Возвращает тело ответа</returns>
        public static string GetResponseString(this HttpWebResponse response)
        {
            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                return stream.ReadToEnd();
            }
        }

        /// <summary>
        /// Валидация экземпляра транзакции
        /// </summary>
        /// <param name="transaction">Валидируемый экземпляр</param>
        /// <exception cref="ArgumentNullException">При пустой ссылке</exception>
        /// <exception cref="ObjectDisposedException">Передан Disposed-экземпляр</exception>
        public static void Validation<TData>(this TransactionContext<TData> transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException("Передана пустая ссылка на экземпляр транзакции");

            if (transaction.Disposed)
                throw new ObjectDisposedException("Передан Disposed-экземпляр транзакции");
        }

        /// <summary>
        /// Проверка на наличие элементов в коллекции
        /// </summary>
        /// <typeparam name="T">Тип элмента в коллекции</typeparam>
        /// <param name="collection">Проверяемая коллекция</param>
        /// <returns>Возвращает флаг наличия элементов и инициализированной ссылки ндля коллекции</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
                return true;

            foreach (var item in collection)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Получение не пустого значения (пустое значение или ссылка будут заменены соответствующими флагами)
        /// </summary>
        /// <param name="str">Исходная строка</param>
        /// <returns>Возвращает исходную строку или строку-заместитель для пустых ссылок или строк</returns>
        public static string LogValue(this string str)
        {
            if (str == null)
                return "NULL";

            if (string.IsNullOrWhiteSpace(str))
                return "EMPTY";

            return str;
        }

        /// <summary>
        /// Получение GUID из строки
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid Guid(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return System.Guid.Empty;

            var guid = System.Guid.Empty;

            System.Guid.TryParse(str, out guid);

            return guid;
        }

    }
}