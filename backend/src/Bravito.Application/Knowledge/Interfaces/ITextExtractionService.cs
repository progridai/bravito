using System.Threading;
using System.Threading.Tasks;

namespace Bravito.Application.Knowledge.Interfaces;

public interface ITextExtractionService
{
    Task<string> ExtractTextAsync(string filePath, string mimeType, CancellationToken cancellationToken = default);
}
