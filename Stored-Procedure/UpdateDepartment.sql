CREATE PROC UpdateDepartment
	@ID INT,
	@TITLE NVARCHAR(50),
	@DESCRIPTION_ NVARCHAR(MAX)
AS
BEGIN
	UPDATE DEPARTMENT
	SET
	TITLE = @TITLE,
	DESCRIPTION_ = @DESCRIPTION_
	WHERE ID = @ID
END;