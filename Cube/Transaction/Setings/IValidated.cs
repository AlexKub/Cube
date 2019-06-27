namespace PJT
{
    /// <summary>
    /// Валидируемый объект
    /// </summary>
    public interface IValidated
    {
        /// <summary>
        /// Флаг валидности объекта
        /// </summary>
        bool IsValid { get; }
    }
}
