using System;
using System.Reflection;
using System.Text;
using Merlion.ECR.Update.Core.Log;

namespace Merlion.ECR.Update.Core.StartupArguments
{
    /// <summary>
    /// Входной парный атрибут ключ-значение
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public abstract class StartupArgValueAttribute : StartupArgBaseAttribute
    {
        /// <summary>
        /// Разделитель имени-значения для входящего аргумента
        /// </summary>
        public const char DefaultSplitChar = '=';

        readonly char m_SplitChar;

        /// <summary>
        /// Значение аргумента
        /// </summary>
        public object Value { get; protected set; }

        /// <summary>
        /// Символ-разделитель пары ключ-значение
        /// </summary>
        public char SplitChar { get { return m_SplitChar; } }

        public virtual bool IsEmpty { get { return Value == null; } }

        /// <summary>
        /// Атрибут без конвертации значения
        /// </summary>
        /// <param name="name">Имя аргумента</param>
        public StartupArgValueAttribute(string name, char splitChar = DefaultSplitChar, string descr = null, string example = null) : base(name, descr, example)
        {
            m_SplitChar = splitChar;
        }

        public sealed override void SetValue<T>(PropertyInfo property, T args, string incArg)
        {
            try
            {
                if (args == null)
                    throw new ArgumentNullException("Пустая ссылка на экземпляр Аргументов приложения");

                if (property == null)
                    throw new ArgumentNullException("Пустая ссылка на проставляемое свойство");

                if (string.IsNullOrWhiteSpace(incArg))
                    throw new InvalidOperationException("Попытка распарсить пустой аргумент");

                //берём значение из входного аргумента
                var val = ParseArgValue(incArg);

                Value = ParseValue(val);
                //проставляем значение в свойство
                property.SetValue(args, Value);

                HasMapped = true;
            }
            catch (Exception ex)
            {
                StartupArgs.Loger.Log("Возникло исключение при простановке/конвертации значения входного аргумента", ex
                    , new LogParameter("Name", LogMessageBuilder.GetStringLogVal(Name))
                    , new LogParameter("Входное значение", LogMessageBuilder.GetStringLogVal(incArg))
                    , new LogParameter("Проставляемое свойство", LogMessageBuilder.GetStringLogVal(property.Name)));
            }
        }

        /// <summary>
        /// Конвертация строкового значения аргумента
        /// </summary>
        /// <param name="value">Сроковое значение аргумента (экранирующие кавычки исключены)</param>
        /// <returns>Возвращает обёртку над указанным типом</returns>
        protected abstract object ParseValue(string value);

        /// <summary>
        /// Получение строкового представления для текущего значения (без экранирующих кавычек)
        /// </summary>
        /// <returns>Строковое значение для построения аргументов</returns>
        /// <remarks>Экранирующие кавычки проставляются для значения автоматически</remarks>
        protected abstract string GetStringValue<TInstance>(PropertyInfo property, TInstance instance);

        /// <summary>
        /// Проверка на соответствие аргумента текущему атрибуту по названию
        /// </summary>
        /// <param name="arg">"Сырой" входной аргумент приложения</param>
        /// <returns>Возвращает флаг соответствия аргументов</returns>
        public override bool Match(string arg)
        {
            if (arg == null)
                return false;

            var splitIndex = arg.IndexOf(SplitChar);

            if (splitIndex < 0)
                return false;

            var name = arg.Substring(0, splitIndex);

            return Name.Equals(name, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Парсинг значения Аргумента с логированием исключения
        /// </summary>
        /// <param name="rawAttr">Входной чистый Агрумент в формате ключ'разделитель'значение</param>
        /// <returns>Вовращает значение (экранирующие кавычки исключены) или null при исключении</returns>
        string ParseArgValue(string rawAttr)
        {
            try
            {
                var atrVal = rawAttr.Split(m_SplitChar);

                if (atrVal.Length < 1)
                    throw new IndexOutOfRangeException($"Неожиданный результат парсинга аргумента '{rawAttr}'. Длина массива: {atrVal.Length.ToString()}");

                var atrName = atrVal[0];
                if (!atrName.Equals(Name, StringComparison.InvariantCultureIgnoreCase))
                    throw new InvalidOperationException($"Попытка парсинга не соответствующего атрибута. Получен: {atrName} Ожидался: {Name}");

                const char quote = '"';

                var rawVal = string.Empty;
                switch (atrVal.Length)
                {
                    case 1:
                        break; //если значения нет
                    case 2:
                        rawVal = atrVal[1]; //при "нормальных условиях"
                        break;
                    default:
                        //для случая, когда разделитель встречается в значении (значение разбито на несколько)
                        for (int i = 1; i < atrVal.Length; i++)
                            rawVal += atrVal[i]; //собираем в одно
                        break;
                }

                //удаляем кавычки
                if (rawVal != null && rawVal.Length > 2)
                {
                    var c = rawVal[0];
                    if (c == quote)
                        rawVal = rawVal.Remove(0, 1);

                    var lastIndex = rawVal.Length - 1;
                    c = rawVal[lastIndex];

                    if (c == quote)
                        rawVal = rawVal.Remove(lastIndex, 1);
                }

                return rawVal;
            }
            catch (Exception ex)
            {
                StartupArgs.Loger.Log("Возникло исключение при парсинге входного атрибута", ex
                    , new LogParameter("Входное значение", LogMessageBuilder.GetStringLogVal(rawAttr)));

                return null;
            }
        }

        protected sealed override StringBuilder AppendAttributeBase<TInstance>(PropertyInfo property, TInstance instance, StringBuilder sb)
        {
            //Имя="Значение"
            if (PropertyHasValue(property, instance))
                return sb.Append(Name).Append(SplitChar).Append("\"").Append(GetStringValue(property, instance)).Append("\"");
            else
                return sb;
        }

        protected virtual bool PropertyHasValue<TInstance>(PropertyInfo property, TInstance instance)
        {
            var val = property.GetValue(instance);

            return val != null;
        }

        string DebugDisplay()
        {
            return (Name == null ? "NULL" : Name);
        }
    }
}
