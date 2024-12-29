CREATE PROC InsertPerson
	@NationalCode INT,
	@FirstName CHAR(50),
	@LastName CHAR(50),
	@Password NVARCHAR(20)
AS
BEGIN
	INSERT INTO PERSON(NATIONALCODE,FIRSTNAME,LASTNAME,PASSWORD)
	VALUES(@NationalCode,@FirstName,@LastName,@Password)
END;
