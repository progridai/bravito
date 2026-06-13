using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Bravito.Application.Knowledge.DTOs;

namespace Bravito.Application.Knowledge.Interfaces;

public interface IKnowledgeDocumentService
{
    Task<HealthResponse> CheckHealthAsync(CancellationToken cancellationToken = default);
    Task<UploadResponse> UploadAsync(Stream fileStream, string fileName, string? app, CancellationToken cancellationToken = default);
    Task<DeleteResponse> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ReplaceResponse> ReplaceAsync(Guid id, Stream fileStream, string fileName, CancellationToken cancellationToken = default);
    Task<ReprocessResponse> ReprocessAsync(Guid id, CancellationToken cancellationToken = default);
}
