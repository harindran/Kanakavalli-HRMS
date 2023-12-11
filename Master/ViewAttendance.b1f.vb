Option Strict Off
Option Explicit On

'Imports SAPbouiCOM.Framework
'Imports System.IO
'Imports System.Threading
'Imports Excel = Microsoft.Office.Interop.Excel
'Imports System.Data.OleDb
'Imports System.Windows.Forms
'Imports SAPbouiCOM
Imports SAPbouiCOM.Framework
Imports System.IO
Imports System.Threading
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Data.OleDb
Imports System.Xml

Namespace HRMS
    <FormAttribute("VATT", "Master/ViewAttendance.b1f")>
    Friend Class ViewAttendance
        Inherits UserFormBase
        Dim FormCount As Integer = 0
        Private WithEvents objform As SAPbouiCOM.Form
        Private WithEvents pCFL As SAPbouiCOM.ISBOChooseFromListEventArg
        Dim objrs As SAPbobsCOM.Recordset
        Private WithEvents odbdsheader, odbdsDetails As SAPbouiCOM.DBDataSource
        Dim strsql As String
        Public Filename As String = ""
        Dim BankFileName = ""
        Public objfile As FileInfo
        'Public Shared objFinalDT As New DataTable
        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.Button0 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.Button1 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.StaticText0 = CType(Me.GetItem("lblentry").Specific, SAPbouiCOM.StaticText)
            Me.EditText0 = CType(Me.GetItem("txtentry").Specific, SAPbouiCOM.EditText)
            Me.Matrix0 = CType(Me.GetItem("matattd").Specific, SAPbouiCOM.Matrix)
            Me.StaticText1 = CType(Me.GetItem("ldocdate").Specific, SAPbouiCOM.StaticText)
            Me.EditText1 = CType(Me.GetItem("txtdate").Specific, SAPbouiCOM.EditText)
            Me.Button3 = CType(Me.GetItem("btnimport").Specific, SAPbouiCOM.Button)
            Me.EditText2 = CType(Me.GetItem("txtFName").Specific, SAPbouiCOM.EditText)
            Me.Button2 = CType(Me.GetItem("ClrData").Specific, SAPbouiCOM.Button)
            Me.StaticText2 = CType(Me.GetItem("lrem").Specific, SAPbouiCOM.StaticText)
            Me.EditText3 = CType(Me.GetItem("txtrem").Specific, SAPbouiCOM.EditText)
            Me.StaticText3 = CType(Me.GetItem("lblseries").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox0 = CType(Me.GetItem("cmbseries").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText4 = CType(Me.GetItem("lbldate").Specific, SAPbouiCOM.StaticText)
            Me.EditText4 = CType(Me.GetItem("txtadate").Specific, SAPbouiCOM.EditText)
            Me.EditText5 = CType(Me.GetItem("txtday").Specific, SAPbouiCOM.EditText)
            Me.EditText6 = CType(Me.GetItem("tentry").Specific, SAPbouiCOM.EditText)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()
            AddHandler DataLoadAfter, AddressOf Me.Form_DataLoadAfter
            AddHandler ResizeAfter, AddressOf Me.Form_ResizeAfter
            AddHandler DataAddBefore, AddressOf Me.Form_DataAddBefore

        End Sub

        Private Sub OnCustomInitialize()
            Try
                objform = objaddon.objapplication.Forms.GetForm("VATT", Me.FormCount)
                objform = objaddon.objapplication.Forms.ActiveForm
                objform.Freeze(True)
                odbdsheader = objform.DataSources.DBDataSources.Item(CType(0, Object))
                odbdsDetails = objform.DataSources.DBDataSources.Item(CType(1, Object))
                objform.Items.Item("txtdate").Specific.string = Now.Date.ToString("dd/MM/yy")
                odbdsheader.SetValue("DocEntry", 0, objaddon.objglobalmethods.GetNextDocentry_Value("@SMPR_ODAS"))
                objaddon.objglobalmethods.LoadSeries(objform, odbdsheader, "ODAS")
                odbdsheader.SetValue("DocNum", 0, objaddon.objglobalmethods.GetDocnum_BaseonSeries("ODAS"))

                objform.Items.Item("txtrem").Specific.String = "Created By " & objaddon.objcompany.UserName & " on " & Now.ToString("dd/MMM/yyyy HH:mm:ss")
                FieldSettings()
                Matrix0.Columns.Item("AttDate").Editable = True
                Matrix0.Columns.Item("EmpNo").Editable = True
                Matrix0.Columns.Item("Shift").Editable = True
                Matrix0.Columns.Item("ShiftN").Visible = False
                objform.Update()
                'Matrix0.AddRow()
                objaddon.objglobalmethods.Matrix_Addrow(Matrix0, "AttDate", "#")
                'Matrix0.Columns.Item("Branch").Visible = False
                'Matrix0.Columns.Item("Loc").Visible = False
                objform.Settings.Enabled = True
                objaddon.objapplication.Menus.Item("1300").Activate()
                EditText6.Item.Visible = False
                objform.Freeze(False)
            Catch ex As Exception
                objform.Freeze(False)
            End Try

        End Sub

#Region "Field Details"

        Private WithEvents Button0 As SAPbouiCOM.Button
        Private WithEvents Button1 As SAPbouiCOM.Button
        Private WithEvents StaticText0 As SAPbouiCOM.StaticText
        Private WithEvents EditText0 As SAPbouiCOM.EditText
        Private WithEvents Matrix0 As SAPbouiCOM.Matrix
        Private WithEvents StaticText1 As SAPbouiCOM.StaticText
        Private WithEvents EditText1 As SAPbouiCOM.EditText
        Private WithEvents Button3 As SAPbouiCOM.Button
        Private WithEvents EditText2 As SAPbouiCOM.EditText
        Private WithEvents Button4 As SAPbouiCOM.Button
        Private WithEvents Button2 As SAPbouiCOM.Button
        Private WithEvents StaticText2 As SAPbouiCOM.StaticText
        Private WithEvents EditText3 As SAPbouiCOM.EditText
        Private WithEvents StaticText3 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox0 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText4 As SAPbouiCOM.StaticText
        Private WithEvents EditText4 As SAPbouiCOM.EditText
        Private WithEvents EditText5 As SAPbouiCOM.EditText

#End Region

        Private Sub RemoveLastrow(ByVal omatrix As SAPbouiCOM.Matrix, ByVal Columname_check As String)
            Try
                If omatrix.VisualRowCount = 0 Then Exit Sub
                If Columname_check.ToString = "" Then Exit Sub
                If omatrix.Columns.Item(Columname_check).Cells.Item(omatrix.VisualRowCount).Specific.string = "" Then
                    omatrix.DeleteRow(omatrix.VisualRowCount)
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub FieldSettings()
            Try
                'objform.Freeze(True)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtentry", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtdate", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "ClrData", True, False, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtadate", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtday", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cmbseries", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "btnimport", True, False, False)
                'objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "matattd", True, False, False)
                Dim Fsize As Size
                Fsize = TextRenderer.MeasureText(Button3.Caption, New Font("Arial", 12.0F))
                Button3.Item.Width = Fsize.Width + 30
                'Matrix0.CommonSetting.SetRowEditable(1, False)
                'Matrix0.Columns.Item("EmpName").Visible = False
                Matrix0.Columns.Item("actlineid").Visible = False
                Matrix0.Columns.Item("Desig").Visible = False
                Matrix0.Columns.Item("Post").Visible = False
                Matrix0.Columns.Item("shifthrs").Visible = False
                Matrix0.Columns.Item("HrsWork").Visible = False


                'Matrix0.AutoResizeColumns()
                'objform.Freeze(False)
            Catch ex As Exception
                objform.Freeze(False)
            End Try

        End Sub

        Private Sub Button0_ClickBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles Button0.ClickBefore
            'Try
            '    If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
            '        If Matrix0.VisualRowCount = 1 Then
            '            If Matrix0.Columns.Item("AttDate").Cells.Item(1).Specific.String = "" Then
            '                objaddon.objapplication.SetStatusBarMessage("Minimum One Line required...", SAPbouiCOM.BoMessageTime.bmt_Short, True) : BubbleEvent = False : Exit Sub
            '            End If
            '        End If

            '        RemoveLastrow(Matrix0, "EmpId")

            '        Dim Errmsg As String = "", Status = ""
            '        'objaddon.objapplication.SetStatusBarMessage("Validating Excel data Please wait...", SAPbouiCOM.BoMessageTime.bmt_Medium, False)
            '        Dim m_oProgBar As SAPbouiCOM.ProgressBar
            '        m_oProgBar = objaddon.objapplication.StatusBar.CreateProgressBar("My Progress Bar", Matrix0.VisualRowCount, True)
            '        m_oProgBar.Text = "Validating Excel data Please wait..."
            '        'Dim iPos As Integer = 0
            '        'iPos = m_oProgBar.Value
            '        Try
            '            objform.Freeze(True)
            '            Dim AttDate As String = "", EmpId As String = ""
            '            For i As Integer = 1 To Matrix0.VisualRowCount
            '                AttDate = Matrix0.Columns.Item("AttDate").Cells.Item(i).Specific.String
            '                EmpId = Matrix0.Columns.Item("EmpNo").Cells.Item(i).Specific.String
            '                For j As Integer = i + 1 To Matrix0.VisualRowCount
            '                    If EmpId = Matrix0.Columns.Item("EmpNo").Cells.Item(j).Specific.String And AttDate = Matrix0.Columns.Item("AttDate").Cells.Item(j).Specific.String Then
            '                        'If objaddon.objapplication.MessageBox("Duplicate Found... Do you want delete the duplicate records?", 2, "OK", "Cancel") <> 1 Then j += 1
            '                        Errmsg += vbCrLf + "Duplicate Attendance entered for the employee " & EmpId & " AttDate" & AttDate & " Please Remove" & " RowNo: " & j
            '                        'Matrix0.DeleteRow(j)
            '                    End If
            '                Next
            '            Next

            '            For i As Integer = 1 To Matrix0.VisualRowCount
            '                If Matrix0.Columns.Item("AttDate").Cells.Item(i).Specific.String <> "" Then
            '                    'Dim st As String = Matrix0.Columns.Item("attstatus").Cells.Item(i).Specific.String
            '                    If Matrix0.Columns.Item("attstatus").Cells.Item(i).Specific.String <> "LOP" And Matrix0.Columns.Item("attstatus").Cells.Item(i).Specific.String <> "WO" And Matrix0.Columns.Item("attstatus").Cells.Item(i).Specific.String <> "EL" Then
            '                        'If Matrix0.Columns.Item("AttDate").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("EmpId").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("EmpNo").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("EmpName").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("Desig").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("Post").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("attstatus").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("Half").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("Shift").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("shifthrs").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("Timein").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("Timeout").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("HrsWork").Cells.Item(i).Specific.String = "" Then
            '                        If Matrix0.Columns.Item("AttDate").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("EmpId").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("EmpNo").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("attstatus").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("Half").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("Shift").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("Timein").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("Timeout").Cells.Item(i).Specific.String = "" Then
            '                            'objaddon.objapplication.SetStatusBarMessage("Please update all the column values" & " RowNo: " & i, SAPbouiCOM.BoMessageTime.bmt_Medium, True)
            '                            'BubbleEvent = False : Exit Sub
            '                            Errmsg += vbCrLf + "Please update all the column values" & " RowNo: " & i
            '                        End If
            '                        'If Matrix0.Columns.Item("shifthrs").Cells.Item(i).Specific.String = "" Then
            '                        '    Matrix0.Columns.Item("shifthrs").Cells.Item(i).Specific.String = "0"
            '                        'End If
            '                        If Matrix0.Columns.Item("Timein").Cells.Item(i).Specific.String = "" Then
            '                            Matrix0.Columns.Item("Timein").Cells.Item(i).Specific.String = "0"
            '                        End If
            '                        If Matrix0.Columns.Item("Timeout").Cells.Item(i).Specific.String = "" Then
            '                            Matrix0.Columns.Item("Timeout").Cells.Item(i).Specific.String = "0"
            '                        End If
            '                        'If Matrix0.Columns.Item("HrsWork").Cells.Item(i).Specific.String = "" Then
            '                        '    Matrix0.Columns.Item("HrsWork").Cells.Item(i).Specific.String = "0"
            '                        'End If
            '                        'If Left(Matrix0.Columns.Item("shifthrs").Cells.Item(i).Specific.String, 2) > 15 Then
            '                        '    'objaddon.objapplication.SetStatusBarMessage("Shift Hours not to exceed 15" & " RowNo: " & i, SAPbouiCOM.BoMessageTime.bmt_Medium, True)
            '                        '    Errmsg += vbCrLf + "Shift Hours not to exceed 15" & " RowNo: " & i
            '                        'End If

            '                        If Left(Matrix0.Columns.Item("Timein").Cells.Item(i).Specific.String, 2) < 8 And Left(Matrix0.Columns.Item("Timein").Cells.Item(i).Specific.String, 2) <= 0 Then
            '                            ' objaddon.objapplication.SetStatusBarMessage("In Time not less than 8" & " RowNo: " & i, SAPbouiCOM.BoMessageTime.bmt_Medium, True)
            '                            Errmsg += vbCrLf + "In Time not less than 8 or 0" & " RowNo: " & i
            '                        End If
            '                        If Left(Matrix0.Columns.Item("Timeout").Cells.Item(i).Specific.String, 2) > 22 And Left(Matrix0.Columns.Item("Timeout").Cells.Item(i).Specific.String, 2) <= 0 Then
            '                            '  objaddon.objapplication.SetStatusBarMessage("Out Time not to exceed 22" & " RowNo: " & i, SAPbouiCOM.BoMessageTime.bmt_Medium, True)
            '                            Errmsg += vbCrLf + "Out Time not to exceed 22 or 0" & " RowNo: " & i
            '                        End If
            '                        'If Left(Matrix0.Columns.Item("HrsWork").Cells.Item(i).Specific.String, 2) > 24 And Left(Matrix0.Columns.Item("HrsWork").Cells.Item(i).Specific.String, 2) <= 0 Then
            '                        '    ' objaddon.objapplication.SetStatusBarMessage("Hours Worked not to exceed 24" & " RowNo: " & i, SAPbouiCOM.BoMessageTime.bmt_Medium, True)
            '                        '    Errmsg += vbCrLf + "Hours Worked not to exceed 24 or 0" & " RowNo: " & i
            '                        'End If
            '                        'Status = objaddon.objglobalmethods.getSingleValue("select 1 as ""Status"" from ""@SMPR_OHEM"" where ""U_empID""='" & Trim(Matrix0.Columns.Item("EmpId").Cells.Item(i).Specific.String) & "' and ""U_ExtEmpNo""='" & Trim(Matrix0.Columns.Item("EmpNo").Cells.Item(i).Specific.String) & "' and ""U_shiftcde""=(select ""Code"" from ""@SMHR_OSFT"" where ""Name""='" & Trim(Matrix0.Columns.Item("Shift").Cells.Item(i).Specific.String) & "') ")
            '                        'If Status <> "1" Then
            '                        '    ' objaddon.objapplication.SetStatusBarMessage("Please enter the valid data of EMPID or EMPNO" & " RowNo: " & i, SAPbouiCOM.BoMessageTime.bmt_Medium, True)
            '                        '    Errmsg += vbCrLf + "Please enter the valid data of EMPID or EMPNO or Shift Name" & " RowNo: " & i
            '                        'End If
            '                        'Format(ExcelWorkSheet.Cells(i, 1).Value, "dd/MM/yy")
            '                    End If
            '                    Dim Result As String
            '                    Dim AttdDate As Date
            '                    Dim txtAttDate As SAPbouiCOM.EditText
            '                    txtAttDate = Matrix0.Columns.Item("AttDate").Cells.Item(i).Specific
            '                    If Matrix0.Columns.Item("AttDate").Cells.Item(i).Specific.String <> "" Then
            '                        AttdDate = Date.ParseExact(txtAttDate.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo) 'Matrix0.Columns.Item("AttDate").Cells.Item(i).Specific.String
            '                    Else
            '                        Errmsg += vbCrLf + "Attendance date cannot be empty" & " RowNo: " & i
            '                    End If
            '                    Result = objaddon.objglobalmethods.getSingleValue("select 1 as ""Result"" from ""@SMPR_ODAS"" T0 join ""@SMPR_DAS1"" T1 on T0.""DocEntry""=T1.""DocEntry"" where T1.""U_IDNo""='" & Matrix0.Columns.Item("EmpNo").Cells.Item(i).Specific.String & "' and T1.""U_AttDate""='" & AttdDate.ToString("yyyyMMdd") & "'")
            '                    If Result = "1" Then
            '                        '  objaddon.objapplication.SetStatusBarMessage("Already Posted Attendance to this Employee " & Matrix0.Columns.Item("EmpNo").Cells.Item(i).Specific.String & " - " & Matrix0.Columns.Item("AttDate").Cells.Item(i).Specific.ToString("yyyyMMdd") & "", SAPbouiCOM.BoMessageTime.bmt_Short, True)
            '                        Errmsg += vbCrLf + "Please remove the Already Posted Attendance Employee " & Matrix0.Columns.Item("EmpNo").Cells.Item(i).Specific.String & " for the date: " & AttdDate.ToString("dd/MM/yy") & " RowNo: " & i
            '                    End If
            '                End If
            '                m_oProgBar.Value = i
            '            Next
            '            m_oProgBar.Stop()
            '            m_oProgBar = Nothing
            '            GC.Collect()

            '            If Errmsg <> "" Then
            '                ' objaddon.objglobalmethods.WriteSMSLog(Errmsg)
            '                objaddon.objglobalmethods.WriteErrorLog(Errmsg)
            '                'If objaddon.objapplication.MessageBox("Please see the error log and correct the mentioned errors...", 1, "OK", "Cancel") = 2 Then Exit Sub
            '                objaddon.objapplication.SetStatusBarMessage("Please see the error log in SAP attachment folder and correct the mentioned errors...", SAPbouiCOM.BoMessageTime.bmt_Short, True)
            '                'If MessageBox.Show("Please see the error log and correct the mentioned errors...", "Errors Found", MessageBoxButtons.OKCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, False) = DialogResult.Cancel Then Exit Sub
            '                BubbleEvent = False : Exit Sub
            '            End If
            '            objform.Freeze(False)
            '            objaddon.objapplication.SetStatusBarMessage("Validation Completed...", SAPbouiCOM.BoMessageTime.bmt_Medium, False)

            '        Catch ex As Exception
            '            m_oProgBar.Stop()
            '            m_oProgBar = Nothing
            '            objform.Freeze(False)
            '            objaddon.objapplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)
            '            BubbleEvent = False
            '        End Try
            '    End If
            'Catch ex As Exception

            'End Try
        End Sub

        Private Sub ReadExcel(ByVal FileName As String)
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            Dim ExcelWorkbook As Microsoft.Office.Interop.Excel.Workbook = Nothing
            Dim ExcelWorkSheet As Microsoft.Office.Interop.Excel.Worksheet = Nothing
            Dim excelRng As Microsoft.Office.Interop.Excel.Range
            Dim j As Integer = 1, i As Integer = 0
            Dim Getvalue As String
            Dim m_oProgBar As SAPbouiCOM.ProgressBar
            Dim Flag As Boolean = False
            'Dim iPos As Integer = 0
            'iPos = m_oProgBar.Value
            Try

                FileName = objform.Items.Item("txtFName").Specific.string
                'Dim RowIndex As Integer
                ExcelWorkbook = ExcelApp.Workbooks.Open(FileName)
                ExcelWorkSheet = ExcelWorkbook.ActiveSheet
                'excelRng = ExcelWorkSheet.Range("A1")
                excelRng = ExcelWorkSheet.UsedRange
                'objaddon.objapplication.SetStatusBarMessage("Excel Loading please wait...", SAPbouiCOM.BoMessageTime.bmt_Long, False)
                objaddon.objapplication.StatusBar.SetText("Excel Loading please wait...", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
                m_oProgBar = objaddon.objapplication.StatusBar.CreateProgressBar("My Progress", excelRng.Rows.Count, True)
                m_oProgBar.Text = "Excel Loading please wait..."
                m_oProgBar.Value = 0
                objform.Freeze(True)
                'Matrix0.Clear()
                odbdsDetails = objform.DataSources.DBDataSources.Item(CType(1, Object))
                odbdsDetails.Clear()
                Matrix0.LoadFromDataSource()
                odbdsDetails.InsertRecord(odbdsDetails.Size)
                If objform.Items.Item("txtFName").Specific.String <> "" Then
                    'If ExcelWorkSheet.Cells(1, 1).Value = "AttendanceDate" And ExcelWorkSheet.Cells(1, 2).Value = "EmpID" And ExcelWorkSheet.Cells(1, 3).Value = "EmpNo" And ExcelWorkSheet.Cells(1, 4).Value = "EmpName" And ExcelWorkSheet.Cells(1, 5).Value = "Designation" And ExcelWorkSheet.Cells(1, 6).Value = "Position" And ExcelWorkSheet.Cells(1, 7).Value = "Attendance" And ExcelWorkSheet.Cells(1, 8).Value = "HalfDay" And ExcelWorkSheet.Cells(1, 9).Value = "ShiftName" And ExcelWorkSheet.Cells(1, 10).Value = "ShiftHrs" And ExcelWorkSheet.Cells(1, 11).Value = "TimeIn" And ExcelWorkSheet.Cells(1, 12).Value = "TimeOut" And ExcelWorkSheet.Cells(1, 13).Value = "HrsWorked" Then
                    'If ExcelWorkSheet.Cells(1, 1).Value = "AttendanceDate" And ExcelWorkSheet.Cells(1, 2).Value = "EmpID" And ExcelWorkSheet.Cells(1, 3).Value = "EmpNo" And ExcelWorkSheet.Cells(1, 4).Value = "Attendance" And ExcelWorkSheet.Cells(1, 5).Value = "HalfDay" And ExcelWorkSheet.Cells(1, 6).Value = "ShiftName" And ExcelWorkSheet.Cells(1, 7).Value = "TimeIn" And ExcelWorkSheet.Cells(1, 8).Value = "TimeOut" Then
                    If ExcelWorkSheet.Cells(1, 1).Value = "AttendanceDate" And ExcelWorkSheet.Cells(1, 2).Value = "EmpNo" And ExcelWorkSheet.Cells(1, 3).Value = "Attendance" And ExcelWorkSheet.Cells(1, 4).Value = "HalfDay" And ExcelWorkSheet.Cells(1, 5).Value = "ShiftName" And ExcelWorkSheet.Cells(1, 6).Value = "TimeIn" And ExcelWorkSheet.Cells(1, 7).Value = "TimeOut" Then
                        'For RowIndex = 2 To excelRng.Rows.Count
                        '    If CStr(ExcelWorkSheet.Cells(RowIndex, 1).Value) = "" Then Continue For
                        '    If Matrix0.VisualRowCount = 0 Then Matrix0.AddRow()
                        '    If Matrix0.Columns.Item("AttDate").Cells.Item(j).Specific.String <> "" Then Matrix0.AddRow()
                        '    '  Dim AttDate As Date = Date.ParseExact(ExcelWorkSheet.Cells(RowIndex, 1).Value, "DD/MM/YY", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                        '    Matrix0.Columns.Item("#").Cells.Item(j).Specific.String = j
                        '    Matrix0.Columns.Item("AttDate").Cells.Item(j).Specific.String = ExcelWorkSheet.Cells(RowIndex, 1).Value 'Format(ExcelWorkSheet.Cells(RowIndex, 1).Value, "dd/MM/yy")
                        '    Matrix0.Columns.Item("EmpId").Cells.Item(j).Specific.String = CStr(ExcelWorkSheet.Cells(RowIndex, 2).Value)
                        '    Matrix0.Columns.Item("EmpNo").Cells.Item(j).Specific.String = CStr(ExcelWorkSheet.Cells(RowIndex, 3).Value)
                        '    'Getvalue = objaddon.objglobalmethods.getSingleValue("Select case when T0.""U_lastName""<>'' then T0.""U_firstNam"" || ' ' || T0.""U_lastName"" else T0.""U_firstNam"" end AS ""EmpName"" from ""@SMPR_OHEM"" T0 where T0.""U_empID""='" & CStr(ExcelWorkSheet.Cells(RowIndex, 2).Value) & "' ")
                        '    'Matrix0.Columns.Item("EmpName").Cells.Item(j).Specific.String = Getvalue
                        '    'Matrix0.Columns.Item("EmpName").Cells.Item(j).Specific.String = CStr(ExcelWorkSheet.Cells(RowIndex, 4).Value)
                        '    'Matrix0.Columns.Item("Desig").Cells.Item(j).Specific.String = CStr(ExcelWorkSheet.Cells(RowIndex, 5).Value)
                        '    'Matrix0.Columns.Item("Post").Cells.Item(j).Specific.String = CStr(ExcelWorkSheet.Cells(RowIndex, 6).Value)
                        '    Matrix0.Columns.Item("attstatus").Cells.Item(j).Specific.String = CStr(ExcelWorkSheet.Cells(RowIndex, 4).Value)
                        '    Matrix0.Columns.Item("Half").Cells.Item(j).Specific.String = CStr(ExcelWorkSheet.Cells(RowIndex, 5).Value)
                        '    Matrix0.Columns.Item("Shift").Cells.Item(j).Specific.String = CStr(ExcelWorkSheet.Cells(RowIndex, 6).Value)
                        '    'Dim Val As String = GetConcatenateValue(ExcelWorkSheet.Cells(RowIndex, 10).Value)
                        '    'Matrix0.Columns.Item("shifthrs").Cells.Item(j).Specific.String = GetConcatenateValue(ExcelWorkSheet.Cells(RowIndex, 10).Value)
                        '    Matrix0.Columns.Item("Timein").Cells.Item(j).Specific.String = GetConcatenateValue(ExcelWorkSheet.Cells(RowIndex, 7).Value)
                        '    Matrix0.Columns.Item("Timeout").Cells.Item(j).Specific.String = GetConcatenateValue(ExcelWorkSheet.Cells(RowIndex, 8).Value)
                        '    'Matrix0.Columns.Item("HrsWork").Cells.Item(j).Specific.String = GetConcatenateValue(ExcelWorkSheet.Cells(RowIndex, 13).Value)
                        '    'Matrix0.Columns.Item("AInTime").Cells.Item(j).Specific.String = CStr(ExcelWorkSheet.Cells(RowIndex, 14).Value)
                        '    'Matrix0.Columns.Item("AOutTime").Cells.Item(j).Specific.String = CStr(ExcelWorkSheet.Cells(RowIndex, 15).Value)
                        '    j += 1
                        'Next RowIndex

                        objFinalDT.Clear()
                        If objFinalDT.Columns.Count = 0 Then
                            objFinalDT.Columns.Add(Matrix0.Columns.Item("actlineid").UniqueID)
                            objFinalDT.Columns.Add(Matrix0.Columns.Item("AttDate").UniqueID)
                            objFinalDT.Columns.Add(Matrix0.Columns.Item("EmpNo").UniqueID)
                            objFinalDT.Columns.Add(Matrix0.Columns.Item("attstatus").UniqueID)
                            objFinalDT.Columns.Add(Matrix0.Columns.Item("Half").UniqueID)
                            objFinalDT.Columns.Add(Matrix0.Columns.Item("Shift").UniqueID)
                            objFinalDT.Columns.Add(Matrix0.Columns.Item("Timein").UniqueID)
                            objFinalDT.Columns.Add(Matrix0.Columns.Item("Timeout").UniqueID)
                        End If
                        Dim TimeInValue, TimeOutValue As TimeSpan
                        For RowIndex As Integer = 2 To excelRng.Rows.Count
                            If CStr(ExcelWorkSheet.Cells(RowIndex, 1).Value) = "" Then Continue For
                            odbdsDetails.SetValue("LineId", i, j)
                            odbdsDetails.SetValue("U_ALineId", i, j)
                            odbdsDetails.SetValue("U_AttDate", i, Format(ExcelWorkSheet.Cells(RowIndex, 1).Value, "yyyyMMdd"))
                            Getvalue = objaddon.objglobalmethods.getSingleValue("Select ""U_empID"" from ""@SMPR_OHEM"" T0 where T0.""U_ExtEmpNo""='" & CStr(ExcelWorkSheet.Cells(RowIndex, 2).Value) & "' ")
                            odbdsDetails.SetValue("U_empID", i, Getvalue)
                            odbdsDetails.SetValue("U_IDNo", i, CStr(ExcelWorkSheet.Cells(RowIndex, 2).Value))
                            Getvalue = objaddon.objglobalmethods.getSingleValue("Select case when T0.""U_lastName""<>'' then T0.""U_firstNam"" || ' ' || T0.""U_lastName"" else T0.""U_firstNam"" end AS ""EmpName"" from ""@SMPR_OHEM"" T0 where T0.""U_ExtEmpNo""='" & CStr(ExcelWorkSheet.Cells(RowIndex, 2).Value) & "' ")
                            odbdsDetails.SetValue("U_empName", i, Getvalue)
                            odbdsDetails.SetValue("U_AttStatus", i, CStr(ExcelWorkSheet.Cells(RowIndex, 3).Value))
                            odbdsDetails.SetValue("U_Halfday", i, CStr(ExcelWorkSheet.Cells(RowIndex, 4).Value))
                            'Getvalue = objaddon.objglobalmethods.getSingleValue("Select ""Name"" from ""@SMHR_OSFT"" where ""Code""=(Select ""U_shiftcde"" from ""@SMPR_OHEM"" where ""U_ExtEmpNo""='" & CStr(ExcelWorkSheet.Cells(RowIndex, 2).Value) & "') ")
                            odbdsDetails.SetValue("U_ShiftCode", i, CStr(ExcelWorkSheet.Cells(RowIndex, 5).Value))
                            Getvalue = objaddon.objglobalmethods.getSingleValue("Select ""Name"" from ""@SMHR_OSFT"" where ""Code""='" & CStr(ExcelWorkSheet.Cells(RowIndex, 5).Value) & "' ")
                            odbdsDetails.SetValue("U_ShiftName", i, Getvalue) ' Getvalue CStr(ExcelWorkSheet.Cells(RowIndex, 5).Value)
                            TimeInValue = TimeSpan.FromHours(24 * CStr(ExcelWorkSheet.Cells(RowIndex, 6).Value.ToString))
                            odbdsDetails.SetValue("U_TimeIn", i, Trim(Left(TimeInValue.ToString.Replace(":", ""), 4)))
                            TimeOutValue = TimeSpan.FromHours(24 * CStr(ExcelWorkSheet.Cells(RowIndex, 7).Value.ToString))
                            odbdsDetails.SetValue("U_TimeOut", i, Trim(Left(TimeOutValue.ToString.Replace(":", ""), 4)))
                            i += 1 : j += 1
                            If j <> excelRng.Rows.Count Then odbdsDetails.InsertRecord(odbdsDetails.Size)
                            m_oProgBar.Value = RowIndex
                            objFinalDT.Rows.Add(i, Format(ExcelWorkSheet.Cells(RowIndex, 1).Value, "yyyyMMdd"), CStr(ExcelWorkSheet.Cells(RowIndex, 2).Value), CStr(ExcelWorkSheet.Cells(RowIndex, 3).Value), CStr(ExcelWorkSheet.Cells(RowIndex, 4).Value), CStr(ExcelWorkSheet.Cells(RowIndex, 5).Value), Trim(Left(TimeInValue.ToString.Replace(":", ""), 4)), Trim(Left(TimeOutValue.ToString.Replace(":", ""), 4)))
                        Next
                        Matrix0.LoadFromDataSource()
                    Else
                        objaddon.objapplication.StatusBar.SetText("Expected ColumnName Not found...Please check the excel format", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                        Exit Sub
                    End If
                    objform.Freeze(False)
                    objaddon.objapplication.StatusBar.SetText("Excel Loaded Successfully...", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                    'm_oProgBar.Stop()
                    'System.Runtime.InteropServices.Marshal.ReleaseComObject(m_oProgBar)
                    'm_oProgBar = Nothing
                    'GC.Collect()
                End If
                Dim indicesOfRowsToDelete = objFinalDT.AsEnumerable _
                              .Select(Function(r, n) New With {Key r, Key n}) _
                              .GroupBy(Function(rn) New With {Key .OrderNumber = rn.r.Field(Of String)("EmpNo"), Key .RequestType = rn.r.Field(Of String)("AttDate")}) _
                              .SelectMany(Function(rg) rg.Skip(1).Select(Function(rn) rn.n)) _
                              .OrderByDescending(Function(n) n)

                For Each n In indicesOfRowsToDelete
                    'objFinalDT.Rows(n).Delete()
                    'Matrix0.CommonSetting.SetRowFontColor(n + 1, Color.Blue.B)
                    Matrix0.CommonSetting.SetCellFontColor(n + 1, 2, Color.Blue.B)
                    Matrix0.CommonSetting.SetCellFontColor(n + 1, 4, Color.Blue.B)
                    Flag = True
                    'Errmsg += vbCrLf + "Duplicate Attendance entered Please Remove on RowNo: " & n + 1
                Next
                If Flag = True Then
                    'm_oProgBar.Text = "Duplicate Attendance entered. Please Remove the highlighted data..."
                    objaddon.objapplication.StatusBar.SetText("Duplicate Attendance entered. Please Remove the highlighted data...", SAPbouiCOM.BoMessageTime.bmt_Long, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                End If
                objaddon.objapplication.Menus.Item("1300").Activate()
                'Dim ss As String
                'ss = Matrix0.SerializeAsXML(SAPbouiCOM.BoMatrixXmlSelect.mxs_All)

                'Dim objDTable As SAPbouiCOM.DataTable
                'If objform.DataSources.DataTables.Count.Equals(0) Then
                '    objform.DataSources.DataTables.Add("DT_List")
                'Else
                '    objform.DataSources.DataTables.Item("DT_List").Clear()
                'End If
                'objDTable = objform.DataSources.DataTables.Item("DT_List")
                'Dim xdoc As System.Xml.XmlDocument = New System.Xml.XmlDocument()
                'xdoc.LoadXml(ss)
                'xdoc.Save("E:\Chitra\Common Payroll\Dec 16\test1.xml") 'Windows.Forms.Application.StartupPath & "\test" & ".xml"

                'Dim myFileStream As FileStream = New FileStream("E:\Chitra\Common Payroll\Dec 16\test1.xml", FileMode.Open)
                'Dim sr1 As StreamReader = New StreamReader(myFileStream)
                'Dim strUTF8OnlyColumn As String = sr1.ReadToEnd()
                ''objDTable.LoadFromXML(strUTF8OnlyColumn) 'strUTF8OnlyColumn
                'objDTable.LoadSerializedXML(SAPbouiCOM.BoDataTableXmlSelect.dxs_MetaData, strUTF8OnlyColumn)
                'sr1.Close()
                'For ir As Integer = 0 To objDTable.Rows.Count - 1
                '    MsgBox(ir)
                'Next
                ''objDTable.LoadSerializedXML(SAPbouiCOM.BoDataTableXmlSelect.dxs_All, ss)
                'ExcelApp.ActiveWorkbook.Close()

                'objaddon.objapplication.SetStatusBarMessage("Excel Loaded..." & DocEntry, SAPbouiCOM.BoMessageTime.bmt_Long, False)
            Catch ex As Exception
                objform.Freeze(False)
                'm_oProgBar.Stop()
                'System.Runtime.InteropServices.Marshal.ReleaseComObject(m_oProgBar)
                'm_oProgBar = Nothing
                'ExcelApp.ActiveWorkbook.Close()
                objaddon.objapplication.SetStatusBarMessage("Read-Excel: " & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short)
            Finally
                objform.Freeze(False)
                m_oProgBar.Stop()
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_oProgBar)
                m_oProgBar = Nothing
                ExcelApp.ActiveWorkbook.Close()
                GC.Collect()
            End Try
        End Sub

        Private Function Matrix_DataTable_Update(ByVal Row As Integer, ByVal ColName As String) As DataTable
            Try

                If objFinalDT.Rows.Count > 0 Then

                    If objFinalDT.Rows(Row - 1)("actlineid").ToString = Matrix0.Columns.Item("actlineid").Cells.Item(Row).Specific.Value Then
                        If ColName <> "" Then
                            objFinalDT.Rows(Row - 1)(Matrix0.Columns.Item(ColName).UniqueID) = Matrix0.Columns.Item(ColName).Cells.Item(Row).Specific.Value
                        End If
                    End If
                    'For DTRow As Integer = 0 To objFinalDT.Rows.Count - 1
                    '    If objFinalDT.Rows(DTRow)("actlineid").ToString = Matrix0.Columns.Item("actlineid").Cells.Item(Row).Specific.Value Then
                    '        If ColName <> "" Then
                    '            objFinalDT.Rows(DTRow)(Matrix0.Columns.Item(ColName).UniqueID) = Matrix0.Columns.Item(ColName).Cells.Item(Row).Specific.Value
                    '            'DataFlag = True
                    '            Exit For
                    '        End If
                    '    End If
                    'Next

                Else

                End If


                Return objFinalDT
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Private Sub TestReadExcel(ByVal FileName As String)
            Try
                Dim ExcelConnection As New System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\MyExcelSpreadsheet.xlsx;Extended Properties=""Excel 12.0 Xml;HDR=Yes""")
                ExcelConnection.Open()

                Dim expr As String = "SELECT * FROM [Sheet1$]"


                Dim objCmdSelect As OleDbCommand = New OleDbCommand(expr, ExcelConnection)
                'Dim objDR As OleDbDataReader

                'Dim SQLconn As New SqlConnection()
                'Dim ConnString As String = "Data Source=MMSQL1;Initial Catalog=DbName; User Id=UserName; Password=password;"
                'SQLconn.ConnectionString = ConnString
                'SQLconn.Open()
            Catch ex As Exception

            End Try
        End Sub

        Private Function GetConcatenateValue(ByVal Input As String)
            Try
                'Dim ggetval As Integer = Len(Input)
                If Len(Input) = 2 Then
                    Input += "00"
                ElseIf Len(Input) = 3 Then
                    Input += "0"
                ElseIf Len(Input) = 1 Then
                    Input = "0" + Input + "00"
                ElseIf Len(Input) = 4 Then
                    Input += "0"
                Else
                    Input = Input
                End If
            Catch ex As Exception
                Input = ""
            End Try
            Return Input
        End Function

        Public Function FindFile() As String

            Dim ShowFolderBrowserThread As Threading.Thread
            Try
                ShowFolderBrowserThread = New Threading.Thread(AddressOf ShowFolderBrowser)

                If ShowFolderBrowserThread.ThreadState = System.Threading.ThreadState.Unstarted Then
                    ShowFolderBrowserThread.SetApartmentState(System.Threading.ApartmentState.STA)
                    ShowFolderBrowserThread.Start()
                ElseIf ShowFolderBrowserThread.ThreadState = System.Threading.ThreadState.Stopped Then
                    ShowFolderBrowserThread.Start()
                    ShowFolderBrowserThread.Join()
                End If

                While ShowFolderBrowserThread.ThreadState = Threading.ThreadState.Running
                    System.Windows.Forms.Application.DoEvents()
                    ' ShowFolderBrowserThread.Sleep(100)
                    Thread.Sleep(100)
                End While

                If BankFileName <> "" Then
                    Return BankFileName
                End If

            Catch ex As Exception

                objaddon.objapplication.MessageBox("File Find  Method Failed : " & ex.Message)
            End Try
            Return ""
        End Function

        Public Sub ShowFolderBrowser()
            Dim MyProcs() As System.Diagnostics.Process
            Dim nw As New NativeWindow

            Dim OpenFile As New OpenFileDialog
            Try
                ' Dim initialpath As String = objaddon.objglobalmethods.getSingleValue("select ""ExcelPath"" from oadm")
                Dim initialpath As String = System.Windows.Forms.Application.StartupPath + "\"
                OpenFile.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
                OpenFile.Multiselect = False
                'OpenFile.ShowDialog()
                OpenFile.Filter = "All files(*.)|*.*" '   "|*.*"
                Dim filterindex As Integer = 0
                Try
                    filterindex = 0
                Catch ex As Exception
                End Try

                Dim form As New System.Windows.Forms.Form
                form.TopMost = True
                OpenFile.FilterIndex = filterindex
                OpenFile.RestoreDirectory = True
                'OpenFile.CheckFileExists = True
                'OpenFile.CheckPathExists = True
                MyProcs = Process.GetProcessesByName("SAP Business One")
                'nw.AssignHandle(System.Diagnostics.Process.GetProcessesByName("SAP Business One")(0).MainWindowHandle)
                'NativeWindow.FromHandle(System.Diagnostics.Process.GetProcessesByName("SAP Business One")(0).MainWindowHandle)
                'If MyProcs.Length = 1 Then
                If MyProcs.Length >= 1 Then
                    For i As Integer = 0 To MyProcs.Length - 1
                        Dim comname As String() = MyProcs(i).MainWindowTitle.ToString.Split("-")

                        'Open dialog only for the company where the button is clicked
                        Dim com As String = objaddon.objcompany.CompanyName.ToString.Trim.ToUpper
                        'If comname(1).ToString.Trim.ToUpper = com Then
                        Dim MyWindow As New WindowWrapper(MyProcs(i).MainWindowHandle)

                        'Dim ret As System.Windows.Forms.DialogResult = OpenFile.ShowDialog(MyWindow)
                        'Dim ret As System.Windows.Forms.DialogResult = OpenFile.
                        If OpenFile.ShowDialog(NativeWindow.FromHandle(System.Diagnostics.Process.GetProcessesByName("SAP Business One")(0).MainWindowHandle)) <> System.Windows.Forms.DialogResult.Cancel Then
                            BankFileName = OpenFile.FileName
                            'OpenFile.Dispose()
                        Else
                            System.Windows.Forms.Application.ExitThread()
                        End If
                        'End If
                        Exit For
                    Next
                    '  Else
                End If
            Catch ex As Exception
                objaddon.objapplication.StatusBar.SetText(ex.Message)
                BankFileName = ""
            Finally
                OpenFile.Dispose()
            End Try
        End Sub

        Public Class WindowWrapper

            Implements System.Windows.Forms.IWin32Window
            Private _hwnd As IntPtr

            Public Sub New(ByVal handle As IntPtr)
                _hwnd = handle
            End Sub

            Public ReadOnly Property Handle() As System.IntPtr Implements System.Windows.Forms.IWin32Window.Handle
                Get
                    Return _hwnd
                End Get
            End Property

        End Class

        'Private Sub Button2_ClickBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles Button2.ClickBefore

        '    'Try
        '    '    Filename = FindFile()
        '    '    objfile = New FileInfo(Filename)
        '    '    If Filename <> "" Then
        '    '        objform.Items.Item("txtFName").Specific.string = CStr(objfile.Name)
        '    '    Else
        '    '        Exit Sub
        '    '    End If
        '    'Catch ex As Exception
        '    '    If Filename <> "" Then
        '    '        objform.Items.Item("txtFName").Specific.string = CStr(objfile.Name)
        '    '    Else
        '    '        Exit Sub
        '    '    End If
        '    'End Try
        'End Sub

        'Private Sub Button2_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button2.ClickAfter

        '    'Try
        '    '    If Filename <> "" Then
        '    '        objform.Items.Item("txtFName").Specific.string = CStr(objfile.Name)
        '    '    Else
        '    '        Exit Sub
        '    '    End If
        '    'Catch ex As Exception
        '    '    If Filename <> "" Then
        '    '        objform.Items.Item("txtFName").Specific.string = CStr(objfile.Name)
        '    '    Else
        '    '        Exit Sub
        '    '    End If
        '    'End Try
        'End Sub

        Private Sub Button3_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button3.ClickAfter
            Try
                If EditText2.Value = "" Then Exit Sub
                'objaddon.objapplication.SetStatusBarMessage("Attendance Loading Please wait...", SAPbouiCOM.BoMessageTime.bmt_Short, False)
                ReadExcel(Filename)
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Button0_PressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button0.PressedAfter
            Try
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE And pVal.ActionSuccess = True Then
                    objform.Items.Item("txtdate").Specific.string = Now.Date.ToString("dd/MM/yy")
                    'odbdsheader.SetValue("DocEntry", 0, objaddon.objglobalmethods.GetNextDocentry_Value("@SMPR_ODAS"))
                    objaddon.objglobalmethods.LoadSeries(objform, odbdsheader, "ODAS")
                    'Matrix0.AddRow()
                    objform.Items.Item("txtrem").Specific.String = "Created By " & objaddon.objcompany.UserName & " on " & Now.ToString("dd/MMM/yyyy HH:mm:ss")
                    objaddon.objglobalmethods.Matrix_Addrow(Matrix0, "AttDate", "#")
                    Matrix0.Columns.Item("AttDate").Editable = True
                    Matrix0.Columns.Item("EmpNo").Editable = True
                    Matrix0.Columns.Item("Shift").Editable = True
                    objFinalDT.Clear()
                End If

            Catch ex As Exception

            End Try

        End Sub

        Private Sub EditText2_LostFocusAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText2.LostFocusAfter
            'objform.Items.Item("txtdate").Specific.string = Now.Date.ToString("dd/MM/yy")
            'odbdsheader.SetValue("DocEntry", 0, objaddon.objglobalmethods.GetNextDocentry_Value("@SMPR_ODAS"))
        End Sub

        Private Sub Button3_ClickBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles Button3.ClickBefore
            Try
                If Button3.Item.Enabled = False Then BubbleEvent = False : Exit Sub
                If EditText2.Value = "" Then
                    objaddon.objapplication.SetStatusBarMessage("Please Select a Excel File...", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Button2_ClickBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles Button2.ClickBefore
            Try
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                    Matrix0.Clear()
                    EditText2.Value = ""
                    'Matrix0.AddRow()
                    objaddon.objglobalmethods.Matrix_Addrow(Matrix0, "AttDate", "#")
                End If
            Catch ex As Exception
            End Try

        End Sub

        Private Sub Matrix0_ValidateAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.ValidateAfter
            Try
                If pVal.ItemChanged = False Then Exit Sub
                If pVal.ColUID = "EmpNo" Then
                    Dim Getval As String
                    Getval = objaddon.objglobalmethods.getSingleValue("Select ""U_empID"" from ""@SMPR_OHEM"" T0 where T0.""U_ExtEmpNo""='" & Matrix0.Columns.Item("EmpNo").Cells.Item(pVal.Row).Specific.String & "' ")
                    If Getval <> "" Then Matrix0.Columns.Item("EmpId").Cells.Item(pVal.Row).Specific.String = Getval

                    Getval = objaddon.objglobalmethods.getSingleValue("Select case when T0.""U_lastName""<>'' then T0.""U_firstNam"" || ' ' || T0.""U_lastName"" else T0.""U_firstNam"" end AS ""EmpName"" from ""@SMPR_OHEM"" T0 where T0.""U_ExtEmpNo""='" & Matrix0.Columns.Item("EmpNo").Cells.Item(pVal.Row).Specific.String & "' ")
                    If Getval <> "" Then Matrix0.Columns.Item("EmpName").Cells.Item(pVal.Row).Specific.String = Getval ': Matrix0.AutoResizeColumns()

                    'Getval = objaddon.objglobalmethods.getSingleValue("Select ""Name"" from ""@SMHR_OSFT"" where ""Code""=(Select ""U_shiftcde"" from ""@SMPR_OHEM"" where ""U_ExtEmpNo""='" & Matrix0.Columns.Item("EmpNo").Cells.Item(pVal.Row).Specific.String & "') ")
                    'If Getval <> "" Then Matrix0.Columns.Item("Shift").Cells.Item(pVal.Row).Specific.String = Getval
                End If

            Catch ex As Exception

            End Try

        End Sub

        Private Sub Form_DataLoadAfter(ByRef pVal As SAPbouiCOM.BusinessObjectInfo)
            Try
                Matrix0.AutoResizeColumns()
                Matrix0.Columns.Item("AttDate").Editable = False
                Matrix0.Columns.Item("EmpNo").Editable = False
                Matrix0.Columns.Item("Shift").Editable = False
                objFinalDT.Clear()
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Matrix0_LostFocusAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.LostFocusAfter
            Try
                If objform.Mode <> SAPbouiCOM.BoFormMode.fm_ADD_MODE Then Exit Sub
                If pVal.ColUID = "AttDate" Then
                    'If pVal.InnerEvent = True Then Exit Sub
                    objaddon.objglobalmethods.Matrix_Addrow(Matrix0, "AttDate", "#")
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Matrix0_LinkPressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.LinkPressedAfter
            Try
                If pVal.ColUID = "EmpId" Then
                    If Matrix0.Columns.Item("EmpId").Cells.Item(pVal.Row).Specific.string = "" Then Exit Sub
                    Link_Value = Matrix0.Columns.Item("EmpId").Cells.Item(pVal.Row).Specific.string : Link_objtype = "OHEM"
                    Dim activeform As New frmEmployeeMaster
                    activeform.Show()
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub ComboBox0_ComboSelectAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox0.ComboSelectAfter
            Try
                If objform.Mode <> SAPbouiCOM.BoFormMode.fm_ADD_MODE Then Exit Sub
                'objaddon.objglobalmethods.LoadSeries(objform, odbdsheader, "ODAS")
                odbdsDetails.SetValue("DocNum", 0, objaddon.objglobalmethods.GetDocNum("ODAS", CInt(ComboBox0.Selected.Value)))
            Catch ex As Exception

            End Try

        End Sub

        Private Sub EditText4_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText4.LostFocusAfter
            Try
                If EditText4.String <> "" Then EditText5.Value = Date.ParseExact(EditText4.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo).ToString("dddd")
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Form_DataAddBefore(ByRef pVal As SAPbouiCOM.BusinessObjectInfo, ByRef BubbleEvent As Boolean)

            Dim m_oProgBar As SAPbouiCOM.ProgressBar = Nothing
            Dim Getval As String

            Try
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                    If Matrix0.VisualRowCount = 1 Then
                        If Matrix0.Columns.Item("AttDate").Cells.Item(1).Specific.String = "" Then
                            objaddon.objapplication.SetStatusBarMessage("Minimum One Line required...", SAPbouiCOM.BoMessageTime.bmt_Short, True) : BubbleEvent = False : Exit Sub
                        End If
                    End If
                    RemoveLastrow(Matrix0, "EmpId")
                    Dim Errmsg As String = "", Status = ""
                    'objaddon.objapplication.SetStatusBarMessage("Validating Excel data Please wait...", SAPbouiCOM.BoMessageTime.bmt_Medium, False)
                    m_oProgBar = objaddon.objapplication.StatusBar.CreateProgressBar("My Progress Bar", Matrix0.VisualRowCount, True)
                    m_oProgBar.Text = "Validating Excel data Please wait..."
                    Dim iPos As Integer = 0
                    iPos = m_oProgBar.Value
                    Try
                        objform.Freeze(True)
                        Dim AttDate As String = "", EmpId As String = ""
                        'For i As Integer = 1 To Matrix0.VisualRowCount
                        '    AttDate = Matrix0.Columns.Item("AttDate").Cells.Item(i).Specific.String
                        '    EmpId = Matrix0.Columns.Item("EmpNo").Cells.Item(i).Specific.String
                        '    For j As Integer = i + 1 To Matrix0.VisualRowCount
                        '        If EmpId = Matrix0.Columns.Item("EmpNo").Cells.Item(j).Specific.String And AttDate = Matrix0.Columns.Item("AttDate").Cells.Item(j).Specific.String Then
                        '            'If objaddon.objapplication.MessageBox("Duplicate Found... Do you want delete the duplicate records?", 2, "OK", "Cancel") <> 1 Then j += 1
                        '            Errmsg += vbCrLf + "Duplicate Attendance entered for the employee " & EmpId & " AttDate" & AttDate & " Please Remove" & " RowNo: " & j
                        '            Exit For
                        '            'Matrix0.DeleteRow(j)
                        '        End If
                        '    Next
                        'Next



                        Dim indicesOfRowsToDelete = objFinalDT.AsEnumerable _
                          .Select(Function(r, n) New With {Key r, Key n}) _
                          .GroupBy(Function(rn) New With {Key .OrderNumber = rn.r.Field(Of String)("EmpNo"), Key .RequestType = rn.r.Field(Of String)("AttDate")}) _
                          .SelectMany(Function(rg) rg.Skip(1).Select(Function(rn) rn.n)) _
                          .OrderByDescending(Function(n) n)
                        '.Where(Function(rg) rg.Key.RequestType = "empid") _   
                        For Each n In indicesOfRowsToDelete
                            'objFinalDT.Rows(n).Delete()
                            'Matrix0.CommonSetting.SetRowFontColor(n + 1, Color.Blue.B)
                            Matrix0.CommonSetting.SetCellFontColor(n + 1, 2, Color.Blue.B)
                            Matrix0.CommonSetting.SetCellFontColor(n + 1, 4, Color.Blue.B)
                            Errmsg += vbCrLf + "Duplicate Attendance entered Please Remove on RowNo: " & n + 1
                        Next
                        If indicesOfRowsToDelete.Count = 0 Then
                            Matrix0.Columns.Item("AttDate").ForeColor = Color.Black.B
                            Matrix0.Columns.Item("EmpNo").ForeColor = Color.Black.B
                        End If
                        If objFinalDT.Rows.Count > 0 Then
                            For DTRow As Integer = 0 To objFinalDT.Rows.Count - 1
                                If objFinalDT.Rows(DTRow)("AttDate").ToString = "" Then
                                    Errmsg += vbCrLf + "Attendance date cannot be empty" & " RowNo: " & DTRow + 1
                                End If
                                If objFinalDT.Rows(DTRow)("AttDate").ToString = "" Or objFinalDT.Rows(DTRow)("EmpNo").ToString = "" Or objFinalDT.Rows(DTRow)("attstatus").ToString = "" Or objFinalDT.Rows(DTRow)("Half").ToString = "" Or objFinalDT.Rows(DTRow)("Shift").ToString = "" Or objFinalDT.Rows(DTRow)("Timein").ToString = "" Or objFinalDT.Rows(DTRow)("Timeout").ToString = "" Then
                                    Errmsg += vbCrLf + "Please update all the column values" & " RowNo: " & DTRow + 1
                                End If
                                If objFinalDT.Rows(DTRow)("attstatus").ToString <> "LOP" And objFinalDT.Rows(DTRow)("attstatus").ToString <> "WO" And objFinalDT.Rows(DTRow)("attstatus").ToString <> "EL" And objFinalDT.Rows(DTRow)("attstatus").ToString <> "WE" And objFinalDT.Rows(DTRow)("attstatus").ToString <> "CO" Then
                                    'Getval = objaddon.objglobalmethods.getSingleValue("Select 1 as ""Status"" from ""@SMHR_OSFT"" where ""Name""='" & objFinalDT.Rows(DTRow)("Shift").ToString & "'")
                                    Getval = objaddon.objglobalmethods.getSingleValue("Select 1 as ""Status"" from ""@SMHR_OSFT"" where ""Code""='" & objFinalDT.Rows(DTRow)("Shift").ToString & "'")
                                    If Getval = "" Then
                                        Errmsg += vbCrLf + "Please update valid shift name" & " RowNo: " & DTRow + 1
                                        Matrix0.CommonSetting.SetCellFontColor(DTRow + 1, 10, Color.Blue.B)
                                    Else
                                        Matrix0.CommonSetting.SetCellFontColor(DTRow + 1, 10, Color.Black.B)
                                        'Matrix0.CommonSetting.SetRowFontColor(DTRow + 1, Color.Blue.B) 'Color.DarkRed.ToArgb
                                        'Matrix0.Columns.Item("Shift").TextStyle = FontStyle.Bold
                                    End If
                                End If
                                Getval = objaddon.objglobalmethods.getSingleValue("select 1 as ""Result"" from ""@SMPR_ODAS"" T0 join ""@SMPR_DAS1"" T1 on T0.""DocEntry""=T1.""DocEntry"" where T1.""U_IDNo""='" & objFinalDT.Rows(DTRow)("EmpNo").ToString & "' and T1.""U_AttDate""='" & objFinalDT.Rows(DTRow)("AttDate").ToString & "'")
                                If Getval = "1" Then
                                    Errmsg += vbCrLf + "Please remove the Already Posted Attendance Employee " & objFinalDT.Rows(DTRow)("EmpNo").ToString & " for the date: " & objFinalDT.Rows(DTRow)("AttDate").ToString & " RowNo: " & DTRow + 1
                                End If
                                m_oProgBar.Value = DTRow + 1
                            Next
                        Else
                            objaddon.objapplication.StatusBar.SetText("No Data Found to Validate...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                            BubbleEvent = False : Exit Sub
                        End If


                        'For i As Integer = 1 To Matrix0.VisualRowCount
                        '    If Matrix0.Columns.Item("AttDate").Cells.Item(i).Specific.String <> "" Then
                        '        'Dim st As String = Matrix0.Columns.Item("attstatus").Cells.Item(i).Specific.String
                        '        If Matrix0.Columns.Item("attstatus").Cells.Item(i).Specific.String <> "LOP" And Matrix0.Columns.Item("attstatus").Cells.Item(i).Specific.String <> "WO" And Matrix0.Columns.Item("attstatus").Cells.Item(i).Specific.String <> "EL" And Matrix0.Columns.Item("attstatus").Cells.Item(i).Specific.String <> "WE" And Matrix0.Columns.Item("attstatus").Cells.Item(i).Specific.String <> "CO" Then
                        '            'If Matrix0.Columns.Item("AttDate").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("EmpId").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("EmpNo").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("EmpName").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("Desig").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("Post").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("attstatus").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("Half").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("Shift").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("shifthrs").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("Timein").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("Timeout").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("HrsWork").Cells.Item(i).Specific.String = "" Then
                        '            If Matrix0.Columns.Item("AttDate").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("EmpId").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("EmpNo").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("attstatus").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("Half").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("Shift").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("Timein").Cells.Item(i).Specific.String = "" Or Matrix0.Columns.Item("Timeout").Cells.Item(i).Specific.String = "" Then
                        '                'objaddon.objapplication.SetStatusBarMessage("Please update all the column values" & " RowNo: " & i, SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                        '                'BubbleEvent = False : Exit Sub
                        '                Errmsg += vbCrLf + "Please update all the column values" & " RowNo: " & i
                        '            End If
                        '            'If Matrix0.Columns.Item("shifthrs").Cells.Item(i).Specific.String = "" Then
                        '            '    Matrix0.Columns.Item("shifthrs").Cells.Item(i).Specific.String = "0"
                        '            'End If

                        '            If Matrix0.Columns.Item("Shift").Cells.Item(i).Specific.String <> "" Then
                        '                Getval = objaddon.objglobalmethods.getSingleValue("Select 1 as ""Status"" from ""@SMHR_OSFT"" where ""Name""='" & Matrix0.Columns.Item("Shift").Cells.Item(i).Specific.String & "'")
                        '                If Getval = "" Then
                        '                    Errmsg += vbCrLf + "Please update valid shift name" & " RowNo: " & i
                        '                End If
                        '            End If
                        '            If Matrix0.Columns.Item("Timein").Cells.Item(i).Specific.String = "" Then
                        '                Matrix0.Columns.Item("Timein").Cells.Item(i).Specific.String = "0"
                        '            End If
                        '            If Matrix0.Columns.Item("Timeout").Cells.Item(i).Specific.String = "" Then
                        '                Matrix0.Columns.Item("Timeout").Cells.Item(i).Specific.String = "0"
                        '            End If
                        '            'If Left(Matrix0.Columns.Item("Timein").Cells.Item(i).Specific.String, 2) < 8 And Left(Matrix0.Columns.Item("Timein").Cells.Item(i).Specific.String, 2) <= 0 Then
                        '            '    ' objaddon.objapplication.SetStatusBarMessage("In Time not less than 8" & " RowNo: " & i, SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                        '            '    Errmsg += vbCrLf + "In Time not less than 8 or 0" & " RowNo: " & i
                        '            'End If
                        '            'If Left(Matrix0.Columns.Item("Timeout").Cells.Item(i).Specific.String, 2) > 22 And Left(Matrix0.Columns.Item("Timeout").Cells.Item(i).Specific.String, 2) <= 0 Then
                        '            '    '  objaddon.objapplication.SetStatusBarMessage("Out Time not to exceed 22" & " RowNo: " & i, SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                        '            '    Errmsg += vbCrLf + "Out Time not to exceed 22 or 0" & " RowNo: " & i
                        '            'End If

                        '        End If
                        '        Dim Result As String
                        '        Dim AttdDate As Date
                        '        Dim txtAttDate As SAPbouiCOM.EditText
                        '        txtAttDate = Matrix0.Columns.Item("AttDate").Cells.Item(i).Specific
                        '        If Matrix0.Columns.Item("AttDate").Cells.Item(i).Specific.String <> "" Then
                        '            AttdDate = Date.ParseExact(txtAttDate.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo) 'Matrix0.Columns.Item("AttDate").Cells.Item(i).Specific.String
                        '        Else
                        '            Errmsg += vbCrLf + "Attendance date cannot be empty" & " RowNo: " & i
                        '        End If
                        '        Result = objaddon.objglobalmethods.getSingleValue("select 1 as ""Result"" from ""@SMPR_ODAS"" T0 join ""@SMPR_DAS1"" T1 on T0.""DocEntry""=T1.""DocEntry"" where T1.""U_IDNo""='" & Matrix0.Columns.Item("EmpNo").Cells.Item(i).Specific.String & "' and T1.""U_AttDate""='" & AttdDate.ToString("yyyyMMdd") & "'")
                        '        If Result = "1" Then
                        '            '  objaddon.objapplication.SetStatusBarMessage("Already Posted Attendance to this Employee " & Matrix0.Columns.Item("EmpNo").Cells.Item(i).Specific.String & " - " & Matrix0.Columns.Item("AttDate").Cells.Item(i).Specific.ToString("yyyyMMdd") & "", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                        '            Errmsg += vbCrLf + "Please remove the Already Posted Attendance Employee " & Matrix0.Columns.Item("EmpNo").Cells.Item(i).Specific.String & " for the date: " & AttdDate.ToString("dd/MM/yy") & " RowNo: " & i
                        '        End If
                        '    End If
                        '    AttDate = Matrix0.Columns.Item("AttDate").Cells.Item(i).Specific.String
                        '    EmpId = Matrix0.Columns.Item("EmpNo").Cells.Item(i).Specific.String
                        '    For j As Integer = i + 1 To Matrix0.VisualRowCount
                        '        If EmpId = Matrix0.Columns.Item("EmpNo").Cells.Item(j).Specific.String And AttDate = Matrix0.Columns.Item("AttDate").Cells.Item(j).Specific.String Then
                        '            'If objaddon.objapplication.MessageBox("Duplicate Found... Do you want delete the duplicate records?", 2, "OK", "Cancel") <> 1 Then j += 1
                        '            Errmsg += vbCrLf + "Duplicate Attendance entered for the employee " & EmpId & " AttDate" & AttDate & " Please Remove" & " RowNo: " & j
                        '            Exit For
                        '            'Matrix0.DeleteRow(j)
                        '        End If
                        '    Next
                        '    m_oProgBar.Value = i
                        'Next
                        'm_oProgBar.Stop()
                        'm_oProgBar = Nothing
                        'GC.Collect()
                        'objform.Freeze(False)
                        If Errmsg <> "" Then
                            ' objaddon.objglobalmethods.WriteSMSLog(Errmsg)
                            objaddon.objglobalmethods.WriteErrorLog(Errmsg)
                            'If objaddon.objapplication.MessageBox("Please see the error log and correct the mentioned errors...", 1, "OK", "Cancel") = 2 Then Exit Sub
                            'objaddon.objapplication.SetStatusBarMessage("Please see the error log in SAP attachment folder and correct the mentioned errors...", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                            objaddon.objapplication.MessageBox("Please see the error log in SAP attachment folder and correct the mentioned errors...", , "OK")
                            'If MessageBox.Show("Please see the error log and correct the mentioned errors...", "Errors Found", MessageBoxButtons.OKCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, False) = DialogResult.Cancel Then Exit Sub
                            BubbleEvent = False : Exit Sub
                        End If
                        objaddon.objapplication.SetStatusBarMessage("Validation Completed...", SAPbouiCOM.BoMessageTime.bmt_Medium, False)

                    Catch ex As Exception
                        'objform.Freeze(False)
                        'm_oProgBar.Stop()
                        'm_oProgBar = Nothing
                        objaddon.objapplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)
                        BubbleEvent = False
                    Finally
                        objform.Freeze(False)
                    End Try
                End If
            Catch ex As Exception
                objaddon.objapplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)
            Finally
                m_oProgBar.Stop()
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_oProgBar)
                m_oProgBar = Nothing
                GC.Collect()
                objform.Freeze(False)
            End Try

        End Sub

        Private Sub Matrix0_ValidateBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Matrix0.ValidateBefore
            Try
                If pVal.ItemChanged = True Then
                    Matrix_DataTable_Update(pVal.Row, pVal.ColUID)
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Form_ResizeAfter(pVal As SAPbouiCOM.SBOItemEventArg)
            Try
                Matrix0.AutoResizeColumns()
            Catch ex As Exception

            End Try

        End Sub

        Private WithEvents EditText6 As SAPbouiCOM.EditText

    End Class
End Namespace
