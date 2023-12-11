Option Strict Off
Option Explicit On

Imports SAPbouiCOM.Framework
Imports System.IO
Imports System.Threading
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Data.OleDb

Namespace HRMS
    <FormAttribute("ODAS", "Transcation/frmDailyAttendance.b1f")>
    Friend Class frmDailyAttendance
        Inherits UserFormBase
        Dim FormCount As Integer = 0
        Private WithEvents objform As SAPbouiCOM.Form
        Private WithEvents pCFL As SAPbouiCOM.ISBOChooseFromListEventArg
        Dim objrs As SAPbobsCOM.Recordset
        Dim strsql As String
        Private WithEvents ocombo As SAPbouiCOM.ComboBox
        Private WithEvents cmbdesignation As SAPbouiCOM.ComboBox
        Private WithEvents cmbdepartment As SAPbouiCOM.ComboBox
        Private WithEvents cmbattendance As SAPbouiCOM.ComboBox
        Private WithEvents cmbhalfstatus As SAPbouiCOM.ComboBox
        Private WithEvents odbdsheader, odbdsDetails As SAPbouiCOM.DBDataSource

        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.Button0 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.Button1 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.StaticText0 = CType(Me.GetItem("lbldate").Specific, SAPbouiCOM.StaticText)
            Me.EditText0 = CType(Me.GetItem("txtadate").Specific, SAPbouiCOM.EditText)
            Me.StaticText1 = CType(Me.GetItem("Item_0").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox0 = CType(Me.GetItem("cmbloc").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText3 = CType(Me.GetItem("Item_6").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox2 = CType(Me.GetItem("cmbseries").Specific, SAPbouiCOM.ComboBox)
            Me.EditText3 = CType(Me.GetItem("txtdocno").Specific, SAPbouiCOM.EditText)
            Me.StaticText4 = CType(Me.GetItem("Item_9").Specific, SAPbouiCOM.StaticText)
            Me.EditText4 = CType(Me.GetItem("txtdocdt").Specific, SAPbouiCOM.EditText)
            Me.Matrix0 = CType(Me.GetItem("mtattn").Specific, SAPbouiCOM.Matrix)
            Me.StaticText6 = CType(Me.GetItem("Item_14").Specific, SAPbouiCOM.StaticText)
            Me.EditText5 = CType(Me.GetItem("txtremarks").Specific, SAPbouiCOM.EditText)
            Me.EditText6 = CType(Me.GetItem("txtdentry").Specific, SAPbouiCOM.EditText)
            Me.Button2 = CType(Me.GetItem("btnload").Specific, SAPbouiCOM.Button)
            Me.Button3 = CType(Me.GetItem("Item_1").Specific, SAPbouiCOM.Button)
            Me.EditText1 = CType(Me.GetItem("txtday").Specific, SAPbouiCOM.EditText)
            Me.Button4 = CType(Me.GetItem("Item_2").Specific, SAPbouiCOM.Button)
            Me.StaticText2 = CType(Me.GetItem("lblbranch").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox1 = CType(Me.GetItem("CmbBranch").Specific, SAPbouiCOM.ComboBox)
            Me.ComboBox3 = CType(Me.GetItem("CmbeGroup").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText5 = CType(Me.GetItem("lblegrp").Specific, SAPbouiCOM.StaticText)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()
            AddHandler DataLoadAfter, AddressOf Me.frmDailyAttendance_DataLoadAfter

        End Sub

        Private Sub OnCustomInitialize()
            Try

                objform = objaddon.objapplication.Forms.GetForm("ODAS", Me.FormCount)
                objform = objaddon.objapplication.Forms.ActiveForm

                odbdsheader = objform.DataSources.DBDataSources.Item(CType(0, Object))
                odbdsDetails = objform.DataSources.DBDataSources.Item(CType(1, Object))

                objform.Freeze(True)

                'objform.Left = objform.Left ' (objaddon.objapplication.Desktop.Width - objform.Width) / 2
                'objform.Top = objform.Top ' (objaddon.objapplication.Desktop.Height - objform.Height) / 2

                Load_Combobox(objform) 'Combo box Load
                LoadComboDetails()
                objform.Items.Item("txtdocdt").Specific.string = Now.Date.ToString("yyyyMMdd")
                odbdsheader.SetValue("DocEntry", 0, objaddon.objglobalmethods.GetNextDocentry_Value("@SMPR_ODAS"))

                Matrix0.Columns.Item("empid").Visible = False
                Matrix0.Columns.Item("scode").Visible = False
                Matrix0.Columns.Item("halfst").Visible = False
                FieldDisable()
                manage_fields()
                'Matrix0.CommonSetting.EnableArrowKey = True
                EditText3.Value = objaddon.objglobalmethods.GetDocnum_BaseonSeries("ODAS")

                objform.EnableMenu("1292", False) 'Add row
                objform.EnableMenu("1293", False) 'Delete Row
                objform.EnableMenu("1283", False) 'Remove 
                objform.EnableMenu("1284", False) 'Cancel
                objform.EnableMenu("1286", False) 'Close
                objform.Settings.Enabled = True
                If objaddon.objcompany.UserName.ToString.ToUpper <> "MANAGER" Then objform.EnableMenu("6913", False) 'User Defined Field
                objform.ActiveItem = "txtadate"
                objaddon.objapplication.Menus.Item("1300").Activate() 'Fit colum width
                objform.Settings.Enabled = True




                'Dim pdate As Date = #04/20/2022#
                'Update_DailyAttendance(pdate, "9")
                'Creating_Attendance(pdate, "10")
                objform.Freeze(False)
            Catch ex As Exception
                objform.Freeze(False)
                'objaddon.objapplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, True)
            End Try
        End Sub

        Private Sub FieldDisable()
            Try
                If GetPayrollEnabledIndia = True Then
                    ComboBox3.Item.Visible = False
                    StaticText5.Item.Visible = False
                    ComboBox1.Item.Visible = True
                    StaticText2.Item.Visible = True
                    Matrix0.Columns.Item("prjcode").Visible = False
                    Matrix0.Columns.Item("prjname").Visible = False
                    Matrix0.Columns.Item("chkholi").Visible = False
                    Matrix0.Columns.Item("chkwend").Visible = False
                    Matrix0.Columns.Item("otappl").Visible = False
                    Matrix0.Columns.Item("OTH").Visible = False
                Else
                    ComboBox1.Item.Visible = False
                    StaticText2.Item.Visible = False
                    ComboBox3.Item.Visible = True
                    StaticText5.Item.Visible = True
                    StaticText5.Item.Left = StaticText1.Item.Left
                    StaticText5.Item.Top = StaticText1.Item.Top + StaticText1.Item.Height + 4
                    ComboBox3.Item.Left = ComboBox0.Item.Left
                    ComboBox3.Item.Top = ComboBox0.Item.Top + ComboBox0.Item.Height + 4
                    Matrix0.Columns.Item("AttDate").Visible = False
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub LoadComboDetails()
            Try
                ComboBox1.ValidValues.Add("-1", "All")
                Dim objrs As SAPbobsCOM.Recordset
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery("CALL ""MIPL_HRMS_EMPMASTER_COMBO_FILLING"" ('OHEM')")
                If objrs.RecordCount = 0 Then Exit Sub
                For i As Integer = 0 To objrs.RecordCount - 1
                    Try
                        Select Case objrs.Fields.Item("Type").Value.ToString.ToUpper
                            Case "BRANCH" : ComboBox1.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                        End Select
                        objrs.MoveNext()
                    Catch ex As Exception
                        objrs.MoveNext()
                    End Try
                Next
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Load_Combobox(ByVal oform As SAPbouiCOM.Form)
            Try
                ComboBox0.ValidValues.Add("-1", "All")
                Dim cmbdesignation As SAPbouiCOM.Column = Matrix0.Columns.Item("desig")
                Dim cmbdepartment As SAPbouiCOM.Column = Matrix0.Columns.Item("dept")
                Dim Cmbleave As SAPbouiCOM.Column = Matrix0.Columns.Item("attstatus")
                Dim objrs As SAPbobsCOM.Recordset
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery("CALL ""MIPL_HRMS_EMPMASTER_COMBO_FILLING"" ('ODAS')")
                If objrs.RecordCount = 0 Then Exit Sub
                For i As Integer = 0 To objrs.RecordCount - 1
                    Try
                        Select Case objrs.Fields.Item("Type").Value.ToString.ToUpper
                            Case "EMPTYPE" : ComboBox3.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "LOCATION" : ComboBox0.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "POSITION" : cmbdesignation.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "DEPARTMENT" : cmbdepartment.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "LEAVE" : Cmbleave.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                                'Case "EMPTYPE" : ComboBox1.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                        End Select
                        objrs.MoveNext()
                    Catch ex As Exception
                        objrs.MoveNext()
                    End Try
                Next
            Catch ex As Exception

            End Try

        End Sub

        Private Sub manage_fields()
            Try
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtadate", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cmbloc", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cmbseries", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtdocno", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtdocdt", True, True, False)

                objform.Items.Item("btnload").SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Visible, SAPbouiCOM.BoAutoFormMode.afm_Add, SAPbouiCOM.BoModeVisualBehavior.mvb_True)
                objform.Items.Item("btnload").SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, SAPbouiCOM.BoAutoFormMode.afm_Find, SAPbouiCOM.BoModeVisualBehavior.mvb_False)
                objform.Items.Item("btnload").SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, SAPbouiCOM.BoAutoFormMode.afm_Ok, SAPbouiCOM.BoModeVisualBehavior.mvb_False)
            Catch ex As Exception

            End Try
        End Sub

        Private Sub UploadExcel(ByVal StrPath As String)
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            Dim ExcelWorkbook As Microsoft.Office.Interop.Excel.Workbook = Nothing
            Dim ExcelWorkSheet As Microsoft.Office.Interop.Excel.Worksheet = Nothing
            Dim excelRng As Microsoft.Office.Interop.Excel.Range
            Try
                ExcelWorkbook = ExcelApp.Workbooks.Open(StrPath)
                ExcelWorkSheet = ExcelWorkbook.ActiveSheet
                'excelRng = ExcelWorkSheet.Range("A1")
                excelRng = ExcelWorkSheet.UsedRange
                Dim RowIndex As Integer = 2

                'While excelRng.Range("A" & RowIndex & "").Text <> ""
                '    RowIndex = RowIndex + 1
                'End While

                'Dim IntYear, MonthDays As Integer
                Dim EmpID, StrEmpNo, StrEmpName, Desig, Dept, AttStat, HalfDay, HalfStat, ShiftName, Shifthrs, TimeIn, TimeOut, HrsWrk As String
                'EmpID = excelRng.Range("AttendanceDate", 1).Text
                For i = 2 To 2
                    Dim oGeneralService As SAPbobsCOM.GeneralService
                    Dim oGeneralData As SAPbobsCOM.GeneralData
                    Dim oGeneralDataCollection As SAPbobsCOM.GeneralDataCollection
                    Dim oChild As SAPbobsCOM.GeneralData
                    oGeneralService = objaddon.objcompany.GetCompanyService.GetGeneralService("ODAS")
                    oGeneralData = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)
                    oGeneralDataCollection = oGeneralData.Child("SMPR_DAS1")

                    If excelRng.Range("A" & i & "").Text <> "" Then

                        ' Dim Atdate = excelRng.Range("A" & i & "").Text
                        'Dim Atdate1 = Convert.ToDateTime(excelRng.Range("A" & i & "").Text)
                        'Dim dif = Atdate.Subtract(DateTime.Now())


                        Dim AttdDate As Date = Date.ParseExact(excelRng.Range("A" & i & "").Text, "dd/MM/yy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                        If Format(AttdDate, "dd/MM/yy") > Format(Today.Date.ToString, "dd/MM/yy") Then
                            objaddon.objapplication.StatusBar.SetText("Date should not be exceed... ", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                        Else
                            MsgBox("not exceed")
                        End If
                        If excelRng.Range("A" & 1 & "").Text = "AttendanceDate" And excelRng.Range("B" & 1 & "").Text = "EmpID" And excelRng.Range("C" & 1 & "").Text = "EmpNo" And excelRng.Range("D" & 1 & "").Text = "EmpName" And excelRng.Range("E" & 1 & "").Text = "Designation" And excelRng.Range("F" & 1 & "").Text = "Position" And excelRng.Range("G" & 1 & "").Text = "Attendance" And excelRng.Range("H" & 1 & "").Text = "HalfDay" And excelRng.Range("I" & 1 & "").Text = "HalfStatus" And excelRng.Range("J" & 1 & "").Text = "ShiftName" And excelRng.Range("K" & 1 & "").Text = "ShiftHrs" And excelRng.Range("L" & 1 & "").Text = "TimeIn" And excelRng.Range("M" & 1 & "").Text = "TimOut" And excelRng.Range("N" & 1 & "").Text = "HrsWorked" Then
                            For j = 2 To excelRng.Rows.Count

                                If AttdDate.ToString("dd/MM/yy") <> "" Then
                                    EmpID = excelRng.Range("B" & j & "").Text
                                    StrEmpNo = excelRng.Range("C" & j & "").Text
                                    StrEmpName = excelRng.Range("D" & j & "").Text
                                    Desig = excelRng.Range("E" & j & "").Text
                                    Dept = excelRng.Range("F" & j & "").Text
                                    AttStat = excelRng.Range("G" & j & "").Text
                                    HalfDay = excelRng.Range("H" & j & "").Text
                                    HalfStat = excelRng.Range("I" & j & "").Text
                                    ShiftName = excelRng.Range("J" & j & "").Text
                                    Shifthrs = excelRng.Range("K" & j & "").Text
                                    TimeIn = excelRng.Range("L" & j & "").Text
                                    TimeOut = excelRng.Range("M" & j & "").Text
                                    HrsWrk = excelRng.Range("N" & j & "").Text
                                    Dim AttDate As Date = Date.ParseExact(excelRng.Range("A" & j & "").Text, "dd/MM/yy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                                    Dim Result As String = objaddon.objglobalmethods.getSingleValue("select 1 as ""Result"" from ""@SMPR_ODAS"" T0 join ""@SMPR_DAS1"" T1 on T0.""DocEntry""=T1.""DocEntry"" where T1.""U_IDNo""='" & StrEmpNo & "' and T1.""U_AttDate""='" & AttDate.ToString("yyyyMMdd") & "'")
                                    If Result = "1" Then
                                        objaddon.objapplication.SetStatusBarMessage("Already Posted Attendance to this Employee " & StrEmpNo & " - " & AttDate.ToString("dd/MM/yy") & "", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                                    Else
                                        oGeneralData.SetProperty("U_AttdDate", AttdDate.ToString("yy/MM/dd"))
                                        oGeneralData.SetProperty("U_AttdDay", WeekdayName(Weekday(AttdDate)).ToString)
                                        oChild = oGeneralDataCollection.Add
                                        oChild.SetProperty("U_IDNo", StrEmpNo.ToString)
                                        oChild.SetProperty("U_empID", EmpID.ToString)
                                        oChild.SetProperty("U_empName", StrEmpName.ToString)
                                        oChild.SetProperty("U_AttDate", AttDate.ToString("yy/MM/dd"))
                                        oChild.SetProperty("U_Designat", Desig.ToString)
                                        oChild.SetProperty("U_Dept", Dept.ToString)
                                        oChild.SetProperty("U_AttStatus", AttStat.ToString)
                                        oChild.SetProperty("U_Halfday", HalfDay.ToString)
                                        oChild.SetProperty("U_HalfStatus", HalfStat.ToString)
                                        oChild.SetProperty("U_ShiftName", ShiftName.ToString)
                                        oChild.SetProperty("U_shifthrs", Shifthrs.ToString)
                                        oChild.SetProperty("U_TimeIn", TimeIn.ToString)
                                        oChild.SetProperty("U_TimeOut", TimeOut.ToString)
                                        oChild.SetProperty("U_HrsWrk", HrsWrk.ToString)
                                    End If
                                Else
                                End If

                            Next
                            oGeneralService.Add(oGeneralData)
                            objaddon.objapplication.StatusBar.SetText("Attendance Posted Successfully...", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                        Else
                            objaddon.objapplication.StatusBar.SetText("Expected ColumnName Not found...Please check the format", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                        End If
                    Else
                    End If
                Next
                ExcelApp.ActiveWorkbook.Close()
                ' ExcelWorkbook.Close()
            Catch ex As Exception
                objaddon.objapplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short)
            End Try
        End Sub
        Dim BankFileName = ""
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

            Dim OpenFile As New OpenFileDialog
            Try
                Dim initialpath As String = objaddon.objglobalmethods.getSingleValue("select ""ExcelPath"" from oadm")
                OpenFile.InitialDirectory = initialpath
                OpenFile.Multiselect = False
                'OpenFile.ShowDialog()
                OpenFile.Filter = "All files(*.)|*.*" '   "|*.*"
                Dim filterindex As Integer = 0
                Try
                    filterindex = 0
                Catch ex As Exception
                End Try
                OpenFile.FilterIndex = filterindex
                OpenFile.RestoreDirectory = False
                MyProcs = Process.GetProcessesByName("SAP Business One")
                OpenFile.CustomPlaces.Add(initialpath)
                Try

                Catch
                End Try
                'If MyProcs.Length = 1 Then
                If MyProcs.Length >= 1 Then
                    For i As Integer = 0 To MyProcs.Length - 1
                        'Dim comname As String() = MyProcs(i).MainWindowTitle.ToString.Split("-")

                        'Open dialog only for the company where the button is clicked
                        Dim com As String = objaddon.objcompany.CompanyName.ToString.Trim.ToUpper
                        'If comname(1).ToString.Trim.ToUpper = com Then
                        Dim MyWindow As New WindowWrapper(MyProcs(i).MainWindowHandle)

                        'Dim ret As Windows.Forms.DialogResult = OpenFile.ShowDialog(MyWindow)
                        If OpenFile.ShowDialog(MyWindow) = Windows.Forms.DialogResult.OK Then
                            BankFileName = OpenFile.FileName
                            'OpenFile.Dispose()
                        Else
                            System.Windows.Forms.Application.ExitThread()
                        End If
                        ' End If
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

#Region "Field Details"

        Private WithEvents Button0 As SAPbouiCOM.Button
        Private WithEvents Button1 As SAPbouiCOM.Button
        Private WithEvents StaticText0 As SAPbouiCOM.StaticText
        Private WithEvents EditText0 As SAPbouiCOM.EditText
        Private WithEvents StaticText1 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox0 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText3 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox2 As SAPbouiCOM.ComboBox
        Private WithEvents EditText3 As SAPbouiCOM.EditText
        Private WithEvents StaticText4 As SAPbouiCOM.StaticText
        Private WithEvents EditText4 As SAPbouiCOM.EditText
        Private WithEvents Matrix0 As SAPbouiCOM.Matrix
        Private WithEvents StaticText6 As SAPbouiCOM.StaticText
        Private WithEvents EditText5 As SAPbouiCOM.EditText
        Private WithEvents EditText6 As SAPbouiCOM.EditText
        Private WithEvents Button2 As SAPbouiCOM.Button
        Private WithEvents Button3 As SAPbouiCOM.Button
        Private WithEvents EditText1 As SAPbouiCOM.EditText
        Private WithEvents Button4 As SAPbouiCOM.Button
#End Region

        Private Sub ComboBox2_ComboSelectAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox2.ComboSelectAfter
            If ComboBox2.Selected Is Nothing Then Exit Sub
            EditText3.Value = objaddon.objglobalmethods.GetDocnum_BaseonSeries("ODAS")

        End Sub

        Private Sub Matrix0_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.ChooseFromListAfter
            Try
                If pVal.ColUID = "trzid" And pVal.ActionSuccess = True Then
                    Try
                        Dim rowno As Integer
                        pCFL = pVal
                        rowno = pVal.Row
                        If pCFL.SelectedObjects Is Nothing Then Exit Sub
                        If pCFL.SelectedObjects.Rows.Count > 1 Then Matrix0.LoadFromDataSource() : rowno = Matrix0.VisualRowCount
                        Dim strEmpid As String = "#"

                        For i = 0 To pCFL.SelectedObjects.Rows.Count - 1
                            Try
                                strEmpid = strEmpid + pCFL.SelectedObjects.Columns.Item("U_ExtEmpNo").Cells.Item(i).Value + "#"
                            Catch ex As Exception
                            End Try
                        Next
                        If strEmpid <> "#" Then Load_employees(strEmpid, Matrix0.Columns.Item("#").Cells.Item(rowno).Specific.string - 1)
                        'Button4.Item.Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                        Addrow()
                    Catch ex As Exception
                    End Try
                ElseIf pVal.ColUID = "prjcode" And pVal.ActionSuccess = True Then
                    Try
                        pCFL = pVal
                        If pCFL.SelectedObjects Is Nothing Then Exit Sub
                        Try
                            Matrix0.Columns.Item("prjcode").Cells.Item(pVal.Row).Specific.String = pCFL.SelectedObjects.Columns.Item("PrjCode").Cells.Item(0).Value
                        Catch ex As Exception
                        End Try
                        Try
                            Matrix0.Columns.Item("prjname").Cells.Item(pVal.Row).Specific.String = pCFL.SelectedObjects.Columns.Item("PrjName").Cells.Item(0).Value
                        Catch ex As Exception
                        End Try
                        Try
                            odbdsDetails.SetValue("U_PrjCode", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1, Matrix0.Columns.Item("prjcode").Cells.Item(pVal.Row).Specific.string)
                            odbdsDetails.SetValue("U_PrjName", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1, Matrix0.Columns.Item("prjname").Cells.Item(pVal.Row).Specific.string)
                        Catch ex As Exception

                        End Try
                    Catch ex As Exception

                    End Try
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Matrix0_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Matrix0.ChooseFromListBefore
            If pVal.ColUID = "trzid" Then
                'If Matrix0.Columns.Item("trzid").Cells.Item(pVal.Row).Specific.string <> "" Then Matrix0.Columns.Item("trzid").Cells.Item(pVal.Row).Specific.string = ""
                Try
                    Dim oCFL As SAPbouiCOM.ChooseFromList = objform.ChooseFromLists.Item("empde")
                    Dim oConds As SAPbouiCOM.Conditions
                    Dim oCond As SAPbouiCOM.Condition
                    Dim oEmptyConds As New SAPbouiCOM.Conditions
                    oCFL.SetConditions(oEmptyConds)
                    oConds = oCFL.GetConditions()

                    If Not ComboBox0.Selected Is Nothing Then
                        If ComboBox0.Selected.Value <> "-1" Then
                            oCond = oConds.Add()
                            oCond.Alias = "U_location"
                            oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                            oCond.CondVal = ComboBox0.Selected.Value.ToString
                        End If
                    End If
                    If GetPayrollEnabledIndia = True Then
                        If Not ComboBox1.Selected Is Nothing Then
                            If ComboBox1.Selected.Value <> "-1" Then
                                If oConds.Count > 0 Then oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND
                                oCond = oConds.Add()
                                oCond.Alias = "U_branch"
                                oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                                oCond.CondVal = ComboBox1.Selected.Value.ToString
                            End If
                        End If

                    Else
                        If Not ComboBox3.Selected Is Nothing Then
                            If ComboBox3.Selected.Value <> "-1" Then
                                If oConds.Count > 0 Then oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND
                                oCond = oConds.Add()
                                oCond.Alias = "U_gropCode"
                                oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                                oCond.CondVal = ComboBox3.Selected.Value.ToString
                            End If
                        End If

                    End If

                    For i As Integer = 1 To Matrix0.VisualRowCount
                        If Matrix0.Columns.Item("trzid").Cells.Item(i).Specific.string <> "" And i <> pVal.Row Then
                            If oConds.Count > 0 Then oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND
                            oCond = oConds.Add()
                            oCond.Alias = "U_ExtEmpNo"
                            oCond.Operation = SAPbouiCOM.BoConditionOperation.co_NOT_EQUAL
                            oCond.CondVal = Matrix0.Columns.Item("trzid").Cells.Item(i).Specific.string
                        End If
                    Next

                    If oConds.Count > 0 Then oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND
                    oCond = oConds.Add()
                    oCond.Alias = "U_status"
                    oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                    oCond.CondVal = "1"
                    oCFL.SetConditions(oConds)

                    objform.ActiveItem = "txtremarks"
                Catch ex As Exception

                End Try
            ElseIf pVal.ColUID = "prjcode" Then
                Try
                    Dim oCFL As SAPbouiCOM.ChooseFromList = objform.ChooseFromLists.Item("project")
                    Dim oConds As SAPbouiCOM.Conditions
                    Dim oCond As SAPbouiCOM.Condition
                    Dim oEmptyConds As New SAPbouiCOM.Conditions
                    oCFL.SetConditions(oEmptyConds)
                    oConds = oCFL.GetConditions()

                    oCond = oConds.Add()
                    oCond.Alias = "Active"
                    oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                    oCond.CondVal = "Y"
                    oCFL.SetConditions(oConds)
                Catch ex As Exception

                End Try
            End If
        End Sub

        Private Sub Matrix0_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.ClickAfter
            'If pVal.ColUID = "#" Then Exit Sub
            'If pVal.Row <= 0 Then Exit Sub
            'Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Click(SAPbouiCOM.BoCellClickType.ct_Regular)
        End Sub

        'Private Sub Matrix0_ComboSelectAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.ComboSelectAfter
        'Try
        '    If pVal.ColUID = "attstatus" Then
        '        Dim ocombo As SAPbouiCOM.ComboBox
        '        ocombo = Matrix0.Columns.Item("attstatus").Cells.Item(pVal.Row).Specific
        '        If ocombo.Selected.Value.ToString.ToUpper = "PS" Then
        '            Matrix0.CommonSetting.SetCellEditable(pVal.Row, 13, True)
        '            Matrix0.CommonSetting.SetCellEditable(pVal.Row, 14, True)
        '        Else
        '            Matrix0.CommonSetting.SetCellEditable(pVal.Row, 13, False)
        '            Matrix0.CommonSetting.SetCellEditable(pVal.Row, 14, False)
        '        End If
        '    End If
        'Catch ex As Exception

        'End Try
        'End Sub

        Private Sub Matrix0_KeyDownBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Matrix0.KeyDownBefore
            Try
                If (pVal.ColUID = "Timeout" Or pVal.ColUID = "Timein") Then
                    If pVal.CharPressed = 38 Or pVal.CharPressed = 40 Or pVal.CharPressed = "9" Then
                    Else
                        Dim ocombo As SAPbouiCOM.ComboBox
                        ocombo = Matrix0.Columns.Item("attstatus").Cells.Item(pVal.Row).Specific
                        If ocombo.Selected.Value.ToString.ToUpper <> "PS" Then BubbleEvent = False
                    End If
                End If
            Catch ex As Exception

            End Try
            'If pVal.ColUID = "trzid" Then
            '    If pVal.CharPressed = "9" Or pVal.CharPressed = "36" Or pVal.CharPressed = "38" Then
            '    Else
            '        BubbleEvent = False
            '    End If
            'End If

        End Sub

        Private Sub Matrix0_LinkPressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.LinkPressedAfter
            If pVal.ColUID = "trzid" Then
                If Matrix0.Columns.Item("empid").Cells.Item(pVal.Row).Specific.string = "" Then Exit Sub
                Link_Value = Matrix0.Columns.Item("empid").Cells.Item(pVal.Row).Specific.string : Link_objtype = "OHEM"
                Dim activeform As New frmEmployeeMaster
                activeform.Show()
            ElseIf pVal.ColUID = "sname" Then
                If Matrix0.Columns.Item("scode").Cells.Item(pVal.Row).Specific.string = "" Then Exit Sub
                Link_Value = Matrix0.Columns.Item("scode").Cells.Item(pVal.Row).Specific.string : Link_objtype = "OSFT"
                Dim activeform As New frmShiftMaster
                activeform.Show()
            End If
        End Sub

        Private Sub Load_employees(ByVal strempid As String, ByVal rowno As Integer, Optional ByVal Clearmatrix As Boolean = False)
            Try
                Dim i As Integer = 0
                objaddon.objapplication.SetStatusBarMessage("Loading Employee Details.Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Long, False)
                objform.Freeze(True)
                If Clearmatrix = True Then
                    odbdsDetails.Clear()
                    Matrix0.LoadFromDataSource()
                    odbdsDetails.InsertRecord(odbdsDetails.Size)
                End If
                'objaddon.objapplication.SetStatusBarMessage("Retriving Employee Details.Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Long, False)
                Dim strsql As String = " CALL ""MIPL_SP_ODAS_FillEmployee"" ('" & strempid & "','" & EditText0.Value & "'"
                If Not ComboBox0.Selected Is Nothing Then
                    If ComboBox0.Selected.Value = "-1" Then strsql += " ,''" Else strsql += " ,'" & ComboBox0.Selected.Value & "'"
                Else
                    strsql += " ,''"
                End If
                If Not ComboBox1.Selected Is Nothing Then
                    If ComboBox1.Selected.Value = "-1" Then strsql += " ,'')" Else strsql += " ,'" & ComboBox1.Selected.Value & "')"
                Else
                    strsql += " ,'')"
                End If
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strsql)
                ' objform.ActiveItem = "txtremarks"
                'objaddon.objapplication.SetStatusBarMessage("Filling Employee Details.Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Long, False)
                For i = 1 To objrs.RecordCount
                    odbdsDetails.SetValue("LineId", rowno, rowno + 1)
                    odbdsDetails.SetValue("U_IDNo", rowno, objrs.Fields.Item("U_ExtEmpNo").Value.ToString)
                    odbdsDetails.SetValue("U_AttDate", rowno, EditText0.Value)
                    odbdsDetails.SetValue("U_empID", rowno, objrs.Fields.Item("U_empID").Value)
                    odbdsDetails.SetValue("U_empName", rowno, objrs.Fields.Item("Name").Value)
                    odbdsDetails.SetValue("U_Dept", rowno, objrs.Fields.Item("Dept").Value)
                    odbdsDetails.SetValue("U_Designat", rowno, objrs.Fields.Item("Desig").Value)
                    odbdsDetails.SetValue("U_Holiday", rowno, objrs.Fields.Item("PH").Value)
                    odbdsDetails.SetValue("U_Friday", rowno, objrs.Fields.Item("Weekoff").Value)
                    odbdsDetails.SetValue("U_AttStatus", rowno, objrs.Fields.Item("Attn").Value)
                    odbdsDetails.SetValue("U_Halfday", rowno, objrs.Fields.Item("halfday").Value)
                    odbdsDetails.SetValue("U_HalfStatus", rowno, "-1")
                    odbdsDetails.SetValue("U_ShiftCode", rowno, objrs.Fields.Item("scode").Value)
                    odbdsDetails.SetValue("U_ShiftName", rowno, objrs.Fields.Item("sname").Value)
                    odbdsDetails.SetValue("U_shifthrs", rowno, objrs.Fields.Item("shrs").Value)
                    odbdsDetails.SetValue("U_TimeIn", rowno, objrs.Fields.Item("sfrom").Value)
                    odbdsDetails.SetValue("U_TimeOut", rowno, objrs.Fields.Item("sto").Value)
                    odbdsDetails.SetValue("U_HrsWrk", rowno, objrs.Fields.Item("shrs").Value)
                    odbdsDetails.SetValue("U_otappl", rowno, objrs.Fields.Item("otappl").Value)
                    objrs.MoveNext()
                    rowno = rowno + 1
                    If i <> objrs.RecordCount And odbdsDetails.Size = rowno Then odbdsDetails.InsertRecord(odbdsDetails.Size)

                Next
                'objaddon.objapplication.SetStatusBarMessage("Binding Employee Details.Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Long, False)
                'odbdsDetails.SetValue("LineId", rowno, rowno + 1)
                Matrix0.LoadFromDataSource()
                Matrix0.AutoResizeColumns()
                ComboBox2.Select(0, SAPbouiCOM.BoSearchKey.psk_Index)

                'objaddon.objapplication.SetStatusBarMessage("Finishing Loading Employee Details.Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Long, False)
                If Matrix0.VisualRowCount > 0 Then Matrix0.Columns.Item("trzid").Cells.Item(Matrix0.VisualRowCount).Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                objform.Freeze(False)
                objaddon.objapplication.Menus.Item("1300").Activate()
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE
                objaddon.objapplication.SetStatusBarMessage("Employee Details Loaded Successfully.", SAPbouiCOM.BoMessageTime.bmt_Short, False)
            Catch ex As Exception
                objaddon.objapplication.SetStatusBarMessage("Error While Loading Employee Details in Daily Attendance", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                objform.Freeze(False)
            End Try
        End Sub

        Private Sub Button2_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button2.ClickAfter
            If objaddon.objapplication.MessageBox("Loading Employee Details will clear the Exsisting data.Press Yes to Continue", 1, "Yes", "No") = 2 Then Exit Sub
            Load_employees("-1", 0, True)
        End Sub

        Private Sub Button2_ClickBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button2.ClickBefore
            If EditText0.Value = "" Then
                objaddon.objapplication.SetStatusBarMessage("Daily Attendance Date is Missing", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                BubbleEvent = False : Exit Sub
            End If
        End Sub

        Private Sub Matrix0_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.LostFocusAfter
            Try
                Dim chk As SAPbouiCOM.CheckBox
                Select Case pVal.ColUID
                    Case "trzid"
                        odbdsDetails.SetValue("U_IDNo", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1, Matrix0.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Specific.string)
                        'If Matrix0.Columns.Item("trzid").Cells.Item(Matrix0.VisualRowCount).Specific.string <> "" Then
                        '    objform.Freeze(True)
                        '    odbdsDetails.InsertRecord(odbdsDetails.Size)
                        '    odbdsDetails.SetValue("LineId", Matrix0.VisualRowCount, Matrix0.VisualRowCount + 1)
                        '    Matrix0.LoadFromDataSource()
                        '    objform.Freeze(False)
                        'End If
                        'Case "prjcode"
                        '    odbdsDetails.SetValue("U_PrjCode", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1, Matrix0.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Specific.string)
                        'Case "prjname"
                        '    odbdsDetails.SetValue("U_PrjName", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1, Matrix0.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Specific.string)
                    Case "chkholi"
                        chk = Matrix0.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Specific : odbdsDetails.SetValue("U_Holiday", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1, IIf(chk.Checked, "Y", "N"))
                    Case "chkwend"
                        chk = Matrix0.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Specific : odbdsDetails.SetValue("U_Friday", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1, IIf(chk.Checked, "Y", "N"))
                    Case "attstatus"
                        cmbattendance = Matrix0.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Specific
                        odbdsDetails.SetValue("U_AttStatus", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1, cmbattendance.Selected.Value)
                    Case "hday"
                        chk = Matrix0.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Specific : odbdsDetails.SetValue("U_Halfday", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1, IIf(chk.Checked, "Y", "N"))
                    Case "Timein"
                        odbdsDetails.SetValue("U_TimeIn", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1, Matrix0.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Specific.value)

                    Case "Timeout"
                        odbdsDetails.SetValue("U_TimeOut", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1, Matrix0.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Specific.value)

                    Case "whour"

                        odbdsDetails.SetValue("U_HrsWrk", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1, Matrix0.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Specific.value)
                    Case "OTH"
                        odbdsDetails.SetValue("U_OTHrs", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1, Matrix0.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Specific.value)
                    Case "halfst"
                        cmbhalfstatus = Matrix0.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Specific

                        odbdsDetails.SetValue("U_HalfStatus", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1, cmbhalfstatus.Selected.Value)
                End Select
            Catch ex As Exception
                objform.Freeze(False)
            End Try
        End Sub

        'Private Sub Matrix0_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.PressedAfter
        '    Try
        '        If pVal.ColUID = "hday" Then
        '            Dim chk As SAPbouiCOM.CheckBox
        '            chk = Matrix0.Columns.Item("hday").Cells.Item(pVal.Row).Specific
        '            Dim ocombo As SAPbouiCOM.ComboBox
        '            ocombo = Matrix0.Columns.Item("halfst").Cells.Item(pVal.Row).Specific
        '            If chk.Checked = False Then
        '                ocombo.Select("-1", SAPbouiCOM.BoSearchKey.psk_ByValue) : Matrix0.CommonSetting.SetCellEditable(pVal.Row, 10, False)
        '            Else : Matrix0.CommonSetting.SetCellEditable(pVal.Row, 10, True)
        '            End If

        '        End If
        '    Catch ex As Exception

        '    End Try
        'End Sub

        Private Sub Matrix0_ValidateAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.ValidateAfter
            Try
                If (pVal.ColUID = "Timein" Or pVal.ColUID = "Timeout") Then
                    Dim ocombo As SAPbouiCOM.ComboBox
                    ocombo = Matrix0.Columns.Item("attstatus").Cells.Item(pVal.Row).Specific
                    If ocombo.Selected.Value.ToString.ToUpper <> "PS" Then Exit Sub
                    Dim chkotapp As SAPbouiCOM.CheckBox
                    Dim chkholiday, chkweekend As SAPbouiCOM.CheckBox
                    chkotapp = Matrix0.Columns.Item("otappl").Cells.Item(pVal.Row).Specific
                    If Matrix0.Columns.Item("Timein").Cells.Item(pVal.Row).Specific.string = "" Or Matrix0.Columns.Item("Timeout").Cells.Item(pVal.Row).Specific.string = "" Then Exit Sub
                    If objaddon.objglobalmethods.GetDuration_BetWeenTime(Matrix0.Columns.Item("Timein").Cells.Item(pVal.Row).Specific.string, Matrix0.Columns.Item("Timeout").Cells.Item(pVal.Row).Specific.string) = Matrix0.Columns.Item("whour").Cells.Item(pVal.Row).Specific.string Then Exit Sub
                    objform.Freeze(True)
                    odbdsDetails.SetValue("U_HrsWrk", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1, objaddon.objglobalmethods.GetDuration_BetWeenTime(Matrix0.Columns.Item("Timein").Cells.Item(pVal.Row).Specific.string, Matrix0.Columns.Item("Timeout").Cells.Item(pVal.Row).Specific.string))
                    'Dim val = objaddon.objglobalmethods.GetDuration_BetWeenTime(Matrix0.Columns.Item("Timein").Cells.Item(pVal.Row).Specific.string, Matrix0.Columns.Item("Timeout").Cells.Item(pVal.Row).Specific.string)
                    Matrix0.Columns.Item("whour").Cells.Item(pVal.Row).Specific.value = objaddon.objglobalmethods.GetDuration_BetWeenTime(Matrix0.Columns.Item("Timein").Cells.Item(pVal.Row).Specific.string, Matrix0.Columns.Item("Timeout").Cells.Item(pVal.Row).Specific.string)

                    If chkotapp.Checked = True Then
                        chkholiday = Matrix0.Columns.Item("chkholi").Cells.Item(pVal.Row).Specific
                        chkweekend = Matrix0.Columns.Item("chkwend").Cells.Item(pVal.Row).Specific
                        If chkholiday.Checked = True Or chkweekend.Checked = True Then
                            odbdsDetails.SetValue("U_OTHrs", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1, odbdsDetails.GetValue("U_HrsWrk", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1))
                            Matrix0.Columns.Item("OTH").Cells.Item(pVal.Row).Specific.string = odbdsDetails.GetValue("U_HrsWrk", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1)
                        Else
                            Dim HrsWorked = String.Join(":", Left(Matrix0.Columns.Item("whour").Cells.Item(pVal.Row).Specific.value, 2), Right(Matrix0.Columns.Item("whour").Cells.Item(pVal.Row).Specific.value, 2))
                            ' Dim arr() As String = Split(odbdsDetails.GetValue("U_HrsWrk", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.String - 1), ":")
                            Dim arr() As String = Split(HrsWorked, ":")
                            Dim shift_mins As Double = CDbl(arr(0) * 60 + Left(arr(1), 2)) 'Int(objform.Items.Item("txtstotal").Specific.string) * 60 + (objform.Items.Item("txtstotal").Specific.string - Int(objform.Items.Item("txtstotal").Specific.string))
                            arr = Split(Matrix0.Columns.Item("shour").Cells.Item(pVal.Row).Specific.string, ":")
                            shift_mins = shift_mins - (arr(0) * 60 + Left(arr(1), 2))
                            ' Dim Numb As Double = (CDbl(shift_mins / 60).ToString + CDbl(shift_mins - CDbl(shift_mins / 60) * 60).ToString).ToString
                            If (Int(shift_mins / 60).ToString + Int(shift_mins - Int(shift_mins / 60) * 60).ToString).ToString <= 0 Then
                                odbdsDetails.SetValue("U_OTHrs", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1, 0)
                                Matrix0.Columns.Item("OTH").Cells.Item(pVal.Row).Specific.string = 0
                            Else

                                odbdsDetails.SetValue("U_OTHrs", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1, (Int(shift_mins / 60).ToString + "." + Int(shift_mins - Int(shift_mins / 60) * 60).ToString).ToString)
                                Matrix0.Columns.Item("OTH").Cells.Item(pVal.Row).Specific.string = (Int(shift_mins / 60).ToString + "." + Int(shift_mins - Int(shift_mins / 60) * 60).ToString).ToString
                            End If

                        End If
                    Else
                        odbdsDetails.SetValue("U_OTHrs", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1, 0)
                        Matrix0.Columns.Item("OTH").Cells.Item(pVal.Row).Specific.string = 0
                    End If
                    'Matrix0.LoadFromDataSource()
                    Matrix0.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                    'If pVal.ColUID = "Timein" Then
                    '    Matrix0.Columns.Item("Timein").Cells.Item(pVal.Row).Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                    'Else
                    '    Matrix0.Columns.Item("trzid").Cells.Item(pVal.Row + 1).Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                    'End If
                    objform.Refresh()
                    objform.Update()
                    objform.Freeze(False)

                ElseIf pVal.ColUID = "attstatus" Then
                    Dim ocombo As SAPbouiCOM.ComboBox
                    ocombo = Matrix0.Columns.Item("attstatus").Cells.Item(pVal.Row).Specific
                    If ocombo.Selected.Value.ToString.ToUpper <> "PS" Then Exit Sub

                    Dim chkotapp As SAPbouiCOM.CheckBox
                    chkotapp = Matrix0.Columns.Item("otappl").Cells.Item(pVal.Row).Specific
                    If chkotapp.Checked = False Then Exit Sub

                    Dim chkholiday, chkweekend As SAPbouiCOM.CheckBox
                    chkholiday = Matrix0.Columns.Item("chkholi").Cells.Item(pVal.Row).Specific
                    chkweekend = Matrix0.Columns.Item("chkwend").Cells.Item(pVal.Row).Specific
                    If chkholiday.Checked = True Or chkweekend.Checked = True Then
                        odbdsDetails.SetValue("U_OTHrs", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1, odbdsDetails.GetValue("U_HrsWrk", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1))
                        Matrix0.Columns.Item("OTH").Cells.Item(pVal.Row).Specific.string = Matrix0.Columns.Item("whour").Cells.Item(pVal.Row).Specific.string ' odbdsDetails.GetValue("U_HrsWrk", Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Specific.string - 1)
                        objform.Refresh()
                        objform.Update()
                    Else
                        Matrix0.Columns.Item("OTH").Cells.Item(pVal.Row).Specific.string = 0
                    End If
                End If
            Catch ex As Exception
                objform.Freeze(False)
            End Try
        End Sub

        Private Sub EditText0_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText0.LostFocusAfter
            If Matrix0.VisualRowCount = 0 And EditText0.Value <> "" Then
                objform.Freeze(True)
                odbdsDetails.SetValue("LineID", 0, 1)
                Matrix0.LoadFromDataSource()
                objform.Freeze(False)
            End If
            If EditText0.String <> "" Then EditText1.Value = Date.ParseExact(EditText0.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo).ToString("dddd")

        End Sub

        Private Sub Button0_ClickBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button0.ClickBefore
            Try
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then Exit Sub
                objform.Freeze(True)
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Or objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE Then
                Else
                    objform.Freeze(False)
                    Exit Sub
                End If
                If EditText0.Value = "" Then
                    objaddon.objapplication.SetStatusBarMessage("Attendance Date is Missing", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                    BubbleEvent = False : objform.Freeze(False) : Exit Sub
                End If

                If EditText3.Value = "" Then
                    objaddon.objapplication.SetStatusBarMessage("Doc No is Missing", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                    BubbleEvent = False : objform.Freeze(False) : Exit Sub
                End If

                If EditText4.Value = "" Then
                    objaddon.objapplication.SetStatusBarMessage("Document Date is Missing", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                    BubbleEvent = False : objform.Freeze(False) : Exit Sub
                End If

                If Matrix0.VisualRowCount = 0 Then
                    objaddon.objapplication.SetStatusBarMessage("Attendance Details is Missing", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                    BubbleEvent = False : objform.Freeze(False) : Exit Sub
                End If

                If Matrix0.Columns.Item("trzid").Cells.Item(1).Specific.string = "" Then
                    objaddon.objapplication.SetStatusBarMessage("Attendance Details is Missing", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                    BubbleEvent = False : objform.Freeze(False) : Exit Sub
                End If

                'objform.Freeze(True)
                Dim removed As Boolean = False
                For i As Integer = Matrix0.VisualRowCount To 1 Step -1
                    If Matrix0.Columns.Item("trzid").Cells.Item(i).Specific.String = "" Then
                        odbdsDetails.RemoveRecord(i - 1)
                        removed = True
                    End If
                Next

                If removed = True Then
                    For i As Integer = 1 To Matrix0.VisualRowCount : odbdsDetails.SetValue("LineId", i - 1, i) : Next
                    Matrix0.LoadFromDataSource()
                End If
                objform.Freeze(False)
            Catch ex As Exception
                objform.Freeze(False)
            End Try

        End Sub

        Private Sub Button3_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button3.ClickAfter
            Try
                objform.ActiveItem = "txtremarks"
                Dim selectedrow As Integer = Matrix0.GetNextSelectedRow
                If selectedrow = -1 Then
                    objaddon.objapplication.SetStatusBarMessage("No Rows Selected", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                    Exit Sub
                Else
                    objform.Freeze(True)
                    odbdsDetails.RemoveRecord(Matrix0.Columns.Item("#").Cells.Item(selectedrow).Specific.string - 1)
                    For i As Integer = 1 To odbdsDetails.Size
                        odbdsDetails.SetValue("LineId", i - 1, i)
                    Next
                    Matrix0.LoadFromDataSource()
                    If selectedrow > Matrix0.VisualRowCount Then
                        Matrix0.Columns.Item("trzid").Cells.Item(Matrix0.VisualRowCount).Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                    Else
                        Matrix0.Columns.Item("trzid").Cells.Item(selectedrow).Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                    End If
                    objaddon.objapplication.SetStatusBarMessage("Selected Row Deleted Successfully", SAPbouiCOM.BoMessageTime.bmt_Short, False)
                    If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE
                    objform.Freeze(False)
                End If
            Catch ex As Exception
                objform.Freeze(False)
            End Try
        End Sub

        Private Sub frmDailyAttendance_DataLoadAfter(ByRef pVal As SAPbouiCOM.BusinessObjectInfo) Handles Me.DataLoadAfter
            Try

                FieldDisable()
                objaddon.objglobalmethods.LoadCombo_SingleSeries_AfterFind(objform, "cmbseries", "ODAS", ComboBox2.Value)
                Matrix0.AutoResizeColumns()
                objaddon.objapplication.Menus.Item("1300").Activate()
            Catch ex As Exception

            End Try
        End Sub

        Private Sub EditText4_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText4.LostFocusAfter
            Try
                objaddon.objglobalmethods.LoadCombo_Series(objform, "cmbseries", "ODAS", IIf(EditText4.String = "", Now.Date, Date.ParseExact(EditText4.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)))
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Button0_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button0.PressedAfter
            Try
                If pVal.ActionSuccess = True And objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                    objform.Items.Item("txtdocdt").Specific.string = Now.Date.ToString("yyyyMMdd")
                    odbdsheader.SetValue("DocEntry", 0, objaddon.objglobalmethods.GetNextDocentry_Value("@SMPR_ODAS"))
                    objform.ActiveItem = "txtadate"
                ElseIf pVal.ActionSuccess = True And objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then
                    objaddon.objapplication.Menus.Item("1304").Activate()
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Button4_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button4.ClickAfter
            Addrow()
        End Sub

        Private Sub Addrow()
            Try
                If Matrix0.VisualRowCount > 0 And EditText0.Value <> "" Then
                    If odbdsDetails.GetValue("U_IDNo", Matrix0.VisualRowCount - 1) = "" Then Exit Sub
                    objform.Freeze(True)
                    odbdsDetails.InsertRecord(odbdsDetails.Size)
                    odbdsDetails.SetValue("LineId", Matrix0.VisualRowCount, Matrix0.VisualRowCount + 1)
                    Matrix0.LoadFromDataSource()
                    objform.Freeze(False)
                End If
            Catch ex As Exception

            End Try
        End Sub
        Private WithEvents StaticText2 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox1 As SAPbouiCOM.ComboBox
        Private WithEvents ComboBox3 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText5 As SAPbouiCOM.StaticText


        Private Function Update_DailyAttendance(ByVal AttnDate As Date, ByVal DocEntry As String) As Integer
            Try
                Dim oGeneralService As SAPbobsCOM.GeneralService
                Dim oCompanyservice As SAPbobsCOM.CompanyService
                Dim oGeneralParams As SAPbobsCOM.GeneralDataParams
                Dim oGeneralData As SAPbobsCOM.GeneralData
                Dim ochildren As SAPbobsCOM.GeneralDataCollection
                Dim ochild As SAPbobsCOM.GeneralData
                Dim Getval As String = ""
                Dim Flag As Boolean
                Dim objrsdetail As SAPbobsCOM.Recordset
                oCompanyservice = objaddon.objcompany.GetCompanyService
                oGeneralService = oCompanyservice.GetGeneralService("ODAS")
                oGeneralData = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)
                'strsql = "Select distinct A.""Att_DocEntry"" from (SELECT ifnull((select distinct T0.""DocEntry"" from ""@SMPR_ODAS"" T0 join ""@SMPR_DAS1"" T1 on T0.""DocEntry""=T1.""DocEntry"" where T0.""U_AttdDate"" = '20220421'),0) as ""Att_DocEntry"""
                'strsql += vbCrLf + "FROM ""@SMPR_OHEM"" T0 WHERE T0.""U_status"" = 1 AND CAST('20220421' AS date) BETWEEN IFNULL(T0.""U_startdte"", '1950-10-09 00:00:00.000') AND IFNULL(T0.""U_termdate"", '2080-10-09 00:00:00.000')"
                'strsql += vbCrLf + "and T0.""U_empID"" not in (select distinct T1.""U_empID"" from ""@SMPR_ODAS"" T0 join ""@SMPR_DAS1"" T1 on T0.""DocEntry""=T1.""DocEntry"""
                'strsql += vbCrLf + "left join ""@SMPR_OHEM"" T2 on T1.""U_empID""=T2.""U_empID"" where T0.""U_AttdDate"" = '20220421' order by T1.""U_empID"" )"
                'strsql += vbCrLf + "and T0.""U_ExtEmpNo"" is not null order by T0.""U_firstNam"") as A"

                Try
                    oGeneralParams = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams)
                    oGeneralParams.SetProperty("DocEntry", DocEntry)
                    oGeneralData = oGeneralService.GetByParams(oGeneralData)
                    Flag = True
                Catch ex As Exception
                    Flag = False
                End Try
                ochildren = oGeneralData.Child("SMPR_DAS1")

                strsql = "SELECT T0.""U_empID"" AS ""Empid"", T0.""U_ExtEmpNo"" AS ""extempid"", case when T0.""U_lastName""<>'' then T0.""U_firstNam"" || ' ' || T0.""U_lastName"" else T0.""U_firstNam"" end AS ""Empname"","
                strsql += vbCrLf + "T0.""U_dept"" AS ""dept"", IFNULL(T0.""U_position"", 0) AS ""Position"" FROM ""@SMPR_OHEM"" T0 WHERE T0.""U_status"" = 1 AND CAST('" & AttnDate.ToString("yyyyMMdd") & "' AS date) BETWEEN IFNULL(T0.""U_startdte"", '1950-10-09 00:00:00.000') AND"
                strsql += vbCrLf + "IFNULL(T0.""U_termdate"", '2080-10-09 00:00:00.000')"
                strsql += vbCrLf + "and T0.""U_empID"" not in (select distinct T1.""U_empID"" from ""@SMPR_ODAS"" T0 join ""@SMPR_DAS1"" T1 on T0.""DocEntry""=T1.""DocEntry"""
                strsql += vbCrLf + " left join ""@SMPR_OHEM"" T2 on T1.""U_empID""=T2.""U_empID"" where T0.""U_AttdDate"" = '" & AttnDate.ToString("yyyyMMdd") & "'"
                strsql += vbCrLf + "order by T1.""U_empID"" )  and T0.""U_ExtEmpNo"" is not null order by T0.""U_firstNam"""
                objrsdetail = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrsdetail.DoQuery(strsql)

                For i As Integer = 0 To objrsdetail.RecordCount - 1
                    ochild = ochildren.Add
                    'ochild.SetProperty("DocEntry", DocEntry)
                    ochild.SetProperty("U_empID", objrsdetail.Fields.Item("Empid").Value.ToString)
                    ochild.SetProperty("U_empName", objrsdetail.Fields.Item("Empname").Value.ToString)
                    ochild.SetProperty("U_Dept", objrsdetail.Fields.Item("dept").Value.ToString)
                    ochild.SetProperty("U_Designat", objrsdetail.Fields.Item("Position").Value.ToString)
                    ochild.SetProperty("U_IDNo", objrsdetail.Fields.Item("extempid").Value.ToString)
                    'ochild.SetProperty("U_ShiftCode", objrsdetail.Fields.Item("").Value.ToString)
                    'ochild.SetProperty("U_ShiftName", objrsdetail.Fields.Item("").Value.ToString)
                    'ochild.SetProperty("U_TimeIn", objrsdetail.Fields.Item("").Value.ToString)
                    'ochild.SetProperty("U_TimeOut", objrsdetail.Fields.Item("").Value.ToString)
                    'ochild.SetProperty("U_HrsWrk", objrsdetail.Fields.Item("").Value.ToString)
                    ochild.SetProperty("U_Friday", "N")
                    ochild.SetProperty("U_Holiday", "N")
                    ochild.SetProperty("U_Halfday", "N")
                    ochild.SetProperty("U_HalfStatus", "-1")
                    ochild.SetProperty("U_AttStatus", "")
                    'ochild.SetProperty("", "")
                    objrsdetail.MoveNext()
                Next

                'oGeneralParams = oGeneralService.Add(oGeneralData)
                oGeneralService.Update(oGeneralData)
                'Return oGeneralParams.GetProperty("DocEntry")
            Catch ex As Exception

                Return -1
            End Try
        End Function

        Private Function Creating_Attendance(ByVal AttnDate As Date, ByVal DocEntry As String) As Boolean
            Try
                Dim Flag As Boolean = False
                Dim oGeneralService As SAPbobsCOM.GeneralService
                Dim oGeneralData As SAPbobsCOM.GeneralData
                Dim oGeneralParams As SAPbobsCOM.GeneralDataParams
                Dim oGeneralDataCollection As SAPbobsCOM.GeneralDataCollection
                Dim oChild As SAPbobsCOM.GeneralData
                Dim objrsdetail As SAPbobsCOM.Recordset
                oGeneralService = objaddon.objcompany.GetCompanyService.GetGeneralService("ODAS")
                oGeneralData = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)
                oGeneralParams = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams)
                oGeneralDataCollection = oGeneralData.Child("SMPR_DAS1")
                Try
                    oGeneralParams.SetProperty("DocEntry", DocEntry)
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams)
                    Flag = True
                Catch ex As Exception
                    Flag = False
                End Try
                strsql = "SELECT T0.""U_empID"" AS ""Empid"", T0.""U_ExtEmpNo"" AS ""extempid"", case when T0.""U_lastName""<>'' then T0.""U_firstNam"" || ' ' || T0.""U_lastName"" else T0.""U_firstNam"" end AS ""Empname"","
                strsql += vbCrLf + "T0.""U_dept"" AS ""dept"", IFNULL(T0.""U_position"", 0) AS ""Position"" FROM ""@SMPR_OHEM"" T0 WHERE T0.""U_status"" = 1 AND CAST('" & AttnDate.ToString("yyyyMMdd") & "' AS date) BETWEEN IFNULL(T0.""U_startdte"", '1950-10-09 00:00:00.000') AND"
                strsql += vbCrLf + "IFNULL(T0.""U_termdate"", '2080-10-09 00:00:00.000')"
                strsql += vbCrLf + "and T0.""U_empID"" not in (select distinct T1.""U_empID"" from ""@SMPR_ODAS"" T0 join ""@SMPR_DAS1"" T1 on T0.""DocEntry""=T1.""DocEntry"""
                strsql += vbCrLf + " left join ""@SMPR_OHEM"" T2 on T1.""U_empID""=T2.""U_empID"" where T0.""U_AttdDate"" = '" & AttnDate.ToString("yyyyMMdd") & "'"
                strsql += vbCrLf + "order by T1.""U_empID"")  and T0.""U_ExtEmpNo"" is not null order by T0.""U_firstNam"""
                objrsdetail = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrsdetail.DoQuery(strsql)
                oChild = oGeneralDataCollection.Add()
                Dim Row As Integer
                For i As Integer = 0 To objrsdetail.RecordCount - 1
                    Row = oGeneralData.Child("SMPR_DAS1").Count
                    If Row >= oGeneralData.Child("SMPR_DAS1").Count Then
                        oGeneralData.Child("SMPR_DAS1").Add()
                    End If
                    oGeneralData.Child("SMPR_DAS1").Item(Row).SetProperty("U_empID", objrsdetail.Fields.Item("Empid").Value.ToString)
                    oGeneralData.Child("SMPR_DAS1").Item(Row).SetProperty("U_empName", objrsdetail.Fields.Item("Empname").Value.ToString)
                    oGeneralData.Child("SMPR_DAS1").Item(Row).SetProperty("U_Dept", objrsdetail.Fields.Item("dept").Value.ToString)
                    oGeneralData.Child("SMPR_DAS1").Item(Row).SetProperty("U_Designat", objrsdetail.Fields.Item("Position").Value.ToString)
                    oGeneralData.Child("SMPR_DAS1").Item(Row).SetProperty("U_IDNo", objrsdetail.Fields.Item("extempid").Value.ToString)
                    oGeneralData.Child("SMPR_DAS1").Item(Row).SetProperty("U_Friday", "N")
                    oGeneralData.Child("SMPR_DAS1").Item(Row).SetProperty("U_Holiday", "N")
                    oGeneralData.Child("SMPR_DAS1").Item(Row).SetProperty("U_Halfday", "N")
                    oGeneralData.Child("SMPR_DAS1").Item(Row).SetProperty("U_HalfStatus", "-1")
                    oGeneralData.Child("SMPR_DAS1").Item(Row).SetProperty("U_AttStatus", "")
                    objrsdetail.MoveNext()
                    Row += 1
                Next

                If Flag = True Then
                    oGeneralService.Update(oGeneralData)
                    objaddon.objapplication.StatusBar.SetText("Modified Successfully...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                    Return True
                Else
                    oGeneralParams = oGeneralService.Add(oGeneralData)
                    objaddon.objapplication.StatusBar.SetText("Copied Successfully...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                    Return True
                End If

            Catch ex As Exception
                objaddon.objapplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                Return False
            End Try
        End Function

    End Class
End Namespace
