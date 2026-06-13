using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bravito.Application.Knowledge.Interfaces;
using Bravito.Domain.Knowledge.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Bravito.Infrastructure.Data.Repositories;

public class KnowledgeDocumentRepository : IKnowledgeDocumentRepository
{
    private readonly KnowledgeDbContext _dbContext;
    private IDbContextTransaction? _currentTransaction;

    public KnowledgeDocumentRepository(KnowledgeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<KnowledgeDocument?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.KnowledgeDocuments.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task<List<KnowledgeDocument>> ListAsync(string? app = null, bool includeDeleted = false, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.KnowledgeDocuments.AsQueryable();

        if (!string.IsNullOrWhiteSpace(app))
            query = query.Where(d => d.App == app);

        if (!includeDeleted)
            query = query.Where(d => d.DeletedAt == null);

        return await query.OrderByDescending(d => d.UploadedAt).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(KnowledgeDocument document, CancellationToken cancellationToken = default)
    {
        await _dbContext.KnowledgeDocuments.AddAsync(document, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(KnowledgeDocument document, CancellationToken cancellationToken = default)
    {
        _dbContext.KnowledgeDocuments.Update(document);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null)
            throw new InvalidOperationException("A transaction is already in progress.");

        _currentTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
            if (_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync(cancellationToken);
            }
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync(cancellationToken);
            }
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }
}
