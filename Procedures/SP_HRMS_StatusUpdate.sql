---HRMS-Loan Status Update------------------------------------------------------------
--Loan Status Update
if exists(select 1 from sys.procedures where name='Innova_HRMS_LoanStatusUpdate_ORCT') Drop Procedure Innova_HRMS_LoanStatusUpdate_ORCT
Go
CREATE Procedure [Innova_HRMS_LoanStatusUpdate_ORCT](@docentry as varchar(100))
as
Begin
	UPDATE B SET U_trgtenty=A.DocEntry,U_trgttype =A.objtype,U_PaidAmt=SumApplied,U_Status='C' FROM
	(SELECT T0.DocNum,T0.DocEntry,T1.U_BaseEntry,T1.U_LineNo,T0.DocDate,T0.ObjType,T1.SumApplied FROM ORCT T0 inner join RCT4 T1 on T0.DocEntry=T1.DocNum WHERE T0.DocEntry =@DocEntry and T1.U_PaymentType='LA') A 
	INNER JOIN [@SMPR_LOA1] B ON ISNULL( A.U_BaseEntry, '')= B.DocEntry and B.lineid=ISNULL(A.U_LineNo, '')

	Update T0 set T0.U_paidamt=T1.total from [@SMPR_OLOA] T0 inner join (select DOcentry,sum(isnull(U_PaidAmt,0))[total] from [@SMPR_LOA1] group by DOcentry) T1 on T0.DocEntry=T1.DocEntry Where isnull(T0.status,'')='O'
	Update [@SMPR_OLOA] set U_PendAmt=U_LoanAmt-U_paidamt Where isnull(status,'')='O'
	Update [@SMPR_OLOA] set Status='C' Where isnull(status,'')='O' and isnull(U_PendAmt,0)=0
End
Go
--Leave Settlement Status Update
if exists(select 1 from sys.procedures where name='Innova_HRMS_LeaveSettlement_Status_Update') Drop Procedure Innova_HRMS_LeaveSettlement_Status_Update
Go
Create PROC [dbo].[Innova_HRMS_LeaveSettlement_Status_Update] (@DocEntry AS NVARCHAR(MAX)) 
AS
BEGIN
	--Declare @docentry as varchar(100)
	--set @docentry=183
	if exists(select 1 from [@SMPR_OLSE] where DocEntry=@docentry and U_approved='Y' and status='O')
	Begin
		--Leave Application Status Update
		 Update [@SMPR_OLVA] set status='C',U_trgtenty=@docentry,U_trgttype='OLSE' where DocEntry in (select U_LveAppEntry from [@SMPR_OLSE] where DocEntry=@docentry)
		 
		 --Loan Application EMI Status Update
		 Update T1 set T1.U_Status='C',T1.U_TrgtEnty=@docentry,U_Trgttype='OLSE',T1.U_PaidAmt=T0.U_amount from [@SMPR_LSE3]  T0 inner join [@SMPR_LOA1] T1 on T0.U_loanapen=T1.DocEntry and T0.U_loanline=T1.lineid 
		 where T0.DocEntry=@docentry and isnull(T0.U_select,'N')='Y'
		 Update T0 set T0.U_paidamt=isnull(T1.[PaidAmt],0.0),T0.U_PendAmt=isnull(T1.[TotalAmt],0.0)-isnull(T1.[PaidAmt],0.0) from [@SMPR_OLOA] T0 
		 inner join (select DocEntry,sum(isnull(U_amount,0.0))[TotalAmt],sum(isnull(U_paidamt,0.0))[PaidAmt] from [@SMPR_LOA1] Group by DocEntry ) T1 on T0.DocEntry=T1.DocEntry 
		 where T0.DocEntry in (Select U_loanapen from [@SMPR_LSE3]  where DocEntry=@DocEntry and isnull(U_select,'N')='Y')
		 Update T0 set T0.Status='C' from [@SMPR_OLOA] T0 where T0.DocEntry in (Select U_loanapen from [@SMPR_LSE3]  where DocEntry=@DocEntry and isnull(U_select,'N')='Y') and isnull(T0.U_PendAmt,0)=0.0
		 
		 --Air Ticekt Issue Status Update
		 Update [@SMPR_OTIS] set Status='C',U_Status='C',U_trgttype='OLSE',U_trgtenty=@docentry  where docentry in (select U_airtkten from [@SMPR_OLSE] where DocEntry=@docentry)
	End
End
Go
--Payroll Status Update
if exists(select 1 from sys.procedures where name='Innova_HRMS_Payroll_Status_Update') Drop Procedure Innova_HRMS_Payroll_Status_Update
Go
CREATE PROC [dbo].[Innova_HRMS_Payroll_Status_Update] (@DocEntry AS NVARCHAR(MAX)) 
AS
BEGIN
	--Declare @docentry as varchar(100)
	--set @docentry=183
	Declare @Fromdate as datetime,@todate as datetime,@payperiod as varchar(100)
	Select @fromdate=U_Fromdate,@todate=U_Todate,@payperiod=U_PayPerid from [@SMPR_OPRC] where isnull(U_Process,'')='Y' and DocEntry=@DocEntry

	if exists( select 1 from [@SMPR_OPRC] where isnull(U_Process,'')='Y' and DocEntry=@DocEntry)
	Begin
		if object_ID('tempdb..#temp_empdetails') IS NOT NULL Drop table #temp_empdetails
		select * into #temp_empdetails from (select U_empid,isnull(U_FD1,0.00)[LoanDeducted],isnull(U_FA1,0.00)[Airticketpaid] from [@SMPR_PRC1] where DocEntry=@DocEntry)A

		--Leave Application Status Update
		Update [@SMPR_OLVA] set status='C',U_trgtenty=@DocEntry,U_trgttype='OPRC' where isnull(status,'')<>'C' and isnull(U_Payable,'')='Y' and U_todate <=@todate 
		and U_empid in (select U_empid from #temp_empdetails) and isnull(U_approved,'')='Y'

		--Loan Application EMI Status Update
		Update T1 set T1.U_Status='C',T1.U_TrgtEnty=@docentry,U_Trgttype='OPRC',T1.U_PaidAmt=T1.U_amount from [@SMPR_OLOA] T0 inner join [@SMPR_LOA1] T1 on T0.DocEntry=T1.DocEntry 
		where T1.U_Date between @Fromdate and @todate and T0.U_empid in (select U_empid from #temp_empdetails where isnull(LoanDeducted,0.00)<>0) and isnull(T1.U_dedsal,'')='Y'and isnull(T1.U_Status,'O')<>'C'
		and isnull(T0.U_Approved,'')='Y' and isnull(T0.Status,'')='O' and isnull(T0.Canceled,'')<>'Y'

		Update T0 set T0.U_paidamt=isnull(T1.[PaidAmt],0.0),T0.U_PendAmt=isnull(T1.[TotalAmt],0.0)-isnull(T1.[PaidAmt],0.0) from [@SMPR_OLOA] T0 
		inner join (select DocEntry,sum(isnull(U_amount,0.0))[TotalAmt],sum(isnull(U_paidamt,0.0))[PaidAmt] from [@SMPR_LOA1] Group by DocEntry) T1 on T0.DocEntry=T1.DocEntry 
		where T0.DocEntry in (select distinct Docentry from [@SMPR_LOA1] where U_Date between @Fromdate and @todate) and T0.U_empid in (select U_empid from #temp_empdetails  where isnull(LoanDeducted,0.00)<>0)

		Update T0 set T0.Status='C' from [@SMPR_OLOA] T0 where T0.DocEntry in (select distinct Docentry from [@SMPR_LOA1] where U_Date between @Fromdate and @todate) and isnull(T0.U_PendAmt,0)=0.0
		and T0.U_empid in (select U_empid from #temp_empdetails  where isnull(LoanDeducted,0.00)<>0)
		
		--Air Ticekt Issue Status Update
		Update [@SMPR_OTIS] set Status='C',U_Status='C',U_trgttype='OPRC',U_trgtenty=@docentry  where isnull(Status,'O')='O' and Canceled<>'Y' and isnull(U_Approved,'')='Y' and isnull(U_Payroll,'')='Y'
		and U_DocDate between @fromdate and @todate and U_empid in (select U_empid from #temp_empdetails where isnull(Airticketpaid,0.00)<>0)

		----Addition & Deduction Screen Status Update
		--Update [@SMPR_OPAD] set Status='C' where U_PayPerid=@payperiod and Canceled<>'Y' and isnull(Status,'O')='O'
		if object_ID('tempdb..#temp_empdetails') IS NOT NULL Drop table #temp_empdetails
	End
End