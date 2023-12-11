-------Procedure for AirticketIssue--------------------------------------------------------------------
CREATE PROCEDURE "MIPL_HR_GetEmpDetails_AirticketIssue" (IN trzid varchar(100), IN issuedate varchar(100)) 
AS lastclaimdate date;
lastclaimamt decimal(30, 6);
lopdays integer;
BEGIN 
SELECT IFNULL(T1."U_TickDate", IFNULL(T0."U_airlstdt", T0."U_startdte")), "U_Total" INTO lastclaimdate, lastclaimamt FROM "@SMPR_OHEM" T0
 LEFT OUTER JOIN "@SMPR_OTIS" T1 ON T0."U_empID" = T1."U_empID" AND T1."DocEntry" = (SELECT MAX("DocEntry") AS "docentry" FROM "@SMPR_OTIS" 
 WHERE "U_IDNo" = :trzid AND "Canceled" <> 'Y');

SELECT (SELECT COUNT(1) FROM "@SMPR_ODAS" T0 INNER JOIN "@SMPR_DAS1" T1 ON T0."DocEntry" = T1."DocEntry" WHERE T0."U_AttdDate" 
BETWEEN :lastclaimdate AND :issuedate AND T1."U_IDNo" = :trzid AND IFNULL(T1."U_AttStatus", '') = 'LP') INTO lopdays FROM DUMMY;

SELECT "U_empID", "U_firstNam" || ' ' || "U_lastName" AS "Name", T1."Name" AS "Department", T2."name" AS "Designation", T5."Descr" AS "Emptype",
 T6."Name" AS "Country", TO_NVARCHAR(T0."U_startdte", 'dd/MM/yy') AS "JoiningDate", TO_NVARCHAR(:lastclaimdate, 'dd/MM/yy') AS "Lastdate",
  CAST(IFNULL(:lastclaimamt, 0) AS decimal(30, 2)) AS "Lastclaimamt", IFNULL(T4."U_eligiamt", 0) AS "Eligibleamt", IFNULL(T4."U_tcktpryr", 0) AS "TcktPeryear", 

  DAYS_BETWEEN(:lastclaimdate,:issuedate) AS "TotalDays", :lopdays AS "LOPDays", 
  DAYS_BETWEEN(:lastclaimdate, :issuedate) - :lopdays AS "noofday", 
  Round((IFNULL(T4."U_eligiamt", 0) / (365 * IFNULL(T4."U_tcktpryr", 1))) * (DAYS_BETWEEN(:lastclaimdate,:issuedate))
   - (CASE WHEN :lopdays < 15 THEN 0 ELSE :Lopdays END), 2) AS "TicketAmount",

    TO_NVARCHAR(:issuedate, 'dd/MM/yy') AS "IssueDate", IFNULL(T4."U_nooftckt", 0) AS "nooftckt" 
    
    FROM "@SMPR_OHEM" T0 INNER JOIN 
    OUDP T1 ON T0."U_dept" = T1."Code" INNER JOIN OHPS T2 ON T2."posID" = T0."U_position" LEFT OUTER JOIN "@SMPR_HEM10" T4 ON T4."Code" = T0."Code" 
    AND :issuedate BETWEEN T4."U_fromdate" AND IFNULL(T4."U_todate",:issuedate) LEFT OUTER JOIN (SELECT "FldValue", "Descr" FROM UFD1 WHERE "TableID" = '@smpr_ohem' 
    AND "FieldID" IN (SELECT "FieldID" FROM CUFD WHERE "TableID" = '@smpr_ohem' AND "AliasID" = 'gropCode')) AS T5 ON T5."FldValue" = T0."U_gropCode" LEFT OUTER JOIN 
    OCRY T6 ON T6."Code" = T0."U_ncountry" WHERE "U_ExtEmpNo" = :trzid;
END;



-------Leave/FInal Settlement EMployee Details FIlling--------------------------------------------------------------------
CREATE PROCEDURE "MIPL_HR_Airticket_History" (IN empid varchar(100), IN HistoryTYpe varchar(100)) 
AS BEGIN 
IF :HistoryTYpe = 'OITS' THEN
 SELECT T0."DocEntry", T0."DocNum", T0."Object" AS "objtype", "U_IDNo" AS "Empid", "U_empName" AS "Employee Name", "U_TickDate" AS "Ticket Issued Date",
  "U_noofday" AS "Claim Days", "U_Total" AS "Claimed Amount", T0."U_nooftckt" AS "Eligible No of Ticket", "U_tcktpryr" AS "Eligible Ticket Per Year", 
  "U_eligiamt" AS "Eligible Amount" FROM "@SMPR_OTIS" T0 WHERE T0."U_empID" = :empid;
END IF;
IF :HistoryTYpe = 'OHEM' THEN
 SELECT T0."U_ExtEmpNo" AS "Empid", "U_firstNam" || ' ' || "U_lastName" AS "Employee Name", T1."U_fromdate" AS "FromDate", T1."U_todate" AS "ToDate", 
 T1."U_nooftckt" AS "Eligible No of Ticket", T1."U_tcktpryr" AS "Eligible Ticket By Year", T1."U_eligiamt" AS "Eligible Amount", 'OHEM' AS "objtype" 
 FROM "@SMPR_OHEM" T0 LEFT OUTER JOIN "@SMPR_HEM10" T1 ON T0."Code" = T1."Code" WHERE T0."U_empID" = :empid;
END IF;
END;
