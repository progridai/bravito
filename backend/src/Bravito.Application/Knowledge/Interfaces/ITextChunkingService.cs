using System.Collections.Generic;

namespace Bravito.Application.Knowledge.Interfaces;

public interface ITextChunkingService
{
    List<string> ChunkText(string text);
}
