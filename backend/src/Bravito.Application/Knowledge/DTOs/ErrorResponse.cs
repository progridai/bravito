namespace Bravito.Application.Knowledge.DTOs;

public class ErrorResponse
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public string Step { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public string? TraceId { get; set; }
}
