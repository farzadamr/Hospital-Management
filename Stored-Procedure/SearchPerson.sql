USE [HospitalDB]
GO
/****** Object:  StoredProcedure [dbo].[SearchPerson]    Script Date: 12/29/2024 5:31:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[SearchPerson]
	@SearchKey VARCHAR(10)
AS
BEGIN 
	SELECT * FROM PERSON WHERE NATIONALCODE LIKE '%' + @SearchKey + '%'
END;