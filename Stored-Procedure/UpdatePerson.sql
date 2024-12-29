CREATE PROC UpdatePerson
	@NationalCode CHAR(10),
	@FirstName CHAR(50),
	@LastName CHAR(50),
	@Password NVARCHAR(20)
AS
BEGIN
	UPDATE PERSON
	SET
	FIRSTNAME = @FirstName,
	LASTNAME = @LastName,
	PASSWORD = @Password
	WHERE NATIONALCODE = @NationalCode
END;