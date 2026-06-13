using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Bravito.Application.Knowledge.Interfaces;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(Stream fileStream, string fileName, string documentId, CancellationToken cancellationToken = default);
    Task<bool> FileExistsAsync(string filePath);
    Task DeleteFileAsync(string filePath);
    bool CheckStoragePathWritable();
    string GetStoragePath();
}
