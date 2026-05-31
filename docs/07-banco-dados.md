# 07 — Banco de Dados

## Banco oficial

O banco de dados oficial do Projeto Bravito será PostgreSQL.

## Regra principal

O PostgreSQL nunca deve ser acessado diretamente pelo Flutter.

O acesso ao banco deve acontecer somente pela API backend.

## Padrão obrigatório de nomenclatura

Toda a estrutura própria do banco de dados do Bravito deve usar nomes em português.

Essa regra vale para:

* Tabelas.
* Colunas.
* Índices.
* Constraints.
* Migrations.
* Entidades persistidas.
* Configurações do Entity Framework.

Não usar nomes de tabelas ou campos em inglês para estruturas próprias da aplicação.

## Convenção de nomes

### Tabelas

Usar nomes no plural, em português e com PascalCase.

Exemplos:

```text
Empresas
Usuarios
Perfis
UsuariosPerfis
ConversasChat
MensagensChat
LogsAuditoria
ConfiguracoesIntegracao
```

### Colunas

Usar nomes claros em português, também em PascalCase.

Exemplos:

```text
Id
Nome
Email
Ativo
EmpresaId
UsuarioId
DataCriacao
DataAlteracao
CriadoPor
AlteradoPor
```

### Campos de auditoria padrão

Sempre que fizer sentido, tabelas operacionais devem conter:

```text
DataCriacao
DataAlteracao
CriadoPor
AlteradoPor
Ativo
```

### Multiempresa

Toda tabela operacional que pertença a uma empresa deve conter:

```text
EmpresaId
```

Quando futuramente existir controle por unidade ou loja, usar:

```text
LojaId
```

## Entidades iniciais sugeridas

### Empresas

Representa o tenant ou empresa dona dos dados.

```text
Id
Nome
Documento
Ativo
DataCriacao
DataAlteracao
```

### Usuarios

Representa o vínculo local do usuário autenticado no Keycloak com a aplicação Bravito.

```text
Id
KeycloakUsuarioId
EmpresaId
Nome
Email
Ativo
DataCriacao
DataAlteracao
```

### Perfis

Representa perfis funcionais da aplicação.

```text
Id
Nome
Descricao
Ativo
DataCriacao
DataAlteracao
```

### UsuariosPerfis

Relaciona usuários com perfis.

```text
Id
UsuarioId
PerfilId
DataCriacao
```

### ConversasChat

Representa uma conversa iniciada por um usuário.

```text
Id
EmpresaId
UsuarioId
Titulo
DataCriacao
DataAlteracao
```

### MensagensChat

Representa mensagens enviadas pelo usuário, assistente ou sistema.

```text
Id
ConversaChatId
TipoRemetente
Mensagem
Metadados
DataCriacao
```

Valores possíveis para `TipoRemetente`:

```text
Usuario
Assistente
Sistema
```

### LogsAuditoria

Registra ações relevantes executadas no sistema.

```text
Id
EmpresaId
UsuarioId
Acao
Entidade
EntidadeId
EnderecoIp
UserAgent
Payload
DataCriacao
```

### ConfiguracoesIntegracao

Representa configurações de integrações externas por empresa.

```text
Id
EmpresaId
TipoIntegracao
UrlBase
ReferenciaSegredo
Ativo
DataCriacao
DataAlteracao
```

## Equivalência com termos técnicos

Quando houver termos arquiteturais em inglês, usar o equivalente em português no banco:

```text
Tenant              -> Empresa
User                -> Usuario
UserProfile         -> Usuario
Role                -> Perfil
ChatConversation    -> ConversaChat
ChatMessage         -> MensagemChat
AuditLog            -> LogAuditoria
IntegrationSetting  -> ConfiguracaoIntegracao
```

## Exceções permitidas

Podem permanecer em inglês apenas nomes técnicos externos que não pertencem diretamente ao modelo de dados do Bravito, como:

* Claims JWT: `sub`, `email`, `preferred_username`, `realm_access`.
* Client IDs do Keycloak: `bravito-flutter`, `bravito-api`.
* Variáveis exigidas por ferramentas externas.
* Nomes de pacotes, bibliotecas e frameworks.

## Regras

* Usar migrations.
* Não criar banco manual sem versionamento.
* Não armazenar secrets diretamente em tabelas comuns.
* Não registrar tokens em logs.
* Não criar tabelas fora do escopo sem aprovação.
* Não criar tabelas ou campos em inglês.
* Antes de criar uma migration, validar se os nomes estão em português.
