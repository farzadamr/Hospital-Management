 CREATE PROC InsertPrescription
	@DOCTORID INT,
	@DESCRIPTION NVARCHAR(MAX),
	@PATIENTID INT
AS
BEGIN
	INSERT INTO PRESCRIOTION(DOCTORID,DESCRIPTION,PATIENTID)
	OUTPUT inserted.ID
	VALUES(@DOCTORID,@DESCRIPTION,@PATIENTID)
END;