CREATE PROC SearchPerson
	@SearchKey CHAR(10)
AS
BEGIN 
	SELECT * FROM PERSON WHERE NATIONALCODE = @SearchKey
END;