USE [master]
GO
/****** Object:  Table [dbo].[Chamados]    Script Date: 02/10/2025 16:59:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chamados](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[Email] [varchar](150) NOT NULL,
	[Setor] [varchar](100) NOT NULL,
	[Assunto] [varchar](200) NOT NULL,
	[Descricao] [varchar](max) NOT NULL,
	[DataAbertura] [datetime] NOT NULL,
	[Prioridade] [bit] NOT NULL,
	[Status] [varchar](30) NOT NULL,
 CONSTRAINT [PK__Chamados__3214EC072593DEF5] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 02/10/2025 16:59:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Senha] [varchar](50) NOT NULL,
	[TipoUsuario] [bit] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Chamados] ADD  CONSTRAINT [DF__Chamados__DataAb__693CA210]  DEFAULT (getdate()) FOR [DataAbertura]
GO
ALTER TABLE [dbo].[Chamados] ADD  CONSTRAINT [DF__Chamados__Priori__6A30C649]  DEFAULT ((0)) FOR [Prioridade]
GO
ALTER TABLE [dbo].[Chamados] ADD  DEFAULT ('Em Aberto') FOR [Status]
GO
ALTER TABLE [dbo].[Chamados]  WITH CHECK ADD  CONSTRAINT [FK_Chamados_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Chamados] CHECK CONSTRAINT [FK_Chamados_Usuarios]
GO

