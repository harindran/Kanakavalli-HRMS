-------Table Creation for Provision--------------------------------------------------------------------------------------------------------------------------------------------------------------
If not exists (select 1 from sys.tables where name='HRMS_POSTINGLOG')
Begin
	CREATE TABLE [dbo].[HRMS_POSTINGLOG]([ID] [int] IDENTITY(1,1) NOT NULL,[Createdate] [datetime] NOT NULL DEFAULT (getdate()),[OBJTYPE] [varchar](20) NULL,[DocEntry] [varchar](100) NULL,[JENO] [varchar](100) NULL,
	[Status] [varchar](10) NULL,[Remarks] [varchar](max) NULL,PRIMARY KEY CLUSTERED ([ID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
End

Go
-------Procedure for Employee Details Update--------------------------------------------------------------------
if exists(select 1 from sys.procedures where name='Innova_HRMS_EmployeeDetailsUpdate_OHEM') Drop Procedure Innova_HRMS_EmployeeDetailsUpdate_OHEM
Go

/*CREATE PROCEDURE "Innova_HRMS_EmployeeDetailsUpdate_OHEM" AS BEGIN 
UPDATE T1 SET T1."ExtEmpNo" = T0."U_ExtEmpNo", T1."firstName" = T0."U_firstNam", T1."lastName" = T0."U_lastName", T1."U_EmpGrpCode" = T0."U_GropCode",
T1."jobTitle" = left(T0."U_jobTitle", 20), T1."position" = T0."U_position", T1."dept" = T0."U_dept", T1."branch" = T0."U_branch", T1."manager" = T0."U_manager",
T1."userId" = T0."U_userid", T1."salesPrson" = T0."U_slpcode", T1."officeTel" = T0."U_oficetel", T1."officeExt" = T0."U_oficeext", T1."mobile" = T0."U_mobile", 
T1."pager" = T0."U_pager", T1."homeTel" = T0."U_hometel", T1."fax" = left(T0."U_fax", 20), T1."email" = T0."U_email", T1."workStreet" = T0."U_wstreet", 
T1."StreetNoW" = T0."U_wstretno", T1."WorkBuild" = T0."U_wbuildng", T1."workBlock" = T0."U_wblock", T1."workZip" = T0."U_wzipcode", T1."workCity" = T0."U_wcity",
T1."workCounty" = T0."U_wcounty", T1."workCountr" = T0."U_wcountry", T1."workState" = T0."U_wtate", T1."homeStreet" = T0."U_nstreet", T1."StreetNoH" = T0."U_nstretno",
T1."HomeBuild" = T0."U_nbuildng", T1."homeBlock" = T0."U_nblock", T1."homeZip" = T0."U_nzipcode", T1."homeCity" = T0."U_ncity", T1."homeCounty" = T0."U_ncounty", 
T1."homeCountr" = T0."U_ncountry", T1."homeState" = T0."U_ntate", T1."birthDate" = T0."U_obirthDt", T1."brthCountr" = T0."U_brthcont", T1."citizenshp" = T0."U_citizen"
, T1."martStatus" = T0."U_mrstatus", T1."nChildren" = T0."U_noofchld", T1."sex" = T0."U_sex", T1."U_Bloodgrp" = T0."U_bloodgrp", T1."U_OutsourID" = T0."U_religion",
T1."U_PhotoAttach" = T0."U_photoatt", T1."passportNo" = T0."U_passpno", T1."passportEx" = T0."U_passexdt", T1."PassIssue" = T0."U_passisdt", T1."PassIssuer" = T0."U_passisur"
,T1."startDate" = T0."U_startdte", T1."status" = T0."U_status", T1."U_ProbMonth" = T0."U_probmnth", T1."U_Probdate" = T0."U_probdate", T1."U_ProbExtdate" = T0."U_probexdt", 
T1."U_ContEndDate" = T0."U_conenddt", T1."termDate" = T0."U_termdate", T1."termReason" = T0."U_termreas", T1."U_ResgDate" = T0."U_resgdate", 
T1."U_NoticePerdDays" = T0."U_noteperd", T1."U_termType" = T0."U_termtype", T1."U_PayMode" = T0."U_paymode", T1."bankCode" = T0."U_bankcode",
 T1."bankBranch" = T0."U_bankbrch", T1."bankAcount" = T0."U_bankacct", T1.U_IBAN = T0."U_bankiban", T1."U_BFirstName" = T0."U_bankfnam", T1."U_BLastName" = T0."U_banklnam", 
 T1."U_Location" = T0."U_location", T1."U_ShiftCode" = T0."U_shiftcde", T1.U_OT = T0.U_OT, T1."U_GradeCode" = T0."U_grade", T1."U_SubGrade1" = T0."U_subgrad1", 
 T1."U_SubGrade2" = T0."U_subgrad2", T1."U_FandF" = T0."U_fandf", T1."U_CampCode" = T0."U_campcode", T1."U_RoomNo" = T0."U_roomno", T1."U_Destination" = T0."U_destplac",
  T1."U_ApprovedUser" = T0."U_approved", T1."U_LoanEligible" = T0."U_loanelgi", T1."U_PPFileName" = T0."U_ppfname", T1."U_PPAttach" = T0."U_ppattach",
   T1."U_LveSettlmentOBDate" = T0."U_lvstobdt", T1."U_LveSettlmentOBDays" = T0."U_lvstobdy" FROM OHEM T1 INNER JOIN "@SMPR_OHEM" T0 ON T1."ExtEmpNo" = T0."U_ExtEmpNo";
END;*/

Go
-------Procedure for Loan APplication Posting--------------------------------------------------------------------
CREATE PROCEDURE "Innova_HRMS_Posting_LoanApplication" AS BEGIN  

WITH "empdetails" AS (SELECT T0."U_empID", T0."U_gropCode", T1."U_costcode" AS "Dept_CC", T2."U_costcode" AS "Location_CC", T0."U_ExtEmpNo"
 AS "Employee_CC", T0."U_otherscc" AS "Others_CC" FROM "@SMPR_OHEM" T0 INNER JOIN OUDP T1 ON T0."U_dept" = T1."Code" INNER JOIN OUBR T2 ON
  T2."Code" = T0."U_branch") 
  
  SELECT T0."DocEntry", T0."DocNum", T0."U_empID", T0."U_empName", T0."U_IDNo", T0."U_DocDate" AS "Date", 
  T1."Code", T1."Name", T0."U_LoanAmt" AS "Amount", T3."U_loandc" AS "DebitAccount", T3."U_loancc" AS "CreditAccount", t2."Location_CC" AS "Ocrcode1", 
  t2."Dept_CC" AS "Ocrcode2", t2."Employee_CC" AS "Ocrcode3", '' AS "Ocrcode4", t2."Others_CC" AS "Ocrcode5", 'OLOA' AS "Transcode",
   'Loan Application - ' || T0."U_IDNo" AS "Memo", left('Loan Application - Entry No : ' || CAST(T0."DocEntry" AS varchar) || ' & Appl No : ' ||
    CAST(T0."DocNum" AS varchar) || ' & Loan Type : ' || T1."Name", 250) AS "Narration", left(('Employee No : ' || T0."U_IDNo" || '  ID : ' || 
    T0."U_empID"), 99) AS "Ref1", left(T0."U_empName", 99) AS "Ref2", left(T1."Name", 26) AS "Ref3" FROM "@SMPR_OLOA" T0 INNER JOIN "@SMPR_OLON" T1 
    ON T0."U_LoanCode" = T1."Code" INNER JOIN "empdetails" T2 ON T2."U_empID" = T0."U_empID"     INNER JOIN (SELECT "U_emptype", "U_fromdate", "U_todate", 
    T1."U_loancode", T1."U_loandc", T1."U_loancc" FROM "@SMPR_ACCT" T0 INNER JOIN "@SMPR_ACCT1" T1 ON T0."Code" = T1."Code" WHERE IFNULL(T1."U_loancode", '') <> '') 
    AS T3 ON T3."U_emptype" = T2."U_gropCode" AND T3."U_loancode" = T0."U_LoanCode" AND CAST(T0."U_DocDate" AS date) BETWEEN CAST(T3."U_fromdate" AS date) 
    AND CAST(IFNULL(T3."U_todate", T0."U_DocDate") AS date) WHERE IFNULL(T0."U_jeno", '') = '' AND IFNULL(T0."U_Approved", 'N') = 'Y' AND IFNULL(T0."Canceled", 'N') = 'N' 
    AND IFNULL(T0."Status", '') = 'O';
END;

Go
-------Procedure for Loan APplication Repayment Manual Posting--------------------------------------------------------------------
CREATE PROCEDURE "Innova_HRMS_Posting_LoanRepayment_Manual" AS BEGIN  

WITH "empdetails" AS (SELECT T0."U_empID", T0."U_gropCode", T1."U_costcode" AS "Dept_CC", T2."U_costcode" AS "Location_CC", T0."U_ExtEmpNo" AS "Employee_CC", 
T0."U_otherscc" AS "Others_CC" FROM "@SMPR_OHEM" T0 INNER JOIN OUDP T1 ON T0."U_dept" = T1."Code" INNER JOIN OUBR T2 ON T2."Code" = T0."U_branch") ,

"IncomingDetails" as (Select "DocEntry","DocNum","DocDate","ObjType" from ORCT)

SELECT T0."DocEntry", T0."DocNum", T4."LineId", T0."U_empID", T0."U_empName", T0."U_IDNo", T5."DocDate" AS "Date", T1."Code", T1."Name", T4."U_PaidAmt" AS "Amount", 
T3."U_loancc" AS "DebitAccount", T3."U_loandc" AS "CreditAccount", t2."Location_CC" AS "Ocrcode1", t2."Dept_CC" AS "Ocrcode2", t2."Employee_CC" AS "Ocrcode3", 
'' AS "Ocrcode4", t2."Others_CC" AS "Ocrcode5", 'OLOA' AS "Transcode", 'Loan Application Deduction- ' || T0."U_IDNo" AS "Memo", left('Loan Application - Entry No : ' 
|| CAST(T0."DocEntry" AS varchar) || ' & Appl No : ' || CAST(T0."DocNum" AS varchar) || ' & Line No : ' || CAST(T4."LineId" AS varchar) || ' & Loan Type : ' || T1."Name", 
250) AS "Narration", left(('Employee No : ' || T0."U_IDNo" || '  ID : ' || T0."U_empID"), 99) AS "Ref1", left(T0."U_empName", 99) AS "Ref2", left(CAST(T1."Name" AS varchar) 
|| ' - Deduction', 26) AS "Ref3" FROM "@SMPR_OLOA" T0 INNER JOIN "@SMPR_LOA1" T4 ON T4."DocEntry" = T0."DocEntry" INNER JOIN "@SMPR_OLON" T1 ON T0."U_LoanCode" = T1."Code" 
INNER JOIN "empdetails" T2 ON T2."U_empID" = T0."U_empID" INNER JOIN "IncomingDetails" T5 ON T5."DocEntry" = T4."U_trgtenty" AND T5."ObjType" = T4."U_trgttype" 
INNER JOIN (SELECT "U_emptype", "U_fromdate", "U_todate", T1."U_loancode", T1."U_loandc", T1."U_loancc" FROM "@SMPR_ACCT" T0 INNER JOIN "@SMPR_ACCT1" T1 ON T0."Code" = 
T1."Code" WHERE IFNULL(T1."U_loancode", '') <> '') AS T3 ON T3."U_emptype" = T2."U_gropCode" AND T3."U_loancode" = T0."U_LoanCode" AND CAST(T5."DocDate" AS date) 
BETWEEN CAST(T3."U_fromdate" AS date) AND CAST(IFNULL(T3."U_todate", T5."DocDate") AS date) WHERE IFNULL(T0."U_Approved", 'N') = 'Y' AND IFNULL(T0."Canceled", 'N') = 'N'
 AND IFNULL(T4."U_Status", '') = 'C' AND IFNULL(T4."U_trgttype", '') = '24' AND IFNULL(T4."U_jeno", '') = '';
END;
-------Procedure for Settlement Posting--------------------------------------------------------------------
CREATE PROCEDURE "Innova_HRMS_Posting_Settlement" (IN Docentry varchar(100)) AS 
GLEntry varchar(100);
settype varchar(100); 

Begin



SELECT T2."Code", (CASE WHEN T0."U_setltype" = 'LS' THEN 'Leave Settlement' ELSE 'Full & Final Settlement' END) 
INTO GLEntry, settype FROM "@SMPR_OLSE" T0 INNER JOIN "@SMPR_OHEM" T1 ON T0."U_EmpID" = T1."U_empID" INNER JOIN "@SMPR_ACCT" T2 ON T2."U_emptype" = T1."U_gropCode" 
AND T0."U_LveSettDate" BETWEEN T2."U_fromdate" AND IFNULL(T2."U_todate", T0."U_LveSettDate");




WITH "cte" AS (SELECT T0."DocEntry", T0."U_lvsalamt", T2."U_lvesaldc", T2."U_lvesaldn", T2."U_lvesalcc", T2."U_lvesalcn", T0."U_lvncshmt", T2."U_lveencdc", 
T2."U_lveencdn", T2."U_lveenccc", T2."U_lveenccn", T0."U_AiTiketAmt", T2."U_aircladc", T2."U_aircladn", T2."U_airclacc", T2."U_airclacn", T0."U_advsalry",
 T2."U_advsaldc", T2."U_advsaldn", T2."U_advsalcc", T2."U_advsalcn", T0."U_gratuity", T2."U_gratiydc", T2."U_gratiydn", T2."U_gratiycc", T2."U_gratiycn", 
 T0."U_Remarks" AS "Remarks" FROM "@SMPR_OLSE" T0 INNER JOIN "@SMPR_ACCT" T2 ON T2."Code" = :GLEntry WHERE T0."DocEntry" = :Docentry),

"Details" AS (
SELECT "DocEntry", 'Leave Salary' AS "Type", '' AS "Name", "U_lvsalamt" AS "Amount", "U_lvesaldc" AS "DebitCode", "U_lvesaldn" AS "DebitName",
"U_lvesalcc" AS "CreditCode", "U_lvesalcn" AS "CreditName" FROM "cte" WHERE IFNULL("U_lvsalamt", 0) <> 0
  
UNION ALL SELECT "DocEntry", 'Leave Encashment' AS "Type",  '' AS "Name", "U_lvncshmt" AS "Amount", "U_lveencdc", "U_lveencdn", "U_lveenccc", "U_lveenccn" 
FROM "cte" WHERE IFNULL("U_lvncshmt", 0) <> 0

UNION ALL SELECT "DocEntry", 'Air Ticket Claim' AS "Type", '' AS "Name", "U_AiTiketAmt" AS "Amount", "U_aircladc", "U_aircladn", "U_airclacc", 
"U_airclacn" FROM "cte" WHERE IFNULL("U_AiTiketAmt", 0) <> 0 
   
UNION ALL SELECT "DocEntry", 'Advance Salary' AS "Type", '' AS "Name", "U_advsalry" AS "Amount", "U_advsaldc", "U_advsaldn", "U_advsalcc", "U_advsalcn"
FROM "cte" WHERE IFNULL("U_advsalry", 0) <> 0 
    
UNION ALL SELECT "DocEntry", 'Gratuity' AS "Type", '' AS "Name", "U_gratuity" AS "Amount", "U_gratiydc", "U_gratiydn", "U_gratiycc", "U_gratiycn" 
FROM "cte" WHERE IFNULL("U_gratuity", 0) <> 0 

UNION ALL SELECT T0."DocEntry", 'Loan Deduction' AS "Type", T4."Name" AS "Name", T1."U_amount", T3."U_loancc", T3."U_loancn", T3."U_loandc", T3."U_loandn" 
FROM "@SMPR_OLSE" T0 INNER JOIN "@SMPR_LSE3" T1 ON T1."DocEntry" = T0."DocEntry" INNER JOIN "@SMPR_OLOA" T2 ON T2."DocEntry" = T1."U_loanapen" AND 
T2."DocNum" = T1."U_loanapno" INNER JOIN "@SMPR_ACCT1" T3 ON T3."Code" = :GLEntry AND T3."U_loancode" = T2."U_LoanCode" INNER JOIN "@SMPR_OLON" T4 
ON T4."Code" = T2."U_LoanCode" WHERE IFNULL(T1."U_amount", 0) <> 0 AND T0."DocEntry" = :Docentry 

UNION ALL SELECT T0."DocEntry", 'Addition/Deduction' AS "Type", T3."Name", T1."U_amount", T2."U_adddeddc", T2."U_adddeddn", T2."U_adddedcc", T2."U_adddedcn" 
FROM "@SMPR_OLSE" T0 INNER JOIN "@SMPR_LSE4" T1 ON T1."DocEntry" = T0."DocEntry" INNER JOIN "@SMPR_ACCT2" T2 ON T2."Code" = :GLEntry AND T2."U_andncode" = T1."U_type" 
INNER JOIN "@SMPR_OPYE" T3 ON T3."Code" = T1."U_type" WHERE IFNULL(T1."U_amount", 0) <> 0 AND T0."DocEntry" = :Docentry),

 "CostCenterdetails" AS (SELECT T0."U_empID", T2."U_costcode" AS "Ocrcode1", T1."U_costcode" AS "Ocrcode2", T0."U_ExtEmpNo" AS "Ocrcode3", '' AS "Ocrcode4",
  T0."U_otherscc" AS "Ocrcode5" FROM "@SMPR_OHEM" T0 INNER JOIN OUDP T1 ON T0."U_dept" = T1."Code" INNER JOIN OUBR T2 ON T2."Code" = T0."U_branch"),



"Results" As(
SELECT T0."DocEntry", T0."Type" AS "SettType", T0."Name" AS "Sett_Name", T0."Amount", T0."DebitCode", T0."DebitName", 
T0."CreditCode", T0."CreditName", T1."DocNum", T1."U_LveSettDate" AS "Date", 'OLSE' AS "Transcode", T1."U_EmpID" AS "EmpID", T1."U_IDNo" AS "InnovaID", 
T1."U_EmpName" AS "EmpName", T1."U_Remarks", left(('Employee No : ' || T1."U_IDNo" || '  ID : ' || T1."U_EmpID"), 99) AS "Ref1", left(T1."U_EmpName", 99) AS "Ref2",
 left(:settype, 26) AS "Ref3", :settype || ' - ' || T1."U_IDNo" AS "Memo", left(:settype || ' - Entry No : ' || CAST(T0."DocEntry" AS varchar) || ' & Appl No : ' || 
 CAST(T1."DocNum" AS varchar) || (CASE WHEN CAST(IFNULL("U_Remarks", '') AS varchar) <> '' THEN '  Remarks : ' || CAST("U_Remarks" AS varchar) ELSE '' END), 250) 
 AS "Narration", T2."Ocrcode1", T2."Ocrcode2", T2."Ocrcode3", T2."Ocrcode4", T2."Ocrcode5" FROM "Details" T0 INNER JOIN "@SMPR_OLSE" T1 ON T0."DocEntry" = T1."DocEntry" 
 INNER JOIN "CostCenterdetails" T2 ON T2."U_empID" = T1."U_EmpID"
)



SELECT "DocEntry", "SettType" AS "Lref1", "Sett_Name" AS "Lref2", "DocNum" AS "Lref3", "DebitCode" AS "AcctCode", "DebitName" AS "AcctName", "Amount" AS "DebitAmount",
0 AS "CreditAmount", "Date", "Transcode", "EmpID", "InnovaID", "EmpName", "Ref1", "Ref2", "Ref3", "Memo", "Narration", "Ocrcode1", "Ocrcode2", "Ocrcode3", "Ocrcode4", 
 "Ocrcode5" FROM "Results"
 
  UNION ALL SELECT "DocEntry", '', '', "DocNum" AS "Lref3", "CreditCode" AS "AcctCode", "CreditName" AS "AcctName", 0 AS "DebitAmount", 
 SUM("Amount") AS "CreditAmount", "Date", "Transcode", "EmpID", "InnovaID", "EmpName", "Ref1", "Ref2", "Ref3", "Memo", "Narration", "Ocrcode1", "Ocrcode2", "Ocrcode3", 
 "Ocrcode4", "Ocrcode5" FROM  "Results" GROUP BY "DocEntry", "CreditCode", "CreditName", "DocNum", "Date", "Transcode", "EmpID", "InnovaID", "EmpName", "Ref1", "Ref2", 
 "Ref3", "Memo", "Narration", "Ocrcode1", "Ocrcode2", "Ocrcode3", "Ocrcode4", "Ocrcode5";
 

END;

-------Procedure for Payroll Process Posting--------------------------------------------------------------------
CREATE PROCEDURE "Innova_HRMS_Posting_PayrollProcess" (IN DocEntry varchar(100)) AS 
BEGIN  
--Getting Cost Center details for the employees

WITH "CostCenterdetails" AS (SELECT T0."U_empID", T2."U_costcode" AS "Ocrcode1", T1."U_costcode" AS "Ocrcode2", T1."Name", T0."U_ExtEmpNo" AS "Ocrcode3", ''
 AS "Ocrcode4", T0."U_otherscc" AS "Ocrcode5" FROM "@SMPR_OHEM" T0 INNER JOIN OUDP T1 ON T0."U_dept" = T1."Code" INNER JOIN OUBR T2 ON T2."Code" = T0."U_branch") ,
 
"temp_GLEntry" AS (SELECT T1."U_IDNo", T1."U_empID", T3."Code" AS "GLEntry", T4."Ocrcode1", T4."Ocrcode2", T4."Ocrcode3",
T4."Ocrcode4", T4."Ocrcode5", T0."U_ToDate" AS "Date", 'OPRC' AS "Transcode", 'Payroll Process For the Month of ' || MONTH(T0."U_ToDate") || ' ' || 
CAST(YEAR(T0."U_ToDate") AS varchar) AS "Memo", 'Payroll Process For the Month of ' || MONTH(T0."U_ToDate") || ' ' || CAST(YEAR(T0."U_ToDate") AS varchar) AS 
"Narration", 'Payroll Entry No :' || CAST(T0."DocEntry" AS varchar(100)) || ' Doc No :' || CAST(T0."DocNum" AS varchar(100)) AS "Ref1", 'PayrollProcess' AS "Ref2",
T0."DocNum" AS "Ref3", T3."U_otdc" AS "OtdebitCode", T3."U_otdn" AS "OtdebitName", T3."U_otcc" AS "OtCreditCode", T3."U_otcn" AS "OtCreditName", T3."U_aircladc" AS
"AirDebitCode", T3."U_aircladn" AS "AirDebitName", T3."U_airclacc" AS "AirCreditCode", T3."U_airclacn" AS "AirCreditName", T3."U_tripaldc" AS "TripDebitCode",
T3."U_tripaldn" AS "TripDebitName", T3."U_tripalcc" AS "TripCreditCode", T3."U_tripalcn" AS "TripCreditName", T3."U_lvesaldc" AS "ALSalDebitCode", T3."U_lvesaldn"
AS "ALSalDebitName", T3."U_lvesalcc" AS "ALSalCreditCode", T3."U_lvesalcn" AS "ALSalCreditName", T3."U_advsaldc" AS "AdvsalDebitCode", T3."U_advsaldn" 
AS "AdvsalDebitName", T3."U_advsalcc" AS "AdvsalCreditCode", T3."U_advsalcn" AS "AdvsalCreditName" FROM "@SMPR_OPRC" T0 INNER JOIN "@SMPR_PRC1" T1 ON 
T0."DocEntry" = T1."DocEntry" INNER JOIN "@SMPR_OHEM" T2 ON T2."U_empID" = T1."U_empID" INNER JOIN "@SMPR_ACCT" T3 ON T3."U_emptype" = T2."U_gropCode" AND 
T0."U_ToDate" BETWEEN T3."U_fromdate" AND IFNULL(T3."U_todate", T0."U_ToDate") INNER JOIN "CostCenterdetails" T4 ON T4."U_empID" = T1."U_empID"
WHERE T0."DocEntry" = :DocEntry
),

"Payroll_posting" As (
--Salary Pay Element Posting
 SELECT 'A' AS "Type", T1."Date", T1."Transcode", T1."Memo", T1."Narration", T1."Ref1", T1."Ref2", T1."Ref3", CAST('Salary-PayElements' AS varchar(100)) AS "Lref1"
 , CAST(T3."Name" AS varchar(100)) AS "Lref2", T1."Ref3" AS "Lref3", CAST(T1."Ocrcode1" AS varchar(100)) AS "Ocrcode1", CAST(T1."Ocrcode2" AS varchar(100)) AS "ocrcode2", 
 CAST('' AS varchar(100)) AS "ocrcode3", CAST('' AS varchar(100)) AS "ocrcode4", CAST(T1."Ocrcode5" AS varchar(100)) AS "ocrcode5", T2."U_payeledc" AS "DebitCode", 
 T2."U_payeledn" AS "DebitName", T2."U_payelecc" AS "CreditCode", T2."U_payelecn" AS "CreditName", SUM(IFNULL(T0."Amount", 0)) AS "Amount" FROM 
 (SELECT "U_empID", U_A1 AS "Amount", 'A1' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_A1, 0) > 0 AND "DocEntry" = :DocEntry 
 UNION ALL SELECT "U_empID", U_A2 AS "Amount", 'A2' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_A2, 0) > 0 AND "DocEntry" = :DocEntry 
 UNION ALL SELECT "U_empID", U_A3 AS "Amount", 'A3' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_A3, 0) > 0 AND "DocEntry" = :DocEntry 
 UNION ALL SELECT "U_empID", U_A4 AS "Amount", 'A4' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_A4, 0) > 0 AND "DocEntry" = :DocEntry 
 UNION ALL SELECT "U_empID", U_A5 AS "Amount", 'A5' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_A5, 0) > 0 AND "DocEntry" = :DocEntry 
 UNION ALL SELECT "U_empID", U_A6 AS "Amount", 'A6' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_A6, 0) > 0 AND "DocEntry" = :DocEntry 
 UNION ALL SELECT "U_empID", U_A7 AS "Amount", 'A7' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_A7, 0) > 0 AND "DocEntry" = :DocEntry 
 UNION ALL SELECT "U_empID", U_A8 AS "Amount", 'A8' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_A8, 0) > 0 AND "DocEntry" = :DocEntry 
 UNION ALL SELECT "U_empID", U_A9 AS "Amount", 'A9' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_A9, 0) > 0 AND "DocEntry" = :DocEntry 
 UNION ALL SELECT "U_empID", U_A10 AS "Amount", 'A10' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_A10, 0) > 0 AND "DocEntry" = :DocEntry 
 UNION ALL SELECT "U_empID", U_A11 AS "Amount", 'A11' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_A11, 0) > 0 AND "DocEntry" = :DocEntry 
 UNION ALL SELECT "U_empID", U_A12 AS "Amount", 'A12' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_A12, 0) > 0 AND "DocEntry" = :DocEntry 
 UNION ALL SELECT "U_empID", U_A13 AS "Amount", 'A13' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_A13, 0) > 0 AND "DocEntry" = :DocEntry 
 UNION ALL SELECT "U_empID", U_A14 AS "Amount", 'A14' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_A14, 0) > 0 AND "DocEntry" = :DocEntry 
 UNION ALL SELECT "U_empID", U_A15 AS "Amount", 'A15' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_A15, 0) > 0 AND "DocEntry" = :DocEntry 
 UNION ALL SELECT "U_empID", U_A16 AS "Amount", 'A16' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_A16, 0) > 0 AND "DocEntry" = :DocEntry 
 UNION ALL SELECT "U_empID", U_A17 AS "Amount", 'A17' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_A17, 0) > 0 AND "DocEntry" = :DocEntry 
 UNION ALL SELECT "U_empID", U_A18 AS "Amount", 'A18' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_A18, 0) > 0 AND "DocEntry" = :DocEntry 
 UNION ALL SELECT "U_empID", U_A19 AS "Amount", 'A19' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_A19, 0) > 0 AND "DocEntry" = :DocEntry 
 UNION ALL SELECT "U_empID", U_A20 AS "Amount", 'A20' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_A20, 0) > 0 AND "DocEntry" = :DocEntry
) 
 AS T0 INNER JOIN "temp_GLEntry" T1 ON T0."U_empID" = T1."U_empID" INNER JOIN "@SMPR_ACCT3" T2 ON T2."Code" = T1."GLEntry" INNER JOIN "@SMPR_OPYE" 
 T3 ON T3."Code" = T2."U_paycode" AND T3."U_Sequence" = T0."Type" WHERE IFNULL(T2."U_paycode", '') <> '' GROUP BY T1."Date", T1."Transcode", T1."Memo", 
 T1."Narration", T1."Ref1", T1."Ref2", T1."Ref3", T1."Ocrcode1", T1."Ocrcode2", T1."Ocrcode5", "U_payeledc", "U_payeledn", "U_payelecc", "U_payelecn", T3."Name"

--Select * From "@SMPR_OPYE" 
 Union ALL
 --OT Amount Posting
  SELECT 'A' AS "Type", T1."Date", T1."Transcode", T1."Memo", T1."Narration", T1."Ref1", T1."Ref2", T1."Ref3", 'Salary-Addition' AS "Lref1", 'OT' AS "Lref2", 
  T1."Ref3" AS "Lref3", T1."Ocrcode1", T1."Ocrcode2", '' AS "ocrcode3", '' AS "ocrcode4", T1."Ocrcode5", T1."OtdebitCode", T1."OtdebitName", T1."OtCreditCode", 
  T1."OtCreditName", SUM("U_TotalOTAmt") AS "Amount" FROM "@SMPR_PRC1" T0 INNER JOIN "temp_GLEntry" T1 ON T0."U_empID" = T1."U_empID" 
  WHERE IFNULL("U_TotalOTAmt", 0) > 0 AND "DocEntry" = :DocEntry 
  GROUP BY T1."Date", T1."Transcode", T1."Memo", T1."Narration", T1."Ref1", T1."Ref2", T1."Ref3", 
  T1."Ocrcode1", T1."Ocrcode2", T1."Ocrcode5", T1."OtdebitCode", T1."OtdebitName", T1."OtCreditCode", T1."OtCreditName"
 
 Union ALL
 --Fixed Addition (Air Ticket Claim)
 SELECT 'A' AS "Type", T1."Date", T1."Transcode", T1."Memo", T1."Narration", T1."Ref1", T1."Ref2", T1."Ref3", 'Additions' AS "Lref1", 'Air Ticket Claim' AS "Lref2", 
 T1."Ref3" AS "Lref3", T1."Ocrcode1", T1."Ocrcode2", '' AS "ocrcode3", '' AS "ocrcode4", T1."Ocrcode5", T1."AirDebitCode", T1."AirDebitName", T1."AirCreditCode", 
 T1."AirCreditName" , SUM(U_FA1) AS "Amount" FROM "@SMPR_PRC1" T0 INNER JOIN "temp_GLEntry" T1 ON T0."U_empID" = T1."U_empID" 
 WHERE IFNULL(U_FA1, 0) > 0 AND "DocEntry" = :DocEntry 
 GROUP BY T1."Date", T1."Transcode", T1."Memo", T1."Narration", T1."Ref1", T1."Ref2", T1."Ref3", T1."Ocrcode1", 
 T1."Ocrcode2", T1."Ocrcode5", T1."AirDebitCode", T1."AirDebitName", T1."AirCreditCode", T1."AirCreditName"
 

 
 Union ALL
 --Fixed Addition (Trip Allowance)
 SELECT 'A' AS "Type", T1."Date", T1."Transcode", T1."Memo", T1."Narration", T1."Ref1", T1."Ref2", T1."Ref3", 'Additions' AS "Lref1", 'Trip Allowance' AS "Lref2", 
 T1."Ref3" AS "Lref3", T1."Ocrcode1", T1."Ocrcode2", '' AS "ocrcode3", '' AS "Ocrcode4", T1."Ocrcode5", T1."TripDebitCode", T1."TripDebitName", T1."TripCreditCode", 
 T1."TripCreditName", SUM("U_FA2") AS "Amount" FROM "@SMPR_PRC1" T0 INNER JOIN "temp_GLEntry" T1 ON T0."U_empID" = T1."U_empID" 
 WHERE IFNULL("U_FA2", 0) > 0 AND "DocEntry" = :DocEntry 
 GROUP BY T1."Date", T1."Transcode", T1."Memo", T1."Narration", T1."Ref1", T1."Ref2", T1."Ref3", T1."Ocrcode1", 
 T1."Ocrcode2", T1."Ocrcode5", T1."TripDebitCode", T1."TripDebitName", T1."TripCreditCode", T1."TripCreditName"

UNION ALL
--Variable Addition(Addition/Deduction)
SELECT 'A' AS "Type", T1."Date", T1."Transcode", T1."Memo", T1."Narration", T1."Ref1", T1."Ref2", T1."Ref3", 'Additions' AS "Lref1", T3."Name" AS "Lref2", T1."Ref3" 
AS "Lref3", T1."Ocrcode1", T1."Ocrcode2", '' AS "ocrcode3", '' AS "ocrcode4", T1."Ocrcode5", T2."U_adddeddc" AS "DebitCode", T2."U_adddeddn" AS "DebitName", 
T2."U_adddedcc" AS "CreditCode", T2."U_adddedcn" AS "CreditName", SUM(IFNULL(T0."Amount", 0)) AS "Amount" FROM (
SELECT "U_empID", U_AB1 AS "Amount", 'AB1' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_AB1, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_AB2 AS "Amount", 'AB2' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_AB2, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_AB3 AS "Amount", 'AB3' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_AB3, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_AB4 AS "Amount", 'AB4' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_AB4, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_AB5 AS "Amount", 'AB5' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_AB5, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_AB6 AS "Amount", 'AB6' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_AB6, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_AB7 AS "Amount", 'AB7' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_AB7, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_AB8 AS "Amount", 'AB8' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_AB8, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_AB9 AS "Amount", 'AB9' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_AB9, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_AB10 AS "Amount", 'AB10' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_AB10, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_AB11 AS "Amount", 'AB11' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_AB11, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_AB12 AS "Amount", 'AB12' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_AB12, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_AB13 AS "Amount", 'AB13' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_AB13, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_AB14 AS "Amount", 'AB14' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_AB14, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_AB15 AS "Amount", 'AB15' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_AB15, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_AB16 AS "Amount", 'AB16' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_AB16, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_AB17 AS "Amount", 'AB17' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_AB17, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_AB18 AS "Amount", 'AB18' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_AB18, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_AB19 AS "Amount", 'AB19' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_AB19, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_AB20 AS "Amount", 'AB20' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_AB20, 0) > 0 AND "DocEntry" = :DocEntry
)
 AS T0 INNER JOIN "temp_GLEntry" T1 ON T0."U_empID" = T1."U_empID" INNER JOIN "@SMPR_ACCT2" T2 ON T2."Code" = T1."GLEntry" INNER JOIN "@SMPR_OPYE" T3 
 ON T3."Code" = T2."U_andncode" AND T3."U_Sequence" = T0."Type" WHERE IFNULL(T2."U_andncode", '') <> '' GROUP BY T1."Date", T1."Transcode", T1."Memo", 
 T1."Narration", T1."Ref1", T1."Ref2", T1."Ref3", T1."Ocrcode1", T1."Ocrcode2", T1."Ocrcode5", "U_adddeddc", "U_adddeddn", "U_adddedcc", "U_adddedcn", T3."Name"
 
 Union ALL
 --Fixed Deduction (Loan Deduction)
 SELECT 'D' AS "Type", T3."Date", T3."Transcode", T3."Memo", T3."Narration", T3."Ref1", T3."Ref2", T3."Ref3", 'Deductions-Loan' AS "Lref1", A."Name" AS "Lref2", 
 T3."Ref3" AS "Lref3", T3."Ocrcode1", T3."Ocrcode2", T0."U_IDNo" AS "ocrcode3", '' AS "ocrcode4", T3."Ocrcode5", T4."U_loancc" AS "DebitCode", T4."U_loancn" 
 AS "DebitName", T4."U_loandc" AS "CreditCode", T4."U_loandn" AS "CreditName", SUM(T1."U_PaidAmt") AS "Amount" FROM "@SMPR_OLOA" T0 INNER JOIN "@SMPR_LOA1" T1 
 ON T0."DocEntry" = T1."DocEntry" INNER JOIN "@SMPR_OLON" A ON A."Code" = T0."U_LoanCode" INNER JOIN "@SMPR_OPRC" T2 ON T1."U_Date" BETWEEN T2."U_FromDate" 
 AND T2."U_ToDate" AND T2."DocEntry" = :DocEntry 
 INNER JOIN "temp_GLEntry" T3 ON T3."U_empID" = T0."U_empID" INNER JOIN "@SMPR_ACCT1" T4 ON T4."Code" = T3."GLEntry" 
 AND T4."U_loancode" = T0."U_LoanCode" WHERE IFNULL(T0."U_Approved", '') = 'Y' AND IFNULL(T0."Canceled", '') <> 'Y' AND IFNULL(T1."U_Status", 'O') = 'C' 
 AND IFNULL(T1."U_dedsal", '') = 'Y' AND IFNULL(T1."U_PaidAmt", 0) <> 0 GROUP BY T3."Date", T3."Transcode", T3."Memo", T3."Narration", T3."Ref1", T3."Ref2", T3."Ref3", 
 A."Name", T3."Ocrcode1", T3."Ocrcode2", T0."U_IDNo", T3."Ocrcode5", T4."U_loancc", T4."U_loancn", T4."U_loandc", T4."U_loandn"

UNION ALL
--Fixed Deduction(Annual Leave Salary Deduction From Leave Settlement)
SELECT 'D' AS "Type", T1."Date", T1."Transcode", T1."Memo", T1."Narration", T1."Ref1", T1."Ref2", T1."Ref3", 'Deductions' AS "Lref1", 'Annual Salary' AS "Lref2", 
T1."Ref3" AS "Lref3", T1."Ocrcode1", T1."Ocrcode2", T0."U_IDNo" AS "ocrcode3", '' AS "ocrcode4", T1."Ocrcode5", T1."ALSalCreditCode" AS "DebitCode", 
T1."ALSalCreditName" AS "DebitName", T1."ALSalDebitCode" AS "CreditCode", T1."ALSalDebitName" AS "CreditName", SUM(U_FD2) AS "Amount" FROM "@SMPR_PRC1" T0 
INNER JOIN "temp_GLEntry" T1 ON T0."U_empID" = T1."U_empID" WHERE IFNULL(U_FD2, 0) > 0 AND "DocEntry" = :DocEntry 
GROUP BY T1."Date", T1."Transcode", T1."Memo", 
T1."Narration", T1."Ref1", T1."Ref2", T1."Ref3", T1."Ocrcode1", T1."Ocrcode2", T0."U_IDNo", T1."Ocrcode5", T1."ALSalCreditCode", T1."ALSalCreditName", 
T1."ALSalDebitCode", T1."ALSalDebitName"

UNION ALL
--Fixed Deduction(Advance Leave Salary Deduction From Leave Settlement)
SELECT 'D' AS "Type", T1."Date", T1."Transcode", T1."Memo", T1."Narration", T1."Ref1", T1."Ref2", T1."Ref3", 'Deductions' AS "Lref1", 'Advance Salary' AS "Lref2", 
T1."Ref3" AS "Lref3", T1."Ocrcode1", T1."Ocrcode2", T0."U_IDNo" AS "ocrcode3", '' AS "ocrcode4", T1."Ocrcode5", T1."AdvsalCreditCode" AS "DebitCode", 
T1."AdvsalCreditName" AS "DebitName", T1."AdvsalDebitCode" AS "CreditCode", T1."AdvsalDebitName" AS "CreditName", SUM(U_FD3) AS "Amount" FROM "@SMPR_PRC1" 
T0 INNER JOIN "temp_GLEntry" T1 ON T0."U_empID" = T1."U_empID" WHERE IFNULL(U_FD3, 0) > 0 AND "DocEntry" = :DocEntry 
GROUP BY T1."Date", T1."Transcode", 
T1."Memo", T1."Narration", T1."Ref1", T1."Ref2", T1."Ref3", T1."Ocrcode1", T1."Ocrcode2", T0."U_IDNo", T1."Ocrcode5", T1."AdvsalCreditCode", T1."AdvsalCreditName", 
T1."AdvsalDebitCode", T1."AdvsalDebitName"

UNION ALL
--Variable Deduction (Addition/Deduction)
SELECT 'D' AS "Type", T1."Date", T1."Transcode", T1."Memo", T1."Narration", T1."Ref1", T1."Ref2", T1."Ref3", 'Deductions' AS "Lref1", T3."Name" AS "Lref2", 
T1."Ref3" AS "Lref3", T1."Ocrcode1", T1."Ocrcode2", '' AS "ocrcode3", '' AS "ocrcode4", T1."Ocrcode5", T2."U_adddedcc" AS "DebitCode", T2."U_adddedcn" AS "DebitName", 
T2."U_adddeddc" AS "CreditCode", T2."U_adddeddn" AS "CreditName", SUM(IFNULL(T0."Amount", 0)) AS "Amount" FROM (
SELECT "U_empID", U_DB1 AS "Amount", 'DB1' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_DB1, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_DB2 AS "Amount", 'DB2' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_DB2, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_DB3 AS "Amount", 'DB3' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_DB3, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_DB4 AS "Amount", 'DB4' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_DB4, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_DB5 AS "Amount", 'DB5' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_DB5, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_DB6 AS "Amount", 'DB6' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_DB6, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_DB7 AS "Amount", 'DB7' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_DB7, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_DB8 AS "Amount", 'DB8' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_DB8, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_DB9 AS "Amount", 'DB9' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_DB9, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_DB10 AS "Amount", 'DB10' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_DB10, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_DB11 AS "Amount", 'DB11' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_DB11, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_DB12 AS "Amount", 'DB12' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_DB12, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_DB13 AS "Amount", 'DB13' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_DB13, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_DB14 AS "Amount", 'DB14' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_DB14, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_DB15 AS "Amount", 'DB15' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_DB15, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_DB16 AS "Amount", 'DB16' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_DB16, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_DB17 AS "Amount", 'DB17' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_DB17, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_DB18 AS "Amount", 'DB18' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_DB18, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_DB19 AS "Amount", 'DB19' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_DB19, 0) > 0 AND "DocEntry" = :DocEntry 
UNION ALL SELECT "U_empID", U_DB20 AS "Amount", 'DB20' AS "Type" FROM "@SMPR_PRC1" WHERE IFNULL(U_DB20, 0) > 0 AND "DocEntry" = :DocEntry
) 
AS T0 INNER JOIN "temp_GLEntry" T1 ON T0."U_empID" = T1."U_empID" INNER JOIN "@SMPR_ACCT2" T2 ON T2."Code" = T1."GLEntry" INNER JOIN "@SMPR_OPYE" T3 ON 
T3."Code" = T2."U_andncode" AND T3."U_Sequence" = T0."Type" WHERE IFNULL(T2."U_andncode", '') <> '' GROUP BY T1."Date", T1."Transcode", T1."Memo", T1."Narration",
 T1."Ref1", T1."Ref2", T1."Ref3", T1."Ocrcode1", T1."Ocrcode2", T1."Ocrcode5", "U_adddeddc", "U_adddeddn", "U_adddedcc", "U_adddedcn", T3."Name")
 
 
 SELECT "Type", "Date", "Transcode", "Memo", "Narration", "Ref1", "Ref2", "Ref3", "Lref1", "Lref2", "Lref3", "Ocrcode1", "ocrcode2", "ocrcode3", "ocrcode4", 
 "ocrcode5", "DebitCode" AS "AccountCode", "DebitName" AS "AccountName", "Amount" AS "DebitAmount", 0.00 AS "CreditAmount" FROM "Payroll_posting" WHERE "Type" = 'A' 
 
 UNION ALL SELECT "Type", "Date", "Transcode", "Memo", "Narration", "Ref1", "Ref2", "Ref3", '', '', "Lref3", '', '', '', '', "ocrcode5", "CreditCode" AS "AccountCode", 
 "CreditName" AS "AccountName", 0 AS "DebitAmount", SUM("Amount") AS "CreditAmount" FROM "Payroll_posting" WHERE "Type" = 'A' GROUP BY "Type", "Date", "Transcode", 
 "Memo", "Narration", "Ref1", "Ref2", "Ref3", "Lref3", "ocrcode5", "CreditCode", "CreditName" 
 
 UNION ALL SELECT "Type", "Date", "Transcode", "Memo", "Narration", "Ref1", "Ref2", "Ref3", "Lref1", "Lref2", "Lref3", "Ocrcode1", "ocrcode2", "ocrcode3", "ocrcode4", 
 "ocrcode5", "DebitCode" AS "AccountCode", "DebitName" AS "AccountName", "Amount" AS "DebitAmount", 0.00 AS "CreditAmount" FROM "Payroll_posting" WHERE "Type" = 'D' 
 
 UNION ALL SELECT "Type", "Date", "Transcode", "Memo", "Narration", "Ref1", "Ref2", "Ref3", "Lref1", "Lref2", "Lref3", "Ocrcode1", "ocrcode2", "ocrcode3", "ocrcode4", 
 "ocrcode5", "CreditCode" AS "AccountCode", "CreditName" AS "AccountName", 0.00 AS "DebitAmount", "Amount" AS "CreditAmount" FROM "Payroll_posting" WHERE "Type" = 'D';
 

END;
-------Procedure for Provision posting--------------------------------------------------------------------
if exists(select 1 from sys.procedures where name='Innova_HRMS_Provision_Posting') Drop Procedure Innova_HRMS_Provision_Posting
Go

Create Procedure [dbo].[Innova_HRMS_Provision_Posting](@docentry as varchar(10))
As
Begin
	--Declare @docentry as varchar(10)
	--set @docentry='2'

	Declare @previousEntry as varchar(10)
	set @previousEntry=(Select Max(Docentry) from HRMS_PROVISION_DETAILS where ProvisionDate=(select Max(ProvisionDate) from HRMS_PROVISION_DETAILS where ProvisionDate <(Select distinct ProvisionDate from HRMS_PROVISION_DETAILS WHere Docentry=@docentry)))

	;with previousdetails as (
	select EmpID,IDNO,ProvisionDate,Gratuity_Amount,AirTicket_Amount,Leave_Amount from HRMS_PROVISION_DETAILS WHere Docentry=@previousEntry)

	select 'PROV' Transcode,DateName(MM,T0.ProvisionDate)+'-'+Convert(varchar,Datepart(YYYY,T0.ProvisionDate))[Period],T0.ProvisionDate,
	sum(isnull(T0.Gratuity_Amount,0))[Current_Gratuity],sum(isnull(T1.Gratuity_Amount,0))[Previous_Gratuity],sum(isnull(T0.Gratuity_Amount,0)-isnull(T1.Gratuity_Amount,0))Gratuity_Amount,
	sum(isnull(T1.AirTicket_Amount,0))[Previous_AirTicket_Amount],sum(isnull(T0.AirTicket_Amount,0))[Current_AirTicket_Amount],sum(isnull(T0.AirTicket_Amount,0)-isnull(T1.AirTicket_Amount,0)) [AirTicket_Amount],
	sum(isnull(T1.Leave_Amount,0))[Previous_Leave_Amount],sum(isnull(T0.Leave_Amount,0))[Current_Leave_Amount],sum(isnull(T0.Leave_Amount,0)-isnull(T1.Leave_Amount,0)) [Leave_Amount],
	T0.Ocrcode1,T0.Ocrcode2,'' Ocrcode3,T0.Ocrcode4,T0.Ocrcode5,T0.Gratuity_debitCode,T0.Gratuity_debitName,T0.Gratuity_CreditCode,T0.Gratuity_CreditName,T0.Air_debitCode,T0.Air_debitName,T0.Air_CreditCode,T0.Air_CreditName
	,T0.Leave_debitCode,T0.Leave_debitName,T0.Leave_CreditCode,T0.Leave_CreditName
	from HRMS_PROVISION_DETAILS T0 left join previousdetails T1 on T0.empid=T1.empid 
	Where T0.Docentry=@docentry
	Group by DateName(MM,T0.ProvisionDate)+'-'+Convert(varchar,Datepart(YYYY,T0.ProvisionDate)),T0.ProvisionDate,T0.Ocrcode1,T0.Ocrcode2,T0.Ocrcode4,T0.Ocrcode5,
	T0.Gratuity_debitCode,T0.Gratuity_debitName,T0.Gratuity_CreditCode,T0.Gratuity_CreditName,T0.Air_debitCode,T0.Air_debitName,T0.Air_CreditCode,T0.Air_CreditName,T0.Leave_debitCode,T0.Leave_debitName,T0.Leave_CreditCode,T0.Leave_CreditName
End

