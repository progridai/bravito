# 15 - Estruturação Base do Frontend (Flutter)

Este documento descreve a estrutura inicial construída para o frontend do projeto **Bravito**, desenvolvido em Flutter. O objetivo principal desta etapa foi preparar uma fundação modularizada, aderente ao Design System, e estruturada para integrações futuras, mas **sem** implementação real de lógica de negócio no momento.

## 📁 Estrutura Criada

A árvore de diretórios em `frontend/bravito_app/lib` segue a padronização orientada a features (*Feature-First*):

- `lib/app/`: Configurações de inicialização, roteamento central e temas.
- `lib/core/`: Elementos fundamentais do sistema.
  - `config/`, `constants/`, `errors/`, `http/`, `security/`, `storage/`, `utils/`.
- `lib/features/`: Módulos da aplicação.
  - `auth/`: Contém a interface inicial de Login (placeholder).
  - `chat/`: Contém a interface do Chat (placeholder).
- `lib/shared/`: Componentes reutilizáveis em todo o app (Widgets, Layouts, Extensions).

## 📦 Dependências Adicionadas

Foram configurados os pacotes essenciais para as próximas fases:
- `flutter_riverpod`: Para gerenciamento de estado previsível.
- `go_router`: Para roteamento avançado e declarativo.
- `dio`: Para requisições HTTP seguras com o backend (futuramente).
- `flutter_secure_storage`: Para armazenamento seguro local (tokens, etc.).

## 🎨 Tema Visual e Cores Oficiais

O arquivo de tema (`lib/app/theme.dart`) e os tokens de design (`lib/core/constants/`) foram criados centralizando as cores oficiais do Bravito:

- **Azul Principal**: `#1E3A8A` (usado em botões principais, app bars, e títulos).
- **Azul Secundário**: `#2563EB` (destaques, bordas ativas).
- **Dourado**: `#D4AF37` (elementos de destaque - terciário).
- **Cinza Claro / Fundo**: `#F2F4F7` (fundo da aplicação).
- **Cinza Escuro / Texto**: `#334155` (textos gerais).

Constantes adicionais de espaçamento (`AppSpacing`), raio de borda (`AppRadius`), e tipografia (`AppTextStyles`) foram implementadas para garantir reusabilidade e coesão visual, evitando o uso de valores fixos espalhados pelo código.

## 🛣️ Roteamento Inicial

O roteamento base foi estruturado usando `go_router` no arquivo `lib/app/router.dart`.
Rotas atuais:
- `/login`: Direciona para a tela visual de entrada. Definida como a rota inicial da aplicação.
- `/chat`: Direciona para a interface do chat simulado.

## 🖥️ Telas e Componentes (Placeholders)

### Componentes Reutilizáveis
Foram construídos componentes globais em `lib/shared/widgets/`:
- `BravitoPrimaryButton`: Botão padrão do sistema (com suporte a estado de loading).
- `BravitoTextField`: Campo de texto estilizado, usado para inputs como usuário e senha.
- `BravitoCard`: Card base com sombra suave, alinhado à identidade limpa e moderna.
- `BravitoAppScaffold`: Estrutura de página (Scaffold) padrão para consistência.

### Telas
- **LoginPage** (`/login`): Uma interface limpa, corporativa, focada na conversão. Possui logo/mascote provisório (ícone), título "Bravito", e slogan "Sua IA parceira para vender mais e melhor." Campos de usuário e senha apenas ilustrativos.
- **ChatPage** (`/chat`): Interface que contém área de entrada de mensagem e um aviso no centro de que a integração (n8n/backend) será realizada no futuro.

## ⛔ O Que NÃO Foi Implementado (Deliberadamente)
Conforme requisitos restritos desta etapa:
- Nenhum fluxo OAuth / OIDC / PKCE foi ativado.
- Não há comunicação real com o Keycloak.
- Nenhuma chamada de rede foi efetuada para o Backend ASP.NET Core.
- Banco de Dados ou n8n não foram alterados ou acessados.
- Não foram salvas ou lidas informações sensíveis (tokens) localmente.
- O botão "Entrar" na tela de login apenas faz uma navegação visual simples para `/chat`.

## ⏭️ Próximo Passo Recomendado
Com a base visual do Flutter solidificada, o próximo passo lógico deve ser a **Implementação do Fluxo de Autenticação PKCE** (Authorization Code Flow) no Flutter, integrando de fato com o Keycloak e guardando o token no `flutter_secure_storage`.
