# Controle de Permissões e Recursos (Frontend Flutter)

## Objetivo
Centralizar a leitura dos recursos (`permissions`) fornecidos pela API Bravito (`/api/auth/me`) e condicionar a interface de usuário baseada nessas permissões.
Isso promove uma experiência visual polida, evitando que usuários sem permissão sequer vejam ou esbarrem em botões restritos, redirecionando-os adequadamente quando tentam forçar o acesso.

## Segurança Real x Controle Visual
- **A segurança real reside no Backend (ASP.NET Core)**: Se um usuário mal-intencionado fraudar o estado do Flutter e enviar uma requisição restrita, o backend barrará a ação com um HTTP `403 Forbidden`.
- **O controle visual no Flutter serve para UX**: Evita frustrações ao não exibir caminhos inúteis para perfis sem privilégios.

## Como as permissões são carregadas
1. O usuário faz o login (via Keycloak) e obtém o token.
2. O aplicativo chama o endpoint `/api/auth/me`.
3. O Backend retorna um JSON com dados do perfil e os recursos (ex: `["chat.acessar", "usuarios.visualizar"]`).
4. O Riverpod captura o retorno e salva na Entidade `UserEntity`, disponibilizando o método helper `possuiRecurso('string_do_recurso')`.

## Constantes Centralizadas
Todas as chaves de permissão da aplicação ficam listadas no arquivo `lib/core/security/recursos_app.dart`:
```dart
class RecursosApp {
  static const chatAcessar = 'chat.acessar';
  static const conversasVisualizar = 'conversas.visualizar';
  // ...
}
```

## Como o Menu e Rotas são protegidos

### MenuPage e HomePage
Usando o `authControllerProvider` (Riverpod), lemos a propriedade `.possuiRecurso(RecursosApp.chatAcessar)` e utilizamos instruções `if` no corpo da tela para incluir ou excluir os Cards (`ListTile`) e Botões da UI.

### GoRouter
Foi interceptado o fluxo da propriedade `redirect` na declaração do `GoRouter` no arquivo `router.dart`.
Se o usuário tenta ir para `/menu/usuarios` e ele não possui a string `usuarios.visualizar` nos recursos logados, o router automaticamente o redireciona para a tela isolada `/acesso-negado`.

## Tela de Acesso Negado
Localizada em `shared/pages/acesso_negado_page.dart`. Ela alerta visualmente o usuário sobre sua falta de autorização e entrega um botão de fallback seguro de volta à "HomePage".

## Próximo Passo Recomendado
O projeto encontra-se robusto e modularizado de ponta a ponta. O próximo passo lógico é a implementação do **Módulo de Perfis Globais**, permitindo criar e editar Perfis no Flutter para assim adicionar recursos (checkboxes dos `RecursosApp`) a esses novos Perfis.
