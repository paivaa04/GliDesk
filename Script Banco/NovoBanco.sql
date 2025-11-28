/* --- schema: SistemaChamados --- */

/* --- Tabelas auxiliares --- */
CREATE TABLE Prioridades (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(50) NOT NULL,
    Nivel INT NOT NULL, -- 1 Baixa, 2 Média, 3 Alta, 4 Critica
    CONSTRAINT UQ_Prioridades_Nome UNIQUE (Nome)
);

CREATE TABLE StatusChamado (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(50) NOT NULL,
    Ordem INT NOT NULL,
    CONSTRAINT UQ_StatusChamado_Nome UNIQUE (Nome)
);

CREATE TABLE Setores (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Descricao VARCHAR(250) NULL,
    CONSTRAINT UQ_Setores_Nome UNIQUE (Nome)
);

CREATE TABLE Categorias (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    SetorId INT NOT NULL,
    CONSTRAINT UQ_Categorias_Setor_Nome UNIQUE (SetorId, Nome),
    FOREIGN KEY (SetorId) REFERENCES Setores (Id)
);

-- Usuarios (senha armazenada como hash)
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    UserCode VARCHAR(50) NOT NULL, -- código funcional
    CPF VARCHAR(14) NULL,
    Email VARCHAR(256) NOT NULL,
    PasswordHash VARCHAR(512) NOT NULL,
    Telefone VARCHAR(20) NULL,
    TipoUsuario INT NOT NULL DEFAULT(1), -- 1=Usuario,2=Atendente,3=Supervisor,4=Admin
    IsActive BIT NOT NULL DEFAULT(1),
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NULL,
    CONSTRAINT UQ_Usuarios_Email UNIQUE (Email),
    CONSTRAINT UQ_Usuarios_UserCode UNIQUE (UserCode)
);

-- Chamados
CREATE TABLE Chamados (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    SetorId INT NOT NULL,
    CategoriaId INT NULL,
    Titulo VARCHAR(200) NOT NULL,
    Descricao VARCHAR(MAX) NOT NULL,
    PrioridadeId INT NOT NULL,
    StatusId INT NOT NULL,
    DataAbertura DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    DataFechamento DATETIME2 NULL,
    IsDeleted BIT NOT NULL DEFAULT(0),
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NULL,
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios (Id),
    FOREIGN KEY (SetorId) REFERENCES Setores (Id),
    FOREIGN KEY (CategoriaId) REFERENCES Categorias (Id),
    FOREIGN KEY (PrioridadeId) REFERENCES Prioridades (Id),
    FOREIGN KEY (StatusId) REFERENCES StatusChamado (Id)
);

-- Histórico de mudanças (status / comentários)
CREATE TABLE ChamadoHistorico (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ChamadoId INT NOT NULL,
    UsuarioId INT NULL,
    OldStatusId INT NULL,
    NewStatusId INT NULL,
    Comentario VARCHAR(MAX) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    FOREIGN KEY (ChamadoId) REFERENCES Chamados (Id),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios (Id),
    FOREIGN KEY (OldStatusId) REFERENCES StatusChamado (Id),
    FOREIGN KEY (NewStatusId) REFERENCES StatusChamado (Id)
);

-- Anexos
CREATE TABLE Anexos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ChamadoId INT NOT NULL,
    FileName VARCHAR(260) NOT NULL,
    FileUrl VARCHAR(1024) NOT NULL, -- se usar Blob Storage / Azure Storage
    ContentType VARCHAR(100) NULL,
    SizeBytes BIGINT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    FOREIGN KEY (ChamadoId) REFERENCES Chamados (Id)
);

-- Tokens revogados (refresh tokens)
CREATE TABLE TokensRevogados (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    TokenId VARCHAR(200) NOT NULL,
    RevokedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios (Id)
);

-- Índices importantes
CREATE INDEX IX_Usuarios_Email ON Usuarios (Email);
CREATE INDEX IX_Chamados_UsuarioId ON Chamados (UsuarioId);
CREATE INDEX IX_Chamados_StatusId ON Chamados (StatusId);
CREATE INDEX IX_Chamados_PrioridadeId ON Chamados (PrioridadeId);

-- Seeds básicos para Prioridade e Status
INSERT INTO Prioridades (Nome, Nivel) VALUES
('Baixa', 1), ('Média', 2), ('Alta', 3), ('Crítica', 4);

INSERT INTO StatusChamado (Nome, Ordem) VALUES
('Em Aberto', 1), ('Em Atendimento', 2), ('Aguardando Cliente', 3), ('Resolvido', 4), ('Fechado', 5);
