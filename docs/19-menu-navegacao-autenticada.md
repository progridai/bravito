# 19 - Menu de Navegação Autenticada

## 🎯 Objetivo
Criar uma tela inicial segura para os usuários após o login bem-sucedido. Esta tela serve como um hub de navegação para as funcionalidades do sistema, atualmente direcionando para o chat existente e para as novas telas (placeholders) de menu.

## 🛠️ O que foi feito
- Criada a `HomePage` (`lib/features/home/presentation/pages/home_page.dart`) que atua como dashboard pós-login.
- Criada a `MenuPage` (`lib/features/menu/presentation/pages/menu_page.dart`) para opções adicionais como configurações de usuário.
- Criadas as telas de placeholder para "Alterar Senha" e "Visualizar Conversas".
- Atualizada a configuração do `GoRouter` (`lib/app/router.dart`) para redirecionar usuários autenticados para `/home` ao invés de `/chat` diretamente.
- O padrão visual corporativo do Bravito (identidade de cores e componentes) foi preservado e aplicado rigorosamente nestas novas telas.

## 🛣️ Rotas e Fluxo
- **Fluxo após login**: `Login -> /home`.
- `/home`: Exibe saudação e dois botões ("Abrir Chat" e "Abrir Menu").
- `/chat`: Tela de chat existente.
- `/menu`: Nova tela com opções.
- `/menu/alterar-senha`: Placeholder visual para troca de senha.
- `/menu/conversas`: Placeholder visual para histórico de conversas.

Todas essas rotas são protegidas e o redirecionamento ao `/login` ocorrerá se o usuário perder a sessão.

## ⚠️ Limitações / O que NÃO foi implementado
- **Alteração de Senha**: É apenas um layout visual sem integração com o Keycloak.
- **Histórico de Conversas**: É apenas um layout de estado vazio. Não consome endpoint nem interage com o banco de dados.
- **Backend/Banco de Dados/N8N**: Nenhuma mudança foi feita nessas áreas. Não há novas migrations e nem novos controllers na API.

## ⏭️ Próximo Passo Recomendado
- Integrar a funcionalidade de "Alterar Senha" conectando-a à API do Backend (que por sua vez atualizará o Keycloak).
- Desenvolver a listagem real de histórico de conversas a partir dos dados gravados no banco PostgreSQL (via API Backend).
