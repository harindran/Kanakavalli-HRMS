Create Procedure [HRMS_Payroll_Register](@month varchar(100),@year varchar(100))  
as  
Begin  
--Declare @month as varchar(100),@Year as varchar(100)  
--set @year='2018'  
--set @month='07'  
Declare @docentry as varchar(100)  
select @Docentry=COALESCE(@Docentry+'#' ,'') + convert(varchar,Docentry) from [@SMPR_OPRC] where Datepart(MM,U_fromdate)=@month and Datepart(YYYY,U_fromdate)=@year  
select @Docentry='#'+@Docentry +'#'  
  
;with EmployeeDetails as (  
Select T0.DocNum ,T0.U_PayPerid,T0.U_Fromdate,T0.U_Todate,T1.U_empid,T1.U_empname,T4.name [Designation],T3.Name[Department],T5.Location,T1.U_IDNO,  
T1.U_TotalDays,T1.U_HoliDays,T1.U_LOPDays,T1.U_TDayWrkd,T1.U_PaidDays,T1.U_LveDays,T1.U_WODays,T2.U_paymode,T6.Descr[PayMode_Name]  
from [@SMPR_OPRC] T0 inner join [@SMPR_PRC1] T1 on T0.DocEntry=T1.DocEntry inner join [@SMPR_OHEM] T2 on T2.U_empid=T1.U_Empid  
left join OUDP T3 on T3.code=T1.U_Dept left join OHPS T4 on T4.posid=T1.U_Designat left join OLCT T5 on T5.code=T2.U_location   
left join (select T1.FldValue,T1.Descr from CUFD T0 inner join ufd1 T1 on T0.tableid=T1.TableID and T0.FieldID=T1.FieldID where T0.AliasID='Paymode' and T0.TableID='@SMPR_OHEM') T6 on T6.FldValue=T2.U_paymode  
where @Docentry  like '%#'+convert(varchar,T0.Docentry)+'#%'),  
  
Paydetails as(  
select Name Name,U_type,U_Sequence from [@SMPR_OPYE] union all select 'OT'Name,'S','A21' union all  
select 'AIR TICKET'Name,'A','AB0.1' union all select 'TRIP ALLOWANCE'Name,'A','AB0.2' union all   
select 'LOAN'Name,'D','DB0.1'  union all select 'AL SALARY'Name,'D','DB0.2'  union all select 'ADVANCE'Name,'D','DB0.3' union all   
select ''Name,'G','G1' union all select ''Name,'R','R1' union all select ''Name,'N','N1' ),  
  
SalaryDetails as (  
select U_empID,U_A1[Amount],'A1'[Type] from [@SMPR_PRC1] Where isnull(U_A1,0)>0 and  @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_A2[Amount],'A2'[Type] from [@SMPR_PRC1] Where isnull(U_A2,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_A3[Amount],'A3'[Type] from [@SMPR_PRC1] Where isnull(U_A3,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_A4[Amount],'A4'[Type] from [@SMPR_PRC1] Where isnull(U_A4,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_A5[Amount],'A5'[Type] from [@SMPR_PRC1] Where isnull(U_A5,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_A6[Amount],'A6'[Type] from [@SMPR_PRC1] Where isnull(U_A6,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_A7[Amount],'A7'[Type] from [@SMPR_PRC1] Where isnull(U_A7,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_A8[Amount],'A8'[Type] from [@SMPR_PRC1] Where isnull(U_A8,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_A9[Amount],'A9'[Type] from [@SMPR_PRC1] Where isnull(U_A9,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_A10[Amount],'A10'[Type] from [@SMPR_PRC1] Where isnull(U_A10,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_A11[Amount],'A11'[Type] from [@SMPR_PRC1] Where isnull(U_A11,0)>0 and  @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_A12[Amount],'A12'[Type] from [@SMPR_PRC1] Where isnull(U_A12,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_A13[Amount],'A13'[Type] from [@SMPR_PRC1] Where isnull(U_A13,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_A14[Amount],'A14'[Type] from [@SMPR_PRC1] Where isnull(U_A14,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_A15[Amount],'A15'[Type] from [@SMPR_PRC1] Where isnull(U_A15,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_A16[Amount],'A16'[Type] from [@SMPR_PRC1] Where isnull(U_A16,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_A17[Amount],'A17'[Type] from [@SMPR_PRC1] Where isnull(U_A17,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_A18[Amount],'A18'[Type] from [@SMPR_PRC1] Where isnull(U_A18,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_A19[Amount],'A19'[Type] from [@SMPR_PRC1] Where isnull(U_A19,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_A20[Amount],'A20'[Type] from [@SMPR_PRC1] Where isnull(U_A20,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_TotalOTAmt [Amount],'A21'[Type] from [@SMPR_PRC1] Where isnull(U_TotalOTAmt,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_GrossAmt [Amount] ,'G1'[Type] from [@SMPR_PRC1] Where isnull(U_GrossAmt,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
  
union all  
  
select U_empID,U_FA1[Amount],'AB0.1'[Type] from [@SMPR_PRC1] Where isnull(U_FA1,0)>0 and  @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_FA2[Amount],'AB0.2'[Type] from [@SMPR_PRC1] Where isnull(U_FA2,0)>0 and  @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_AB1[Amount],'AB1'[Type] from [@SMPR_PRC1] Where isnull(U_AB1,0)>0 and  @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_AB2[Amount],'AB2'[Type] from [@SMPR_PRC1] Where isnull(U_AB2,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_AB3[Amount],'AB3'[Type] from [@SMPR_PRC1] Where isnull(U_AB3,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_AB4[Amount],'AB4'[Type] from [@SMPR_PRC1] Where isnull(U_AB4,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_AB5[Amount],'AB5'[Type] from [@SMPR_PRC1] Where isnull(U_AB5,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_AB6[Amount],'AB6'[Type] from [@SMPR_PRC1] Where isnull(U_AB6,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_AB7[Amount],'AB7'[Type] from [@SMPR_PRC1] Where isnull(U_AB7,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_AB8[Amount],'AB8'[Type] from [@SMPR_PRC1] Where isnull(U_AB8,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_AB9[Amount],'AB9'[Type] from [@SMPR_PRC1] Where isnull(U_AB9,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_AB10[Amount],'AB10'[Type] from [@SMPR_PRC1] Where isnull(U_AB10,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_AB11[Amount],'AB11'[Type] from [@SMPR_PRC1] Where isnull(U_AB11,0)>0 and  @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_AB12[Amount],'AB12'[Type] from [@SMPR_PRC1] Where isnull(U_AB12,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_AB13[Amount],'AB13'[Type] from [@SMPR_PRC1] Where isnull(U_AB13,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_AB14[Amount],'AB14'[Type] from [@SMPR_PRC1] Where isnull(U_AB14,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_AB15[Amount],'AB15'[Type] from [@SMPR_PRC1] Where isnull(U_AB15,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_AB16[Amount],'AB16'[Type] from [@SMPR_PRC1] Where isnull(U_AB16,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_AB17[Amount],'AB17'[Type] from [@SMPR_PRC1] Where isnull(U_AB17,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_AB18[Amount],'AB18'[Type] from [@SMPR_PRC1] Where isnull(U_AB18,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_AB19[Amount],'AB19'[Type] from [@SMPR_PRC1] Where isnull(U_AB19,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_AB20[Amount],'AB20'[Type] from [@SMPR_PRC1] Where isnull(U_AB20,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
  
union all  
  
select U_empID,U_FD1[Amount],'DB0.1'[Type] from [@SMPR_PRC1] Where isnull(U_FD1,0)>0 and  @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_FD2[Amount],'DB0.2'[Type] from [@SMPR_PRC1] Where isnull(U_FD2,0)>0 and  @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_FD3[Amount],'DB0.3'[Type] from [@SMPR_PRC1] Where isnull(U_FD3,0)>0 and  @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_DB1[Amount],'DB1'[Type] from [@SMPR_PRC1] Where isnull(U_DB1,0)>0 and  @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_DB2[Amount],'DB2'[Type] from [@SMPR_PRC1] Where isnull(U_DB2,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_DB3[Amount],'DB3'[Type] from [@SMPR_PRC1] Where isnull(U_DB3,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_DB4[Amount],'DB4'[Type] from [@SMPR_PRC1] Where isnull(U_DB4,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_DB5[Amount],'DB5'[Type] from [@SMPR_PRC1] Where isnull(U_DB5,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_DB6[Amount],'DB6'[Type] from [@SMPR_PRC1] Where isnull(U_DB6,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_DB7[Amount],'DB7'[Type] from [@SMPR_PRC1] Where isnull(U_DB7,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_DB8[Amount],'DB8'[Type] from [@SMPR_PRC1] Where isnull(U_DB8,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_DB9[Amount],'DB9'[Type] from [@SMPR_PRC1] Where isnull(U_DB9,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_DB10[Amount],'DB10'[Type] from [@SMPR_PRC1] Where isnull(U_DB10,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_DB11[Amount],'DB11'[Type] from [@SMPR_PRC1] Where isnull(U_DB11,0)>0 and  @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_DB12[Amount],'DB12'[Type] from [@SMPR_PRC1] Where isnull(U_DB12,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_DB13[Amount],'DB13'[Type] from [@SMPR_PRC1] Where isnull(U_DB13,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_DB14[Amount],'DB14'[Type] from [@SMPR_PRC1] Where isnull(U_DB14,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_DB15[Amount],'DB15'[Type] from [@SMPR_PRC1] Where isnull(U_DB15,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_DB16[Amount],'DB16'[Type] from [@SMPR_PRC1] Where isnull(U_DB16,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_DB17[Amount],'DB17'[Type] from [@SMPR_PRC1] Where isnull(U_DB17,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_DB18[Amount],'DB18'[Type] from [@SMPR_PRC1] Where isnull(U_DB18,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_DB19[Amount],'DB19'[Type] from [@SMPR_PRC1] Where isnull(U_DB19,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_DB20[Amount],'DB20'[Type] from [@SMPR_PRC1] Where isnull(U_DB20,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
  
  
union all  
select U_empID,U_NetAmt [Amount] ,'N1'[Type] from [@SMPR_PRC1] Where isnull(U_NetAmt,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%'  
union all  
select U_empID,U_RoundOff [Amount] ,'R1'[Type] from [@SMPR_PRC1] Where isnull(U_RoundOff,0)>0 and @Docentry  like '%#'+convert(varchar,Docentry)+'#%')  
  
select @month[Month],@year[Year],T0.*,Convert(int,(Case when T2.U_Type='S' then 1 when T2.U_Type='G' then 2 when T2.U_Type='A' then 3 When T2.U_Type='D' then 4 When T2.U_Type='R' then 5 When T2.U_Type='N' then 6 End))[HeaderSequence],  
Convert(varchar(100),(Case when T2.U_Type='S' then 'SALARY' when T2.U_Type='G' then 'GROSS SALARY' when T2.U_Type='A' then 'ADDITION'  When T2.U_Type='D' then 'DEDUCTION' When T2.U_Type='R' then 'ROUNDOFF' When T2.U_Type='N' then 'NET SALARY' End))[PayType],  
T2.Name[PayName],Convert(numeric(30,2),Replace(Replace(Replace(Replace(Replace(Replace(T2.U_Sequence,'A',''),'B',''),'D',''),'N',''),'G',''),'R',''))Sequence,T1.Amount  
from EmployeeDetails T0 inner join SalaryDetails T1 on T0.U_empid=T1.U_empID  
inner join Paydetails T2 on T2.U_Sequence=T1.Type  
--where T0.U_IDNo='TRZ295'  
End  
  
  
--[HRMS_Payroll_Register] '07','2018'  
  
  
  
--Select DocNum from [@SMPR_OPRC] where datepart(MM,U_Fromdate)='{?Month}' and datepart(YYYY,U_todate)='{?Year@select Distinct Datepart(YYYY,U_FromDate) from [@SMPR_OPRC]}'r