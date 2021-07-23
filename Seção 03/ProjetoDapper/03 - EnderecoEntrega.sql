/*One-To-Many*/
CREATE TABLE [dbo].[EnderecosEntrega] (
	[Id] INT IDENTITY(1,1) NOT NULL,
	[UsuarioId] INT NOT NULL,
	[NomeEndereco] VARCHAR(100) NOT NULL,
	[CEP] VARCHAR(10) NOT NULL,
	[Estado] CHAR(2) NOT NULL,
	[Cidade] VARCHAR(120) NOT NULL,
	[Bairro] VARCHAR(200) NOT NULL,
	[Endereco] VARCHAR(200) NOT NULL,
	[Numero] VARCHAR(20) NULL,
	[Complemento] VARCHAR(30) NULL,
	
	CONSTRAINT [PK_EnderecosEntrega] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_EnderecosEntrega_Usuarios] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuarios] ([Id]) ON DELETE CASCADE

);