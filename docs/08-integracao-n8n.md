# 08 — Integração com n8n

## Objetivo

O n8n será usado para processar mensagens enviadas pelo chat e orquestrar o assistente de IA do Bravito.

## Regra principal

O Flutter nunca deve chamar o webhook do n8n diretamente.

A comunicação correta é:

```text
Flutter
  ↓
API Backend
  ↓
n8n
Fluxo correto
Usuário envia mensagem no Flutter
  ↓
Flutter envia para API
  ↓
API valida JWT
  ↓
API valida permissões
  ↓
API registra a mensagem
  ↓
API chama webhook do n8n
  ↓
n8n processa
  ↓
n8n retorna resposta
  ↓
API registra resposta
  ↓
API retorna para Flutter
Payload sugerido para envio ao n8n
{
  "tenantId": "empresa_001",
  "userId": "usuario_123",
  "userName": "Nome do Usuário",
  "conversationId": "conv_123",
  "message": "Mensagem enviada pelo usuário",
  "source": "bravito-app",
  "timestamp": "2026-05-29T00:00:00Z"
}
Resposta esperada do n8n
{
  "success": true,
  "conversationId": "conv_123",
  "message": "Resposta gerada pelo assistente",
  "metadata": {
    "workflow": "assistente-bravito",
    "executionId": "123456"
  }
}
Segurança

A API deve chamar o n8n usando:

URL configurada por variável de ambiente.
Token interno.
Timeout.
Tratamento de erro.
Log seguro.
Validação de resposta.
Headers sugeridos
Authorization: Bearer {N8N_INTERNAL_TOKEN}
X-Bravito-Tenant: empresa_001
X-Bravito-Source: api
Regras
Não colocar URL do webhook no Flutter.
Não colocar token do n8n no Flutter.
Não expor resposta técnica do n8n para o usuário.
Registrar falhas de integração.
Retornar mensagem amigável em caso de erro.