# 07 — Banco de Dados

## Banco oficial

O banco de dados oficial do projeto Bravito será PostgreSQL.

## Regra principal

O PostgreSQL nunca deve ser acessado diretamente pelo Flutter.

O acesso ao banco deve acontecer somente pela API backend.

## Dados iniciais da fase 1

A fase 1 deve armazenar:

- Tenants.
- Perfis de usuário.
- Conversas.
- Mensagens.
- Auditoria.
- Configurações de integração.

## Entidades iniciais sugeridas

### Tenant

```text
Id
Name
Document
Active
CreatedAt
UpdatedAt
UserProfile
Id
KeycloakUserId
TenantId
Name
Email
Active
CreatedAt
UpdatedAt
ChatConversation
Id
TenantId
UserId
Title
CreatedAt
UpdatedAt
ChatMessage
Id
ConversationId
SenderType
Message
Metadata
CreatedAt

Valores possíveis para SenderType:

User
Assistant
System
AuditLog
Id
TenantId
UserId
Action
Entity
EntityId
IpAddress
UserAgent
Payload
CreatedAt
IntegrationSetting
Id
TenantId
IntegrationType
BaseUrl
SecretReference
Active
CreatedAt
UpdatedAt
Multiempresa

Toda tabela operacional deve considerar:

TenantId.
UserId.
CreatedAt.
UpdatedAt.
Active, quando aplicável.
Regras
Usar migrations.
Não criar banco manual sem versionamento.
Não armazenar secrets diretamente em tabelas comuns.
Não registrar tokens em logs.
Não criar tabelas fora do escopo sem aprovação.