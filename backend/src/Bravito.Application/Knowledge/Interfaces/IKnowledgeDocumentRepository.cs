using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bravito.Domain.Knowledge.Entities;

namespace Bravito.Application.Knowledge.Interfaces;

public interface IKnowledgeDocumentRepository
{
    Task<KnowledgeDocument?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<KnowledgeDocument>> ListAsync(string? app = null, bool includeDeleted = false, CancellationToken cancellationToken = default);
    Task AddAsync(KnowledgeDocument document, CancellationToken cancellationToken = default);
    Task UpdateAsync(KnowledgeDocument document, CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
