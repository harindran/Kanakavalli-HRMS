Option Strict Off
Option Explicit On

Imports SAPbouiCOM
Imports SAPbouiCOM.Framework

Namespace HRMS
    <FormAttribute("FRMHSTRY", "CommonForms/FrmHistory.b1f")>
    Friend Class FrmHistory
        Inherits UserFormBase
        Private WithEvents objform As SAPbouiCOM.Form
        Dim FormCount As Integer = 0
        Dim strsql As String
        Private WithEvents col As EditTextColumn
        Private WithEvents olabel As SAPbouiCOM.StaticText

        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.Grid0 = CType(Me.GetItem("grdheader").Specific, SAPbouiCOM.Grid)
            Me.StaticText0 = CType(Me.GetItem("Item_0").Specific, SAPbouiCOM.StaticText)
            Me.Grid1 = CType(Me.GetItem("Item_2").Specific, SAPbouiCOM.Grid)
            Me.StaticText2 = CType(Me.GetItem("Item_3").Specific, SAPbouiCOM.StaticText)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()
            AddHandler ResizeAfter, AddressOf Me.FrmHistory_ResizeAfter

        End Sub

#Region "Field Details"
        Private WithEvents Grid0 As SAPbouiCOM.Grid
        Private WithEvents StaticText0 As SAPbouiCOM.StaticText
        Private WithEvents Grid1 As SAPbouiCOM.Grid
        Private WithEvents StaticText2 As SAPbouiCOM.StaticText
#End Region

        Private Sub OnCustomInitialize()
            Try
                objform = objaddon.objapplication.Forms.GetForm("FRMHSTRY", FormCount)
                objform = objaddon.objapplication.Forms.ActiveForm
                objform.Freeze(True)

                Select Case Link_objtype.ToString.ToUpper
                    Case "TRANOLAP_SHD" : LoanApplicaiton(objform, Link_Value, Link_Value_Additional, "SHD")
                    Case "TRANOLAP_FHD" : LoanApplicaiton(objform, Link_Value, "-1", "FHD")
                    Case "TRANOLVA" : LeaveApplication(objform, Link_Value)
                    Case "OTIS_HD" : AirTicketIssueform(objform, Link_Value, "HD")
                    Case "OTIS_SP" : AirTicketIssueform(objform, Link_Value, "SP")
                    Case "OLSE_HD" : SettlementForm(objform, Link_Value)
                    Case "OPRC_PJE" : PayrollProcess_ProvisionalJE(objform, Link_Value)
                    Case "PROV_PJE" : ProvisionProcess_ProvisionalJE(objform, Link_Value)
                End Select

                Link_objtype = "-1"
                Link_Value = "-1"

                objform = objaddon.objapplication.Forms.ActiveForm
                objaddon.objapplication.Menus.Item("1300").Activate()
                objform.Freeze(False)
            Catch ex As Exception
                objaddon.objapplication.SetStatusBarMessage("Error in History Details.", BoMessageTime.bmt_Short, True)
                Link_objtype = "-1"
                Link_Value = "-1"
                objform.Freeze(False)
            End Try
        End Sub

        Private Sub LoanApplicaiton(ByVal objform As SAPbouiCOM.Form, ByVal empid As String, ByVal loancode As String, ByVal menuid As String)
            Try

                objform = objaddon.objapplication.Forms.ActiveForm

                strsql = "CALL ""MIPL_HRMS_LoanApplicaiton_History"" ('" & empid.ToString & "','H','-1','" & loancode.ToString & "')"
                Grid0.DataTable.ExecuteQuery(strsql)

                If Grid0.DataTable.Rows.Count = 0 Or Grid0.DataTable.Columns.Count = 0 Then
                    objaddon.objapplication.SetStatusBarMessage("No History Found", BoMessageTime.bmt_Short, True)
                    objform.Close()
                    Exit Sub
                End If

                col = Grid0.Columns.Item(0)
                col.LinkedObjectType = "OLOA"

                Grid0.Item.Description = "Loan Application Details"
                Grid1.Item.Description = "Loan Installment Details"

                olabel = objform.Items.Item("Item_0").Specific
                If menuid.ToString = "SHD" Then
                    objform.Title = "" & Grid0.DataTable.Columns.Item("Loan Type").Cells.Item(0).Value.ToString & " History Details - Loan Application"
                    olabel.Caption = "Employee ID : " & Grid0.DataTable.Columns.Item("header").Cells.Item(0).Value.ToString & "   &   Loan Type : " & Grid0.DataTable.Columns.Item("Loan Type").Cells.Item(0).Value.ToString & " "
                    Grid0.Columns.Item("Loan Type").Visible = False
                Else
                    objform.Title = "Full History Details - Loan Application"
                    olabel.Caption = "Employee ID : " & Grid0.DataTable.Columns.Item("header").Cells.Item(0).Value.ToString & ""
                End If

                objaddon.objglobalmethods.HeaderLabel_Color(StaticText0.Item, 13, Color.Red.ToArgb, 15)
                objaddon.objglobalmethods.HeaderLabel_Color(StaticText2.Item, 13, Color.Red.ToArgb, 15)

                Grid0.Columns.Item("header").Visible = False
                Grid0.Columns.Item("objtype").Visible = False

                'OneGrid()
                Grid0.Rows.SelectedRows.Add(0)

                If Grid0.DataTable.Columns.Item("objtype").Cells.Item(0).Value.ToString.ToUpper = "OLOA" Then
                    strsql = "CALL ""MIPL_HRMS_LoanApplicaiton_History"" ('-1','D','" & Grid0.DataTable.Columns.Item(0).Cells.Item(0).Value.ToString & "','-1')"
                    Grid1.DataTable.ExecuteQuery(strsql)
                End If

                TwoGrid_Visible(True)
            Catch ex As Exception

            End Try
        End Sub

        Private Sub LeaveApplication(ByVal objform As SAPbouiCOM.Form, ByVal empid As String)
            objform = objaddon.objapplication.Forms.ActiveForm
            objform.Title = "History Details - Leave Application"

            strsql = "CALL ""MIPL_HRMS_LeaveApplicaiton_History"" ('" & empid & "','-1','-1')"
            Grid0.DataTable.ExecuteQuery(strsql)

            If Grid0.DataTable.Rows.Count = 0 Or Grid0.DataTable.Columns.Count = 0 Then
                objaddon.objapplication.SetStatusBarMessage("No History Found", BoMessageTime.bmt_Short, True)
                objform.Close()
                Exit Sub
            End If

            col = Grid0.Columns.Item(0)
            col.LinkedObjectType = "OLVA"

            Grid0.Item.Description = "Leave Application Details"
            'Grid1.Item.Description = "Loan Installment Details"


            olabel = objform.Items.Item("Item_0").Specific
            olabel.Caption = "Employee ID : " & Grid0.DataTable.Columns.Item("header").Cells.Item(0).Value.ToString

            objaddon.objglobalmethods.HeaderLabel_Color(StaticText0.Item, 13, Color.Red.ToArgb, 15)
            objaddon.objglobalmethods.HeaderLabel_Color(StaticText2.Item, 13, Color.Red.ToArgb, 15)

            Grid0.Columns.Item("header").Visible = False
            Grid0.Columns.Item("objtype").Visible = False
            'OneGrid()
        End Sub

        Private Sub TwoGrid_Visible(Optional ByVal twogrid As Boolean = False)
            Try
                objform = objaddon.objapplication.Forms.ActiveForm
                If twogrid Then
                    Grid1.Item.Visible = True
                    StaticText2.Item.Visible = True
                    objform.Height = objform.Height * 1.5 + 30
                    Grid0.Item.Height = objform.Height / 3
                    Grid1.Item.Height = objform.Height / 2 - 5
                    Grid1.Item.Top = Grid0.Item.Height + Grid0.Item.Top + 20
                    StaticText2.Item.Top = Grid0.Item.Height + Grid0.Item.Top
                Else
                    Grid1.Item.Visible = False
                    StaticText2.Item.Visible = False
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Grid0_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Grid0.ClickAfter
            Try
                If Grid1.Item.Visible = False Then Exit Sub
                'If pVal.ColUID <> "RowsHeader" Then Exit Sub
                If pVal.Row = -1 Then Exit Sub
                Grid0.Rows.SelectedRows.Add(pVal.Row)
                If Grid0.DataTable.Columns.Item("objtype").Cells.Item(pVal.Row).Value.ToString.ToUpper = "OLOA" Then
                    strsql = "CALL ""MIPL_HRMS_LoanApplicaiton_History"" ('-1','D','" & Grid0.DataTable.Columns.Item(0).Cells.Item(pVal.Row).Value.ToString & "','-1')"
                Else
                    strsql = ""
                End If

                If strsql <> "" Then Grid1.DataTable.ExecuteQuery(strsql)


            Catch ex As Exception
                objaddon.objapplication.SetStatusBarMessage("Error in Loading Detail.Please Check it.", BoMessageTime.bmt_Short, True)
            End Try
        End Sub

        Private Sub Grid0_LinkPressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Grid0.LinkPressedAfter
            Try

                If pVal.Row = -1 Then Exit Sub
                Select Case Grid0.DataTable.Columns.Item("objtype").Cells.Item(pVal.Row).Value.ToString.ToUpper
                    Case "OLOA"
                        Link_Value = Grid0.DataTable.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Value.ToString
                        Link_objtype = "OLOA"
                        Dim activeform As New frmLoanApplication
                        activeform.Show()
                    Case "OLVA"
                        Link_Value = Grid0.DataTable.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Value.ToString
                        Link_objtype = "OLVA"
                        Dim activeform As New frmLeaveApplicaiton
                        activeform.Show()
                    Case "OTIS"
                        Link_Value = Grid0.DataTable.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Value.ToString
                        Link_objtype = "OTIS"
                        Dim activeform As New frmAirTicketIssue
                        activeform.Show()
                    Case "OLSE"
                        Link_Value = Grid0.DataTable.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Value.ToString
                        Link_objtype = "OLSE"
                        Dim activeform As New FrmSettlment
                        activeform.Show()
                End Select
            Catch ex As Exception

            End Try
        End Sub

        Private Sub AirTicketIssueform(ByVal objform As SAPbouiCOM.Form, ByVal empid As String, ByVal menuid As String)
            Try

                objform = objaddon.objapplication.Forms.ActiveForm
                'OneGrid()

                If menuid.ToString.ToUpper = "HD" Then
                    objform.Title = "History Details - Air ticket Issue"
                    Grid0.Item.Description = " Air ticket Issue Details"
                    strsql = "CALL ""MIPL_HR_Airticket_History"" ('" & empid.ToString & "','OITS')"
                ElseIf menuid.ToString.ToUpper = "SP" Then
                    objform.Title = "Eligible Amount Details - Air ticket Issue"
                    Grid0.Item.Description = " Air ticket Eligible Amount Details"
                    strsql = "CALL ""MIPL_HR_Airticket_History"" ('" & empid.ToString & "','OHEM')"
                End If

                Grid0.DataTable.ExecuteQuery(strsql)

                If Grid0.DataTable.Rows.Count = 0 Or Grid0.DataTable.Columns.Count = 0 Then
                    objaddon.objapplication.SetStatusBarMessage("No Details Found", BoMessageTime.bmt_Short, True)
                    objform.Close()
                    Exit Sub
                ElseIf Grid0.DataTable.Rows.Count = 1 Then
                    Try
                        If Grid0.DataTable.GetValue("Empid", 0).ToString = "" Then
                            objaddon.objapplication.SetStatusBarMessage("No Details Found", BoMessageTime.bmt_Short, True)
                            objform.Close()
                            Exit Sub
                        End If
                    Catch ex As Exception

                    End Try
                End If

                If menuid.ToString.ToUpper = "HD" Then
                    col = Grid0.Columns.Item("DocEntry")
                    col.LinkedObjectType = "OTIS"
                    Grid0.Item.Description = "History Details - Air Ticket Issue"
                Else
                    Grid0.Item.Description = "Eligibility Details - Air Ticket"

                End If

                olabel = objform.Items.Item("Item_0").Specific
                olabel.Caption = "Employee ID : " & Grid0.DataTable.Columns.Item("Empid").Cells.Item(0).Value.ToString & " - " & Grid0.DataTable.Columns.Item("Employee Name").Cells.Item(0).Value.ToString

                objaddon.objglobalmethods.HeaderLabel_Color(StaticText0.Item, 13, Color.Red.ToArgb, 15)
                'objaddon.objglobalmethods.HeaderLabel_Color(StaticText2.Item, 13, Color.Red.ToArgb, 15)

                'Grid0.Columns.Item("header").Visible = False
                Grid0.Columns.Item("objtype").Visible = False
                Grid0.Columns.Item("Employee Name").Visible = False
                Grid0.Columns.Item("Empid").Visible = False

                Grid0.Rows.SelectedRows.Add(0)
            Catch ex As Exception

            End Try
        End Sub

        Private Sub SettlementForm(ByVal objform As SAPbouiCOM.Form, ByVal empid As String)
            Try

                objform = objaddon.objapplication.Forms.ActiveForm
                'OneGrid()

                objform.Title = "History Details - Settlements"
                Grid0.Item.Description = " Settlement Details"
                strsql = "CALL ""MIPL_HRMS_Settlement_History"" ('" & empid.ToString & "')"
                Grid0.DataTable.ExecuteQuery(strsql)

                If Grid0.DataTable.Rows.Count = 0 Or Grid0.DataTable.Columns.Count = 0 Then
                    objaddon.objapplication.SetStatusBarMessage("No Details Found", BoMessageTime.bmt_Short, True)
                    objform.Close()
                    Exit Sub
                ElseIf Grid0.DataTable.Rows.Count = 1 Then
                    Try
                        If Grid0.DataTable.GetValue("Empid", 0).ToString = "" Then
                            objaddon.objapplication.SetStatusBarMessage("No Details Found", BoMessageTime.bmt_Short, True)
                            objform.Close()
                            Exit Sub
                        End If
                    Catch ex As Exception

                    End Try
                End If

                col = Grid0.Columns.Item("DocEntry")
                col.LinkedObjectType = "OLSE"
                Grid0.Item.Description = "History Details - Settlements"

                olabel = objform.Items.Item("Item_0").Specific
                olabel.Caption = "Employee ID : " & Grid0.DataTable.Columns.Item("Empid").Cells.Item(0).Value.ToString & " - " & Grid0.DataTable.Columns.Item("Employee Name").Cells.Item(0).Value.ToString

                objaddon.objglobalmethods.HeaderLabel_Color(StaticText0.Item, 13, Color.Red.ToArgb, 15)
                'objaddon.objglobalmethods.HeaderLabel_Color(StaticText2.Item, 13, Color.Red.ToArgb, 15)

                'Grid0.Columns.Item("header").Visible = False
                Grid0.Columns.Item("objtype").Visible = False
                Grid0.Columns.Item("Employee Name").Visible = False
                Grid0.Columns.Item("Empid").Visible = False

                Grid0.Rows.SelectedRows.Add(0)
            Catch ex As Exception

            End Try
        End Sub

        Private Sub PayrollProcess_ProvisionalJE(ByVal objform As SAPbouiCOM.Form, ByVal Docentry As String)
            Try

                objform = objaddon.objapplication.Forms.ActiveForm
                'OneGrid()

                objform.Title = "Payroll Process Provisional JE Details"
                Grid0.Item.Description = " Payroll Process"
                strsql = "select 'OPRC'[objtype],Transid,convert(varchar,Refdate,103)[JE Date],Loctotal,Memo [Remarks],isnull(U_Narration,'')[Narration],ref1[Ref1],ref2[Ref2],ref3[Ref3],DateName(Month,Refdate)+' - '+convert(varchar,Datepart(YYYY,refdate))[period] from ojdt where BatchNum =(select left(U_jeno,CHARINDEX('	',U_jeno)-1)  from [@SMPR_OPRC] where DocEntry='" & Docentry & "')"
                Grid0.DataTable.ExecuteQuery(strsql)

                If Grid0.DataTable.Rows.Count = 0 Or Grid0.DataTable.Columns.Count = 0 Then
                    objaddon.objapplication.SetStatusBarMessage("No Details Found", BoMessageTime.bmt_Short, True)
                    objform.Close()
                    Exit Sub
                ElseIf Grid0.DataTable.Rows.Count = 1 Then
                    Try
                        If Grid0.DataTable.GetValue("Transid", 0).ToString = "" Then
                            objaddon.objapplication.SetStatusBarMessage("No Details Found", BoMessageTime.bmt_Short, True)
                            objform.Close()
                            Exit Sub
                        End If
                    Catch ex As Exception

                    End Try
                End If

                col = Grid0.Columns.Item("Transid")
                col.LinkedObjectType = 30
                Grid0.Item.Description = " Payroll Process"

                olabel = objform.Items.Item("Item_0").Specific
                olabel.Caption = "Payroll JE Posting For the Period : " & Grid0.DataTable.Columns.Item("period").Cells.Item(0).Value.ToString

                objaddon.objglobalmethods.HeaderLabel_Color(StaticText0.Item, 13, Color.Red.ToArgb, 15)
                'objaddon.objglobalmethods.HeaderLabel_Color(StaticText2.Item, 13, Color.Red.ToArgb, 15)

                Grid0.Columns.Item("objtype").Visible = False
                Grid0.Columns.Item("period").Visible = False

                Grid0.Rows.SelectedRows.Add(0)
            Catch ex As Exception

            End Try
        End Sub

        Private Sub ProvisionProcess_ProvisionalJE(ByVal objform As SAPbouiCOM.Form, ByVal Docentry As String)
            Try

                objform = objaddon.objapplication.Forms.ActiveForm
                'OneGrid()

                objform.Title = "Provision JE Details"
                Grid0.Item.Description = " Provision Process"
                strsql = "select 'OPRC'[objtype],Transid,convert(varchar,Refdate,103)[JE Date],Loctotal,Memo [Remarks],isnull(U_Narration,'')[Narration],ref1[Ref1],ref2[Ref2],ref3[Ref3],DateName(Month,Refdate)+' - '+convert(varchar,Datepart(YYYY,refdate))[period] from ojdt where BatchNum =left('" & Link_Value.ToString & "',CHARINDEX('	','" & Link_Value.ToString & "')-1)"
                Grid0.DataTable.ExecuteQuery(strsql)

                If Grid0.DataTable.Rows.Count = 0 Or Grid0.DataTable.Columns.Count = 0 Then
                    objaddon.objapplication.SetStatusBarMessage("No Details Found", BoMessageTime.bmt_Short, True)
                    objform.Close()
                    Exit Sub
                ElseIf Grid0.DataTable.Rows.Count = 1 Then
                    Try
                        If Grid0.DataTable.GetValue("Transid", 0).ToString = "" Then
                            objaddon.objapplication.SetStatusBarMessage("No Details Found", BoMessageTime.bmt_Short, True)
                            objform.Close()
                            Exit Sub
                        End If
                    Catch ex As Exception

                    End Try
                End If

                col = Grid0.Columns.Item("Transid")
                col.LinkedObjectType = 30
                Grid0.Item.Description = " Provision Process"

                olabel = objform.Items.Item("Item_0").Specific
                olabel.Caption = "Provision JE Posting For the Period : " & Grid0.DataTable.Columns.Item("period").Cells.Item(0).Value.ToString

                objaddon.objglobalmethods.HeaderLabel_Color(StaticText0.Item, 13, Color.Red.ToArgb, 15)
                'objaddon.objglobalmethods.HeaderLabel_Color(StaticText2.Item, 13, Color.Red.ToArgb, 15)

                Grid0.Columns.Item("objtype").Visible = False
                Grid0.Columns.Item("period").Visible = False

                Grid0.Rows.SelectedRows.Add(0)
            Catch ex As Exception

            End Try
        End Sub

        Private Sub FrmHistory_ResizeAfter(pVal As SAPbouiCOM.SBOItemEventArg) Handles Me.ResizeAfter
            Try
                If Grid1 Is Nothing Then Exit Sub
                If Grid1.Item.Visible = True Then
                    objform.Freeze(True)
                    Grid0.Item.Height = objform.Height / 3
                    Grid1.Item.Height = objform.Height / 2 - 5
                    Grid1.Item.Top = Grid0.Item.Height + Grid0.Item.Top + 20
                    StaticText2.Item.Top = Grid0.Item.Height + Grid0.Item.Top
                    objform.Freeze(False)
                End If
            Catch ex As Exception
                objform.Freeze(False)
            End Try
        End Sub

    End Class
End Namespace

