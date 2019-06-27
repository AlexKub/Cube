using Merlion.ECR.Update.Core.Manifest;
using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Merlion.ECR.Update.Core.Win32_API;
using Merlion.ECR.Update.Core.Environment;
using System.Collections.Generic;

namespace Merlion.ECR.Update.Core
{
    /// <summary>
    /// Общие расширения
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Удаление символов \r и \n из строки
        /// </summary>
        /// <param name="str">Входная строка</param>
        /// <returns>Возвращает входную строку с удалёнными \r и \n</returns>
        public static string Clean_CRLF(this string str)
        {
            return str.Replace("\r", "").Replace("\n", "");
        }

        /// <summary>
        /// Ожидание указанного времени
        /// </summary>
        /// <param name="time">Время, которое стоит подождать</param>
        /// <returns>Возвращает задачу, которую можно подождать</returns>
        public static Task Wait(this TimeSpan time)
        {
            Console.WriteLine();
            return Task.Run(() => { System.Threading.Thread.Sleep((int)time.TotalMilliseconds); });
        }

        #region System.Diagnostics.Process

        /// <summary>
        /// Получение полного имени исполняемого файла Процесса
        /// </summary>
        /// <param name="p">Интересующий Процесс</param>
        /// <returns>Возвращает полное имя (с путём) исполняемого файла, которым был запущен текущий Процесс</returns>
        public static string ExePathName(this Process p)
        {
            return p.MainModule.FileName;
        }

        #endregion



        #region SYstem.Reflection.Assembly

        /// <summary>
        /// Получение GUID'а сборки (если задан)
        /// </summary>
        /// <param name="asm">Сборка, для которой необходимо получить GUID</param>
        /// <returns>Возвращает GUID сборки или путую строку</returns>
        public static string GetGUID(this Assembly asm)
        {
            string id = "";
            foreach (object attr in asm.GetCustomAttributes(true))
            {
                if (attr is GuidAttribute)
                    id = ((GuidAttribute)attr).Value;
            }
            return id;
        }

        /// <summary>
        /// Получение токена публичного ключа сборки
        /// </summary>
        /// <param name="assembly">Сборка</param>
        /// <returns>Возвращает токен публичного ключа или пустую строку</returns>
        public static string GetPublicKeyToken(this Assembly assembly)
        {
            var bytes = assembly.GetName().GetPublicKeyToken();
            if (bytes == null || bytes.Length == 0)
                return string.Empty;

            var publicKeyToken = string.Empty;
            for (int i = 0; i < bytes.GetLength(0); i++)
                publicKeyToken += string.Format("{0:x2}", bytes[i]);

            return publicKeyToken;
        }

        /// <summary>
        /// Получение основной версии .NET, под которую была собрана библиотека
        /// </summary>
        /// <param name="asm"></param>
        /// <returns></returns>
        public static RuntimeVersions GetDotNetVersion(this Assembly asm)
        {
            var strVersion = asm.ImageRuntimeVersion;

            var parts = strVersion.Split('.');

            var mainVersion = parts[0];

            int version = 0;
            int.TryParse(mainVersion[1].ToString(), out version);

            switch (version)
            {
                case 1:
                    return RuntimeVersions.v1;
                case 2:
                    return RuntimeVersions.v2;
                case 3:
                    return RuntimeVersions.v3;
                case 4:
                    return RuntimeVersions.v4;
                default:
                    return RuntimeVersions.UnKnown;
            }
        }

        #endregion

        public static void OpenExplorer(this DirectoryInfo dir)
        {
            if (!dir.Exists)
                MessageBox.Show("Каталог " + dir.FullName + " не найден.");

            using (Process p = new Process())
            {
                p.StartInfo.FileName = dir.FullName;
                p.Start();
            }

        }

        #region System.IO.FileInfo

        public static void OpenExplorer(this FileInfo fInfo)
        {
            if (!fInfo.Exists)
                MessageBox.Show("Файл " + fInfo.FullName + " не найден.");

            using (Process p = new Process())
            {
                p.StartInfo.FileName = fInfo.FullName;
                p.Start();
            }

        }

        /// <summary>
        /// Проверка управляемых сборки
        /// </summary>
        /// <param name="fInfo">Ссылка на файл</param>
        /// <returns>Возвращает флаг соответствия загаловка файла управляемым сборкам CLR</returns>
        public static bool Is_CLR_Assembly(this FileInfo fInfo)
        {
            return Core_2_0.Utilites.Is_CLR_Assembly(fInfo.FullName);
        }

        public static bool Is_DLL(this FileInfo fInfo)
        {
            return fInfo.Extension.Equals(Constants.FileExtensions.DLL, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion

        /// <summary>
        /// Исключение в красивой строке для записи в лог
        /// </summary>
        /// <param name="ex">Экземпляр исключения</param>
        /// <returns>Возвращает красивую строку с данными исключения (включая вложенные) или пустую строку, если экземпляр не задан</returns>
        public static string ToLogString(this Exception ex)
        {
            return new StringBuilder().AppendException(ex).ToString();
        }

        #region System.Text.StringBuilder

        /// <summary>
        /// рекурсивное добавление описания и стека исключения (включая вложенные) к текущей строке
        /// </summary>
        /// <param name="sb">Текущая строка</param>
        /// <param name="ex">Экземпляр исключения</param>
        /// <param name="prevIndent">Текущий отступ (для построение итогового сообщения "лесенкой")</param>
        /// <remarks>Для добавления отступа, передать пустую строку в prevIndent</remarks>
        /// <returns>Возвращает текущий экземпляр StringBuilder с добавленным описанием исключения</returns>
        public static StringBuilder AppendException(this StringBuilder sb, Exception ex, string prevIndent = null)
        {
            if (ex == null)
                return sb;

            const string indent = "    ";
            string curIndent = prevIndent == null ? string.Empty : prevIndent + indent;

            sb.Append(curIndent).Append("Сообщение исключения: ").AppendLine(ex.Message);

            sb.Append(curIndent).AppendLine("Стек вызова:");

            using (TextReader stringReader = new StringReader(ex.StackTrace))
            {
                sb.Append(curIndent).AppendLine(stringReader.ReadLine().Trim());
            }

            if (ex.InnerException != null)
            {
                sb.Append(curIndent).AppendLine("Вложенное исключение: ---------------------");
                sb.AppendException(ex.InnerException, curIndent);
            }

            return sb;
        }

        #endregion

        /// <summary>
        /// Удаление указанного количества символов с конца строки
        /// </summary>
        /// <param name="sb">Экземпляр StringBuilder для работы</param>
        /// <param name="charCount">Количество символов для обрезания с конца</param>
        /// <returns>Возвращает ткеузий экземпляр с обрезанным количсетвом символов</returns>
        public static StringBuilder RemoveLast(this StringBuilder sb, int charCount)
        {
            if (charCount > 0 && sb.Length > charCount)
                sb.Remove(sb.Length - charCount, charCount);

            return sb;
        }

        /// <summary>
        /// Объединение коллекции с помощью StringBuilder
        /// </summary>
        /// <typeparam name="T">Тип элемента коллекции</typeparam>
        /// <param name="sb">Текущий экземпляр StringBuiler</param>
        /// <param name="collection">Коллекция элементов</param>
        /// <param name="joinFunc">Функция объединения</param>
        /// <param name="separator">Разделитель (если необходим)</param>
        /// <returns>Возваращает текущий экземпляр с добавленными элементами коллекции</returns>
        public static StringBuilder Join<T>(this StringBuilder sb, IEnumerable<T> collection, Func<StringBuilder, T, StringBuilder> joinFunc, string separator = null)
        {
            if (collection.Count() > 0)
            {
                if (string.IsNullOrEmpty(separator))
                    foreach (var item in collection)
                        joinFunc(sb, item);
                else
                {
                    foreach (var item in collection)
                        joinFunc(sb, item).Append(separator);

                    sb.RemoveLast(separator.Length);
                }
            }

            return sb;
        }

        #region CustomAttributes

        /// <summary>
        /// Проверка наличия Атрибута на Свойстве
        /// </summary>
        /// <typeparam name="TAttribute">Тип атрибута</typeparam>
        /// <param name="info">Свойство</param>
        /// <param name="inherite">Флаг проверки родителей</param>
        /// <returns>Возвращает флаг наличия атрибута указанного типа у переданного свойства</returns>
        public static bool HasAttribute<TAttribute>(this PropertyInfo info, bool inherite = false) where TAttribute : Attribute
        {
            var attr = info.GetCustomAttribute<TAttribute>(inherite);

            return attr != null;
        }

        #endregion

        /// <summary>
        /// Получение известных WMI-значений типов авторизации для сервиса
        /// </summary>
        /// <param name="type">Тип авторизации</param>
        /// <returns>Возвращает коллекцию Тип/Значение WMI</returns>
        public static IDictionary<AccountType, string> GetCommonValues(this AccountType type)
        {
            var eType = type.GetType();

            var commonTypes = new Dictionary<AccountType, string>();
            foreach (var val in eType.GetMembers())
            {
                var attr = val.GetCustomAttribute<AccountTypeValueAttribute>();

                if (attr != null)
                    commonTypes.Add((AccountType)Enum.Parse(eType, val.Name, false), attr.WMI_Value);
            }

            return commonTypes;
        }

        /// <summary>
        /// Получение WMI-значения для текущего значения перечисления
        /// </summary>
        /// <param name="type">Значение перечисления</param>
        /// <returns>Вовращает значение аттрибута WMI для жанного поля или string.Empty</returns>
        public static string GetCommonValue(this AccountType type)
        {
            var eType = type.GetType();

            var commonTypes = new Dictionary<AccountType, string>();

            var stringVal = type.ToString();
            foreach (var val in eType.GetMembers())
            {
                if (val.Name.Equals(stringVal))
                {
                    var attr = val.GetCustomAttribute<AccountTypeValueAttribute>();

                    if (attr != null)
                        return attr.WMI_Value;
                }
            }

            return "";
        }
    }
}
