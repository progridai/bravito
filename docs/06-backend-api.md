# 06 — Backend API

## Objetivo

O backend será uma API em ASP.NET Core responsável por autenticação, autorização, integração com banco, integração com n8n, auditoria e regras de aplicação.

## Stack

Usar:

- ASP.NET Core.
- C#.
- Clean Architecture.
- PostgreSQL.
- Entity Framework Core.
- Keycloak.
- JWT Bearer Authentication.
- Serilog.
- Swagger/OpenAPI.
- Docker.
- Health Checks.

## Estrutura

```text
backend/
  src/
    Bravito.Api/
    Bravito.Application/
    Bravito.Domain/
    Bravito.Infrastructure/
    Bravito.Shared/

  tests/
    Bravito.UnitTests/
    Bravito.IntegrationTests/
Responsabilidades
Bravito.Api

Responsável por:

Controllers.
Middlewares.
Autenticação JWT.
Autorização.
Swagger.
Health Checks.
Tratamento global de erros.
Versionamento de API.
Bravito.Application

Responsável por:

Casos de uso.
DTOs.
Commands.
Queries.
Interfaces de serviços.
Validações de aplicação.
Orquestração entre domínio e infraestrutura.
Bravito.Domain

Responsável por:

Entidades.
Value Objects.
Regras de domínio.
Enums.
Contratos centrais.
Bravito.Infrastructure

Responsável por:

PostgreSQL.
Entity Framework Core.
Repositórios.
Integração com n8n.
Integração com Keycloak.
Serviços externos.
Logs técnicos.
Bravito.Shared

Responsável por:

Tipos compartilhados.
Result pattern.
Constantes.
Helpers genéricos.
Extensões.
Endpoints iniciais esperados
Auth
GET  /api/auth/me
POST /api/auth/logout
POST /api/auth/refresh
Chat
POST /api/chat/conversations
GET  /api/chat/conversations
GET  /api/chat/conversations/{id}/messages
POST /api/chat/conversations/{id}/messages
Health
GET /health
GET /health/database
GET /health/n8n
Regras obrigatórias
API deve validar JWT.
API deve validar permissões.
API deve validar TenantId.
API deve registrar auditoria.
API deve proteger endpoints sensíveis.
API deve não retornar stack trace ao frontend.
API deve não expor segredos em logs.
API deve centralizar chamadas ao n8n.