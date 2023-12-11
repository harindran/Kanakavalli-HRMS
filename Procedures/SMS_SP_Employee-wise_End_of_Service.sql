CREATE PROCEDURE "@SMS_SP_Employee-wise_End_of_Service" (IN asonDate date)
 AS LeaveCalculationStartdate date;
BEGIN  
LeaveCalculationStartdate := '20170101';

WITH "salarydetails" AS (SELECT "Code", SUM(CASE WHEN "U_PayElCod" = 'Basic' THEN "U_Amount" ELSE 0 END) AS "Basic", 
SUM(CASE WHEN "U_PayElCod" <> 'Basic' THEN "U_Amount" ELSE 0 END) AS "Allowance" FROM "@SMPR_HEM1" GROUP BY "Code"), 


"employee_workingdays" AS (SELECT T0."U_empID" AS "empID", CAST(IFNULL("U_termdate", :asondate ) AS date) AS "Termination Date",
CAST("U_startdte" AS date) AS "Joining Dt", CAST(:asondate  AS date) AS "Report Dt", 
IFNULL(DAYS_BETWEEN(T0."U_startdte", :asondate ), 1) AS "Total Working Days", 
Round((CAST((IFNULL(DAYS_BETWEEN(T0."U_startdte",:asondate ), 1)) AS float) / CAST(30.4167 AS float) / 
CAST(12 AS float)), 6) AS "Year" FROM "@SMPR_OHEM" T0),


"GradutiyDetails" AS (SELECT "empID", "Joining Dt", "Report Dt", "Total Working Days", "Year" AS "Gratuity Years", 
CASE WHEN T0."Year" < 5 THEN CAST(T0."Year" AS integer) ELSE 5 END AS "Grad @21 (Years)", CASE WHEN T0."Year" < 5 THEN (T0."Year" - CAST(T0."Year" AS integer)) 
ELSE 0 END AS "Grad @21 (Days)", CASE WHEN T0."Year" > 5 THEN CAST(T0."Year" AS integer) - 5 ELSE 0 END AS "Grad @30 (Years)", CASE WHEN T0."Year" > 5 
THEN (T0."Year" - CAST(T0."Year" AS integer)) ELSE 0 END AS "Grad @30 (Days)" FROM "employee_workingdays" T0), 


"LOP_airticket" AS (SELECT A."U_empID", COUNT(*) AS "LOP" FROM "@SMPR_DAS1" A INNER JOIN "@SMPR_ODAS" B ON A."DocEntry" = B."DocEntry" 
INNER JOIN "@SMPR_OHEM" C ON C."U_empID" = A."U_empID" WHERE A."U_AttStatus" IN ('LP') AND B."U_AttdDate" BETWEEN IFNULL(C."U_airlstdt", C."U_startdte") 
AND :asondate  GROUP BY A."U_empID"),


"AirTicketIssued_MaxDate" AS (SELECT "U_empID", MAX("U_TickDate") AS "ClaimedDate" FROM "@SMPR_OTIS" WHERE IFNULL("U_Approved", '') = 'Y' 
AND IFNULL("Canceled", '') <> 'Y' AND "U_DocDate" <= :asondate  GROUP BY "U_empID"),


"AirTicket_LastClaimDate" AS (SELECT T0."U_empID", IFNULL(T1."ClaimedDate", IFNULL(T0."U_airlstdt", T0."U_startdte")) AS "LastClaimDate" FROM "@SMPR_OHEM" 
T0 LEFT OUTER JOIN "AirTicketIssued_MaxDate" T1 ON T0."U_empID" = T1."U_empID"), 


"AirticketDetails" AS (SELECT T0."U_empID" AS "EMPID", T0."U_ExtEmpNo", IFNULL(T1."U_tcktpryr", 0) AS "Eligible Airticket Years", 
IFNULL(T1."U_eligiamt", 0) AS "Eligible Air Ticket", T0."U_startdte" AS "U_startdte", T2."LastClaimDate" AS "Last AirTicket Booked Date", 1 +
DAYS_BETWEEN(T2."LastClaimDate",:asondate ) - IFNULL(T4.LOP, 0) AS "Current Year Days", 
(CASE WHEN IFNULL(T1."U_tcktpryr", 1) = 0 THEN 0 ELSE Round((1 + DAYS_BETWEEN(T2."LastClaimDate",:asondate ) - IFNULL(T4.LOP, 0)) * 
(IFNULL(T1."U_eligiamt", 0) / (IFNULL(T1."U_tcktpryr", 1) * 365)), 0) END) AS "Accrued Air Ticket_CurrentPeriod" FROM "@SMPR_OHEM" T0 
LEFT OUTER JOIN "@SMPR_HEM10" T1 ON T0."Code" = T1."Code" AND :asondate  BETWEEN T1."U_fromdate" AND IFNULL(T1."U_todate", :asondate )
LEFT OUTER JOIN "AirTicket_LastClaimDate" T2 ON T2."U_empID" = T0."U_empID" LEFT OUTER JOIN (SELECT "U_empID", (CASE WHEN LOP <= 30 THEN 0 ELSE LOP END) 
AS "LOP" FROM "LOP_airticket") AS T4 ON T4."U_empID" = T0."U_empID"), 


"leavetaken" AS (SELECT A."U_empID", COUNT(*) AS "LeaveTaken", SUM(CASE WHEN B."U_AttdDate" >= '20180101' THEN 0 ELSE 1 END) AS "AL_TOBE_Deducted" 
FROM "@SMPR_DAS1" A INNER JOIN "@SMPR_ODAS" B ON A."DocEntry" = B."DocEntry" WHERE A."U_AttStatus" IN ('AL') AND B."U_AttdDate" BETWEEN 
:LeaveCalculationStartdate AND :asondate  GROUP BY A."U_empID"),


"LOP" AS (SELECT A."U_empID", COUNT(*) AS "LOP" FROM "@SMPR_DAS1" A INNER JOIN "@SMPR_ODAS" B ON A."DocEntry" = B."DocEntry" WHERE A."U_AttStatus" IN ('LP') 
AND B."U_AttdDate" BETWEEN :LeaveCalculationStartdate AND :asondate  GROUP BY A."U_empID"),


"Leave_encash" AS (SELECT T0."U_EmpID" AS "EMpid", 0 AS "encashdays_OB", SUM((CASE WHEN T0."U_LveSettDate" BETWEEN :LeaveCalculationStartdate AND :asondate  
THEN IFNULL(T0."U_lvncshdy", 0) ELSE 0 END)) AS "encashdays" FROM "@SMPR_OLSE" t0 WHERE T0."U_approved" = 'Y' AND T0."U_LveSettDate" <= :asondate  GROUP BY T0."U_EmpID") ,




"AnnualLeave" AS (SELECT DISTINCT "U_empID", 'AL' AS "Type", T1."U_DOJAfterLveDate" AS "AL_OBDate", T1."U_DOJAfterLveBal" AS "AL_OBDays" FROM "@SMPR_OHEM" T0 
INNER JOIN "@SMPR_HEM2" T1 ON T0."Code" = T1."Code" WHERE T1."U_LveCode" = 'AL'),



 "LeaveCalculation" AS (SELECT T0."U_empID", T0."U_ExtEmpNo", t1."Location", IFNULL(T5."AL_OBDays", 0) AS "OBDAYS_INITIAL", IFNULL(T5."AL_OBDate", T0."U_startdte") 
 AS "LEAVESTARTDATE", 
 ((CASE WHEN IFNULL(T5."AL_OBDate", '20000101') > :LeaveCalculationStartdate THEN 0 ELSE IFNULL(T5."AL_OBDays", 0) END) - 
 IFNULL(T4."encashdays_OB", 0) + 
 ROUND((
 (CASE WHEN IFNULL(T5."AL_OBDate", T0."U_startdte") < :LeaveCalculationStartdate THEN  DAYS_BETWEEN(IFNULL(T5."AL_OBDate", T0."U_startdte"),:LeaveCalculationStartdate) ELSE 0 END)
 - (SELECT COUNT(*) FROM "@SMPR_DAS1" A  INNER JOIN "@SMPR_ODAS" B ON A."DocEntry" = B."DocEntry" WHERE A."U_empID" = T0."U_empID" AND A."U_AttStatus" IN ('LP','AL') AND B."U_AttdDate" 
 BETWEEN IFNULL(T5."AL_OBDate", T0."U_startdte") AND ADD_DAYS(:LeaveCalculationStartdate,-1))) * 
 (30.00 / 365.00), 2) - (SELECT COUNT(*) FROM "@SMPR_DAS1" A INNER JOIN "@SMPR_ODAS" B ON A."DocEntry" = B."DocEntry" WHERE A."U_empID" = T0."U_empID" AND 
 A."U_AttStatus" IN ('AL') AND B."U_AttdDate" BETWEEN IFNULL(T5."AL_OBDate", T0."U_startdte") 
 AND ADD_DAYS(:LeaveCalculationStartdate,-1))) AS "OB", 
  (CASE WHEN IFNULL(T5."Type", '') = 'AL'  THEN ROUND((( (DAYS_BETWEEN((CASE WHEN :LeaveCalculationStartdate > IFNULL(T5."AL_OBDate", T0."U_startdte") THEN :LeaveCalculationStartdate ELSE IFNULL(T5."AL_OBDate", T0."U_startdte") END), :asondate ) + 1) 
 -   IFNULL(T2."AL_TOBE_Deducted", 0) - IFNULL(T3."LOP", 0)) * (30.00 / 365.00)), 2) ELSE 0 END) AS "LEAVE ACCRUED"
  , IFNULL(T2."LeaveTaken", 0) AS "LEAVE TAKEN",  IFNULL(T3."LOP", 0) AS "LOP", IFNULL(T4."encashdays", 0) AS "encashdays" FROM "@SMPR_OHEM" T0 
  LEFT OUTER JOIN "OLCT" t1 ON t0."U_location" = t1."Code" 
  LEFT OUTER JOIN "leavetaken" T2 ON T2."U_empID" = T0."U_empID" LEFT OUTER JOIN LOP T3 ON T3."U_empID" = T0."U_empID" LEFT OUTER JOIN "Leave_encash" T4 
  ON T4."EMpid" = T0."U_empID" INNER JOIN "AnnualLeave" T5 ON T5."U_empID" = T0."U_empID" WHERE t0."U_location" IN (4,3,5)   AND IFNULL(T5."AL_OBDate", T0."U_startdte") < :asondate ) 
 


 
 SELECT T0."U_empID" AS "SAP ID", T0."U_ExtEmpNo" AS "Innova Employee ID", Replace(IFNULL("U_firstNam", '') || ' ' || IFNULL("U_lastName", ''), '  ', ' ') AS "Emp Name", 
 IFNULL(T0."U_visaspon", '') AS "Visa Sponsor", T6."Location", T3."Joining Dt", T3."Report Dt", T3."Total Working Days", T3."Gratuity Years", T3."Grad @21 (Days)", 
 T3."Grad @21 (Years)", T3."Grad @30 (Days)", T3."Grad @30 (Years)", Round(IFNULL((((CAST(T2."Basic" AS float) * CAST(12 AS float)) / CAST(365 AS float)) * 21 * (T3."Grad @21 (Years)" +
  T3."Grad @21 (Days)")) + (((CAST(T2."Basic" AS float) * CAST(12 AS float)) / CAST(365 AS float)) * 30 * (T3."Grad @30 (Years)" + T3."Grad @30 (Days)")), 0), 6) AS "Gratuity Amount", 
  IFNULL(T2."Basic", 0) AS "Basic", IFNULL(T2."Allowance", 0) AS "Allowance", IFNULL(T2."Basic", 0) + IFNULL(T2."Allowance", 0) AS "Gross Salary", T4."Eligible Airticket Years", 
  T4."Eligible Air Ticket", T4."Last AirTicket Booked Date", T4."Current Year Days", T4."Accrued Air Ticket_CurrentPeriod" AS "Accrued Air Ticket_CurrentPeriod", 
  IFNULL(T5.OB, 0.00) AS "Leave Opening Balance", IFNULL(T5."LEAVE ACCRUED", 0.00) AS "LEAVE ACCRUED", IFNULL(T5."LEAVE TAKEN", 0.00) AS "LEAVE TAKEN", 
  IFNULL(T5."encashdays", 0.00) AS "Leave Encashed Days", IFNULL(T5.LOP, 0.00) AS "Leave Loss of Pay", IFNULL((T5.OB + T5."LEAVE ACCRUED" - T5."LEAVE TAKEN" - T5."encashdays"), 0.00)
   AS "Leave Balance Days", IFNULL(Round(((T5.OB + T5."LEAVE ACCRUED" - T5."LEAVE TAKEN" - T5."encashdays") * ((IFNULL(T2."Basic", 0) + IFNULL(T2."Allowance", 0)) / 30)), 2), 0.00) 
   AS "Leave Balance Amount" FROM "@SMPR_OHEM" T0 LEFT OUTER JOIN "salarydetails" T2 ON T2."Code" = T0."U_empID" LEFT OUTER JOIN "GradutiyDetails" T3 ON T3."empID" = T0."U_empID"
    LEFT OUTER JOIN "AirticketDetails" T4 ON T4."EMPID" = T0."U_empID" LEFT OUTER JOIN "LeaveCalculation" T5 ON T5."U_empID" = T0."U_empID" INNER JOIN olct t6 ON t0."U_location" = t6."Code"
     LEFT OUTER JOIN (SELECT "FldValue", "Descr" FROM ufd1 WHERE "TableID" = '@SMPR_OHEM' AND "FieldID" = '4') AS T7 ON T0."U_gropCode" = T7."FldValue" WHERE t0."U_location" IN (3,4,5) AND 
     T3."Joining Dt" <= :asondate  AND IFNULL(T0."U_termdate", ADD_DAYS( :asondate ,1)) > :asondate  ORDER BY T0."U_ExtEmpNo" ;
     

END;


