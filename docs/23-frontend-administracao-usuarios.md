# Frontend de Administração de Usuários

## Objetivo
Criar a área administrativa no Flutter para a gestão de usuários da aplicação Bravito, consumindo de forma 100% segura os endpoints criados no backend sem armazenar ou trafegar regras essenciais no frontend. Todo o controle final de permissões (403, etc) fica sob a tutela da API.

## Endpoints Consumidos
A aplicação Flutter utiliza os endpoints abaixo através de uma instância Dio configurada com tokens `Bearer`:
- `GET /api/usuarios` - Lista os usuários.
- `GET /api/usuarios/{id}` - Obtém um usuário para o formulário.
- `GET /api/acesso/perfis` - Lista os perfis de acesso disponíveis.
- `POST /api/usuarios` - Cria um novo usuário no banco e no Keycloak (enviando senha temporária).
- `PUT /api/usuarios/{id}` - Edita as informações cadastrais e perfis de um usuário.
- `PATCH /api/usuarios/{id}/ativar` - Ativa a conta.
- `PATCH /api/usuarios/{id}/desativar` - Desativa a conta.

## Rotas Adicionadas
- `/menu/usuarios` (Lista)
- `/menu/usuarios/form` (Criação)
- `/menu/usuarios/form/:id` (Edição)

## Telas Criadas
- `UsuariosPage`: A listagem exibe a relação de usuários, seus status e os perfis, utilizando o `UsuarioCard`.
- `UsuarioFormPage`: A página unificada para criação e edição, incluindo validações (obrigatório e formato de email), campos para senha temporária na criação e exibição dos checkboxes com os perfis disponíveis.

## Controle Visual de Ações
A interface exibe/oculta dinamicamente componentes consultando os `recursos` trazidos no objeto do usuário logado via payload `/api/auth/me`:
- Botão "Usuários" no Menu: exige `usuarios.visualizar`
- Botão "Novo usuário" no topo: exige `usuarios.cadastrar`
- Ícone "Lápis" no Card: exige `usuarios.editar`
- Ícone "Bloquear/Ativar" no Card: exige `usuarios.desativar`

## Tratamento de Erro
Utilizando os interceptors do Dio, a classe lida com cenários como `403` (Sem Permissão), `409` (Email duplicado) e outros, exibindo um `SnackBar` vermelho e amigável, prevenindo stack traces na tela do usuário.

## Limitações Atuais
A listagem não possui paginação no momento, carregando a lista completa. Isso será perfeitamente aceitável até que a base comece a crescer para milhares de registros.

## Próximo Passo Recomendado
Criar testes unitários para o frontend desta feature ou então partir para a criação de "Cargos e Permissões Globais" (Gestão de Perfis de Acesso), permitindo a criação de grupos com recursos dinâmicos.
