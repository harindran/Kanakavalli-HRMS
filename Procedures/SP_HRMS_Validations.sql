-------Validation SP for HRMS----------------------------------------------------------------------------------------
--Loan Master Validation
if exists(select 1 from sys.procedures where name='Innova_HRMS_LoanMaster_Validation') Drop Procedure Innova_HRMS_LoanMaster_Validation
Go
CREATE PROC [dbo].[Innova_HRMS_LoanMaster_Validation](@Code nvarchar(255) ,  @object_type nvarchar(20) ,  @error AS NVARCHAR(10) OUTPUT ,@error_message AS NVARCHAR(MAX) OUTPUT  ) 
AS
BEGIN
	--==================================================================================================== 
	--SELECT @error='39004', @error_message='Maximum No Of Installment Less than or Equal to 10!!!!'
	--FROM [@SMPR_OLON] WHERE ISNULL(U_MxInstal,0)>10

	SELECT @error='39003', @error_message='Minimum PayAmount Should be lesser than Maximum PayAmount!!!!'
	FROM [@SMPR_OLON] WHERE ISNULL(U_MxAmt,0) <= ISNULL(U_MnPayAmt,0) AND Code=@Code

	SELECT  @error = '39002',  @error_message = 'Loan Name should not be left empty !!!'
	FROM [@SMPR_OLON]  A  WHERE  ISNULL(A.Name,'') = '' AND  A.Code = @Code

	SELECT  @error = '39001',  @error_message = 'Maximum No of Installment greater than equal to ['+ CONVERT(NVARCHAR(10),ISNULL(B.LoanMaxInstallment  ,0) )  +'] should not be left empty !!!'
	FROM [@SMPR_OLON]  A INNER JOIN ( SELECT ISNULL( U_LoanInstallment , 'N') LoanInstallment , ISNULL(U_LoanMaxInstallment  ,'')  LoanMaxInstallment FROM OADM ) B 
	ON A.Code IS NOT NULL WHERE  ISNULL( B.LoanInstallment , 'N') = 'Y' AND ISNULL(A.U_MxInstal,0) <> 0  AND ISNULL(B.LoanMaxInstallment  ,0) < ISNULL(A.U_MxInstal ,0) AND A.Code = @Code
	--====================================================================================================
END
Go
--Account Determination Validation
if exists(select 1 from sys.procedures where name='Innova_HRMS_AccountDetermination_Validation') Drop Procedure Innova_HRMS_AccountDetermination_Validation
Go
Create PROC [dbo].[Innova_HRMS_AccountDetermination_Validation] (@DocEntry AS NVARCHAR(MAX),@transaction_type varchar(10),@error AS NVARCHAR(10) OUTPUT,@error_message AS NVARCHAR(MAX) OUTPUT ) 
AS 
BEGIN 
	Declare @emptype varchar(10), @fromdate as datetime,@todate as datetime

	Select @emptype=U_Emptype,@fromdate=U_fromdate,@todate=U_todate from [@SMPR_ACCT] where DocEntry=@DocEntry 

	IF EXISTS (SELECT 1 where isnull(@todate,@fromdate) <@fromdate) Begin SET @error = '1201' SET @error_message = 'Account Determination To Date should be greater than fromdate' RETURN @error End

	IF EXISTS (SELECT 1 FROM [@SMPR_ACCT] where DocEntry<>@DocEntry and U_emptype=@emptype and U_todate is null) 
	Begin SET @error = '1201' SET @error_message = 'Account Determination for the Same Employee Type Already exists with out End date' RETURN @error End

	IF EXISTS (SELECT 1 FROM [@SMPR_ACCT] where DocEntry<>@DocEntry and U_emptype=@emptype and 
	(U_Todate between @fromdate and @todate or U_fromdate  between @fromdate and @todate or @fromdate between U_fromdate  and U_todate or @todate between U_fromdate  and U_todate))
	Begin SET @error = '1201' SET @error_message = 'Account Determination for the Same Employee Type Already exists in the given date range' RETURN @error End

Return @error 
End
Go
--Loan APplication Validation
if exists(select 1 from sys.procedures where name='Innova_HRMS_LoanApplicaiton_Validation') Drop Procedure Innova_HRMS_LoanApplicaiton_Validation
Go
CREATE PROC [dbo].[Innova_HRMS_LoanApplicaiton_Validation](@DocEntry nvarchar(255) ,  @object_type nvarchar(20),@transaction_type varchar(10) ,  @error AS NVARCHAR(10) OUTPUT ,@error_message AS NVARCHAR(MAX) OUTPUT  ) 
AS
BEGIN
	Select * into #temp_OLOA from [@SMPR_OLOA] where DocEntry=@DocEntry
	--Status Checking
	--IF exists(Select 1 from #temp_OLOA where isnull(status,'')='D' and @transaction_type='U')Begin SET @error = '1201' SET @error_message ='Cannot Update the document .Its already sent for Approval'RETURN @error end
	IF exists(Select 1 from #temp_OLOA where isnull(status,'')='C' and @transaction_type='U')Begin SET @error = '1201' SET @error_message = 'Closed Loan Application Cannot be updated.' RETURN @error end
	if  exists(SELECT  1 FROM #temp_OLOA WHERE isnull(Status,'')='C' and @transaction_type='U') Begin select @error = '40001',  @error_message = 'Loan Application Already Closed. You cannot Update' Return @error End
	if exists(SELECT  1 FROM #temp_OLOA WHERE convert(varchar,isnull(Series,''))='') Begin select @error = '40001',  @error_message = 'Series is Missing' Return @error End
	if exists(SELECT  1 FROM #temp_OLOA WHERE convert(varchar,isnull(Series,''))='') Begin select @error = '40001',  @error_message = 'Series is Missing' Return @error End
	
	if exists(Select 1 FROM #temp_OLOA WHERE Convert(date,ISNULL(U_EffDate,'20000101'))<convert(date,getdate()) AND @transaction_type='A') 
	Begin SELECT  @error = '40001',  @error_message = 'Effective Date Should be Greater than today date' Return @error end

	if exists (select 1 FROM #temp_OLOA a INNER JOIN [@SMPR_OLON] b ON a.U_LoanCode=b.Code WHERE ISNULL(a.U_LoanAmt,0)>ISNULL(B.U_MxAmt,0) AND @transaction_type='A') 
	Begin SELECT  @error = '40002',  @error_message = 'Loan Amount Should be Less than or Equal to Maximum Loan Amount From Loan Master'  Return @error End

	if exists (Select 1 from #temp_OLOA T0 inner join [@SMPR_LOA1] T1 on T0.DocEntry=T1.DocEntry group by T0.DocEntry,T0.U_LoanAmt having sum(T1.U_amount)<>T0.U_LoanAmt)
	Select @error = '40003',@error_message = 'Loan Amount And Installment Amount is not matching.Please check it.'
END
Go
--Leave APplication Validation
if exists(select 1 from sys.procedures where name='Innova_HRMS_LeaveApplication_Validation') Drop Procedure Innova_HRMS_LeaveApplication_Validation
Go
CREATE PROC [dbo].[Innova_HRMS_LeaveApplication_Validation] (@DocEntry AS NVARCHAR(MAX),@transaction_type varchar(10),@error AS NVARCHAR(10) OUTPUT,@error_message AS NVARCHAR(MAX) OUTPUT ) 
AS 
BEGIN 
	Declare @paydate as DateTime 
	if OBJECT_ID('tempdb..#Temp_OLVA') is not null Drop table #Temp_OLVA 
	Select * into #Temp_OLVA from [@SMPR_OLVA] WHERE DocEntry = @DocEntry 
	set @paydate=(select min(F_RefDate) from OFPR where isnull(U_HR,'')='Y')
	--Status Checking
	IF exists(Select 1 from #Temp_OLVA where isnull(status,'')='O' and @transaction_type='U') Begin SET @error = '1201' SET @error_message = 'Cannot Update the document. Its Already Approved.' RETURN @error end
	IF exists(Select 1 from #Temp_OLVA where isnull(status,'')='D' and @transaction_type='U')Begin SET @error = '1201' SET @error_message ='Cannot Update the document .Its already sent for Approval'RETURN @error end
	IF exists(Select 1 from #Temp_OLVA where isnull(status,'')='C' and @transaction_type='U')Begin SET @error = '1201' SET @error_message = 'Closed Leave Application Cannot be updated.' RETURN @error end
	IF exists(Select 1 from #Temp_OLVA where isnull(status,'')<>'R' and @transaction_type='U')Begin SET @error = '1201' SET @error_message = 'Cannot Update the document. Either its Already Approved or already sent for Approval.' RETURN @error end
	--Series checking
	IF EXISTS (SELECT 1 FROM #Temp_OLVA WHERE isnull(convert(varchar,Series),'')='') Begin SET @error = '1203' SET @error_message = 'Series is Missing' RETURN @error End
	--Document Date checking
	IF EXISTS (SELECT 1 FROM #Temp_OLVA WHERE U_DocDate is null) Begin SET @error = '1203' SET @error_message = 'Document Date is Missing' RETURN @error End
	--Employee Details checking
	IF EXISTS (SELECT 1 FROM #Temp_OLVA WHERE isnull(U_empID,'')='') Begin SET @error = '1203' SET @error_message = 'Employee Details is Missing' RETURN @error End
	--Leave Type checking
	IF EXISTS (SELECT 1 FROM #Temp_OLVA WHERE isnull(U_LveCode,'')='') Begin SET @error = '1203' SET @error_message = 'Leave Type is Missing' RETURN @error End
	--Date Field Checking
	IF EXISTS (SELECT 1 FROM #Temp_OLVA WHERE U_RejoinDt is null or U_FromDate  is null or U_Todate is null) Begin SET @error = '1203' SET @error_message = 'Leave Date or Rejoining date is Missing' RETURN @error End
	--No of Leave Days Checking
	IF EXISTS (SELECT 1 FROM #Temp_OLVA WHERE isnull(U_NoDayLve,0)<=0) Begin SET @error = '1203' SET @error_message = 'No of Leave Days Should be Greater than Zero' RETURN @error End
	--Elgigible Leave Days Checking
	IF EXISTS (SELECT 1 FROM #Temp_OLVA WHERE isnull(U_EliLvDay,0)<=0) Begin SET @error = '1203' SET @error_message = 'No of Elgigible Leave Days Should be Greater than Zero' RETURN @error End
	--Balance Leave Days Checking
	IF EXISTS (SELECT 1 FROM #Temp_OLVA WHERE isnull(U_BalLeave,0)<-0.49) Begin SET @error = '1203' SET @error_message = 'No of Leave Days Should be less than Elgigible Leave Days' RETURN @error End
	--Rejoining Date Checking with Leave to date
	IF EXISTS (SELECT 1 FROM #Temp_OLVA WHERE U_RejoinDt<=U_Todate) Begin SET @error = '1203' SET @error_message = 'Rejoining date Should be Greater than Leave End Date' RETURN @error End
	--Rejoining Date Checking with Leave to date
	IF EXISTS (Select 1 from #Temp_OLVA T0 inner join [@SMPR_OHEM] T1 on T0.U_IDNo=T1.U_ExtEmpNo and isnull(T1.U_probdate,Dateadd(MM,3,T1.U_startdte))>=T0.U_fromdate)
	Begin SET @error = '1203' SET @error_message = 'Leave Application Not allowed in Probation Period' RETURN @error End
	--Pay Period Checking
	If exists(Select 1 from #Temp_OLVA where (U_fromdate<@paydate or U_DocDate<@paydate))Begin SET @error = '1201' SET @error_message = 'Cannot post leave application for the closed period.' RETURN @error end
	--From date & To date Checking for Duplicate 
	IF EXISTS (SELECT 1 FROM [@SMPR_OLVA] T0 inner join #Temp_OLVA T1 on T0.U_empid=T1.U_empid  WHERE T0.Canceled='N' AND T0.DocEntry <> @DocEntry and --and T0.U_LveCode=T1.U_lvecode
	((T1.U_Fromdate between T0.U_fromdate and T0.U_todate) or (T1.U_todate between T0.U_fromdate and T0.U_todate) or (T0.U_Fromdate between T1.U_fromdate and T1.U_todate) or (T0.U_todate between T1.U_fromdate and T1.U_todate)))
	Begin SET @error = '1201' SET @error_message = 'Given Leave Date is already exists in Another leave application.' RETURN @error End
	--Replacement Employee Checking
	IF exists(Select 1 from #Temp_OLVA where isnull(U_rempid,'')='' and @transaction_type='A')Begin SET @error = '1201' SET @error_message = 'Replacement Employee Details is Missing. Please Check.' RETURN @error end
	if OBJECT_ID('tempdb..#Temp_OLVA') is not null Drop table #Temp_OLVA
	Return @error 
End
Go
--Daily Attendance Sheet Validation
if exists(select 1 from sys.procedures where name='Innvoa_HRMS_ODAS_Validation') Drop Procedure Innvoa_HRMS_ODAS_Validation
Go
CREATE PROC  [dbo].[Innvoa_HRMS_ODAS_Validation] ( @DocEntry AS NVARCHAR(MAX) ,  @object_type nvarchar(20),@transaction_type VARCHAR(10) ,  @error AS NVARCHAR(10) OUTPUT ,@error_message AS NVARCHAR(MAX) OUTPUT  ) 
AS
BEGIN
	IF OBJECT_ID('tempdb..#SMPR_ODAS') IS NOT NULL Drop table #SMPR_ODAS
	DECLARE @details VARCHAR(8000)
	Select T0.U_AttdDate,series,U_docdate,T1.LineId,T1.U_empID,T1.U_IDNo,T1.U_AttStatus,T1.U_TimeIn,T1.U_TimeOut,T1.U_HrsWrk,T1.U_OTHrs,T1.U_Halfday,T1.U_HalfStatus,T0.U_Remarks,T0.UserSign,@transaction_type [Transtype],T0.U_Location Location
	into #SMPR_ODAS from [@SMPR_ODAS] T0 inner join [@SMPR_DAS1] T1 on T0.DocEntry=T1.DocEntry where T0.DocEntry=@DocEntry 

	if exists (Select 1 from #SMPR_ODAS  where Location=4 and usersign<>'1' and Transtype='A') Begin select @error = 5100,@error_message= 'Manual Attendance Posting for Head office is not allowed.'  Return @error End
	If exists(SELECT 1 FROM #SMPR_ODAS WHERE U_AttdDate is null) Begin Select @error = '10002', @error_message = 'Attendance Date is Missing' Return @error End
	If exists(SELECT 1 FROM #SMPR_ODAS WHERE ISNULL(Series,'') = '') Begin Select @error = '10002', @error_message = 'Series Is Missing' Return @error End
	If exists(SELECT 1 FROM #SMPR_ODAS WHERE U_DocDate is null) Begin Select @error = '10002', @error_message = 'Document Date Is Missing' Return @error End
	If exists(SELECT 1 FROM #SMPR_ODAS WHERE U_AttdDate <=(Select max(U_Todate) from [@SMPR_OPRC] where isnull(U_Process,'')='Y')) 
	Begin Select @error = '10002', @error_message = 'Attendance Date is Not Valid.Payroll Already Processed' Return @error End

	set @details=''
	Select @details=coalesce(@details+',','')+Convert(varchar,LineId) FROM #SMPR_ODAS WHERE ISNULL(U_empID,'') = ''
	if @details<>'' Begin Select @error = '10002', @error_message = 'Employee Details is Missing For the Line No ('+@details +')' Return @error End

	set @details=''
	Select @details=coalesce(@details+',','')+Convert(varchar,U_IDNo) FROM #SMPR_ODAS WHERE (U_Timein is null or U_Timeout is null ) and UserSign<>'1'

	if @details<>'' Begin Select @error = '10002', @error_message = 'Time In or Time Out is Missing for the Employees ('+@details +')' Return @error End

	set @details=''
	Select @details=coalesce(@details+',','')+Convert(varchar,U_IDNo)  FROM #SMPR_ODAS WHERE U_HrsWrk is null and UserSign<>'1'
	if @details<>'' Begin Select @error = '10002', @error_message = 'Hours Worked is Missing for the Employees ('+ @details +')' Return @error End

	set @details=''
	Select @details=coalesce(@details+',','')+Convert(varchar,T2.U_IDNo) From [@SMPR_ODAS] T0 inner join  [@SMPR_DAS1] T1 on T0.DocEntry=T1.docentry 
	inner join #SMPR_ODAS T2 on T0.U_AttdDate=T2.U_AttdDate and T1.U_empid=T2.U_empid where T0.DocEntry<>@DocEntry
	if @details<>'' Begin Select @error = '10002', @error_message = 'Same date Attendance already avaialble for the Employees (' +@details +').' Return @error End

	set @details=''
	Select @details=coalesce(@details+',','')+Convert(varchar,T0.U_ExtEmpNo) From [@SMPR_OHEM] T0 inner join #SMPR_ODAS T1 on T0.U_empid=T1.U_empid Where T0.U_location='4' and T1.UserSign<>'1'
	if @details<>'' Begin Select @error = '10002', @error_message = 'Manual Attendance Not Allowed for the Head Office Employees (' +Replace(@details,'(,','(')+').' Return @error End

	set @details=''
	Select @details=coalesce(@details+',','')+Convert(varchar,T1.LineId ) From #SMPR_ODAS T1 where T1.U_Halfday='Y' and U_HalfStatus='-1'
	if @details<>'' Begin Select @error = '10002', @error_message = 'Half Day status Missing for the lines(' +Replace(@details,'(,','(')+').' Return @error End

	set @details=''
	Select @details=coalesce(@details+',','')+Convert(varchar,T1.LineId ) From #SMPR_ODAS T1 where T1.U_Halfday='N' and U_HalfStatus<>'-1'
	if @details<>'' Begin Select @error = '10002', @error_message = 'Half Day status Not Required for the lines(' +Replace(@details,'(,','(')+').' Return @error End

	set @details=''
	Select @details=coalesce(@details+',','')+Convert(varchar,T1.LineId ) From #SMPR_ODAS T1 where isnull(T1.U_AttStatus,'')=isnull(U_HalfStatus,'')
	if @details<>'' Begin Select @error = '10002', @error_message = 'Half Day status & Attendance Status Should not be same for the lines(' +Replace(@details,'(,','(')+').' Return @error End

	set @details=''
	Select @details=coalesce(@details+',','')+Convert(varchar,T1.LineId ) From #SMPR_ODAS T1 left join 
	(select U_empid,U_Fromdate,U_todate,U_lvecode from [@SMPR_OLVA] where Canceled<>'Y' and U_approved='Y' )T2 on T1.U_empid=T2.U_empid and T1.U_AttdDate between T2.U_Fromdate and T2.U_todate
	where T1.U_AttStatus in (select Code from [@SMPR_OLVE]  where isnull(U_empmastr,'')='Y') and isnull(T1.U_AttStatus,'') <> isnull(T2.U_lvecode,'')
	if @details<>'' Begin Select @error = '10002', @error_message = 'Leave Application Not exists for the lines(' +Replace(@details,'(,','(')+').' Return @error End

END
Go
--Air Ticket Issue Validation
if exists(select 1 from sys.procedures where name='Innvoa_HRMS_OTIS_Validation') Drop Procedure Innvoa_HRMS_OTIS_Validation
Go
Create PROC  [dbo].[Innvoa_HRMS_OTIS_Validation] ( @DocEntry AS NVARCHAR(MAX) ,  @transaction_type nvarchar(20) ,  @error AS NVARCHAR(10) OUTPUT ,@error_message AS NVARCHAR(MAX) OUTPUT  ) 
AS
BEGIN
	IF OBJECT_ID('tempdb..#SMPR_OTIS') IS NOT NULL Drop table #SMPR_OTIS
	Select T0.U_TickDate,T0.U_empid,T0.U_IDNO,T0.U_empname,T0.U_noofday,T0.U_total,T0.U_DocDate,T0.U_JOIndate,T0.U_LastTkDt,T0.U_LstTkAmt,T0.U_tcktpryr,T0.U_eligiamt,T0.Series,status
	into #SMPR_OTIS  from [@SMPR_OTIS] T0 where T0.DocEntry=@DocEntry 

	If exists(SELECT 1 FROM #SMPR_OTIS  WHERE status='C' and @transaction_type='U') Begin Select @error = '10002', @error_message = 'Update not allowed for the closed Claim' Return @error End
	If exists(SELECT 1 FROM #SMPR_OTIS  WHERE U_DocDate  is null) Begin Select @error = '10002', @error_message = 'Document Date is Missing' Return @error End
	If exists(SELECT 1 FROM #SMPR_OTIS  WHERE U_TickDate  is null) Begin Select @error = '10002', @error_message = 'Ticket Issue Date is Missing' Return @error End
	If exists(SELECT 1 FROM #SMPR_OTIS  WHERE isnull(series,'')='') Begin Select @error = '10002', @error_message = 'Series is Missing' Return @error End
	If exists(SELECT 1 FROM #SMPR_OTIS  WHERE isnull(U_IDNo ,'')='') Begin Select @error = '10002', @error_message = 'Employee Details is Missing' Return @error End
	If exists(SELECT 1 FROM #SMPR_OTIS  WHERE convert(date,U_DocDate)>convert(date,getdate())) Begin Select @error = '10002', @error_message = 'Future Date not allowed in Document Date ' Return @error End
	If exists(SELECT 1 FROM #SMPR_OTIS  WHERE convert(date,U_DocDate)<=(select max(U_todate) from [@SMPR_OPRC] where isnull(U_process,'')='Y')) 
				Begin Select @error = '10002', @error_message = 'Payroll already procesed for the given Document Date' Return @error End
	If exists(SELECT 1 FROM #SMPR_OTIS  WHERE U_TickDate <=U_LastTkDt) Begin Select @error = '10002', @error_message = 'Ticket Issue Date should be greater than Last Claimed date' Return @error End
	If exists(SELECT 1 FROM #SMPR_OTIS  WHERE U_noofday <=0) Begin Select @error = '10002', @error_message = 'No of Days Should be Greater than Zero' Return @error End
	If exists(SELECT 1 FROM #SMPR_OTIS  WHERE U_Total <=0) Begin Select @error = '10002', @error_message = 'Ticket Issue Amount should be greater than Zero' Return @error End

	IF OBJECT_ID('tempdb..#SMPR_OTIS') IS NOT NULL Drop table #SMPR_OTIS
END
Go
--Addition/Deduction Validation
if exists(select 1 from sys.procedures where name='Innova_HRMS_Addition_Deduction_Validation') Drop Procedure Innova_HRMS_Addition_Deduction_Validation
Go
Create PROC [dbo].[Innova_HRMS_Addition_Deduction_Validation] (@DocEntry AS NVARCHAR(MAX),@error AS NVARCHAR(10) OUTPUT,@error_message AS NVARCHAR(MAX) OUTPUT  )   
AS  
BEGIN  
	IF OBJECT_ID('tempdb..#SMPR_OPAD') IS NOT NULL Drop table #SMPR_OPAD
	SELECT T0.DocEntry,T0.U_PayPerid,T0.Series,T0.U_docdate,T1.lineid,T1.U_ExtEmpNo,T1.U_Amount,T1.U_PayCode,T1.U_Type into #SMPR_OPAD FROM [@SMPR_OPAD] T0 inner join [@SMPR_PAD1] T1 on T0.DocEntry=T1.DocEntry  WHERE T0.DocEntry = @DocEntry   

	if exists (Select 1 from #SMPR_OPAD where isnull(U_PayPerid,'')='') Begin Select @error='10001',@error_message='Pay Period Is Missing' Return @error End
	if exists (Select 1 from #SMPR_OPAD where isnull(Series,'')='') Begin Select @error='10001',@error_message='Series Is Missing' Return @error End
	if exists (Select 1 from #SMPR_OPAD where U_DocDate is null) Begin Select @error='10001',@error_message='Document Date Is Missing' Return @error End
	if exists (Select 1 from #SMPR_OPAD where isnull(U_ExtEmpNo,'')='') Begin Select @error='10001',@error_message='Employee Details is Missing' Return @error End
	if exists (Select 1 from #SMPR_OPAD where isnull(U_Type,'')='') Begin Select @error='10001',@error_message='Addition/Deduction Pay Type is Missing' Return @error End
	if exists (Select 1 from #SMPR_OPAD where isnull(U_PayCode,'')='') Begin Select @error='10001',@error_message='Addition/Deduction Pay Element Code is Missing'Return @error End
	if exists (Select 1 from #SMPR_OPAD T0 inner join [@SMPR_OPYE] T1 on T0.U_paycode=T1.code where isnull(T0.U_type,'')<>isnull(T1.U_type,'')) 
		Begin Select @error='10001',@error_message='Addition/Deduction Pay Element Code & Pay Type is not matching'Return @error End
	if exists (Select 1 from #SMPR_OPAD where isnull(U_Amount,0)<=0) Begin Select @error='10001',@error_message='Addition/Deduction Amount Should be Greater than Zero' Return @error End
	IF OBJECT_ID('tempdb..#SMPR_OPAD') IS NOT NULL Drop table #SMPR_OPAD
	Return @error
End
Go
--Leave/Final Settlement Validation
if exists(select 1 from sys.procedures where name='Innova_HRMS_LeaveSettlement_Validation') Drop Procedure Innova_HRMS_LeaveSettlement_Validation
Go
Create PROC [dbo].[Innova_HRMS_LeaveSettlement_Validation] (@DocEntry AS NVARCHAR(MAX) ,  @transaction_type nvarchar(2) ,  @error AS NVARCHAR(10) OUTPUT ,@error_message AS NVARCHAR(MAX) OUTPUT ) 
AS
BEGIN
	if @transaction_type='U' Begin Select @error='10001',@error_message='Settlement Cannot be Updated' Return @error End
	if OBJECT_ID('tempdb..#temp_OLSE') is not null Drop table #temp_OLSE Select * into #temp_OLSE from [@SMPR_OLSE] Where DocEntry=@DocEntry
	----------------------------------------Header Field Validations-------------------------------------------------------------
	if exists (Select 1 from #temp_OLSE where isnull(U_setltype,'')='') Begin Select @error='10001',@error_message='Settlement Type is Missing. Please Check.' Return @error End
	if exists (Select 1 from #temp_OLSE where U_LveSettDate is null) Begin Select @error='10001',@error_message='Settlement Date is Missing. Please Check.' Return @error End
	if exists (Select 1 from #temp_OLSE where isnull(U_EmpID,'')='') Begin Select @error='10001',@error_message='Employee Details is Missing. Please Check.' Return @error End
	if exists (Select 1 from #temp_OLSE where isnull(Series,'')='') Begin Select @error='10001',@error_message='Series is Missing. Please Check.' Return @error End
	if exists (Select 1 from #temp_OLSE where U_Docdate is null) Begin Select @error='10001',@error_message='Document Date is Missing. Please Check.' Return @error End
	---------------------------------------Leave Application VaLIDATION-------------------------------------------------------------
	If Exists(Select 1 from #temp_OLSE where isnull(U_LveAppEntry,'')<>'')
	Begin
	 if exists (Select 1 from #temp_OLSE where isnull(U_lvsalamt,0)=0) Begin Select @error='10001',@error_message='Leave Salary Details is Missing. Please Check.' Return @error End
	 If not exists(Select 1 from [@SMPR_OLVA] T0 where T0.DocEntry in (select isnull(U_LveAppEntry,'') from #temp_OLSE) and isnull(T0.Status,'O')='O' and isnull(T0.U_Approved,'N')='Y')
	 Begin Select @error='10001',@error_message='Selected Leave Application is not Valid. Please Check.' Return @error End
	End
	----------------------------------------Leave Encashement Validations-------------------------------------------------------------
	if exists (Select 1 from #temp_OLSE where isnull(U_lvbalday,0)<0) Begin Select @error='10001',@error_message='Leave balance days should not be less than zero. Please Check.' Return @error End
	if exists (Select 1 from #temp_OLSE where isnull(U_lvncshdy,0)<>0 and isnull(U_lvncshmt ,0)=0) Begin Select @error='10001',@error_message='Leave Encashment Details is Missing. Please Check.' Return @error End
	---------------------------------------Air Ticket Validations-------------------------------------------------------------
	if exists (Select 1 from #temp_OLSE where isnull(U_airtkten,'')<>'') 
	Begin 
	 if exists (Select 1 from #temp_OLSE where isnull(U_AiTiketAmt,0)=0) Begin Select @error='10001',@error_message='Air Ticket Details is Missing. Please Check.' Return @error End
	 If not exists(Select 1 from [@SMPR_OTIS] T0 where T0.DocEntry in (select isnull(U_airtkten,'') from #temp_OLSE) and isnull(T0.Status,'O')='O' )
	 Begin Select @error='10001',@error_message='Selected Air Ticket Entry is not Valid. Please Check.' Return @error End
	End
	------------------------------Advance Salary Validations-------------------------------------------------------------
	if exists (Select 1 from #temp_OLSE where U_salfrmdt is not null and U_saltodt is not null) 
	Begin 
	 if not exists(Select 1 from [@SMPR_LSE2] Where DocEntry=@DocEntry) Begin Select @error='10001',@error_message='Advance Salary Details Splitup is missing. Please Check.' Return @error End
	 if exists(Select 1 from #temp_OLSE T0 inner join (select Docentry,Min(Convert(date,U_fromdate))fromdate,max(Convert(date,U_todate))todate from [@SMPR_LSE2] Where DocEntry=@DocEntry Group by Docentry) T1 on T0.DocEntry=T1.DocEntry
	 Where Convert(date,T0.U_salfrmdt)<>Convert(date,T1.fromdate) Or Convert(date,T0.U_saltodt)<>Convert(date,T1.todate))
	 Begin Select @error='10001',@error_message='Advance Salary Splitup Dates are not Matching With From & To Date. Please Check.' Return @error End
	End
	------------------------------Loan Deduction Validations-------------------------------------------------------------
	If exists (Select 1 from #temp_OLSE T0 where isnull(U_retention,0) +(Select isnull(sum(isnull(U_amount,0)),0) from [@SMPR_LSE3] where docentry=@DocEntry and isnull(U_select,'N')='Y')<>0)
	Begin Select @error='10001',@error_message='Selected Loan Deduction  & Total Loan Deduction is not matching. Please check.' Return @error End
	----------------------------Addition Deduction Validations-------------------------------------------------------------
	If exists (Select 1 from #temp_OLSE T0 where isnull(U_addedamt,0) <>(Select isnull(sum((Case when isnull(U_mode,'')='A' then isnull(U_amount,0) else -isnull(U_amount,0) end)),0) from [@SMPR_LSE4] where docentry=@DocEntry))
	Begin Select @error='10001',@error_message='Detail Addition/Deduction  & Total Addition/Deduction is not matching. Please check.' Return @error End

	If exists(select 1 from [@SMPR_LSE4] T0 Where T0.U_mode='A' and T0.U_type not in (select code from [@SMPR_OPYE]  where U_Type='A') and docentry=@DocEntry)
	Begin Select @error='10001',@error_message='Addition Type is not valid in Additions Row. Please check.' Return @error End

	If exists(select 1 from [@SMPR_LSE4] T0 Where T0.U_mode='D' and T0.U_type not in (select code from [@SMPR_OPYE]  where U_Type='D') and docentry=@DocEntry)
	Begin Select @error='10001',@error_message='Deduction Type is not valid in Additions Row. Please check.' Return @error End

	if exists(Select 1 from [@SMPR_LSE4] where docentry=@DocEntry and ((isnull(U_mode,'')<>'' and isnull(U_amount,0)=0) or (isnull(U_mode,'')='' and isnull(U_amount,0)<>0)))
	Begin Select @error='10001',@error_message='Some Details Missing in Addition/Deduction. Please check.' Return @error End
	
	if exists(Select 1 from [@SMPR_LSE4] where docentry=@DocEntry and isnull(U_payroll,'N')='Y' and U_paydate is null)
	Begin Select @error='10001',@error_message='Payroll Date is Mandatory to Adjust in Payroll Process. Please check.' Return @error End
	------------------------------Gratuity Validation-------------------------------------------------------------
	if exists (Select 1 from #temp_OLSE where isnull(U_setltype,'')='FF' and isnull(U_gratuity,0)=0) Begin Select @error='10001',@error_message='Gratuity Details is Missing' Return @error End
	-------------------------------Footer Validation-------------------------------------------------------------
	if exists (Select 1 from #temp_OLSE where isnull(U_TotalAmt,0)<=0) Begin Select @error='10001',@error_message='Some Details is Missing. Total Payable should be Greater than zero' Return @error End
	if exists (Select 1 from #temp_OLSE where isnull(U_TotalAmt,0)<>(isnull(U_lvsalamt,0)+isnull(U_lvncshmt,0)+isnull(U_AiTiketAmt,0)+isnull(U_advsalry,0)+isnull(U_Retention,0)+isnull(U_addedamt,0)+isnull(U_gratuity,0))) 
	Begin Select @error='10001',@error_message='Total Payable is not matching with the Details. Please check.' Return @error End
	---------------------------------------------------------------------------------------------------------------------------
	if OBJECT_ID('tempdb..#temp_OLSE') is not null Drop table #temp_OLSE
	--Select @error='10001',@error_message='Settlement Cannot be Updated' Return @error 
	Return @error
END
Go
--Payroll Validation
if exists(select 1 from sys.procedures where name='Innova_HRMS_PayrollProcess_Validation') Drop Procedure Innova_HRMS_PayrollProcess_Validation
Go
Create PROC  [dbo].[Innova_HRMS_PayrollProcess_Validation] ( @DocEntry AS NVARCHAR(MAX) ,  @object_type nvarchar(20) ,  @error AS NVARCHAR(10) OUTPUT ,@error_message AS NVARCHAR(MAX) OUTPUT  ) 
AS
BEGIN
	if OBJECT_ID('tempdb..#Temp_payrol') is not null Drop table #Temp_payrol
	Select T0.DocEntry,T0.Series,T0.U_PayPerid,T0.U_FromDate,T0.U_todate,T0.U_EmployeeStatus,T0.U_docdate,T0.U_Location,T1.U_empid into #Temp_payrol 
	from [@SMPR_OPRC] T0 inner join [@SMPR_PRC1] T1 on T0.docentry=T1.docentry where T0.docentry=@docentry

	if exists(Select 1 from #temp_payrol where isnull(series,'')='') Begin Select @error='10001',@error_message='Series is Missing. Please Check.' Return @error End
	if exists(Select 1 from #temp_payrol where isnull(U_payperid,'')='') Begin Select @error='10001',@error_message='Payperiod is Missing. Please Check.' Return @error End
	if exists(Select 1 from #temp_payrol where U_Fromdate is null or U_Todate is NUll) Begin Select @error='10001',@error_message='From date or To date is Missing. Please Check.' Return @error End
	if exists(Select 1 from #temp_payrol where isnull(U_Employeestatus,'')='') Begin Select @error='10001',@error_message='Employee Status is Missing. Please Check.' Return @error End
	if exists(Select 1 from #temp_payrol where U_docdate is null) Begin Select @error='10001',@error_message='Document Date is Missing. Please Check.' Return @error End
	if exists(Select 1 from #temp_payrol where isnull(U_location,'#')='#') Begin Select @error='10001',@error_message='Location Details is Missing. Please Check.' Return @error End
	if exists(Select 1 from #temp_payrol Where isnull(U_empid,'')='') Begin Select @error='10001',@error_message='Payroll Details is Missing. Please Check.' Return @error End
	if OBJECT_ID('tempdb..#Temp_payrol') is not null Drop table #Temp_payrol
	Return @error
END
