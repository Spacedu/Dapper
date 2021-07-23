/*Many-To-Many*/
CREATE TABLE [dbo].[Departamentos] (
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Nome] VARCHAR(100) NOT NULL,
	CONSTRAINT [PK_Departamentos] PRIMARY KEY CLUSTERED ([Id] ASC),
);

CREATE TABLE [dbo].[UsuariosDepartamentos] (
	[Id] INT IDENTITY(1,1) NOT NULL,
	[UsuarioId] INT NOT NULL,
	[DepartamentoId] INT NOT NULL,

	CONSTRAINT [PK_UsuariosDepartamentos] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_UsuariosDepartamentos_Usuarios] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuarios] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_UsuariosDepartamentos_Departamentos] FOREIGN KEY ([DepartamentoId]) REFERENCES [dbo].[Departamentos] ([Id]) ON DELETE CASCADE
);