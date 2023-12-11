Option Strict Off
Option Explicit On

Imports SAPbouiCOM.Framework
Imports System.Net.Mail
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports
Imports System.IO
Namespace HRMS
    <FormAttribute("OPAY", "Transcation/FrmPayrollCalculation.b1f")>
    Friend Class FrmPayrollCalculation
        Inherits UserFormBase
        Public WithEvents objform As SAPbouiCOM.Form
        Dim FormCount As Integer = 0
        Dim strsql As String
        Dim objrs As SAPbobsCOM.Recordset
        Private WithEvents odbdsDetails As SAPbouiCOM.DBDataSource
        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.Button0 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.Button1 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.ComboBox0 = CType(Me.GetItem("cmbseries").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText0 = CType(Me.GetItem("Series").Specific, SAPbouiCOM.StaticText)
            Me.EditText0 = CType(Me.GetItem("txtdocnum").Specific, SAPbouiCOM.EditText)
            Me.StaticText1 = CType(Me.GetItem("Item_3").Specific, SAPbouiCOM.StaticText)
            Me.EditText1 = CType(Me.GetItem("txtDocDate").Specific, SAPbouiCOM.EditText)
            Me.ComboBox1 = CType(Me.GetItem("cmbpayprd").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText2 = CType(Me.GetItem("lPayprd").Specific, SAPbouiCOM.StaticText)
            Me.StaticText3 = CType(Me.GetItem("lfrmdate").Specific, SAPbouiCOM.StaticText)
            Me.EditText2 = CType(Me.GetItem("tfrmdate").Specific, SAPbouiCOM.EditText)
            Me.StaticText4 = CType(Me.GetItem("ltodate").Specific, SAPbouiCOM.StaticText)
            Me.EditText3 = CType(Me.GetItem("ttodate").Specific, SAPbouiCOM.EditText)
            Me.CheckBox0 = CType(Me.GetItem("chkfin").Specific, SAPbouiCOM.CheckBox)
            Me.Button2 = CType(Me.GetItem("btnPayroll").Specific, SAPbouiCOM.Button)
            Me.Matrix0 = CType(Me.GetItem("mtxpayroll").Specific, SAPbouiCOM.Matrix)
            Me.Button3 = CType(Me.GetItem("btnpayslip").Specific, SAPbouiCOM.Button)
            Me.Button4 = CType(Me.GetItem("btnJE").Specific, SAPbouiCOM.Button)
            Me.StaticText6 = CType(Me.GetItem("Item_19").Specific, SAPbouiCOM.StaticText)
            Me.EditText5 = CType(Me.GetItem("txtJENo").Specific, SAPbouiCOM.EditText)
            Me.LinkedButton0 = CType(Me.GetItem("lnkje").Specific, SAPbouiCOM.LinkedButton)
            Me.ComboBox2 = CType(Me.GetItem("cmbbranch").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText7 = CType(Me.GetItem("lbranch").Specific, SAPbouiCOM.StaticText)
            Me.EditText4 = CType(Me.GetItem("txtentry").Specific, SAPbouiCOM.EditText)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()
            AddHandler LoadAfter, AddressOf Me.Form_LoadAfter

        End Sub
        Private WithEvents Button0 As SAPbouiCOM.Button

        Private Sub OnCustomInitialize()
            Try
                objform = objaddon.objapplication.Forms.GetForm("OPAY", Me.FormCount)
                objform = objaddon.objapplication.Forms.ActiveForm
                objform.Freeze(True)
                'objform.Items.Item("Item_3").Visible = False
                'odbdsDetails = objform.DataSources.DBDataSources.Item(CType(1, Object))
                odbdsDetails = objform.DataSources.DBDataSources.Item("@MIPL_OPCL")
                objaddon.objglobalmethods.LoadSeries(objform, odbdsDetails, "")
                objform.Items.Item("txtDocDate").Specific.string = Now.Date.ToString("dd/MM/yy")

                'Entry = objaddon.objglobalmethods.GetNextDocentry_Value("@MIPL_OPPI")
                'objform.Items.Item("txtentry").Specific.string = objaddon.objglobalmethods.GetNextDocentry_Value("@MIPL_OPPI")
                Comboload()
                Load_Combobox(objform)
                LoadComboDetails()
                'ManageAttributes()
                Button3.Item.Enabled = False
                Matrix0.AddRow()
                objform.Settings.Enabled = True
                MultiBranch = objaddon.objglobalmethods.getSingleValue("select ""MltpBrnchs"" from OADM")
                Matrix0.AutoResizeColumns()
                objform.Freeze(False)
            Catch ex As Exception
                objform.Freeze(False)
            End Try
        End Sub
        Private WithEvents Button1 As SAPbouiCOM.Button
        Private WithEvents ComboBox0 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText0 As SAPbouiCOM.StaticText
        Private WithEvents EditText0 As SAPbouiCOM.EditText
        Private WithEvents StaticText1 As SAPbouiCOM.StaticText
        Private WithEvents EditText1 As SAPbouiCOM.EditText
        Private WithEvents ComboBox1 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText2 As SAPbouiCOM.StaticText
        Private WithEvents StaticText3 As SAPbouiCOM.StaticText
        Private WithEvents EditText2 As SAPbouiCOM.EditText
        Private WithEvents StaticText4 As SAPbouiCOM.StaticText
        Private WithEvents EditText3 As SAPbouiCOM.EditText
        Private WithEvents CheckBox0 As SAPbouiCOM.CheckBox
        Private WithEvents Button2 As SAPbouiCOM.Button
        Private WithEvents Matrix0 As SAPbouiCOM.Matrix
        Private WithEvents Button3 As SAPbouiCOM.Button
        Private WithEvents Button4 As SAPbouiCOM.Button
        Private WithEvents StaticText6 As SAPbouiCOM.StaticText
        Private WithEvents EditText5 As SAPbouiCOM.EditText
        Private WithEvents LinkedButton0 As SAPbouiCOM.LinkedButton
        Private WithEvents ComboBox2 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText7 As SAPbouiCOM.StaticText

        Private Sub Comboload()
            Try
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery("CALL ""MIPL_HRMS_EMPMASTER_COMBO_FILLING"" ('OPAY')")
                If objrs.RecordCount = 0 Then Exit Sub
                For i As Integer = 0 To objrs.RecordCount - 1
                    Try
                        Select Case objrs.Fields.Item("Type").Value.ToString.ToUpper
                            Case "PAYPERIOD" : ComboBox1.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)

                        End Select
                        objrs.MoveNext()
                    Catch ex As Exception
                        objrs.MoveNext()
                    End Try
                Next
            Catch ex As Exception

            End Try
        End Sub

        Private Sub LoadComboDetails()
            Try
                'ComboBox0.ValidValues.Add("-1", "All")
                Dim objrs As SAPbobsCOM.Recordset
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery("CALL ""MIPL_HRMS_EMPMASTER_COMBO_FILLING"" ('OHEM')")
                If objrs.RecordCount = 0 Then Exit Sub
                For i As Integer = 0 To objrs.RecordCount - 1
                    Try
                        Select Case objrs.Fields.Item("Type").Value.ToString.ToUpper
                            Case "BRANCH" : ComboBox2.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                        End Select
                        objrs.MoveNext()
                    Catch ex As Exception
                        objrs.MoveNext()
                    End Try
                Next
            Catch ex As Exception

            End Try
        End Sub

        Private Sub ManageAttributes()
            Try
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtFDate", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtTDate", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtDocDate", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtentry", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtJENo", False, False, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cmbbranch", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "ChkProcess", True, False, False)

            Catch ex As Exception

            End Try
        End Sub

        Private Sub Load_Combobox(ByVal oform As SAPbouiCOM.Form)
            Try
                Dim cmbdesignation As SAPbouiCOM.Column = Matrix0.Columns.Item("Desig")
                Dim cmbdepartment As SAPbouiCOM.Column = Matrix0.Columns.Item("Dept")

                Dim objrs As SAPbobsCOM.Recordset
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery("CALL ""MIPL_HRMS_EMPMASTER_COMBO_FILLING"" ('ODAS')")
                If objrs.RecordCount = 0 Then Exit Sub
                For i As Integer = 0 To objrs.RecordCount - 1
                    Try
                        Select Case objrs.Fields.Item("Type").Value.ToString.ToUpper
                            Case "POSITION" : cmbdesignation.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "DEPARTMENT" : cmbdepartment.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                        End Select
                        objrs.MoveNext()
                    Catch ex As Exception
                        objrs.MoveNext()
                    End Try
                Next
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Matrix_Field_Setup()
            Try
                Matrix0.Columns.Item("U_A1").Visible = False
                Matrix0.Columns.Item("U_A2").Visible = False
                Matrix0.Columns.Item("U_A3").Visible = False
                Matrix0.Columns.Item("U_A4").Visible = False
                Matrix0.Columns.Item("U_A5").Visible = False
                Matrix0.Columns.Item("U_A6").Visible = False
                Matrix0.Columns.Item("U_A7").Visible = False
                Matrix0.Columns.Item("U_A8").Visible = False
                Matrix0.Columns.Item("U_A9").Visible = False
                Matrix0.Columns.Item("U_A10").Visible = False
                Matrix0.Columns.Item("U_A11").Visible = False
                Matrix0.Columns.Item("U_A12").Visible = False
                Matrix0.Columns.Item("U_A13").Visible = False
                Matrix0.Columns.Item("U_A14").Visible = False
                Matrix0.Columns.Item("U_A15").Visible = False
                Matrix0.Columns.Item("U_A16").Visible = False
                Matrix0.Columns.Item("U_A17").Visible = False
                Matrix0.Columns.Item("U_A18").Visible = False
                Matrix0.Columns.Item("U_A19").Visible = False
                Matrix0.Columns.Item("U_A20").Visible = False

                ''Matrix0.Columns.Item("basic").Visible = False
                ''Matrix0.Columns.Item("totsal").Visible = False

                strsql = "select 'U_'||""U_Sequence"" ""ColName"",""Name"" from ""@SMPR_OPYE"" Where ""U_Type""='S' and ifnull(""U_Active"",'')='Y' "
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strsql)
                If objrs.RecordCount > 0 Then
                    For i As Integer = 0 To objrs.RecordCount - 1
                        Matrix0.Columns.Item(objrs.Fields.Item("ColName").Value.ToString).Visible = True
                        Matrix0.Columns.Item(objrs.Fields.Item("ColName").Value.ToString).TitleObject.Caption = objrs.Fields.Item("Name").Value.ToString
                        Matrix0.Columns.Item(objrs.Fields.Item("ColName").Value.ToString).RightJustified = True
                        Matrix0.Columns.Item(objrs.Fields.Item("ColName").Value.ToString).ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                        objrs.MoveNext()
                    Next
                End If

                'Matrix0.Columns.Item("netsal").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                'Matrix0.Columns.Item("totsal").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                'Matrix0.Columns.Item("totamt").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                'Matrix0.Columns.Item("Gsal").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                'Matrix0.Columns.Item("tadd").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                ''Matrix0.Columns.Item("atick").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                ''Matrix0.Columns.Item("trip").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                'Matrix0.Columns.Item("TD").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                'Matrix0.Columns.Item("loan").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                'Matrix0.Columns.Item("alsal").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                'Matrix0.Columns.Item("advsal").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                'Matrix0.Columns.Item("round").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                'Matrix0.Columns.Item("ASum").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto

                ''Matrix0.Columns.Item("netsal").BackColor = Color.SeaGreen.ToArgb
                'Matrix0.Columns.Item("netsal").ForeColor = Color.Red.ToArgb
                'Matrix0.Columns.Item("netsal").TextStyle = FontStyle.Bold
                'Matrix0.Columns.Item("pdays").ForeColor = Color.Green.ToArgb
                'Matrix0.Columns.Item("pdays").TextStyle = FontStyle.Bold
                'Matrix0.Columns.Item("totamt").ForeColor = Color.DarkOrange.ToArgb
                'Matrix0.Columns.Item("totamt").TextStyle = FontStyle.Bold
                'Matrix0.Columns.Item("Gsal").ForeColor = 150
                'Matrix0.Columns.Item("Gsal").TextStyle = FontStyle.Bold
                'Matrix0.Columns.Item("tadd").ForeColor = Color.Brown.ToArgb
                'Matrix0.Columns.Item("tadd").TextStyle = FontStyle.Bold
                'Matrix0.Columns.Item("TD").ForeColor = Color.DarkMagenta.ToArgb
                'Matrix0.Columns.Item("TD").TextStyle = FontStyle.Bold

                Matrix0.CommonSetting.FixedColumnsCount = 5

                objaddon.objapplication.Menus.Item("1300").Activate()
            Catch ex As Exception

            End Try
        End Sub

        Private WithEvents EditText4 As SAPbouiCOM.EditText

        Private Sub ComboBox1_ComboSelectAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox1.ComboSelectAfter
            Try
                If objform.Mode <> SAPbouiCOM.BoFormMode.fm_ADD_MODE Then Exit Sub
                If ComboBox1.Selected Is Nothing Then Exit Sub
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery("select TO_VARCHAR(""F_RefDate"",'yyyyMMdd') ""F_RefDate"",TO_VARCHAR(""T_RefDate"",'yyyyMMdd') ""T_RefDate"" from OFPR where ""Code""='" & ComboBox1.Selected.Value & "'")
                'objrs.DoQuery("select ""F_RefDate"" ""F_RefDate"",""T_RefDate"" ""T_RefDate"" from OFPR where ""Code""='" & ComboBox0.Selected.Value & "'")
                If objrs.RecordCount > 0 Then
                    objform.Items.Item("tfrmdate").Specific.string = objrs.Fields.Item("F_RefDate").Value.ToString
                    objform.Items.Item("ttodate").Specific.string = objrs.Fields.Item("T_RefDate").Value.ToString
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Button2_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button2.ClickAfter
            Try

                Dim FDate As Date = Date.ParseExact(EditText2.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Dim TDate As Date = Date.ParseExact(EditText3.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                If objaddon.objapplication.MessageBox("Do you want to Calculate Payroll?", 2, "Yes", "No") <> 1 Then Exit Sub
                objaddon.objapplication.SetStatusBarMessage("Calculating Payroll Details. Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Long, False)
                objform.Freeze(True)
                odbdsDetails = objform.DataSources.DBDataSources.Item(CType(1, Object))
                odbdsDetails.Clear()
                Matrix0.LoadFromDataSource()

                If MultiBranch = "Y" Then
                    strsql = "CALL ""MIPL_PayrollCalculationUpdated"" ('" & ComboBox1.Selected.Value & "'"
                    If Not ComboBox2.Selected Is Nothing Then
                        If ComboBox2.Selected.Value = "-1" Then strsql += " ,'')" Else strsql += " ,'" & ComboBox2.Selected.Value & "')"
                    Else
                        strsql += " ,'')"
                    End If
                Else
                    strsql = "CALL ""MIPL_PayrollCalculationUpdated"" ('" & ComboBox1.Selected.Value & "','')"
                End If

                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strsql)

                If objrs.RecordCount = 0 Then objaddon.objapplication.SetStatusBarMessage("No records Found", SAPbouiCOM.BoMessageTime.bmt_Short, True) : objform.Freeze(False) : Exit Sub

                odbdsDetails.InsertRecord(odbdsDetails.Size)
                objaddon.objapplication.SetStatusBarMessage("Filling Payroll Details. Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Long, False)

                For i As Integer = 0 To objrs.RecordCount - 1
                    odbdsDetails.SetValue("LineId", i, i + 1)
                    odbdsDetails.SetValue("U_IDNo", i, objrs.Fields.Item("U_empid").Value.ToString)
                    odbdsDetails.SetValue("U_empID", i, objrs.Fields.Item("EmpCode").Value.ToString)
                    odbdsDetails.SetValue("U_empName", i, objrs.Fields.Item("EmpName").Value.ToString)
                    odbdsDetails.SetValue("U_Designat", i, objrs.Fields.Item("Designation").Value.ToString)
                    odbdsDetails.SetValue("U_Dept", i, objrs.Fields.Item("DeptCode").Value.ToString)
                    odbdsDetails.SetValue("U_Paymode", i, objrs.Fields.Item("PayMode").Value.ToString)
                    odbdsDetails.SetValue("U_TotalDays", i, objrs.Fields.Item("TotalDays").Value.ToString)
                    odbdsDetails.SetValue("U_PaidDays", i, objrs.Fields.Item("WorkedDays").Value.ToString)
                    odbdsDetails.SetValue("U_PayDays", i, objrs.Fields.Item("PayableDays").Value.ToString)
                    ' odbdsDetails.SetValue("U_WoDays", i, objrs.Fields.Item("PayableDays").Value.ToString)
                    'odbdsDetails.SetValue("U_Basic", i, objrs.Fields.Item("TotalBasic").Value.ToString)
                    'odbdsDetails.SetValue("U_HRA", i, objrs.Fields.Item("HRA").Value.ToString)
                    odbdsDetails.SetValue("U_PF", i, objrs.Fields.Item("PF").Value.ToString)
                    odbdsDetails.SetValue("U_EmpPF", i, objrs.Fields.Item("PF").Value.ToString)
                    odbdsDetails.SetValue("U_EmpESI", i, objrs.Fields.Item("EmployerESI").Value.ToString)
                    odbdsDetails.SetValue("U_LOPAmt", i, objrs.Fields.Item("LOPAmount").Value.ToString)
                    odbdsDetails.SetValue("U_ESI", i, objrs.Fields.Item("ESI").Value.ToString)
                    odbdsDetails.SetValue("U_A1", i, objrs.Fields.Item("A1").Value.ToString)
                    odbdsDetails.SetValue("U_A2", i, objrs.Fields.Item("A2").Value.ToString)
                    odbdsDetails.SetValue("U_A3", i, objrs.Fields.Item("A3").Value.ToString)
                    odbdsDetails.SetValue("U_A4", i, objrs.Fields.Item("A4").Value.ToString)
                    odbdsDetails.SetValue("U_A5", i, objrs.Fields.Item("A5").Value.ToString)
                    odbdsDetails.SetValue("U_A6", i, objrs.Fields.Item("A6").Value.ToString)
                    odbdsDetails.SetValue("U_A7", i, objrs.Fields.Item("A7").Value.ToString)
                    odbdsDetails.SetValue("U_A8", i, objrs.Fields.Item("A8").Value.ToString)
                    odbdsDetails.SetValue("U_A9", i, objrs.Fields.Item("A9").Value.ToString)
                    odbdsDetails.SetValue("U_A10", i, objrs.Fields.Item("A10").Value.ToString)
                    odbdsDetails.SetValue("U_A11", i, objrs.Fields.Item("A11").Value.ToString)
                    odbdsDetails.SetValue("U_A12", i, objrs.Fields.Item("A12").Value.ToString)
                    odbdsDetails.SetValue("U_A13", i, objrs.Fields.Item("A13").Value.ToString)
                    odbdsDetails.SetValue("U_A14", i, objrs.Fields.Item("A14").Value.ToString)
                    odbdsDetails.SetValue("U_A15", i, objrs.Fields.Item("A15").Value.ToString)
                    odbdsDetails.SetValue("U_A16", i, objrs.Fields.Item("A16").Value.ToString)
                    odbdsDetails.SetValue("U_A17", i, objrs.Fields.Item("A17").Value.ToString)
                    odbdsDetails.SetValue("U_A18", i, objrs.Fields.Item("A18").Value.ToString)
                    odbdsDetails.SetValue("U_A19", i, objrs.Fields.Item("A19").Value.ToString)
                    odbdsDetails.SetValue("U_A20", i, objrs.Fields.Item("A20").Value.ToString)

                    odbdsDetails.SetValue("U_GrossSal", i, objrs.Fields.Item("GrossSalary").Value.ToString)
                    odbdsDetails.SetValue("U_TDS", i, objrs.Fields.Item("TDS").Value.ToString)
                    odbdsDetails.SetValue("U_ProfTax", i, objrs.Fields.Item("PT").Value.ToString)
                    odbdsDetails.SetValue("U_LOPDays", i, objrs.Fields.Item("LOPDays").Value.ToString)
                    odbdsDetails.SetValue("U_LateHrs", i, objrs.Fields.Item("LateHrs").Value.ToString)
                    odbdsDetails.SetValue("U_Loan", i, objrs.Fields.Item("Loan").Value.ToString)
                    odbdsDetails.SetValue("U_TotHrs", i, objrs.Fields.Item("TotHrs").Value.ToString)
                    odbdsDetails.SetValue("U_WorkHrs", i, objrs.Fields.Item("WorkedHrs").Value.ToString)
                    odbdsDetails.SetValue("U_DaySal", i, objrs.Fields.Item("DaySalary").Value.ToString)
                    odbdsDetails.SetValue("U_HrSal", i, objrs.Fields.Item("HrSalary").Value.ToString)
                    odbdsDetails.SetValue("U_LopHrs", i, objrs.Fields.Item("LOPHrs").Value.ToString)
                    'odbdsDetails.SetValue("U_CODays", i, objrs.Fields.Item("CompOff").Value.ToString)
                    odbdsDetails.SetValue("U_ELDays", i, objrs.Fields.Item("CarryFwdLv").Value.ToString)
                    odbdsDetails.SetValue("U_LeaveBal", i, objrs.Fields.Item("LeaveBal").Value.ToString)
                    odbdsDetails.SetValue("U_LeaveTak", i, objrs.Fields.Item("LeaveTaken").Value.ToString)
                    odbdsDetails.SetValue("U_shifthrs", i, objrs.Fields.Item("ShiftHrs").Value.ToString)
                    odbdsDetails.SetValue("U_ELTaken", i, objrs.Fields.Item("ELLIGIBLELVDAYS").Value.ToString)
                    odbdsDetails.SetValue("U_RoundOff", i, objrs.Fields.Item("RoundOff").Value.ToString)
                    objrs.MoveNext()

                    If i <> objrs.RecordCount - 1 Then odbdsDetails.InsertRecord(odbdsDetails.Size)
                Next

                Matrix0.LoadFromDataSource()
                Matrix0.CommonSetting.FixedColumnsCount = 5
                objaddon.objapplication.Menus.Item("1300").Activate() 'Fit colum width
                ' Matrix_Field_Setup()
                TaxCalculation()
                Matrix_Total()
                objform.Update()
                objform.Refresh()
                Dim status As String = ""
                Dim Row As Integer = 1
                'While Row <= Matrix0.RowCount
                '    status = objaddon.objglobalmethods.getSingleValue("select case when count(*)>1 or count(*)=1  then True else False end as ""status"" from  ""@MIPL_PPI1"" T1 join ""@MIPL_OPPI"" T2" &
                '                                                         " on T1.""DocEntry""=T2.""DocEntry"" where ""U_empID""='" & Matrix0.Columns.Item("empID").Cells.Item(Row).Specific.String & "'and T2.""U_FromDate"" between '" & FDate.ToString("yyyyMMdd") & "' AND '" & TDate.ToString("yyyyMMdd") & "'" &
                '                                                        " and T2.""U_ToDate"" between '" & FDate.ToString("yyyyMMdd") & "' AND '" & TDate.ToString("yyyyMMdd") & "' and ifnull(T1.""U_SalProcess"",'')='Y';")

                '    If status = "1" Then
                '        Matrix0.DeleteRow(Row)
                '    Else
                '        Row += 1
                '        If Row = Matrix0.RowCount Then
                '            Exit While
                '        End If
                '    End If
                'End While
                objform.Update()
                objform.Refresh()
                If Matrix0.RowCount = 0 Then
                    objaddon.objapplication.StatusBar.SetText("No Records Found", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                Else
                    objaddon.objapplication.StatusBar.SetText("Payroll Details Loaded successfully", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                End If

                'If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE
                objform.Freeze(False)
                objform.Items.Item("txtDocDate").Specific.string = Now.Date.ToString("dd/MM/yy")
            Catch ex As Exception
                objform.Freeze(False)
            End Try

        End Sub

        Public Sub Matrix_Total()
            Try
                Matrix0.Columns.Item("GrossSal").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Manual
                Matrix0.Columns.Item("NetSal").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("Bonus").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("Incent").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("PF").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("ESI").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("ProfTax").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("TDS").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("Loan").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("Payable").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("TotDed").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("TotAdd").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("EPF").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("EESI").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                'Matrix0.Columns.Item("GrossSal").ForeColor = Color.Aquamarine.ToArgb
                Matrix0.Columns.Item("GrossSal").TextStyle = FontStyle.Bold
                'Matrix0.Columns.Item("NetSal").ForeColor = Color.Aquamarine.ToArgb
                Matrix0.Columns.Item("NetSal").TextStyle = FontStyle.Bold
                'Matrix0.Columns.Item("Payable").ForeColor = Color.Aquamarine.ToArgb
                Matrix0.Columns.Item("Payable").TextStyle = FontStyle.Bold
                'Matrix0.CommonSetting.FixedColumnsCount = 3

                objaddon.objapplication.Menus.Item("1300").Activate()
            Catch ex As Exception

            End Try
        End Sub

        Private Sub TaxCalculation()
            Try
                objform.Freeze(True)
                'Dim PT As String, Location As String
                Dim Payable, Deduction, PTCal As Double
                Dim Addition, GrossSal As Double, LOAN As Double = 0, NetSal As Double

                For i As Integer = 1 To Matrix0.VisualRowCount
                    If Matrix0.Columns.Item("empID").Cells.Item(i).Specific.String <> "" Then
                        Deduction = CDbl(Matrix0.Columns.Item("ProfTax").Cells.Item(i).Specific.string) + CDbl(Matrix0.Columns.Item("ESI").Cells.Item(i).Specific.string) + CDbl(Matrix0.Columns.Item("PF").Cells.Item(i).Specific.string) + CDbl(Matrix0.Columns.Item("EPF").Cells.Item(i).Specific.string) + CDbl(Matrix0.Columns.Item("EESI").Cells.Item(i).Specific.string)
                        Addition = CDbl(Matrix0.Columns.Item("Bonus").Cells.Item(i).Specific.string) + CDbl(Matrix0.Columns.Item("Incent").Cells.Item(i).Specific.string)
                        GrossSal = CDbl(Matrix0.Columns.Item("GrossSal").Cells.Item(i).Specific.string)
                        'EmpPF = CDbl(Matrix0.Columns.Item("EPF").Cells.Item(i).Specific.string)
                        NetSal = GrossSal - Deduction
                        Payable = NetSal + Addition
                        Matrix0.Columns.Item("TotDed").Cells.Item(i).Specific.string = CStr(Deduction)
                        'Matrix0.Columns.Item("TotAdd").Cells.Item(i).Specific.string = CStr(Addition)
                        If NetSal < 0 Then
                            Matrix0.Columns.Item("NetSal").Cells.Item(i).Specific.string = 0
                        Else
                            Matrix0.Columns.Item("NetSal").Cells.Item(i).Specific.string = CStr(NetSal)
                        End If
                        If Payable < 0 Then
                            Matrix0.Columns.Item("Payable").Cells.Item(i).Specific.string = 0
                        Else
                            Matrix0.Columns.Item("Payable").Cells.Item(i).Specific.string = CStr(Payable)
                        End If

                    End If
                Next
                objform.Freeze(False)
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Button2_ClickBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button2.ClickBefore
            Try
                If ComboBox1.Value = "" Then
                    objaddon.objapplication.SetStatusBarMessage("Pay Period is Missing", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If
                If MultiBranch = "Y" Then
                    If ComboBox2.Value = "" Then
                        objaddon.objapplication.SetStatusBarMessage("Branch is Missing", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                        BubbleEvent = False : Exit Sub
                    End If
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Matrix0_LinkPressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.LinkPressedAfter
            Try
                If pVal.ItemUID = "mtxpayroll" And pVal.ColUID = "IDNo" Then
                    If Matrix0.Columns.Item("IDNo").Cells.Item(pVal.Row).Specific.string = "" Then Exit Sub
                    Link_Value = Matrix0.Columns.Item("IDNo").Cells.Item(pVal.Row).Specific.string : Link_objtype = "OHEM"
                    Dim activeform As New frmEmployeeMaster
                    activeform.Show()
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Function PDFCreationUpdated() As Boolean
            Dim cryRpt As New ReportDocument
            Dim DBUserName As String = "KADMIN", Filename As String = "" ' = "KADMIN"
            Dim DbPassword As String = "India@1947" '"India@1947"
            Dim EmpId, Month, EmpName As String
            Dim IntYear As Integer
            Dim Foldername, sDocOutPath As String
            Dim Flag As Boolean = False
            Try
                Filename = System.Windows.Forms.Application.StartupPath & "\PaySlip1.rpt"
                Dim FDate As Date = Date.ParseExact(EditText0.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                cryRpt.Load(Filename)
                Dim initialpath As String = objaddon.objglobalmethods.getSingleValue("select ""AttachPath"" from OADP")
                Foldername = initialpath & "Payslip PDF" & "\" & CStr(FDate.Year) & "\" & CStr(MonthName(FDate.Month, False)) & "\" & System.DateTime.Now.ToString("ddMMyyHHmmss")
                If Directory.Exists(Foldername) Then
                Else
                    Directory.CreateDirectory(Foldername)
                End If
                'DBUserName = objaddon.objglobalmethods.getSingleValue("select ""U_DBUser"" from OADM")
                'DbPassword = objaddon.objglobalmethods.getSingleValue("select ""U_DBPass"" from OADM")
                cryRpt.DataSourceConnections(0).SetConnection(objaddon.objcompany.Server, objaddon.objcompany.CompanyDB, False)
                cryRpt.DataSourceConnections(0).SetLogon(DBUserName, DbPassword)
                For i As Integer = 1 To Matrix0.VisualRowCount
                    EmpId = Matrix0.Columns.Item("empID").Cells.Item(i).Specific.string '"EMP005"
                    EmpName = Matrix0.Columns.Item("empName").Cells.Item(i).Specific.string
                    sDocOutPath = Foldername + "\" + EmpName + ".pdf"
                    Month = MonthName(FDate.Month, False)
                    IntYear = FDate.Year
                    cryRpt.SetParameterValue("Month", CStr(Month))
                    cryRpt.SetParameterValue("Year@select year(current_date) from dummy union all select year(current_date)-1 from dummy union all select year(current_date)-2 from dummy", Convert.ToInt32(IntYear))
                    cryRpt.SetParameterValue("Emp@select Distinct T1.""U_empID"",T1.""U_empName"" from ""@MIPL_PPI1"" T1 where ifnull(T1.""U_empID"",'')<>''", EmpId)
                    'cryRpt.ExportToStream(ExportFormatType.PortableDocFormat)
                    cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, sDocOutPath)
                    Flag = True
                Next

            Catch ex As Exception
                Flag = False
                objaddon.objapplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
            End Try
            Return Flag
        End Function

        Private Sub Button3_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button3.ClickAfter
            Try
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then Exit Sub
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Or objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE Then
                    objaddon.objapplication.SetStatusBarMessage("Generating PDF files Please wait...", SAPbouiCOM.BoMessageTime.bmt_Short, False)
                    'CreatePayslipPDF()
                    If PDFCreationUpdated() = True Then
                        objaddon.objapplication.StatusBar.SetText("PDF Files generated Successfully...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                    Else
                        objaddon.objapplication.StatusBar.SetText("Error While Creating PDF Files...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                    End If
                End If
            Catch ex As Exception
                objaddon.objapplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
            End Try

        End Sub

        Private Sub Matrix0_ValidateAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.ValidateAfter
            Try
                Dim PF As Double, ESI As Double, LOAN As Double = 0, NetSal, PT As Double
                Dim Bonus, Incentive, Addition, TDSNew, Deduct, GrossSal As Double
                objaddon.objapplication.Menus.Item("1300").Activate()
                TDSNew = CDbl(Matrix0.Columns.Item("TDS").Cells.Item(pVal.Row).Specific.string)
                LOAN = CDbl(Matrix0.Columns.Item("Loan").Cells.Item(pVal.Row).Specific.string)
                Bonus = CDbl(Matrix0.Columns.Item("Bonus").Cells.Item(pVal.Row).Specific.string)
                Incentive = CDbl(Matrix0.Columns.Item("Incent").Cells.Item(pVal.Row).Specific.string)
                Deduct = Matrix0.Columns.Item("TotDed").Cells.Item(pVal.Row).Specific.string
                NetSal = CDbl(Matrix0.Columns.Item("NetSal").Cells.Item(pVal.Row).Specific.string)
                GrossSal = CDbl(Matrix0.Columns.Item("GrossSal").Cells.Item(pVal.Row).Specific.string)
                PF = CDbl(Matrix0.Columns.Item("PF").Cells.Item(pVal.Row).Specific.string)
                ESI = CDbl(Matrix0.Columns.Item("ESI").Cells.Item(pVal.Row).Specific.string)
                PT = CDbl(Matrix0.Columns.Item("ProfTax").Cells.Item(pVal.Row).Specific.string)
                Try

                    If pVal.ItemChanged = True And pVal.ActionSuccess = True Then
                        Addition = Bonus + Incentive
                        Matrix0.Columns.Item("TotAdd").Cells.Item(pVal.Row).Specific.string = Addition
                        Matrix0.Columns.Item("TotDed").Cells.Item(pVal.Row).Specific.string = CDbl(PF + PF + ESI + PT + LOAN + TDSNew)
                        'Matrix0.Columns.Item("NetSal").Cells.Item(pVal.Row).Specific.string = CDbl(NetSal - LOAN)
                        Matrix0.Columns.Item("Payable").Cells.Item(pVal.Row).Specific.string = CDbl((NetSal + (Bonus + Incentive)) - (LOAN + TDSNew))
                        'Matrix0.Columns.Item("NetSal").Cells.Item(pVal.Row).Specific.string = CDbl((GrossSal - PF - ESI - oProfTax - TDSNew) - LOAN)
                        'Matrix0.Columns.Item("Payable").Cells.Item(pVal.Row).Specific.string = CDbl((GrossSal - (PF + ESI + oProfTax + TDSNew + LOAN)) + (Bonus + Incentive))
                    End If
                    'Select Case pVal.ColUID
                    '    Case "TDS"
                    '        Matrix0.Columns.Item("NetSal").Cells.Item(pVal.Row).Specific.string = CDbl((GrossSal - PF - ESI - oProfTax) - TDSNew)
                    '    Case "Loan"

                    '    Case "Bonus"
                    '        Matrix0.Columns.Item("Payable").Cells.Item(pVal.Row).Specific.string = CDbl((GrossSal - PF - ESI - oProfTax - TDSNew - LOAN) + Bonus)
                    '    Case "Incent"

                    'End Select
                Catch ex As Exception
                End Try
            Catch ex As Exception
            End Try
        End Sub

        Public Class JETransaction
            Public Amount As Double
            Public Type As String

        End Class

        Private Function gettranforJE(ByVal value As String)
            Try
                Dim recsetvalue As String = ""
                Select Case value
                    Case "TotGross"
                        value = "Debit"
                        recsetvalue = "6"
                    Case "TotBonus"
                        value = "Debit"
                        recsetvalue = "6"
                    Case "TotIncent"
                        value = "Debit"
                        recsetvalue = "6"
                    Case "TotPF"
                        value = "Credit"
                        recsetvalue = "0"
                    Case "TotESI"
                        value = "Credit"
                        recsetvalue = "2"
                    Case "TotPT"
                        value = "Credit"
                        recsetvalue = "1"
                    Case "TotTDS"
                        value = "Credit"
                        recsetvalue = "3"
                    Case "TotLoan"
                        value = "Credit"
                        recsetvalue = "4"
                    Case "TotSalPayable"
                        value = "Credit"
                        recsetvalue = "5"
                    Case "TotEmployerPF_ESI"
                        value = "Credit"
                        recsetvalue = "5"
                    Case Else
                        value = Nothing
                End Select
                Return {value, recsetvalue}
            Catch ex As Exception

            End Try
        End Function

        Public Sub IN_JournalEntry()
            Dim objjournalentry As SAPbobsCOM.JournalEntries
            Dim DocEntry As String
            Dim Flag As Boolean = False

            objjournalentry = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries)
            strsql = "CALL ""MIPL_GetJEAccount_Details"" "            '
            objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            objrs.DoQuery(strsql)
            Dim TotGross As Double = 0, TotEncash As Double = 0, TotBonus As Double = 0, TotIncent As Double = 0
            Dim TotPF As Double = 0, TotEmployerPF_ESI As Double = 0, TotESI As Double = 0, TotPT As Double = 0, TotTDS As Double = 0, TotLoan As Double = 0, TotSalPayable As Double = 0
            For i As Integer = 1 To Matrix0.VisualRowCount
                If Matrix0.Columns.Item("salstat").Cells.Item(i).Specific.Checked = True Then
                    TotGross += CDbl(Matrix0.Columns.Item("GrossSal").Cells.Item(i).Specific.string)
                    ' TotEncash += CDbl(Matrix0.Columns.Item("Encash").Cells.Item(i).Specific.string)
                    TotBonus += CDbl(Matrix0.Columns.Item("Bonus").Cells.Item(i).Specific.string)
                    TotIncent += CDbl(Matrix0.Columns.Item("Incent").Cells.Item(i).Specific.string)
                    TotPF += CDbl(Matrix0.Columns.Item("PF").Cells.Item(i).Specific.string)
                    TotESI += CDbl(Matrix0.Columns.Item("ESI").Cells.Item(i).Specific.string)
                    TotPT += CDbl(Matrix0.Columns.Item("ProfTax").Cells.Item(i).Specific.string)
                    TotTDS += CDbl(Matrix0.Columns.Item("TDS").Cells.Item(i).Specific.string)
                    TotLoan += CDbl(Matrix0.Columns.Item("Loan").Cells.Item(i).Specific.string)
                    TotSalPayable += Matrix0.Columns.Item("Payable").Cells.Item(i).Specific.string
                    TotEmployerPF_ESI += CDbl(Matrix0.Columns.Item("EPF").Cells.Item(i).Specific.string) + CDbl(Matrix0.Columns.Item("EESI").Cells.Item(i).Specific.string)
                    Flag = True
                End If
            Next

            Dim GetVariableNames() As Double
            Dim GetIndexNames() As String
            GetVariableNames = {TotGross, TotBonus, TotIncent, TotPF, TotESI, TotPT, TotTDS, TotLoan, TotSalPayable, TotEmployerPF_ESI}
            GetIndexNames = {"TotGross", "TotBonus", "TotIncent", "TotPF", "TotESI", "TotPT", "TotTDS", "TotLoan", "TotSalPayable", "TotEmployerPF_ESI"}
            Dim GetTranValues As New List(Of JETransaction)
            For ivalue As Integer = 0 To GetVariableNames.Length - 1
                If GetVariableNames(ivalue) > 0 Then
                    Dim GetTran As New JETransaction
                    GetTran.Amount = GetVariableNames(ivalue)
                    GetTran.Type = GetIndexNames(ivalue)
                    GetTranValues.Add(GetTran)
                End If
            Next

            If Flag = False Then
                objaddon.objapplication.SetStatusBarMessage("Please select the status in line level...", SAPbouiCOM.BoMessageTime.bmt_Short)
                Exit Sub
            End If
            Dim FDate As Date = Date.ParseExact(EditText2.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            Dim Branch As String = ""
            If MultiBranch = "Y" Then
                If ComboBox2.Selected.Value = "-1" Or ComboBox2.Selected.Value Is Nothing Then
                    Branch = objaddon.objglobalmethods.getSingleValue("select ""BPLId"" from OBPL where ""BPLId""='1'")
                Else
                    Branch = objaddon.objglobalmethods.getSingleValue("select ""BPLId"" from OBPL where ""BPLId""='" & ComboBox2.Selected.Value & "'")
                End If
            End If

            If Not objaddon.objcompany.InTransaction Then objaddon.objcompany.StartTransaction()
            'objjournalentry.Series = Series
            objjournalentry.ReferenceDate = Now.ToString("yyyy-MM-dd")
            objjournalentry.Memo = "Payroll Process for the month -" & CStr(MonthName(FDate.Month, True))
            Dim JEValues() As String
            For Rec As Integer = 0 To GetTranValues.Count - 1
                JEValues = gettranforJE(GetTranValues.ElementAt(Rec).Type)
                objjournalentry.Lines.AccountCode = objrs.Fields.Item(CInt(JEValues(1))).Value.ToString
                If JEValues(0) = "Debit" Then objjournalentry.Lines.Debit = GetTranValues.ElementAt(Rec).Amount Else objjournalentry.Lines.Credit = GetTranValues.ElementAt(Rec).Amount
                If Branch <> "" Then
                    objjournalentry.Lines.BPLID = Branch
                End If

                objjournalentry.Lines.Add()
            Next

            If objjournalentry.Add <> 0 Then
                If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                objaddon.objapplication.SetStatusBarMessage(objaddon.objcompany.GetLastErrorDescription, SAPbouiCOM.BoMessageTime.bmt_Short)
                CheckBox0.Checked = False
            Else
                If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                DocEntry = objaddon.objcompany.GetNewObjectKey()
                objform.Items.Item("txtJENo").Specific.String = DocEntry
                objaddon.objapplication.SetStatusBarMessage("Journal Entry Successfully Posted..." & DocEntry, SAPbouiCOM.BoMessageTime.bmt_Long, False)
            End If
        End Sub

        Private Sub Button0_ClickBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button0.ClickBefore
            Try
                ''If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then Exit Sub
                ''If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                ''    If ComboBox1.Value = "" Then
                ''        objaddon.objapplication.SetStatusBarMessage("You cannot submit the blank document", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                ''        BubbleEvent = False : Exit Sub
                ''    End If

                ''    If Matrix0.RowCount = 0 Then
                ''        objaddon.objapplication.SetStatusBarMessage("Row Data Missing", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                ''        BubbleEvent = False : Exit Sub
                ''    End If
                ''    If EditText5.Value = "" Then
                ''        objaddon.objapplication.SetStatusBarMessage("Please finalize the document & Post JE...", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                ''        BubbleEvent = False : Exit Sub
                ''    End If
                ''End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Button4_ClickBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button4.ClickBefore
            Try
                If Not ApprovedUser_Employee Then
                    objaddon.objapplication.SetStatusBarMessage("You are not authorized to post JE", SAPbouiCOM.BoMessageTime.bmt_Long, True)
                    BubbleEvent = False : Exit Sub
                End If
                If ComboBox1.Value = "" Then
                    objaddon.objapplication.SetStatusBarMessage("Please select Pay Period...", SAPbouiCOM.BoMessageTime.bmt_Long, True)
                    BubbleEvent = False : Exit Sub
                End If
                If Matrix0.VisualRowCount <= 1 Then
                    If Matrix0.Columns.Item("empID").Cells.Item(1).Specific.String = "" Then
                        objaddon.objapplication.SetStatusBarMessage("Line Data missing", SAPbouiCOM.BoMessageTime.bmt_Long, True)
                        BubbleEvent = False : Exit Sub
                    End If
                End If
                If Not CheckBox0.Checked = True Then
                    objaddon.objapplication.SetStatusBarMessage("Please Tick the Finalize", SAPbouiCOM.BoMessageTime.bmt_Long, True)
                    BubbleEvent = False : Exit Sub
                End If
                If EditText5.Value <> "" Then
                    objaddon.objapplication.SetStatusBarMessage("Joural Entry Posted for this entry", SAPbouiCOM.BoMessageTime.bmt_Long, True)
                    BubbleEvent = False : Exit Sub
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Button4_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button4.ClickAfter
            Try
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Or objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE Then
                    ApprovedUser_Employee = objaddon.ApprovedUser()
                    If CheckBox0.Checked = True Then
                        If ApprovedUser_Employee Then
                            If EditText5.Value = "" Then
                                IN_JournalEntry()
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub ComboBox0_ComboSelectAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox0.ComboSelectAfter
            Try
                odbdsDetails.SetValue("DocNum", 0, objaddon.objglobalmethods.GetDocNum("MIPAY", CInt(ComboBox0.Selected.Value)))
            Catch ex As Exception
            End Try

        End Sub

        Private Sub Form_LoadAfter(pVal As SAPbouiCOM.SBOItemEventArg)


        End Sub
    End Class
End Namespace
