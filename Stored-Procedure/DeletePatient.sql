CREATE PROC DeletePatient
	@Id INT
AS
BEGIN
	DELETE FROM PATIENT WHERE ID = @Id
END;