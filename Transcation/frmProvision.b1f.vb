Option Strict Off
Option Explicit On

Imports SAPbouiCOM.Framework

Namespace HRMS
    <FormAttribute("PROV", "Transcation/frmProvision.b1f")>
    Friend Class frmProvision
        Inherits UserFormBase
        Dim strsql As String
        Dim objrs As SAPbobsCOM.Recordset
        Private WithEvents objform As SAPbouiCOM.Form
        Dim FormCount As Integer = 0
        Dim lastrowid As Integer = -1

        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.StaticText0 = CType(Me.GetItem("Item_0").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox0 = CType(Me.GetItem("cmbmonth").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText1 = CType(Me.GetItem("Item_3").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox1 = CType(Me.GetItem("cmbyear").Specific, SAPbouiCOM.ComboBox)
            Me.Grid0 = CType(Me.GetItem("grd").Specific, SAPbouiCOM.Grid)
            Me.Button0 = CType(Me.GetItem("Item_6").Specific, SAPbouiCOM.Button)
            Me.Button1 = CType(Me.GetItem("Item_7").Specific, SAPbouiCOM.Button)
            Me.Button2 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.CheckBox0 = CType(Me.GetItem("chkfinal").Specific, SAPbouiCOM.CheckBox)
            Me.StaticText2 = CType(Me.GetItem("Item_2").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox2 = CType(Me.GetItem("Item_4").Specific, SAPbouiCOM.ComboBox)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()

        End Sub


        Private Sub OnCustomInitialize()
            objform = objaddon.objapplication.Forms.GetForm("PROV", FormCount)
            objform = objaddon.objapplication.Forms.ActiveForm
            Try
                strsql = "select Distinct To_Varchar(""ProvisionDate"",'YYYY'),To_Varchar(""ProvisionDate"",'YYYY') from ""HRMS_PROVISION_DETAILS"" "
                objaddon.objglobalmethods.LoadCombo(ComboBox1, strsql, Nothing)

                CheckBox0.Item.Height = CheckBox0.Item.Height + 4

                ComboBox2.Select("-1", SAPbouiCOM.BoSearchKey.psk_ByValue)

                objform.ActiveItem = "cmbmonth"

                For i As Integer = -5 To 10
                    If i = 0 Then
                        i += 1
                    End If
                    Dim date1 As String = DateTime.Now.AddYears(i).Year.ToString
                    ComboBox1.ValidValues.Add(date1, date1)

                Next
            Catch ex As Exception

            End Try
        End Sub

#Region "Field Details"

        Private WithEvents StaticText0 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox0 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText1 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox1 As SAPbouiCOM.ComboBox
        Private WithEvents Grid0 As SAPbouiCOM.Grid
        Private WithEvents Button0 As SAPbouiCOM.Button
        Private WithEvents Button1 As SAPbouiCOM.Button
        Private WithEvents Button2 As SAPbouiCOM.Button
        Private WithEvents CheckBox0 As SAPbouiCOM.CheckBox
        Private WithEvents StaticText2 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox2 As SAPbouiCOM.ComboBox

#End Region

        Private Sub Button0_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button0.ClickAfter
            Try
                If ComboBox0.Selected Is Nothing Or ComboBox1.Selected Is Nothing Then
                    objaddon.objapplication.SetStatusBarMessage("Month/Year Missing", SAPbouiCOM.BoMessageTime.bmt_Long, True)
                    Exit Sub
                End If


                objaddon.objapplication.SetStatusBarMessage("Loading Details.Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Long, False)
                objform.Freeze(True)

                strsql = "CALL ""MIPL_HRMS_Provision_Report"" ('" & ComboBox0.Selected.Value.ToString & "','" & ComboBox1.Selected.Value.ToString & "')"
                Grid0.DataTable.ExecuteQuery(strsql)

                Dim Previous_MonthName As String = "", Current_MonthName As String = ""
                If Grid0.Rows.Count > 0 Then Previous_MonthName = Grid0.DataTable.GetValue("Previous", 0) : Current_MonthName = Grid0.DataTable.GetValue("Current", 0)

                Grid0.Columns.Item(0).Visible = False
                Grid0.Columns.Item(Grid0.Columns.Count - 1).Visible = False
                Grid0.Columns.Item(Grid0.Columns.Count - 2).Visible = False
                Grid0.Columns.Item(Grid0.Columns.Count - 3).Visible = False
                Grid0.Columns.Item(Grid0.Columns.Count - 4).Visible = False

                Dim ocol1 As SAPbouiCOM.EditTextColumn
                ocol1 = Grid0.Columns.Item("Emp ID")

                ocol1.LinkedObjectType = "OHEM"

                Dim ocol As SAPbouiCOM.EditTextColumn

                ocol = Grid0.Columns.Item("P_GratuityDays")
                ocol.ForeColor = Color.Red.ToArgb
                ocol.ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                ocol.RightJustified = True
                ocol.TitleObject.Caption = Previous_MonthName.ToString + " Gratuity Days"

                ocol = Grid0.Columns.Item("C_GratuityDays")
                ocol.ForeColor = Color.Red.ToArgb
                ocol.ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                ocol.RightJustified = True
                ocol.TitleObject.Caption = Current_MonthName.ToString + " Gratuity Days"

                ocol = Grid0.Columns.Item("GratuityDays")
                ocol.ForeColor = Color.Red.ToArgb
                ocol.ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                ocol.RightJustified = True
                ocol.TextStyle = FontStyle.Bold
                ocol.TitleObject.Caption = "Accrued Gratuity Days"

                ocol = Grid0.Columns.Item("P_Gratuity")
                ocol.ForeColor = Color.Red.ToArgb
                ocol.ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                ocol.RightJustified = True
                ocol.TitleObject.Caption = Previous_MonthName.ToString + " Gratuity"

                ocol = Grid0.Columns.Item("C_Gratuity")
                ocol.ForeColor = Color.Red.ToArgb
                ocol.ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                ocol.RightJustified = True
                ocol.TitleObject.Caption = Current_MonthName.ToString + " Gratuity"

                ocol = Grid0.Columns.Item("Gratuity")
                ocol.ForeColor = Color.Red.ToArgb
                ocol.ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                ocol.RightJustified = True
                ocol.TextStyle = FontStyle.Bold
                ocol.TitleObject.Caption = "Accrued Gratuity"

                ocol = Grid0.Columns.Item("Air_EligibleAmt")
                ocol.RightJustified = True

                ocol = Grid0.Columns.Item("P_AirTicket")
                ocol.ForeColor = Color.Green.ToArgb
                ocol.ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                ocol.RightJustified = True
                ocol.TitleObject.Caption = Previous_MonthName.ToString + " AirTicket"

                ocol = Grid0.Columns.Item("C_AirTicket")
                ocol.ForeColor = Color.Green.ToArgb
                ocol.ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                ocol.RightJustified = True
                ocol.TitleObject.Caption = Current_MonthName.ToString + " AirTicket"

                ocol = Grid0.Columns.Item("AirTicket")
                ocol.ForeColor = Color.Green.ToArgb
                ocol.ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                ocol.RightJustified = True
                ocol.TextStyle = FontStyle.Bold
                ocol.TitleObject.Caption = "Accrued Air Ticket"

                ocol = Grid0.Columns.Item("P_Leave")
                ocol.ForeColor = Color.DarkMagenta.ToArgb
                ocol.ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                ocol.RightJustified = True
                ocol.TitleObject.Caption = Previous_MonthName.ToString + " Leave Salary"

                ocol = Grid0.Columns.Item("C_Leave")
                ocol.ForeColor = Color.DarkMagenta.ToArgb
                ocol.ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                ocol.RightJustified = True
                ocol.TitleObject.Caption = Current_MonthName.ToString + " Leave  Salary"

                ocol = Grid0.Columns.Item("Leave")
                ocol.ForeColor = Color.DarkMagenta.ToArgb
                ocol.ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                ocol.RightJustified = True
                ocol.TextStyle = FontStyle.Bold
                ocol.TitleObject.Caption = "Accrued Leave Salary"

                ocol = Grid0.Columns.Item("P_leavebalance")
                ocol.ForeColor = Color.DarkMagenta.ToArgb
                ocol.ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                ocol.RightJustified = True
                ocol.TitleObject.Caption = Previous_MonthName.ToString + " Leave Days"

                ocol = Grid0.Columns.Item("C_Leavebalance")
                ocol.ForeColor = Color.DarkMagenta.ToArgb
                ocol.ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                ocol.RightJustified = True
                ocol.TitleObject.Caption = Current_MonthName.ToString + " Leave Days"

                ocol = Grid0.Columns.Item("LeaveBalance")
                ocol.ForeColor = Color.DarkMagenta.ToArgb
                ocol.ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                ocol.RightJustified = True
                ocol.TextStyle = FontStyle.Bold
                ocol.TitleObject.Caption = "Accrued Leave Days"


                objaddon.objapplication.Menus.Item("1300").Activate()

                If Grid0.DataTable.GetValue("Finalize", 0).ToString.ToUpper = "Y" Then
                    Button1.Item.Enabled = False
                    CheckBox0.Item.Enabled = True
                    CheckBox0.Checked = True
                    CheckBox0.Item.Enabled = False
                Else
                    Button1.Item.Enabled = True
                    CheckBox0.Item.Enabled = True
                    CheckBox0.Checked = False
                    CheckBox0.Item.Enabled = False
                End If

                If ComboBox2.Selected.Value.ToString.ToUpper <> "-1" Then
                    Grid0.Columns.Item("P_GratuityDays").Visible = False
                    Grid0.Columns.Item("C_GratuityDays").Visible = False
                    Grid0.Columns.Item("GratuityDays").Visible = False
                    Grid0.Columns.Item("P_Gratuity").Visible = False
                    Grid0.Columns.Item("C_Gratuity").Visible = False
                    Grid0.Columns.Item("Gratuity").Visible = False

                    Grid0.Columns.Item("Last Booked Date").Visible = False
                    Grid0.Columns.Item("Air_EligibleYear").Visible = False
                    Grid0.Columns.Item("Air_EligibleAmt").Visible = False
                    Grid0.Columns.Item("P_AirTicket").Visible = False
                    Grid0.Columns.Item("C_AirTicket").Visible = False
                    Grid0.Columns.Item("AirTicket").Visible = False

                    Grid0.Columns.Item("C_Leavebalance").Visible = False
                    Grid0.Columns.Item("P_leavebalance").Visible = False
                    Grid0.Columns.Item("LeaveBalance").Visible = False
                    Grid0.Columns.Item("P_Leave").Visible = False
                    Grid0.Columns.Item("C_Leave").Visible = False
                    Grid0.Columns.Item("Leave").Visible = False
                End If
                If ComboBox2.Selected.Value.ToString.ToUpper = "G" Then
                    Grid0.Columns.Item("P_GratuityDays").Visible = True
                    Grid0.Columns.Item("C_GratuityDays").Visible = True
                    Grid0.Columns.Item("GratuityDays").Visible = True
                    Grid0.Columns.Item("P_Gratuity").Visible = True
                    Grid0.Columns.Item("C_Gratuity").Visible = True
                    Grid0.Columns.Item("Gratuity").Visible = True
                ElseIf ComboBox2.Selected.Value.ToString.ToUpper = "A" Then
                    Grid0.Columns.Item("Last Booked Date").Visible = True
                    Grid0.Columns.Item("Air_EligibleYear").Visible = True
                    Grid0.Columns.Item("Air_EligibleAmt").Visible = True
                    Grid0.Columns.Item("P_AirTicket").Visible = True
                    Grid0.Columns.Item("C_AirTicket").Visible = True
                    Grid0.Columns.Item("AirTicket").Visible = True
                ElseIf ComboBox2.Selected.Value.ToString.ToUpper = "L" Then
                    Grid0.Columns.Item("C_Leavebalance").Visible = True
                    Grid0.Columns.Item("P_leavebalance").Visible = True
                    Grid0.Columns.Item("LeaveBalance").Visible = True
                    Grid0.Columns.Item("P_Leave").Visible = True
                    Grid0.Columns.Item("C_Leave").Visible = True
                    Grid0.Columns.Item("Leave").Visible = True
                End If

                Grid0.CommonSetting.FixedColumnsCount = 8

                For i As Integer = 0 To Grid0.Columns.Count - 1
                    Grid0.Columns.Item(i).TitleObject.Sortable = True
                Next
                objform.Freeze(False)
                objaddon.objapplication.StatusBar.SetText("Details Loaded Successfully.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
            Catch ex As Exception
                objform.Freeze(False)
            End Try

        End Sub

        Private Sub Button1_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button1.ClickAfter
            If objaddon.objapplication.MessageBox("Do you want to finalize and post the Journal vouchers?", 2, "Yes", "No") = 1 Then
                Try
                    strsql = "Update ""HRMS_PROVISION_DETAILS"" set ""Finalize""='Y' where To_Varchar(""ProvisionDate"",'MM')='" & ComboBox0.Selected.Value.ToString & "' and To_Varchar(""ProvisionDate"",'YYYY')='" & ComboBox1.Selected.Value.ToString & "'"
                    objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    objrs.DoQuery(strsql)
                    Button1.Item.Enabled = False
                    CheckBox0.Item.Enabled = True
                    CheckBox0.Checked = True
                    CheckBox0.Item.Enabled = False
                    objaddon.objapplication.SetStatusBarMessage("Provison Finalized Successfully", SAPbouiCOM.BoMessageTime.bmt_Short, False)
                Catch ex As Exception

                End Try
            End If
        End Sub

        Private Sub Grid0_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Grid0.ClickAfter
            If pVal.Row = -1 Then Exit Sub
            If lastrowid <> -1 Then Grid0.CommonSetting.SetRowBackColor(lastrowid, Grid0.Item.BackColor)
            Grid0.CommonSetting.SetRowBackColor(pVal.Row + 1, Color.PaleGoldenrod.ToArgb)
            lastrowid = pVal.Row + 1
        End Sub

        Private Sub Grid0_LinkPressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Grid0.LinkPressedAfter
            If pVal.Row = -1 Then Exit Sub
            If pVal.ColUID = "Emp ID" Then
                Link_objtype = "OHEM"
                Link_Value = Grid0.DataTable.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Value.ToString
                Dim oactiveform As New frmEmployeeMaster
                oactiveform.Show()
            End If
        End Sub
        Dim posted_entryno As String
        Dim lretcode
        Private Sub Provision_posting_JV()
            Try

                Dim objrsheader As SAPbobsCOM.Recordset
                objrsheader = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strsql = "select Distinct Docentry from HRMS_PROVISION_DETAILS WHere Isnull(JENO,'')='' and isnull(finalize,'')='Y'"
                objrsheader.DoQuery(strsql)


                strsql = " [Innova_HRMS_Provision_Posting] '" & objrsheader.Fields.Item("Docentry").Value.ToString & "'"
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strsql)

                Try
                    If Not objaddon.objcompany.InTransaction Then objaddon.objcompany.StartTransaction()
                    Dim OprovisionJE As SAPbobsCOM.JournalVouchers
                    OprovisionJE = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalVouchers)

                    OprovisionJE.JournalEntries.ReferenceDate = objrs.Fields.Item("ProvisionDate").Value
                    OprovisionJE.JournalEntries.DueDate = objrs.Fields.Item("ProvisionDate").Value
                    OprovisionJE.JournalEntries.TaxDate = objrs.Fields.Item("ProvisionDate").Value

                    If objrs.Fields.Item("Transcode").Value.ToString <> "" Then OprovisionJE.JournalEntries.TransactionCode = objrs.Fields.Item("Transcode").Value.ToString

                    OprovisionJE.JournalEntries.Memo = "Leave Salary Provision"
                    OprovisionJE.JournalEntries.UserFields.Fields.Item("U_Narration").Value = "Leave Salary Provision " + objrs.Fields.Item("Period").Value.ToString
                    OprovisionJE.JournalEntries.Reference = objrs.Fields.Item("Period").Value.ToString
                    OprovisionJE.JournalEntries.Reference2 = "Leave Salary Provision"
                    'If objrs.Fields.Item("Ref3").Value.ToString <> "" Then OprovisionJE.JournalEntries.Reference3 = objrs.Fields.Item("Ref3").Value.ToString


                    OprovisionJE.JournalEntries.Lines.AccountCode = objrs.Fields.Item("Leave_debitCode").Value
                    OprovisionJE.JournalEntries.Lines.Debit = objrs.Fields.Item("Leave_Amount").Value
                    'OprovisionJE.JournalEntries.Lines.Reference1 = objrs.Fields.Item("Lref1").Value 'OprovisionJE.JournalEntries.Lines.Reference2 = objrs.Fields.Item("Lref2").Value 'OprovisionJE.JournalEntries.Lines.AdditionalReference = objrs.Fields.Item("Lref3").Value

                    'If objrs.Fields.Item("Ocrcode1").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode = objrs.Fields.Item("Ocrcode1").Value
                    'If objrs.Fields.Item("Ocrcode2").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode2 = objrs.Fields.Item("Ocrcode2").Value
                    'If objrs.Fields.Item("Ocrcode3").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode3 = objrs.Fields.Item("Ocrcode3").Value
                    'If objrs.Fields.Item("Ocrcode4").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode4 = objrs.Fields.Item("Ocrcode4").Value
                    'If objrs.Fields.Item("Ocrcode5").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode5 = objrs.Fields.Item("Ocrcode5").Value
                    OprovisionJE.JournalEntries.Lines.Add()

                    OprovisionJE.JournalEntries.Lines.AccountCode = objrs.Fields.Item("Leave_CreditCode").Value
                    OprovisionJE.JournalEntries.Lines.Credit = objrs.Fields.Item("Leave_Amount").Value
                    'OprovisionJE.JournalEntries.Lines.Reference1 = objrs.Fields.Item("Lref1").Value 'OprovisionJE.JournalEntries.Lines.Reference2 = objrs.Fields.Item("Lref2").Value 'OprovisionJE.JournalEntries.Lines.AdditionalReference = objrs.Fields.Item("Lref3").Value

                    'If objrs.Fields.Item("Ocrcode1").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode = objrs.Fields.Item("Ocrcode1").Value
                    'If objrs.Fields.Item("Ocrcode2").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode2 = objrs.Fields.Item("Ocrcode2").Value
                    'If objrs.Fields.Item("Ocrcode3").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode3 = objrs.Fields.Item("Ocrcode3").Value
                    'If objrs.Fields.Item("Ocrcode4").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode4 = objrs.Fields.Item("Ocrcode4").Value
                    'If objrs.Fields.Item("Ocrcode5").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode5 = objrs.Fields.Item("Ocrcode5").Value
                    OprovisionJE.JournalEntries.Lines.Add()


                    OprovisionJE.JournalEntries.Add()



                    OprovisionJE.JournalEntries.ReferenceDate = objrs.Fields.Item("ProvisionDate").Value
                    OprovisionJE.JournalEntries.DueDate = objrs.Fields.Item("ProvisionDate").Value
                    OprovisionJE.JournalEntries.TaxDate = objrs.Fields.Item("ProvisionDate").Value

                    If objrs.Fields.Item("Transcode").Value.ToString <> "" Then OprovisionJE.JournalEntries.TransactionCode = objrs.Fields.Item("Transcode").Value.ToString

                    OprovisionJE.JournalEntries.Memo = "Air Ticket Provision"
                    OprovisionJE.JournalEntries.UserFields.Fields.Item("U_Narration").Value = "Air Ticket Provision " + objrs.Fields.Item("Period").Value.ToString
                    OprovisionJE.JournalEntries.Reference = objrs.Fields.Item("Period").Value.ToString
                    OprovisionJE.JournalEntries.Reference2 = "Air Ticket Provision"
                    'If objrs.Fields.Item("Ref3").Value.ToString <> "" Then OprovisionJE.JournalEntries.Reference3 = objrs.Fields.Item("Ref3").Value.ToString


                    OprovisionJE.JournalEntries.Lines.AccountCode = objrs.Fields.Item("Air_debitCode").Value
                    OprovisionJE.JournalEntries.Lines.Debit = objrs.Fields.Item("AirTicket_Amount").Value
                    'OprovisionJE.JournalEntries.Lines.Reference1 = objrs.Fields.Item("Lref1").Value 'OprovisionJE.JournalEntries.Lines.Reference2 = objrs.Fields.Item("Lref2").Value 'OprovisionJE.JournalEntries.Lines.AdditionalReference = objrs.Fields.Item("Lref3").Value

                    'If objrs.Fields.Item("Ocrcode1").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode = objrs.Fields.Item("Ocrcode1").Value
                    'If objrs.Fields.Item("Ocrcode2").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode2 = objrs.Fields.Item("Ocrcode2").Value
                    'If objrs.Fields.Item("Ocrcode3").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode3 = objrs.Fields.Item("Ocrcode3").Value
                    'If objrs.Fields.Item("Ocrcode4").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode4 = objrs.Fields.Item("Ocrcode4").Value
                    'If objrs.Fields.Item("Ocrcode5").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode5 = objrs.Fields.Item("Ocrcode5").Value
                    OprovisionJE.JournalEntries.Lines.Add()

                    OprovisionJE.JournalEntries.Lines.AccountCode = objrs.Fields.Item("Air_CreditCode").Value
                    OprovisionJE.JournalEntries.Lines.Credit = objrs.Fields.Item("AirTicket_Amount").Value
                    'OprovisionJE.JournalEntries.Lines.Reference1 = objrs.Fields.Item("Lref1").Value 'OprovisionJE.JournalEntries.Lines.Reference2 = objrs.Fields.Item("Lref2").Value 'OprovisionJE.JournalEntries.Lines.AdditionalReference = objrs.Fields.Item("Lref3").Value

                    'If objrs.Fields.Item("Ocrcode1").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode = objrs.Fields.Item("Ocrcode1").Value
                    'If objrs.Fields.Item("Ocrcode2").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode2 = objrs.Fields.Item("Ocrcode2").Value
                    'If objrs.Fields.Item("Ocrcode3").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode3 = objrs.Fields.Item("Ocrcode3").Value
                    'If objrs.Fields.Item("Ocrcode4").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode4 = objrs.Fields.Item("Ocrcode4").Value
                    'If objrs.Fields.Item("Ocrcode5").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode5 = objrs.Fields.Item("Ocrcode5").Value
                    OprovisionJE.JournalEntries.Lines.Add()

                    OprovisionJE.JournalEntries.Add()



                    OprovisionJE.JournalEntries.ReferenceDate = objrs.Fields.Item("ProvisionDate").Value
                    OprovisionJE.JournalEntries.DueDate = objrs.Fields.Item("ProvisionDate").Value
                    OprovisionJE.JournalEntries.TaxDate = objrs.Fields.Item("ProvisionDate").Value

                    If objrs.Fields.Item("Transcode").Value.ToString <> "" Then OprovisionJE.JournalEntries.TransactionCode = objrs.Fields.Item("Transcode").Value.ToString

                    OprovisionJE.JournalEntries.Memo = "Gratuity Provision"
                    OprovisionJE.JournalEntries.UserFields.Fields.Item("U_Narration").Value = "Gratuity Provision " + objrs.Fields.Item("Period").Value.ToString
                    OprovisionJE.JournalEntries.Reference = objrs.Fields.Item("Period").Value.ToString
                    OprovisionJE.JournalEntries.Reference2 = "Gratuity Provision"
                    'If objrs.Fields.Item("Ref3").Value.ToString <> "" Then OprovisionJE.JournalEntries.Reference3 = objrs.Fields.Item("Ref3").Value.ToString


                    OprovisionJE.JournalEntries.Lines.AccountCode = objrs.Fields.Item("Gratuity_debitCode").Value
                    OprovisionJE.JournalEntries.Lines.Debit = objrs.Fields.Item("Gratuity_Amount").Value
                    'OprovisionJE.JournalEntries.Lines.Reference1 = objrs.Fields.Item("Lref1").Value 'OprovisionJE.JournalEntries.Lines.Reference2 = objrs.Fields.Item("Lref2").Value 'OprovisionJE.JournalEntries.Lines.AdditionalReference = objrs.Fields.Item("Lref3").Value

                    'If objrs.Fields.Item("Ocrcode1").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode = objrs.Fields.Item("Ocrcode1").Value
                    'If objrs.Fields.Item("Ocrcode2").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode2 = objrs.Fields.Item("Ocrcode2").Value
                    'If objrs.Fields.Item("Ocrcode3").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode3 = objrs.Fields.Item("Ocrcode3").Value
                    'If objrs.Fields.Item("Ocrcode4").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode4 = objrs.Fields.Item("Ocrcode4").Value
                    'If objrs.Fields.Item("Ocrcode5").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode5 = objrs.Fields.Item("Ocrcode5").Value
                    OprovisionJE.JournalEntries.Lines.Add()

                    OprovisionJE.JournalEntries.Lines.AccountCode = objrs.Fields.Item("Gratuity_CreditCode").Value
                    OprovisionJE.JournalEntries.Lines.Credit = objrs.Fields.Item("Gratuity_Amount").Value
                    'OprovisionJE.JournalEntries.Lines.Reference1 = objrs.Fields.Item("Lref1").Value 'OprovisionJE.JournalEntries.Lines.Reference2 = objrs.Fields.Item("Lref2").Value 'OprovisionJE.JournalEntries.Lines.AdditionalReference = objrs.Fields.Item("Lref3").Value

                    'If objrs.Fields.Item("Ocrcode1").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode = objrs.Fields.Item("Ocrcode1").Value
                    'If objrs.Fields.Item("Ocrcode2").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode2 = objrs.Fields.Item("Ocrcode2").Value
                    'If objrs.Fields.Item("Ocrcode3").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode3 = objrs.Fields.Item("Ocrcode3").Value
                    'If objrs.Fields.Item("Ocrcode4").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode4 = objrs.Fields.Item("Ocrcode4").Value
                    'If objrs.Fields.Item("Ocrcode5").Value.ToString <> "" Then OprovisionJE.JournalEntries.Lines.CostingCode5 = objrs.Fields.Item("Ocrcode5").Value
                    OprovisionJE.JournalEntries.Lines.Add()

                    OprovisionJE.JournalEntries.Add()

                    lretcode = OprovisionJE.Add()
                    If lretcode <> 0 Then
                        If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                        objaddon.objglobalmethods.status_Update("PROV", objrsheader.Fields.Item("Docentry").Value.ToString, 0, objaddon.objcompany.GetLastErrorDescription, -1)
                    Else
                        objaddon.objcompany.GetNewObjectCode(posted_entryno)
                        objaddon.objglobalmethods.status_Update("PROV", objrsheader.Fields.Item("Docentry").Value.ToString, 1, "Success", posted_entryno.ToString)
                        If objaddon.objglobalmethods.Update_query("update HRMS_PROVISION_DETAILS set jeno='" & posted_entryno & "' where Docentry='" & objrsheader.Fields.Item("Docentry").Value.ToString & "'") Then
                            If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                        Else
                            If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                        End If
                    End If

                Catch ex As Exception
                    If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                    objaddon.objglobalmethods.status_Update("PROV", objrsheader.Fields.Item("Docentry").Value.ToString, 0, ex.Message.ToString, -1)
                End Try

            Catch ex As Exception

            End Try
        End Sub

        Private Sub CheckBox0_PressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles CheckBox0.PressedAfter
            If CheckBox0.Checked = True Then
                CheckBox0.Item.Enabled = False
            End If

        End Sub
    End Class
End Namespace

