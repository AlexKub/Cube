using Merlion.ECR.Update.Core.Log;
using System;

namespace Merlion.ECR.Update.Core.StartupArguments
{
    /// <summary>
    /// Базовый атрибут для мапинга свойста - входного аргумента приложения
    /// </summary>
    public abstract class StartupArgBaseAttribute : Attribute
    {
        /// <summary>
        /// Имя входного аргумента Приложения
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Соответствующий аргумент из входных параметров был сопоставлен с текущим свойством
        /// </summary>
        public bool HasMapped { get; protected set; }

        /// <summary>
        /// Описание аттрибута (инструкция)
        /// </summary>
        public string Description { get; private set; } = "Not Set";

        /// <summary>
        /// Пример использования аттрибута
        /// </summary>
        public string Example { get; private set; } = "Not Set";

        /// <summary>
        /// Атрибут без конвертации значения
        /// </summary>
        /// <param name="name">Имя аргумента</param>
        public StartupArgBaseAttribute(string name, string descr = null, string example = null)
        {
            Name = name;

            if (descr != null)
                Description = descr;

            if (example != null)
                Example = example;
        }

        public abstract void SetValue<T>(System.Reflection.PropertyInfo property, T args, string atr) where T : StartupArgs<T>, new();

        /// <summary>
        /// Проверка на соответствие Атрибута аргументу
        /// </summary>
        /// <param name="arg">Входной аргумент</param>
        /// <returns>Возвращает флаг соответствия Атрибута аргументу</returns>
        public abstract bool Match(string arg);

        /// <summary>
        /// Добавление текущего Атрибута к Строке аргументов
        /// </summary>
        /// <param name="property">Свойство, связанное с Атрибутом</param>
        /// <param name="instance">Экземпляра StartupAtrs, на котором извлекается свойство</param>
        /// <param name="attribute">Атрибут</param>
        /// <param name="sb">Собираемая строка</param>
        /// <returns>Возвращает собираемую строку со значением текущего аргумента</returns>
        public System.Text.StringBuilder AppendAttribute<TInstance>(System.Reflection.PropertyInfo property, TInstance instance, System.Text.StringBuilder sb)
            where TInstance : StartupArgs
        {
            try
            {
                //общие проверки и отлов исключений делаем здесь, чтобы не ебаться при каждом наследовании
                if (property == null)
                    throw new ArgumentNullException("При построении Строки аргументов не передана ссылка на свойство");

                if (instance == null)
                    throw new ArgumentNullException("При построении Строки аргументов не передана ссылка на экземпляр");

                if (sb == null)
                    sb = new System.Text.StringBuilder();

                //основная логика преобразования в строку 
                //для каждого наследника своя
                return AppendAttributeBase(property, instance, sb);
            }
            catch(Exception ex)
            {
                StartupArgs.Loger.Log("Возникло исключение при добавлении Аргумента к Строке аргументов", ex
                    , new LogParameter("Свойство", property == null ? "NULL" : property.Name)
                    , new LogParameter("Экземпляр", instance == null ? "NULL" : instance.GetType().FullName));
            }

            return sb;
        }

        /// <summary>
        /// Безопасное добавление Атрибута к Строке аргументов
        /// </summary>
        /// <param name="property">Свойство, связанное с Атрибутом</param>
        /// <param name="instance">Экземпляра StartupAtrs, на котором извлекается свойство</param>
        /// <param name="attribute">Атрибут</param>
        /// <param name="sb">Собираемая строка</param>
        /// <remarks>Проверки входных аргументов и логирование уже есть выше</remarks>
        /// <returns>Возвращает собираемую строку со значением текущего аргумента</returns>
        protected abstract System.Text.StringBuilder AppendAttributeBase<TInstance>(System.Reflection.PropertyInfo property, TInstance instance, System.Text.StringBuilder sb)
            where TInstance : StartupArgs;
    }
}
