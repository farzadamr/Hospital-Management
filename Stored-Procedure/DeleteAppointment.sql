CREATE PROC DeleteAppointment
	@ID INT
AS
BEGIN
	DELETE FROM APPOINTMENT WHERE ID = @ID
END;