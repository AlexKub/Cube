using Merlion.ECR.Update.Core.Log;
using System;
using System.Reflection;
using System.Text;

namespace Merlion.ECR.Update.Core.StartupArguments
{
    /// <summary>
    /// Входной атрибут-флаг
    /// </summary>
    public class BoolArgAttribute : StartupArgBaseAttribute
    {
        /// <summary>
        /// Атрибут без конвертации значения
        /// </summary>
        /// <param name="name">Имя аргумента</param>
        public BoolArgAttribute(string name, string descr = null) : base(name, descr, name) { }

        public override bool Match(string arg)
        {
            if (arg == null)
                return false;

            return arg.Equals(Name, StringComparison.CurrentCultureIgnoreCase);
        }

        public override void SetValue<T>(PropertyInfo property, T args, string incArg)
        {
            try
            {
                if (args == null)
                    throw new ArgumentNullException("Пустая ссылка на экземпляр Аргументов приожения");

                if (property == null)
                    throw new ArgumentNullException("Пустая ссылка на проставляемое свойство");

                if (string.IsNullOrWhiteSpace(incArg))
                    throw new ArgumentNullException("Попытка проставить пустой аргумент");

                if (!incArg.Equals(Name, StringComparison.InvariantCultureIgnoreCase))
                    throw new ArgumentException("Имя полученного аргумента не соответствует ожидаемому");

                //проставляем значение в свойство
                property.SetValue(args, true);

                HasMapped = true;
            }
            catch (Exception ex)
            {
                StartupArgs<T>.Loger.Log("Возникло исключение при простановке/конвертации значения входного аргумента", ex
                    , new LogParameter("Name", LogMessageBuilder.GetStringLogVal(Name))
                    , new LogParameter("Входное значение", LogMessageBuilder.GetStringLogVal(incArg))
                    , new LogParameter("Проставляемое свойство", LogMessageBuilder.GetStringLogVal(property.Name)));
            }
        }

        protected override StringBuilder AppendAttributeBase<TInstance>(PropertyInfo property, TInstance instance, StringBuilder sb)
        {
            //если свойство проставлено - ставим флаг
            if ((bool)property.GetValue(instance))
                return sb.Append(Name);
            else
                return sb;
        }
    }
}
