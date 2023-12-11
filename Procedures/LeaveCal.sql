-----------------------------------------------------------
IF ((:transaction_type=(n'A') OR :transaction_type=(n'U')) AND :object_type='OLVA') THEN

Declare FromDate  Date;
Declare ToDate  Date;

Select "U_FromDate" into FromDate FROM "@SMPR_OLVA" WHERE "DocEntry" = :list_of_cols_val_tab_del;
Select "U_Todate" into ToDate FROM "@SMPR_OLVA" WHERE "DocEntry" = :list_of_cols_val_tab_del;

SELECT ( 
select 1 from "@SMPR_OLVA" T0 Where (:FromDate between T0."U_FromDate" and T0."U_Todate" or :ToDate between T0."U_FromDate" and T0."U_Todate")
and T0."U_IDNo"=(Select "U_IDNo" FROM "@SMPR_OLVA" WHERE "DocEntry" = :list_of_cols_val_tab_del) having Count(*)>1
         ) 
INTO temp_var_0 FROM DUMMY;
IF :temp_var_0 > 0 
THEN error := 10008;
error_message := 'Duplicate leave attendance found for this employee...';
END IF;
End IF;
----------------------------------------backup SP for POST transactional Notification----------------------------
if  ((:transaction_type =( N'U') or :transaction_type =(N'A'))  and :object_type='OPPII') then 
Declare FromDate  Date;
Declare ToDate  Date;
Select "U_FromDate" into FromDate FROM "@MIPL_OPPI" WHERE "DocEntry" = :list_of_cols_val_tab_del;
Select "U_ToDate" into ToDate FROM "@MIPL_OPPI" WHERE "DocEntry" = :list_of_cols_val_tab_del;

UPDATE T0 SET T0."U_EmpLvBal" = (T0."U_EmpLvBal"+T1."CarryFrwdLve")-T1."LeaveTaken", 
T0."U_EmpLvTak" = T0."U_EmpLvTak"+T0."U_CurLeave" ,T0."U_CurLeave"=T1."LeaveTaken"
 FROM "@SMPR_HEM2" T0 INNER JOIN
(select T1."U_IDNo",T0."DocEntry", ifnull(T1."U_ELDays",0)"LeaveTaken",ifnull(T1."U_LeaveBal",0)"CarryFrwdLve"
FROM "@MIPL_OPPI" T0 INNER JOIN "@MIPL_PPI1" T1 ON T0."DocEntry" = T1."DocEntry" 
WHERE T1."U_empID" IS NOT NULL AND CAST(T0."U_FromDate" AS date)
BETWEEN :FromDate AND :ToDate) AS T1 on T1."U_IDNo" = T0."Code"
WHERE T1."DocEntry" = :list_of_cols_val_tab_del;
end if;
-------------------------------------------------------------------


-- B1 DEPENDS: BEFORE:PT:PROCESS_START

Alter PROCEDURE SBO_SP_PostTransactionNotice
(
	in object_type nvarchar(30), 				-- SBO Object Type
	in transaction_type nchar(1),			-- [A]dd, [U]pdate, [D]elete, [C]ancel, C[L]ose
	in num_of_cols_in_key int,
	in list_of_key_cols_tab_del nvarchar(255),
	in list_of_cols_val_tab_del nvarchar(255)
)
LANGUAGE SQLSCRIPT
AS
-- Return values
error  int;				-- Result (0 for no error)
error_message nvarchar (200); 		-- Error string to be displayed
begin

error := 0;
error_message := N'Ok';

--------------------------------------------------------------------------------------------------------------------------------

if  ((:transaction_type =( N'U') or :transaction_type =(N'A'))  and :object_type='OPPII') then 
Declare FromDate  Date;
Declare ToDate  Date;
Select "U_FromDate" into FromDate FROM "@MIPL_OPPI" WHERE "DocEntry" = :list_of_cols_val_tab_del;
Select "U_ToDate" into ToDate FROM "@MIPL_OPPI" WHERE "DocEntry" = :list_of_cols_val_tab_del;

UPDATE T0 SET T0."U_EmpLvBal" = (T0."U_EmpLvBal"+T1."CarryFrwdLve")-T1."LeaveTaken", 
T0."U_EmpLvTak" = T0."U_EmpLvTak"+T0."U_CurLeave" ,T0."U_CurLeave"=T1."LeaveTaken"
 FROM "@SMPR_HEM2" T0 INNER JOIN
(select T1."U_IDNo",T0."DocEntry", ifnull(T1."U_ELDays",0)"LeaveTaken",ifnull(T1."U_LeaveBal",0)"CarryFrwdLve"
FROM "@MIPL_OPPI" T0 INNER JOIN "@MIPL_PPI1" T1 ON T0."DocEntry" = T1."DocEntry" 
WHERE T1."U_empID" IS NOT NULL AND CAST(T0."U_FromDate" AS date)
BETWEEN :FromDate AND :ToDate) AS T1 on T1."U_IDNo" = T0."Code"
WHERE T1."DocEntry" = :list_of_cols_val_tab_del;
end if;

--------------------------------------------------------------------------------------------------------------------------------

-- Select the return values
select :error, :error_message FROM dummy;

end;





