using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Bravito.Application.Acesso.Interfaces;
using Bravito.Infrastructure.Integrations.Keycloak.Options;
using Microsoft.Extensions.Options;

namespace Bravito.Infrastructure.Integrations.Keycloak
{
    public class KeycloakAdminService : IKeycloakAdminService
    {
        private readonly HttpClient _httpClient;
        private readonly KeycloakAdminOptions _options;
        private string? _adminToken;
        private DateTime _tokenExpiration;

        public KeycloakAdminService(HttpClient httpClient, IOptions<KeycloakAdminOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        private async Task<string> GetAdminTokenAsync(CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(_adminToken) && DateTime.UtcNow < _tokenExpiration)
            {
                return _adminToken;
            }

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_options.BaseUrl}/realms/{_options.Realm}/protocol/openid-connect/token");
            
            var collection = new List<KeyValuePair<string, string>>
            {
                new("grant_type", "client_credentials"),
                new("client_id", _options.ClientId),
                new("client_secret", _options.ClientSecret)
            };
            request.Content = new FormUrlEncodedContent(collection);

            var response = await _httpClient.SendAsync(request, cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorMsg = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new Exception($"Erro ao obter token do Keycloak Admin API: {errorMsg}");
            }

            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            var tokenData = JsonSerializer.Deserialize<JsonElement>(json);
            
            _adminToken = tokenData.GetProperty("access_token").GetString();
            var expiresIn = tokenData.GetProperty("expires_in").GetInt32();
            _tokenExpiration = DateTime.UtcNow.AddSeconds(expiresIn - 10); // Margem de segurança

            return _adminToken!;
        }

        public async Task<string> CriarUsuarioAsync(string nome, string email, string senhaTemporaria, bool ativo, CancellationToken cancellationToken = default)
        {
            var token = await GetAdminTokenAsync(cancellationToken);
            
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_options.BaseUrl}/admin/realms/{_options.Realm}/users");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Separar Nome em FirstName e LastName de forma básica
            var partesNome = nome.Split(' ', 2);
            var firstName = partesNome[0];
            var lastName = partesNome.Length > 1 ? partesNome[1] : "";

            var userData = new
            {
                username = email,
                email = email,
                enabled = ativo,
                emailVerified = true,
                firstName = firstName,
                lastName = lastName,
                credentials = new[]
                {
                    new
                    {
                        type = "password",
                        value = senhaTemporaria,
                        temporary = true
                    }
                }
            };

            request.Content = new StringContent(JsonSerializer.Serialize(userData), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorMsg = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new Exception($"Erro ao criar usuário no Keycloak: {response.StatusCode} - {errorMsg}");
            }

            // O Keycloak retorna a URL do usuário criado no header Location
            var location = response.Headers.Location?.ToString();
            if (string.IsNullOrEmpty(location))
            {
                throw new Exception("O Keycloak não retornou o cabeçalho Location contendo o ID do usuário criado.");
            }

            // A URL geralmente é .../users/{id}
            var id = location.Substring(location.LastIndexOf('/') + 1);
            return id;
        }

        public async Task AtualizarUsuarioAsync(string keycloakId, string nome, string email, bool ativo, CancellationToken cancellationToken = default)
        {
            var token = await GetAdminTokenAsync(cancellationToken);
            
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_options.BaseUrl}/admin/realms/{_options.Realm}/users/{keycloakId}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var partesNome = nome.Split(' ', 2);
            var firstName = partesNome[0];
            var lastName = partesNome.Length > 1 ? partesNome[1] : "";

            var userData = new
            {
                email = email,
                enabled = ativo,
                firstName = firstName,
                lastName = lastName
            };

            request.Content = new StringContent(JsonSerializer.Serialize(userData), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorMsg = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new Exception($"Erro ao atualizar usuário no Keycloak: {response.StatusCode} - {errorMsg}");
            }
        }

        public async Task HabilitarDesabilitarUsuarioAsync(string keycloakId, bool ativo, CancellationToken cancellationToken = default)
        {
            var token = await GetAdminTokenAsync(cancellationToken);
            
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_options.BaseUrl}/admin/realms/{_options.Realm}/users/{keycloakId}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var userData = new
            {
                enabled = ativo
            };

            request.Content = new StringContent(JsonSerializer.Serialize(userData), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorMsg = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new Exception($"Erro ao habilitar/desabilitar usuário no Keycloak: {response.StatusCode} - {errorMsg}");
            }
        }
    }
}
