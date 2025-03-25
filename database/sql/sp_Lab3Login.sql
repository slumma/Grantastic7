CREATE PROCEDURE sp_Lab3Login

@Username AS NVARCHAR(200)

AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT Password FROM HashedCredentials
	WHERE Username = @Username;

END;
