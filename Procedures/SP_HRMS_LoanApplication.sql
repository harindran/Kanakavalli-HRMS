-------Loan Application History Details--------------------------------------------------------------------
---if exists(select 1 from sys.procedures where name='Innova_HRMS_LoanApplicaiton_History') Drop Procedure Innova_HRMS_LoanApplicaiton_History
--Go
Create Procedure "MIPL_HRMS_LoanApplicaiton_History"(in empid varchar(100),
in Tabletype varchar(1),
in docentry varchar(100),
in loancode varchar(100))
Language SQLSCRIPT
as
/*
BEGIN
Declare temp_var_0 varchar(10);
SELECT (select count(*) from  "PROCEDURES"
 where procedure_name = 'Innova_HRMS_LoanApplicaiton_History') INTO temp_var_0 FROM DUMMY;
IF :temp_var_0 > 0 THEN exec 'DROP PROCEDURE Innova_HRMS_LoanApplicaiton_History';
END IF;
End;
*/
begin

	if :Tabletype = 'H' then
	
		 Select T0."DocEntry", T0."DocNum", To_varchar(T0."U_DocDate", 'dd/MM/yy') as "Date",
		 (Case when T0."Canceled" = 'Y' then 'Cancelled' Else 
		 (Case when T0."Status" = 'O' then 'Open' when T0."Status" = 'C' 
		 then 'Close' when T0."Status" = 'D' then 'Waiting for Approval' 
		 else T0."Status" end) end)"Status" ,
		 T1."Name" as "Loan Type",
		 To_decimal(T0."U_LoanAmt", 30,2 ) as "Loan Amount",
		 "U_NoOfInst" as "No.Of.Inst",
		 To_decimal(T0."U_AmtMonth", 30,2 ) as "Amount/Month",
		 To_varchar("U_EffDate", 'dd/MM/yy') as "Eff Date",
		 To_decimal(ifnull("U_paidamt", 0),30,2) as "Paid Amt",
		 (Case when T0."Canceled" = 'Y' then  0 else 
		 To_decimal (ifnull("U_PendAmt", 0), 30,2) end) as "Bal Amt",
		 To_varchar(T0."U_IDNo") || ' - ' ||
		 To_varchar(T0."U_empName" ) as "header", T0."Object" as "objtype"
		 from "@SMPR_OLOA" T0 left join "@SMPR_OLON" T1 on T0."U_LoanCode" = T1."Code" 
		 Where T0."U_empID" =:empid and (T0."U_LoanCode" =:loancode or :loancode = '-1');
		 
	End if;
	
	if :Tabletype = 'D' then
	
		Select T0."DocEntry", To_varchar(T1."U_Date", 'dd/MM/yy') as "Installment Date",
		To_decimal(T1."U_Amount", 30,2) as "Amount",
		To_decimal(T1."U_PaidAmt", 30 ,2) as "Paid Amount",
		(Case when T0."Status" = 'O' then (Case when T1."U_Status" = 'C' 
		then 'Closed' else 'Open' end) when T0."Status" = 'C' then 'Close' 
		when T0."Status" = 'D' then 'Waiting for Approval' else T0."Status" end)"Status" ,
		(Case when ifnull(T1."U_dedsal", 'N') = 'N' then 'No' 
		else 'Yes' end) as "Consider in Payroll",
		ifnull("U_Detail", '') as "Details",
		ifnull(T1."U_trgttype", '') as "Target Type",
		ifnull(T1."U_trgtenty", '') as "Target Entry"
		from "@SMPR_OLOA" T0 inner join "@SMPR_LOA1" T1 on T0."DocEntry" = T1."DocEntry"
		Where T0."DocEntry" =:docentry;
		  
	End if;


End;
