namespace PJT.Models
{
    /// <summary>
    /// Метки Результатов операций
    /// </summary>
    public enum OperationResultMark
    {
        /// <summary>
        /// Не определён (по умолчанию)
        /// </summary>
        UnKnown,
        /// <summary>
        /// Ошибка
        /// </summary>
        Error,
        /// <summary>
        /// Успех
        /// </summary>
        Success,
        /// <summary>
        /// Неожиданное состояние
        /// </summary>
        Unexpected,
        /// <summary>
        /// Пустой результат
        /// </summary>
        Empty,
        /// <summary>
        /// Проблема во вложенной операции
        /// </summary>
        Nested
    }
}