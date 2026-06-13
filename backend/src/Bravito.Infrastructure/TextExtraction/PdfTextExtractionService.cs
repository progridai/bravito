using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bravito.Application.Knowledge.Interfaces;
using UglyToad.PdfPig;

namespace Bravito.Infrastructure.TextExtraction;

public class PdfTextExtractionService : ITextExtractionService
{
    public Task<string> ExtractTextAsync(string filePath, string mimeType, CancellationToken cancellationToken = default)
    {
        var sb = new StringBuilder();
        
        using (var document = PdfDocument.Open(filePath))
        {
            foreach (var page in document.GetPages())
            {
                sb.AppendLine(page.Text);
            }
        }
        
        return Task.FromResult(sb.ToString());
    }
}
