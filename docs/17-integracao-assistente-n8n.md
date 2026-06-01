# Integração com Assistente n8n

## Objetivo
Esta etapa estabelece a infraestrutura no backend para comunicação segura entre a API do Bravito e o Webhook do Assistente no n8n. O foco principal é garantir o envio de mensagens encapsuladas com o contexto do usuário (id, empresa, email, etc.) mantendo a proteção dos dados.

## Por que o Flutter não chama o n8n diretamente?
Conforme as Políticas de Segurança do projeto e a arquitetura `Clean Architecture` adotada:
1. **Proteção da URL e Headers Secretos:** Chamar o n8n do Flutter exporia a URL do webhook e eventuais tokens/secrets no código client-side (que pode ser descompilado).
2. **Confiança do Contexto:** Se o frontend montar o payload, um usuário mal intencionado poderia forjar o `usuarioId` ou `empresaId`. Ao passar pela API, nós extraímos os dados diretamente do Token JWT validado (via Keycloak), garantindo que o payload enviado para o n8n seja 100% autêntico e seguro.
3. **Resiliência e Logs:** O backend pode aplicar *timeouts*, tentativas (*retries*) e registrar logs de auditoria sem expor erros técnicos ao usuário final.

## Endpoints Criados

### 1. `POST /api/chat/enviar`
Endpoint principal, exige autenticação Bearer JWT válida.

**Payload de Entrada:**
```json
{
  "conversaId": "uuid-da-conversa-opcional",
  "mensagem": "Olá, qual a minha escala de amanhã?"
}
```

**Payload Enviado pela API ao n8n:**
O backend intercepta, extrai o usuário do JWT e envia este formato para o n8n:
```json
{
  "empresaId": null,
  "usuarioId": "e23c0f49-...",
  "nomeUsuario": "dev.admin",
  "email": "admin@bravito",
  "conversaId": "uuid-da-conversa-opcional",
  "mensagem": "Olá, qual a minha escala de amanhã?",
  "origem": "bravito-api",
  "dataHora": "2026-06-01T14:30:00.0000000Z"
}
```

### 2. `GET /health/n8n`
Retorna o status da configuração do n8n na API. Não dispara chamadas para o assistente.
Pode ser testado livremente para verificar se a API leu a URL corretamente do `appsettings.json`.

## Formatos de Resposta Aceitos do n8n
O serviço foi desenvolvido para ser flexível e aceitar múltiplos formatos que o n8n possa retornar:

**Formato 1 (Padrão sugerido):**
```json
{ "success": true, "message": "Sua escala de amanhã é..." }
```
**Formato 2:**
```json
{ "sucesso": true, "resposta": "Sua escala de amanhã é..." }
```
**Formato 3:**
```json
{ "output": "Sua escala de amanhã é..." }
```
**Formato 4:**
```json
{ "text": "Sua escala de amanhã é..." }
```

## Como configurar e testar

A URL do Webhook do n8n foi definida via `appsettings.Development.json` (seção `N8n`). 
Em produção, ela será substituída pela variável de ambiente `N8n__WebhookUrl`.

### Teste (Sem Token - Deve falhar com 401 Unauthorized)
```bash
curl -X POST http://localhost:5132/api/chat/enviar \
  -H "Content-Type: application/json" \
  -d "{\"mensagem\": \"Teste sem token\"}"
```

### Teste (Com Token Válido)
```bash
# Obtenha um Token JWT no Keycloak ou copie do terminal do Flutter
curl -X POST http://localhost:5132/api/chat/enviar \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer <SEU_TOKEN_AQUI>" \
  -d "{\"mensagem\": \"Oi assistente, tudo bem?\"}"
```

## Próximo Passo Recomendado
O próximo passo deve ser conectar as mensagens geradas e renderizadas na tela de Chat do **Flutter** para chamar este novo endpoint `/api/chat/enviar` via `Dio`, consumindo a integração n8n diretamente pelo app em tempo real. Não implementaremos o histórico de conversas no PostgreSQL agora, apenas o envio e resposta efêmera.
