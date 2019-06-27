namespace PJT.Transactions
{
    /// <summary>
    /// Объект с Project-контекстом
    /// </summary>
    interface IProjectContextHost
    {
        /// <summary>
        /// Контекст Project
        /// </summary>
        IProjectContext Context { get; }
    }
}
