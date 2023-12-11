-------Procedure for Leave/FInal Settlement--------------------------------------------------------------------
-------Leave/FInal Settlement EMployee Details FIlling--------------------------------------------------------------------
Create Procedure "MIPL_HRMS_GetEmpDetails_Settlement"(
in trzid varchar(100))
Language SQLSCRIPT
as
/*
BEGIN
Declare temp_var_0 varchar(10);
SELECT (select count(*) from  "PROCEDURES"
 where procedure_name = 'Innova_HRMS_GetEmpDetails_Settlement') INTO temp_var_0 FROM DUMMY;
IF :temp_var_0 > 0 THEN exec 'DROP PROCEDURE Innova_HRMS_GetEmpDetails_Settlement';
END IF;
End;
*/
Begin
	 select "U_empID", "U_firstNam" || ' ' || "U_lastName" "Name", T1."Name" "Department",
	 T2."name" "Designation",T0."U_gropCode" "Emptype", T3."Name" "Country",
	 Replace(To_varchar(T0."U_startdte", 'dd/MM/yy'), '/', '.') "JoiningDate",
	 ifnull(Replace(To_varchar(T0."U_termdate", 'dd/MM/yy'), '/', '.'), '') "termDate",
	 Replace(ifnull((select To_varchar(Max("U_LveSettDate"), 'dd/MM/yy') "DocEntry" 
	 from "@SMPR_OLSE" where "U_empID" = T0."U_empID" and "Canceled" <> 'Y'), ''), '/', '.') "Leavesettleddate",
	 ifnull((select Max("DocEntry") "DocEntry" from "@SMPR_OLVA" 
	 where "U_empID" = T0."U_empID" and ifnull("U_Approved", '') = 'Y' and "Canceled" <> 'Y' 
	 and ifnull("Status", '') = 'O' and ifnull("U_Payable",'N') <> 'Y' and "U_LveCode" = 'AL'), 0) "LeaveAppentry",
	 ifnull((Select Max("DocEntry") "DocEntry" from "@SMPR_OTIS" 
	 where "U_empID" = T0."U_empID" and ifnull("U_approved", '')='Y' and "Canceled" <> 'Y' and 
	 ifnull("Status", '')= 'O' 
	 and ifnull("U_payroll", 'N') <> 'Y'), 0) "Airticket",
	 (T4."LvstSalary" * 12 / 365) "PerDaySalary_lvst", T4."LvstSalary" "Salary_lvst",
	 "Salary" "Salary_month", ifnull(T0."U_paymode", '') "Paymode",
	 ifnull(T0."U_bankacct", '') "BankAcct",
	 ifnull(T0."U_bankiban", '') "Bankiban"
	 from "@SMPR_OHEM" T0 inner join "OUDP" T1 on T0."U_dept" = T1."Code"
	 inner join "OHPS" T2 on T2."posID" = T0."U_position"  left join "OCRY" T3 on T3."Code" = T0."U_ncountry" 
	 left join (select "Code", sum(Case when ifnull("U_LveSettlement", 'N') = 'Y' 
	 then "U_Amount" else 0 end)"LvstSalary", sum("U_Amount") "Salary" 
	 from "@SMPR_HEM1" Group by "Code") T4 on T4."Code" = T0."Code"
	 Where "U_ExtEmpNo" =:trzid;	 

End;
      
     

-------Leave/FInal Settlement Gratuity Calculation--------------------------------------------------------------------
Create Procedure "MIPL_HRMS_Grauity_Settlement"(
in asondate timestamp,
in empid varchar(10))
as 
/*
BEGIN
Declare temp_var_0 varchar(10);
SELECT (select count(*) from  "PROCEDURES"
where procedure_name = 'Innnova_HRMS_Grauity_Settlement') INTO temp_var_0 FROM DUMMY;
IF :temp_var_0 > 0 THEN exec 'DROP PROCEDURE Innnova_HRMS_Grauity_Settlement';
END IF;
End;
*/
Begin
	
	Declare Workingdays decimal(30,6);
	Declare lopdays decimal(30,6);
	Declare year decimal(30,6);
	Declare basic decimal(30,6);
		
	--Workingdays
 	
 	Select (Select ifnull(Days_between("U_startdte",	
	To_timestamp(ifnull("U_termdate", :asondate), 'dd/MM/yy')),0) 
 	from "@SMPR_OHEM" where "U_empID" =:empid
 	)into Workingdays from dummy;
	
	--lopdays

	Select(Select count(1) from "@SMPR_ODAS" T0 
	inner join "@SMPR_DAS1" T1 on T0."DocEntry" = T1."DocEntry" 
	Where T0."U_AttdDate" <= :asondate and T1."U_empID" =:empid and T1."U_AttStatus" = 'LP')
	into lopdays from dummy;
	
	--year

	Select(
	Round((cast(:Workingdays - :lopdays as float) / cast(30.4167 as float) / cast(12 as float)), 6)
	)
	into year from dummy;
    
    --basic

    Select(
	cast(ifnull((select Sum("U_Amount")  from "@SMPR_OHEM" T0 
	inner join "@SMPR_HEM1" T1 on T0."Code" = T1."Code" where T0."U_empID" =:empid and 
	ifnull(T1."U_FandF", '') = 'Y'), 0) as float)* Cast(12 as float) / cast(365 as float)
	)
	into basic from dummy;
	
	with "GradutiyDetails" as (Select case when :year  < 5 then To_int(:year) else 5 end "Grad @21 (Years)",
	case when :year  < 5 then (:year  - To_int(:year )) else 0 end "Grad @21 (Days)",
	case when :year > 5 then To_int(:year )- 5 else 0 end "Grad @30 (Years)",
	case when :year  > 5 then (:year  - To_int(:year ))  else 0 end "Grad @30 (Days)" from dummy)

	Select :Workingdays "Totaldays", :lopdays "LOP", :Workingdays - :lopdays "Working Days",
	:year "Year", :basic "basic", 
	 21 * ("Grad @21 (Years)" + "Grad @21 (Days)")+
	 30 * ("Grad @30 (Years)" +	"Grad @30 (Days)") as "Gratuity Days" ,
	Round(ifnull((:basic * 21 * ("Grad @21 (Years)" + "Grad @21 (Days)")) + 
	(:basic * 30 * ("Grad @30 (Years)" + "Grad @30 (Days)")), 0), 6) as "Gratuity Amount"
	from "GradutiyDetails" Where :year > 1;

End;

-------Leave/FInal Settlement History Details--------------------------------------------------------------------
Create Procedure "MIPL_HRMS_Settlement_History"(
in empid varchar(100))
Language SQLSCRIPT
as
/*
BEGIN
Declare temp_var_0 varchar(10);
SELECT (select count(*) from  "PROCEDURES"
 where procedure_name = 'Innova_HRMS_Settlement_History') INTO temp_var_0 FROM DUMMY;
IF :temp_var_0 > 0 THEN exec 'DROP PROCEDURE Innova_HRMS_Settlement_History';
END IF;
End;
*/
Begin

	select T0."DocEntry", T0."DocNum", T0."Object" "objtype", "U_IDNo" "Empid",
	"U_EmpName" "Employee Name",
	T0."U_DocDate" "Settlement Date",(Case when T0."U_setltype" = 'LS' then 'Leave Settlement' 
	Else 'Final Settlement' end) "Settlement Type",
	(Case when T0."Canceled" = 'Y' then 'Cancelled' 
	Else (Case when T0."Status" = 'O' then 'Open' when T0."Status" = 'C' then 'Close' 
	when T0."Status" = 'D' then 'Waiting for Approval' else T0."Status" end) end) "Status",
	ifnull("U_TotalAmt", 0) "Total Payable", 
	ifnull("U_lvsalamt", 0) "Leave Salary Amt",
	ifnull("U_lvncshmt", 0) "Leave Encashed Amt",
	ifnull("U_AiTiketAmt", 0) "AirTicket Amt",
	ifnull("U_advsalry", 0) "Advance Salary Amt",
	ifnull("U_Retention", 0) "Loan Deduction",
	ifnull("U_addedamt", 0) "Addition/Deduction",
	ifnull("U_gratuity", 0) "Gratutity"
	from "@SMPR_OLSE" T0  Where T0."U_EmpID" =:empid;
End;

--EXEC [Innova_HR_Airticket_History] '97','OHEM'""""

