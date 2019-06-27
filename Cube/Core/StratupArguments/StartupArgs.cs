using Merlion.ECR.Update.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Merlion.ECR.Update.Core.StartupArguments
{
    /// <summary>
    /// Аргументы запуска приложения
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public abstract class StartupArgs
    {
        /// <summary>
        /// Разделитель аргументов при построении строки (пробел)
        /// </summary>
        protected const string ArgSplitter = " ";

        protected static readonly LogSet m_loger = LogManager.GetDefaultLogSet<StartupArgs>();

        internal static LogSet Loger { get { return m_loger; } }

        /// <summary>
        /// Флаг наличия аргументов
        /// </summary>
        public bool IsEmpty { get { return ParsedCount == 0; } }

        /// <summary>
        /// Количество проставленных аргументов
        /// </summary>
        public int ParsedCount { get; protected set; }

        /// <summary>
        /// Флаг поиска Атрибутов в наследниках
        /// </summary>
        protected bool Inherit { get; set; }

        string DebugDisplay()
        {
            return "ParsedCount: " + ParsedCount.ToString();
        }
    }

    /// <summary>
    /// Аргументы запуска приложения
    /// </summary>

    public abstract class StartupArgs<T> : StartupArgs
        where T : StartupArgs<T>, new()
    {
        /// <summary>
        /// Коллекция свойств текущего типа
        /// </summary>
        readonly IEnumerable<PropertyInfo> m_properties;

        /// <summary>
        /// Наружу не показывать
        /// </summary>
        public StartupArgs()
        {
            m_properties = GetProperties();
        }

        /// <summary>
        /// Парсинг входных аргументов приложения
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T Parse(string[] args)
        {
            try
            {
                if (args == null || args.Length == 0)
                    return new T();

                var sa = new T();

                int i = 0;
                bool[] _argumetMaped = new bool[args.Length];

                foreach (var property in sa.m_properties)
                {
                    var attr = property.GetCustomAttribute<StartupArgBaseAttribute>(sa.Inherit);

                    if (attr != null)
                        for (i = 0; i < args.Length; i++)
                        {
                            if (_argumetMaped[i])
                                continue;

                            var a = args[i];

                            if (attr.Match(a))
                            {
                                attr.SetValue(property, sa, a);

                                _argumetMaped[i] = true;

                                sa.ParsedCount++;

                                break;
                            }
                        }
                }

                return sa;
            }
            catch (Exception ex)
            {
                m_loger.Log("Возникло исключение при парсинге аргументов приложения. Возвращён пустой набор.", ex
                    , new LogParameter("Входные аргументы", args == null ? "NULL" : args.Length == 0 ? "EMPTY" : string.Join(";", args)));

                return new T();
            }
        }

        /// <summary>
        /// Получение списка свойств с соответсвующими атриюутами из текущего экземпляра
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<PropertyInfo> GetProperties()
        {
            return GetArgProperties(Inherit);
        }

        /// <summary>
        /// Поулчение всех свойств с аттрибутами аргументов приложения
        /// </summary>
        /// <param name="inherit">Флаг поиска аттрибутов по родителям</param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetArgProperties(bool inherit = true)
        {
            return typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.HasAttribute<StartupArgBaseAttribute>(inherit));
        }

        /// <summary>
        /// Получение всех входных аргументов приложения
        /// </summary>
        /// <returns>Возвращает набор проставленных аргументов приложения</returns>
        public static IEnumerable<StartupArgBaseAttribute> GetAttributes()
        {
            var attrs = new List<StartupArgBaseAttribute>();

            var aType = typeof(StartupArgBaseAttribute);
            foreach (var prop in GetArgProperties())
            {
                var a = prop.GetCustomAttribute<StartupArgBaseAttribute>();

                if (a != null)
                    attrs.Add(a);
            }

            return attrs;
        }

        /// <summary>
        /// Построение набора аргументов из текущих значений
        /// </summary>
        /// <returns>Возвращает StringBuilder с собранным набором текущих пропертей</returns>
        public virtual StringBuilder Build()
        {
            try
            {
                return new StringBuilder(250)
                    .Join(m_properties
                    //преобразуем Свойство с Атрибутом в строку согласно их методу AppendAttributeBase и добавляем разделитель между ними
                    , (s, p) => p.GetCustomAttribute<StartupArgBaseAttribute>().AppendAttribute(p, this, s)
                    , ArgSplitter);
            }
            catch (Exception ex)
            {
                m_loger.Log("Возникло исключение при генерации строки аргументов для экземпляра. Возвращена пустая строка.", ex);

                return new StringBuilder();
            }
        }
    }
}
