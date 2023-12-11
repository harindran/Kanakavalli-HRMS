-------Procedure for Daily Attendance--------------------------------------------------------------------

Create procedure "MIPL_SP_ODAS_FillEmployee"(
in empid NVARCHAR(5000) ,  
in attndate timestamp,
in Location VARCHAR(100),in empgroup NVARCHAR(100))
AS
/*
BEGIN
Declare temp_var_0 varchar(10);

SELECT (select count(*) from  "PROCEDURES"
 where procedure_name='Innova_HRMS_ODAS_Validation') INTO temp_var_0 FROM DUMMY;
IF :temp_var_0 > 0 THEN exec 'DROP PROCEDURE Innova_HRMS_ODAS_Validation';
END IF;
	End;
	*/
   BEGIN
	
	DECLARE attstatus VARCHAR(2);
	
	Declare temp_var_0 varchar(10);

	Select (Case when dayname(:attndate) in ('Friday') then 'WO' else 'PS' end) into attstatus from dummy;
	
	SELECT (select count(*) from HLD1 where :attndate between "StrDate" and "EndDate") INTO temp_var_0 FROM DUMMY;
	
	IF :temp_var_0 > 0 THEN select (select 'PH' from dummy) into attstatus from dummy ; 
	END IF;
	
		if :empid <> '-1' then
 Select T0."U_empID",T0."U_ExtEmpNo",T0."U_firstNam"||'0'||T0."U_lastName" "Name",
 ifnull(T0."U_position",'0') "Desig",
 ifnull(T0."U_dept",'0')"Dept",T0."U_shiftcde" "scode",T1."Name" "Sname",
		 T1."U_FromTime" "sfrom",T1."U_ToTime" "sto",
		 (case when T1."U_Include"='Y' then T1."U_ShiftHrs"+T1."U_LunchHrs" else T1."U_ShiftHrs" end)"shrs",
		 ifnull(T2."U_LveCode",:attstatus) "Attn",
		 ifnull(T0."U_OT",'N') "otappl",ifnull(T2."U_halfday",'N') "halfday",
		 (Case when :attstatus='WO' then 'Y' else 'N' end ) "Weekoff",
		 (Case when :attstatus='PH' then 'Y' else 'N' end) "PH"
		 from 
		 "@SMPR_OHEM" T0 left join "@SMHR_OSFT" T1 on T0."U_shiftcde"=T1."Code"
		 
		 inner join
		 (select "Rowno","splitdata" from "fnSplitString"((right (:empid,length(:empid)-1)) ))
		 S on S."splitdata"=T0."U_ExtEmpNo"
		  left join (select "U_empID","U_LveCode",ifnull("U_HalfDay",'N')"U_halfday" from "@SMPR_OLVA"
		 	  Where :attndate between "U_FromDate" and "U_Todate" and "@SMPR_OLVA"."Canceled"='N' 
		 	  and ifnull("U_Approved",'')='Y') T2 on T2."U_empID"=T0."U_empID" 
		 where  :empid like '%#' ||"U_ExtEmpNo" ||'#%' and T0."U_status"=1 order by S."Rowno";
		
		
   else
		
		 Select T0."U_empID",T0."U_ExtEmpNo",T0."U_firstNam"||'0'||T0."U_lastName" "Name",
 ifnull(T0."U_position",'0') "Desig",
 ifnull(T0."U_dept",'0')"Dept",T0."U_shiftcde" "scode",T1."Name" "Sname",
		 T1."U_FromTime" "sfrom",T1."U_ToTime" "sto",
		 (case when T1."U_Include"='Y' then T1."U_ShiftHrs"+T1."U_LunchHrs" else T1."U_ShiftHrs" end)"shrs",
		 ifnull(T2."U_LveCode",:attstatus) "Attn",
		 ifnull(T0."U_OT",'N') "otappl",ifnull(T2."U_halfday",'N') "halfday",
		 (Case when :attstatus='WO' then 'Y' else 'N' end ) "Weekoff",
		 (Case when :attstatus='PH' then 'Y' else 'N' end) "PH"
		 from 
		 "@SMPR_OHEM" T0 left join "@SMHR_OSFT" T1 on T0."U_shiftcde"=T1."Code"
		 
		 inner join
		 (select "Rowno","splitdata" from "fnSplitString"((right (:empid,length(:empid)-1)) ))
		 S on S."splitdata"=T0."U_ExtEmpNo"
		   left join (select "U_empID","U_LveCode",ifnull("U_HalfDay",'N')"U_halfday" from "@SMPR_OLVA"
		 	  Where :attndate between "U_FromDate" and "U_Todate" and "@SMPR_OLVA"."Canceled"='N' 
		 	  and ifnull("U_Approved",'')='Y') T2 on T2."U_empID"=T0."U_empID"
		 where  (T0."U_location"=:location or :Location='') 
		  and (T0."U_gropCode"=:empgroup or :empgroup='') 
		  and T0."U_status"=1 order by S."Rowno";
		end if ;		
		

	End;
