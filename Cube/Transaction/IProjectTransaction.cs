using Microsoft.ProjectServer.Client;
using System;

namespace PJT.Transactions
{
    /// <summary>
    /// Транзакция в рамках одного проекта SharePoint
    /// </summary>
    interface IProjectTransaction
    {
        /// <summary>
        /// ID публикуемого проекта в Project
        /// </summary>
        Guid PublishingProjectID { get; }

        /// <summary>
        /// Публикуемый проект
        /// </summary>
        PublishedProject PublishingProject { get; }
    }
}
