CREATE PROC UpdateAppointment
	@ID INT,
	@DESCRIPTIONN NVARCHAR(MAX),
	@TIME_ DATETIME
AS
BEGIN
	UPDATE APPOINTMENT
	SET
	DESCRIPTIONN = @DESCRIPTIONN,
	TIME_ = @TIME_
	WHERE ID = @ID
END;