using System;

namespace Bravito.Application.Knowledge.DTOs;

public class ReprocessResponse
{
    public bool Success { get; set; }
    public Guid Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public int ChunkCount { get; set; }
}
