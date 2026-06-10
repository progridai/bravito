# Controle de Acesso por Recursos

## Decisão Arquitetural

Para garantir a segurança da aplicação de forma granular, adotamos uma arquitetura híbrida de controle de acesso:

1. **Autenticação (Quem é o usuário?)**: Responsabilidade exclusiva do **Keycloak**. O Keycloak emite o JWT Bearer contendo a identidade do usuário (claim `sub` mapeada para `KeycloakId`).
2. **Autorização (O que o usuário pode fazer?)**: Responsabilidade do **Banco de Dados da Aplicação (Bravito)**. A aplicação mapeia as permissões granulares por meio de *Recursos*, atribuindo-os a *Perfis de Acesso*, que são vinculados ao usuário autenticado.

Isso elimina a necessidade de sincronizar a todo momento regras de negócio complexas de permissões do sistema no Identity Provider, mantendo a responsabilidade no domínio do sistema Bravito.

## Entidades e Tabelas Criadas

- **Usuario** (`usuarios`): Representa um usuário interno, vinculado ao `KeycloakId` (Claim `sub`).
- **PerfilAcesso** (`perfis_acesso`): Agrupamentos lógicos de permissões (Ex: Administrador, Operador).
- **Recurso** (`recursos`): Permissões granulares do sistema (Ex: `chat.acessar`).
- **UsuarioPerfilAcesso** (`usuarios_perfis_acesso`): Relacionamento N:N entre Usuário e Perfil.
- **PerfilAcessoRecurso** (`perfis_acesso_recursos`): Relacionamento N:N entre Perfil e Recurso.

## Perfis Iniciais (Seed)

- **Administrador**: Possui todos os recursos.
- **Operador**: `chat.acessar`, `conversas.visualizar`.
- **Somente Chat**: `chat.acessar`.

## Recursos Iniciais (Seed)

- `chat.acessar`: Permite acessar e enviar mensagens no chat.
- `conversas.visualizar`: Permite visualizar histórico de conversas.
- `usuarios.visualizar`: Permite visualizar lista de usuários.
- `usuarios.cadastrar`: Permite cadastrar novos usuários.
- `usuarios.editar`: Permite editar usuários existentes.
- `usuarios.desativar`: Permite desativar/ativar usuários.

## Sincronização do Usuário

A cada chamada aos endpoints autenticados críticos (ex: `/api/auth/me` ou endpoints com o atributo `[RequerRecurso]`), o serviço `IUsuarioAplicacaoService` garante que:
1. O usuário (KeycloakId) existe no banco. Se não, ele é criado.
2. Nome e E-mail são atualizados automaticamente a partir das claims do JWT.
3. Se for o primeiro usuário a acessar a base vazia, ele receberá automaticamente o perfil de **Administrador**.

## Validação e Proteção de Endpoints

Foi criado o atributo `[RequerRecurso("codigo.recurso")]`, que atua como um `IAsyncAuthorizationFilter`. Ele verifica se o usuário autenticado possui aquele recurso na base de dados (através de algum de seus perfis).

Exemplo de uso:
```csharp
[HttpPost("enviar")]
[RequerRecurso("chat.acessar")]
public async Task<IActionResult> EnviarMensagem(...) { ... }
```

### Endpoints Protegidos nesta Etapa

- `POST /api/chat/enviar` exige `chat.acessar`
- `GET /api/chat/historico` exige `conversas.visualizar`
- `GET /api/acesso/recursos` exige `usuarios.visualizar`
- `GET /api/acesso/perfis` exige `usuarios.visualizar`

## Limitações da Etapa Atual

- Não implementamos tela Flutter de usuários/perfis.
- Não há CRUD completo (POST/PUT/DELETE) para usuários internamente ainda.
- Não estamos chamando a Keycloak Admin API para criar contas de usuário no Identity Provider.
- O cadastro de usuário visual ficará para uma fase posterior.

## Próximo Passo Recomendado

Implementar as rotas de CRUD (POST/PUT/DELETE) de Usuários no backend e posteriormente integrar com a tela Flutter (telas de gestão de acesso).
