	--Add in Transcation Notification
----================================================HRMS Validation Started=====================================================================================================================================================================    
---> HRMS - Pay Elements <---  
if (@object_type = 'OPYE') and (@transaction_type in (N'A',N'U')) if exists (SELECT 1 FROM [@SMPR_OPYE] WHERE ISNULL(U_Sequence,'')=''  AND Code =@list_of_cols_val_tab_del) Begin Select @error='39001', @error_message='Element Sequence' End
---> Loan Master <---  
if (@object_type = 'OLON') and (@transaction_type in (N'A',N'U')) EXEC [dbo].[Innova_HRMS_LoanMaster_Validation]  @list_of_cols_val_tab_del, @object_Type , @error OUTPUT , @error_message OUTPUT  
---> Account Determination <---  
if @object_type='SMPRACCT' EXEC [dbo].[Innova_HRMS_AccountDetermination_Validation] @list_of_cols_val_tab_del,@transaction_type,@error OUTPUT , @error_message OUTPUT    
---> HRMS - Loan Application Form  <---  
if (@object_type = 'OLOA') and (@transaction_type in (N'A',N'U'))  EXEC [dbo].[Innova_HRMS_LoanApplicaiton_Validation]   @list_of_cols_val_tab_del, @object_Type ,@transaction_type, @error OUTPUT , @error_message OUTPUT        
---> Leave APplication <---  
if @object_type='OLVA' and @transaction_type IN ( N'U',N'A') EXEC [dbo].[Innova_HRMS_LeaveApplication_Validation] @list_of_cols_val_tab_del,@transaction_type ,@error OUTPUT , @error_message OUTPUT 
---> Daily Attendance Sheet<---  
if @object_type='ODAS' and @transaction_type IN ( N'U',N'A')  EXEC [Innvoa_HRMS_ODAS_Validation] @list_of_cols_val_tab_del,@object_type,@transaction_type, @error OUTPUT , @error_message OUTPUT         
---> Air Ticket Issue <---  
if @object_type='OTIS' and @transaction_type IN (N'U',N'A')  EXEC [dbo].[Innvoa_HRMS_OTIS_Validation] @list_of_cols_val_tab_del, @transaction_type ,@error OUTPUT , @error_message OUTPUT 
---> HRMS - Addition Deduction <---  
if (@object_type='OPAD' and @transaction_type IN ( N'U',N'A')) EXEC [dbo].[Innova_HRMS_Addition_Deduction_Validation] @list_of_cols_val_tab_del ,@error OUTPUT , @error_message OUTPUT    
---> HRMS- Settlemtn <---  
if (@object_type = 'OLSE') and (@transaction_type in (N'A',N'U')) EXEC [dbo].[Innova_HRMS_LeaveSettlement_Validation]  @list_of_cols_val_tab_del, @transaction_type , @error OUTPUT , @error_message OUTPUT 
---> PayRoll process <---  
IF (@object_type = 'OPRC') and (@transaction_type in (N'A',N'U')) EXEC [Innova_HRMS_PayrollProcess_Validation] @list_of_cols_val_tab_del, @object_Type , @error OUTPUT , @error_message OUTPUT   
 ----================================================HRMS Validation Ended=====================================================================================================================================================================    

 --Add in Post Transcation Notification
 ----=====================================HRMS Status Update=====================================================================================================================================================================    
---Incoming Payment --Loan Application Row & Header Status Update for the Manual Receipt(HRMS)-------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF  @object_type = '24'  and @transaction_type In (N'A',N'U') Begin    EXEC  [dbo].[Innova_HRMS_LoanStatusUpdate_ORCT]  @list_of_cols_val_tab_del End  
---Leave Application Approval Status Update-------------------------------------------------------------------------------------------------------------------------------------------------
if @object_type='OLVA' and @transaction_type IN ( N'U',N'A')  UPDATE [@SMPR_OLVA] SET Status = 'D',U_Approved = 'W'  WHERE DocEntry = @list_of_cols_val_tab_del 
--- HRMS - Loan Application Form-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
if (@object_type = 'OLOA') and (@transaction_type in (N'A',N'U')) Delete [@SMPR_LOA1] where docentry=@list_of_cols_val_tab_del and U_Date  is null and U_amount=0
---HRMS - Leave/Final Settlement Form-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
if (@object_type = 'OLSE') and (@transaction_type in (N'A',N'U'))  EXEC [dbo].[Innova_HRMS_LeaveSettlement_Status_Update]  @list_of_cols_val_tab_del
---Payroll Process Update-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF (@object_type = 'OPRC') and (@transaction_type in (N'A',N'U'))  EXEC [dbo].[Innova_HRMS_Payroll_Status_Update] @list_of_cols_val_tab_del
----=====================================HRMS Status Update Ended=====================================================================================================================================================================    