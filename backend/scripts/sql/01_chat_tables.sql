-- Habilita extensão pgcrypto para gerar UUID nativo, caso não exista no DB
CREATE EXTENSION IF NOT EXISTS pgcrypto;

-- 1. Tabela de Conversas
CREATE TABLE conversas (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    usuario_id VARCHAR(100) NOT NULL,
    identificador_externo VARCHAR(100) NULL,
    canal_origem VARCHAR(50) NOT NULL DEFAULT 'api',
    status VARCHAR(50) NOT NULL DEFAULT 'aberta',
    data_criacao TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    data_ultima_interacao TIMESTAMP WITH TIME ZONE NULL,
    metadados JSONB NULL
);

CREATE INDEX idx_conversas_usuario_id ON conversas(usuario_id);
CREATE INDEX idx_conversas_status ON conversas(status);

-- 2. Tabela de Mensagens da Conversa
CREATE TABLE conversas_mensagens (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    conversa_id UUID NOT NULL,
    tipo_remetente VARCHAR(50) NOT NULL, -- usuario, assistente, sistema, ferramenta
    conteudo TEXT NOT NULL,
    conteudo_bruto JSONB NULL,
    tokens_entrada INT NULL,
    tokens_saida INT NULL,
    modelo_usado VARCHAR(100) NULL,
    data_criacao TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    status VARCHAR(50) NOT NULL DEFAULT 'enviada',
    
    CONSTRAINT fk_conversa_mensagem FOREIGN KEY (conversa_id) 
        REFERENCES conversas (id) ON DELETE CASCADE
);

CREATE INDEX idx_conversas_mensagens_conversa_id ON conversas_mensagens(conversa_id);

-- 3. Tabela de Contexto da Conversa (Memória Resumida)
CREATE TABLE conversas_contextos (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    conversa_id UUID NOT NULL UNIQUE,
    resumo_atual TEXT NOT NULL,
    dados_auxiliares JSONB NULL,
    data_atualizacao TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    
    CONSTRAINT fk_conversa_contexto FOREIGN KEY (conversa_id) 
        REFERENCES conversas (id) ON DELETE CASCADE
);

-- 4. Tabela de Eventos (Auditoria Técnica)
CREATE TABLE conversas_eventos (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    conversa_id UUID NOT NULL,
    tipo_evento VARCHAR(100) NOT NULL, -- webhook_recebido, llm_chamada, erro
    detalhes JSONB NULL,
    data_criacao TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    
    CONSTRAINT fk_conversa_evento FOREIGN KEY (conversa_id) 
        REFERENCES conversas (id) ON DELETE CASCADE
);

CREATE INDEX idx_conversas_eventos_conversa_id ON conversas_eventos(conversa_id);
