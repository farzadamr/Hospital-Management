CREATE PROC GetPatient
@ID INT
AS 
BEGIN
	SELECT * FROM PATIENT WHERE ID = @ID
END;