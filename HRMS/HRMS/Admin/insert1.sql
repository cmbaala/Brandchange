USE [HRMS]
GO

/****** Object:  StoredProcedure [dbo].[sp_insert_employee_details]    Script Date: 08/05/2013 22:54:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Kaspon	
-- Create date: 06/06/2013
-- Description:	For Inserting Employee Details
-- =============================================
CREATE PROCEDURE [dbo].[sp_insert_employee_details]
	-- Add the parameters for the stored procedure here
	@emp_id varchar(50),@emp_first_name varchar(50),@emp_last_name varchar(50),@location varchar(50),@doj date,@pannumber varchar(50),@designation varchar(50), 
	@gender bit, @bank_name varchar(50),@account_number varchar(50),@active bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    insert into Tbl_Employee_Details OUTPUT Inserted.Emp_Detail_Id as ID values(@emp_id,@emp_first_name,@emp_last_name,@location,@pannumber,@designation,@doj,@gender,@bank_name,@account_number,@active);
    
	
END

GO

