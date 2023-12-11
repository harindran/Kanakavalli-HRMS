-------Common Procedure to Fill the Dropdown in HRMS----------------------------------------------------------------------------------------





CREATE Procedure "MIPL_HRMS_EMPMASTER_COMBO_FILLING" (objtype varchar(100))
as
Begin
	select 'Position' "Type",CAST("posID" As Varchar)"Code",iFnull("name",'')"Name" from OHPS where :objtype in ('OHEM','ODAS','OPAD','OPRC')
	Union all
	select 'Department' "Type",Cast("Code" As Varchar),IFNULL("Name",'') from OUDP where :objtype in ('OHEM','ODAS','OPAD','OPRC')
	Union all
	select 'Branch' "Type",Cast("Code" As Varchar),IFNULL("Name",'')from OUBR where :objtype in ('OHEM')
	Union all
	select 'SAPUser' "Type",Cast("USERID" As varchar),IFNULL("U_NAME",'') from OUSR where :objtype in ('OHEM')
	Union all
	Select 'SalesEmployee' "Type",Cast("SlpCode" AS varchar),IFNULL("SlpName",'') from OSLP where :objtype in ('OHEM')
	Union all
	Select 'status' "Type",Cast("statusID" AS varchar),IFNULL("name",'') from OHST where :objtype in ('OHEM','OPRC')
	Union all
	Select 'TerminationReason' "Type",Cast("reasonID" As varchar),IFNULL("name",'') from OHTR where :objtype in ('OHEM')
	Union all
	Select 'Location' "Type",Cast("Code" As Varchar),IFNULL("Location",'') froM OLCT Where IFNULL(U_HR,'N')='Y' and :objtype in ('OHEM','ODAS')
	Union all
	Select 'Shift' "Type",Cast("Code" As Varchar),IFNULL("Name",'') from "@SMHR_OSFT" where :objtype in ('OHEM')
	Union all
	Select 'Grade' "Type",Cast("Code" As Varchar),IFNULL("Name",'') from "@SMPR_OGRA" where :objtype in ('OHEM')
	Union all
	Select 'Country' "Type",Cast("Code" As Varchar),IFNULL("Name",'') froM OCRY Where IFNULL("U_HR",'N')='Y' and :objtype in ('OHEM')
	Union all
	Select 'Education' "Type",Cast("edType" AS varchar) ,IFNULL("name",'') froM OHED where :objtype in ('OHEM')
	Union all
	Select 'State' "Type",Cast(-1 AS varchar),IFNULL('-1','') from dummy where :objtype in ('OHEM')
	Union all
	Select 'Bank' "Type",Cast(-1 AS varchar),IFNULL('-1','') from dummy  where :objtype in ('OHEM')
	Union all
	Select 'SubGrade1' "Type",Cast(-1 AS varchar),IFNULL('-1','') from dummy where :objtype in ('OHEM')
	Union all
	Select 'SubGrade2' "Type",Cast(-1 AS varchar),IFNULL('-1','') from dummy where :objtype in ('OHEM')
	union all
	select 'OTHERSCC' "Type","PrcCode","PrcName" from OPRC where "Active"='Y' and current_Date between "ValidFrom" and IFNULL("ValidTo",current_Date) and "DimCode"=5
	union all
	SELECT 'EmpType' "Type","FldValue" ,"Descr" FROM UFD1  WHERE "TableID"='@SMPR_OHEM' and "FieldID" in (select "FieldID" from cufd where "TableID"='@SMPR_OHEM' and "AliasID"='gropCode') and :objtype in ('OLSE','ACCT')
	union all
	select 'Leave' "Type","Code","Name" from "@SMPR_OLVE" where :objtype in ('ODAS')
	union all 
	Select 'Leave' "Type",'PS','Present' From dummy where :objtype in ('ODAS')
	union all 
	select Distinct (Case when "U_Type" in ('A','D') then 'SETTYPE' when "U_Type"='S' then 'PAY' end) "Type","Code","Name" from "@SMPR_OPYE" Where :objtype in ('OLSE','ACCT')
	union all
	select 'PAYPERIOD'  "Type","Code","Name" from OFPR where IFNULL("U_HR",'')='Y' and "F_RefDate">(select max("U_ToDate") from "@SMPR_OPRC" where IFNULL("U_Process",'')='Y') and :objtype in ('OPAD','OPRC')
	union all
	select 'Loan' "Type","Code","Name" from "@SMPR_OLON" where :objtype in ('ACCT')
	union all
	Select 'PAYMODE'  "Type","FldValue" "Code","Descr" "Name" from UFD1 where "TableID"='@SMPR_OHEM' and "FieldID"=65 and :objtype in ('OPRC')
	Order by "Type","Name";
	END