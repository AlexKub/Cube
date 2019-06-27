using Microsoft.ProjectServer.Client;

namespace PJT.Transactions
{
    /// <summary>
    /// Контекст для работы с Project
    /// </summary>
    public interface IProjectContext : IErrorsCollection
    {
        /// <summary>
        /// Информация из Настроек транзакции Project
        /// </summary>
        Settings.Project Settings { get; }
        /// <summary>
        /// CSOM-контекст
        /// </summary>
        ProjectContext ProjectContext { get; }
        /// <summary>
        /// Публикуемый проект
        /// </summary>
        PublishedProject PublishedProject { get; }
    }
}
