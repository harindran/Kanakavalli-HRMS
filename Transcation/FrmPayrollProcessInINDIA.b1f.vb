Option Strict Off
Option Explicit On

Imports SAPbouiCOM.Framework
Imports System.Net.Mail
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports
Imports System.IO
'Imports System.Data.SqlClient
'Imports System.Data
'Imports System.Net
'Imports System
'Imports System.ComponentModel
'Imports CrystalDecisions.CrystalReports.Design
'Imports CrystalDecisions.ReportAppServer.ClientDoc
'Imports CrystalDecisions.ReportAppServer.CommonControls
'Imports CrystalDecisions.ReportAppServer.CommonObjectModel
'Imports CrystalDecisions.ReportAppServer.Controllers
'Imports CrystalDecisions.ReportAppServer.CubeDefModel
''Imports CrystalDecisions.ReportAppServer.DataDefModel
'Imports CrystalDecisions.ReportAppServer.DataSetConversion
'Imports CrystalDecisions.ReportAppServer.ObjectFactory
'Imports CrystalDecisions.ReportAppServer.Prompting
''Imports CrystalDecisions.ReportAppServer.ReportDefModel
'Imports CrystalDecisions.ReportAppServer.XmlSerialize
'Imports CrystalDecisions.ReportSource
'Imports CrystalDecisions.Web
'Imports CrystalDecisions.Windows.Forms


Namespace HRMS

    <FormAttribute("OPPII", "Transcation/FrmPayrollProcessInINDIA.b1f")>
    Friend Class FrmPayrollProcessInINDIA
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
            Me.Matrix0 = CType(Me.GetItem("mtxpayroll").Specific, SAPbouiCOM.Matrix)
            Me.EditText3 = CType(Me.GetItem("txtentry").Specific, SAPbouiCOM.EditText)
            Me.EditText4 = CType(Me.GetItem("Item_1").Specific, SAPbouiCOM.EditText)
            Me.Button4 = CType(Me.GetItem("btnPayslip").Specific, SAPbouiCOM.Button)
            Me.ComboBox4 = CType(Me.GetItem("cmbseries").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText12 = CType(Me.GetItem("Series").Specific, SAPbouiCOM.StaticText)
            Me.EditText12 = CType(Me.GetItem("txtdocnum").Specific, SAPbouiCOM.EditText)
            Me.StaticText13 = CType(Me.GetItem("ldocdate").Specific, SAPbouiCOM.StaticText)
            Me.EditText13 = CType(Me.GetItem("txtDocDate").Specific, SAPbouiCOM.EditText)
            Me.ComboBox5 = CType(Me.GetItem("cmbpayprd").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText14 = CType(Me.GetItem("lPayprd").Specific, SAPbouiCOM.StaticText)
            Me.StaticText15 = CType(Me.GetItem("lfrmdate").Specific, SAPbouiCOM.StaticText)
            Me.EditText14 = CType(Me.GetItem("tfrmdate").Specific, SAPbouiCOM.EditText)
            Me.StaticText16 = CType(Me.GetItem("ltodate").Specific, SAPbouiCOM.StaticText)
            Me.EditText15 = CType(Me.GetItem("ttodate").Specific, SAPbouiCOM.EditText)
            Me.CheckBox2 = CType(Me.GetItem("chkfin").Specific, SAPbouiCOM.CheckBox)
            Me.Button7 = CType(Me.GetItem("btnPayroll").Specific, SAPbouiCOM.Button)
            Me.Button8 = CType(Me.GetItem("btnJE").Specific, SAPbouiCOM.Button)
            Me.LinkedButton2 = CType(Me.GetItem("Item_41").Specific, SAPbouiCOM.LinkedButton)
            Me.EditText16 = CType(Me.GetItem("txtJENo").Specific, SAPbouiCOM.EditText)
            Me.StaticText17 = CType(Me.GetItem("Item_43").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox6 = CType(Me.GetItem("cmbbranch").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText18 = CType(Me.GetItem("lbranch").Specific, SAPbouiCOM.StaticText)
            Me.EditText17 = CType(Me.GetItem("tentry").Specific, SAPbouiCOM.EditText)
            Me.StaticText0 = CType(Me.GetItem("Item_0").Specific, SAPbouiCOM.StaticText)
            Me.EditText0 = CType(Me.GetItem("txtrem").Specific, SAPbouiCOM.EditText)
            Me.ComboBox0 = CType(Me.GetItem("cmbloc").Specific, SAPbouiCOM.ComboBox)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()
            AddHandler DataLoadAfter, AddressOf Me.Form_DataLoadAfter
            AddHandler ActivateAfter, AddressOf Me.Form_ActivateAfter

        End Sub

#Region "fields"

        Private WithEvents Button0 As SAPbouiCOM.Button
        Private WithEvents Button1 As SAPbouiCOM.Button
        Private WithEvents Matrix0 As SAPbouiCOM.Matrix
        Private WithEvents EditText3 As SAPbouiCOM.EditText
        Private WithEvents EditText4 As SAPbouiCOM.EditText
        Private WithEvents Button4 As SAPbouiCOM.Button
        Private WithEvents ComboBox4 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText12 As SAPbouiCOM.StaticText
        Private WithEvents EditText12 As SAPbouiCOM.EditText
        Private WithEvents StaticText13 As SAPbouiCOM.StaticText
        Private WithEvents EditText13 As SAPbouiCOM.EditText
        Private WithEvents ComboBox5 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText14 As SAPbouiCOM.StaticText
        Private WithEvents StaticText15 As SAPbouiCOM.StaticText
        Private WithEvents EditText14 As SAPbouiCOM.EditText
        Private WithEvents StaticText16 As SAPbouiCOM.StaticText
        Private WithEvents EditText15 As SAPbouiCOM.EditText
        Private WithEvents CheckBox2 As SAPbouiCOM.CheckBox
        Private WithEvents Button7 As SAPbouiCOM.Button
        Private WithEvents Button8 As SAPbouiCOM.Button
        Private WithEvents LinkedButton2 As SAPbouiCOM.LinkedButton
        Private WithEvents EditText16 As SAPbouiCOM.EditText
        Private WithEvents StaticText17 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox6 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText18 As SAPbouiCOM.StaticText
        Private WithEvents EditText17 As SAPbouiCOM.EditText

#End Region

        Public Entry As String = ""
        Dim addupdate As Boolean = False

        Private Sub OnCustomInitialize()
            Try
                objform = objaddon.objapplication.Forms.GetForm("OPPII", Me.FormCount)
                objform = objaddon.objapplication.Forms.ActiveForm
                objform.Freeze(True)
                ' odbdsDetails = objform.DataSources.DBDataSources.Item(CType(1, Object))
                objform.Items.Item("txtDocDate").Specific.string = Now.Date.ToString("dd/MM/yy")
                odbdsDetails = objform.DataSources.DBDataSources.Item("@MIPL_OPPI")
                objaddon.objglobalmethods.LoadSeries(objform, odbdsDetails, "OPPII")
                objform.Items.Item("txtrem").Specific.String = "Created By " & objaddon.objcompany.UserName & " on " & Now.ToString("dd/MMM/yyyy HH:mm:ss")
                ' Entry = objaddon.objglobalmethods.GetNextDocentry_Value("@MIPL_OPPI")
                'objform.Items.Item("txtentry").Specific.string = objaddon.objglobalmethods.GetNextDocentry_Value("@MIPL_OPPI")
                'Load_Combobox(objform)
                If MultiBranch = "Y" Then
                    StaticText18.Caption = "Branch"
                    ComboBox0.Item.Visible = False
                    ComboBox6.Item.Left = EditText15.Item.Left
                    ComboBox6.Item.Top = StaticText18.Item.Top
                Else
                    StaticText18.Caption = "Location"
                    ComboBox6.Item.Visible = False
                    ComboBox0.Item.Left = EditText15.Item.Left
                    ComboBox0.Item.Top = StaticText18.Item.Top
                End If
                Matrix0.Columns.Item("Dept").Visible = False
                'Matrix0.Columns.Item("TotalDays").Visible = False
                Matrix0.Columns.Item("TotHrs").Visible = False
                Matrix0.Columns.Item("DaySal").Visible = False
                Matrix0.Columns.Item("HrSal").Visible = False
                Matrix0.Columns.Item("shifthrs").Visible = False
                CheckBox2.Item.Height = CheckBox2.Item.Height + 2
                Comboload()
                LoadComboDetails()
                ManageAttributes()
                Matrix0.AddRow()
                Button4.Item.Enabled = False
                objform.Settings.Enabled = True
                'objform.Settings.MatrixUID = "mtxpayroll"
                'MultiBranch = objaddon.objglobalmethods.getSingleValue("select ""MltpBrnchs"" from OADM")
                objaddon.objapplication.Menus.Item("1300").Activate() 'Fit colum width
                'objform.EnableMenu("4870", True)
                'objaddon.objapplication.Menus.Item("4870").Activate()
                objform.Freeze(False)
            Catch ex As Exception
                objform.Freeze(False)
            End Try
        End Sub

        Private Sub Comboload()
            Try
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery("CALL ""MIPL_HRMS_EMPMASTER_COMBO_FILLING"" ('OPAY')")
                If objrs.RecordCount = 0 Then Exit Sub
                For i As Integer = 0 To objrs.RecordCount - 1
                    Try
                        Select Case objrs.Fields.Item("Type").Value.ToString.ToUpper
                            Case "PAYPERIOD" : ComboBox5.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)

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
                'ComboBox6.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, SAPbouiCOM.BoAutoFormMode.afm_Ok, SAPbouiCOM.BoModeVisualBehavior.mvb_False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "tfrmdate", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "ttodate", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtDocDate", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtdocnum", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "tentry", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtJENo", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cmbbranch", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "chkfin", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cmbpayprd", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cmbseries", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "btnPayroll", True, False, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cmbloc", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "btnJE", True, False, True)
                Dim Fsize As Size
                Fsize = TextRenderer.MeasureText(Button4.Caption, New Font("Arial", 12.0F))
                Button4.Item.Width = Fsize.Width + 30
                Matrix0.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, SAPbouiCOM.BoAutoFormMode.afm_Ok, SAPbouiCOM.BoModeVisualBehavior.mvb_False)
            Catch ex As Exception

            End Try
        End Sub

        'Private Sub Button2_ClickBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles Button2.ClickBefore
        '    'Throw New System.NotImplementedException()
        '    If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Or objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE Then Exit Sub
        '    If EditText12.Value = "" Then
        '        objaddon.objapplication.SetStatusBarMessage("FromDate is Missing", SAPbouiCOM.BoMessageTime.bmt_Short, True)
        '        BubbleEvent = False : Exit Sub
        '    End If
        '    If EditText3.Value = "" Then
        '        objaddon.objapplication.SetStatusBarMessage("ToDate is Missing", SAPbouiCOM.BoMessageTime.bmt_Short, True)
        '        BubbleEvent = False : Exit Sub
        '    End If
        '    If MultiBranch = "Y" Then
        '        If ComboBox4.Value = "" Then
        '            objaddon.objapplication.SetStatusBarMessage("Branch is Missing", SAPbouiCOM.BoMessageTime.bmt_Short, True)
        '            BubbleEvent = False : Exit Sub
        '        End If
        '    End If

        'End Sub

        'Public Sub IN_JournalEntry()
        '    Dim objjournalentry As SAPbobsCOM.JournalEntries
        '    Dim DocEntry As String
        '    Dim Flag As Boolean = False

        '    objjournalentry = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries)
        '    strsql = "CALL ""MIPL_GetJEAccount_Details"" "            '
        '    objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        '    objrs.DoQuery(strsql)
        '    Dim TotGross As Double = 0, TotEncash As Double = 0, TotBonus As Double = 0, TotIncent As Double = 0
        '    Dim TotPF As Double = 0, TotEmployerPF_ESI As Double = 0, TotESI As Double = 0, TotPT As Double = 0, TotTDS As Double = 0, TotLoan As Double = 0, TotSalPayable As Double = 0
        '    For i As Integer = 1 To Matrix0.VisualRowCount
        '        If Matrix0.Columns.Item("Salstat").Cells.Item(i).Specific.Checked = True Then
        '            TotGross += CDbl(Matrix0.Columns.Item("GrossSal").Cells.Item(i).Specific.string)
        '            ' TotEncash += CDbl(Matrix0.Columns.Item("Encash").Cells.Item(i).Specific.string)
        '            TotBonus += CDbl(Matrix0.Columns.Item("Bonus").Cells.Item(i).Specific.string)
        '            TotIncent += CDbl(Matrix0.Columns.Item("Incent").Cells.Item(i).Specific.string)
        '            TotPF += CDbl(Matrix0.Columns.Item("PF").Cells.Item(i).Specific.string)
        '            TotESI += CDbl(Matrix0.Columns.Item("ESI").Cells.Item(i).Specific.string)
        '            TotPT += CDbl(Matrix0.Columns.Item("ProfTax").Cells.Item(i).Specific.string)
        '            TotTDS += CDbl(Matrix0.Columns.Item("TDS").Cells.Item(i).Specific.string)
        '            TotLoan += CDbl(Matrix0.Columns.Item("Loan").Cells.Item(i).Specific.string)
        '            TotSalPayable += Matrix0.Columns.Item("Payable").Cells.Item(i).Specific.string
        '            TotEmployerPF_ESI += CDbl(Matrix0.Columns.Item("EPF").Cells.Item(i).Specific.string) + CDbl(Matrix0.Columns.Item("EESI").Cells.Item(i).Specific.string)
        '            Flag = True
        '        End If
        '    Next
        '    If Flag = False Then
        '        objaddon.objapplication.SetStatusBarMessage("Please select the status in line level...", SAPbouiCOM.BoMessageTime.bmt_Short)
        '        Exit Sub
        '    End If
        '    Dim FDate As Date = Date.ParseExact(EditText3.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
        '    Dim Branch As String = ""
        '    If ComboBox2.Selected.Value = "-1" Or ComboBox2.Selected.Value Is Nothing Then
        '        Branch = objaddon.objglobalmethods.getSingleValue("select ""BPLId"" from OBPL where ""BPLId""='1'")
        '    Else
        '        Branch = objaddon.objglobalmethods.getSingleValue("select ""BPLId"" from OBPL where ""BPLId""='" & ComboBox0.Selected.Value & "'")
        '    End If
        '    If Not objaddon.objcompany.InTransaction Then objaddon.objcompany.StartTransaction()
        '    'objjournalentry.Series = Series
        '    objjournalentry.ReferenceDate = Now.ToString("yyyy-MM-dd")
        '    objjournalentry.Memo = "Payroll Process for the month -" & CStr(MonthName(FDate.Month, True))
        '    If TotPF > 0 Then
        '        objjournalentry.Lines.AccountCode = objrs.Fields.Item("PF").Value.ToString   ' credit
        '        objjournalentry.Lines.Credit = TotPF
        '        objjournalentry.Lines.BPLID = Branch
        '        ' objjournalentry.Lines.LocationCode = 1
        '        objjournalentry.Lines.Debit = 0
        '        objjournalentry.Lines.Add()
        '    End If
        '    If TotGross > 0 Then
        '        objjournalentry.Lines.AccountCode = objrs.Fields.Item("Salary").Value.ToString  'debit
        '        objjournalentry.Lines.Credit = 0
        '        objjournalentry.Lines.Debit = TotGross
        '        objjournalentry.Lines.BPLID = Branch
        '        'objjournalentry.Lines.LocationCode = 1
        '        objjournalentry.Lines.Add()
        '    End If
        '    If TotESI > 0 Then
        '        objjournalentry.Lines.AccountCode = objrs.Fields.Item("ESI").Value.ToString    'Credit
        '        objjournalentry.Lines.Credit = TotESI
        '        objjournalentry.Lines.BPLID = Branch
        '        'objjournalentry.Lines.LocationCode = 1
        '        objjournalentry.Lines.Debit = 0
        '        objjournalentry.Lines.Add()
        '    End If
        '    If TotBonus > 0 Then
        '        objjournalentry.Lines.AccountCode = objrs.Fields.Item("Salary").Value.ToString 'Debit
        '        objjournalentry.Lines.Credit = 0
        '        objjournalentry.Lines.Debit = TotBonus
        '        objjournalentry.Lines.BPLID = Branch
        '        'objjournalentry.Lines.LocationCode = 1
        '        objjournalentry.Lines.Add()
        '    End If
        '    If TotTDS > 0 Then
        '        objjournalentry.Lines.AccountCode = objrs.Fields.Item("TDS").Value.ToString  'Credit
        '        objjournalentry.Lines.Credit = TotTDS
        '        objjournalentry.Lines.BPLID = Branch
        '        'objjournalentry.Lines.LocationCode = 1
        '        objjournalentry.Lines.Debit = 0
        '        objjournalentry.Lines.Add()
        '    End If
        '    If TotIncent > 0 Then
        '        objjournalentry.Lines.AccountCode = objrs.Fields.Item("Salary").Value.ToString  'Debit
        '        objjournalentry.Lines.Credit = 0
        '        objjournalentry.Lines.Debit = TotIncent
        '        objjournalentry.Lines.BPLID = Branch
        '        'objjournalentry.Lines.LocationCode = 1
        '        objjournalentry.Lines.Add()
        '    End If
        '    If TotPT > 0 Then
        '        objjournalentry.Lines.AccountCode = objrs.Fields.Item("PT").Value.ToString  'Credit
        '        objjournalentry.Lines.Credit = TotPT
        '        objjournalentry.Lines.BPLID = Branch
        '        ' objjournalentry.Lines.LocationCode = 1
        '        objjournalentry.Lines.Debit = 0
        '        objjournalentry.Lines.Add()
        '    End If
        '    If TotLoan > 0 Then
        '        objjournalentry.Lines.AccountCode = objrs.Fields.Item("Loan").Value.ToString  'Credit
        '        objjournalentry.Lines.Credit = TotLoan
        '        objjournalentry.Lines.Debit = 0
        '        objjournalentry.Lines.BPLID = Branch
        '        'objjournalentry.Lines.LocationCode = 1
        '        objjournalentry.Lines.Add()
        '    End If
        '    If TotSalPayable > 0 Then
        '        objjournalentry.Lines.AccountCode = objrs.Fields.Item("SalPay").Value.ToString  'Credit
        '        objjournalentry.Lines.Credit = TotSalPayable
        '        objjournalentry.Lines.BPLID = Branch
        '        'objjournalentry.Lines.LocationCode = 1
        '        objjournalentry.Lines.Debit = 0
        '        objjournalentry.Lines.Add()
        '    End If
        '    If TotEmployerPF_ESI > 0 Then
        '        objjournalentry.Lines.AccountCode = objrs.Fields.Item("SalPay").Value.ToString  'Credit
        '        objjournalentry.Lines.Credit = TotEmployerPF_ESI
        '        objjournalentry.Lines.BPLID = Branch
        '        'objjournalentry.Lines.LocationCode = 1
        '        objjournalentry.Lines.Debit = 0
        '        objjournalentry.Lines.Add()
        '    End If
        '    If objjournalentry.Add <> 0 Then
        '        If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
        '        objaddon.objapplication.SetStatusBarMessage(objaddon.objcompany.GetLastErrorDescription, SAPbouiCOM.BoMessageTime.bmt_Short)
        '        CheckBox1.Checked = False
        '    Else
        '        If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
        '        DocEntry = objaddon.objcompany.GetNewObjectKey()
        '        objform.Items.Item("txtJENo").Specific.String = DocEntry
        '        objaddon.objapplication.SetStatusBarMessage("Journal Entry Successfully Posted..." & DocEntry, SAPbouiCOM.BoMessageTime.bmt_Long, False)
        '    End If
        'End Sub

        Private Sub LoadComboDetails()
            Try
                'ComboBox6.ValidValues.Add("-1", "All")
                ComboBox0.ValidValues.Add("-1", "All")
                Dim objrs As SAPbobsCOM.Recordset
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery("CALL ""MIPL_HRMS_EMPMASTER_COMBO_FILLING"" ('OHEM')")
                If objrs.RecordCount = 0 Then Exit Sub
                For i As Integer = 0 To objrs.RecordCount - 1
                    Try
                        Select Case objrs.Fields.Item("Type").Value.ToString.ToUpper
                            Case "BRANCH" : ComboBox6.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "LOCATION" : ComboBox0.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
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
                Dim cmbdesignation As SAPbouiCOM.Column = Matrix0.Columns.Item("Designat")
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

        Private Function GetProfTaxAmount(ByVal amount As Double, ByVal Location As String) As Double
            Dim PTAmt As String = objaddon.objglobalmethods.getSingleValue("select Top 1 ""U_Amount"" from ""@MIPL_PT"" where " & amount & " between ""U_FromLimit"" and ""U_ToLimit"" and ""U_Location""='" & Location & "' and IFNULL(""U_Active"", '')= 'Y'")
            If PTAmt <> "" Then
                Return PTAmt
            Else
                Return 0
            End If

        End Function

        Public Sub Matrix_Total()
            Try
                Matrix0.Columns.Item("GrossSal").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
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
                Matrix0.Columns.Item("EmpCont").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("LopAmt").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("round").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
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

        'Private Sub Button2_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button2.ClickAfter
        '    Dim TotDays As String
        '    Try
        '        If CheckBox2.Checked = True Then
        '            objaddon.objapplication.SetStatusBarMessage("Payroll Already Finalized.", SAPbouiCOM.BoMessageTime.bmt_Short, True)
        '            Exit Sub
        '        End If
        '        Dim FDate As Date = Date.ParseExact(EditText14.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
        '        Dim TDate As Date = Date.ParseExact(EditText15.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
        '        If objaddon.objapplication.MessageBox("Do you want to Calculate Payroll?", 2, "Yes", "No") <> 1 Then Exit Sub
        '        'TotDays = objaddon.objglobalmethods.getSingleValue("select 1 from ""@MIPL_CALM"" where ""U_Year""=year('" & FDate.ToString("yyyyMMdd") & "') and upper(""U_MonName"")=upper (left(MonthName('" & FDate.ToString("yyyyMMdd") & "'),3))  ")
        '        TotDays = objaddon.objglobalmethods.getSingleValue("Select 1 from ""@MIPL_OCAL"" T0 left join ""@MIPL_CAL1"" T1 on T0.""DocEntry""=T1.""DocEntry"" where T0.""U_Year""=year('" & FDate.ToString("yyyyMMdd") & "') and upper(T0.""U_MonName"")=upper (left(MonthName('" & FDate.ToString("yyyyMMdd") & "'),3)) and T1.""U_Branch""='" & ComboBox0.Selected.Value & "' Order by T1.""DocEntry"" desc")
        '        If TotDays <> "1" Then
        '            objaddon.objapplication.SetStatusBarMessage("You should define total working days for the month in calendar master...", SAPbouiCOM.BoMessageTime.bmt_Long, True)
        '            Exit Sub
        '        End If
        '        objaddon.objapplication.SetStatusBarMessage("Calculating Payroll Details. Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Long, False)
        '        objform.Freeze(True)
        '        odbdsDetails = objform.DataSources.DBDataSources.Item(CType(1, Object))
        '        odbdsDetails.Clear()
        '        Matrix0.LoadFromDataSource()

        '        If MultiBranch = "Y" Then
        '            strsql = "CALL ""MIPL_Payroll_Calculation"" ('" & FDate.ToString("yyyyMMdd") & "','" & TDate.ToString("yyyyMMdd") & "'"
        '            If Not ComboBox0.Selected Is Nothing Then
        '                If ComboBox0.Selected.Value = "-1" Then strsql += " ,'')" Else strsql += " ,'" & ComboBox0.Selected.Value & "')"
        '            Else
        '                strsql += " ,'')"
        '            End If
        '        Else
        '            strsql = "CALL ""MIPL_Payroll_CalculationWOBranch"" ('" & FDate.ToString("yyyyMMdd") & "','" & TDate.ToString("yyyyMMdd") & "')"
        '        End If

        '        objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        '        objrs.DoQuery(strsql)

        '        If objrs.RecordCount = 0 Then objaddon.objapplication.SetStatusBarMessage("No records Found", SAPbouiCOM.BoMessageTime.bmt_Short, True) : objform.Freeze(False) : Exit Sub

        '        odbdsDetails.InsertRecord(odbdsDetails.Size)
        '        objaddon.objapplication.SetStatusBarMessage("Filling Payroll Details. Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Long, False)

        '        For i As Integer = 0 To objrs.RecordCount - 1
        '            odbdsDetails.SetValue("LineId", i, i + 1)
        '            odbdsDetails.SetValue("U_IDNo", i, objrs.Fields.Item("U_empid").Value.ToString)
        '            odbdsDetails.SetValue("U_empID", i, objrs.Fields.Item("EmpCode").Value.ToString)
        '            odbdsDetails.SetValue("U_empName", i, objrs.Fields.Item("EmpName").Value.ToString)
        '            odbdsDetails.SetValue("U_Designat", i, objrs.Fields.Item("Designation").Value.ToString)
        '            odbdsDetails.SetValue("U_Dept", i, objrs.Fields.Item("DeptCode").Value.ToString)
        '            odbdsDetails.SetValue("U_Paymode", i, objrs.Fields.Item("PayMode").Value.ToString)
        '            odbdsDetails.SetValue("U_TotalDays", i, objrs.Fields.Item("TotalDays").Value.ToString)
        '            odbdsDetails.SetValue("U_PaidDays", i, objrs.Fields.Item("WorkedDays").Value.ToString)
        '            odbdsDetails.SetValue("U_PayDays", i, objrs.Fields.Item("TotalWorkableDays").Value.ToString)
        '            odbdsDetails.SetValue("U_WoDays", i, objrs.Fields.Item("PayableDays").Value.ToString)
        '            odbdsDetails.SetValue("U_Basic", i, objrs.Fields.Item("TotalBasic").Value.ToString)
        '            odbdsDetails.SetValue("U_HRA", i, objrs.Fields.Item("HRA").Value.ToString)
        '            odbdsDetails.SetValue("U_PF", i, objrs.Fields.Item("PF").Value.ToString)
        '            odbdsDetails.SetValue("U_EmpPF", i, objrs.Fields.Item("PF").Value.ToString)
        '            odbdsDetails.SetValue("U_EmpESI", i, objrs.Fields.Item("EmployerESI").Value.ToString)
        '            odbdsDetails.SetValue("U_LOPAmt", i, objrs.Fields.Item("LOPAmount").Value.ToString)
        '            odbdsDetails.SetValue("U_ESI", i, objrs.Fields.Item("ESI").Value.ToString)
        '            odbdsDetails.SetValue("U_DearAll", i, objrs.Fields.Item("DearnessAllowance").Value.ToString)
        '            odbdsDetails.SetValue("U_MedAll", i, objrs.Fields.Item("MedicalAllowance").Value.ToString)
        '            odbdsDetails.SetValue("U_OthrAll", i, objrs.Fields.Item("OtherAllowance").Value.ToString)
        '            odbdsDetails.SetValue("U_GrossSal", i, objrs.Fields.Item("GrossSalary").Value.ToString)
        '            odbdsDetails.SetValue("U_TDS", i, objrs.Fields.Item("TDS").Value.ToString)
        '            odbdsDetails.SetValue("U_ProfTax", i, objrs.Fields.Item("PT").Value.ToString)
        '            odbdsDetails.SetValue("U_LOPDays", i, objrs.Fields.Item("LOPDays").Value.ToString)
        '            odbdsDetails.SetValue("U_LateHrs", i, objrs.Fields.Item("LateHrs").Value.ToString)
        '            odbdsDetails.SetValue("U_Loan", i, objrs.Fields.Item("Loan").Value.ToString)
        '            odbdsDetails.SetValue("U_TotHrs", i, objrs.Fields.Item("TotHrs").Value.ToString)
        '            odbdsDetails.SetValue("U_WorkHrs", i, objrs.Fields.Item("WorkedHrs").Value.ToString)
        '            odbdsDetails.SetValue("U_DaySal", i, objrs.Fields.Item("DaySalary").Value.ToString)
        '            odbdsDetails.SetValue("U_HrSal", i, objrs.Fields.Item("HrSalary").Value.ToString)
        '            odbdsDetails.SetValue("U_LopHrs", i, objrs.Fields.Item("LOPHrs").Value.ToString)
        '            odbdsDetails.SetValue("U_CODays", i, objrs.Fields.Item("CompOff").Value.ToString)
        '            odbdsDetails.SetValue("U_ELDays", i, objrs.Fields.Item("CarryFwdLv").Value.ToString)
        '            odbdsDetails.SetValue("U_LeaveBal", i, objrs.Fields.Item("LeaveBal").Value.ToString)
        '            odbdsDetails.SetValue("U_LeaveTak", i, objrs.Fields.Item("LeaveTaken").Value.ToString)
        '            odbdsDetails.SetValue("U_shifthrs", i, objrs.Fields.Item("ShiftHrs").Value.ToString)
        '            odbdsDetails.SetValue("U_ELTaken", i, objrs.Fields.Item("ELLIGIBLELVDAYS").Value.ToString)
        '            objrs.MoveNext()

        '            If i <> objrs.RecordCount - 1 Then odbdsDetails.InsertRecord(odbdsDetails.Size)
        '        Next

        '        Matrix0.LoadFromDataSource()
        '        Matrix0.CommonSetting.FixedColumnsCount = 5
        '        objaddon.objapplication.Menus.Item("1300").Activate() 'Fit colum width
        '        TaxCalculation()
        '        Matrix_Total()
        '        objform.Update()
        '        objform.Refresh()
        '        Dim status As String = ""
        '        Dim Row As Integer = 1
        '        While Row <= Matrix0.RowCount
        '            status = objaddon.objglobalmethods.getSingleValue("select case when count(*)>1 or count(*)=1  then True else False end as ""status"" from  ""@MIPL_PPI1"" T1 join ""@MIPL_OPPI"" T2" &
        '                                                                 " on T1.""DocEntry""=T2.""DocEntry"" where ""U_empID""='" & Matrix0.Columns.Item("empID").Cells.Item(Row).Specific.String & "'and T2.""U_FromDate"" between '" & FDate.ToString("yyyyMMdd") & "' AND '" & TDate.ToString("yyyyMMdd") & "'" &
        '                                                                " and T2.""U_ToDate"" between '" & FDate.ToString("yyyyMMdd") & "' AND '" & TDate.ToString("yyyyMMdd") & "' and ifnull(T1.""U_SalProcess"",'')='Y';")

        '            If status = "1" Then
        '                Matrix0.DeleteRow(Row)
        '            Else
        '                Row += 1
        '                If Row = Matrix0.RowCount Then
        '                    Exit While
        '                End If
        '            End If
        '        End While
        '        objform.Update()
        '        objform.Refresh()
        '        If Matrix0.RowCount = 0 Then
        '            objaddon.objapplication.StatusBar.SetText("No Records Found", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
        '        Else
        '            objaddon.objapplication.StatusBar.SetText("Payroll Details Loaded successfully", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
        '        End If

        '        'If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE
        '        objform.Freeze(False)
        '        objform.Items.Item("txtDocDate").Specific.string = Now.Date.ToString("dd/MM/yy")
        '        ' End If
        '    Catch ex As Exception
        '        objaddon.objapplication.SetStatusBarMessage("Error While Loading Payroll Details : " & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)
        '        objform.Freeze(False)
        '    End Try
        'End Sub

        'Private Sub TaxCalculation()
        '    Try
        '        objform.Freeze(True)
        '        Dim PT As String, Location As String
        '        Dim Payable, Deduction, PTCal As Double
        '        Dim Addition, GrossSal As Double, LOAN As Double = 0, NetSal As Double

        '        For i As Integer = 1 To Matrix0.RowCount
        '            If Matrix0.Columns.Item("empID").Cells.Item(i).Specific.String <> "" Then
        '                PT = objaddon.objglobalmethods.getSingleValue(" select ""U_PTStat"" from ""@SMPR_OHEM"" T0 where IFNULL(""U_PTStat"", '')= 'Y' and  T0.""U_ExtEmpNo""='" & Matrix0.Columns.Item("empID").Cells.Item(i).Specific.String & "'")
        '                Location = objaddon.objglobalmethods.getSingleValue(" select ""U_location"" from ""@SMPR_OHEM"" T0 where IFNULL(""U_PTStat"", '')= 'Y' and  T0.""U_ExtEmpNo""='" & Matrix0.Columns.Item("empID").Cells.Item(i).Specific.String & "'")
        '                If PT = "Y" Then
        '                    strsql = "CALL ""MIPLGetSalary"" ('" & Matrix0.Columns.Item("empID").Cells.Item(i).Specific.String & "')"
        '                    objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        '                    objrs.DoQuery(strsql)
        '                    PTCal = Math.Round(GetProfTaxAmount(objrs.Fields.Item("GAmount").Value, Location))
        '                    Matrix0.Columns.Item("ProfTax").Cells.Item(i).Specific.string = PTCal
        '                Else
        '                    PTCal = 0
        '                    Matrix0.Columns.Item("ProfTax").Cells.Item(i).Specific.string = PTCal
        '                End If

        '                Deduction = CDbl(PTCal) + CDbl(Matrix0.Columns.Item("ESI").Cells.Item(i).Specific.string) + CDbl(Matrix0.Columns.Item("PF").Cells.Item(i).Specific.string) + CDbl(Matrix0.Columns.Item("EPF").Cells.Item(i).Specific.string) + CDbl(Matrix0.Columns.Item("EESI").Cells.Item(i).Specific.string)
        '                Addition = CDbl(Matrix0.Columns.Item("Bonus").Cells.Item(i).Specific.string) + CDbl(Matrix0.Columns.Item("Incent").Cells.Item(i).Specific.string)
        '                GrossSal = CDbl(Matrix0.Columns.Item("GrossSal").Cells.Item(i).Specific.string)
        '                'EmpPF = CDbl(Matrix0.Columns.Item("EPF").Cells.Item(i).Specific.string)
        '                NetSal = GrossSal - Deduction
        '                Payable = NetSal + Addition
        '                Matrix0.Columns.Item("TotDed").Cells.Item(i).Specific.string = CStr(Deduction)
        '                'Matrix0.Columns.Item("TotAdd").Cells.Item(i).Specific.string = CStr(Addition)
        '                If NetSal < 0 Then
        '                    Matrix0.Columns.Item("NetSal").Cells.Item(i).Specific.string = 0
        '                Else
        '                    Matrix0.Columns.Item("NetSal").Cells.Item(i).Specific.string = CStr(NetSal)
        '                End If
        '                If Payable < 0 Then
        '                    Matrix0.Columns.Item("Payable").Cells.Item(i).Specific.string = 0
        '                Else
        '                    Matrix0.Columns.Item("Payable").Cells.Item(i).Specific.string = CStr(Payable)
        '                End If

        '            End If
        '        Next
        '        objform.Freeze(False)
        '    Catch ex As Exception

        '    End Try
        'End Sub

        Private Sub TaxCalculation()
            Try
                objform.Freeze(True)
                'Dim PT As String, Location As String
                Dim Payable, Deduction, PTCal As Double
                Dim Addition, GrossSal As Double, LOAN As Double = 0, NetSal As Double

                For i As Integer = 1 To Matrix0.VisualRowCount
                    If Matrix0.Columns.Item("empID").Cells.Item(i).Specific.String <> "" Then
                        'Deduction = CDbl(Matrix0.Columns.Item("ProfTax").Cells.Item(i).Specific.string) + CDbl(Matrix0.Columns.Item("ESI").Cells.Item(i).Specific.string) + CDbl(Matrix0.Columns.Item("PF").Cells.Item(i).Specific.string) '+ CDbl(Matrix0.Columns.Item("EPF").Cells.Item(i).Specific.string) + CDbl(Matrix0.Columns.Item("EESI").Cells.Item(i).Specific.string)
                        Deduction = CDbl(Matrix0.Columns.Item("TotDed").Cells.Item(i).Specific.string) + CDbl(Matrix0.Columns.Item("TDS").Cells.Item(i).Specific.string)
                        Addition = CDbl(Matrix0.Columns.Item("Bonus").Cells.Item(i).Specific.string) + CDbl(Matrix0.Columns.Item("Incent").Cells.Item(i).Specific.string)
                        GrossSal = CDbl(Matrix0.Columns.Item("GrossSal").Cells.Item(i).Specific.string)
                        'EmpPF = CDbl(Matrix0.Columns.Item("EPF").Cells.Item(i).Specific.string)
                        NetSal = GrossSal - Deduction
                        Payable = NetSal + Addition
                        Matrix0.Columns.Item("TotDed").Cells.Item(i).Specific.string = CStr(Deduction)
                        'Matrix0.Columns.Item("TotAdd").Cells.Item(i).Specific.string = CStr(Addition)
                        If NetSal <= 0 Then
                            Matrix0.Columns.Item("NetSal").Cells.Item(i).Specific.string = 0
                        Else
                            Matrix0.Columns.Item("NetSal").Cells.Item(i).Specific.string = CStr(NetSal)
                        End If
                        If Payable <= 0 Then
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

        'Private Sub Button3_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button3.ClickAfter
        '    If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Or objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE Then
        '        If CheckBox0.Checked = True Then
        '            If ApprovedUser_Employee Then
        '                If EditText5.Value = "" Then
        '                    IN_JournalEntry()
        '                End If
        '            End If
        '        End If
        '    End If

        'End Sub

        'Private Sub Button3_ClickBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles Button3.ClickBefore
        '    Try
        '        objform.Refresh()
        '        Matrix_Total()
        '        If EditText0.Value = "" Then
        '            objaddon.objapplication.SetStatusBarMessage("Please select from date...", SAPbouiCOM.BoMessageTime.bmt_Long, True)
        '            BubbleEvent = False : Exit Sub
        '        End If
        '        If Matrix0.VisualRowCount <= 1 Then
        '            If Matrix0.Columns.Item("empID").Cells.Item(1).Specific.String = "" Then
        '                objaddon.objapplication.SetStatusBarMessage("Line Data missing", SAPbouiCOM.BoMessageTime.bmt_Long, True)
        '                BubbleEvent = False : Exit Sub
        '            End If
        '        End If
        '        If Not ApprovedUser_Employee Then
        '            objaddon.objapplication.SetStatusBarMessage("You are not authorized to post JE", SAPbouiCOM.BoMessageTime.bmt_Long, True)
        '            BubbleEvent = False : Exit Sub
        '        End If
        '        If Not CheckBox0.Checked = True Then
        '            objaddon.objapplication.SetStatusBarMessage("Please Tick the Finalize", SAPbouiCOM.BoMessageTime.bmt_Long, True)
        '            BubbleEvent = False : Exit Sub
        '        End If
        '        If EditText5.Value <> "" Then
        '            objaddon.objapplication.SetStatusBarMessage("Joural Entry Posted for this entry", SAPbouiCOM.BoMessageTime.bmt_Long, True)
        '            BubbleEvent = False : Exit Sub
        '        End If
        '    Catch ex As Exception

        '    End Try

        'End Sub

        Private Sub Matrix0_DoubleClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.DoubleClickAfter

            'If pVal.ColUID = "salstat" Then
            'objform.Freeze(True)
            'For i As Integer = 1 To Matrix0.VisualRowCount
            '    'Matrix0.Columns.Item("Salstat").Cells.Item(i).checked = True
            '    Matrix0.Columns.Item("salstat").Cells.Item(i).Click(SAPbouiCOM.BoCellClickType.ct_Regular)
            'Next
            'objform.Freeze(False)

            'End If
            Try
                If pVal.ColUID = "salstat" Then
                    Dim tick As String = "N"
                    Try
                        objform.Freeze(True)
                        Matrix0.FlushToDataSource()
                        Dim odbdsDetails1 As SAPbouiCOM.DBDataSource
                        odbdsDetails1 = objform.DataSources.DBDataSources.Item("@MIPL_PPI1")
                        For rowNum As Integer = 0 To odbdsDetails1.Size - 1
                            If odbdsDetails1.GetValue("U_SalProcess", rowNum).Trim().ToUpper() = "N" Then
                                tick = "Y"
                                Exit For
                            End If
                        Next
                        For rowNum As Integer = 0 To odbdsDetails1.Size - 1
                            odbdsDetails1.SetValue("U_SalProcess", rowNum, tick)
                        Next
                        Matrix0.LoadFromDataSource()
                        ' If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE
                    Catch ex As Exception
                    Finally
                        objform.Freeze(False)
                    End Try
                End If
            Catch ex As Exception
                objform.Freeze(False)
            End Try
        End Sub

        Private Sub Button0_ClickBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles Button0.ClickBefore
            If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then Exit Sub
            'If CheckBox0.Checked = True And objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE Then
            '    objaddon.objapplication.SetStatusBarMessage("You are not permitted to Update the finalized document", SAPbouiCOM.BoMessageTime.bmt_Short, True)
            '    BubbleEvent = False : Exit Sub
            'End If
            If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                If EditText14.Value = "" Or EditText15.Value = "" Then
                    objaddon.objapplication.SetStatusBarMessage("You cannot submit the blank document", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If

                If Matrix0.RowCount = 0 Then
                    objaddon.objapplication.SetStatusBarMessage("Row Data Missing", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If
                'If EditText16.Value = "" Then
                '    objaddon.objapplication.SetStatusBarMessage("Please finalize the document & Post JE...", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                '    BubbleEvent = False : Exit Sub
                'End If
            End If

        End Sub

        'Private Sub CheckBox0_PressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles CheckBox0.PressedAfter
        '    If CheckBox2.Checked = True Then
        '        CheckBox2.Item.Enabled = False
        '    End If

        'End Sub

        Private Sub Form_DataLoadAfter(ByRef pVal As SAPbouiCOM.BusinessObjectInfo)
            Try
                If CheckBox2.Checked = True Then
                    CheckBox2.Item.Enabled = False
                Else
                    CheckBox2.Item.Enabled = True
                End If
                If EditText16.Value <> "" Then
                    Button8.Item.Enabled = False
                    Dim Value() As String = EditText16.Value.Split(vbTab)
                    Dim TEntry As String = objaddon.objglobalmethods.getSingleValue("Select T1.""TransId"" from OBTF T0 inner join OJDT T1 on T0.""BatchNum""=T1.""BatchNum"" where T0.""BatchNum""='" & Value(0) & "' and ifnull(T0.""BtfStatus"",'')='C'")
                    If TEntry <> "" Then
                        EditText16.Value = TEntry
                        If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE
                        objform.Items.Item("1").Click()
                    End If
                Else
                    Button8.Item.Enabled = True
                End If
                Button7.Item.Enabled = False
                Button4.Item.Enabled = True
                Matrix_Total()
                Matrix_Field_Setup()
                Matrix0.AutoResizeColumns()
                ' Matrix0.CommonSetting.SetCellEditable(Matrix0.RowCount, 2, True)
            Catch ex As Exception

            End Try


        End Sub

        Private Sub Matrix0_LinkPressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.LinkPressedAfter
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

        Friend Sub CreatePayslipPDF()
            Dim crzReport As New ReportDocument
            Dim sTempPath As String = Nothing, sDocOutPath As String = Nothing
            Dim sCreatePDFDebug As String = Nothing
            Dim CrzPdfOptions As New PdfFormatOptions
            Dim CrzExportOptions As New ExportOptions
            Dim CrzDiskFileDestinationOptions As New DiskFileDestinationOptions()
            Dim CrzFormatTypeOptions As New PdfRtfWordFormatOptions()
            Dim sReportOutName As String = Nothing, sReportOutSuffix As String = Nothing
            Dim sImageDocFiles As String() = Nothing
            Dim zFileInfo As IO.FileInfo = Nothing
            Dim bAlreadyExists As Boolean = False
            Dim bAExistsOverwrite As Boolean = False
            Dim bImagesMoved As Boolean = False
            Dim cnxInfo As ConnectionInfo = Nothing
            Dim Filename As String
            Dim Foldername As String
            Try
                'Filename = "D:\Chitra\HRMS\Rajesh\JAN13\ReportByVinod\New PaySlip.rpt"
                'Filename = "D:\Chitra\HRMS\Rajesh\JAN13\Report\New PaySlip.rpt"
                ' Filename = System.Windows.Forms.Application.StartupPath & "\Reports\New PaySlip.rpt"
                Dim objedit As SAPbouiCOM.EditText
                objedit = objform.Items.Item("tfrmdate").Specific
                Dim FDate As Date = Date.ParseExact(objedit.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Dim initialpath As String = objaddon.objglobalmethods.getSingleValue("select ""AttachPath"" from OADP")
                Foldername = initialpath & "Payslip PDF" & "\" & CStr(FDate.Year) & "\" & CStr(MonthName(FDate.Month, False)) & "\" & System.DateTime.Now.ToString("ddMMyyHHmmss")
                If Directory.Exists(Foldername) Then
                Else
                    Directory.CreateDirectory(Foldername)
                End If

                Filename = "\\newton.tmicloud.net\DB1SHARE\KANAKAVALLI_DB\Attachments\Payroll\RptFile\PaySlip1.rpt" 'System.Windows.Forms.Application.StartupPath & "\PaySlip1.rpt"
                ' create directory structure for output documents and path.
                crzReport.Load(Filename)
                'objaddon.objglobalmethods.WriteSMSLog(Filename)
                Dim crParameterFieldDefinitions As ParameterFieldDefinitions
                Dim crParameterFieldDefinition As ParameterFieldDefinition
                Dim crParameterValues As New ParameterValues
                Dim crParameterDiscreteValue As New ParameterDiscreteValue

                Dim IntYear As Integer
                Dim EmpId, Month, EmpName As String

                Dim crTable As Engine.Table
                Dim crTableLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
                Dim ConnInfo As New CrystalDecisions.Shared.ConnectionInfo
                ConnInfo.ServerName = objaddon.objcompany.Server
                ConnInfo.DatabaseName = objaddon.objcompany.CompanyDB
                ConnInfo.UserID = "KANASA"
                ConnInfo.Password = "R&s$!a#f%ru$456"

                For Each crTable In crzReport.Database.Tables
                    crTableLogonInfo = crTable.LogOnInfo
                    crTableLogonInfo.ConnectionInfo = ConnInfo
                    crTable.ApplyLogOnInfo(crTableLogonInfo)
                Next
                For i As Integer = 1 To Matrix0.VisualRowCount
                    EmpId = Matrix0.Columns.Item("empID").Cells.Item(i).Specific.string '"EMP005"
                    EmpName = Matrix0.Columns.Item("empName").Cells.Item(i).Specific.string
                    Month = MonthName(FDate.Month, False)
                    IntYear = FDate.Year
                    sDocOutPath = Foldername + "\" + EmpName + ".pdf"

                    crParameterDiscreteValue.Value = CStr(Month)
                    crParameterFieldDefinitions = crzReport.DataDefinition.ParameterFields()
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("Month")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)


                    crParameterDiscreteValue.Value = Convert.ToInt32(IntYear)
                    crParameterFieldDefinitions = crzReport.DataDefinition.ParameterFields()
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("Year@select year(current_date) from dummy union all select year(current_date)-1 from dummy union all select year(current_date)-2 from dummy")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                    crParameterDiscreteValue.Value = CStr(EmpId)
                    crParameterFieldDefinitions = crzReport.DataDefinition.ParameterFields()
                    crParameterFieldDefinition = crParameterFieldDefinitions.Item("Emp@select Distinct T1.""U_empID"",T1.""U_empName"" from ""@MIPL_PPI1"" T1 where ifnull(T1.""U_empID"",'')<>''")
                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    crParameterValues.Add(crParameterDiscreteValue)
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                    CrzDiskFileDestinationOptions.DiskFileName = sDocOutPath 'Set the destination path and file name
                    CrzExportOptions = crzReport.ExportOptions 'Set export options
                    With CrzExportOptions
                        .ExportDestinationType = ExportDestinationType.DiskFile ' DiskFile, ExchangeFolder, MicrosoftMail, NoDestination
                        .ExportFormatType = ExportFormatType.PortableDocFormat 'ExcelWorkBook, HTML32, HTML40, NoFormat, PDF, RichText, RTPR, TabSeperatedText, Text
                        .DestinationOptions = CrzDiskFileDestinationOptions
                        .FormatOptions = CrzFormatTypeOptions
                    End With
                    crzReport.Export()
                    crParameterFieldDefinition.CurrentValues.Clear()
                    'crParameterValues.Clear()
                Next
                objaddon.objapplication.StatusBar.SetText("PDF Files are generated Successfully...", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
            Catch exc As Exception
                MsgBox("Error in CreatePDF:" & vbCrLf & exc.ToString)
            Finally
                crzReport.Close()
                crzReport.Dispose()
                CrzPdfOptions = Nothing
                CrzExportOptions = Nothing
                CrzDiskFileDestinationOptions = Nothing
                CrzFormatTypeOptions = Nothing
            End Try

        End Sub

        Public Sub Payslip_AutoEmail_Updated_Old()
            Try
                Dim FromMail_id As String = "", FromMail_Password As String = "", Mail_Host As String = "", Mail_Port As String = ""
                Dim strquery, Foldername, Filename As String
                Dim objrs As SAPbobsCOM.Recordset
                Dim objrsupdate As SAPbobsCOM.Recordset
                Dim Mailbody, ServerName, CompanyDb, DBUserName, DbPassword As String
                'Dim Payroll_Report_FileName = System.Windows.Forms.Application.StartupPath & "\" & "PaySlip_YMH.rpt"

                Dim Payroll_Report_FileName = System.Windows.Forms.Application.StartupPath & "\" & "PaySlip_OEC.rpt"
                Dim initialpath As String = objaddon.objglobalmethods.getSingleValue("select ""AttachPath"" from OADP")
                Foldername = initialpath + "Payroll\RptFile"
                If Directory.Exists(Foldername) Then
                Else
                    Directory.CreateDirectory(Foldername)
                End If
                Filename = Foldername & "\PaySlip1.rpt"

                ServerName = "WATSON.TMICLOUD.NET:30013" '"WAT@WATSON.TMICLOUD.NET:30013"
                CompanyDb = "KANAKAVALLI_LIVE"
                DBUserName = "KANASA" '"OECDBBR"
                DbPassword = "R&s$!a#f%ru$456" ' "India@1947"

                'ServerName = "WATSON.TMICLOUD.NET:30013" '"WAT@WATSON.TMICLOUD.NET:30013"
                'CompanyDb = "OEC_TEST"
                'DBUserName = "OECDBBR"
                'DbPassword = "India@1947"
                'strsql = " Select T0.DocEntry,Datepart(MM,T0.U_fromdate)[Month],Datepart(yyyy,T0.U_Fromdate)[Year],DateName(Month,T0.U_fromdate)+' - '+Convert(varchar,Datepart(yyyy,T0.U_Fromdate))[Period],"
                'strsql += vbCrLf + " T2.U_empid[Empid],T2.U_ExtEmpNo,isnull(T2.U_firstNam,'')+' '+isnull(T2.U_lastName,'')[ToName],isnull(T2.U_Email,'')[ToEmail],'N'[OTTA]"
                'strsql += vbCrLf + " from [@SMPR_OPRC] T0 inner join [@SMPR_PRC1] T1 on T0.DOcentry=T1.DocEntry Inner join [@SMPR_OHEM] T2 on T1.U_Empid=T2.U_empid"
                'strsql += vbCrLf + " Where T0.U_Fromdate=(Select Max(U_Fromdate) from [@SMPR_OPRC] Where U_process='Y') and isnull(T2.U_payslip,'')='Y' and isnull(T2.U_Email,'')<>''"
                'strsql += vbCrLf + " and isnull(T1.U_payslip,'N')='N' and isnull(T0.U_Apayslip,'N')='Y'"

                strquery = "Select T0.""DocEntry"",MONTHNAME(T0.""U_FromDate"") ""MonthName"", MONTH(T0.""U_FromDate"") AS ""Month"", YEAR(T0.""U_FromDate"") AS ""Year"", MONTH(T0.""U_FromDate"") || ' - ' || CAST(YEAR(T0.""U_FromDate"") AS varchar) AS ""Period"","
                strquery += vbCrLf + " T2.""U_empID"" AS ""Empid"", T2.""U_ExtEmpNo"", IFNULL(T2.""U_firstNam"", '') || ' ' || IFNULL(T2.""U_lastName"", '') AS ""ToName"", IFNULL(T2.""U_email"", '') AS ""ToEmail"","
                strquery += vbCrLf + " 'N' AS ""OTTA"" FROM ""@SMPR_OPRC"" T0 INNER JOIN ""@SMPR_PRC1"" T1 ON T0.""DocEntry"" = T1.""DocEntry"" INNER Join ""@SMPR_OHEM"" T2 ON T1.""U_empID"" = T2.""U_empID"" "
                strquery += vbCrLf + " WHERE T0.""U_FromDate"" = (SELECT MAX(""U_FromDate"") FROM ""@SMPR_OPRC"" WHERE ""U_Process"" = 'Y') AND IFNULL(T2.""U_PaySlip"", '') = 'Y' "
                strquery += vbCrLf + " And IFNULL(T2.""U_email"", '') <> '' AND IFNULL(T1.""U_PaySlip"", 'N') = 'N' AND IFNULL(T0.""U_APayslip"", 'N') = 'Y'"


                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strquery)
                If objrs.RecordCount = 0 Then Exit Sub
                FromMail_id = "saptech18@mukeshinfoserve.com"
                FromMail_Password = "D@rloo@30895"
                Mail_Host = "smtp-mail.outlook.com"
                Mail_Port = "587"
                If FromMail_id = "" Or FromMail_Password = "" Or Mail_Host = "" Or Mail_Port = "" Then Exit Sub
                'MsgBox(Payroll_Report_FileName)   
                Dim cryRpt As New ReportDocument
                cryRpt.Load(Payroll_Report_FileName)
                cryRpt.DataSourceConnections(0).SetConnection(Trim(ServerName), Trim(CompanyDb), False)
                cryRpt.DataSourceConnections(0).SetLogon(Trim(DBUserName), Trim(DbPassword))


                For i As Integer = 0 To objrs.RecordCount - 1
                    If objrs.Fields.Item("ToEmail").Value.ToString = "" Then Continue For

                    Dim Email As New System.Net.Mail.MailMessage
                    Dim MailServer As New System.Net.Mail.SmtpClient()

                    Try
                        MailServer.Host = Mail_Host
                        MailServer.Port = Mail_Port
                        MailServer.Credentials = New System.Net.NetworkCredential(FromMail_id.ToString.Trim, FromMail_Password.ToString.Trim)
                        MailServer.EnableSsl = True
                        Email.From = New System.Net.Mail.MailAddress(FromMail_id.ToString.Trim)

                        Email.To.Add(New System.Net.Mail.MailAddress(objrs.Fields.Item("ToEmail").Value.ToString))
                        Email.Subject = "Pay Slip - " & objrs.Fields.Item("ToName").Value.ToString & " - " & objrs.Fields.Item("MonthName").Value.ToString & " - " & objrs.Fields.Item("Year").Value.ToString

                        Mailbody = "Dear " & objrs.Fields.Item("ToName").Value.ToString & ","
                        Mailbody += vbCrLf + " "
                        Mailbody += vbCrLf + " Please Find the Attached Payslip for the Month of " & objrs.Fields.Item("MonthName").Value.ToString & " - " & objrs.Fields.Item("Year").Value.ToString & "."
                        Mailbody += vbCrLf + " "
                        Mailbody += vbCrLf + "With Regards,"
                        Mailbody += vbCrLf + "HR Team"
                        Mailbody += vbCrLf + " "
                        Mailbody += vbCrLf + " "
                        Mailbody += "This is Auto generated E-Mail from SAP Business One . Please do not reply to this message. Thank you! "

                        Email.Body = Mailbody
                        Email.Priority = Net.Mail.MailPriority.High

                        'cryRpt.SetParameterValue("Emp@select empid,FIRSTNAME+'  '+LASTNAME from ohem order by Firstname", objrs.Fields.Item("Empid").Value.ToString)
                        'cryRpt.SetParameterValue("Month", objrs.Fields.Item("Month").Value.ToString)
                        'cryRpt.SetParameterValue("year@select distinct year(T0.u_todate) year from [@SMPR_OPRC] T0", objrs.Fields.Item("Year").Value.ToString)
                        'cryRpt.SetParameterValue("OTTA", "N")

                        cryRpt.SetParameterValue("Month", CStr(objrs.Fields.Item("MonthName").Value.ToString))
                        cryRpt.SetParameterValue("Year@select year(current_date) from dummy union all select year(current_date)-1 from dummy union all select year(current_date)-2 from dummy", Convert.ToInt32(objrs.Fields.Item("Year").Value.ToString))
                        cryRpt.SetParameterValue("Emp@select Distinct T1.""U_IDNo"",T1.""U_empName"" from ""@SMPR_PRC1"" T1 where ifnull(T1.""U_IDNo"",'')<>''", CStr(Trim(objrs.Fields.Item("U_ExtEmpNo").Value.ToString)))
                        'cryRpt.SetParameterValue("Emp@select Distinct T1.""U_empID"",T1.""U_empName"" from ""@MIPL_PPI1"" T1 where ifnull(T1.""U_empID"",'')<>''", objrs.Fields.Item("U_ExtEmpNo").Value.ToString) 'objrs.Fields.Item("U_ExtEmpNo").Value.ToString
                        'Dim Foldername, sDocOutPath As String
                        'Foldername = System.Windows.Forms.Application.StartupPath & "Payslip PDF"
                        'sDocOutPath = Foldername + "\" + Trim(objrs.Fields.Item("U_ExtEmpNo").Value.ToString) + ".pdf"
                        'cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, sDocOutPath)
                        Email.Attachments.Add(New Attachment(cryRpt.ExportToStream(ExportFormatType.PortableDocFormat), "Pay Slip - " & objrs.Fields.Item("ToName").Value.ToString & " - " & objrs.Fields.Item("MonthName").Value.ToString & " - " & objrs.Fields.Item("Year").Value.ToString & ".PDF"))
                        'cryRpt.Dispose()
                        cryRpt.Refresh()
                        MailServer.Send(Email)
                        Dim strsql As String
                        strsql = "Update ""@SMPR_PRC1"" set ""U_PaySlip""='Y' where ""DocEntry""='" & objrs.Fields.Item("DocEntry").Value.ToString & "' and ""U_empID""='" & objrs.Fields.Item("Empid").Value.ToString & "'"
                        objrsupdate = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                        objrsupdate.DoQuery(strsql)

                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    Finally
                        If Not Email Is Nothing Then Email.Dispose()
                        MailServer = Nothing
                    End Try
                    objrs.MoveNext()
                Next
                MsgBox("Mail Sent")
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

        End Sub

        Private Function PDFCreationUpdated() As Boolean
            Dim cryRpt As New ReportDocument
            Dim DBUserName As String = "", Filename As String = "" ' "KADMIN" KANASA
            Dim DbPassword As String = "" '"India@1947"'R&s$!a#f%ru$456
            Dim EmpId, Month, EmpName As String
            Dim IntYear As Integer
            Dim Foldername, sDocOutPath As String
            Dim Flag As Boolean = False
            Try
                Dim initialpath As String = objaddon.objglobalmethods.getSingleValue("select ""AttachPath"" from OADP")
                Foldername = initialpath + "Payroll\RptFile"
                If Directory.Exists(Foldername) Then
                Else
                    Directory.CreateDirectory(Foldername)
                End If
                Filename = Foldername & "\PaySlip.rpt" ' System.Windows.Forms.Application.StartupPath & "\PaySlip1.rpt"
                'Filename = "E:\Chitra\Common Payroll\Dec 16\BackUp Source Payroll\April 27 2022\HRMS_Common\HRMS\bin\x64\Debug\PaySlip1.rpt"
                Dim FDate As Date = Date.ParseExact(EditText14.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                cryRpt.Load(Filename)
                If CStr(ComboBox6.Selected.Description) Is Nothing Then
                    Foldername = initialpath & "Payslip PDF" & "\" & CStr(FDate.Year) & "\" & CStr(MonthName(FDate.Month, False)) & "\" & System.DateTime.Now.ToString("ddMMyyHHmmss")
                Else
                    Foldername = initialpath & "Payslip PDF" & "\" & CStr(FDate.Year) & "\" & CStr(MonthName(FDate.Month, False)) & "\" & CStr(ComboBox6.Selected.Description) 'System.DateTime.Now.ToString("ddMMyyHHmmss")
                End If

                If Directory.Exists(Foldername) Then
                Else
                    Directory.CreateDirectory(Foldername)
                End If
                DBUserName = objaddon.objglobalmethods.getSingleValue("select ""U_DBUser"" from OADM")
                DbPassword = objaddon.objglobalmethods.getSingleValue("select ""U_DBPass"" from OADM")
                'Dim ass As String = objaddon.objcompany.Server
                cryRpt.DataSourceConnections(0).SetConnection(objaddon.objcompany.Server, objaddon.objcompany.CompanyDB, False)
                'cryRpt.DataSourceConnections(0).SetConnection("WATSON.TMICLOUD.NET:30015", "KANAKAVALLI_DB", True)
                cryRpt.DataSourceConnections(0).SetLogon(Trim(DBUserName), Trim(DbPassword))
                'cryRpt.DataSourceConnections(0).SetLogon("KANASA", "R&s$!a#f%ru$456")
                'cryRpt.SetDatabaseLogon("KANASA", "R&s$!a#f%ru$456", "WATSON.TMICLOUD.NET:30015", "KANAKAVALLI_DB")
                'Dim sss As String = cryRpt.FileName
                'cryRpt.ReadRecords()
                Try
                    cryRpt.Refresh()
                    cryRpt.VerifyDatabase()
                Catch ex As Exception
                End Try

                'cryRpt.ParameterFields.Clear()
                For i As Integer = 1 To Matrix0.VisualRowCount
                    EmpId = Matrix0.Columns.Item("empID").Cells.Item(i).Specific.string '"EMP005"
                    EmpName = Matrix0.Columns.Item("empName").Cells.Item(i).Specific.string
                    sDocOutPath = Foldername + "\" + EmpName + " -Payslip for the Month of " + CStr(MonthName(FDate.Month, False)) + " " + CStr(FDate.Year) + ".pdf"
                    Month = MonthName(FDate.Month, False)
                    IntYear = FDate.Year
                    cryRpt.SetParameterValue("Month", CStr(Month))
                    cryRpt.SetParameterValue("Year@select year(current_date) from dummy union all select year(current_date)-1 from dummy union all select year(current_date)-2 from dummy", Convert.ToInt32(IntYear))
                    cryRpt.SetParameterValue("Emp@select Distinct T1.""U_empID"",T1.""U_empName"" from ""@MIPL_PPI1"" T1 where ifnull(T1.""U_empID"",'')<>''", EmpId)
                    'cryRpt.ExportToStream(ExportFormatType.PortableDocFormat)
                    cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, sDocOutPath)
                    'cryRpt.Refresh()

                    Flag = True
                Next

            Catch ex As Exception
                Flag = False
                objaddon.objapplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
            Finally
                cryRpt.Close()
                cryRpt.Dispose()
            End Try
            Return Flag
        End Function

        Public Shared Sub CreatePDF()
            Try
                Dim FileFound As Boolean = False
                Dim InvoicePath, Foldername, Filename As String
                Dim InvoiceFileName As String = String.Empty
                Dim SAP_Server As String = "WATSON.TMICLOUD.NET:30015"
                Dim SAP_DBUID As String = "KANASA"
                Dim SAP_DBPass As String = "R&s$!a#f%ru$456"
                Dim SAP_DBName As String = "KANAKAVALLI_DB"
                'InvoiceFileName = String.Format("Invoice_{0}", InvoiceNo) & ".pdf"
                Dim CRRpt As ReportDocument = New ReportDocument()
                'InvoicePath = String.Concat(ReportPath, "\PDF-Invoices\", InvoiceFileName)
                'CRRpt.Load(ReportPath & "\D4W-Invoice-2.rpt")
                Dim initialpath As String = objaddon.objglobalmethods.getSingleValue("select ""AttachPath"" from OADP")
                Foldername = initialpath + "Payroll\RptFile"
                If Directory.Exists(Foldername) Then
                Else
                    Directory.CreateDirectory(Foldername)
                End If
                Filename = Foldername & "\PaySlip1.rpt"
                CRRpt.Load(Filename)
                InvoicePath = Foldername + "\" + "CHITHU" + ".pdf"
                Dim strConnection As String = String.Format("DRIVER={0};UID={1};PWD={2};SERVERNODE={3};DATABASE={4};", "{B1CRHPROXY32}", SAP_DBUID, SAP_DBPass, SAP_Server, SAP_DBName)
                Dim logonProps2 As NameValuePairs2 = CRRpt.DataSourceConnections(0).LogonProperties
                logonProps2.[Set]("Provider", "B1CRHPROXY32")
                logonProps2.[Set]("Server Type", "B1CRHPROXY32")
                logonProps2.[Set]("Connection String", strConnection)
                CRRpt.DataSourceConnections(0).SetLogonProperties(logonProps2)
                CRRpt.DataSourceConnections(0).SetConnection(SAP_Server, SAP_DBName, False)
                CRRpt.SetParameterValue("Month", "December")
                CRRpt.SetParameterValue("Year@select year(current_date) from dummy union all select year(current_date)-1 from dummy union all select year(current_date)-2 from dummy", 2022)
                CRRpt.SetParameterValue("Emp@select Distinct T1.""U_empID"",T1.""U_empName"" from ""@MIPL_PPI1"" T1 where ifnull(T1.""U_empID"",'')<>''", "EMP/KVL/00064")

                'CRRpt.SetParameterValue("DocKey@", DocEntry)
                'CRRpt.SetParameterValue("ObjectId@", Integer.Parse("13"))
                CRRpt.ExportToDisk(ExportFormatType.PortableDocFormat, InvoicePath)
                CRRpt.Close()
                CRRpt.Dispose()
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Button4_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button4.ClickAfter
            'Send_Mail_toEmployee()
            Try
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then Exit Sub
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Or objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE Then
                    objaddon.objapplication.SetStatusBarMessage("Generating PDF files Please wait...", SAPbouiCOM.BoMessageTime.bmt_Long, False)
                    'CreatePayslipPDF()
                    'Payslip_AutoEmail()
                    'CreatePDF()

                    If PDFCreationUpdated() = True Then
                        objaddon.objapplication.StatusBar.SetText("PDF Files generated Successfully...", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                    Else
                        objaddon.objapplication.StatusBar.SetText("Error While Creating PDF Files...", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                    End If
                End If
            Catch ex As Exception
                objaddon.objapplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
            End Try


        End Sub

        Private Sub Matrix0_ValidateAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.ValidateAfter
            'Try
            '    Dim PF As Double, ESI As Double, LOAN As Double = 0, NetSal, ProfTax As Double
            '    Dim Bonus, Incentive, Addition, TDSNew, Deduct, GrossSal As Double
            '    objaddon.objapplication.Menus.Item("1300").Activate()
            '    TDSNew = CDbl(Matrix0.Columns.Item("TDS").Cells.Item(pVal.Row).Specific.string)
            '    LOAN = CDbl(Matrix0.Columns.Item("Loan").Cells.Item(pVal.Row).Specific.string)
            '    Bonus = CDbl(Matrix0.Columns.Item("Bonus").Cells.Item(pVal.Row).Specific.string)
            '    Incentive = CDbl(Matrix0.Columns.Item("Incent").Cells.Item(pVal.Row).Specific.string)
            '    Deduct = Matrix0.Columns.Item("TotDed").Cells.Item(pVal.Row).Specific.string
            '    NetSal = CDbl(Matrix0.Columns.Item("NetSal").Cells.Item(pVal.Row).Specific.string)
            '    GrossSal = CDbl(Matrix0.Columns.Item("GrossSal").Cells.Item(pVal.Row).Specific.string)
            '    PF = CDbl(Matrix0.Columns.Item("PF").Cells.Item(pVal.Row).Specific.string)
            '    ESI = CDbl(Matrix0.Columns.Item("ESI").Cells.Item(pVal.Row).Specific.string)
            '    ProfTax = CDbl(Matrix0.Columns.Item("ProfTax").Cells.Item(pVal.Row).Specific.string)
            '    Try

            '        If pVal.ItemChanged = True And pVal.ActionSuccess = True Then
            '            Addition = Bonus + Incentive
            '            Matrix0.Columns.Item("TotAdd").Cells.Item(pVal.Row).Specific.string = Addition
            '            Matrix0.Columns.Item("TotDed").Cells.Item(pVal.Row).Specific.string = CDbl(PF + ESI + ProfTax + LOAN + TDSNew)
            '            'Matrix0.Columns.Item("NetSal").Cells.Item(pVal.Row).Specific.string = CDbl(NetSal - LOAN)
            '            Matrix0.Columns.Item("Payable").Cells.Item(pVal.Row).Specific.string = CDbl((NetSal + (Bonus + Incentive)) - (LOAN + TDSNew))
            '            'Matrix0.Columns.Item("NetSal").Cells.Item(pVal.Row).Specific.string = CDbl((GrossSal - PF - ESI - oProfTax - TDSNew) - LOAN)
            '            'Matrix0.Columns.Item("Payable").Cells.Item(pVal.Row).Specific.string = CDbl((GrossSal - (PF + ESI + oProfTax + TDSNew + LOAN)) + (Bonus + Incentive))
            '        End If
            '        'Select Case pVal.ColUID
            '        '    Case "TDS"
            '        '        Matrix0.Columns.Item("NetSal").Cells.Item(pVal.Row).Specific.string = CDbl((GrossSal - PF - ESI - oProfTax) - TDSNew)
            '        '    Case "Loan"

            '        '    Case "Bonus"
            '        '        Matrix0.Columns.Item("Payable").Cells.Item(pVal.Row).Specific.string = CDbl((GrossSal - PF - ESI - oProfTax - TDSNew - LOAN) + Bonus)
            '        '    Case "Incent"

            '        'End Select
            '    Catch ex As Exception
            '    End Try
            'Catch ex As Exception
            'End Try

        End Sub

        Private Sub Button7_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button7.ClickAfter
            Try
                If objform.Mode <> SAPbouiCOM.BoFormMode.fm_ADD_MODE Then Exit Sub
                Dim FDate As Date = Date.ParseExact(EditText14.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Dim TDate As Date = Date.ParseExact(EditText15.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                If objaddon.objapplication.MessageBox("Do you want to Calculate Payroll?", 2, "Yes", "No") <> 1 Then Exit Sub
                objaddon.objapplication.SetStatusBarMessage("Calculating Payroll Details. Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Long, False)
                objform.Freeze(True)
                odbdsDetails = objform.DataSources.DBDataSources.Item(CType(1, Object))
                odbdsDetails.Clear()
                Matrix0.LoadFromDataSource()

                If MultiBranch = "Y" Then
                    strsql = "CALL ""MIPL_PayrollCalculationUpdated"" ('" & ComboBox5.Selected.Value & "'"
                    If Not ComboBox6.Selected Is Nothing Then
                        If ComboBox6.Selected.Value = "-1" Then strsql += " ,'')" Else strsql += " ,'" & ComboBox6.Selected.Value & "','')"
                    Else
                        strsql += " ,'','')"
                    End If
                Else
                    strsql = "CALL ""MIPL_PayrollCalculationUpdated"" ('" & ComboBox5.Selected.Value & "','-1'"
                    If Not ComboBox0.Selected Is Nothing Then
                        If ComboBox0.Selected.Value = "-1" Then strsql += " ,'')" Else strsql += " ,'" & ComboBox0.Selected.Value & "')"
                    Else
                        strsql += " ,'')"
                    End If
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
                    odbdsDetails.SetValue("U_WoDays", i, objrs.Fields.Item("WoDays").Value.ToString)
                    'odbdsDetails.SetValue("U_Basic", i, objrs.Fields.Item("TotalBasic").Value.ToString)
                    'odbdsDetails.SetValue("U_HRA", i, objrs.Fields.Item("HRA").Value.ToString)
                    odbdsDetails.SetValue("U_PF", i, objrs.Fields.Item("PF").Value.ToString)
                    odbdsDetails.SetValue("U_EmpPF", i, objrs.Fields.Item("EPF").Value.ToString)
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

                    odbdsDetails.SetValue("U_EmpCont", i, CDbl(objrs.Fields.Item("EPF").Value.ToString) + CDbl(objrs.Fields.Item("EmployerESI").Value.ToString))
                    odbdsDetails.SetValue("U_GrossSal", i, objrs.Fields.Item("GrossSalary").Value.ToString)
                    odbdsDetails.SetValue("U_TDS", i, objrs.Fields.Item("TDS").Value.ToString)
                    odbdsDetails.SetValue("U_ProfTax", i, objrs.Fields.Item("PT").Value.ToString)
                    odbdsDetails.SetValue("U_LOPDays", i, objrs.Fields.Item("LOPDays").Value.ToString)
                    odbdsDetails.SetValue("U_LateHrs", i, objrs.Fields.Item("LateHrs").Value.ToString)
                    odbdsDetails.SetValue("U_Loan", i, objrs.Fields.Item("Loan").Value.ToString)
                    odbdsDetails.SetValue("U_TotHrs", i, objrs.Fields.Item("TotHrs").Value.ToString)
                    odbdsDetails.SetValue("U_WorkHrs", i, objrs.Fields.Item("WorkedHrs").Value.ToString)
                    odbdsDetails.SetValue("U_DaySal", i, objrs.Fields.Item("DaySalary").Value.ToString)
                    odbdsDetails.SetValue("U_HrSal", i, objrs.Fields.Item("HrSalary").Value.ToString)  '
                    odbdsDetails.SetValue("U_TotDed", i, objrs.Fields.Item("TotalDeduction").Value.ToString)
                    odbdsDetails.SetValue("U_NetSal", i, objrs.Fields.Item("NetSalary").Value.ToString)
                    odbdsDetails.SetValue("U_Payable", i, objrs.Fields.Item("NetSalary").Value.ToString)
                    odbdsDetails.SetValue("U_LopHrs", i, objrs.Fields.Item("LOPHrs").Value.ToString)

                    odbdsDetails.SetValue("U_PayLeave", i, objrs.Fields.Item("PayLeave").Value.ToString)
                    odbdsDetails.SetValue("U_CODays", i, objrs.Fields.Item("CompOffBalance").Value.ToString)
                    odbdsDetails.SetValue("U_ELBal", i, objrs.Fields.Item("ELBalance").Value.ToString)
                    'odbdsDetails.SetValue("U_ELDays", i, objrs.Fields.Item("CarryFwdLv").Value.ToString)
                    odbdsDetails.SetValue("U_LeaveBal", i, objrs.Fields.Item("LeaveBal").Value.ToString)
                    odbdsDetails.SetValue("U_LeaveTak", i, objrs.Fields.Item("LeaveTaken").Value.ToString)
                    odbdsDetails.SetValue("U_shifthrs", i, objrs.Fields.Item("ShiftHrs").Value.ToString)
                    'odbdsDetails.SetValue("U_ELTaken", i, objrs.Fields.Item("ELLIGIBLELVDAYS").Value.ToString)
                    odbdsDetails.SetValue("U_RoundOff", i, objrs.Fields.Item("RoundOff").Value.ToString)
                    objrs.MoveNext()

                    If i <> objrs.RecordCount - 1 Then odbdsDetails.InsertRecord(odbdsDetails.Size)
                Next

                Matrix0.LoadFromDataSource()
                Matrix0.CommonSetting.FixedColumnsCount = 5
                objaddon.objapplication.Menus.Item("1300").Activate() 'Fit colum width
                Matrix_Field_Setup()
                'TaxCalculation()
                Matrix_Total()
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
                objaddon.objapplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
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
                Matrix0.Columns.Item("U_AB1").Visible = False
                Matrix0.Columns.Item("U_AB2").Visible = False
                Matrix0.Columns.Item("U_AB3").Visible = False
                Matrix0.Columns.Item("U_AB4").Visible = False
                Matrix0.Columns.Item("U_AB5").Visible = False
                Matrix0.Columns.Item("U_DB1").Visible = False
                Matrix0.Columns.Item("U_DB2").Visible = False
                Matrix0.Columns.Item("U_DB3").Visible = False
                Matrix0.Columns.Item("U_DB4").Visible = False
                Matrix0.Columns.Item("U_DB5").Visible = False
                ''Matrix0.Columns.Item("basic").Visible = False
                ''Matrix0.Columns.Item("totsal").Visible = False

                'strsql = "select 'U_'||""U_Sequence"" ""ColName"",""Name"" from ""@SMPR_OPYE"" Where ""U_Type""='S' and ifnull(""U_Active"",'')='Y' "
                strsql = "select 'U_'||""U_Sequence"" ""ColName"",""Name"",""U_Type"" ""Type"" from ""@SMPR_OPYE"" Where  ifnull(""U_Active"",'')='Y'"
                strsql += vbCrLf + "and UPPER(""Name"") not in ('PROFESSIONAL TAX','EMPLOYER PF','EMPLOYER ESI','PROVIDENT FUND','EMPLOYEE STATE INSURANCE',"
                strsql += vbCrLf + "'INCENTIVE','BONUS','TAX DEDUCTED SOURCE','LOAN','SALARY PAYABLE')"
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strsql)
                Dim D, A As String
                If objrs.RecordCount > 0 Then
                    For i As Integer = 0 To objrs.RecordCount - 1

                        If objrs.Fields.Item("Type").Value.ToString = "D" Then
                            D += 1
                            Matrix0.Columns.Item("U_DB" + CStr(D)).Visible = True
                            Matrix0.Columns.Item("U_DB" + CStr(D)).TitleObject.Caption = objrs.Fields.Item("Name").Value.ToString
                            Matrix0.Columns.Item("U_DB" + CStr(D)).RightJustified = True
                            Matrix0.Columns.Item("U_DB" + CStr(D)).ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                        ElseIf objrs.Fields.Item("Type").Value.ToString = "A" Then
                            A += 1
                            Dim ii As String = "U_AB" + CStr(A)
                            Matrix0.Columns.Item("U_AB" + CStr(A)).Visible = True
                            Matrix0.Columns.Item("U_AB" + CStr(A)).TitleObject.Caption = objrs.Fields.Item("Name").Value.ToString
                            Matrix0.Columns.Item("U_AB" + CStr(A)).RightJustified = True
                            Matrix0.Columns.Item("U_AB" + CStr(A)).ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                        Else
                            Matrix0.Columns.Item(objrs.Fields.Item("ColName").Value.ToString).Visible = True
                            Matrix0.Columns.Item(objrs.Fields.Item("ColName").Value.ToString).TitleObject.Caption = objrs.Fields.Item("Name").Value.ToString
                            Matrix0.Columns.Item(objrs.Fields.Item("ColName").Value.ToString).RightJustified = True
                            Matrix0.Columns.Item(objrs.Fields.Item("ColName").Value.ToString).ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                        End If
                        objrs.MoveNext()
                    Next
                End If
                Matrix0.CommonSetting.FixedColumnsCount = 9

                objaddon.objapplication.Menus.Item("1300").Activate()
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Button7_ClickBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button7.ClickBefore
            Try
                If ComboBox5.Value = "" Then
                    objaddon.objapplication.SetStatusBarMessage("Pay Period is Missing", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If
                If MultiBranch = "Y" Then
                    If ComboBox6.Value = "" Or ComboBox6.Selected Is Nothing Then
                        objaddon.objapplication.SetStatusBarMessage("Branch is Missing", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                        BubbleEvent = False : Exit Sub
                    End If
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Button8_ClickBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button8.ClickBefore
            Try
                If Button8.Item.Enabled = False Then BubbleEvent = False : Exit Sub
                If Not ApprovedUser_Employee Then
                    objaddon.objapplication.SetStatusBarMessage("You are not authorized to post JE", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                    BubbleEvent = False : Exit Sub
                End If
                If ComboBox5.Value = "" Then
                    objaddon.objapplication.SetStatusBarMessage("Please select the Pay Period...", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                    BubbleEvent = False : Exit Sub
                End If
                If Matrix0.VisualRowCount <= 1 Then
                    If Matrix0.Columns.Item("empID").Cells.Item(1).Specific.String = "" Then
                        objaddon.objapplication.SetStatusBarMessage("Line Data is missing", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                        BubbleEvent = False : Exit Sub
                    End If
                End If
                If Not CheckBox2.Checked = True Then
                    objaddon.objapplication.SetStatusBarMessage("Please Finalize the document...", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                    BubbleEvent = False : Exit Sub
                End If
                If objaddon.objapplication.MessageBox("Do you want to post the Journal Voucher?", 2, "Yes", "No") <> 1 Then BubbleEvent = False : Exit Sub

                If EditText16.Value <> "" Then
                    objaddon.objapplication.SetStatusBarMessage("Journal Transaction Posted for this document...", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                    BubbleEvent = False : Exit Sub
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Button8_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button8.ClickAfter
            Try
                If Button8.Item.Enabled = False Then Exit Sub
                If objform.Mode <> SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                    If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE
                    ApprovedUser_Employee = objaddon.ApprovedUser()
                    If CheckBox2.Checked = True Then
                        If ApprovedUser_Employee Then
                            If EditText16.Value = "" Then
                                Payroll_JournalVoucher()
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
            End Try

        End Sub

        'Public Class JETransaction
        '    Public Amount As Double
        '    Public Type As String
        'End Class

        'Public Class JVTransaction
        '    Public Amount As Double
        '    Public ElementCode As String
        '    Public ElementName As String
        'End Class

        'Private Function gettranforJE(ByVal value As String)
        '    Try
        '        Dim recsetvalue As String = ""
        '        Select Case value
        '            Case "TotGross"
        '                value = "Debit"
        '                recsetvalue = "6"
        '            Case "TotBonus"
        '                value = "Debit"
        '                recsetvalue = "8"
        '            Case "TotIncent"
        '                value = "Debit"
        '                recsetvalue = "7"
        '            Case "TotPF"
        '                value = "Credit"
        '                recsetvalue = "0"
        '            Case "TotESI"
        '                value = "Credit"
        '                recsetvalue = "2"
        '            Case "TotPT"
        '                value = "Credit"
        '                recsetvalue = "1"
        '            Case "TotTDS"
        '                value = "Credit"
        '                recsetvalue = "3"
        '            Case "TotLoan"
        '                value = "Credit"
        '                recsetvalue = "4"
        '            Case "TotSalPayable"
        '                value = "Credit"
        '                recsetvalue = "5"
        '            Case "TotEmployerPF"
        '                value = "Credit"
        '                recsetvalue = "9"
        '            Case "TotEmployerESI"
        '                value = "Credit"
        '                recsetvalue = "10"
        '            Case "TotEmployerExpenses"
        '                value = "Debit"
        '                recsetvalue = "11"
        '            Case Else
        '                value = Nothing
        '        End Select
        '        Return {value, recsetvalue}
        '    Catch ex As Exception

        '    End Try
        'End Function

        'Public Sub IN_JournalEntry()
        '    Dim objjournalentry As SAPbobsCOM.JournalEntries
        '    Dim DocEntry As String
        '    Dim Flag As Boolean = False

        '    objjournalentry = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries)
        '    strsql = "CALL ""MIPL_GetJEAccount_Details"" "            '
        '    objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        '    objrs.DoQuery(strsql)
        '    If objrs.RecordCount = 0 Then objaddon.objapplication.StatusBar.SetText("Please update the GL Master...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error) : Exit Sub
        '    Dim TotGross As Double = 0, TotEncash As Double = 0, TotBonus As Double = 0, TotIncent As Double = 0
        '    Dim TotPF As Double = 0, TotEmployerPF As Double = 0, TotEmployerESI As Double = 0, TotESI As Double = 0, TotPT As Double = 0, TotTDS As Double = 0, TotLoan As Double = 0, TotSalPayable As Double = 0
        '    For i As Integer = 1 To Matrix0.VisualRowCount
        '        If Matrix0.Columns.Item("salstat").Cells.Item(i).Specific.Checked = True Then
        '            TotGross += CDbl(Matrix0.Columns.Item("GrossSal").Cells.Item(i).Specific.string)
        '            ' TotEncash += CDbl(Matrix0.Columns.Item("Encash").Cells.Item(i).Specific.string)
        '            TotBonus += CDbl(Matrix0.Columns.Item("Bonus").Cells.Item(i).Specific.string)
        '            TotIncent += CDbl(Matrix0.Columns.Item("Incent").Cells.Item(i).Specific.string)
        '            TotPF += CDbl(Matrix0.Columns.Item("PF").Cells.Item(i).Specific.string)
        '            TotESI += CDbl(Matrix0.Columns.Item("ESI").Cells.Item(i).Specific.string)
        '            TotPT += CDbl(Matrix0.Columns.Item("ProfTax").Cells.Item(i).Specific.string)
        '            TotTDS += CDbl(Matrix0.Columns.Item("TDS").Cells.Item(i).Specific.string)
        '            TotLoan += CDbl(Matrix0.Columns.Item("Loan").Cells.Item(i).Specific.string)
        '            TotSalPayable += CDbl(Matrix0.Columns.Item("Payable").Cells.Item(i).Specific.string)
        '            TotEmployerPF += CDbl(Matrix0.Columns.Item("EPF").Cells.Item(i).Specific.string)
        '            TotEmployerESI += CDbl(Matrix0.Columns.Item("EESI").Cells.Item(i).Specific.string)
        '            Flag = True
        '        End If
        '    Next

        '    If Flag = False Then
        '        objaddon.objapplication.SetStatusBarMessage("Please select the status in line level...", SAPbouiCOM.BoMessageTime.bmt_Short)
        '        Exit Sub
        '    End If
        '    Dim GetVariableNames() As Double
        '    Dim GetIndexNames() As String
        '    GetVariableNames = {TotGross, TotBonus, TotIncent, TotPF, TotESI, TotPT, TotTDS, TotLoan, TotSalPayable, TotEmployerPF, TotEmployerESI}
        '    GetIndexNames = {"TotGross", "TotBonus", "TotIncent", "TotPF", "TotESI", "TotPT", "TotTDS", "TotLoan", "TotSalPayable", "TotEmployerPF", "TotEmployerESI"}
        '    Dim GetTranValues As New List(Of JETransaction)
        '    For ivalue As Integer = 0 To GetVariableNames.Length - 1
        '        If GetVariableNames(ivalue) > 0 Then
        '            Dim GetTran As New JETransaction
        '            GetTran.Amount = GetVariableNames(ivalue)
        '            GetTran.Type = GetIndexNames(ivalue)
        '            GetTranValues.Add(GetTran)
        '        End If
        '    Next

        '    Dim FDate As Date = Date.ParseExact(EditText14.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
        '    Dim Branch As String = ""
        '    If MultiBranch = "Y" Then
        '        If ComboBox6.Selected.Value = "-1" Or ComboBox6.Selected.Value Is Nothing Then
        '            Branch = objaddon.objglobalmethods.getSingleValue("select ""BPLId"" from OBPL where ""BPLId""='1'")
        '        Else
        '            Branch = objaddon.objglobalmethods.getSingleValue("select ""BPLId"" from OBPL where ""BPLId""='" & ComboBox6.Selected.Value & "'")
        '        End If
        '    End If

        '    If Not objaddon.objcompany.InTransaction Then objaddon.objcompany.StartTransaction()
        '    'objjournalentry.Series = Series
        '    objjournalentry.ReferenceDate = Now.ToString("yyyy-MM-dd")
        '    objjournalentry.Memo = "Payroll Process for the month -" & CStr(MonthName(FDate.Month, True))
        '    Dim JEValues() As String
        '    For Rec As Integer = 0 To GetTranValues.Count - 1
        '        JEValues = gettranforJE(GetTranValues.ElementAt(Rec).Type)
        '        objjournalentry.Lines.AccountCode = objrs.Fields.Item(CInt(JEValues(1))).Value.ToString
        '        If JEValues(0) = "Debit" Then objjournalentry.Lines.Debit = GetTranValues.ElementAt(Rec).Amount Else objjournalentry.Lines.Credit = GetTranValues.ElementAt(Rec).Amount
        '        If Branch <> "" Then
        '            objjournalentry.Lines.BPLID = Branch
        '        End If
        '        objjournalentry.Lines.Add()
        '    Next

        '    If objjournalentry.Add <> 0 Then
        '        If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
        '        objaddon.objapplication.SetStatusBarMessage(objaddon.objcompany.GetLastErrorDescription, SAPbouiCOM.BoMessageTime.bmt_Short)
        '        CheckBox2.Checked = False
        '    Else
        '        If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
        '        DocEntry = objaddon.objcompany.GetNewObjectKey()
        '        objform.Items.Item("txtJENo").Specific.String = DocEntry
        '        objaddon.objapplication.SetStatusBarMessage("Journal Entry Successfully Posted..." & DocEntry, SAPbouiCOM.BoMessageTime.bmt_Long, False)
        '    End If
        'End Sub

        'Public Sub IN_JournalVoucher()
        '    Dim objJournalVouchers As SAPbobsCOM.JournalVouchers
        '    Dim DocEntry As String = ""
        '    Dim Flag As Boolean = False

        '    objJournalVouchers = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalVouchers)
        '    strsql = "CALL ""MIPL_GetJEAccount_Details"" "            '
        '    objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        '    objrs.DoQuery(strsql)
        '    If objrs.RecordCount = 0 Then objaddon.objapplication.StatusBar.SetText("Please update the GL Master...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error) : Exit Sub
        '    Dim TotGross As Double = 0, TotEncash As Double = 0, TotBonus As Double = 0, TotIncent As Double = 0
        '    Dim TotPF As Double = 0, TotEmployerExpenses As Double = 0, TotEmployerPF As Double = 0, TotEmployerESI As Double = 0, TotESI As Double = 0, TotPT As Double = 0, TotTDS As Double = 0, TotLoan As Double = 0, TotSalPayable As Double = 0
        '    For i As Integer = 1 To Matrix0.VisualRowCount
        '        If Matrix0.Columns.Item("salstat").Cells.Item(i).Specific.Checked = True Then
        '            TotGross += CDbl(Matrix0.Columns.Item("GrossSal").Cells.Item(i).Specific.string)
        '            TotBonus += CDbl(Matrix0.Columns.Item("Bonus").Cells.Item(i).Specific.string)
        '            TotIncent += CDbl(Matrix0.Columns.Item("Incent").Cells.Item(i).Specific.string)
        '            TotPF += CDbl(Matrix0.Columns.Item("PF").Cells.Item(i).Specific.string)
        '            TotESI += CDbl(Matrix0.Columns.Item("ESI").Cells.Item(i).Specific.string)
        '            TotPT += CDbl(Matrix0.Columns.Item("ProfTax").Cells.Item(i).Specific.string)
        '            TotTDS += CDbl(Matrix0.Columns.Item("TDS").Cells.Item(i).Specific.string)
        '            TotLoan += CDbl(Matrix0.Columns.Item("Loan").Cells.Item(i).Specific.string)
        '            TotSalPayable += CDbl(Matrix0.Columns.Item("Payable").Cells.Item(i).Specific.string)
        '            TotEmployerPF += CDbl(Matrix0.Columns.Item("EPF").Cells.Item(i).Specific.string)
        '            TotEmployerESI += CDbl(Matrix0.Columns.Item("EESI").Cells.Item(i).Specific.string)
        '            TotEmployerExpenses += CDbl(Matrix0.Columns.Item("EmpCont").Cells.Item(i).Specific.string)
        '            Flag = True
        '        End If
        '    Next

        '    If Flag = False Then
        '        objaddon.objapplication.SetStatusBarMessage("Please select the status in line level...", SAPbouiCOM.BoMessageTime.bmt_Short)
        '        Exit Sub
        '    End If
        '    Dim GetVariableNames() As Double
        '    Dim GetIndexNames() As String
        '    GetVariableNames = {TotGross, TotBonus, TotIncent, TotPF, TotESI, TotPT, TotTDS, TotLoan, TotSalPayable, TotEmployerPF, TotEmployerESI, TotEmployerExpenses}
        '    GetIndexNames = {"TotGross", "TotBonus", "TotIncent", "TotPF", "TotESI", "TotPT", "TotTDS", "TotLoan", "TotSalPayable", "TotEmployerPF", "TotEmployerESI", "TotEmployerExpenses"}
        '    Dim GetTranValues As New List(Of JETransaction)
        '    For ivalue As Integer = 0 To GetVariableNames.Length - 1
        '        If GetVariableNames(ivalue) > 0 Then
        '            Dim GetTran As New JETransaction
        '            GetTran.Amount = GetVariableNames(ivalue)
        '            GetTran.Type = GetIndexNames(ivalue)
        '            GetTranValues.Add(GetTran)
        '        End If
        '    Next

        '    Dim FDate As Date = Date.ParseExact(EditText14.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
        '    Dim Branch As String = ""
        '    If MultiBranch = "Y" Then
        '        If ComboBox6.Selected.Value = "-1" Or ComboBox6.Selected.Value Is Nothing Then
        '            Branch = objaddon.objglobalmethods.getSingleValue("select ""BPLId"" from OBPL where ""BPLId""='1'")
        '        Else
        '            Branch = objaddon.objglobalmethods.getSingleValue("select ""BPLId"" from OBPL where ""BPLId""='" & ComboBox6.Selected.Value & "'")
        '        End If
        '    End If

        '    If Not objaddon.objcompany.InTransaction Then objaddon.objcompany.StartTransaction()
        '    'objjournalentry.Series = Series
        '    objJournalVouchers.JournalEntries.ReferenceDate = Now.ToString("yyyy-MM-dd")
        '    objJournalVouchers.JournalEntries.DueDate = Now.ToString("yyyy-MM-dd")
        '    objJournalVouchers.JournalEntries.TaxDate = Now.ToString("yyyy-MM-dd")
        '    objJournalVouchers.JournalEntries.Memo = "Payroll Process for the month -" & CStr(MonthName(FDate.Month, True))
        '    Dim JEValues() As String
        '    For Rec As Integer = 0 To GetTranValues.Count - 1
        '        JEValues = gettranforJE(GetTranValues.ElementAt(Rec).Type)
        '        objJournalVouchers.JournalEntries.Lines.AccountCode = objrs.Fields.Item(CInt(JEValues(1))).Value.ToString
        '        If JEValues(0) = "Debit" Then objJournalVouchers.JournalEntries.Lines.Debit = GetTranValues.ElementAt(Rec).Amount Else objJournalVouchers.JournalEntries.Lines.Credit = GetTranValues.ElementAt(Rec).Amount
        '        If Branch <> "" Then
        '            objJournalVouchers.JournalEntries.Lines.BPLID = Branch
        '        End If
        '        objJournalVouchers.JournalEntries.Lines.Add()
        '    Next

        '    objJournalVouchers.JournalEntries.Add()
        '    Dim lretcode = objJournalVouchers.Add()
        '    If lretcode <> 0 Then
        '        If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
        '        objaddon.objapplication.SetStatusBarMessage(objaddon.objcompany.GetLastErrorDescription, SAPbouiCOM.BoMessageTime.bmt_Short)
        '        CheckBox2.Checked = False
        '    Else
        '        objaddon.objcompany.GetNewObjectCode(DocEntry)
        '        If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
        '        objform.Items.Item("txtJENo").Specific.String = DocEntry
        '        objaddon.objapplication.SetStatusBarMessage("Journal Entry Successfully Posted..." & DocEntry, SAPbouiCOM.BoMessageTime.bmt_Short, False)
        '    End If
        'End Sub

        Private Sub Payroll_JournalVoucher()
            Try
                Dim Rec_A_Count As Integer = 0
                Dim DocEntry As String = "", Series As String

                If MultiBranch = "Y" Then
                    strsql = "CALL ""MIPL_Payroll_GL"" ('" & EditText17.Value & "','" & ComboBox6.Selected.Value & "')"
                Else
                    strsql = "CALL ""MIPL_Payroll_GL"" ('" & EditText17.Value & "','-1')"
                End If

                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strsql)
                If objrs.RecordCount > 0 Then

                    objaddon.objapplication.StatusBar.SetText("Creating Journal Voucher. Please wait...", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
                    Try
                        If Not objaddon.objcompany.InTransaction Then objaddon.objcompany.StartTransaction()
                        Dim oPayrollJV As SAPbobsCOM.JournalVouchers
                        oPayrollJV = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalVouchers)

                        oPayrollJV.JournalEntries.ReferenceDate = objrs.Fields.Item("Date").Value
                        'oPayrollJV.JournalEntries.DueDate = objrs.Fields.Item("Date").Value
                        oPayrollJV.JournalEntries.TaxDate = objrs.Fields.Item("Date").Value
                        Dim DocDate As Date = Date.ParseExact(EditText15.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo) 'objrs.Fields.Item("Date").Value.ToString
                        'If objrs.Fields.Item("Transcode").Value.ToString <> "" Then oPayrollJV.JournalEntries.TransactionCode = objrs.Fields.Item("Transcode").Value.ToString
                        If objaddon.HANA Then
                            Series = objaddon.objglobalmethods.getSingleValue("SELECT Top 1 ""Series"" FROM NNM1 WHERE ""ObjectCode""='30' and ""Indicator""=(Select ""Indicator"" from OFPR where '" & DocDate.ToString("yyyyMMdd") & "' Between ""F_RefDate"" and ""T_RefDate"") " &
                                                                                      " and Ifnull(""Locked"",'')='N' and ""BPLId""='" & objrs.Fields.Item("BPLId").Value.ToString & "'")
                        Else
                            Series = objaddon.objglobalmethods.getSingleValue("SELECT Top 1 Series FROM NNM1 WHERE ObjectCode='30' and Indicator=(Select Indicator from OFPR where '" & DocDate.ToString("yyyyMMdd") & "' Between F_RefDate and T_RefDate) " &
                                                                                      " and Isnull(Locked,'')='N' and BPLId='" & objrs.Fields.Item("BPLId").Value.ToString & "'")
                        End If
                        If Series <> "" Then oPayrollJV.JournalEntries.Series = Series
                        If objrs.Fields.Item("Memo").Value.ToString <> "" Then oPayrollJV.JournalEntries.Memo = objrs.Fields.Item("Memo").Value.ToString
                        'If objrs.Fields.Item("Narration").Value.ToString <> "" Then oPayrollJV.JournalEntries.UserFields.Fields.Item("U_Narration").Value = objrs.Fields.Item("Narration").Value.ToString

                        If objrs.Fields.Item("Ref1").Value.ToString <> "" Then oPayrollJV.JournalEntries.Reference = objrs.Fields.Item("Ref1").Value.ToString
                        If objrs.Fields.Item("Ref2").Value.ToString <> "" Then oPayrollJV.JournalEntries.Reference2 = objrs.Fields.Item("Ref2").Value.ToString
                        'If objrs.Fields.Item("Ref3").Value.ToString <> "" Then oPayrollJV.JournalEntries.Reference3 = objrs.Fields.Item("Ref3").Value.ToString

                        For i As Integer = 0 To objrs.RecordCount - 1
                            'If objrs.Fields.Item("Type").Value.ToString.ToUpper = "A" Then
                            'strsql = objrs.Fields.Item("Ref3").Value
                            'If (objrs.Fields.Item("Ref3").Value = "EMP/KVL/00200") Then 'Or objrs.Fields.Item("Ref3").Value = "EMP/KVL/00177"
                            oPayrollJV.JournalEntries.Lines.AccountCode = objrs.Fields.Item("AccountCode").Value
                            If objrs.Fields.Item("DebitAmount").Value <> 0 Then oPayrollJV.JournalEntries.Lines.Debit = objrs.Fields.Item("DebitAmount").Value Else oPayrollJV.JournalEntries.Lines.Credit = objrs.Fields.Item("CreditAmount").Value
                                If objrs.Fields.Item("BPLId").Value.ToString <> "-1" Then oPayrollJV.JournalEntries.Lines.BPLID = objrs.Fields.Item("BPLId").Value.ToString
                                oPayrollJV.JournalEntries.Lines.Reference1 = objrs.Fields.Item("Lref1").Value
                                oPayrollJV.JournalEntries.Lines.Reference2 = objrs.Fields.Item("Lref2").Value
                                oPayrollJV.JournalEntries.Lines.AdditionalReference = objrs.Fields.Item("Ref3").Value
                                oPayrollJV.JournalEntries.Lines.Add()
                            'End If

                            'End If
                            objrs.MoveNext()
                        Next
                        oPayrollJV.JournalEntries.Add()

                        Dim lretcode = oPayrollJV.Add()
                        If lretcode <> 0 Then
                            If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                            objaddon.objapplication.SetStatusBarMessage(objaddon.objcompany.GetLastErrorDescription, SAPbouiCOM.BoMessageTime.bmt_Short)
                            CheckBox2.Checked = False
                        Else
                            If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                            objaddon.objcompany.GetNewObjectCode(DocEntry)
                            Dim Value() As String = DocEntry.Split(vbTab)
                            objform.Items.Item("txtJENo").Specific.String = Value(0) ' DocEntry
                            'objform.Items.Item("txtrem").Specific.String = objform.Items.Item("txtrem").Specific.String & " Voucher- " & CStr(Value(0))
                            If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE
                            objform.Items.Item("1").Click()
                            objaddon.objapplication.SetStatusBarMessage("Journal Voucher Successfully Posted..." & DocEntry, SAPbouiCOM.BoMessageTime.bmt_Short, False)
                        End If
                    Catch ex As Exception
                        If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                        objaddon.objapplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                    End Try
                Else
                    objaddon.objapplication.SetStatusBarMessage("No Data found for posting the Journal Transaction..." & DocEntry, SAPbouiCOM.BoMessageTime.bmt_Short, False)
                End If

            Catch ex As Exception
                objaddon.objapplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
            End Try
        End Sub

        'Public Sub Journal_Voucher()
        '    Try
        '        Dim oPayrollJV As SAPbobsCOM.JournalVouchers
        '        oPayrollJV = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalVouchers)
        '        Dim DocEntry As String = ""
        '        Dim Flag As Boolean = False
        '        strsql = "CALL ""MIPL_GetJEAccount_Details"" "            '
        '        objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        '        objrs.DoQuery(strsql)
        '        If objrs.RecordCount = 0 Then objaddon.objapplication.StatusBar.SetText("Please update the GL Master...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error) : Exit Sub
        '        Dim TotGross As Double = 0, TotEncash As Double = 0, TotBonus As Double = 0, TotIncent As Double = 0
        '        Dim TotPF As Double = 0, TotEmployerPF As Double = 0, TotEmployerESI As Double = 0, TotESI As Double = 0, TotPT As Double = 0, TotTDS As Double = 0, TotLoan As Double = 0, TotSalPayable As Double = 0
        '        Dim GetTranValues As New List(Of JVTransaction)
        '        For Col As Integer = 15 To Matrix0.Columns.Count - 1
        '            Dim GetTran As New JVTransaction
        '            For Row As Integer = 1 To Matrix0.VisualRowCount
        '                If Matrix0.Columns.Item("salstat").Cells.Item(Row).Specific.Checked = True Then
        '                    GetTran.Amount += CDbl(Matrix0.Columns.Item(Col).Cells.Item(Row).Specific.string)
        '                    GetTran.ElementName = Matrix0.Columns.Item(Col).TitleObject.Caption
        '                    GetTranValues.Add(GetTran)
        '                    Flag = True
        '                End If
        '            Next
        '        Next

        '        'For i As Integer = 1 To Matrix0.VisualRowCount
        '        '    If Matrix0.Columns.Item("salstat").Cells.Item(i).Specific.Checked = True Then
        '        '        TotGross += CDbl(Matrix0.Columns.Item("GrossSal").Cells.Item(i).Specific.string)
        '        '        ' TotEncash += CDbl(Matrix0.Columns.Item("Encash").Cells.Item(i).Specific.string)
        '        '        TotBonus += CDbl(Matrix0.Columns.Item("Bonus").Cells.Item(i).Specific.string)
        '        '        TotIncent += CDbl(Matrix0.Columns.Item("Incent").Cells.Item(i).Specific.string)
        '        '        TotPF += CDbl(Matrix0.Columns.Item("PF").Cells.Item(i).Specific.string)
        '        '        TotESI += CDbl(Matrix0.Columns.Item("ESI").Cells.Item(i).Specific.string)
        '        '        TotPT += CDbl(Matrix0.Columns.Item("ProfTax").Cells.Item(i).Specific.string)
        '        '        TotTDS += CDbl(Matrix0.Columns.Item("TDS").Cells.Item(i).Specific.string)
        '        '        TotLoan += CDbl(Matrix0.Columns.Item("Loan").Cells.Item(i).Specific.string)
        '        '        TotSalPayable += CDbl(Matrix0.Columns.Item("Payable").Cells.Item(i).Specific.string)
        '        '        TotEmployerPF += CDbl(Matrix0.Columns.Item("EPF").Cells.Item(i).Specific.string)
        '        '        TotEmployerESI += CDbl(Matrix0.Columns.Item("EESI").Cells.Item(i).Specific.string)
        '        '        Flag = True
        '        '    End If
        '        'Next

        '        If Flag = False Then
        '            objaddon.objapplication.SetStatusBarMessage("Please select the status in line level...", SAPbouiCOM.BoMessageTime.bmt_Short)
        '            Exit Sub
        '        End If
        '        Dim FDate As Date = Date.ParseExact(EditText14.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
        '        oPayrollJV.JournalEntries.ReferenceDate = Now.ToString("yyyy-MM-dd")
        '        oPayrollJV.JournalEntries.DueDate = Now.ToString("yyyy-MM-dd")
        '        oPayrollJV.JournalEntries.TaxDate = Now.ToString("yyyy-MM-dd")
        '        oPayrollJV.JournalEntries.Memo = "Payroll Process for the month -" & CStr(MonthName(FDate.Month, True))
        '        Dim Branch As String = ""
        '        If MultiBranch = "Y" Then
        '            If ComboBox6.Selected.Value = "-1" Or ComboBox6.Selected.Value Is Nothing Then
        '                Branch = objaddon.objglobalmethods.getSingleValue("select ""BPLId"" from OBPL where ""BPLId""='1'")
        '            Else
        '                Branch = objaddon.objglobalmethods.getSingleValue("select ""BPLId"" from OBPL where ""BPLId""='" & ComboBox6.Selected.Value & "'")
        '            End If
        '        End If
        '        oPayrollJV.JournalEntries.Lines.AccountCode = objrs.Fields.Item("AccountCode").Value
        '        If objrs.Fields.Item("DebitAmount").Value <> 0 Then oPayrollJV.JournalEntries.Lines.Debit = objrs.Fields.Item("DebitAmount").Value Else oPayrollJV.JournalEntries.Lines.Credit = objrs.Fields.Item("CreditAmount").Value
        '        If Branch <> "" Then
        '            oPayrollJV.JournalEntries.Lines.BPLID = Branch
        '        End If
        '        oPayrollJV.JournalEntries.Lines.Add()

        '        oPayrollJV.JournalEntries.Add()
        '        Dim lretcode = oPayrollJV.Add()
        '        If lretcode <> 0 Then
        '            If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
        '            objaddon.objapplication.SetStatusBarMessage(objaddon.objcompany.GetLastErrorDescription, SAPbouiCOM.BoMessageTime.bmt_Short)
        '            CheckBox2.Checked = False
        '        Else
        '            objaddon.objcompany.GetNewObjectCode(DocEntry)
        '            If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
        '            objform.Items.Item("txtJENo").Specific.String = DocEntry
        '            objaddon.objapplication.SetStatusBarMessage("Journal Entry Successfully Posted..." & DocEntry, SAPbouiCOM.BoMessageTime.bmt_Long, False)
        '        End If

        '    Catch ex As Exception

        '    End Try
        'End Sub

        Private Sub ComboBox4_ComboSelectAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox4.ComboSelectAfter
            Try
                If objform.Mode <> SAPbouiCOM.BoFormMode.fm_ADD_MODE Then Exit Sub
                odbdsDetails.SetValue("DocNum", 0, objaddon.objglobalmethods.GetDocNum("OPPII", CInt(ComboBox4.Selected.Value)))
            Catch ex As Exception
            End Try


        End Sub

        Private Sub ComboBox5_ComboSelectAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox5.ComboSelectAfter
            Try
                If objform.Mode <> SAPbouiCOM.BoFormMode.fm_ADD_MODE Then Exit Sub
                If ComboBox5.Selected Is Nothing Then Exit Sub
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery("select TO_VARCHAR(""F_RefDate"",'yyyyMMdd') ""F_RefDate"",TO_VARCHAR(""T_RefDate"",'yyyyMMdd') ""T_RefDate"" from OFPR where ""Code""='" & ComboBox5.Selected.Value & "'")
                'objrs.DoQuery("select ""F_RefDate"" ""F_RefDate"",""T_RefDate"" ""T_RefDate"" from OFPR where ""Code""='" & ComboBox0.Selected.Value & "'")
                If objrs.RecordCount > 0 Then
                    objform.Items.Item("tfrmdate").Specific.string = objrs.Fields.Item("F_RefDate").Value.ToString
                    objform.Items.Item("ttodate").Specific.string = objrs.Fields.Item("T_RefDate").Value.ToString
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Matrix0_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg)
            Try
                Matrix0.AutoResizeColumns()
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Button0_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button0.PressedAfter
            Try
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE And pVal.ActionSuccess = True Then
                    odbdsDetails = objform.DataSources.DBDataSources.Item("@MIPL_OPPI")
                    objaddon.objglobalmethods.LoadSeries(objform, odbdsDetails, "OPPII")
                    objform.Items.Item("txtrem").Specific.String = "Created By " & objaddon.objcompany.UserName & " on " & Now.ToString("dd/MMM/yyyy HH:mm:ss")
                End If
                If (objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Or objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE) Then
                    addupdate = True
                Else
                    addupdate = False
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub LinkedButton2_PressedBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles LinkedButton2.PressedBefore
            Try
                Dim Value() As String = EditText16.Value.Split(vbTab)
                Dim TEntry As String = objaddon.objglobalmethods.getSingleValue("Select 1 as ""Status"" from OBTF where ""BatchNum""='" & Value(0) & "' and ifnull(""BtfStatus"",'')='O'")
                LinkedButton2.LinkedObject = "-1"
                If TEntry <> "" Then
                    LinkedButton2.LinkedObject = "28"
                    StaticText17.Caption = "JV No"
                Else
                    LinkedButton2.LinkedObject = "30"
                    StaticText17.Caption = "JE No"
                End If

            Catch ex As Exception

            End Try

        End Sub

        Private Sub Form_ActivateAfter(pVal As SAPbouiCOM.SBOItemEventArg)
            Try
                'If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Matrix0_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.ClickAfter
            If pVal.Row <= 0 Then Exit Sub
            Try
                'Matrix0.CommonSetting.SetRowBackColor(pVal.Row, Matrix0.Item.BackColor)
                'Matrix0.CommonSetting.SetRowBackColor(pVal.Row, Color.PaleGoldenrod.ToArgb)
                Matrix0.SelectRow(pVal.Row, True, False)
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Matrix0_ValidateBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Matrix0.ValidateBefore
            Try
                If pVal.InnerEvent = True Then Exit Sub
                If pVal.ItemChanged = False Then Exit Sub
                Dim PF As Double, ESI As Double, LOAN As Double = 0, NetSal, ProfTax As Double
                Dim Bonus, Incentive, Addition, TDSNew, Deduct, GrossSal, DedElements, AddElements As Double
                objform.Freeze(True)
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
                ProfTax = CDbl(Matrix0.Columns.Item("ProfTax").Cells.Item(pVal.Row).Specific.string)

                AddElements = CDbl(Matrix0.Columns.Item("U_AB1").Cells.Item(pVal.Row).Specific.string) + CDbl(Matrix0.Columns.Item("U_AB2").Cells.Item(pVal.Row).Specific.string) + CDbl(Matrix0.Columns.Item("U_AB3").Cells.Item(pVal.Row).Specific.string) + CDbl(Matrix0.Columns.Item("U_AB4").Cells.Item(pVal.Row).Specific.string) + CDbl(Matrix0.Columns.Item("U_AB5").Cells.Item(pVal.Row).Specific.string)
                DedElements = CDbl(Matrix0.Columns.Item("U_DB1").Cells.Item(pVal.Row).Specific.string) + CDbl(Matrix0.Columns.Item("U_DB2").Cells.Item(pVal.Row).Specific.string) + CDbl(Matrix0.Columns.Item("U_DB3").Cells.Item(pVal.Row).Specific.string) + CDbl(Matrix0.Columns.Item("U_DB4").Cells.Item(pVal.Row).Specific.string) + CDbl(Matrix0.Columns.Item("U_DB5").Cells.Item(pVal.Row).Specific.string)
                Try
                    If pVal.ColUID = "ProfTax" Then
                        If Val(Matrix0.Columns.Item("ProfTax").Cells.Item(pVal.Row).Specific.string) <> 0 Then
                            Dim Status As String = ""
                            Status = objaddon.objglobalmethods.getSingleValue("select 1 as ""Status"" from ""@SMPR_OHEM"" where Ifnull(""U_PTStat"",'')='Y'  and ""U_ExtEmpNo""='" & Matrix0.Columns.Item("empID").Cells.Item(pVal.Row).Specific.string & "'")
                            If Status = "" Then
                                BubbleEvent = False
                                Matrix0.SetCellWithoutValidation(pVal.Row, "ProfTax", 0)
                                objaddon.objapplication.StatusBar.SetText("Professional Tax not enabled. Please check in Employee Master...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                                Exit Sub
                            End If
                        End If
                    End If
                    Addition = Bonus + Incentive
                    Matrix0.SetCellWithoutValidation(pVal.Row, "TotAdd", Addition + AddElements)
                    Matrix0.SetCellWithoutValidation(pVal.Row, "TotDed", CDbl(PF + ESI + ProfTax + LOAN + TDSNew + DedElements))
                    Matrix0.SetCellWithoutValidation(pVal.Row, "NetSal", Math.Round(CDbl((GrossSal + (Bonus + Incentive + AddElements)) - (PF + ESI + ProfTax + LOAN + TDSNew + DedElements))))
                    Matrix0.SetCellWithoutValidation(pVal.Row, "Payable", Math.Round(CDbl((GrossSal + (Bonus + Incentive + AddElements)) - (PF + ESI + ProfTax + LOAN + TDSNew + DedElements))))
                    Matrix0.SetCellWithoutValidation(pVal.Row, "round", Math.Round(CDbl((GrossSal + (Bonus + Incentive + AddElements)) - (PF + ESI + ProfTax + LOAN + TDSNew + DedElements))) - CDbl((GrossSal + (Bonus + Incentive + AddElements)) - (PF + ESI + ProfTax + LOAN + TDSNew + DedElements)))
                    'Matrix0.Columns.Item("TotAdd").Cells.Item(pVal.Row).Specific.string = Addition
                    'Matrix0.Columns.Item("TotDed").Cells.Item(pVal.Row).Specific.string = CDbl(PF + ESI + ProfTax + LOAN + TDSNew)
                    ''Matrix0.Columns.Item("Payable").Cells.Item(pVal.Row).Specific.string = CDbl((NetSal + (Bonus + Incentive)) - (LOAN + TDSNew))
                    'Matrix0.Columns.Item("NetSal").Cells.Item(pVal.Row).Specific.string = CDbl((GrossSal + (Bonus + Incentive)) - (PF + ESI + ProfTax + LOAN + TDSNew))
                    'Matrix0.Columns.Item("Payable").Cells.Item(pVal.Row).Specific.string = CDbl((GrossSal + (Bonus + Incentive)) - (PF + ESI + ProfTax + LOAN + TDSNew))


                Catch ex As Exception
                End Try
                'objform.Freeze(False)
            Catch ex As Exception
                'objform.Freeze(False)
            Finally
                objform.Freeze(False)
            End Try

        End Sub

        Private WithEvents StaticText0 As SAPbouiCOM.StaticText
        Private WithEvents EditText0 As SAPbouiCOM.EditText
        Private WithEvents ComboBox0 As SAPbouiCOM.ComboBox


        Public Sub Payslip_AutoEmail()
            Try
                Dim FromMail_id As String = "", FromMail_Password As String = "", Mail_Host As String = "", Mail_Port As String = ""
                Dim strquery, Foldername, Filename As String
                Dim objrs As SAPbobsCOM.Recordset
                Dim objrsupdate As SAPbobsCOM.Recordset
                Dim Mailbody, ServerName, CompanyDb, DBUserName, DbPassword As String
                Dim Payroll_Report_FileName = System.Windows.Forms.Application.StartupPath & "\" & "PaySlip_OEC.rpt"
                Dim initialpath As String = objaddon.objglobalmethods.getSingleValue("select ""AttachPath"" from OADP")
                Foldername = initialpath + "Payroll\RptFile"
                If Directory.Exists(Foldername) Then
                Else
                    Directory.CreateDirectory(Foldername)
                End If
                Filename = Foldername & "\PaySlip1.rpt"
                Filename = "E:\Chitra\Common Payroll\Dec 16\HRMS_Posting\HRMS_Posting\bin\x64\Debug\PaySlip_YMH.rpt"
                '"E:\Chitra\Common Payroll\Dec 16\BackUp Source Payroll\April 27 2022\TestReport.rpt"
                ServerName = "WATSON.TMICLOUD.NET:30013" '"WAT@WATSON.TMICLOUD.NET:30013"
                CompanyDb = "OEC_TEST" '"KANAKAVALLI_LIVE"
                DBUserName = "OECDBBR" '"KANASA" '"OECDBBR"
                DbPassword = "India@1947" '"R&s$!a#f%ru$456" ' "India@1947"

                FromMail_id = "saptech18@mukeshinfoserve.com"
                FromMail_Password = "tyntwdjfwlztgrns"
                Mail_Host = "smtp-mail.outlook.com"
                Mail_Port = "587"
                If FromMail_id = "" Or FromMail_Password = "" Or Mail_Host = "" Or Mail_Port = "" Then Exit Sub
                'MsgBox(Payroll_Report_FileName)   
                Dim cryRpt As New ReportDocument
                cryRpt.Load(Filename)
                cryRpt.DataSourceConnections(0).SetConnection(objaddon.objcompany.Server, CompanyDb, False) 'objaddon.objcompany.CompanyDB
                cryRpt.DataSourceConnections(0).SetLogon(DBUserName, DbPassword)


                'For i As Integer = 0 To objrs.RecordCount - 1
                'If objrs.Fields.Item("ToEmail").Value.ToString = "" Then Continue For

                Dim Email As New System.Net.Mail.MailMessage
                Dim MailServer As New System.Net.Mail.SmtpClient()

                Try
                        MailServer.Host = Mail_Host
                        MailServer.Port = Mail_Port
                        MailServer.Credentials = New System.Net.NetworkCredential(FromMail_id.ToString.Trim, FromMail_Password.ToString.Trim)
                        MailServer.EnableSsl = True
                        Email.From = New System.Net.Mail.MailAddress(FromMail_id.ToString.Trim)

                    Email.To.Add(New System.Net.Mail.MailAddress("saptech18@mukeshinfoserve.com"))
                    Email.Subject = "Pay Slip - " ' & objrs.Fields.Item("ToName").Value.ToString & " - " & objrs.Fields.Item("MonthName").Value.ToString & " - " & objrs.Fields.Item("Year").Value.ToString

                    Mailbody = "Dear Chitra, " '& objrs.Fields.Item("ToName").Value.ToString & ","
                    Mailbody += vbCrLf + " "
                    Mailbody += vbCrLf + " Please Find the Attached Payslip for the Month of " '& objrs.Fields.Item("Period").Value.ToString & "."
                    Mailbody += vbCrLf + " "
                        Mailbody += vbCrLf + "With Regards,"
                        Mailbody += vbCrLf + "HR Team"
                        Mailbody += vbCrLf + " "
                        Mailbody += vbCrLf + " "
                        Mailbody += "This is Auto generated E-Mail from SAP Business One . Please do not reply to this message. Thank you! "

                        Email.Body = Mailbody
                        Email.Priority = Net.Mail.MailPriority.High

                    'cryRpt.SetParameterValue("Emp@select empid,FIRSTNAME+'  '+LASTNAME from ohem order by Firstname", objrs.Fields.Item("Empid").Value.ToString)
                    'cryRpt.SetParameterValue("Month", objrs.Fields.Item("Month").Value.ToString)
                    'cryRpt.SetParameterValue("year@select distinct year(T0.u_todate) year from [@SMPR_OPRC] T0", objrs.Fields.Item("Year").Value.ToString)
                    'cryRpt.SetParameterValue("OTTA", "N")

                    cryRpt.SetParameterValue("Month", "MARCH") '"MARCH"
                    cryRpt.SetParameterValue("Year@select year(current_date) from dummy union all select year(current_date)-1 from dummy union all select year(current_date)-2 from dummy", 2022) '2022 Convert.ToInt32(objrs.Fields.Item("Year").Value.ToString)
                    cryRpt.SetParameterValue("Emp@select Distinct T1.""U_IDNo"",T1.""U_empName"" from ""@SMPR_PRC1"" T1 where ifnull(T1.""U_IDNo"",'')<>''", "EMP/KVL/00007") ' "EMP/KVL/00007" CStr(Trim(objrs.Fields.Item("U_ExtEmpNo").Value.ToString))


                    'cryRpt.SetParameterValue("Month", "MARCH")
                    'cryRpt.SetParameterValue("Year@select year(current_date) from dummy union all select year(current_date)-1 from dummy union all select year(current_date)-2 from dummy", "2020")
                    'cryRpt.SetParameterValue("Emp@select Distinct T1.""U_empID"",T1.""U_empName"" from ""@MIPL_PPI1"" T1 where ifnull(T1.""U_empID"",'')<>''", "EMP/KVL/00016") 'objrs.Fields.Item("U_ExtEmpNo").Value.ToString



                    Email.Attachments.Add(New Attachment(cryRpt.ExportToStream(ExportFormatType.PortableDocFormat), "Pay Slip - .PDF"))

                    MailServer.Send(Email)

                        'strsql = "Update ""@SMPR_PRC1"" set ""U_Payslip""='Y' where ""DocEntry""='" & objrs.Fields.Item("DocEntry").Value.ToString & "' and ""U_empid""='" & objrs.Fields.Item("Empid").Value.ToString & "'"
                        'objrsupdate = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                        'objrsupdate.DoQuery(strsql)

                    Catch ex As Exception
                    Finally
                        If Not Email Is Nothing Then Email.Dispose()
                        MailServer = Nothing
                    End Try

                'objrs.MoveNext()
                'Next

            Catch ex As Exception
                MsgBox(ex.Message.ToString)
            End Try

        End Sub

    End Class
End Namespace
