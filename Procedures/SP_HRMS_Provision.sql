-------Table Creation for Provision--------------------------------------------------------------------------------------------------------------------------------------------------------------
If not exists (select 1 from sys.tables where name='HRMS_PROVISION_DETAILS')
Begin
	CREATE COLUMN TABLE "HRMS_PROVISION_DETAILS" 
("Docentry" integer NULL, "EmpID" nvarchar(11) NULL, "IDNo" nvarchar(100) NULL, "EmpName" nvarchar(4000) NULL, "VisaSponsor" nvarchar(250) NOT NULL, "Location" nvarchar(100) NOT NULL, "Department" nvarchar(20) NOT NULL,
 "JoinDate" date NULL, "ProvisionDate" date NULL, "WorkedDays" integer NULL, "Gratuity_Year" float NULL, "Gratuity_21Days" float NULL, "Gratuity_21Year" integer NULL, "Gratuity_30Days" float NULL, "Gratuity_30Year" integer NULL,
  "Gratuity_Amount" float NOT NULL, "Basic" decimal(38, 6) NOT NULL, "Allowance" decimal(38, 6) NOT NULL, "GrossSalary" decimal(38, 6) NULL, "Air_EligibleYear" smallint NULL, "Air_EligibleAmt" decimal(19, 6) NULL, 
  "Air_lastBooked" timestamp NULL, "Air_Days" integer NULL, "AirTicket_Amount" decimal(38, 14) NULL, "Leave_OB" decimal(38, 6) NULL, "Leave_Accured" decimal(23, 8) NULL, "Leave_taken" integer NULL, "Leave_Encashed" decimal(38, 6) NULL, 
  "Leave_LOP" integer NULL, "Leave_Balance" decimal(38, 6) NULL, "Leave_Amount" decimal(38, 6) NULL, "Ocrcode1" nvarchar(100) NULL, "Ocrcode2" nvarchar(100) NULL, "OCrcode3" nvarchar(100) NULL, "Ocrcode4" varchar(1) NOT NULL,
   "Ocrcode5" nvarchar(100) NULL, "Leave_debitCode" varchar(100) NULL, "Leave_debitName" varchar(100) NULL, "Leave_CreditCode" varchar(100) NULL, "Leave_CreditName" varchar(100) NULL, "Air_debitCode" varchar(100) NULL,
    "Air_debitName" varchar(100) NULL, "Air_CreditCode" varchar(100) NULL, "Air_CreditName" varchar(100) NULL, "Gratuity_debitCode" varchar(100) NULL, "Gratuity_debitName" varchar(100) NULL, "Gratuity_CreditCode" varchar(100) NULL,
     "Gratuity_CreditName" varchar(100) NULL, "JENO" varchar(100) NULL, "Finalize" varchar(10) NULL);

End

Go
-------Procedure for Provision Creation --------------------------------------------------------------------
if exists(select 1 from sys.procedures where name='Innova_HRMS_Provision_Creation') Drop Procedure Innova_HRMS_Provision_Creation
Go

CREATE Procedure [Innova_HRMS_Provision_Creation](@asondate as datetime)
as 
begin
	--Declare @asondate as datetime
	--set @asondate='20180630'
	if not exists (Select 1 from HRMS_PROVISION_DETAILS where ProvisionDate=@asondate)
	Begin

		IF OBJECT_ID('tempdb..#HRMS_PROVISION') IS NOT NULL drop table #HRMS_PROVISION
		
		Select EmpID,IDNO,EmpName,VisaSponsor,Location,JoinDate,ProvisionDate,WorkedDays,Gratuity_Year,Gratuity_21Days,Gratuity_21Year,Gratuity_30Days,Gratuity_30Year,Gratuity_Amount,Basic,Allowance,GrossSalary,
		Air_EligibleYear,Air_EligibleAmt,Air_lastBooked,Air_Days,AirTicket_Amount,Leave_OB,Leave_Accured,Leave_taken,Leave_Encashed,Leave_LOP,Leave_Balance,Leave_Amount 
		into #HRMS_PROVISION from HRMS_PROVISION_DETAILS where 1=2

		Insert into #HRMS_PROVISION 
		(EmpID,IDNO,EmpName,VisaSponsor,Location,JoinDate,ProvisionDate,WorkedDays,Gratuity_Year,Gratuity_21Days,Gratuity_21Year,Gratuity_30Days,Gratuity_30Year,Gratuity_Amount,Basic,Allowance,GrossSalary,
		Air_EligibleYear,Air_EligibleAmt,Air_lastBooked,Air_Days,AirTicket_Amount,Leave_OB,Leave_Accured,Leave_taken,Leave_Encashed,Leave_LOP,Leave_Balance,Leave_Amount)
		Exec [@SMS_SP_Employee-wise_End_of_Service]  @asondate

		Declare @Docentry as int
		set @docentry =(Select isnull(Max(Docentry),0)+1 from HRMS_PROVISION_details)

		Insert into HRMS_PROVISION_DETAILS 
		(Docentry,EmpID,IDNo,EmpName,VisaSponsor,Location,Department,JoinDate,ProvisionDate,WorkedDays,Gratuity_Year,Gratuity_21Days,Gratuity_21Year,Gratuity_30Days,Gratuity_30Year,Gratuity_Amount
		,Basic,Allowance,GrossSalary,Air_EligibleYear,Air_EligibleAmt,Air_lastBooked,Air_Days,AirTicket_Amount,Leave_OB,Leave_Accured,Leave_taken,Leave_Encashed,Leave_LOP,Leave_Balance,Leave_Amount,
		Ocrcode1,Ocrcode2,OCrcode3,Ocrcode4,Ocrcode5,Leave_debitCode,Leave_debitName,Leave_CreditCode,Leave_CreditName,Air_debitCode,Air_debitName,Air_CreditCode,Air_CreditName,
		Gratuity_debitCode,Gratuity_debitName,Gratuity_CreditCode,Gratuity_CreditName,JENO)

		select @docentry [Docentry],T0.EmpID,IDNO,EmpName,VisaSponsor,Location,T2.Name[Department],JoinDate,ProvisionDate,WorkedDays,Gratuity_Year,Gratuity_21Days,Gratuity_21Year,Gratuity_30Days,Gratuity_30Year,Gratuity_Amount,
		Basic,Allowance,GrossSalary,Air_EligibleYear,Air_EligibleAmt,Air_lastBooked,Air_Days,AirTicket_Amount,Leave_OB,Leave_Accured,Leave_taken,Leave_Encashed,Leave_LOP,Leave_Balance,Leave_Amount,
		T3.U_costcode [Ocrcode1],T2.U_costcode[Ocrcode2],T0.IDNO[Ocrcode3],''[Ocrcode4],T1.U_otherscc [Ocrcode5],U_lveprvdc,U_lveprvdn,U_lveprvcc,U_lveprvcn,U_airprvdc,U_airprvdn,U_airprvcc,U_airprvcn,U_graprvdc,U_graprvdn,U_graprvcc,U_graprvcn,''
		from #HRMS_PROVISION T0 left join [@SMPR_OHEM] T1 on T0.empid=T1.U_empid and T0.IDNo=T1.U_ExtEmpNo left join OUDP T2 on T2.code=T1.U_dept  left join oubr T3 on T3.code=T1.U_branch
		left join (select Distinct U_emptype,U_lveprvdc,U_lveprvdn,U_lveprvcc,U_lveprvcn,U_airprvdc,U_airprvdn,U_airprvcc,U_airprvcn,U_graprvdc,U_graprvdn,U_graprvcc,U_graprvcn from [@SMPR_ACCT] 
					Where @asondate between U_Fromdate and isnull(U_Todate,@asondate)) T4 on T4.U_emptype=T1.U_gropCode
	End
	
	IF OBJECT_ID('tempdb..#HRMS_PROVISION') IS NOT NULL drop table #HRMS_PROVISION
End

-------Procedure for the Provision Report--------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE Procedure "MIPL_HRMS_Provision_Report"
(in month varchar(10),
in Year varchar(10))
Language SQLSCRIPT 
As

BEGIN
Declare temp_var_0 varchar(10);
SELECT (select count(*) from  "PROCEDURES"
 where procedure_name = 'Innova_HRMS_Provision_Report') INTO temp_var_0 FROM DUMMY;
IF :temp_var_0 > 0 THEN exec 'DROP PROCEDURE Innova_HRMS_Provision_Report';
END IF;

Begin

	Declare Docentry varchar(10);
	Declare previousEntry varchar(10);
	
	Select (Select distinct "Docentry" from "HRMS_PROVISION_DETAILS"
	Where Month("ProvisionDate") = :month and Year("ProvisionDate") =:Year)
	into Docentry from dummy;
	
	select (Select Max("Docentry") 
	from "HRMS_PROVISION_DETAILS" where "ProvisionDate"=(select Max("ProvisionDate") 
	from "HRMS_PROVISION_DETAILS" 
	where "ProvisionDate" < (Select distinct "ProvisionDate" from "HRMS_PROVISION_DETAILS" 
	WHere "Docentry" = :Docentry))) into previousEntry from dummy;

	with "previousdetails" as (
	select "EmpID", "IDNo", "ProvisionDate", "Gratuity_Amount", "AirTicket_Amount", "Leave_Amount",
	(("Gratuity_30Year" + "Gratuity_30Days" ) * 30) + (("Gratuity_21Year" + "Gratuity_21Days") * 21)"GratuityDays",
	"Leave_Balance" from "HRMS_PROVISION_DETAILS" WHere "Docentry" =:previousEntry),
	"Empdetails" as (
	select T0."U_ExtEmpNo", T1."Descr"  from "@SMPR_OHEM" T0 
	inner join (select B."FldValue", B."Descr" from "CUFD" A 
	inner join "UFD1" B on A."TableID" = B."TableID" and A."FieldID" = B."FieldID"  
	where A."TableID" = '@SMPR_OHEM' and A."AliasID" = 'gropcode') T1 on 
	T0."U_gropCode" = T1."FldValue")

	Select T0."ProvisionDate", T0."IDNo" as "Emp ID", T0."EmpName" as "Emp Name",
	T2."Descr" as "Employee Group", T0."VisaSponsor" as "Visa", T0."Department" as "Department",
	T0."Location" as "Branch",
	ifnull(((T0."Gratuity_30Year" + T0."Gratuity_30Days") * 30) +
	((T0."Gratuity_21Year" + T0."Gratuity_21Days") * 21), 0) as "C_GratuityDays",
	ifnull(T1."GratuityDays", 0) as "P_GratuityDays",
	ifnull(((T0."Gratuity_30Year" + T0."Gratuity_30Days" ) * 30) + 
	((T0."Gratuity_21Year"  + T0."Gratuity_21Days") *21), 0) - 
	ifnull(T1."GratuityDays", 0) as "GratuityDays",
	ifnull(T0."Gratuity_Amount", 0) as "C_Gratuity", 
	ifnull(T1."Gratuity_Amount", 0) as "P_Gratuity",
	ifnull(T0."Gratuity_Amount", 0) - ifnull(T1."Gratuity_Amount", 0) as "Gratuity",
	T0."Air_EligibleAmt", T0."Air_EligibleYear", T0."Air_lastBooked" as "Last Booked Date",
	ifnull(T0."AirTicket_Amount", 0) as "C_AirTicket", 
	ifnull(T1."AirTicket_Amount", 0) as "P_AirTicket",
	ifnull(T0."AirTicket_Amount", 0) - ifnull(T1."AirTicket_Amount", 0) as "AirTicket",
	ifnull(T0."Leave_Balance", 0) as "C_Leavebalance", 
	ifnull(T1."Leave_Balance", 0) as "P_leavebalance",
	ifnull(T0."Leave_Balance", 0) - ifnull(T1."Leave_Balance", 0) as "LeaveBalance",
	ifnull(T0."Leave_Amount", 0) as "C_Leave", ifnull(T1."Leave_Amount", 0) as "P_Leave",
	ifnull(T0."Leave_Amount", 0) - ifnull(T1."Leave_Amount", 0) as "Leave",
	left(DayName(T0."ProvisionDate"), 3) || '-' || 
	To_varchar(Dayname(T0."ProvisionDate")) as "Current",
	left(DayName(T1."ProvisionDate"), 3) || '-' ||
	To_varchar(Dayname(T1."ProvisionDate")) as "Previous",
	ifnull("Finalize", '') as "Finalize",
	ifnull("JENO", '') as "JENO"
	from "HRMS_PROVISION_DETAILS" T0 left join 
	"previousdetails" T1 on T0."EmpID" = T1."EmpID" 
	inner join "Empdetails" T2 on T2."U_ExtEmpNo" = T0."IDNo"
	Where T0."Docentry" =:Docentry order by T0."IDNo"; 

End;
End;

