using System;

namespace Merlion.ECR.Update.Core.SeparatedAssemblyLoad
{
    /// <summary>
    /// Результат выполнения внешней операции
    /// </summary>
    /// <typeparam name="T">Тип возвращаемого значения</typeparam>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    internal class MarshalingResult<T> : MarshalByRefObject
    {
        /*
         * т.к. маршалирование Exception между доменами не всегда возможно
         * и в будущем возможны запросы каких-то не маршалируемых типов
         * 
         * делаем обёртку для результатов операций из другого AppDomain
         */

        /// <summary>
        /// Результат выполнения операции
        /// </summary>
        public T Result { get; private set; }
        /// <summary>
        /// Наличие ошибки при выполнении
        /// </summary>
        public bool HasError { get; private set; }
        /// <summary>
        /// Текст ошибки
        /// </summary>
        public string ErrorText { get; private set; }

        /// <summary>
        /// Маршалируемый результат операции
        /// </summary>
        /// <param name="res">Результат выполнения</param>
        /// <param name="errText">Текст ошибки</param>
        public MarshalingResult(T res, string errText)
        {
            Result = res;
            HasError = true;
            ErrorText = errText;
        }

        /// <summary>
        /// Успешный результат выполнения операции
        /// </summary>
        /// <param name="res">Результат</param>
        public MarshalingResult(T res)
        {
            Result = res;
        }

        string DebugDisplay()
        {
            return "Result: " + (Result?.ToString() ?? "NULL") + (HasError ? (" | " + ErrorText) : string.Empty);
        }
    }
}
