# 05 — Frontend Flutter

## Objetivo

O frontend será desenvolvido em Flutter para suportar mobile e web a partir de uma base de código única.

## Stack

Usar:

- Flutter.
- Dart.
- Riverpod.
- GoRouter.
- Dio.
- Flutter Secure Storage.
- Material 3.

## Estrutura inicial

```text
frontend/
  bravito_app/
    lib/
      main.dart

      app/
        app.dart
        router.dart
        theme.dart

      core/
        config/
        constants/
        errors/
        http/
        security/
        storage/
        utils/

      features/
        auth/
          data/
            datasources/
            models/
            repositories/
          domain/
            entities/
            repositories/
            usecases/
          presentation/
            controllers/
            pages/
            widgets/

        chat/
          data/
            datasources/
            models/
            repositories/
          domain/
            entities/
            repositories/
            usecases/
          presentation/
            controllers/
            pages/
            widgets/

      shared/
        widgets/
        layouts/
        extensions/
Regras do frontend
Não colocar regra sensível no Flutter.
Não colocar webhook do n8n no Flutter.
Não colocar credenciais no Flutter.
Não acessar banco diretamente.
Usar API backend para tudo.
Usar tema centralizado.
Usar componentes reutilizáveis.
Separar UI, estado, domínio e dados.
Preparar para mobile e web.
Manter código organizado por feature.
Módulo Auth

Responsável por:

Tela de login.
Controle de sessão.
Armazenamento seguro de token.
Renovação de token.
Logout.
Redirecionamento por autenticação.
Módulo Chat

Responsável por:

Tela de conversa.
Envio de mensagens.
Exibição de respostas.
Estado de carregamento.
Histórico básico.
Tratamento de erro.
Tema

O tema deve usar as cores oficiais do Bravito:

#1E3A8A
#2563EB
#D4AF37
#F2F4F7
#334155

Não usar cores fixas diretamente nos widgets. Centralizar no arquivo de tema.