# 13 — Autenticação no Backend (ASP.NET Core)

## 🛡️ Visão Geral
A API do Bravito utiliza o padrão **JWT Bearer** para validar o acesso aos recursos protegidos. A validação é feita criptograficamente, confiando nos tokens emitidos pelo **Keycloak**.

## ⚙️ Configurações Atuais
- **Authority:** `http://localhost:8080/realms/bravito` (Garante que o token foi emitido pelo nosso realm).
- **Audience:** `bravito-api` (Garante que o token foi emitido para acesso a esta API).

*Nota: Foi adicionado um `Audience Mapper` no realm bravito para garantir que o token JWT do Flutter contenha o Audience `bravito-api`.*

## 📌 Endpoints de Teste

### 1. `GET /api/public/ping`
- **Acesso:** Público (Anônimo)
- **Objetivo:** Verificar se a API está no ar (Health Check simples).
- **Retorno:** HTTP 200 `{"message": "Bravito API online"}`

### 2. `GET /api/private/ping`
- **Acesso:** Protegido (Requer JWT)
- **Objetivo:** Verificar se a validação de token está operando corretamente.
- **Teste sem Token:** Retorna HTTP 401 Unauthorized.
- **Teste com Token:** Retorna HTTP 200 `{"message": "Acesso autenticado com sucesso"}`

### 3. `GET /api/auth/me`
- **Acesso:** Protegido (Requer JWT)
- **Objetivo:** Ler as *claims* do usuário contidas no token e retornar em JSON.

## 🔑 Como Testar no Swagger

1. Inicie a API e abra a URL do Swagger (geralmente `http://localhost:xxxx/swagger`).
2. Clique no botão **"Authorize"** (Cadeado verde).
3. No campo "Value", insira a palavra `Bearer` seguida de espaço e do seu Token JWT. Exemplo:
   ```text
   Bearer eyJhbGciOiJSUzI1NiIsInR5...
   ```
4. Clique em "Authorize" e depois em "Close". A partir de agora, as chamadas incluirão o cabeçalho correto.

## 🛠 Como Obter um Token JWT Localmente para Testes
Sem o Flutter pronto, você pode obter o token usando o Postman, cURL ou o painel de debug do Keycloak (na aba "Clients" > "bravito-flutter" > "Evaluate"). Uma forma simples via cURL (ajustando para um client público temporário de teste ou se o direct grants estivesse ativo), porém no nosso `bravito-flutter` desativamos Direct Access Grants para manter o padrão seguro PKCE. Se precisar gerar um token rápido apenas para backend tests, ative o *Direct Access Grants* no `bravito-flutter` provisoriamente, rode:
```bash
curl -X POST http://localhost:8080/realms/bravito/protocol/openid-connect/token \
  -d "client_id=bravito-flutter" \
  -d "username=dev.admin" \
  -d "password=Admin@123456" \
  -d "grant_type=password"
```
*(Pegue o valor de `access_token` no JSON retornado).*

## ⚠️ Cuidados em Produção
- O arquivo `appsettings.json` não deve conter Client Secrets (atualmente usamos Audience e Public Client, o que é seguro).
- O `RequireHttpsMetadata` deve ser mudado para `true` em produção.
- O CORS deve ser restrito apenas à URL de produção do frontend Flutter. Nunca deixe `localhost` ou `*` habilitados na nuvem.
- Erros (`GlobalExceptionHandler`) não devem retornar o *Stack Trace* para não expor a estrutura interna da aplicação (já configurado).
