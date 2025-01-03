USE [HospitalDB]
GO
/****** Object:  StoredProcedure [dbo].[InsertDoctor]    Script Date: 12/30/2024 1:57:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[InsertDoctor]
	@M_S_N INT,
	@RESUME_ NVARCHAR(MAX),
	@DEPARTMENTID INT,
	@RATE INT,
	@PersonId NVARCHAR(50)
AS
BEGIN
	INSERT INTO DOCTOR(M_S_N,RATE,RESUME_,DEPARTMENTID,PersonId)
	OUTPUT inserted.M_S_N
	VALUES(@M_S_N,@RATE,@RESUME_,@DEPARTMENTID,@PersonId)
END;