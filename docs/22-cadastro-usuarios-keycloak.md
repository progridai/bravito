# Cadastro e Gestão de Usuários (Integração Keycloak Admin API)

## Objetivo
Implementar endpoints administrativos que unificam a criação, edição, ativação e desativação de usuários em dois ambientes (Keycloak + Banco de Dados da Aplicação Bravito) simultaneamente, mantendo a integridade e garantindo um fluxo seguro orquestrado pelo backend.

## Decisão Arquitetural
Foi escolhida uma abordagem de delegação mista:
- O **Backend (ASP.NET Core)** é o orquestrador e guardião. Ele valida os acessos e faz chamadas Server-to-Server via API.
- O **Keycloak** continua sendo o Provedor de Identidade, gerenciando a vida útil do token, habilitação de conta para login e a senha (que, nesta implementação inicial, é enviada de forma temporária pelo request ou via action de email). **A senha nunca é salva no banco PostgreSQL**.
- O **Banco Bravito** consolida o `KeycloakId`, perfis de acesso, recursos, data de alteração/criação, sendo a base rápida para consultas do frontend (Dashboard/Cadastros) e controle de acesso interno.

## Integração com Keycloak Admin API

Para se comunicar com o Keycloak, o Backend usa o endpoint `/realms/{realm}/protocol/openid-connect/token` usando `grant_type=client_credentials`.
A configuração é fornecida no arquivo `appsettings.json` na seção `"KeycloakAdmin"`:

```json
  "KeycloakAdmin": {
    "BaseUrl": "http://keycloak:8080",
    "Realm": "bravito",
    "ClientId": "bravito-api-admin",
    "ClientSecret": "CHANGE_ME",
    "TimeoutSeconds": 30
  }
```

### Como Configurar o Client Administrativo no Keycloak

1. Logue no Keycloak Console com um usuário administrador do master ou do realm.
2. Acesse o realm `bravito`.
3. Vá em **Clients** -> **Create Client**.
   - Client ID: `bravito-api-admin`.
   - Habilite "Client authentication".
   - Desmarque "Standard flow" e marque "Service accounts roles".
   - Salve.
4. Na aba **Credentials**, copie a "Client Secret".
5. Na aba **Service account roles**, clique em **Assign role** -> Filtre por "Filter by clients" -> Encontre o client `realm-management` -> Adicione as roles `manage-users`, `view-users`.
6. Coloque a "Client Secret" no seu arquivo `.env` para o ambiente ou atualize a secret diretamente no repositório final de secrets.

## Criação e Edição de Usuário

O método orquestrador em `UsuariosAdminService` faz a ponte.

- Na criação: Checa e-mail local. Tenta criar usuário via Keycloak. Pega a location com UUID do usuário recém criado. Grava no PostgreSQL junto com os Perfis. Retorna sem exibir a senha em lugar nenhum.
- Na edição e Desativação: Checa se o usuário existe, faz a atualização base no DB da aplicação, e aplica os mesmos dados no Keycloak (como desabilitar conta usando o `enabled=false`).

## Endpoints e Recursos

- `GET /api/usuarios`: Exige `usuarios.visualizar`.
- `GET /api/usuarios/{id}`: Exige `usuarios.visualizar`.
- `POST /api/usuarios`: Exige `usuarios.cadastrar`.
- `PUT /api/usuarios/{id}`: Exige `usuarios.editar`.
- `PATCH /api/usuarios/{id}/ativar`: Exige as permissões `usuarios.desativar` (ou editar, via controller) para modificar o status de ativo.
- `PATCH /api/usuarios/{id}/desativar`: Exige `usuarios.desativar`.

## Limitações Atuais

- O projeto não enviará e-mail diretamente via SMTP se o Keycloak não estiver configurado para tal. Para contornar, o envio inicial é suportado apenas como senha temporária criada no request (`SenhaTemporaria`), mas não gravada.
- A exclusão permanente de usuários (DELETE) foi contornada pelo uso de _soft-delete_ lógico (Ativo/Inativo) para manter a rastreabilidade e histórico de chats/mensagens amarradas a aquele ID.

## Próximo Passo Recomendado

Criar os formulários e a Tabela Administrativa em Flutter para listar e criar usuários visulamente consumindo estes endpoints.
