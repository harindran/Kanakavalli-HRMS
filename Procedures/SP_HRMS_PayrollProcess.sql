	-------Procedure for Payroll Process----------------------------------------------------------------------------------------
Create Procedure "MIPL_HRMS_PayrollProcess"
 ( in location nvarchar(5000),in payperiod varchar(100),in empstatus varchar(100),in docentry varchar(10))

LANGUAGE SQLSCRIPT

as

BEGIN
Declare temp_var_0 varchar(10);
Declare fromdate  timestamp;
declare todate  timestamp;
/*
SELECT (select count(*) from  "PROCEDURES"
 where procedure_name='Innova_HR_GetEmpDetails_AirticketIssue') INTO temp_var_0 FROM DUMMY;
IF :temp_var_0 > 0 THEN exec 'DROP PROCEDURE Innova_HRMS_PayrollProcess';
end if;
*/
select (select "F_RefDate"  from OFPR Where "Code"=:payperiod) into fromdate from dummy;

select (select "T_RefDate"  from OFPR Where "Code"=:payperiod) into todate from dummy;
/*
IF 'tempdb..#Temp_Payroll' IS NOT NULL THEN exec 'drop table #Temp_Payroll';
END IF;

*/

 create local temporary table #mytemp (
 Code varchar(100) , U_empID varchar(100),EmpCode varchar(100)
 ,EmpName integer,Designation integer,DeptCode varchar,Department varchar,PayMode integer,TotalDays integer,WorkedDays integer,LopDays integer,PHDays integer,LveDays integer,WODays integer ,PayableDays integer,OTHrs integer,OTDays integer,TotalOT integer,TotalOT_Perhour integer,TotalBasic integer,TotalSalary decimal(30, 2),
     A1 decimal(30, 2),A2 decimal(30, 2) ,A3 decimal(30, 2),A4 decimal(30, 2),A5 decimal(30, 2),A6 decimal(30, 2),A7 decimal(30, 2),A8 decimal(30, 2),A9 decimal(30, 2),A10 decimal(30, 2),A11 decimal(30, 2),A12 decimal(30, 2),A13 decimal(30, 2),A14 decimal(30, 2),A15 decimal(30, 2),A16 decimal(30, 2),A17 decimal(30, 2),A18 decimal(30, 2),A19 decimal(30, 2),A20 decimal(30, 2),AB1 decimal(30, 2),AB2 decimal(30, 2),AB3 decimal(30, 2),AB4 decimal(30, 2),AB5 decimal(30, 2),AB6 decimal(30, 2),AB7 decimal(30, 2),AB8 decimal(30, 2)
    ,AB9 decimal(30, 2),AB10 decimal(30, 2),AB11 decimal(30, 2),AB12 decimal(30, 2),AB13 decimal(30, 2),AB14 decimal(30, 2),AB15 decimal(30, 2),AB16 decimal(30, 2)
 ,AB17 decimal(30, 2),AB18 decimal(30, 2),AB19 decimal(30, 2),AB20 decimal(30, 2),DB1 decimal(30, 2),DB2 decimal(30, 2),DB3 decimal(30, 2),DB4 decimal(30, 2),DB5 decimal(30, 2)
 ,DB6 decimal(30, 2),DB7 decimal(30, 2) ,DB8 decimal(30, 2),DB9 decimal(30, 2),DB10 decimal(30, 2),DB11 decimal(30, 2),DB12 decimal(30, 2),
    DB13 decimal(30, 2),DB14 decimal(30, 2),DB15 decimal(30, 2),DB16 decimal(30, 2),DB17 decimal(30, 2),DB18 decimal(30, 2),DB19 decimal(30, 2),DB20 decimal(30, 2) 
    ,OTAmt decimal(30, 2),GrossSalary decimal(30, 2),LoanDeduction decimal(30, 2),AirTicekt_Addition decimal(30, 2),
 AL_Settled_Deduction decimal(30, 2),AdvanceSal_Settlement_Deduction decimal(30, 2),TripAllowance_Addition decimal(30, 2),TotalAddition decimal(30, 2),TotalDeduction decimal(30, 2) 
,NetSalaryB4_Round decimal(30, 2),Roundoff decimal(30, 2),NetSalary decimal(30, 2));


 INSERT INTO #mytemp (Code,U_empID,EmpCode,EmpName,Designation,DeptCode,Department,PayMode,TotalDays,WorkedDays,LopDays,PHDays,LveDays,WODays,PayableDays,OTHrs,OTDays,TotalOT,TotalOT_Perhour,TotalBasic,TotalSalary,

 A1,A2,A3,A4,A5,A6,A7,A8,A9,A10,A11,A12,A13,A14,A15,A16,A17,A18,A19,A20,AB1,AB2,AB3,AB4,AB5,AB6,AB7,AB8,
 AB9,AB10,AB11,AB12,AB13,AB14,AB15,AB16,AB17,AB18,AB19,AB20,DB1,DB2,DB3,DB4,DB5,DB6,DB7,DB8,DB9,DB10,DB11,DB12,
 DB13,DB14,DB15,DB16,DB17,DB18,DB19,DB20,OTAmt,GrossSalary,LoanDeduction,AirTicekt_Addition,
AL_Settled_Deduction,AdvanceSal_Settlement_Deduction,TripAllowance_Addition,TotalAddition,TotalDeduction,NetSalaryB4_Round,Roundoff,NetSalary
)
SELECT T0."Code", T0."U_empID", T0."U_ExtEmpNo" AS "EmpCode",
T0."U_firstNam" || ' ' || T0."U_lastName" AS "EmpName", T0."U_position" AS "Designation",
T0."U_dept" AS "DeptCode", T1."Name" AS "Department", CAST(IFNULL(T0."U_paymode", '') AS varchar(20)) AS "PayMode",
DAYS_BETWEEN( :fromdate, :todate) AS "TotalDays",
CAST(0.00 AS decimal(30, 2)) AS "WorkedDays", CAST(0.00 AS decimal(30, 2)) AS "LopDays",
CAST(0.00 AS decimal(30, 2)) AS "PHDays", CAST(0.00 AS decimal(30, 2)) AS "LveDays",
CAST(0.00 AS decimal(30, 2)) AS "WODays", CAST(0.00 AS decimal(30, 2)) AS "PayableDays",
CAST(0.00 AS decimal(30, 2)) AS "OTHrs", CAST(0.00 AS decimal(30, 2)) AS "OTDays", 
CAST(0.00 AS decimal(30, 6)) AS "TotalOT", CAST(0.00 AS decimal(30, 6)) AS "TotalOT_Perhour", 
CAST(0.00 AS decimal(30, 6)) AS "TotalBasic", CAST(0.00 AS decimal(30, 6)) AS "TotalSalary",
CAST(0.00 AS decimal(30, 2)) AS "A1", CAST(0.00 AS decimal(30, 2)) AS "A2",
CAST(0.00 AS decimal(30, 2)) AS "A3", CAST(0.00 AS decimal(30, 2)) AS "A4", CAST(0.00 AS decimal(30, 2)) AS "A5",
CAST(0.00 AS decimal(30, 2)) AS "A6", CAST(0.00 AS decimal(30, 2)) AS "A7", CAST(0.00 AS decimal(30, 2)) AS "A8",
CAST(0.00 AS decimal(30, 2)) AS "A9", CAST(0.00 AS decimal(30, 2)) AS "A10", CAST(0.00 AS decimal(30, 2)) AS "A11",
CAST(0.00 AS decimal(30, 2)) AS "A12", CAST(0.00 AS decimal(30, 2)) AS "A13", CAST(0.00 AS decimal(30, 2)) AS "A14", 
 CAST(0.00 AS decimal(30, 2)) AS "A15", CAST(0.00 AS decimal(30, 2)) AS "A16", CAST(0.00 AS decimal(30, 2)) AS "A17", 
CAST(0.00 AS decimal(30, 2)) AS "A18", CAST(0.00 AS decimal(30, 2)) AS "A19", CAST(0.00 AS decimal(30, 2)) AS "A20", 
CAST(0.00 AS decimal(30, 2)) AS "AB1", CAST(0.00 AS decimal(30, 2)) AS "AB2", CAST(0.00 AS decimal(30, 2)) AS "AB3", 
CAST(0.00 AS decimal(30, 2)) AS "AB4", CAST(0.00 AS decimal(30, 2)) AS "AB5", CAST(0.00 AS decimal(30, 2)) AS "AB6",
CAST(0.00 AS decimal(30, 2)) AS "AB7", CAST(0.00 AS decimal(30, 2)) AS "AB8", CAST(0.00 AS decimal(30, 2)) AS "AB9",
CAST(0.00 AS decimal(30, 2)) AS "AB10", CAST(0.00 AS decimal(30, 2)) AS "AB11", CAST(0.00 AS decimal(30, 2)) AS "AB12",
CAST(0.00 AS decimal(30, 2)) AS "AB13", CAST(0.00 AS decimal(30, 2)) AS "AB14", CAST(0.00 AS decimal(30, 2)) AS "AB15",
CAST(0.00 AS decimal(30, 2)) AS "AB16", CAST(0.00 AS decimal(30, 2)) AS "AB17", CAST(0.00 AS decimal(30, 2)) AS "AB18",
CAST(0.00 AS decimal(30, 2)) AS "AB19", CAST(0.00 AS decimal(30, 2)) AS "AB20", CAST(0.00 AS decimal(30, 2)) AS "DB1", 
CAST(0.00 AS decimal(30, 2)) AS "DB2", CAST(0.00 AS decimal(30, 2)) AS "DB3", CAST(0.00 AS decimal(30, 2)) AS "DB4",
CAST(0.00 AS decimal(30, 2)) AS "DB5", CAST(0.00 AS decimal(30, 2)) AS "DB6", CAST(0.00 AS decimal(30, 2)) AS "DB7",
CAST(0.00 AS decimal(30, 2)) AS "DB8", CAST(0.00 AS decimal(30, 2)) AS "DB9", CAST(0.00 AS decimal(30, 2)) AS "DB10",
CAST(0.00 AS decimal(30, 2)) AS "DB11", CAST(0.00 AS decimal(30, 2)) AS "DB12", CAST(0.00 AS decimal(30, 2)) AS "DB13",
 CAST(0.00 AS decimal(30, 2)) AS "DB14", CAST(0.00 AS decimal(30, 2)) AS "DB15", CAST(0.00 AS decimal(30, 2)) AS "DB16", CAST(0.00 AS decimal(30, 2)) AS "DB17", CAST(0.00 AS decimal(30, 2)) AS "DB18", CAST(0.00 AS decimal(30, 2)) AS "DB19",
 CAST(0.00 AS decimal(30, 2)) AS "DB20", CAST(0.00 AS decimal(30, 6)) AS "OTAmt", CAST(0.00 AS decimal(30, 6)) AS "GrossSalary", CAST(0.00 AS decimal(30, 6)) AS "LoanDeduction", CAST(0.00 AS decimal(30, 6)) AS "AirTicekt_Addition", CAST(0.00 AS decimal(30, 2)) AS "AL_Settled_Deduction",
 CAST(0.00 AS decimal(30, 2)) AS "AdvanceSal_Settlement_Deduction", CAST(0.00 AS decimal(30, 2)) AS "TripAllowance_Addition",
 CAST(0.00 AS decimal(30, 6)) AS "TotalAddition", CAST(0.00 AS decimal(30, 6)) AS "TotalDeduction", 
 CAST(0.00 AS decimal(30, 6)) AS "NetSalaryB4_Round", CAST(0.00 AS decimal(30, 6)) AS "Roundoff", CAST(0.00 AS decimal(30, 6)) AS "NetSalary" 
FROM "@SMPR_OHEM" T0 LEFT OUTER JOIN OUDP T1 ON T0."U_dept" = T1."Code" 
WHERE (T0."U_status" = :empstatus OR :empstatus = '-1') AND (:location LIKE '%#' || T0."U_location" || '#%' OR :location = '-1') 
 AND T0."U_startdte" <= :todate AND 
T0."U_empID" NOT IN (SELECT T1."U_empID" FROM "@SMPR_OPRC" T0 INNER JOIN "@SMPR_PRC1" T1 ON T0."DocEntry" = T1."DocEntry" WHERE T0."U_FromDate" = :fromdate AND T0."DocEntry" <> :docentry);



select * from #mytemp;

--Attendance Details Fetching
UPDATE T0 SET T0.PayableDays = T1."TWdays", 
T0.WorkedDays = T1."Wdays", T0.LopDays = T1."LOPdays", T0.PHDays = T1."PHdays",
T0.LveDays = T1."LveDays", T0.WODays = T1."WOdays", T0.OTHrs = T1."TotalOT" 
FROM #mytemp T0 
INNER JOIN (SELECT T1."U_empID", SUM(T1."U_OTHrs") AS "TotalOT", 
SUM((CASE WHEN IFNULL(T1."U_AttStatus", '') IN ('LP')
THEN (CASE WHEN IFNULL(T1."U_Halfday", '') = 'Y' THEN 0.50 ELSE 1.00 END) ELSE 0.00 END) + 
(CASE WHEN IFNULL(T1."U_Halfday", '') = 'Y' AND IFNULL("U_HalfStatus", '') = 'LP' THEN 0.50 ELSE 0.00 END))
 AS "LOPdays", SUM(CASE WHEN IFNULL(T1."U_AttStatus", '') IN ('PH') THEN 
(CASE WHEN IFNULL(T1."U_Halfday", '') = 'Y' THEN 0.50 ELSE 1.00 END) ELSE 0.00 END) AS "PHdays", 
SUM(CASE WHEN IFNULL(T1."U_AttStatus", '') IN ('WO') THEN (CASE WHEN IFNULL(T1."U_Halfday", '') = 'Y'
THEN 0.50 ELSE 1.00 END) ELSE 0.00 END) AS "WOdays", SUM(CASE WHEN IFNULL(T1."U_AttStatus", '')
 NOT IN ('PS','WO','PH','LP') THEN (CASE WHEN IFNULL(T1."U_Halfday", '') = 'Y' THEN 0.50 ELSE 1.00 END) 
 ELSE 0.00 END) AS "LveDays", SUM((CASE WHEN IFNULL(T1."U_AttStatus", '') IN ('PS') THEN 
(CASE WHEN IFNULL(T1."U_Halfday", '') = 'Y' THEN 0.50 ELSE 1.00 END) ELSE 0.00 END) + 
(CASE WHEN IFNULL(T1."U_Halfday", '') = 'Y' 
 AND IFNULL("U_HalfStatus", '') = 'PS' THEN 0.50 ELSE 0.00 END)) AS "Wdays",
 SUM((CASE WHEN IFNULL(T1."U_Halfday", '') = 'Y' AND IFNULL("U_HalfStatus", '') = 'PS' 
 THEN 0.50 ELSE 0.00 END) + (CASE WHEN IFNULL(T2."U_Payable", '') = 'N' THEN 0.00 ELSE 
(CASE WHEN IFNULL(T1."U_Halfday", '') = 'Y' THEN 0.50 ELSE 1.00 END) END)) AS "TWdays"     
 FROM "@SMPR_ODAS" T0 INNER JOIN "@SMPR_DAS1" T1 ON T0."DocEntry" = T1."DocEntry"
LEFT OUTER JOIN "@SMPR_OLVE" T2 ON T2."Code" = T1."U_AttStatus" 
WHERE T1."U_empID" IS NOT NULL AND CAST(T0."U_AttdDate" AS date) 
BETWEEN :fromdate AND :todate GROUP BY T1."U_empID") AS T1 ON T1."U_empID" = T0."U_EMPID";

UPDATE T0 SET T0.TotalSalary = T1."TotalSalary", T0.TotalBasic = T1."Basic",
 T0.TotalOT = T1."OTAmount" * 1.5  
 FROM #mytemp T0 
 INNER JOIN (SELECT "Code", SUM("U_Amount") AS "TotalSalary",
  SUM(CASE WHEN "U_PayElCod" = 'Basic' THEN "U_Amount" ELSE 0 END) AS "Basic",
   SUM(CASE WHEN IFNULL(U_OT, '') = 'Y' THEN "U_Amount" ELSE 0 END) AS "OTAmount" 
   FROM "@SMPR_HEM1" GROUP BY "Code") AS T1 ON T0.Code = T1."Code";
   
UPDATE #mytemp SET OTdays = Floor(OTHrs) + (((OTHrs - Floor(OTHrs)) / 60) * 100), TotalOT_Perhour = ((TotalOT * 12 / 365) / 8);

--OT & Gross Salary Calculation
UPDATE #mytemp SET OTAmt = Round((TotalOT_Perhour * OTdays), 2), GrossSalary = Round(((PayableDays * (TotalSalary / TotalDays)) + (TotalOT_Perhour * OTdays)), 2), TotalAddition = 0.00,
 TotalDeduction = 0.00, A1 = Round((PayableDays * (A1 / TotalDays)), 2), A2 = Round((PayableDays * (A2 / TotalDays)), 2), A3 = Round((PayableDays * (A3 / TotalDays)), 2), A4 = Round((PayableDays * (A4 / TotalDays)), 2), A5 = Round((PayableDays * (A5 / TotalDays)), 2), A6 = Round((PayableDays * (A6 / TotalDays)), 2), A7 = Round((PayableDays * (A7 / TotalDays)), 2), A8 = Round((PayableDays * (A8 / TotalDays)), 2), A9 = Round((PayableDays * (A9 / TotalDays)), 2), A10 = Round((PayableDays * (A10 / TotalDays)), 2), A11 = Round((PayableDays * (A11 / TotalDays)), 2), A12 = Round((PayableDays * (A12 / TotalDays)), 2), A13 = Round((PayableDays * (A13 / TotalDays)), 2), A14 = Round((PayableDays * (A14 / TotalDays)), 2), A15 = Round((PayableDays * (A15 / TotalDays)), 2), A16 = Round((PayableDays * (A16 / TotalDays)), 2), A17 = Round((PayableDays * (A17 / TotalDays)), 2), A18 = Round((PayableDays * (A18 / TotalDays)), 2), A19 = Round((PayableDays * (A19 / TotalDays)), 2), A20 = Round((PayableDays * (A20 / TotalDays)), 2);

UPDATE T0 SET T0.AirTicekt_Addition = T1."AirTicket",
 T0.TotalAddition = IFNULL(T0.TotalAddition, 0) + IFNULL(T1."AirTicket", 0)
 FROM #mytemp T0 
 INNER JOIN (SELECT "U_empID", SUM("U_Total") AS "AirTicket" 
 FROM "@SMPR_OTIS" WHERE IFNULL("U_Approved", '') = 'Y' AND "Canceled" <> 'Y' 
 AND IFNULL("Status", 'O') = 'O' 
AND IFNULL("U_payroll", '') = 'Y' 
AND "U_DocDate" BETWEEN :fromdate AND :todate GROUP BY "U_empID") AS T1 ON T0.U_empID = T1."U_empID";

UPDATE T0 SET T0.TotalAddition = IFNULL(T0.TotalAddition, 0) + IFNULL(AB1, 0) + IFNULL(AB2, 0) + IFNULL(AB3, 0) + IFNULL(AB4, 0) + IFNULL(AB5, 0) + IFNULL(AB6, 0) + IFNULL(AB7, 0) + IFNULL(AB8, 0) + IFNULL(AB9, 0) + IFNULL(AB10, 0) + IFNULL(AB11, 0) + IFNULL(AB12, 0) + IFNULL(AB13, 0) + IFNULL(AB14, 0) + IFNULL(AB15, 0) + IFNULL(AB16, 0) + IFNULL(AB17, 0) + IFNULL(AB18, 0) + IFNULL(AB19, 0) + IFNULL(AB20, 0), T0.TotalDeduction = IFNULL(DB1, 0) + IFNULL(DB2, 0) + IFNULL(DB3, 0) + IFNULL(DB4, 0) + IFNULL(DB5, 0) + IFNULL(DB6, 0) + IFNULL(DB7, 0) + IFNULL(DB8, 0) + IFNULL(DB9, 0) + IFNULL(DB10, 0) + IFNULL(DB11, 0) + IFNULL(DB12, 0) + IFNULL(DB13, 0) + IFNULL(DB14, 0) + IFNULL(DB15, 0) + IFNULL(DB16, 0) + IFNULL(DB17, 0) + IFNULL(DB18, 0) + IFNULL(DB19, 0) + IFNULL(DB20, 0)
 FROM #mytemp T0;
 
 select * from #mytemp;

drop table #mytemp;  

End;




--[Innova_HRMS_PayrollProcess]'#3#4#5#12#','FY2018-09',1,58