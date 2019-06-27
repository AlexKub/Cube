namespace PJT.Transactions
{
    /// <summary>
    /// Результат выполнения транзакции
    /// </summary>
    public enum TransactionResult
    {
        /// <summary>
        /// Не определён (по умолчанию)
        /// </summary>
        UnKnown,
        /// <summary>
        /// Возникла ошибка / непредвиденное состояние при выполнении
        /// </summary>
        Failed,
        /// <summary>
        /// Отменена по несоответствию условий
        /// </summary>
        ConditionalCancel,
        /// <summary>
        /// Выолпнена
        /// </summary>
        Success
    }
}