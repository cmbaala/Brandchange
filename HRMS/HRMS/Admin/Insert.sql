USE [HRMS]
GO

/****** Object:  StoredProcedure [dbo].[sp_updateEmpdetails]    Script Date: 08/05/2013 22:53:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Kaspon Bala
-- Create date: Aug 5 2013
-- Description:	Updating employee details
-- =============================================
CREATE PROCEDURE [dbo].[sp_updateEmpdetails] 
	-- Add the parameters for the stored procedure here
	@emp_id varchar(50),@Firstname varchar(50),@Lastname varchar(50), @Email varchar(50), @Location varchar(50), @Pan_Number varchar(50), @Designation varchar(50), @Doj varchar(50), @Gender bit, @Bank_Name varchar(50), @Account_Number varchar(50), @Active bit, @isApplicableForExtraDeduction bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

Update Tbl_Employee_Details set [First Name]=@FirstName,[Last Name]=@LastName , Email=@Email, Location=@Location, Pan_Number =@Pan_Number, Designation=@Designation, Doj=@Doj, Gender=@Gender, Bank_Name=@Bank_Name, Account_Number=@Account_Number, Active=@Active, isApplicableForExtraDeduction=@isApplicableForExtraDeduction OUTPUT Inserted.Emp_Id where Emp_Id=@emp_id;

    
END

GO

