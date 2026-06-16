# 05 — Guia de Implementação: Controle de Acesso e Menus

Este documento detalha o padrão técnico arquitetural adotado no Bravito para restringir ou liberar acessos a novos módulos (menus, telas e endpoints da API) baseando-se em Perfis de Acesso e Recursos.

---

## 🏗️ Conceitos Básicos

O sistema de permissões do Bravito foi construído com uma granularidade fina utilizando a relação entre **Perfis de Acesso** e **Recursos**:

1. **Recurso**: É a menor unidade de permissão. Representa uma ação ou o acesso a uma tela/módulo específico. Exemplo: `base_conhecimento.acessar`, `usuarios.cadastrar`.
2. **Perfil de Acesso (Role)**: É um grupo ou "cargo" atribuído ao usuário (ex: *Administrador*, *Operador*, *Somente Chat*). Cada perfil contém uma lista de Recursos permitidos.

Quando você precisa criar um **novo menu** ou proteger uma **nova funcionalidade** (como a "Base de Conhecimento"), você deve criar um Recurso novo e associá-lo aos Perfis que terão direito a ele.

> [!WARNING]
> **Atenção (Perfis vs. Recursos na Interface):**
> A tela de edição de usuários do aplicativo (onde existem os checkboxes) lista exclusivamente os **Perfis de Acesso** (Cargos). 
> Criar um novo **Recurso** (como `base_conhecimento.acessar`) no banco de dados e atrelá-lo ao perfil de "Administrador" **NÃO** fará com que um novo checkbox apareça na interface de usuários. Qualquer usuário que tiver o checkbox de "Administrador" marcado já herdará o novo recurso automaticamente. 
> *Se a sua regra de negócio exigir um checkbox separado apenas para esse módulo, você deverá criar um novo **Perfil de Acesso** (ex: "Gestor de Conhecimento") em vez de apenas adicionar o recurso a um perfil existente.*

---

## 🛠️ Passo a Passo para Proteger um Novo Módulo

Abaixo está o fluxo de ponta a ponta que você deve executar sempre que adicionar um novo módulo que exija controle de acesso.

### Passo 1: Criar o Recurso no Banco de Dados
A API e o Frontend carregam a lista de módulos dinamicamente do banco de dados (tabela `Recursos`).
Você precisa inserir um novo registro no PostgreSQL. Isso deve ser feito via Migration ou inserção direta no banco durante a criação da feature.

**Exemplo de inserção de um Recurso:**
- `Id`: `(UUID gerado automaticamente)`
- `Codigo`: `"base_conhecimento.acessar"` *(Use um padrão claro, separando módulo e ação por ponto)*
- `Nome`: `"Base de Conhecimento"`
- `Descricao`: `"Acesso ao menu e visualização da Base de Conhecimento"`
- `Ativo`: `true`

### Passo 2: Vincular o Recurso aos Perfis Desejados
Na tabela associativa `PerfilRecurso`, você deve relacionar o `Id` do recurso criado no Passo 1 com o `Id` do Perfil de Acesso desejado (ex: o UUID do perfil "Administrador").

> **Nota:** Quando a tela administrativa de controle de perfis estiver totalmente implementada, esse passo poderá ser feito clicando em caixas de seleção na interface. Por enquanto (Fase 1), esse vínculo de novos recursos ocorre via banco de dados/seeders.

---

### Passo 3: Proteger as Rotas da API Backend (C#)
No código ASP.NET Core, não confie apenas no bloqueio visual do frontend. Qualquer tentativa de consumir a API da Base de Conhecimento precisa ser validada.

No controlador correspondente (`KnowledgeDocumentsController.cs`), adicione a anotação técnica `[RequerRecurso]`. O filtro personalizado (`RequerRecursoAttribute`) se encarregará de interceptar a requisição, ler o Token JWT e verificar no banco se o usuário logado possui aquele recurso.

**Exemplo de Aplicação:**

```csharp
using Bravito.Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bravito.Api.Controllers
{
    [ApiController]
    [Route("api/knowledge")]
    [Authorize] // Garante que apenas usuários logados (autenticados no Keycloak) chamem a API
    [RequerRecurso("base_conhecimento.acessar")] // Bloqueia quem não tem a permissão
    public class KnowledgeDocumentsController : ControllerBase
    {
        // Seus endpoints (Get, Post, Delete)...
    }
}
```
*Dica: Você pode aplicar o `[RequerRecurso]` em cima da declaração da classe (protegendo todos os métodos) ou em métodos específicos (ex: criar um recurso apenas para deleção `base_conhecimento.deletar`).*

---

### Passo 4: Ocultar o Menu no Frontend (Flutter)
No aplicativo Flutter, a usabilidade exige que um usuário sem acesso sequer veja o botão do menu restrito (ou o veja com um ícone de cadeado).

1. Durante o login e carregamento inicial (geralmente via endpoint de `me` ou listagem de recursos do `AcessoController`), o frontend carrega e salva no estado (Riverpod) uma lista de `strings` com os códigos de recursos que o usuário tem.
2. Na hora de renderizar a barra lateral de menus, insira uma condicional verificando se a string `"base_conhecimento.acessar"` existe nessa lista armazenada no estado local do usuário.

**Lógica esperada no Frontend:**
- Se `recursosDoUsuario.contains('base_conhecimento.acessar')` -> Renderiza o botão e permite navegação (`context.go('/knowledge')`).
- Caso contrário -> Esconde o botão do menu.

---

## 🔍 Como o Sistema Sabe Disso por Baixo dos Panos?

- **Frontend (`usuario_form_page.dart`)**: O app exibe os "Perfis de Acesso" usando os dados retornados pelos endpoints de listagem do backend.
- **Backend (`AcessoController.cs`)**: Fornece os endpoints `/api/acesso/recursos` e `/api/acesso/perfis` onde as listas de permissões e as hierarquias são buscadas do banco.
- **Autorização (`RequerRecursoAttribute.cs`)**: Middleware customizado que injeta o filtro responsável por negar com o código HTTP `403 Forbidden` se o usuário tentar dar um "bypass" pelo Postman sem ter o recurso atrelado ao seu perfil.
