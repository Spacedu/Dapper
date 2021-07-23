CREATE PROCEDURE [dbo].[GetUsuarioNome]
	@Id int
AS
BEGIN
	SELECT [Id], [Nome] FROM [dbo].[Usuarios] WHERE Id = @Id;
END