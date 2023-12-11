Namespace HRMS

    Public Class clsTable

        Public Sub HRMS_FieldCreation()
            AddFields("@MIGTIN1", "LabID", "Labour ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 50)
            If GetPayrollEnabledIndia = False Then
                Grade_MasterData()
                Additionanddecution()
                LeaveApplication()
                LoanApplication()
                Air_Ticket_Issue()
                Settlement()
                Payroll_Process()
            End If
            UDF_StandardTable() 'UDF in Standard Table
            'Master Table Field Creation
            IDCARDTYPE_MasterData()
            'AttendUpload()
            CalendarMaster()
            Skill_MasterData()
            Loan_MasterData()
            PayElement_MasterData()
            Leave_MasterData()
            Shift_MasterData()
            EmployeeMasterdata()
            AccountMapping()
            'PF_MasterData()
            Deduction_MasterData()
            Emp_Leave_MasterData()
            'ESI_MasterData()
            'EmployeeSalarySetup()
            'Transcation Table Field Creation
            DailyAttendance()
            Payroll_Process_In_India()
            'Payroll_Calculation()
            'GLMapping()

        End Sub

#Region "Master Data Creation"

        Private Sub UDF_StandardTable()
            'SAP Masters
            AddFields("OCRY", "HR", "HR", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, , , , True) 'Country Master
            AddFields("OLCT", "HR", "HR", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, , , , True) 'Location Master
            AddFields("OFPR", "HR", "HR", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, , , , True) 'Pay Period Master
            AddFields("OADM", "DBUser", "DB UserName", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("OADM", "DBPass", "DB Password", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("OADM", "PayInd", "PayrollIndia Enable", SAPbobsCOM.BoFieldTypes.db_Alpha, 3, , , "N", True)

            AddFields("OUDP", "costcode", "Department Cost Center", SAPbobsCOM.BoFieldTypes.db_Alpha, 100) 'Cost Center in Department
            AddFields("OUBR", "costcode", "Location Cost Center", SAPbobsCOM.BoFieldTypes.db_Alpha, 100) 'Cost Center in Location(Branch)

            AddFields("RCT4", "lineno", "Base Line No", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("RCT4", "PaymentType", "Payment Type", SAPbobsCOM.BoFieldTypes.db_Alpha, 10, , , , False, {"NA,Not Applicable", "LA,Loan Application", "LS,Leave Settlement", "FS,Final Settlement"})
            AddFields("RCT4", "BaseNum", "Doc Number", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("RCT4", "BaseEntry", "Base Entry", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("OJDT", "Narration", "Narration", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
        End Sub

        Private Sub CalendarMaster()

            AddTables("MIPL_OCAL", "Cal Master Header", SAPbobsCOM.BoUTBTableType.bott_Document)
            AddTables("MIPL_CAL1", "Cal Master Lines", SAPbobsCOM.BoUTBTableType.bott_DocumentLines)

            AddFields("@MIPL_OCAL", "MonName", "Month Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 15)
            AddFields("@MIPL_OCAL", "Year", "Year", SAPbobsCOM.BoFieldTypes.db_Numeric, 10)
            AddFields("@MIPL_CAL1", "Branch", "Branch Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 15)
            AddFields("@MIPL_CAL1", "BranchName", "Branch Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@MIPL_CAL1", "TotWDays", "Total Workable Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)

            'AddFields("@MIPL_CALM", "FromDate", "From Date", SAPbobsCOM.BoFieldTypes.db_Date)
            'AddFields("@MIPL_CALM", "ToDate", "To Date", SAPbobsCOM.BoFieldTypes.db_Date)
            'AddFields("@MIPL_CALM", "WOLeave", "Weekoff Leave", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            'AddFields("@MIPL_CALM", "PubHolLv", "Public Holidays", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            'AddFields("@MIPL_CALM", "OPayLv", "Other Payable Leave", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)

            '----------------------UDO Creation
            AddUDO("CALM", "Calendar Master", SAPbobsCOM.BoUDOObjType.boud_Document, "MIPL_OCAL", {"MIPL_CAL1"}, {"DocEntry", "DocNum"}, True, False)
        End Sub

        Private Sub IDCARDTYPE_MasterData()
            AddTables("SMPR_OIDM", "HR Id Type Master", SAPbobsCOM.BoUTBTableType.bott_MasterData)

            '----------------------UDO Creation
            AddUDO("OIDM", "HRMS Id Type Master", SAPbobsCOM.BoUDOObjType.boud_MasterData, "SMPR_OIDM", Nothing, {"Code", "Name"}, True, False)
        End Sub

        Private Sub GLMapping()
            AddTables("MIPL_GL", "GL Master", SAPbobsCOM.BoUTBTableType.bott_MasterData)
            AddFields("@MIPL_GL", "GLC1", "GL Code1", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@MIPL_GL", "GLN1", "GL Name1", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@MIPL_GL", "GLC2", "GL Code2", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@MIPL_GL", "GLN2", "GL Name2", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@MIPL_GL", "GLC3", "GL Code3", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@MIPL_GL", "GLN3", "GL Name3", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@MIPL_GL", "GLC4", "GL Code4", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@MIPL_GL", "GLN4", "GL Name4", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@MIPL_GL", "GLC5", "GL Code5", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@MIPL_GL", "GLN5", "GL Name5", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@MIPL_GL", "GLC6", "GL Code6", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@MIPL_GL", "GLN6", "GL Name6", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@MIPL_GL", "GLC7", "GL Code7", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@MIPL_GL", "GLN7", "GL Name7", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@MIPL_GL", "GLC8", "GL Code8", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@MIPL_GL", "GLN8", "GL Name8", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@MIPL_GL", "GLC9", "GL Code9", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@MIPL_GL", "GLN9", "GL Name9", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@MIPL_GL", "GLC10", "GL Code10", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@MIPL_GL", "GLN10", "GL Name10", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@MIPL_GL", "GLC11", "GL Code11", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@MIPL_GL", "GLN11", "GL Name11", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@MIPL_GL", "GLC12", "GL Code12", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@MIPL_GL", "GLN12", "GL Name12", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            '----------------------UDO Creation
            AddUDO("OGLM", "HRMS GL Master", SAPbobsCOM.BoUDOObjType.boud_MasterData, "MIPL_GL", Nothing, {"Code", "Name"}, True, False)
        End Sub

        Private Sub Skill_MasterData()
            AddTables("SMPR_OSKL", "HR Skill Master", SAPbobsCOM.BoUTBTableType.bott_MasterData)

            AddFields("@SMPR_OSKL", "skilCode", "Skills Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@SMPR_OSKL", "Desc", "Description", SAPbobsCOM.BoFieldTypes.db_Alpha, 250)
            '----------------------UDO Creation
            AddUDO("OSKL", "HRMS Skill Master", SAPbobsCOM.BoUDOObjType.boud_MasterData, "SMPR_OSKL", Nothing, {"Code", "Name", "U_skilCode", "U_Desc"}, True, False)
        End Sub

        Private Sub PF_MasterData()
            AddTables("MIPL_PF", "PF Master", SAPbobsCOM.BoUTBTableType.bott_MasterData)

            AddFields("@MIPL_PF", "FromLimit", "From Salary Limit", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@MIPL_PF", "ToLimit", "To Salary Limit", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@MIPL_PF", "PF", "PF", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Percentage)
            AddFields("@MIPL_PF", "Amount", "Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@MIPL_PF", "Active", "Active", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            AddFields("@MIPL_PF", "FixAmt", "Fixed Amount", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            '----------------------UDO Creation
            AddUDO("MIPF", "PF Master", SAPbobsCOM.BoUDOObjType.boud_MasterData, "MIPL_PF", Nothing, {"Code", "Name", "U_FromLimit", "U_ToLimit"}, True, False)
        End Sub

        Private Sub Deduction_MasterData()
            AddTables("MIPL_PTM", "Deduction Header", SAPbobsCOM.BoUTBTableType.bott_Document)
            AddTables("MIPL_PTM1", "Deduction Lines 1", SAPbobsCOM.BoUTBTableType.bott_DocumentLines)
            AddTables("MIPL_PTM2", "Deduction Lines 2", SAPbobsCOM.BoUTBTableType.bott_DocumentLines)

            AddFields("@MIPL_PTM", "Catgry", "Category", SAPbobsCOM.BoFieldTypes.db_Alpha, 60, , , , , {"M,Monthly", "Q,Quaterly", "H,Halfyearly"})
            AddFields("@MIPL_PTM", "Active", "Active", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            AddFields("@MIPL_PTM", "FromDate", "From Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@MIPL_PTM", "ToDate", "To Date", SAPbobsCOM.BoFieldTypes.db_Date)

            AddFields("@MIPL_PTM1", "FromLimit", "From Salary Limit", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@MIPL_PTM1", "ToLimit", "To Salary Limit", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@MIPL_PTM1", "Amount", "Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@MIPL_PTM1", "Location", "Location", SAPbobsCOM.BoFieldTypes.db_Alpha, 15)
            AddFields("@MIPL_PTM1", "LocName", "Location Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@MIPL_PTM1", "Branch", "Branch Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 15)
            AddFields("@MIPL_PTM1", "BranchName", "Branch Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)

            AddFields("@MIPL_PTM2", "FromLimit", "From Salary Limit", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@MIPL_PTM2", "ToLimit", "To Salary Limit", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@MIPL_PTM2", "Location", "Location", SAPbobsCOM.BoFieldTypes.db_Alpha, 15)
            AddFields("@MIPL_PTM2", "LocName", "Location Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@MIPL_PTM2", "Branch", "Branch Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 15)
            AddFields("@MIPL_PTM2", "BranchName", "Branch Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@MIPL_PTM2", "EmpePf", "Employee Pf", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Percentage)
            AddFields("@MIPL_PTM2", "EmpeEsi", "Employee Esi", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Percentage)
            AddFields("@MIPL_PTM2", "EmprPF", "Employer PF", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Percentage)
            AddFields("@MIPL_PTM2", "EmprEsi", "Employer Esi", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Percentage)
            AddFields("@MIPL_PTM2", "EnAmtInPf", "Enable Amt In Pf", SAPbobsCOM.BoFieldTypes.db_Alpha, 3, , , "N", True)
            AddFields("@MIPL_PTM2", "EmpePfLc", "Employee Pf in Amt", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PTM2", "EmprPfLc", "Employer Pf in Amt", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)


            '----------------------UDO Creation
            AddUDO("MIPTM", "Deduction Master", SAPbobsCOM.BoUDOObjType.boud_Document, "MIPL_PTM", {"MIPL_PTM1", "MIPL_PTM2"}, {"DocEntry", "DocNum"}, True, True)
        End Sub

        Private Sub Emp_Leave_MasterData()
            AddTables("MIPL_OLM", "Emp_Leave_Master Header", SAPbobsCOM.BoUTBTableType.bott_MasterData)
            AddTables("MIPL_OLM1", "Emp_Leave_Master Lines 1", SAPbobsCOM.BoUTBTableType.bott_MasterDataLines)

            AddFields("@MIPL_OLM", "Location", "Location", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@MIPL_OLM", "Branch", "Branch Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)

            AddFields("@MIPL_OLM1", "EmpCode", "Employee Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@MIPL_OLM1", "Empid", "Employee Id", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@MIPL_OLM1", "EmpName", "Employee Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@MIPL_OLM1", "EL", "Earned Leave", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_OLM1", "PH", "Paid Holiday", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_OLM1", "CO", "Comp-Off Leave", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            'Create_UDFInSAP("@MIPL_OLM1")
            '----------------------UDO Creation
            AddUDO("MIOLM", "Emp Leave Master", SAPbobsCOM.BoUDOObjType.boud_MasterData, "MIPL_OLM", {"MIPL_OLM1"}, {"Code", "Name"}, True, False)
        End Sub

        Private Sub Create_UDFInSAP(ByVal TableName As String)
            Try
                Dim strsql As String
                Dim objrs As SAPbobsCOM.Recordset = Nothing
                Dim oSelectedDT As New DataTable
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strsql = "Select ""Code"",""Name"" ""ColName"",'U_'||""Code"" ""FieldName"" from ""@SMPR_OLVE"" where ""U_Active""='Y' and ""U_empmastr""='Y'"
                objrs.DoQuery(strsql)
                If objrs.RecordCount > 0 Then
                    oSelectedDT.Clear()
                    If oSelectedDT.Columns.Count = 0 Then
                        oSelectedDT.Columns.Add("code", GetType(String))
                        oSelectedDT.Columns.Add("name", GetType(String))
                    End If
                    For Rec As Integer = 0 To objrs.RecordCount - 1
                        Dim oRow As DataRow = oSelectedDT.NewRow
                        oRow.Item("code") = Trim(objrs.Fields.Item("Code").Value)
                        oRow.Item("name") = Trim(objrs.Fields.Item("ColName").Value)
                        oSelectedDT.Rows.Add(oRow)
                        objrs.MoveNext()
                    Next
                    objrs = Nothing
                    Dim Code As String = oSelectedDT.Rows(0)("code").ToString 'Trim(objrs.Fields.Item("Code").Value)
                    Dim ColName As String = oSelectedDT.Rows(0)("name").ToString ' Trim(objrs.Fields.Item("ColName").Value)

                    AddFields(TableName, Code, ColName, SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
                    For Rec As Integer = 0 To objrs.RecordCount - 1
                        'AddFields(TableName, Trim(objrs.Fields.Item("Code").Value.ToString), Trim(objrs.Fields.Item("ColName").Value.ToString), SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
                        objrs.MoveNext()
                    Next
                End If
                'AddFields(TableName, Code, ColName, SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            Catch ex As Exception
            End Try
        End Sub

        Private Sub ESI_MasterData()
            AddTables("MIPL_ESI", "ESI Master", SAPbobsCOM.BoUTBTableType.bott_MasterData)

            AddFields("@MIPL_ESI", "FromLimit", "From Salary Limit", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@MIPL_ESI", "ToLimit", "To Salary Limit", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@MIPL_ESI", "ESI", "ESI", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Percentage)
            AddFields("@MIPL_ESI", "Active", "Active", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            '----------------------UDO Creation
            AddUDO("MIESI", "ESI Master", SAPbobsCOM.BoUDOObjType.boud_MasterData, "MIPL_ESI", Nothing, {"Code", "Name", "U_FromLimit", "U_ToLimit"}, True, False)
        End Sub

        Private Sub Grade_MasterData()
            AddTables("SMPR_OGRA", "HR Grade Master", SAPbobsCOM.BoUTBTableType.bott_MasterData)

            AddFields("@SMPR_OGRA", "FromLimit", "Skills Code", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_OGRA", "ToLimit", "Description", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_OGRA", "Remarks", "Remarks", SAPbobsCOM.BoFieldTypes.db_Memo)
            '----------------------UDO Creation
            AddUDO("OGRA", "HRMS Grade Master", SAPbobsCOM.BoUDOObjType.boud_MasterData, "SMPR_OGRA", Nothing, {"Code", "Name", "U_FromLimit", "U_ToLimit"}, True, False)
        End Sub

        Private Sub Loan_MasterData()
            AddTables("SMPR_OLON", "HR Loan Master", SAPbobsCOM.BoUTBTableType.bott_MasterData)

            AddFields("@SMPR_OLON", "MxAmt", "Max Amount", SAPbobsCOM.BoFieldTypes.db_Numeric, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_OLON", "InstRate", "Rate of Interest", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Percentage)
            AddFields("@SMPR_OLON", "MnPayAmt", "Min Repayment Amt", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_OLON", "MxInstal", "Max No of Installment", SAPbobsCOM.BoFieldTypes.db_Numeric, 10)
            AddFields("@SMPR_OLON", "Remarks", "Remarks", SAPbobsCOM.BoFieldTypes.db_Memo)
            AddFields("@SMPR_OLON", "Active", "Active", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)

            '----------------------UDO Creation
            AddUDO("OLON", "HRMS Loan Master", SAPbobsCOM.BoUDOObjType.boud_MasterData, "SMPR_OLON", Nothing, {"Code", "Name"}, True, False)
        End Sub

        Private Sub PayElement_MasterData()
            AddTables("SMPR_OPYE", "HR Pay Element Master", SAPbobsCOM.BoUTBTableType.bott_MasterData)

            AddFields("@SMPR_OPYE", "Type", "Emlement Type", SAPbobsCOM.BoFieldTypes.db_Alpha, 10, , , , , {"A,Addition", "D,Deduction", "S,Salary"})
            AddFields("@SMPR_OPYE", "PaidCate", "Paid Category", SAPbobsCOM.BoFieldTypes.db_Alpha, 10, , , , , {"M,Monthly", "Q,Quaterly", "A,Annualy"})
            AddFields("@SMPR_OPYE", "Sequence", "Sequence", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            AddFields("@SMPR_OPYE", "Loanper", "Loan Eligibility %", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Percentage)
            AddFields("@SMPR_OPYE", "Active", "Active", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)

            '----------------------UDO Creation
            AddUDO("OPYE", "HRMS Pay Element Master", SAPbobsCOM.BoUDOObjType.boud_MasterData, "SMPR_OPYE", Nothing, {"Code", "Name"}, True, False)
        End Sub

        Private Sub Shift_MasterData()
            AddTables("SMHR_OSFT", "HR Shift Master", SAPbobsCOM.BoUTBTableType.bott_MasterData)

            AddFields("@SMHR_OSFT", "Type", "Type", SAPbobsCOM.BoFieldTypes.db_Alpha, 10, , , , , {"D,Day Shift", "N,Night Shift"})

            'AddFields("@SMHR_OSFT", "FromTime", "From Time", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Sum)
            'AddFields("@SMHR_OSFT", "ToTime", "To Time", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Sum)

            'AddFields("@SMHR_OSFT", "ShiftHrs", "Shift Hours", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Sum)
            'AddFields("@SMHR_OSFT", "LStarttime", "Lunch Start time", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Sum)
            'AddFields("@SMHR_OSFT", "LEndtime", "Lunch End Time", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Sum)
            'AddFields("@SMHR_OSFT", "LunchHrs", "Lunch Time[Hours]", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Sum)
            'AddFields("@SMHR_OSFT", "Grace", "Grace", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Sum)

            AddFields("@SMHR_OSFT", "Include", "Include Break Hours", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            AddFields("@SMHR_OSFT", "Active", "Active", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)

            AddFields("@SMHR_OSFT", "FromTime", "From Time", SAPbobsCOM.BoFieldTypes.db_Date, , SAPbobsCOM.BoFldSubTypes.st_Time)
            AddFields("@SMHR_OSFT", "ToTime", "To Time", SAPbobsCOM.BoFieldTypes.db_Date, , SAPbobsCOM.BoFldSubTypes.st_Time)
            AddFields("@SMHR_OSFT", "ShiftHrs", "Shift Hours", SAPbobsCOM.BoFieldTypes.db_Date, , SAPbobsCOM.BoFldSubTypes.st_Time)
            AddFields("@SMHR_OSFT", "LunchHrs", "Lunch Time[Hours]", SAPbobsCOM.BoFieldTypes.db_Date, , SAPbobsCOM.BoFldSubTypes.st_Time)
            AddFields("@SMHR_OSFT", "LStarttime", "Lunch Start time", SAPbobsCOM.BoFieldTypes.db_Date, , SAPbobsCOM.BoFldSubTypes.st_Time)
            AddFields("@SMHR_OSFT", "LEndtime", "Lunch End Time", SAPbobsCOM.BoFieldTypes.db_Date, , SAPbobsCOM.BoFldSubTypes.st_Time)
            AddFields("@SMHR_OSFT", "Grace", "Grace", SAPbobsCOM.BoFieldTypes.db_Date, , SAPbobsCOM.BoFldSubTypes.st_Time)

            AddFields("@SMHR_OSFT", "Remarks", "Remarks", SAPbobsCOM.BoFieldTypes.db_Memo)

            '----------------------UDO Creation
            AddUDO("OSFT", "HRMS Shift Master", SAPbobsCOM.BoUDOObjType.boud_MasterData, "SMHR_OSFT", Nothing, {"Code", "Name"}, True, False)
        End Sub

        Private Sub Leave_MasterData()
            AddTables("SMPR_OLVE", "HR Leave Master", SAPbobsCOM.BoUTBTableType.bott_MasterData)

            AddFields("@SMPR_OLVE", "TotalLve", "Total Leaves in Year", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_OLVE", "MxLveFwd", "Max Leave to Forward", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_OLVE", "Calculation", "Calculation", SAPbobsCOM.BoFieldTypes.db_Alpha, 10, , , , , {"F,From Date", "T,To Date"})
            AddFields("@SMPR_OLVE", "Remarks", "Remarks", SAPbobsCOM.BoFieldTypes.db_Memo)

            AddFields("@SMPR_OLVE", "FwdNxtYr", "Forward to Next Year", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, , , , True)
            AddFields("@SMPR_OLVE", "Payable", "Payable", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, , , , True)
            AddFields("@SMPR_OLVE", "Active", "Active", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, , , , True)
            AddFields("@SMPR_OLVE", "HalfDay", "HalfDay", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, , , , True)
            AddFields("@SMPR_OLVE", "empmastr", "Employee Master", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, , , , True)
            AddFields("@SMPR_OLVE", "Sequence", "Sequence", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)

            '----------------------UDO Creation
            AddUDO("OLVE", "Payroll Leave Master", SAPbobsCOM.BoUDOObjType.boud_MasterData, "SMPR_OLVE", Nothing, {"Code", "Name"}, True, False)
        End Sub

        Private Sub EmployeeMasterdata()

            'Employee Master Data Table Creation
            AddTables("SMPR_OHEM", "Employee Master", SAPbobsCOM.BoUTBTableType.bott_MasterData)

            AddTables("SMPR_HEM1", "Employee Salary Details", SAPbobsCOM.BoUTBTableType.bott_MasterDataLines)
            AddTables("SMPR_HEM2", "Employee Leave Details", SAPbobsCOM.BoUTBTableType.bott_MasterDataLines)
            AddTables("SMPR_HEM3", "Employee Id Details", SAPbobsCOM.BoUTBTableType.bott_MasterDataLines)
            AddTables("SMPR_HEM4", "Employee Skill Details", SAPbobsCOM.BoUTBTableType.bott_MasterDataLines)
            AddTables("SMPR_HEM5", "Employee Training Details", SAPbobsCOM.BoUTBTableType.bott_MasterDataLines)
            AddTables("SMPR_HEM6", "Employee Family Details", SAPbobsCOM.BoUTBTableType.bott_MasterDataLines)
            AddTables("SMPR_HEM7", "Employee Grievance", SAPbobsCOM.BoUTBTableType.bott_MasterDataLines)
            AddTables("SMPR_HEM8", "Employee Education Details", SAPbobsCOM.BoUTBTableType.bott_MasterDataLines)
            AddTables("SMPR_HEM9", "Employee Previous Employment", SAPbobsCOM.BoUTBTableType.bott_MasterDataLines)
            AddTables("SMPR_HEM10", "Employee Air Ticket Details", SAPbobsCOM.BoUTBTableType.bott_MasterDataLines)

            '--------------------------------Heaader Table Field Details-------------------------------------------------------------------------
            '-----------Basic Employee Details
            AddFields("@SMPR_OHEM", "empID", "empID", SAPbobsCOM.BoFieldTypes.db_Alpha, 11)
            AddFields("@SMPR_OHEM", "ExtEmpNo", "Employee ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "firstNam", "First Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "lastName", "Last Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "gropCode", "Employee Group Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 60, , , , , {"GS,General Staff", "MM,Middle Management", "SM,Senior Management", "WR,Workers"})
            AddFields("@SMPR_OHEM", "jobTitle", "Designation", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "position", "Position", SAPbobsCOM.BoFieldTypes.db_Numeric, 6, SAPbobsCOM.BoFldSubTypes.st_None)
            AddFields("@SMPR_OHEM", "dept", "Department", SAPbobsCOM.BoFieldTypes.db_Numeric, 6, SAPbobsCOM.BoFldSubTypes.st_None)
            AddFields("@SMPR_OHEM", "branch", "Branch", SAPbobsCOM.BoFieldTypes.db_Numeric, 6, SAPbobsCOM.BoFldSubTypes.st_None)
            AddFields("@SMPR_OHEM", "manager", "Manager", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "userid", "User ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "slpcode", "Sales Employee", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            AddFields("@SMPR_OHEM", "PFStat", "PF Status", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            AddFields("@SMPR_OHEM", "TDSStat", "TDS Status", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            AddFields("@SMPR_OHEM", "ESIStat", "ESI Status", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            AddFields("@SMPR_OHEM", "PTStat", "PT Status", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)

            '-----------Administration Details
            AddFields("@SMPR_OHEM", "startdte", "Start Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OHEM", "status", "Status", SAPbobsCOM.BoFieldTypes.db_Alpha, 11)
            AddFields("@SMPR_OHEM", "probmnth", "Probation Month", SAPbobsCOM.BoFieldTypes.db_Numeric, 6, SAPbobsCOM.BoFldSubTypes.st_None)
            AddFields("@SMPR_OHEM", "probdate", "Probation Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OHEM", "probexdt", "Probation Extenstion Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OHEM", "conenddt", "Contract End Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OHEM", "termdate", "Termination Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OHEM", "termreas", "Termination Reason", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "resgdate", "Resignation Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OHEM", "noteperd", "Notice Period Days", SAPbobsCOM.BoFieldTypes.db_Numeric, 6, SAPbobsCOM.BoFldSubTypes.st_None)
            AddFields("@SMPR_OHEM", "termtype", "Termination Type", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, , , , , {"RG,Resignation", "EC,End Of Contract", "TM,Termination", "TR,Termination Article 120", "-,"})
            AddFields("@SMPR_OHEM", "meddetai", "Medical Details", SAPbobsCOM.BoFieldTypes.db_Memo)
            '-----------Additional Details
            AddFields("@SMPR_OHEM", "location", "Location", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "shiftcde", "Shift Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "OT", "OT", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            AddFields("@SMPR_OHEM", "grade", "Grade", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@SMPR_OHEM", "subgrad1", "Sub Grade 1", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            AddFields("@SMPR_OHEM", "subgrad2", "Sub Grade 2", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            AddFields("@SMPR_OHEM", "fandf", "Final Settlement", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            AddFields("@SMPR_OHEM", "campcode", "Camp Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_OHEM", "roomno", "Room No", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_OHEM", "approved", "Approved User", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            AddFields("@SMPR_OHEM", "loanelgi", "Loan Eligible", SAPbobsCOM.BoFieldTypes.db_Alpha, 11)
            '-----------Personal Details
            AddFields("@SMPR_OHEM", "pbirthdt", "Personal Birth Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OHEM", "obirthdt", "official Birth Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OHEM", "brthCont", "Country of Birth", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "citizen", "Citizenship", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "mrstatus", "Marital Status", SAPbobsCOM.BoFieldTypes.db_Alpha, 20, , , , , {"S,Single", "M,Married", "D,Divorced", "W,Widowed"})
            AddFields("@SMPR_OHEM", "noofchld", "No of Child", SAPbobsCOM.BoFieldTypes.db_Numeric, 6, SAPbobsCOM.BoFldSubTypes.st_None)
            AddFields("@SMPR_OHEM", "sex", "Gender", SAPbobsCOM.BoFieldTypes.db_Alpha, 10, , , , , {"M,Male", "F,Female"})
            AddFields("@SMPR_OHEM", "bloodgrp", "Blood Group", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_OHEM", "religion", "Religion", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "empimage", "Employee Picture", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_Image)
            '-----------Contact Details
            AddFields("@SMPR_OHEM", "oficetel", "Office Phone", SAPbobsCOM.BoFieldTypes.db_Alpha, 40)
            AddFields("@SMPR_OHEM", "oficeext", "Office Ext.", SAPbobsCOM.BoFieldTypes.db_Alpha, 40)
            AddFields("@SMPR_OHEM", "mobile", "Mobile phone", SAPbobsCOM.BoFieldTypes.db_Alpha, 40)
            AddFields("@SMPR_OHEM", "pager", "Native Contact No", SAPbobsCOM.BoFieldTypes.db_Alpha, 40)
            AddFields("@SMPR_OHEM", "hometel", "Home Phone", SAPbobsCOM.BoFieldTypes.db_Alpha, 40)
            AddFields("@SMPR_OHEM", "fax", "Personal Email 1", SAPbobsCOM.BoFieldTypes.db_Alpha, 40)
            AddFields("@SMPR_OHEM", "email", "E-Mail", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
            AddFields("@SMPR_OHEM", "emconname", "Emergency Contact Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "emconno", "Emergency Contact No", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            '-----------Work Address Details
            AddFields("@SMPR_OHEM", "wstreet", "Work Street", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
            AddFields("@SMPR_OHEM", "wstretno", "Work Street No", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
            AddFields("@SMPR_OHEM", "wbuildng", "Work Building/Floor/Room", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
            AddFields("@SMPR_OHEM", "wblock", "Work Block", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
            AddFields("@SMPR_OHEM", "wzipcode", "Work ZipCode", SAPbobsCOM.BoFieldTypes.db_Alpha, 40)
            AddFields("@SMPR_OHEM", "wcity", "Work City", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
            AddFields("@SMPR_OHEM", "wcounty", "Work County", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
            AddFields("@SMPR_OHEM", "wcountry", "Work Country", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
            AddFields("@SMPR_OHEM", "wtate", "Work State", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "wemgcont", "Work Emergency Contact No", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            '-----------Native Address Details
            AddFields("@SMPR_OHEM", "nstreet", "Native Street", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
            AddFields("@SMPR_OHEM", "nstretno", "Native Street No", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
            AddFields("@SMPR_OHEM", "nbuildng", "Native Building/Floor/Room", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
            AddFields("@SMPR_OHEM", "nblock", "Native Block", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
            AddFields("@SMPR_OHEM", "nzipcode", "Native ZipCode", SAPbobsCOM.BoFieldTypes.db_Alpha, 40)
            AddFields("@SMPR_OHEM", "ncity", "Native City", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
            AddFields("@SMPR_OHEM", "ncounty", "Native County", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
            AddFields("@SMPR_OHEM", "ncountry", "Native Country", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
            AddFields("@SMPR_OHEM", "ntate", "Native State", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "nemgcont", "Native Emergency Contact No", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            '-----------Dashboard Details
            AddFields("@SMPR_OHEM", "photoatt", "Photo Attachement", SAPbobsCOM.BoFieldTypes.db_Memo)
            AddFields("@SMPR_OHEM", "ppfname", "PP FileName Dashboard", SAPbobsCOM.BoFieldTypes.db_Alpha, 250)
            AddFields("@SMPR_OHEM", "ppattach", "PP Attachment Dashboard", SAPbobsCOM.BoFieldTypes.db_Memo)
            AddFields("@SMPR_OHEM", "destplac", "Destination Place", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            '-----------Bank Details
            'AddFields("@SMPR_OHEM", "paymode", "Pay Mode", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, , , , , {"00,", "CA,Cash", "CH,Cheque", "BT,Bank Transfer"})
            AddFields("@SMPR_OHEM", "paymode", "Pay Mode", SAPbobsCOM.BoFieldTypes.db_Alpha, 50)
            AddFields("@SMPR_OHEM", "bankcode", "Bank Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "bankbrch", "Bank Branch", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "bankacct", "Bank Account", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "bankiban", "Bank IBAN No", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "bankfnam", "First Name in Bank A/C", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "banklnam", "Last Name in Bank A/c", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "bankfile", "Bank File Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 250)
            AddFields("@SMPR_OHEM", "PANNum", "PAN Number", SAPbobsCOM.BoFieldTypes.db_Alpha, 250)
            AddFields("@SMPR_OHEM", "PFNum", "PF Number", SAPbobsCOM.BoFieldTypes.db_Alpha, 250)
            AddFields("@SMPR_OHEM", "UANNum", "UAN Number", SAPbobsCOM.BoFieldTypes.db_Alpha, 250)
            AddFields("@SMPR_OHEM", "ESINum", "ESI Number", SAPbobsCOM.BoFieldTypes.db_Alpha, 250)
            AddFields("@SMPR_OHEM", "AadharNum", "Aadhar Number", SAPbobsCOM.BoFieldTypes.db_Alpha, 250)
            AddFields("@SMPR_OHEM", "PTDedType", "PT DedType", SAPbobsCOM.BoFieldTypes.db_Alpha, 20, , , "1", , {"1,One", "2,Two", "3,Three", "4,Four", "5,Five", "6,Six"})

            '--------------------------------Line Table Field Details-------------------------------------------------------------------------
            '-----------Salary Details
            AddFields("@SMPR_OHEM", "PaySlip", "Payslip Auto Mail", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)

            AddFields("@SMPR_HEM1", "PayElCod", "PayElement Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_HEM1", "PayElNam", "Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM1", "EffDate", "EffDate", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_HEM1", "PaidCate", "Paid Category", SAPbobsCOM.BoFieldTypes.db_Alpha, 10, , , "M", False, {"M,Monthly", "Q,Quarterly", "A,Annually"})
            AddFields("@SMPR_HEM1", "PayType", "Payable Type", SAPbobsCOM.BoFieldTypes.db_Alpha, 10, , , "A", False, {"A,Addition", "D,Deduction"})
            AddFields("@SMPR_HEM1", "Amount", "Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_HEM1", "OT", "OT Applicable", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            AddFields("@SMPR_HEM1", "LveSettlement", "Leave Settlement", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            AddFields("@SMPR_HEM1", "FandF", "Full & Final Settlement", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            AddFields("@SMPR_HEM1", "PF", "PF Applicable", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            '-----------Leave Details
            'AddFields("@SMPR_OHEM", "lvstobdt", "Leave Settlement OB date", SAPbobsCOM.BoFieldTypes.db_Date)
            'AddFields("@SMPR_OHEM", "lvstobdy", "Leave Settlement OB days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_HEM2", "LveCode", "Leave Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_HEM2", "LveName", "Leave Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM2", "EmpLvBal", "Emp Leave Balance", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_HEM2", "EmpLvTak", "Emp Leave Taken", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_HEM2", "LeaveTak", "Leave Taken", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_HEM2", "LeaveBal", "Leave Balance", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_HEM2", "CurLeave", "Current month Leave", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_HEM2", "CompOff", "CompOff Leave", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_HEM2", "TotalLve", "Total Leaves in Year", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_HEM2", "FwdNxtYr", "Forward to Next Year", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            AddFields("@SMPR_HEM2", "MxLveFwd", "Max Leave to Forward", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_HEM2", "DOJAfterLveDate", "DOJ After Lve. Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_HEM2", "DOJAfterLveBal", "DOJ After Lve. Balance", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            '----------------------ID Details
            'Header Fields
            AddFields("@SMPR_OHEM", "passpno", "Passport No", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_OHEM", "passexdt", "Passport Expiration Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OHEM", "passisdt", "Passport Issue Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OHEM", "passisur", "Passsport Issuer", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OHEM", "otherscc", "Others Cost Center", SAPbobsCOM.BoFieldTypes.db_Alpha, 100) 'Cost Center in Others
            AddFields("@SMPR_OHEM", "visaspon", "Visa Sponsor", SAPbobsCOM.BoFieldTypes.db_Alpha, 250)
            AddFields("@SMPR_OHEM", "Desgperm", "Designaiton in Work Permit", SAPbobsCOM.BoFieldTypes.db_Alpha, 250)
            'Detail Fields
            AddFields("@SMPR_HEM3", "IDCardType", "Id card Type", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_HEM3", "IDCardName", "Id card Type name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM3", "CardNo", "Card Number", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@SMPR_HEM3", "Company", "Company", SAPbobsCOM.BoFieldTypes.db_Alpha, 250)
            AddFields("@SMPR_HEM3", "IssueDt", "Date of Issue", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_HEM3", "ExpiryDt", "Expiry Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_HEM3", "IssuePlc", "Place of Issue", SAPbobsCOM.BoFieldTypes.db_Alpha, 250)
            AddFields("@SMPR_HEM3", "Remarks", "Remarks", SAPbobsCOM.BoFieldTypes.db_Alpha, 250)
            AddFields("@SMPR_HEM3", "Attach", "Attachment", SAPbobsCOM.BoFieldTypes.db_Memo, 100, SAPbobsCOM.BoFldSubTypes.st_Link)
            '----------------------Skill Details
            AddFields("@SMPR_HEM4", "SkilCode", "Skills Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_HEM4", "SkilName", "Skill Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM4", "Remarks", "Remarks", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            '----------------------Training Details
            AddFields("@SMPR_HEM5", "Training", "Training Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM5", "Certify", "Certificate", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM5", "StartDt", "Start Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_HEM5", "EndDt", "End Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_HEM5", "Institut", "Name of the Institute", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM5", "Remarks", "Remarks", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM5", "attach", "Attachment", SAPbobsCOM.BoFieldTypes.db_Memo, 100, SAPbobsCOM.BoFldSubTypes.st_Link)
            '----------------------Family Details
            AddFields("@SMPR_HEM6", "FmlyName", "Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM6", "Relation", "Relationship", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM6", "Passport", "Passport No", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM6", "PExpDate", "Passport Expiry Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_HEM6", "VisaNo", "VisaNo", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM6", "VExpDate", "Visa Expiry Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_HEM6", "VisaType", "Type of visa", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM6", "Contact", "Contact Number", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_HEM6", "IDNo", "IDNo", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM6", "EmiratId", "Emirat ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM6", "OthrInfo", "Other Information", SAPbobsCOM.BoFieldTypes.db_Alpha, 254)
            AddFields("@SMPR_HEM6", "InsureNo", "Insurance No", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM6", "Remarks", "remarks", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            '----------------------Education Details
            AddFields("@SMPR_HEM8", "edufmdte", "Education From Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_HEM8", "edutodte", "Education To Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_HEM8", "edutype", "Education Type", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_HEM8", "institue", "Institue", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM8", "major", "Major", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM8", "diploma", "Diploma", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM8", "qualcode", "Qualification Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM8", "qualname", "Qualification Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM8", "dscriptn", "Description", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM8", "attach1", "Attachment 1", SAPbobsCOM.BoFieldTypes.db_Memo, 100, SAPbobsCOM.BoFldSubTypes.st_Link)
            AddFields("@SMPR_HEM8", "attach2", "Attachment 2", SAPbobsCOM.BoFieldTypes.db_Memo, 100, SAPbobsCOM.BoFldSubTypes.st_Link)
            AddFields("@SMPR_HEM8", "attach3", "Attachment 3", SAPbobsCOM.BoFieldTypes.db_Memo, 100, SAPbobsCOM.BoFldSubTypes.st_Link)
            '----------------------Previous Employment Details
            AddFields("@SMPR_HEM9", "empolyfr", "Employment From Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_HEM9", "empolyto", "Employment To Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_HEM9", "employer", "Employer", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM9", "position", "Position", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM9", "remarks", "Remarks", SAPbobsCOM.BoFieldTypes.db_Memo)
            AddFields("@SMPR_HEM9", "experien", "Experience", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM9", "refcheck", "Reference Check", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            AddFields("@SMPR_HEM9", "ref1", "Reference 1", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM9", "ref2", "Reference 2", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM9", "ref3", "Reference 3", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_HEM9", "attach1", "Attachment 1", SAPbobsCOM.BoFieldTypes.db_Memo, 100, SAPbobsCOM.BoFldSubTypes.st_Link)
            AddFields("@SMPR_HEM9", "attach2", "Attachment 2", SAPbobsCOM.BoFieldTypes.db_Memo, 100, SAPbobsCOM.BoFldSubTypes.st_Link)
            AddFields("@SMPR_HEM9", "attach3", "Attachment 3", SAPbobsCOM.BoFieldTypes.db_Memo, 100, SAPbobsCOM.BoFldSubTypes.st_Link)
            '----------------------Air Ticket Details
            AddFields("@SMPR_OHEM", "airlstdt", "Air Ticket Last Settlement date", SAPbobsCOM.BoFieldTypes.db_Date)

            AddFields("@SMPR_HEM10", "fromdate", "Air Ticket From Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_HEM10", "todate", "Air Ticket To Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_HEM10", "nooftckt", "No of Tickets", SAPbobsCOM.BoFieldTypes.db_Numeric, 6, SAPbobsCOM.BoFldSubTypes.st_None)
            AddFields("@SMPR_HEM10", "tcktpryr", "Ticker per Year", SAPbobsCOM.BoFieldTypes.db_Numeric, 6, SAPbobsCOM.BoFldSubTypes.st_None)
            AddFields("@SMPR_HEM10", "eligiamt", "Eligible Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)

            '----------------------UDO Creation
            AddUDO("OHEM", "Employee Master Data", SAPbobsCOM.BoUDOObjType.boud_MasterData, "SMPR_OHEM", {"SMPR_HEM1", "SMPR_HEM2", "SMPR_HEM3", "SMPR_HEM4", "SMPR_HEM5", "SMPR_HEM6", _
                                                "SMPR_HEM7", "SMPR_HEM8", "SMPR_HEM9", "SMPR_HEM10"}, {"Code", "U_ExtEmpNo", "U_empID", "U_firstNam"}, True, False)
        End Sub

        Private Sub AccountMapping()

            AddTables("SMPR_ACCT", "HRMS Account Determination", SAPbobsCOM.BoUTBTableType.bott_MasterData)
            AddTables("SMPR_ACCT1", "HRMS Loan Acct Dete", SAPbobsCOM.BoUTBTableType.bott_MasterDataLines)
            AddTables("SMPR_ACCT2", "HRMS Add ded Acct Dete", SAPbobsCOM.BoUTBTableType.bott_MasterDataLines)
            AddTables("SMPR_ACCT3", "HRMS Pay Acct Dete", SAPbobsCOM.BoUTBTableType.bott_MasterDataLines)

            AddFields("@SMPR_ACCT", "emptype", "Employee Type", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "fromdate", "From Date", SAPbobsCOM.BoFieldTypes.db_Date, 10)
            AddFields("@SMPR_ACCT", "todate", "To Date", SAPbobsCOM.BoFieldTypes.db_Date, 10)
            AddFields("@SMPR_ACCT", "AGCode", "Auto EmpCode", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)

            AddFields("@SMPR_ACCT", "lvesaldc", "Lve Sly Adv Debit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "lvesaldn", "Lve Sly Adv Debit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "lvesalcc", "Lve Sly Adv Credit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "lvesalcn", "Lve Sly Adv Credit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)

            AddFields("@SMPR_ACCT", "lveencdc", "Lve Encash Adv Debit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "lveencdn", "Lve Encash Adv Debit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "lveenccc", "Lve Encash Adv Credit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "lveenccn", "Lve Encash Adv Credit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)

            AddFields("@SMPR_ACCT", "aircladc", "Air Ticket Debit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "aircladn", "Air Ticket Debit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "airclacc", "Air Ticket Credit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "airclacn", "Air Ticket Credit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)

            AddFields("@SMPR_ACCT", "advsaldc", "Advance Salary Debit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "advsaldn", "Advance Salary Debit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "advsalcc", "Advance Salary Credit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "advsalcn", "Advance Salary Credit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)

            AddFields("@SMPR_ACCT", "gratiydc", "Gratuity Debit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "gratiydn", "Gratuity Debit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "gratiycc", "Gratuity Credit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "gratiycn", "Gratuity Credit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)

            AddFields("@SMPR_ACCT", "otdc", "ot Debit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "otdn", "ot Debit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "otcc", "ot Credit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "otcn", "ot Credit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)

            AddFields("@SMPR_ACCT", "tripaldc", "Trip Allowance Debit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "tripaldn", "Trip Allowance Debit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "tripalcc", "Trip Allowance Credit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "tripalcn", "Trip Allowance Credit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)

            AddFields("@SMPR_ACCT", "providay", "Provision Day", SAPbobsCOM.BoFieldTypes.db_Numeric, 6, SAPbobsCOM.BoFldSubTypes.st_None)

            AddFields("@SMPR_ACCT", "lveprvdc", "Lve Provision Debit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "lveprvdn", "Lve Provision Debit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "lveprvcc", "Lve Provision Credit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "lveprvcn", "Lve Provision Credit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)

            AddFields("@SMPR_ACCT", "airprvdc", "Air Ticket Provision Debit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "airprvdn", "Air Ticket Provision Debit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "airprvcc", "Air Ticket Provision Credit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "airprvcn", "Air Ticket Provision Credit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)

            AddFields("@SMPR_ACCT", "graprvdc", "Gratuity Provision Debit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "graprvdn", "Gratuity Provision Debit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "graprvcc", "Gratuity Provision Credit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT", "graprvcn", "Gratuity Provision Credit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)

            AddFields("@SMPR_ACCT1", "loancode", "Loan code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT1", "loandc", "Loan Debit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT1", "loandn", "Loan Debit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT1", "loancc", "Loan Credit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT1", "loancn", "Loan Credit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)

            AddFields("@SMPR_ACCT2", "andncode", "Addition Deduction code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT2", "adddeddc", "Addition Deduction Debit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT2", "adddeddn", "Addition Deduction Debit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT2", "adddedcc", "Addition Deduction Credit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT2", "adddedcn", "Addition Deduction Credit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)

            AddFields("@SMPR_ACCT3", "paycode", "Pay Element code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT3", "payeledc", "Pay Element Debit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT3", "payeledn", "Pay Element Debit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT3", "payelecc", "Pay Element Credit code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_ACCT3", "payelecn", "Pay Element Credit name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)

            '--UDO Creation
            AddUDO("SMPRACCT", "HRMS Account Determination", SAPbobsCOM.BoUDOObjType.boud_MasterData, "SMPR_ACCT", {"SMPR_ACCT1", "SMPR_ACCT2", "SMPR_ACCT3"}, {"Code", "U_emptype", "U_fromdate", "U_todate"}, True, False)
        End Sub
#End Region

#Region "Document Data Creation"

        Private Sub LeaveApplication()
            AddTables("SMPR_OLVA", "Leave Application", SAPbobsCOM.BoUTBTableType.bott_Document)

            AddFields("@SMPR_OLVA", "empID", "Employee Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 11)
            AddFields("@SMPR_OLVA", "IDNo", "IDNo", SAPbobsCOM.BoFieldTypes.db_Alpha, 64)
            AddFields("@SMPR_OLVA", "empName", "Employee Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
            AddFields("@SMPR_OLVA", "Designat", "Designation", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OLVA", "deptment", "Department", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OLVA", "Location", "Location", SAPbobsCOM.BoFieldTypes.db_Alpha, 11)
            AddFields("@SMPR_OLVA", "LveCode", "Leave Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_OLVA", "leavname", "Leave Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OLVA", "FromDate", "Leave From Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OLVA", "Todate", "Leave To Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OLVA", "RejoinDt", "Rejoining Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OLVA", "NoDayLve", "No Day Leave Required", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_OLVA", "EliLvDay", "Eligible Leave Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_OLVA", "BalLeave", "Balance Leave", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)

            AddFields("@SMPR_OLVA", "DocDate", "Doc Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OLVA", "LveConct", "Leave Contact No.", SAPbobsCOM.BoFieldTypes.db_Alpha, 50)
            AddFields("@SMPR_OLVA", "natconno", "Native Contact No", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OLVA", "rempid", "Replace Emp Id", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_OLVA", "rempname", "Replace Emp Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OLVA", "Reason", "Reason", SAPbobsCOM.BoFieldTypes.db_Memo)
            AddFields("@SMPR_OLVA", "trgttype", "Target Type", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_OLVA", "trgtenty", "Target Entry", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)

            AddFields("@SMPR_OLVA", "HalfDay", "HalfDay", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            AddFields("@SMPR_OLVA", "Issue", "Issue Passport", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            AddFields("@SMPR_OLVA", "Payable", "Payable", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            AddFields("@SMPR_OLVA", "Approved", "Approved", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)

            AddUDO("OLVA", "HRMS Leave Application", SAPbobsCOM.BoUDOObjType.boud_Document, "SMPR_OLVA", Nothing, {"DocEntry", "DocNum", "U_empID", "U_IDNo", "U_empName", "U_LveCode", "U_leavname"}, True, True)
        End Sub

        Private Sub LoanApplication()
            AddTables("SMPR_OLOA", "Loan Application", SAPbobsCOM.BoUTBTableType.bott_Document)
            AddTables("SMPR_LOA1", "Loan Application Details", SAPbobsCOM.BoUTBTableType.bott_DocumentLines)

            AddFields("@SMPR_OLOA", "empID", "Employee ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 11)
            AddFields("@SMPR_OLOA", "IDNo", "ID No.", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_OLOA", "empName", "Employee Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
            AddFields("@SMPR_OLOA", "desig", "Designation", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OLOA", "LoanCode", "Loan Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_OLOA", "loanname", "Loan Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OLOA", "LoanAmt", "Loan Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_OLOA", "EffDate", "Effictive Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OLOA", "NoOfInst", "No Of Installment", SAPbobsCOM.BoFieldTypes.db_Numeric, 6, SAPbobsCOM.BoFldSubTypes.st_None)
            AddFields("@SMPR_OLOA", "AmtMonth", "Amount Per Month", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_OLOA", "Approved", "Approved", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            AddFields("@SMPR_OLOA", "deduction", "Only Deduction", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)

            AddFields("@SMPR_OLOA", "DocDate", "Document Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OLOA", "PvPenAmt", "Pending Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_OLOA", "eligiamt", "Total Elgigible Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_OLOA", "PaymentDocNum", "PaymentDocNum", SAPbobsCOM.BoFieldTypes.db_Alpha, 11)
            AddFields("@SMPR_OLOA", "PaymentDocEntry", "PaymentDocEntry", SAPbobsCOM.BoFieldTypes.db_Alpha, 11)
            AddFields("@SMPR_OLOA", "PaymentDate", "PaymentDate", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OLOA", "jeno", "Journal Entry No", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OLOA", "paidamt", "Total Paid Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_OLOA", "PendAmt", "Pending Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)

            AddFields("@SMPR_OLOA", "Remarks", "Remarks", SAPbobsCOM.BoFieldTypes.db_Memo)

            AddFields("@SMPR_LOA1", "Date", "Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_LOA1", "Amount", "Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_LOA1", "PaidAmt", "Paid Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_LOA1", "dedsal", "Deduct in Salary", SAPbobsCOM.BoFieldTypes.db_Alpha, 10, , , "Y", True)
            AddFields("@SMPR_LOA1", "Status", "Status", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            AddFields("@SMPR_LOA1", "Detail", "Details", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_LOA1", "trgttype", "Target Type", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_LOA1", "trgtenty", "Target Entry", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_LOA1", "jeno", "Journal Entry No", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)

            AddUDO("OLOA", "HRMS Loan Application", SAPbobsCOM.BoUDOObjType.boud_Document, "SMPR_OLOA", {"SMPR_LOA1"}, {"DocEntry", "DocNum", "U_empID", "U_IDNo", "U_empName", "U_LoanCode", "U_loanname"}, True, True)
        End Sub

        Private Sub DailyAttendance()
            AddTables("SMPR_ODAS", "Daily Attendance Sheet", SAPbobsCOM.BoUTBTableType.bott_Document)
            AddTables("SMPR_DAS1", "Daily Attendance Sheet lines", SAPbobsCOM.BoUTBTableType.bott_DocumentLines)

            AddFields("@SMPR_ODAS", "AttdDate", "Attend Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_ODAS", "AttdDay", "Attendance Day", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_ODAS", "Location", "Location", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_ODAS", "EmpGroup", "EmpGroup", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_ODAS", "DocDate", "Document Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_ODAS", "Remarks", "Remarks", SAPbobsCOM.BoFieldTypes.db_Memo)
            AddFields("@SMPR_ODAS", "FileName", "FileName", SAPbobsCOM.BoFieldTypes.db_Alpha, 250)
            AddFields("@SMPR_ODAS", "Branch", "Branch Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 50)
            AddFields("@SMPR_ODAS", "AttachFile", "Attachment File", SAPbobsCOM.BoFieldTypes.db_Memo, 100, SAPbobsCOM.BoFldSubTypes.st_Link)

            AddFields("@SMPR_DAS1", "ALineId", "Actual LineId", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@SMPR_DAS1", "AttDate", "Attendance Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_DAS1", "empID", "Employee ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_DAS1", "IDNo", "ID No.", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_DAS1", "empName", "Employee Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_DAS1", "Dept", "Department", SAPbobsCOM.BoFieldTypes.db_Alpha, 11)
            AddFields("@SMPR_DAS1", "Designat", "Designation", SAPbobsCOM.BoFieldTypes.db_Alpha, 11)
            AddFields("@SMPR_DAS1", "Holiday", "Holiday", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            AddFields("@SMPR_DAS1", "Friday", "Friday", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            AddFields("@SMPR_DAS1", "AttStatus", "Attendance", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@SMPR_DAS1", "Halfday", "Half day Leave", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            AddFields("@SMPR_DAS1", "HalfStatus", "Half Day Attd Status", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            AddFields("@SMPR_DAS1", "ShiftCode", "Shift Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_DAS1", "ShiftName", "Shift Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_DAS1", "prjCode", "Project Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_DAS1", "prjName", "Project Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_DAS1", "Branch", "Branch Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 50)
            AddFields("@SMPR_DAS1", "Loc", "Location Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 50)
            'AddFields("@SMPR_DAS1", "shifthrs", "Shift Hours", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Sum)
            'AddFields("@SMPR_DAS1", "TimeIn", "Time In", SAPbobsCOM.BoFieldTypes.db_Numeric, , SAPbobsCOM.BoFldSubTypes.st_Time)
            'AddFields("@SMPR_DAS1", "TimeOut", "Time Out", SAPbobsCOM.BoFieldTypes.db_Numeric, , SAPbobsCOM.BoFldSubTypes.st_Time)
            'AddFields("@SMPR_DAS1", "HrsWrk", "Hours Worked", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_DAS1", "otappl", "OT Applicable", SAPbobsCOM.BoFieldTypes.db_Alpha, 2)
            AddFields("@SMPR_DAS1", "OTHrs", "OT Hours", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Sum)

            AddFields("@SMPR_DAS1", "shifthrs", "Shift Hours", SAPbobsCOM.BoFieldTypes.db_Date, , SAPbobsCOM.BoFldSubTypes.st_Time)
            AddFields("@SMPR_DAS1", "TimeIn", "Time In", SAPbobsCOM.BoFieldTypes.db_Date, , SAPbobsCOM.BoFldSubTypes.st_Time)
            AddFields("@SMPR_DAS1", "TimeOut", "Time Out", SAPbobsCOM.BoFieldTypes.db_Date, , SAPbobsCOM.BoFldSubTypes.st_Time)
            AddFields("@SMPR_DAS1", "HrsWrk", "Hours Worked", SAPbobsCOM.BoFieldTypes.db_Date, , SAPbobsCOM.BoFldSubTypes.st_Time)
            'AddFields("@SMPR_DAS1", "OTHrs", "OT Hours", SAPbobsCOM.BoFieldTypes.db_Date, , SAPbobsCOM.BoFldSubTypes.st_Time)
            AddFields("@SMPR_DAS1", "ActTimeIn", "Actual Time In", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            AddFields("@SMPR_DAS1", "ActTimeOut", "Actual Time Out", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)

            AddUDO("ODAS", "HRMS Daily Attendance Sheet", SAPbobsCOM.BoUDOObjType.boud_Document, "SMPR_ODAS", {"SMPR_DAS1"}, {"DocEntry", "DocNum", "U_AttdDate", "U_AttdDay", "U_Location", "U_DocDate"}, True, True)
        End Sub

        Private Sub Additionanddecution()
            AddTables("SMPR_OPAD", "PayRoll Addition Deduction", SAPbobsCOM.BoUTBTableType.bott_Document)
            AddTables("SMPR_PAD1", "Daily Attendance Sheet lines", SAPbobsCOM.BoUTBTableType.bott_DocumentLines)

            AddFields("@SMPR_OPAD", "PayPerid", "Pay Period", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_OPAD", "FromDate", "From Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OPAD", "ToDate", "To Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OPAD", "DocDate", "Document Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OPAD", "Approved", "Approved", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            AddFields("@SMPR_OPAD", "Remarks", "Remarks", SAPbobsCOM.BoFieldTypes.db_Memo)
            AddFields("@SMPR_OPAD", "CreditAmt", "Total Credit Amount", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_OPAD", "DebitAmt", "Total Debit  Amount", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Sum)

            AddFields("@SMPR_PAD1", "Date", "Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_PAD1", "EmpID", "Employee ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_PAD1", "ExtEmpNo", "Employee Ext ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_PAD1", "EmpName", "Employee Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_PAD1", "Dept", "Department", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_PAD1", "Designat", "Designation", SAPbobsCOM.BoFieldTypes.db_Alpha, 11)
            AddFields("@SMPR_PAD1", "Type", "Type", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, , , , , {"A,Addition", "D,Deduction"})
            AddFields("@SMPR_PAD1", "PayCode", "Pay Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_PAD1", "PayDesc", "Pay Element Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_PAD1", "Amount", "Amount", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PAD1", "Remarks", "Remarks", SAPbobsCOM.BoFieldTypes.db_Memo)

            AddUDO("OPAD", "HRMS Addition Deduction", SAPbobsCOM.BoUDOObjType.boud_Document, "SMPR_OPAD", {"SMPR_PAD1"}, {"DocEntry", "DocNum", "U_PayPerid", "U_FromDate", "U_ToDate", "U_DocDate"}, True, True)
        End Sub

        Private Sub Air_Ticket_Issue()
            AddTables("SMPR_OTIS", "Air Ticket Issue", SAPbobsCOM.BoUTBTableType.bott_Document)

            AddFields("@SMPR_OTIS", "TickDate", "Ticket Issue Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OTIS", "empID", "Employee ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_OTIS", "IDNo", "IDNo", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_OTIS", "empName", "Employee Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OTIS", "Dept", "Department", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OTIS", "Desig", "Designation", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OTIS", "emptype", "Employee Type", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OTIS", "country", "Employee Country", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OTIS", "noofday", "No Of Days", SAPbobsCOM.BoFieldTypes.db_Numeric, 6, SAPbobsCOM.BoFldSubTypes.st_None)
            AddFields("@SMPR_OTIS", "Total", "Total Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_OTIS", "Remarks", "Remarks", SAPbobsCOM.BoFieldTypes.db_Memo)
            AddFields("@SMPR_OTIS", "Status", "Status", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)

            AddFields("@SMPR_OTIS", "DocDate", "Document Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OTIS", "joindate", "Joined Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OTIS", "LastTkDt", "Last Ticket Issued Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OTIS", "LstTkAmt", "Last Ticket Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_OTIS", "nooftckt", "No of Tickets", SAPbobsCOM.BoFieldTypes.db_Numeric, 6, SAPbobsCOM.BoFldSubTypes.st_None)
            AddFields("@SMPR_OTIS", "tcktpryr", "Ticker per Year", SAPbobsCOM.BoFieldTypes.db_Numeric, 6, SAPbobsCOM.BoFldSubTypes.st_None)
            AddFields("@SMPR_OTIS", "eligiamt", "Eligible Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_OTIS", "trgttype", "Target Type", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_OTIS", "trgtenty", "Target Entry", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_OTIS", "payroll", "Consider in Payroll", SAPbobsCOM.BoFieldTypes.db_Alpha, 2)
            AddFields("@SMPR_OTIS", "Approved", "Approved", SAPbobsCOM.BoFieldTypes.db_Alpha, 2)


            AddUDO("OTIS", "HRMS AIR Ticket Issue", SAPbobsCOM.BoUDOObjType.boud_Document, "SMPR_OTIS", Nothing, {"DocEntry", "DocNum", "U_TickDate", "U_empID", "U_IDNo", "U_empName"}, True, True)
        End Sub

        Private Sub Settlement()

            AddTables("SMPR_OLSE", "HRMS Settlement", SAPbobsCOM.BoUTBTableType.bott_Document)
            AddTables("SMPR_LSE1", "Settlment Pay", SAPbobsCOM.BoUTBTableType.bott_DocumentLines)
            AddTables("SMPR_LSE2", "Settlment Salary", SAPbobsCOM.BoUTBTableType.bott_DocumentLines)
            AddTables("SMPR_LSE3", "Settlment Loan Ded", SAPbobsCOM.BoUTBTableType.bott_DocumentLines)
            AddTables("SMPR_LSE4", "Settlment Add/Deduction", SAPbobsCOM.BoUTBTableType.bott_DocumentLines)
            '--Header Fields
            AddFields("@SMPR_OLSE", "setltype", "Settlement Type", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            AddFields("@SMPR_OLSE", "LveSettDate", "Leave Settlement Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OLSE", "EmpID", "EmpID", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@SMPR_OLSE", "IDNo", "ID No.", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@SMPR_OLSE", "EmpName", "EmpName", SAPbobsCOM.BoFieldTypes.db_Alpha, 150)
            AddFields("@SMPR_OLSE", "empdept", "Employee Department", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OLSE", "empdesi", "Employee Designation", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OLSE", "empGrpCode", "employee Group Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OLSE", "empcntry", "Employee Country", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OLSE", "Paid", "Paid Status", SAPbobsCOM.BoFieldTypes.db_Alpha, 3, , , "N", True)

            AddFields("@SMPR_OLSE", "paymode", "Pay Mode", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, , , , , {"00,", "CA,Cash", "CH,Cheque", "C3,C3 Card - Non WPS", "NW,NON-WPS", "WP,WPS", "C3Card-WPS,C3 Card - WPS"})
            'AddFields("@SMPR_OLSE", "bankcode", "Bank Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            'AddFields("@SMPR_OLSE", "bankbrch", "Bank Branch", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OLSE", "bankacct", "Bank Account", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OLSE", "bankiban", "Bank IBAN No", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)

            AddFields("@SMPR_OLSE", "DocDate", "Document Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OLSE", "JoinDate", "Joined Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OLSE", "LastLveSettDt", "Last Lve Settled Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OLSE", "rejoindt", "Rejoining Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OLSE", "PaymentDocNum", "PaymentDocNum", SAPbobsCOM.BoFieldTypes.db_Alpha, 11)
            AddFields("@SMPR_OLSE", "PaymentDocEntry", "PaymentDocEntry", SAPbobsCOM.BoFieldTypes.db_Alpha, 11)
            AddFields("@SMPR_OLSE", "PaymentDate", "PaymentDate", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OLSE", "jeno", "Journal Entry No", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OLSE", "approved", "Approved", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)

            AddFields("@SMPR_OLSE", "pdayslry", "Per Day Salary", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_OLSE", "pmonslry", "Per Month Salary", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            'Leave APplication
            AddFields("@SMPR_OLSE", "LveAppEntry", "Leave App. Entry", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_OLSE", "LveAppNo", "Leave App. No.", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_OLSE", "AppFromDate", "Appproved From Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OLSE", "AppToDate", "Appproved To Date", SAPbobsCOM.BoFieldTypes.db_Date)
            'Leave Encashment
            AddFields("@SMPR_OLSE", "lvncshdt", "Leave Encash Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OLSE", "EliSetDay", "Eligible Settilment Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_OLSE", "lvncshdy", "LeaveEncash Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_OLSE", "lvbalday", "LeaveEncash Bal Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            'AIr Ticket Details
            AddFields("@SMPR_OLSE", "airtktno", "AirTckt Issue No", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_OLSE", "airtkten", "AirTckt Issue Entry", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_OLSE", "airtktdt", "AirTckt Claim Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OLSE", "airtktdy", "AirTckt CLaim Days", SAPbobsCOM.BoFieldTypes.db_Numeric, 6, SAPbobsCOM.BoFldSubTypes.st_None)
            AddFields("@SMPR_OLSE", "airelgib", "AirTckt Eligi Amnt", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            'Smamry Details
            AddFields("@SMPR_OLSE", "Remarks", "Remarks", SAPbobsCOM.BoFieldTypes.db_Memo)
            AddFields("@SMPR_OLSE", "lvsalamt", "Leave Salary Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_OLSE", "lvncshmt", "Leave Encash Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_OLSE", "AiTiketAmt", "Airticket Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_OLSE", "advsalry", "Advance Salary Amnt", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_OLSE", "Retention", "Retention Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_OLSE", "addedamt", "Add/Deduction Amnt", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_OLSE", "gratuity", "Gratuity Amnt", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_OLSE", "TotalAmt", "Total Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            'Gratuity
            AddFields("@SMPR_OLSE", "gtotdays", "Gratuity Total Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_OLSE", "glopdays", "Gratuity LOP Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_OLSE", "gwordays", "Gratuity Working Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_OLSE", "gratdays", "Gratuity Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_OLSE", "pdybasic", "Per Day Basic", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_OLSE", "gremarks", "Gratuity Remarks", SAPbobsCOM.BoFieldTypes.db_Memo)
            'Pay Elements Details
            AddFields("@SMPR_LSE1", "PayElCod", "Pay Element Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_LSE1", "PayElNam", "Pay Element Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_LSE1", "Amount", "Pay Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            'Advance Salary Details
            AddFields("@SMPR_OLSE", "salfrmdt", "Salary From Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OLSE", "saltodt", "Salary To Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_LSE2", "fromdate", "Salary From Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_LSE2", "todate", "Salary To Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_LSE2", "amount", "Salary Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_LSE2", "noofdays", "Salary Days", SAPbobsCOM.BoFieldTypes.db_Numeric, 6, SAPbobsCOM.BoFldSubTypes.st_None)
            AddFields("@SMPR_LSE2", "remarks", "Salary Remarks", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            'Loan Deduction
            AddFields("@SMPR_LSE3", "select", "Select", SAPbobsCOM.BoFieldTypes.db_Alpha, 2)
            AddFields("@SMPR_LSE3", "loanapno", "Loan App No", SAPbobsCOM.BoFieldTypes.db_Numeric, 6, SAPbobsCOM.BoFldSubTypes.st_None)
            AddFields("@SMPR_LSE3", "loanapen", "Loan App Entry", SAPbobsCOM.BoFieldTypes.db_Numeric, 6, SAPbobsCOM.BoFldSubTypes.st_None)
            AddFields("@SMPR_LSE3", "loanline", "Loan App Line No", SAPbobsCOM.BoFieldTypes.db_Numeric, 6, SAPbobsCOM.BoFldSubTypes.st_None)
            AddFields("@SMPR_LSE3", "month", "Loan App Month", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@SMPR_LSE3", "year", "Loan App Year", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_LSE3", "date", "Loan App Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_LSE3", "amount", "Loan App Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_LSE3", "remarks", "Loan App Remarks", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            'Addition Deduction
            AddFields("@SMPR_LSE4", "mode", "Add/Deduction Mode", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            AddFields("@SMPR_LSE4", "type", "Add/Deduction type", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            AddFields("@SMPR_LSE4", "amount", "Add/Deduction Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_LSE4", "remarks", "Add/Deduction Remarks", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_LSE4", "paydate", "Payroll Date", SAPbobsCOM.BoFieldTypes.db_Date, 10)
            AddFields("@SMPR_LSE4", "payroll", "Consider in Payroll", SAPbobsCOM.BoFieldTypes.db_Alpha, 3)

            AddUDO("OLSE", "HRMS Leave\Final Settlement", SAPbobsCOM.BoUDOObjType.boud_Document, "SMPR_OLSE", {"SMPR_LSE1", "SMPR_LSE2", "SMPR_LSE3", "SMPR_LSE4"}, {"DocEntry", "DocNum", "U_setltype", "U_LveSettDate", "U_EmpID", "U_IDNo", "U_EmpName"}, True, True)
        End Sub

        Private Sub Payroll_Process_In_India()
            AddTables("MIPL_OPPI", "Payroll Process India Header", SAPbobsCOM.BoUTBTableType.bott_Document)
            AddTables("MIPL_PPI1", "Payroll Process India Details", SAPbobsCOM.BoUTBTableType.bott_DocumentLines)

            AddFields("@MIPL_OPPI", "PayPeriod", "Pay Period", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@MIPL_OPPI", "FromDate", "From Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@MIPL_OPPI", "ToDate", "To Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@MIPL_OPPI", "DocDate", "Document Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@MIPL_OPPI", "DocEntry", "DocEntry", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@MIPL_OPPI", "Process", "Process", SAPbobsCOM.BoFieldTypes.db_Alpha, 2)
            AddFields("@MIPL_OPPI", "Cancel", "Cancel Payroll", SAPbobsCOM.BoFieldTypes.db_Alpha, 2)
            AddFields("@MIPL_OPPI", "JENo", "JournalEntry Number", SAPbobsCOM.BoFieldTypes.db_Alpha, 50)
            AddFields("@MIPL_OPPI", "Branch", "Branch Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@MIPL_OPPI", "Location", "Location Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)

            AddFields("@MIPL_PPI1", "SalProcess", "Salary Process Status", SAPbobsCOM.BoFieldTypes.db_Alpha, 3, , , "N", True)
            AddFields("@MIPL_PPI1", "IDNo", "ID No.", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@MIPL_PPI1", "empID", "Employee Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@MIPL_PPI1", "empName", "Employee Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
            AddFields("@MIPL_PPI1", "Designat", "Designation", SAPbobsCOM.BoFieldTypes.db_Alpha, 50)
            AddFields("@MIPL_PPI1", "Dept", "Department", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@MIPL_PPI1", "PayMode", "PayMode", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@MIPL_PPI1", "OthrAll", "OthrAll", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)

            AddFields("@MIPL_PPI1", "WorkHrs", "Worked Hours", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PPI1", "DaySal", "Per Day Salary", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "TotHrs", "Total Hours", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PPI1", "LateHrs", "Late Hours", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PPI1", "TDInMonth", "Total Days In Month", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PPI1", "HrSal", "Hours Salary", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "LopHrs", "LOP Hours", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PPI1", "WorkDays", "Worked Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PPI1", "PayLv", "Payable Leave Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PPI1", "PayDays", "Payable Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PPI1", "LeaveBal", "Leave Balance", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PPI1", "LeaveTak", "Leave Taken", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PPI1", "shifthrs", "Shift Hours", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Sum)

            AddFields("@MIPL_PPI1", "TotalDays", "Total Working Days", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PPI1", "PaidDays", "Paid Days", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PPI1", "LOPDays", "LOP Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PPI1", "ELDays", "Elligible Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PPI1", "CODays", "Compoff Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PPI1", "WoDays", "Weekoff Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PPI1", "LOPAmt", "LOP Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            'AddFields("@MIPL_PPI1", "Basic", "Basic", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            'AddFields("@MIPL_PPI1", "HRA", "Houserent Allowance", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            'AddFields("@MIPL_PPI1", "DearAll", "Dearness Allowance", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            'AddFields("@MIPL_PPI1", "MedAll", "Medical Allowance", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            'AddFields("@MIPL_PPI1", "ConAll", "Conveyance Allowance", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "PF", "Provident fund", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "ESI", "ESI", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "EmpPF", "Employer Providentfund", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "EmpESI", "Employer ESI", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "TDS", "TDS", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "GrossSal", "GrossSalary", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "Loan", "Loan", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "ProfTax", "Professional Tax", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "Encash", "Encash", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "NetSal", "NetSalary", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "Bonus", "Bonus", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "Incent", "Incentive", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "Payable", "Payable", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "TotAdd", "Total Addition", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "TotDed", "Total Deduction", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "Roundoff", "Round off", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            'AddFields("@MIPL_PPI1", "ELTaken", "EL Taken", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PPI1", "EmpCont", "Employer Contribution", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "PayLeave", "Payable Leave", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PPI1", "ELBal", "EL Balance", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)

            AddFields("@MIPL_PPI1", "A1", "A1", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "A2", "A2", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "A3", "A3", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "A4", "A4", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "A5", "A5", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "A6", "A6", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "A7", "A7", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "A8", "A8", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "A9", "A9", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "A10", "A10", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "A11", "A11", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "A12", "A12", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "A13", "A13", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "A14", "A14", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "A15", "A15", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "A16", "A16", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "A17", "A17", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "A18", "A18", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "A19", "A19", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "A20", "A20", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)

            AddFields("@MIPL_PPI1", "FPA1", "FPA1", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "FPA2", "FPA2", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "FPA3", "FPA3", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "FPA4", "FPA4", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "FPA5", "FPA5", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "FPA6", "FPA6", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "FPA7", "FPA7", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "FPA8", "FPA8", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "FPA9", "FPA9", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "FPA10", "FPA10", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)


            AddFields("@MIPL_PPI1", "AB1", "AB1", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "AB2", "AB2", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "AB3", "AB3", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "AB4", "AB4", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "AB5", "AB5", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)

            AddFields("@MIPL_PPI1", "DB1", "DB1", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "DB2", "DB2", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "DB3", "DB3", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "DB4", "DB4", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PPI1", "DB5", "DB5", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)


            AddUDO("OPPII", "Payroll Process Sheet", SAPbobsCOM.BoUDOObjType.boud_Document, "MIPL_OPPI", {"MIPL_PPI1"}, {"DocEntry", "DocNum", "U_FromDate", "U_ToDate", "U_DocDate"}, True, True)
        End Sub

        Private Sub Payroll_Process()

            AddTables("SMPR_OPRC", "Payroll Process Sheet Head", SAPbobsCOM.BoUTBTableType.bott_Document)
            AddTables("SMPR_PRC1", "Payroll Process Sheet Details", SAPbobsCOM.BoUTBTableType.bott_DocumentLines)

            AddFields("@SMPR_OPRC", "PayPerid", "Pay Period", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_OPRC", "FromDate", "From Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OPRC", "ToDate", "To Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OPRC", "EmployeeStatus", "Employee Status", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddFields("@SMPR_OPRC", "Process", "Process", SAPbobsCOM.BoFieldTypes.db_Alpha, 2)
            AddFields("@SMPR_OPRC", "APayslip", "Auto PaySlip", SAPbobsCOM.BoFieldTypes.db_Alpha, 2)

            AddFields("@SMPR_OPRC", "DocDate", "Document Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@SMPR_OPRC", "Location", "Location", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddFields("@SMPR_OPRC", "locname", "Location Name", SAPbobsCOM.BoFieldTypes.db_Memo)
            AddFields("@SMPR_OPRC", "jeno", "Journal Entry No", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)

            AddFields("@SMPR_PRC1", "empID", "Employee Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_PRC1", "IDNo", "ID No.", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_PRC1", "empName", "Employee Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
            AddFields("@SMPR_PRC1", "Designat", "Designation", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_PRC1", "Dept", "Department", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_PRC1", "PayMode", "PayMode", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@SMPR_PRC1", "PaySlip", "Payslip Auto Mail", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)

            AddFields("@SMPR_PRC1", "PaidDays", "Paid Days", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_PRC1", "TotalDays", "Total Working Days", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_PRC1", "TDayWrkd", "Total Days Worked", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_PRC1", "HoliDays", "HoliDays", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_PRC1", "LOPDays", "LOPDays", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_PRC1", "WODays", "Work Off Days", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_PRC1", "LveDays", "Leave Days", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@SMPR_PRC1", "EmpLoc", "Employee Location", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)

            AddFields("@SMPR_PRC1", "A1", "A1", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "A2", "A2", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "A3", "A3", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "A4", "A4", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "A5", "A5", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "A6", "A6", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "A7", "A7", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "A8", "A8", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "A9", "A9", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "A10", "A10", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "A11", "A11", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "A12", "A12", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "A13", "A13", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "A14", "A14", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "A15", "A15", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "A16", "A16", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "A17", "A17", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "A18", "A18", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "A19", "A19", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "A20", "A20", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)

            AddFields("@SMPR_PRC1", "ASum", "A Sum", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)


            AddFields("@SMPR_PRC1", "TotalOTAmt", "Total OT Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "TotalOTHrs", "Total OT Hours", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "OTPHR", "OT Per Hour", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Sum)

            AddFields("@SMPR_PRC1", "Addition", "Addition", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "FA1", "Air Ticket Addition", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "FA2", "Trip Allowance Addition", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "AB1", "AB1", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "AB2", "AB2", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "AB3", "AB3", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "AB4", "AB4", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "AB5", "AB5", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "AB6", "AB6", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "AB7", "AB7", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "AB8", "AB8", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "AB9", "AB9", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "AB10", "AB10", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "AB11", "AB11", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "AB12", "AB12", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "AB13", "AB13", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "AB14", "AB14", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "AB15", "AB15", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "AB16", "AB16", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "AB17", "AB17", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "AB18", "AB18", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "AB19", "AB19", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "AB20", "AB20", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)

            AddFields("@SMPR_PRC1", "Deduction", "Deduction", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "FD1", "Loan Deduction", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_PRC1", "FD2", "AL Advance Deduction", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_PRC1", "FD3", "Advance Salary Deduction", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddFields("@SMPR_PRC1", "DB1", "DB1", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "DB2", "DB2", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "DB3", "DB3", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "DB4", "DB4", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "DB5", "DB5", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "DB6", "DB6", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "DB7", "DB7", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "DB8", "DB8", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "DB9", "DB9", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "DB10", "DB10", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "DB11", "DB11", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "DB12", "DB12", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "DB13", "DB13", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "DB14", "DB14", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "DB15", "DB15", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "DB16", "DB16", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "DB17", "DB17", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "DB18", "DB18", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "DB19", "DB19", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "DB20", "DB20", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)

            AddFields("@SMPR_PRC1", "Basic", "Basic", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "totsal", "Total Salary", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "GrossAmt", "Gross Amt.", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "RoundOff", "Round Off", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "NetAmt", "Net Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "Adjloan", "Adjust Loan", SAPbobsCOM.BoFieldTypes.db_Alpha, 3)


            AddFields("@SMPR_PRC1", "TA1", "TA1", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "TA2", "TA2", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "TA3", "TA3", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "TA4", "TA4", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "TA5", "TA5", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "TA6", "TA6", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "TA7", "TA7", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "TA8", "TA8", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "TA9", "TA9", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "TA10", "TA10", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "TA11", "TA11", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "TA12", "TA12", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "TA13", "TA13", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "TA14", "TA14", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "TA15", "TA15", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "TA16", "TA16", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "TA17", "TA17", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "TA18", "TA18", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "TA19", "TA19", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@SMPR_PRC1", "TA20", "TA20", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)

            AddUDO("OPRC", "Payroll Salary Process Sheet", SAPbobsCOM.BoUDOObjType.boud_Document, "SMPR_OPRC", {"SMPR_PRC1"}, {"DocEntry", "DocNum", "U_PayPerid", "U_FromDate", "U_ToDate", "U_DocDate", "U_locname"}, True, True)
        End Sub

        Private Sub Payroll_Calculation()

            AddTables("MIPL_OPCL", "MIPL Payroll Header", SAPbobsCOM.BoUTBTableType.bott_Document)
            AddTables("MIPL_PCL1", "MIPL Payroll Lines", SAPbobsCOM.BoUTBTableType.bott_DocumentLines)

            AddFields("@MIPL_OPCL", "PayPeriod", "Pay Period", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@MIPL_OPCL", "FromDate", "From Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@MIPL_OPCL", "ToDate", "To Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@MIPL_OPCL", "Process", "Process", SAPbobsCOM.BoFieldTypes.db_Alpha, 2)
            AddFields("@MIPL_OPCL", "Branch", "Branch Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            'AddFields("@MIPL_OPAY", "Process", "Process", SAPbobsCOM.BoFieldTypes.db_Alpha, 3, , , "N", True)

            AddFields("@MIPL_OPCL", "DocDate", "Document Date", SAPbobsCOM.BoFieldTypes.db_Date)
            AddFields("@MIPL_OPCL", "JENum", "Journal Entry No", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)

            AddFields("@MIPL_PCL1", "SalProcess", "Salary Process Status", SAPbobsCOM.BoFieldTypes.db_Alpha, 3, , , "N", True)
            AddFields("@MIPL_PCL1", "IDNo", "ID No.", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@MIPL_PCL1", "empID", "Employee Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@MIPL_PCL1", "empName", "Employee Name", SAPbobsCOM.BoFieldTypes.db_Alpha, 200)
            AddFields("@MIPL_PCL1", "Designat", "Designation", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@MIPL_PCL1", "Dept", "Department", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)
            AddFields("@MIPL_PCL1", "PayMode", "PayMode", SAPbobsCOM.BoFieldTypes.db_Alpha, 20)

            AddFields("@MIPL_PCL1", "A1", "A1", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "A2", "A2", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "A3", "A3", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "A4", "A4", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "A5", "A5", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "A6", "A6", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "A7", "A7", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "A8", "A8", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "A9", "A9", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "A10", "A10", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "A11", "A11", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "A12", "A12", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "A13", "A13", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "A14", "A14", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "A15", "A15", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "A16", "A16", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "A17", "A17", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "A18", "A18", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "A19", "A19", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "A20", "A20", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)

            AddFields("@MIPL_PCL1", "WorkHrs", "Worked Hours", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PCL1", "DaySal", "Per Day Salary", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "TotHrs", "Total Hours", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PCL1", "LateHrs", "Late Hours", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            'AddFields("@MIPL_PPC1", "TDInMonth", "Total Days In Month", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PCL1", "HrSal", "Hours Salary", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "LopHrs", "LOP Hours", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PCL1", "WorkDays", "Worked Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PCL1", "PayLv", "Payable Leave Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PCL1", "PayDays", "Payable Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PCL1", "LeaveBal", "Leave Balance", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PCL1", "LeaveTak", "Leave Taken", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PCL1", "shifthrs", "Shift Hours", SAPbobsCOM.BoFieldTypes.db_Float, , SAPbobsCOM.BoFldSubTypes.st_Sum)

            AddFields("@MIPL_PCL1", "TotalDays", "Total Working Days", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PCL1", "PaidDays", "Paid Days", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PCL1", "LOPDays", "LOP Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PCL1", "ELDays", "Elligible Days", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PCL1", "LOPAmt", "LOP Amount", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "PF", "Provident fund", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "ESI", "ESI", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "EmpPF", "Employer Providentfund", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "EmpESI", "Employer ESI", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "TDS", "TDS", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "GrossSal", "GrossSalary", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "Loan", "Loan", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "ProfTax", "Professional Tax", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "NetSal", "NetSalary", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "Bonus", "Bonus", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "Incent", "Incentive", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "Payable", "Payable", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "TotAdd", "Total Addition", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "TotDed", "Total Deduction", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "Roundoff", "Round off", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)
            AddFields("@MIPL_PCL1", "ELTaken", "EL Taken", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Quantity)
            AddFields("@MIPL_PCL1", "RoundOff", "Round Off", SAPbobsCOM.BoFieldTypes.db_Float, 6, SAPbobsCOM.BoFldSubTypes.st_Sum)

            AddUDO("MIPAY", "MIPL Payroll", SAPbobsCOM.BoUDOObjType.boud_Document, "MIPL_OPCL", {"MIPL_PCL1"}, {"DocEntry", "DocNum"}, True, True)
        End Sub

#End Region

#Region "Table Creation Common Functions"
        Private Sub AddTables(ByVal strTab As String, ByVal strDesc As String, ByVal nType As SAPbobsCOM.BoUTBTableType)
            Dim oUserTablesMD As SAPbobsCOM.UserTablesMD

            oUserTablesMD = Nothing

            Try
                oUserTablesMD = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables)

                'Adding Table
                If Not oUserTablesMD.GetByKey(strTab) Then
                    oUserTablesMD.TableName = strTab
                    oUserTablesMD.TableDescription = strDesc
                    oUserTablesMD.TableType = nType

                    If oUserTablesMD.Add <> 0 Then
                        Throw New Exception(objaddon.objcompany.GetLastErrorDescription & strTab)
                    End If
                End If
            Catch ex As Exception
                Throw ex
            Finally
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserTablesMD)
                oUserTablesMD = Nothing
                GC.WaitForPendingFinalizers()
                GC.Collect()
            End Try
        End Sub

        Private Sub AddFields(ByVal strTab As String, ByVal strCol As String, ByVal strDesc As String, ByVal nType As SAPbobsCOM.BoFieldTypes,
                             Optional ByVal nEditSize As Integer = 10, Optional ByVal nSubType As SAPbobsCOM.BoFldSubTypes = 0, Optional ByVal Mandatory As SAPbobsCOM.BoYesNoEnum = SAPbobsCOM.BoYesNoEnum.tNO,
                              Optional ByVal defaultvalue As String = "", Optional ByVal Yesno As Boolean = False, Optional ByVal Validvalues() As String = Nothing)
            Dim oUserFieldMD1 As SAPbobsCOM.UserFieldsMD
            oUserFieldMD1 = Nothing
            GC.Collect()
            oUserFieldMD1 = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields)

            Try
                'oUserFieldMD1 = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields)
                'If Not (strTab = "OPDN" Or strTab = "OQUT" Or strTab = "OADM" Or strTab = "OPOR" Or strTab = "OWST" Or strTab = "OUSR" Or strTab = "OSRN" Or strTab = "OSPP" Or strTab = "WTR1" Or strTab = "OEDG" Or strTab = "OHEM" Or strTab = "OLCT" Or strTab = "ITM1" Or strTab = "OCRD" Or strTab = "SPP1" Or strTab = "SPP2" Or strTab = "RDR1" Or strTab = "ORDR" Or strTab = "OWHS" Or strTab = "OITM" Or strTab = "INV1" Or strTab = "OWTR" Or strTab = "OWDD" Or strTab = "OWOR" Or strTab = "OWTQ" Or strTab = "OMRV" Or strTab = "JDT1" Or strTab = "OIGN" Or strTab = "OCQG") Then
                '    strTab = "@" + strTab
                'End If
                If Not IsColumnExists(strTab, strCol) Then
                    'If Not oUserFieldMD1 Is Nothing Then
                    '    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserFieldMD1)
                    'End If
                    'oUserFieldMD1 = Nothing
                    'oUserFieldMD1 = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields)
                    oUserFieldMD1.Description = strDesc
                    oUserFieldMD1.Name = strCol
                    oUserFieldMD1.Type = nType
                    oUserFieldMD1.SubType = nSubType
                    oUserFieldMD1.TableName = strTab
                    oUserFieldMD1.EditSize = nEditSize
                    oUserFieldMD1.Mandatory = Mandatory
                    oUserFieldMD1.DefaultValue = defaultvalue

                    If Yesno = True Then
                        oUserFieldMD1.ValidValues.Value = "Y"
                        oUserFieldMD1.ValidValues.Description = "Yes"
                        oUserFieldMD1.ValidValues.Add()
                        oUserFieldMD1.ValidValues.Value = "N"
                        oUserFieldMD1.ValidValues.Description = "No"
                        oUserFieldMD1.ValidValues.Add()
                    End If

                    Dim split_char() As String
                    If Not Validvalues Is Nothing Then
                        If Validvalues.Length > 0 Then
                            For i = 0 To Validvalues.Length - 1
                                If Trim(Validvalues(i)) = "" Then Continue For
                                split_char = Validvalues(i).Split(",")
                                If split_char.Length <> 2 Then Continue For
                                oUserFieldMD1.ValidValues.Value = split_char(0)
                                oUserFieldMD1.ValidValues.Description = split_char(1)
                                oUserFieldMD1.ValidValues.Add()
                            Next
                        End If
                    End If
                    Dim val As Integer
                    val = oUserFieldMD1.Add
                    If val <> 0 Then
                        objaddon.objapplication.SetStatusBarMessage("Add Fields: " & objaddon.objcompany.GetLastErrorDescription & " " & strTab & " " & strCol, True)
                        'Else
                        '    objaddon.objapplication.SetStatusBarMessage(objaddon.objcompany.GetLastErrorDescription & strTab & strCol, False)
                    End If
                    'System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserFieldMD1)
                End If
            Catch ex As Exception
                Throw ex
            Finally

                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserFieldMD1)
                oUserFieldMD1 = Nothing
                GC.WaitForPendingFinalizers()
                GC.Collect()
            End Try
        End Sub

        Private Function IsColumnExists(ByVal Table As String, ByVal Column As String) As Boolean
            Dim oRecordSet As SAPbobsCOM.Recordset
            Dim strSQL As String
            Try
                If objaddon.HANA Then
                    strSQL = "SELECT COUNT(*) FROM CUFD WHERE ""TableID"" = '" & Table & "' AND ""AliasID"" = '" & Column & "'"
                Else
                    strSQL = "SELECT COUNT(*) FROM CUFD WHERE TableID = '" & Table & "' AND AliasID = '" & Column & "'"
                End If

                oRecordSet = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                oRecordSet.DoQuery(strSQL)

                If oRecordSet.Fields.Item(0).Value = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw ex
            Finally
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet)
                oRecordSet = Nothing
                GC.WaitForPendingFinalizers()
                GC.Collect()
            End Try
        End Function

        Private Sub AddKey(ByVal strTab As String, ByVal strColumn As String, ByVal strKey As String, ByVal i As Integer)
            Dim oUserKeysMD As SAPbobsCOM.UserKeysMD

            Try
                '// The meta-data object must be initialized with a
                '// regular UserKeys object
                oUserKeysMD = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserKeys)

                If Not oUserKeysMD.GetByKey("@" & strTab, i) Then

                    '// Set the table name and the key name
                    oUserKeysMD.TableName = strTab
                    oUserKeysMD.KeyName = strKey

                    '// Set the column's alias
                    oUserKeysMD.Elements.ColumnAlias = strColumn
                    oUserKeysMD.Elements.Add()
                    oUserKeysMD.Elements.ColumnAlias = "RentFac"

                    '// Determine whether the key is unique or not
                    oUserKeysMD.Unique = SAPbobsCOM.BoYesNoEnum.tYES

                    '// Add the key
                    If oUserKeysMD.Add <> 0 Then
                        Throw New Exception(objaddon.objcompany.GetLastErrorDescription)
                    End If
                End If
            Catch ex As Exception
                Throw ex
            Finally
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserKeysMD)
                oUserKeysMD = Nothing
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try
        End Sub

        Private Sub AddUDO(ByVal strUDO As String, ByVal strUDODesc As String, ByVal nObjectType As SAPbobsCOM.BoUDOObjType, ByVal strTable As String, ByVal childTable() As String, ByVal sFind() As String, _
                           Optional ByVal canlog As Boolean = False, Optional ByVal Manageseries As Boolean = False)

            Dim oUserObjectMD As SAPbobsCOM.UserObjectsMD
            Dim tablecount As Integer = 0
            Try
                oUserObjectMD = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD)
                If oUserObjectMD.GetByKey(strUDO) = 0 Then

                    oUserObjectMD.Code = strUDO
                    oUserObjectMD.Name = strUDODesc
                    oUserObjectMD.ObjectType = nObjectType
                    oUserObjectMD.TableName = strTable

                    oUserObjectMD.CanCancel = SAPbobsCOM.BoYesNoEnum.tNO : oUserObjectMD.CanClose = SAPbobsCOM.BoYesNoEnum.tNO
                    oUserObjectMD.CanCreateDefaultForm = SAPbobsCOM.BoYesNoEnum.tNO : oUserObjectMD.CanDelete = SAPbobsCOM.BoYesNoEnum.tYES

                    If Manageseries Then oUserObjectMD.ManageSeries = SAPbobsCOM.BoYesNoEnum.tYES Else oUserObjectMD.ManageSeries = SAPbobsCOM.BoYesNoEnum.tNO

                    If canlog Then
                        oUserObjectMD.CanLog = SAPbobsCOM.BoYesNoEnum.tYES
                        oUserObjectMD.LogTableName = "A" + strTable.ToString
                    Else
                        oUserObjectMD.CanLog = SAPbobsCOM.BoYesNoEnum.tNO
                        oUserObjectMD.LogTableName = ""
                    End If

                    oUserObjectMD.CanYearTransfer = SAPbobsCOM.BoYesNoEnum.tNO : oUserObjectMD.ExtensionName = ""

                    oUserObjectMD.CanFind = SAPbobsCOM.BoYesNoEnum.tYES
                    tablecount = 1
                    If sFind.Length > 0 Then
                        For i = 0 To sFind.Length - 1
                            If Trim(sFind(i)) = "" Then Continue For
                            oUserObjectMD.FindColumns.ColumnAlias = sFind(i)
                            oUserObjectMD.FindColumns.Add()
                            oUserObjectMD.FindColumns.SetCurrentLine(tablecount)
                            tablecount = tablecount + 1
                        Next
                    End If

                    tablecount = 0
                    If Not childTable Is Nothing Then
                        If childTable.Length > 0 Then
                            For i = 0 To childTable.Length - 1
                                If Trim(childTable(i)) = "" Then Continue For
                                oUserObjectMD.ChildTables.SetCurrentLine(tablecount)
                                oUserObjectMD.ChildTables.TableName = childTable(i)
                                oUserObjectMD.ChildTables.Add()
                                tablecount = tablecount + 1
                            Next
                        End If
                    End If

                    If oUserObjectMD.Add() <> 0 Then
                        Throw New Exception(objaddon.objcompany.GetLastErrorDescription)
                    End If
                End If

            Catch ex As Exception
                Throw ex
            Finally
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserObjectMD)
                oUserObjectMD = Nothing
                GC.WaitForPendingFinalizers()
                GC.Collect()
            End Try

        End Sub

#End Region

    End Class
End Namespace
