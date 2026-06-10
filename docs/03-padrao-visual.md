# 03 — Padrão Visual do Bravito

## Identidade visual

A interface da aplicação segue rigorosamente o Manual de Identidade Visual oficial da marca **Bravida** e seu mascote, **Bravito**.

Bravito é a coruja da inteligência artificial parceira da Bravida, criada para representar nossos valores e propósito no mercado de seguros de vida e previdência.
A interface deve transmitir os conceitos oficiais:
- Inteligência.
- Orientação.
- Proteção.
- Confiança.
- Performance comercial.
- Suporte técnico.
- Vida e cuidado.
- Evolução e resultados.
- Tecnologia.

**Personalidade da marca e do mascote**: Amigável, Inteligente, Confiável, Protetora, Focada em resultados, Técnica, Clara, Segura.
Bravito deve ser percebido como um guia amigável, técnico e confiável em toda a experiência digital da Bravida.

## Cores oficiais

```text
Dourado: #D4AF37
Branco: #FFFFFF
Preto suave: #1A1A1A
Cinza claro: #F6F6F6
```

### Diretrizes de uso das cores

- **Dourado (#D4AF37)**: Usar como cor de destaque e ação principal. É o foco de atenção (botões principais, detalhes premium, pequenos destaques). Não deve ser usado como fundo dominante de grandes superfícies. Preservar sempre a cor original, sem alterações ou distorções de tom.
- **Branco (#FFFFFF)**: Fundo de cards, painéis principais, modais, etc. Proporciona área de respiro e limpeza visual.
- **Preto suave (#1A1A1A)**: Usar para textos principais, títulos e ícones de alta importância. Proporciona leitura confortável.
- **Cinza claro (#F6F6F6)**: Fundo principal das telas (Scaffold), áreas neutras, e fundos de separação visual.

Evitar o uso de cores fora dessa paleta (exceto cores funcionais de erro/sucesso suaves). Cores antigas provisórias (Azul #1E3A8A / #2563EB) **foram descontinuadas**.

## Tipografia recomendada

A tipografia é baseada em legibilidade clara e hierarquia forte.

- **Títulos**: Montserrat Semibold
- **Interface/Textos**: Inter Regular e Inter Medium

Se estas fontes não puderem ser carregadas, o sistema usará a fonte do sistema corporativa padrão (fallback seguro), sem quebrar o layout.

## O Mascote Bravito

As versões do Bravito seguem assinaturas oficiais para a aplicação:

1. **Bravito em pé**: Telas principais, apresentações, boas-vindas.
2. **Bravito esférico**: Momentos de inovação, transições, carregamento (loading), assistente.
3. **Ícone institucional (com escudo)**: Ícones de aplicativo, atalhos, segurança.

### Aplicações no App
- **Onboarding/Boas-vindas**: Recepcionar usuários de forma amigável.
- **Ajuda contextual**: Suporte rápido e claro.
- **Carregamento/Assistente**: Reforçar a presença da IA durante processamentos.
- **Aprovação/Sucesso**: Reforçar a confiança na conclusão de ações.

### O que Fazer
- Usar fundo claro nas aplicações do mascote.
- Preservar o dourado original e contornos nítidos.
- Usar cards claros com sombras leves, cantos arredondados e manter o visual corporativo, seguro e amigável.
- Usar contraste limpo.

### O que Evitar
- Trocar a cor do dourado.
- Distorcer proporções do mascote.
- Usar fundo poluído que prejudique a leitura.
- Remover o símbolo 'B' do peito/escudo do mascote.
- Espalhar cores hardcoded no código; tudo deve estar via `ThemeData`.
- Usar as imagens completas do manual (anexos inteiros) como assets da interface. O manual é referência; os assets do mascote devem ser recortes individuais.

## Referência Visual

Os anexos do Manual de Identidade Visual oficial servem como a única fonte de verdade para a aplicação da marca. Todos os componentes devem ser validados em comparação com os conceitos descritos lá.
Tamanhos mínimos: Avatar/app icon (64px), Mascote em interface (120px), Slide/vídeo (220px).