# 01 — Visão Geral do Projeto Bravito

## Objetivo

O Projeto Bravito será uma aplicação corporativa multiplataforma, com suporte para mobile e web, desenvolvida inicialmente em Flutter e integrada a uma API backend segura.

O sistema será uma extensão futura de um ERP, portanto deve nascer com arquitetura robusta, governança, controle de acesso, autenticação centralizada e separação clara de responsabilidades.

## Conceito

Bravito é uma IA parceira corporativa para ajudar empresas a vender mais, operar melhor, consultar informações com segurança e automatizar processos internos.

Slogan de referência:

> Sua IA parceira para vender mais e melhor.

## Escopo da fase 1

A fase 1 terá somente:

1. Tela de login.
2. Tela de chat.
3. Comunicação do Flutter com API backend.
4. Comunicação da API backend com n8n.
5. Autenticação com Keycloak.
6. Persistência em PostgreSQL.
7. Auditoria básica.
8. Logs e health checks.

## Objetivo técnico da fase 1

Ao final da fase 1, o projeto deve permitir:

- Usuário acessar a aplicação com autenticação segura.
- Usuário entrar em uma tela de chat.
- Usuário enviar mensagem ao assistente.
- Backend validar autenticação e permissões.
- Backend chamar o n8n com segurança.
- Backend registrar mensagens e auditoria.
- Flutter exibir a resposta ao usuário.
- Estrutura estar preparada para crescimento futuro.

## Princípios do projeto

- Segurança em primeiro lugar.
- Nenhum segredo no frontend.
- Nenhuma regra sensível no frontend.
- Nenhum acesso direto do app ao banco.
- Nenhum acesso direto do app ao webhook do n8n.
- Backend sempre como camada intermediária.
- Autenticação centralizada com Keycloak.
- Controle de acesso validado no backend.
- Estrutura preparada para multiempresa.
- Auditoria desde a primeira fase.
- Código limpo, organizado e testável.
- Separação clara entre domínio, aplicação, infraestrutura e interface.
