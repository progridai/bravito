# 25 — Identidade Visual Oficial: Bravito e Bravida

Este documento sumariza a transição e a implementação final da identidade visual oficial para o sistema Bravito, conforme o Manual da Marca.

## Resumo do Manual Oficial
O mascote oficial é o **Bravito**, a coruja da inteligência artificial parceira da **Bravida**. Representa inteligência, orientação, proteção, confiança, performance comercial e suporte técnico focado em seguros de vida e previdência.
Bravito é uma presença "amigável, técnica e confiável".

## Transição: Provisória x Oficial
Até o momento, o sistema utilizava uma identidade visual provisória e genérica com tons de azul (Azul Principal #1E3A8A, Azul Secundário #2563EB).
**Esta paleta foi descontinuada**. 
O design system do Flutter e toda a aplicação devem seguir exclusivamente a **Paleta Oficial**.

## Paleta Oficial
- **Dourado:** `#D4AF37`
- **Branco:** `#FFFFFF`
- **Preto Suave:** `#1A1A1A`
- **Cinza Claro:** `#F6F6F6`

*Cores funcionais secundárias (sucesso/erro/info) devem ser utilizadas de forma muito sutil, garantindo que não concorram com o Dourado.*

## Tipografia
- **Títulos:** Montserrat Semibold
- **Textos de Interface e Corpo:** Inter Regular / Inter Medium
*(O Flutter deverá usar a fonte nativa do sistema via ThemeData se Montserrat/Inter não estiverem presentes nos assets locais, sem interromper o funcionamento do app).*

## Uso do Mascote Bravito
Existem 3 formatos oficiais:
1. **Em pé:** Boas-vindas, onboarding, mensagens institucionais amplas.
2. **Esférico:** Telas de loading, processamento, interações do assistente n8n, estados de espera.
3. **Com escudo:** App icon, atalhos de sistema, contextos ligados diretamente à segurança/privacidade.

### Aplicações em Telas do App
- **LoginPage:** Minimalista, fundo claro, com card branco e logo/mascote claro. Botão primário Dourado.
- **HomePage/MenuPage:** Cards brancos com bordas sutis e sombra leve. Fundo cinza claro (`#F6F6F6`).
- **ChatPage:** Uso do Bravito Esférico para loading de mensagens. Fundo branco ou cinza claro, texto preto suave.

### Regras para Componentes e Novas Telas
- **Cards e Containers:** Fundo Branco, bordas arredondadas e sombras suaves, com separação visível contra o fundo Cinza Claro.
- **Botões de Ação:** O Dourado deve ser o ponto focal.
- **Textos e Ícones:** Preto Suave para ótima legibilidade.

### Cuidados Importantes e O Que Evitar
- Nunca trocar ou obscurecer o dourado (`#D4AF37`).
- Nunca usar cores hardcoded fora da classe do tema no Flutter (ex: `BravitoColors`).
- Nunca distorcer o mascote, e nunca usar versões alternativas (cores, adereços ou poses inventadas) não previstas no manual.
- Não misturar elementos provisórios (azuis antigos) com os novos.
- **As imagens nos anexos do manual não devem ser importadas inteiras como UI.** Devem ser recortadas posteriormente caso os assets oficiais transparentes individuais não estejam prontos no momento.

## Próximos Passos Recomendados
1. Extrair os assets individuais do mascote (Bravito em pé, Esférico, etc.) do arquivo fonte e adicionar à pasta `assets/images/bravito/` do Flutter.
2. Configurar o download ou a injeção local das fontes Montserrat e Inter no `pubspec.yaml`, caso desejem garantir 100% de consistência em todas as plataformas.
3. Revisar micro-interações do Flutter para adicionar sutileza (efeitos de hover/splash do Material 3) e confirmar alinhamento corporativo da nova UX.
