using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bravito.Application.Knowledge.Interfaces;

public interface IVectorDocumentRepository
{
    Task<bool> CheckDatabaseConnectionAsync(CancellationToken cancellationToken = default);
    Task<bool> CheckTableExistsAsync(string tableName, CancellationToken cancellationToken = default);
    Task InsertChunkAsync(Guid id, string text, string metadataJson, float[] embedding, CancellationToken cancellationToken = default);
    Task<int> DeleteChunksByDocumentIdAsync(Guid documentId, CancellationToken cancellationToken = default);
}
