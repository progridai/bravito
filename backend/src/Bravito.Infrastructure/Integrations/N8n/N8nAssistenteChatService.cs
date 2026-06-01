using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Bravito.Application.Chat.Interfaces;
using Bravito.Application.Chat.Models;
using Bravito.Infrastructure.Integrations.N8n.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bravito.Infrastructure.Integrations.N8n
{
    public class N8nAssistenteChatService : IAssistenteChatService
    {
        private readonly HttpClient _httpClient;
        private readonly N8nOptions _options;
        private readonly ILogger<N8nAssistenteChatService> _logger;

        public N8nAssistenteChatService(
            HttpClient httpClient, 
            IOptions<N8nOptions> options, 
            ILogger<N8nAssistenteChatService> logger)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _logger = logger;
            _httpClient.Timeout = TimeSpan.FromSeconds(_options.TimeoutSeconds);
        }

        public async Task<EnviarMensagemChatResponse> EnviarMensagemAsync(EnviarMensagemChatRequest request, UsuarioAutenticado usuario, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando envio de mensagem para o assistente n8n. UsuarioId: {UsuarioId}", usuario.Id);

            try
            {
                var payload = new
                {
                    empresaId = usuario.EmpresaId,
                    usuarioId = usuario.Id,
                    nomeUsuario = usuario.NomeUsuario,
                    email = usuario.Email,
                    conversaId = request.ConversaId,
                    mensagem = request.Mensagem,
                    origem = "bravito-api",
                    dataHora = DateTime.UtcNow.ToString("o")
                };

                var response = await _httpClient.PostAsJsonAsync(_options.WebhookUrl, payload, cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync(cancellationToken);
                
                string textoResposta = string.Empty;
                bool sucesso = true;

                try
                {
                    using var jsonDoc = JsonDocument.Parse(responseString);
                    var root = jsonDoc.RootElement;

                    if (root.ValueKind == JsonValueKind.Object)
                    {
                        if (root.TryGetProperty("message", out var msgElement) && msgElement.ValueKind == JsonValueKind.String)
                        {
                            textoResposta = msgElement.GetString() ?? string.Empty;
                            if (root.TryGetProperty("success", out var successElement)) sucesso = successElement.GetBoolean();
                        }
                        else if (root.TryGetProperty("resposta", out var respElement) && respElement.ValueKind == JsonValueKind.String)
                        {
                            textoResposta = respElement.GetString() ?? string.Empty;
                            if (root.TryGetProperty("sucesso", out var sucessoElement)) sucesso = sucessoElement.GetBoolean();
                        }
                        else if (root.TryGetProperty("output", out var outElement) && outElement.ValueKind == JsonValueKind.String)
                        {
                            textoResposta = outElement.GetString() ?? string.Empty;
                        }
                        else if (root.TryGetProperty("text", out var textElement) && textElement.ValueKind == JsonValueKind.String)
                        {
                            textoResposta = textElement.GetString() ?? string.Empty;
                        }
                        else
                        {
                            textoResposta = responseString; // JSON genérico
                        }
                    }
                    else if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 0)
                    {
                        textoResposta = root[0].ToString(); // Array JSON
                    }
                    else
                    {
                        textoResposta = responseString;
                    }
                }
                catch (JsonException)
                {
                    // Não é JSON, provavelmente texto puro retornado pelo Respond to Webhook do n8n
                    textoResposta = responseString;
                }

                if (string.IsNullOrWhiteSpace(textoResposta))
                {
                    textoResposta = "✅ O n8n recebeu a mensagem com sucesso, mas retornou uma resposta vazia.";
                }

                _logger.LogInformation("Mensagem processada pelo assistente com sucesso. UsuarioId: {UsuarioId}", usuario.Id);

                return new EnviarMensagemChatResponse
                {
                    Sucesso = sucesso,
                    Resposta = textoResposta,
                    ConversaId = request.ConversaId
                };
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException || !cancellationToken.IsCancellationRequested)
            {
                _logger.LogError(ex, "Timeout ao chamar webhook do n8n para UsuarioId: {UsuarioId}", usuario.Id);
                return new EnviarMensagemChatResponse
                {
                    Sucesso = false,
                    MensagemErro = "A comunicação com o assistente demorou muito para responder.",
                    ConversaId = request.ConversaId
                };
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro HTTP ao chamar n8n para UsuarioId: {UsuarioId}. StatusCode: {StatusCode}", usuario.Id, ex.StatusCode);
                return new EnviarMensagemChatResponse
                {
                    Sucesso = false,
                    MensagemErro = "Houve um erro de comunicação com o assistente.",
                    ConversaId = request.ConversaId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao processar mensagem do n8n. UsuarioId: {UsuarioId}", usuario.Id);
                return new EnviarMensagemChatResponse
                {
                    Sucesso = false,
                    MensagemErro = "Ocorreu um erro interno ao processar sua mensagem.",
                    ConversaId = request.ConversaId
                };
            }
        }
    }
}
