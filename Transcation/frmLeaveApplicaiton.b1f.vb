Option Strict Off
Option Explicit On

Imports SAPbouiCOM
Imports SAPbouiCOM.Framework

Namespace HRMS
    <FormAttribute("TRANOLVA", "Transcation/frmLeaveApplicaiton.b1f")>
    Friend Class frmLeaveApplicaiton
        Inherits UserFormBase
        Dim FormCount As Integer = 0
        Private WithEvents objform As SAPbouiCOM.Form
        Private WithEvents pCFL As SAPbouiCOM.ISBOChooseFromListEventArg
        Dim objrs As SAPbobsCOM.Recordset
        Dim strsql As String
        Private WithEvents ocombo As SAPbouiCOM.ComboBox
        Dim addupdate As Boolean = False

        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.StaticText0 = CType(Me.GetItem("lblempid").Specific, SAPbouiCOM.StaticText)
            Me.EditText0 = CType(Me.GetItem("txtiempid").Specific, SAPbouiCOM.EditText)
            Me.EditText1 = CType(Me.GetItem("txtempid").Specific, SAPbouiCOM.EditText)
            Me.StaticText1 = CType(Me.GetItem("Item_3").Specific, SAPbouiCOM.StaticText)
            Me.EditText2 = CType(Me.GetItem("txtemname").Specific, SAPbouiCOM.EditText)
            Me.StaticText2 = CType(Me.GetItem("lblDesig").Specific, SAPbouiCOM.StaticText)
            Me.EditText3 = CType(Me.GetItem("txtdesig").Specific, SAPbouiCOM.EditText)
            Me.StaticText3 = CType(Me.GetItem("lbldept").Specific, SAPbouiCOM.StaticText)
            Me.EditText4 = CType(Me.GetItem("txtdept").Specific, SAPbouiCOM.EditText)
            Me.StaticText4 = CType(Me.GetItem("lbllcno").Specific, SAPbouiCOM.StaticText)
            Me.EditText5 = CType(Me.GetItem("txtlcno").Specific, SAPbouiCOM.EditText)
            Me.StaticText5 = CType(Me.GetItem("lblncno").Specific, SAPbouiCOM.StaticText)
            Me.EditText6 = CType(Me.GetItem("txtncno").Specific, SAPbouiCOM.EditText)
            Me.StaticText6 = CType(Me.GetItem("lbldno").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox0 = CType(Me.GetItem("cmbseries").Specific, SAPbouiCOM.ComboBox)
            Me.EditText8 = CType(Me.GetItem("txtdno").Specific, SAPbouiCOM.EditText)
            Me.StaticText8 = CType(Me.GetItem("lblddate").Specific, SAPbouiCOM.StaticText)
            Me.EditText9 = CType(Me.GetItem("txtddate").Specific, SAPbouiCOM.EditText)
            Me.StaticText9 = CType(Me.GetItem("lblstatus").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox1 = CType(Me.GetItem("cmbstatus").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText10 = CType(Me.GetItem("lblltype").Specific, SAPbouiCOM.StaticText)
            Me.EditText10 = CType(Me.GetItem("txtlcode").Specific, SAPbouiCOM.EditText)
            Me.EditText11 = CType(Me.GetItem("txtlname").Specific, SAPbouiCOM.EditText)
            Me.StaticText7 = CType(Me.GetItem("lbllstart").Specific, SAPbouiCOM.StaticText)
            Me.EditText7 = CType(Me.GetItem("txtlstart").Specific, SAPbouiCOM.EditText)
            Me.StaticText11 = CType(Me.GetItem("lbllend").Specific, SAPbouiCOM.StaticText)
            Me.EditText12 = CType(Me.GetItem("txtlend").Specific, SAPbouiCOM.EditText)
            Me.StaticText12 = CType(Me.GetItem("lblldays").Specific, SAPbouiCOM.StaticText)
            Me.EditText13 = CType(Me.GetItem("txtldays").Specific, SAPbouiCOM.EditText)
            Me.StaticText13 = CType(Me.GetItem("lblleligi").Specific, SAPbouiCOM.StaticText)
            Me.EditText14 = CType(Me.GetItem("txtleligi").Specific, SAPbouiCOM.EditText)
            Me.StaticText14 = CType(Me.GetItem("lblbalance").Specific, SAPbouiCOM.StaticText)
            Me.EditText15 = CType(Me.GetItem("txtbalance").Specific, SAPbouiCOM.EditText)
            Me.StaticText15 = CType(Me.GetItem("lblrejoin").Specific, SAPbouiCOM.StaticText)
            Me.EditText16 = CType(Me.GetItem("txtrejoin").Specific, SAPbouiCOM.EditText)
            Me.StaticText16 = CType(Me.GetItem("lblreason").Specific, SAPbouiCOM.StaticText)
            Me.EditText18 = CType(Me.GetItem("txtreason").Specific, SAPbouiCOM.EditText)
            Me.StaticText18 = CType(Me.GetItem("lblrid").Specific, SAPbouiCOM.StaticText)
            Me.EditText20 = CType(Me.GetItem("txtrid").Specific, SAPbouiCOM.EditText)
            Me.StaticText19 = CType(Me.GetItem("lblrename").Specific, SAPbouiCOM.StaticText)
            Me.EditText21 = CType(Me.GetItem("txtrname").Specific, SAPbouiCOM.EditText)
            Me.StaticText20 = CType(Me.GetItem("lblloc").Specific, SAPbouiCOM.StaticText)
            Me.EditText22 = CType(Me.GetItem("txtloc").Specific, SAPbouiCOM.EditText)
            Me.CheckBox0 = CType(Me.GetItem("chkhalf").Specific, SAPbouiCOM.CheckBox)
            Me.CheckBox1 = CType(Me.GetItem("chkpayable").Specific, SAPbouiCOM.CheckBox)
            Me.CheckBox2 = CType(Me.GetItem("chkissue").Specific, SAPbouiCOM.CheckBox)
            Me.Grid0 = CType(Me.GetItem("grddetails").Specific, SAPbouiCOM.Grid)
            Me.StaticText21 = CType(Me.GetItem("Item_30").Specific, SAPbouiCOM.StaticText)
            Me.Button0 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.Button1 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.EditText23 = CType(Me.GetItem("txtentry").Specific, SAPbouiCOM.EditText)
            Me.LinkedButton0 = CType(Me.GetItem("lkempid").Specific, SAPbouiCOM.LinkedButton)
            Me.CheckBox3 = CType(Me.GetItem("chkcan").Specific, SAPbouiCOM.CheckBox)
            Me.LinkedButton1 = CType(Me.GetItem("lkltype").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton2 = CType(Me.GetItem("lkrempid").Specific, SAPbouiCOM.LinkedButton)
            Me.CheckBox4 = CType(Me.GetItem("chkapp").Specific, SAPbouiCOM.CheckBox)
            Me.StaticText17 = CType(Me.GetItem("Item_0").Specific, SAPbouiCOM.StaticText)
            Me.EditText17 = CType(Me.GetItem("txttent").Specific, SAPbouiCOM.EditText)
            Me.EditText19 = CType(Me.GetItem("txttype").Specific, SAPbouiCOM.EditText)
            Me.LinkedButton3 = CType(Me.GetItem("Item_4").Specific, SAPbouiCOM.LinkedButton)
            Me.Button2 = CType(Me.GetItem("Item_1").Specific, SAPbouiCOM.Button)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()
            'AddHandler DataLoadAfter, AddressOf Me.frmLeaveApplicaiton_DataLoadAfter
        End Sub

        Private Sub OnCustomInitialize()
            Try

                objform = objaddon.objapplication.Forms.GetForm("TRANOLVA", Me.FormCount)
                objform = objaddon.objapplication.Forms.ActiveForm
                objform.Freeze(True)
                FieldSettings()
                If objaddon.ApprovedUser() Then
                    CheckBox3.Item.Enabled = True
                    CheckBox4.Item.Enabled = True
                End If
                If Link_Value.ToString <> "" And Link_objtype.ToString.ToUpper = "OLVA" Then
                    EditText23.Item.Enabled = True
                    objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE
                    EditText23.Value = Link_Value
                    objform.Items.Item("1").Click(BoCellClickType.ct_Regular)
                    EditText0.Item.Click(BoCellClickType.ct_Regular)
                    EditText23.Item.Enabled = False
                    Link_Value = "-1" : Link_objtype = "-1"
                    LoadHistory_leavedetails()
                ElseIf Link_Value.ToString <> "" And Link_objtype.ToString.ToUpper = "OLVA_AN" Then
                    EditText0.Value = Link_Value
                    EditText10.Value = "AL"
                    EditText23.Value = objaddon.objglobalmethods.GetNextDocentry_Value("@SMPR_OLVA")
                    objform.Items.Item("txtddate").Specific.string = Now.Date.ToString("yyyyMMdd")
                    EditText7.Item.Click(BoCellClickType.ct_Regular)
                    Link_Value = "-1" : Link_objtype = "-1"
                    LoadHistory_leavedetails()
                Else
                    EditText23.Value = objaddon.objglobalmethods.GetNextDocentry_Value("@SMPR_OLVA")
                    objform.Items.Item("txtddate").Specific.string = Now.Date.ToString("yyyyMMdd")
                    EditText0.Item.Click(BoCellClickType.ct_Regular)
                End If
                ComboBox0.Select(0, SAPbouiCOM.BoSearchKey.psk_Index)
                objform.EnableMenu("1283", False) 'Remove menu
                objform.EnableMenu("1286", False) 'Close Menu
                objform.EnableMenu("1285", False) 'Restore Menu
                If objaddon.objcompany.UserName.ToString.ToUpper <> "MANAGER" Then objform.EnableMenu("6913", False) 'User Defined Field
                'If Not ApprovedUser_Employee Then objform.EnableMenu("1284", False) 'Cancel Menu
                'objform.EnableMenu("1287", True)'Duplicate Menu
                objform.Freeze(False)
            Catch ex As Exception
                objform.Freeze(False)
            End Try
        End Sub

        Private Sub FieldSettings()
            Try
                CheckBox0.Item.Height = CheckBox0.Item.Height + 2
                CheckBox1.Item.Height = CheckBox1.Item.Height + 2
                CheckBox2.Item.Height = CheckBox2.Item.Height + 2
                CheckBox3.Item.Height = CheckBox3.Item.Height + 2
                CheckBox4.Item.Height = CheckBox4.Item.Height + 2

                HeaderLabel_Color(StaticText21.Item, 13, Color.Red.ToArgb, 15)

                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cmbseries", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtdno", False, True, False)

                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtddate", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cmbstatus", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtiempid", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtempid", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtemname", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtlcode", True, True, False)
                'objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtlstart", True, True, True)
                'objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtlend", True, True, True)
                'objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtrejoin", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtdesig", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtdept", False, True, False)

                'objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtlcno", True, True, False)
                'objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtncno", True, True, False)
                'objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtrid", True, True, False)
                'objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtreason", True, True, False)
                'objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "chkhalf", True, True, False)
                'objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "chkissue", True, True, False)
                'objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "chkpayable", True, True, False)
            Catch ex As Exception

            End Try
        End Sub

        Private Sub HeaderLabel_Color(ByVal item As SAPbouiCOM.Item, ByVal fontsize As Integer, ByVal forecolor As Integer, ByVal height As Integer, Optional ByVal width As Integer = 0)
            item.TextStyle = FontStyle.Underline
            item.TextStyle = FontStyle.Bold
            item.FontSize = fontsize
            item.ForeColor = forecolor
            item.Height = height
        End Sub

#Region "Field Details"

        Private WithEvents StaticText0 As SAPbouiCOM.StaticText
        Private WithEvents EditText0 As SAPbouiCOM.EditText
        Private WithEvents EditText1 As SAPbouiCOM.EditText
        Private WithEvents StaticText1 As SAPbouiCOM.StaticText
        Private WithEvents EditText2 As SAPbouiCOM.EditText
        Private WithEvents StaticText2 As SAPbouiCOM.StaticText
        Private WithEvents EditText3 As SAPbouiCOM.EditText
        Private WithEvents StaticText3 As SAPbouiCOM.StaticText
        Private WithEvents EditText4 As SAPbouiCOM.EditText
        Private WithEvents StaticText4 As SAPbouiCOM.StaticText
        Private WithEvents EditText5 As SAPbouiCOM.EditText
        Private WithEvents StaticText5 As SAPbouiCOM.StaticText
        Private WithEvents EditText6 As SAPbouiCOM.EditText
        Private WithEvents StaticText6 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox0 As SAPbouiCOM.ComboBox
        Private WithEvents EditText8 As SAPbouiCOM.EditText
        Private WithEvents StaticText8 As SAPbouiCOM.StaticText
        Private WithEvents EditText9 As SAPbouiCOM.EditText
        Private WithEvents StaticText9 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox1 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText10 As SAPbouiCOM.StaticText
        Private WithEvents EditText10 As SAPbouiCOM.EditText
        Private WithEvents EditText11 As SAPbouiCOM.EditText
        Private WithEvents StaticText7 As SAPbouiCOM.StaticText
        Private WithEvents EditText7 As SAPbouiCOM.EditText
        Private WithEvents StaticText11 As SAPbouiCOM.StaticText
        Private WithEvents EditText12 As SAPbouiCOM.EditText
        Private WithEvents StaticText12 As SAPbouiCOM.StaticText
        Private WithEvents EditText13 As SAPbouiCOM.EditText
        Private WithEvents StaticText13 As SAPbouiCOM.StaticText
        Private WithEvents EditText14 As SAPbouiCOM.EditText
        Private WithEvents StaticText14 As SAPbouiCOM.StaticText
        Private WithEvents EditText15 As SAPbouiCOM.EditText
        Private WithEvents StaticText15 As SAPbouiCOM.StaticText
        Private WithEvents EditText16 As SAPbouiCOM.EditText
        Private WithEvents StaticText16 As SAPbouiCOM.StaticText
        Private WithEvents EditText18 As SAPbouiCOM.EditText
        Private WithEvents StaticText18 As SAPbouiCOM.StaticText
        Private WithEvents EditText20 As SAPbouiCOM.EditText
        Private WithEvents StaticText19 As SAPbouiCOM.StaticText
        Private WithEvents EditText21 As SAPbouiCOM.EditText
        Private WithEvents StaticText20 As SAPbouiCOM.StaticText
        Private WithEvents EditText22 As SAPbouiCOM.EditText
        Private WithEvents CheckBox0 As SAPbouiCOM.CheckBox
        Private WithEvents CheckBox1 As SAPbouiCOM.CheckBox
        Private WithEvents CheckBox2 As SAPbouiCOM.CheckBox
        Private WithEvents Grid0 As SAPbouiCOM.Grid
        Private WithEvents StaticText21 As SAPbouiCOM.StaticText
        Private WithEvents Button0 As SAPbouiCOM.Button
        Private WithEvents Button1 As SAPbouiCOM.Button
        Private WithEvents EditText23 As SAPbouiCOM.EditText
        Private WithEvents LinkedButton0 As SAPbouiCOM.LinkedButton
        Private WithEvents CheckBox3 As SAPbouiCOM.CheckBox
        Private WithEvents LinkedButton1 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton2 As SAPbouiCOM.LinkedButton
        Private WithEvents CheckBox4 As SAPbouiCOM.CheckBox
        Private WithEvents StaticText17 As SAPbouiCOM.StaticText
        Private WithEvents EditText17 As SAPbouiCOM.EditText
        Private WithEvents EditText19 As SAPbouiCOM.EditText
        Private WithEvents LinkedButton3 As SAPbouiCOM.LinkedButton
        Private WithEvents Button2 As SAPbouiCOM.Button

#End Region

        Private Sub ComboBox0_ComboSelectAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox0.ComboSelectAfter
            If objform.Mode <> BoFormMode.fm_ADD_MODE Then Exit Sub
            If ComboBox0.Selected Is Nothing Then Exit Sub
            Dim strsql As String = "Select ""NextNumber"" from nnm1 where ""ObjectCode""='OLVA' and ""Series""='" & ComboBox0.Selected.Value.ToString & "'"
            objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            objrs.DoQuery(strsql)
            If objrs.RecordCount = 0 Then Exit Sub
            EditText8.Value = objrs.Fields.Item(0).Value.ToString
        End Sub

        Private Sub EditText0_ChooseFromListAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText0.ChooseFromListAfter
            Try
                If pVal.ActionSuccess = False Then Exit Sub
                pCFL = pVal
                If Not pCFL.SelectedObjects Is Nothing Then
                    Try
                        EditText0.Value = pCFL.SelectedObjects.Columns.Item("U_ExtEmpNo").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                    Try
                        EditText1.Value = pCFL.SelectedObjects.Columns.Item("U_empID").Cells.Item(0).Value
                        EditText2.Value = pCFL.SelectedObjects.Columns.Item("U_firstNam").Cells.Item(0).Value + " " + pCFL.SelectedObjects.Columns.Item("U_lastName").Cells.Item(0).Value

                        Dim strsql As String = " SELECT ""U_ExtEmpNo"", IFNULL(""U_jobTitle"", '') AS ""Desig"", IFNULL(T1.""Name"", '') AS ""Dept"", IFNULL(T2.""Location"", '') AS ""Location"", IFNULL(T0.""U_mobile"", '') AS ""Local"", IFNULL(T0.""U_pager"", '') AS ""Native"" FROM ""@SMPR_OHEM"" T0"
                        strsql += vbCrLf + " LEFT OUTER JOIN OUDP T1 ON T0.""U_dept"" = T1.""Code"" LEFT OUTER JOIN ""OLCT"" T2 ON T2.""Code"" = T0.""U_location"" WHERE ""U_ExtEmpNo""='" & EditText0.Value & "'"
                        objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                        objrs.DoQuery(strsql)
                        If objrs.RecordCount > 0 Then
                            EditText4.Value = objrs.Fields.Item("Dept").Value.ToString
                            EditText3.Value = objrs.Fields.Item("Desig").Value.ToString
                            EditText5.Value = objrs.Fields.Item("Local").Value.ToString
                            EditText6.Value = objrs.Fields.Item("Native").Value.ToString
                            EditText22.Value = objrs.Fields.Item("Location").Value.ToString
                            EditText10.Item.Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                        End If
                    Catch ex As Exception
                    End Try
                    LoadHistory_leavedetails()
                    LeaveCalculation()
                End If
            Catch ex As Exception
            Finally
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try

        End Sub

        Private Sub EditText10_ChooseFromListAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText10.ChooseFromListAfter
            Try
                If pVal.ActionSuccess = False Then Exit Sub
                pCFL = pVal
                If Not pCFL.SelectedObjects Is Nothing Then
                    Try
                        EditText10.Value = pCFL.SelectedObjects.Columns.Item("Code").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                    Try
                        EditText11.Value = pCFL.SelectedObjects.Columns.Item("Name").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                    LoadHistory_leavedetails()
                    LeaveCalculation()
                End If
            Catch ex As Exception
            Finally
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try

        End Sub

        Private Sub EditText20_ChooseFromListAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText20.ChooseFromListAfter
            Try
                If pVal.ActionSuccess = False Then Exit Sub
                pCFL = pVal
                If Not pCFL.SelectedObjects Is Nothing Then
                    Try
                        EditText20.Value = pCFL.SelectedObjects.Columns.Item("U_ExtEmpNo").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                    Try
                        EditText21.Value = pCFL.SelectedObjects.Columns.Item("U_firstNam").Cells.Item(0).Value + " " + pCFL.SelectedObjects.Columns.Item("U_lastName").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                End If
            Catch ex As Exception
            Finally
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try
        End Sub

        Private Sub LinkedButton0_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles LinkedButton0.ClickAfter
            If EditText1.Value.ToString = "" Then Exit Sub
            Link_Value = EditText1.Value : Link_objtype = "OHEM"
            Dim activeform As New frmEmployeeMaster
            activeform.Show()
        End Sub

        Private Sub LinkedButton1_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles LinkedButton1.ClickAfter
            If EditText10.Value.ToString = "" Then Exit Sub
            Link_Value = EditText10.Value : Link_objtype = "MSTRLEVE"
            Dim activeform As New frmLeaveMaster
            activeform.Show()
        End Sub

        Private Sub LinkedButton2_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles LinkedButton2.ClickAfter
            If EditText20.Value.ToString = "" Then Exit Sub
            Link_Value = EditText20.Value : Link_objtype = "OHEM"
            Dim activeform As New frmEmployeeMaster
            activeform.Show()
        End Sub

        Private Sub LoadHistory_leavedetails()
            Try
                If EditText1.Value = "" Or EditText10.Value = "" Then Exit Sub
                strsql = "CALL ""MIPL_HRMS_LeaveApplicaiton_History"" ('" & EditText1.Value & "','" & EditText10.Value & "','" & EditText23.Value & "')"
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strsql)
                'If objrs.RecordCount = 0 Then Exit Sub
                Grid0.DataTable.ExecuteQuery(strsql)
                Grid0.Item.Enabled = False
                Dim col As EditTextColumn
                col = Grid0.Columns.Item("DocEntry")
                col.LinkedObjectType = "OLVE"
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Grid0_LinkPressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Grid0.LinkPressedAfter
            If Grid0.DataTable.Columns.Item("DocEntry").Cells.Item(pVal.Row).Value.ToString <> "" Then
                Link_Value = Grid0.DataTable.Columns.Item("DocEntry").Cells.Item(pVal.Row).Value.ToString : Link_objtype = "OLVA"
                Dim activeform As New frmLeaveApplicaiton
                activeform.Show()
            End If
        End Sub

        Private Sub LeaveCalculation()
            'Calucation for No of Days Leave based on From & To date
            Try
                If EditText7.String <> "" And EditText12.String <> "" Then
                    Dim Fromdate As Date = Date.ParseExact(EditText7.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                    Dim todate As Date = Date.ParseExact(EditText12.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                    Dim leaveno As Double = 0
                    If CheckBox0.Checked = True Then leaveno = 0.5 Else leaveno = 1
                    EditText13.Value = (DateDiff(DateInterval.Day, Fromdate, todate) + 1) * leaveno
                    objform.Items.Item("txtrejoin").Specific.string = DateAdd(DateInterval.Day, 1, todate).ToString("yyyyMMdd")
                Else
                    EditText13.Value = 0
                End If

                'Leave Eligibility Calculation 
                If EditText10.Value = "" Or EditText1.Value = "" Or EditText7.Value = "" Then
                    EditText14.Value = 0
                Else
                    strsql = "CALL ""MIPL_HRMS_LeaveApplication_Balance"" ('" & EditText7.Value & "','" & EditText1.Value & "','" & EditText10.Value & "')"
                    objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    objrs.DoQuery(strsql)
                    If objrs.RecordCount > 0 Then EditText14.Value = objrs.Fields.Item("Available_Leave").Value
                End If

                'Balance Leave Calculation 
                If EditText13.Value <> "" And EditText14.Value <> "" Then EditText15.Value = EditText14.Value - EditText13.Value Else EditText15.Value = 0
            Catch ex As Exception

            End Try
        End Sub

        Private Sub EditText7_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText7.LostFocusAfter
            LeaveCalculation()
        End Sub

        Private Sub EditText12_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText12.LostFocusAfter
            LeaveCalculation()
        End Sub

        Private Sub Button0_ClickBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles Button0.ClickBefore
            If Not (objform.Mode = BoFormMode.fm_ADD_MODE Or objform.Mode = BoFormMode.fm_UPDATE_MODE) Then Exit Sub
            EditText18.Item.Click(BoCellClickType.ct_Regular)
            If CheckBox3.Checked = True Then objaddon.objapplication.SetStatusBarMessage("Canceled Document Cannot be updated", BoMessageTime.bmt_Short, True) : BubbleEvent = False : Exit Sub
            If Display_Validaiton_Msg("Document Date is Missing", BubbleEvent, EditText9, EditText9, True) = False Then Exit Sub
            If Display_Validaiton_Msg("Employee Details is Missing", BubbleEvent, EditText0, EditText0, True) = False Then Exit Sub
            If Display_Validaiton_Msg("Leave Type is Missing", BubbleEvent, EditText10, EditText10, True) = False Then Exit Sub

            If Display_Validaiton_Msg("No of Leave Days should be Greater than Zero", BubbleEvent, EditText13, EditText7, False, False, True) = False Then Exit Sub
            'If Display_Validaiton_Msg("Eligible Leave Days should be Greater than Zero", BubbleEvent, EditText14, EditText7, False, False, True) = False Then Exit Sub
            'If Display_Validaiton_Msg("No of Leave Days should be Less than Eligible Leave Days", BubbleEvent, EditText15, EditText7, False, True, False) = False Then Exit Sub

            If Display_Validaiton_Msg("Rejoining Date is Missing", BubbleEvent, EditText16, EditText16, True) = False Then Exit Sub

        End Sub

        Private Function Display_Validaiton_Msg(ByVal msg As String, ByRef Bubbleevent As System.Boolean, ByVal editcontrol As SAPbouiCOM.EditText, Optional ByVal Focuscontrol As SAPbouiCOM.EditText = Nothing, Optional ByVal StringEmpty As Boolean = False, Optional ByVal lessthanzero As Boolean = False, Optional ByVal lessthan_Equalzero As Boolean = False)
            Dim validationstatus As Boolean = False
            If Focuscontrol Is Nothing Then Focuscontrol = editcontrol
            If lessthanzero Then If editcontrol.Value < 0 Then validationstatus = True
            If lessthan_Equalzero Then If editcontrol.Value <= 0 Then validationstatus = True
            If StringEmpty Then If editcontrol.Value = "" Then validationstatus = True

            If validationstatus = True Then
                objaddon.objapplication.SetStatusBarMessage(msg, BoMessageTime.bmt_Short, True)
                Focuscontrol.Item.Click(BoCellClickType.ct_Regular)
                Bubbleevent = False
                Return False
            End If

            Return True
        End Function

        Private Sub frmLeaveApplicaiton_DataLoadAfter(ByRef pVal As SAPbouiCOM.BusinessObjectInfo) Handles Me.DataLoadAfter
            Try
                If pVal.BeforeAction = True Then Exit Sub
                LoadHistory_leavedetails()

                If objaddon.ApprovedUser() Then
                    If CheckBox4.Checked = True Or CheckBox3.Checked = True Then
                        CheckBox4.Item.Enabled = False
                        CheckBox3.Item.Enabled = False
                    Else
                        CheckBox4.Item.Enabled = True
                        CheckBox3.Item.Enabled = True
                    End If
                End If

                objform.Title = "Leave Application"
                If CheckBox3.Checked = True Then objform.Title = "Leave Application - Cancelled"
                If CheckBox4.Checked = True Then objform.Title = "Leave Application - Approved" : GoTo FIeldDisable

                If ComboBox1.Selected Is Nothing Then
                    objform.Title = "Leave Application"
                ElseIf ComboBox1.Selected.Value.ToString.ToUpper = "D" Then
                    objform.Title = "Leave Application - Waiting for Approval"
                ElseIf ComboBox1.Selected.Value.ToString.ToUpper = "R" Then
                    objform.Title = "Leave Application - Rejected"
                Else
                    objform.Title = "Leave Application"
                End If

FIeldDisable:

                If ComboBox1.Selected.Value.ToString.ToUpper = "R" Then
                    'EditText7.Item.Enabled = True
                    'EditText12.Item.Enabled = True
                    'EditText16.Item.Enabled = True
                    'EditText5.Item.Enabled = True
                    'EditText6.Item.Enabled = True
                    'EditText20.Item.Enabled = True
                    'EditText18.Item.Enabled = True
                    CheckBox0.Item.Enabled = True
                    CheckBox2.Item.Enabled = True
                    CheckBox1.Item.Enabled = True
                Else
                    'EditText7.Item.Enabled = False
                    'EditText12.Item.Enabled = False
                    'EditText16.Item.Enabled = False
                    'EditText5.Item.Enabled = False
                    'EditText6.Item.Enabled = False
                    'EditText20.Item.Enabled = False
                    'EditText18.Item.Enabled = False
                    'CheckBox0.Item.Enabled = False
                    'CheckBox2.Item.Enabled = False
                    'CheckBox1.Item.Enabled = False
                End If
                objaddon.objglobalmethods.LoadCombo_SingleSeries_AfterFind(objform, "cmbseries", "OLVA", ComboBox0.Value)
                'MsgBox(ComboBox0.Value)

            Catch ex As Exception

            End Try
        End Sub

        Private Sub StaticText21_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles StaticText21.ClickAfter
            'If Grid0.Item.Visible = True Then
            '    objform.Height = StaticText21.Item.Top + StaticText21.Item.Height + 40
            '    Grid0.Item.Visible = False
            '    objform.Refresh()
            '    objform.Update()
            'Else
            '    Grid0.Item.Top = StaticText21.Item.Top + StaticText21.Item.Height + 20
            '    objform.Height = Grid0.Item.Top + Grid0.Item.Height + 40
            '    Grid0.Item.Visible = True
            '    objform.Refresh()
            '    objform.Update()
            'End If
            If objform.Height < 500 Then
                objform.Height = 550
            Else
                objform.Height = 330
            End If
        End Sub

        Private Sub LinkedButton3_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles LinkedButton3.ClickAfter
            Try
                If EditText17.Value <> "" And EditText19.Value = "OLSE" Then
                    Link_objtype = "OLSE"
                    Link_Value = EditText17.Value
                    Dim oactiveform As New FrmSettlment
                    oactiveform.Show()
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub EditText9_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText9.LostFocusAfter
            Try
                objaddon.objglobalmethods.LoadCombo_Series(objform, "cmbseries", "OLVA", IIf(EditText9.String = "", Now.Date, Date.ParseExact(EditText9.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)))
            Catch ex As Exception

            End Try
        End Sub

        Private Sub CheckBox0_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles CheckBox0.PressedAfter
            LeaveCalculation()
        End Sub

        Private Sub Button2_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button2.ClickAfter
            Try
                StaticText21.Item.Click(BoCellClickType.ct_Regular)
            Catch ex As Exception
            End Try
        End Sub

        Private Sub Button0_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button0.PressedAfter
            Try
                If pVal.ActionSuccess = True And objform.Mode = BoFormMode.fm_ADD_MODE Then
                    If addupdate = True Then objaddon.objapplication.StatusBar.SetText("Leave Application Added and Document Sent for Approval", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Success)
                    addupdate = False
                    EditText23.Value = objaddon.objglobalmethods.GetNextDocentry_Value("@SMPR_OLVA")
                    objform.Items.Item("txtddate").Specific.string = Now.Date.ToString("yyyyMMdd")
                    EditText0.Item.Click(BoCellClickType.ct_Regular)
                ElseIf pVal.ActionSuccess = True And objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then
                    If addupdate = True Then objaddon.objapplication.StatusBar.SetText("Leave Application Updated and Document Sent for Approval", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Success)
                    addupdate = False
                    objaddon.objapplication.Menus.Item("1304").Activate()
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Button0_PressedBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button0.PressedBefore
            Try
                If (objform.Mode = BoFormMode.fm_ADD_MODE Or objform.Mode = BoFormMode.fm_UPDATE_MODE) Then
                    addupdate = True
                Else
                    addupdate = False
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub EditText0_ChooseFromListBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles EditText0.ChooseFromListBefore
            Try

                Dim oCFL As SAPbouiCOM.ChooseFromList = objform.ChooseFromLists.Item("cfl_emp")
                Dim oConds As SAPbouiCOM.Conditions
                Dim oCond As SAPbouiCOM.Condition
                Dim oEmptyConds As New SAPbouiCOM.Conditions
                oCFL.SetConditions(oEmptyConds)
                oConds = oCFL.GetConditions()
                oCond = oConds.Add()
                oCond.Alias = "U_Status"
                oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                oCond.CondVal = "1"
                oCFL.SetConditions(oConds)

            Catch ex As Exception

            End Try

        End Sub

        Private Sub CheckBox4_PressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles CheckBox4.PressedAfter
            If CheckBox4.Checked = True Then
                'CheckBox4.Item.Enabled = False
                If CheckBox3.Checked = True Then CheckBox3.Checked = False
                'CheckBox3.Item.Enabled = False
            Else
                CheckBox4.Item.Enabled = True
            End If
        End Sub

        Private Sub CheckBox3_PressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles CheckBox3.PressedAfter
            If CheckBox3.Checked = True Then
                'CheckBox3.Item.Enabled = False
                If CheckBox4.Checked = True Then CheckBox4.Checked = False
                'CheckBox4.Item.Enabled = False
            Else
                CheckBox3.Item.Enabled = True
            End If

        End Sub
    End Class
End Namespace
