create PROCEDURE "MIPL_HRMS_LeaveApplicaiton_History" (
IN empid varchar(100), 
IN leavetype varchar(10), 
IN Docetnry varchar(100)) 
LANGUAGE SQLSCRIPT

AS 
/*
BEGIN
Declare temp_var_0 varchar(10);
SELECT (select count(*) from  "PROCEDURES"
 where procedure_name = 'Innova_HRMS_LeaveApplicaiton_History') INTO temp_var_0 FROM DUMMY;
IF :temp_var_0 > 0 THEN exec 'DROP PROCEDURE Innova_HRMS_LeaveApplicaiton_History';
END IF;
END;
*/
BEGIN 
IF :Docetnry <> '-1' THEN 
SELECT "DocEntry", "DocNum", To_varchar("U_DocDate")  AS "Posting date",
	(CASE WHEN "Canceled" = 'Y' THEN 'Cancelled' ELSE 
	(CASE WHEN "Status" = 'C' THEN 'Close' WHEN "Status" = 'D' 
	THEN 'Waiting for Approval' WHEN "Status" = 'O' 
	THEN 'Open' ELSE "Status" END) END) AS "Status", 
	To_varchar("U_FromDate") AS "From Date",
	To_varchar("U_Todate") AS "To Date", 
	To_decimal("U_NoDayLve", 30, 2) AS "Total days", 
	To_decimal("U_EliLvDay" ,30, 2) AS "Eligible days", 
	To_decimal("U_BalLeave" ,30, 2) AS "Balance days", 
	(CASE WHEN "U_HalfDay" = 'Y' THEN 'Yes' ELSE 'No' END) AS "HalfDay", 
	IFNULL("U_rempid", '') AS "Replacement ID", 
	IFNULL("U_rempname", '') AS "Replacement Name", 
	IFNULL("U_Reason", '') AS "Reason" FROM "@SMPR_OLVA" 
WHERE "U_empID" = :empid AND "U_LveCode" = :leavetype 
	AND "DocEntry" < :Docetnry ORDER BY "U_FromDate" DESC;
ELSE 

SELECT T0."DocEntry", T0."DocNum", T1."Name" AS "Leave Type", 
	To_varchar(T0."U_DocDate") AS "Posting date", 
	(CASE WHEN T0."Canceled" = 'Y' THEN 'Cancelled' ELSE 
	(CASE WHEN "Status" = 'C' THEN 'Close' WHEN "Status" = 'D' 
	THEN 'Waiting for Approval' WHEN "Status" = 'O' THEN 'Open'
 	ELSE "Status" END) 
 	END) AS "Status", To_varchar(T0."U_FromDate") AS "From Date",
  	To_varchar(T0."U_Todate") AS "To Date", 
  	To_decimal(T0."U_NoDayLve" ,30, 2) AS "Total days", 
  	To_decimal(T0."U_EliLvDay", 30, 2) AS "Eligible days", 
  	To_decimal(T0."U_BalLeave", 30, 2) AS "Balance days", 
  	(CASE WHEN T0."U_HalfDay" = 'Y' THEN 'Yes' ELSE 'No' END) AS "HalfDay", 
  	IFNULL(T0."U_rempid", '') AS "Replacement ID", 
  	IFNULL(T0."U_rempname", '') AS "Replacement Name", 
  	IFNULL(T0."U_Reason", '') AS "Reason", T0."U_IDNo" || ' - ' || T0."U_empName" AS "header", 
  	T0."Object" AS "objtype" FROM "@SMPR_OLVA" T0 INNER JOIN "@SMPR_OLVE" T1 ON T0."U_LveCode" = T1."Code" 
  WHERE "U_empID" = :empid ORDER BY T1."Name", "U_FromDate" DESC;
END IF;
END;



-------Procedure for Leave Application Balance Leave Details--------------------------------------------------------------------
Create PROCEDURE "MIPL_HRMS_LeaveApplication_Balance" (IN todate date, IN empid varchar(100), IN leavetype varchar(10)) AS 
OB_Leave_Days decimal(30, 3);
LeaveTaken decimal(30, 3);
Lop_Leave decimal(30, 3);
Total_Worked_Days decimal(30, 3);
Totalleave_master decimal(30, 3);
carryfwd_master decimal(30, 3);
leave_encashed decimal(30, 3);
LeaveTaken_ForAccrual decimal(30, 3);
Fromdate date;

BEGIN 
/*
SELECT IFNULL("U_MxLveFwd",0), IFNULL("U_TotalLve" ,0) INTO carryfwd_master, Totalleave_master FROM "@SMPR_OLVE" where "Code"=:leavetype;
IF :carryfwd_master = 0 THEN 
SELECT TO_DATE(left(:todate,4) || '0101','YYYYMMDD') INTO Fromdate FROM DUMMY ; 
END IF;
*/
IF :carryfwd_master <> 0 THEN SELECT IFNULL(T1."U_DOJAfterLveDate", "U_startdte"), IFNULL(T1."U_DOJAfterLveBal", 0) INTO Fromdate, OB_Leave_Days FROM 
"@SMPR_OHEM" T0 INNER JOIN "@SMPR_HEM2" T1 ON T0."Code" = T1."Code" and T1."U_LveCode"=:leavetype;
END IF;
SELECT SUM(IFNULL("U_lvncshdy", 0)) INTO leave_encashed FROM "@SMPR_OLSE";

SELECT SUM((DAYS_BETWEEN((CASE WHEN "U_FromDate" < :fromdate THEN :fromdate ELSE "U_FromDate" END), "U_Todate") + 1) 
* (CASE WHEN "U_HalfDay" = 'Y' THEN 0.5 ELSE 1 END)), SUM(CASE WHEN "U_FromDate" > '20171231' THEN 0 ELSE 
((DAYS_BETWEEN("U_FromDate",(CASE WHEN "U_Todate" > '20171231' THEN '20171231' ELSE "U_Todate" END)) + 1) * 
(CASE WHEN "U_HalfDay" = 'Y' THEN 0.5 ELSE 1 END)) END) INTO LeaveTaken, LeaveTaken_ForAccrual FROM "@SMPR_OLVA";


SELECT DAYS_BETWEEN (:fromdate,  :todate) + 1,
 SUM(CASE WHEN T1."U_AttStatus" = 'LP' THEN (CASE WHEN T1."U_Halfday" = 'Y' THEN 0.5 ELSE 1 END) ELSE 0 END) INTO Total_Worked_Days, Lop_Leave 
 FROM "@SMPR_ODAS" T0 INNER JOIN "@SMPR_DAS1" T1 ON T0."DocEntry" = T1."DocEntry";
SELECT :empid AS "Empid", :fromdate AS "FromDate", :todate AS "ToDate", IFNULL(:OB_Leave_Days, 0) AS "Leave_OB", IFNULL(:LeaveTaken, 0) AS "Leave_Taken", 
IFNULL(:leave_encashed, 0) AS "Leave_Encashed", IFNULL(:Lop_Leave, 0) AS "Leave_LOP", IFNULL(:Total_Worked_Days, 0) AS "TotalDays",
 IFNULL(:Total_Worked_Days, 0) - IFNULL(:Lop_Leave, 0) AS "Worked Days", IFNULL(:LeaveTaken_ForAccrual, 0) AS "LeaveTaken_ForApproval", 
 (CASE WHEN :carryfwd_master = 0 THEN :totalLeave_master ELSE ((IFNULL(:Total_Worked_Days, 0) - IFNULL(:LeaveTaken_ForAccrual, 0) - IFNULL(:Lop_Leave, 0)) *
  IFNULL(:Totalleave_master, 0) / 365) END) AS "Accured_Days", Round(IFNULL(:OB_Leave_Days, 0) + (CASE WHEN :carryfwd_master = 0 THEN :totalLeave_master ELSE 
  ((IFNULL(:Total_Worked_Days, 0) - IFNULL(:LeaveTaken_ForAccrual, 0) - IFNULL(:Lop_Leave, 0)) * IFNULL(:Totalleave_master, 0) / 365) END) - IFNULL(:LeaveTaken, 0) - 
  IFNULL(:leave_encashed, 0), 2) AS "Available_Leave" FROM DUMMY;

END;
