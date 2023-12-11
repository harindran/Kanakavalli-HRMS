Namespace HRMS
    Module modGlobalVariables
        Public ApprovedUser_Employee As Boolean = False 'To Find the approved user
        Public Empmaster_currenmatrix As String = "-1" 'To Find the matrix ID for EMP Master
        Public Link_objtype As String = "-1" 'UDO Link Type
        Public Link_Value As String = "-1" 'UDO Link type value
        Public Link_Value_Additional As String = "-1" 'UDO Link type value
        Public Current_Lineid As Integer = -1 'To get Current Line ID from Matrix
        Public frmmultiselectform As SAPbouiCOM.Form
        Public multi_objtype As String = "-1" 'UDO Link Type
        Public Query_multiselect As String = "" 'Multi select query
        Public GetPayrollEnabledIndia As Boolean = False
        Public MultiBranch As String = ""
        Public objFinalDT As New DataTable
    End Module
End Namespace
