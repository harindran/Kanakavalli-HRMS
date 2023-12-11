Option Strict Off
Option Explicit On

Imports SAPbouiCOM.Framework

Namespace HRMS
    <FormAttribute("OTIS", "Transcation/frmAirTicketIssue.b1f")>
    Friend Class frmAirTicketIssue
        Inherits UserFormBase
        Private WithEvents objform As SAPbouiCOM.Form
        Dim FormCount As Integer = 0
        Private WithEvents pCFL As SAPbouiCOM.ISBOChooseFromListEventArg
        Dim strsql As String
        Dim objrs As SAPbobsCOM.Recordset
        Dim addupdate As Boolean = False

        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.Button0 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.Button1 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.StaticText0 = CType(Me.GetItem("lblempid").Specific, SAPbouiCOM.StaticText)
            Me.EditText0 = CType(Me.GetItem("txttrzid").Specific, SAPbouiCOM.EditText)
            Me.StaticText1 = CType(Me.GetItem("lblename").Specific, SAPbouiCOM.StaticText)
            Me.EditText1 = CType(Me.GetItem("txtename").Specific, SAPbouiCOM.EditText)
            Me.EditText2 = CType(Me.GetItem("txtempid").Specific, SAPbouiCOM.EditText)
            Me.StaticText2 = CType(Me.GetItem("lbldesig").Specific, SAPbouiCOM.StaticText)
            Me.EditText3 = CType(Me.GetItem("txtdesig").Specific, SAPbouiCOM.EditText)
            Me.StaticText3 = CType(Me.GetItem("lbldept").Specific, SAPbouiCOM.StaticText)
            Me.EditText4 = CType(Me.GetItem("txtdept").Specific, SAPbouiCOM.EditText)
            Me.StaticText4 = CType(Me.GetItem("lbljdate").Specific, SAPbouiCOM.StaticText)
            Me.EditText5 = CType(Me.GetItem("txtjdate").Specific, SAPbouiCOM.EditText)
            Me.StaticText5 = CType(Me.GetItem("lbllcdate").Specific, SAPbouiCOM.StaticText)
            Me.EditText6 = CType(Me.GetItem("txtlcdate").Specific, SAPbouiCOM.EditText)
            Me.StaticText6 = CType(Me.GetItem("lbltidate").Specific, SAPbouiCOM.StaticText)
            Me.EditText7 = CType(Me.GetItem("txttidate").Specific, SAPbouiCOM.EditText)
            Me.StaticText7 = CType(Me.GetItem("lbltpyear").Specific, SAPbouiCOM.StaticText)
            Me.EditText8 = CType(Me.GetItem("txttpyear").Specific, SAPbouiCOM.EditText)
            Me.StaticText8 = CType(Me.GetItem("lbleligi").Specific, SAPbouiCOM.StaticText)
            Me.EditText9 = CType(Me.GetItem("txteligi").Specific, SAPbouiCOM.EditText)
            Me.StaticText9 = CType(Me.GetItem("lbldays").Specific, SAPbouiCOM.StaticText)
            Me.EditText10 = CType(Me.GetItem("txtdays").Specific, SAPbouiCOM.EditText)
            Me.StaticText10 = CType(Me.GetItem("lblamount").Specific, SAPbouiCOM.StaticText)
            Me.EditText11 = CType(Me.GetItem("txtamount").Specific, SAPbouiCOM.EditText)
            Me.StaticText11 = CType(Me.GetItem("lblseries").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox0 = CType(Me.GetItem("cmbseries").Specific, SAPbouiCOM.ComboBox)
            Me.EditText13 = CType(Me.GetItem("txtdocno").Specific, SAPbouiCOM.EditText)
            Me.StaticText13 = CType(Me.GetItem("lblddate").Specific, SAPbouiCOM.StaticText)
            Me.EditText14 = CType(Me.GetItem("txtddate").Specific, SAPbouiCOM.EditText)
            Me.StaticText14 = CType(Me.GetItem("lblstatus").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox1 = CType(Me.GetItem("cmbstatus").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText16 = CType(Me.GetItem("lblremark").Specific, SAPbouiCOM.StaticText)
            Me.EditText15 = CType(Me.GetItem("txtremark").Specific, SAPbouiCOM.EditText)
            Me.StaticText17 = CType(Me.GetItem("lblcamt").Specific, SAPbouiCOM.StaticText)
            Me.EditText16 = CType(Me.GetItem("txtlcamt").Specific, SAPbouiCOM.EditText)
            Me.EditText17 = CType(Me.GetItem("txtentry").Specific, SAPbouiCOM.EditText)
            Me.LinkedButton0 = CType(Me.GetItem("lnkemp").Specific, SAPbouiCOM.LinkedButton)
            Me.StaticText12 = CType(Me.GetItem("lbletype").Specific, SAPbouiCOM.StaticText)
            Me.EditText12 = CType(Me.GetItem("txttype").Specific, SAPbouiCOM.EditText)
            Me.StaticText15 = CType(Me.GetItem("lblcountry").Specific, SAPbouiCOM.StaticText)
            Me.EditText18 = CType(Me.GetItem("txtcountry").Specific, SAPbouiCOM.EditText)
            Me.EditText19 = CType(Me.GetItem("txtttyp").Specific, SAPbouiCOM.EditText)
            Me.StaticText19 = CType(Me.GetItem("Item_2").Specific, SAPbouiCOM.StaticText)
            Me.EditText20 = CType(Me.GetItem("txttent").Specific, SAPbouiCOM.EditText)
            Me.LinkedButton1 = CType(Me.GetItem("lnktr").Specific, SAPbouiCOM.LinkedButton)
            Me.CheckBox0 = CType(Me.GetItem("Item_0").Specific, SAPbouiCOM.CheckBox)
            Me.CheckBox1 = CType(Me.GetItem("Item_1").Specific, SAPbouiCOM.CheckBox)
            Me.StaticText18 = CType(Me.GetItem("Item_3").Specific, SAPbouiCOM.StaticText)
            Me.EditText21 = CType(Me.GetItem("txtnot").Specific, SAPbouiCOM.EditText)
            Me.CheckBox2 = CType(Me.GetItem("chkpay").Specific, SAPbouiCOM.CheckBox)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()


        End Sub

        Private Sub OnCustomInitialize()
            objform = objaddon.objapplication.Forms.GetForm("OTIS", Me.FormCount)
            objform = objaddon.objapplication.Forms.ActiveForm
            Try
                objform.Freeze(True)

                CheckBox0.Item.Height = CheckBox0.Item.Height + 3
                CheckBox1.Item.Height = CheckBox1.Item.Height + 3
                CheckBox2.Item.Height = CheckBox2.Item.Height + 3
                EditText13.Value = objaddon.objglobalmethods.GetDocnum_BaseonSeries("OTIS")
                ManageFields()
                If objaddon.ApprovedUser() Then
                    CheckBox0.Item.Enabled = True
                    CheckBox1.Item.Enabled = True
                End If
                If Link_objtype.ToString.ToUpper = "OTIS" Then
                    objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE
                    EditText17.Item.Enabled = True
                    EditText17.Value = Link_Value
                    Button0.Item.Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                    objform.ActiveItem = "txtremark"
                    EditText17.Item.Enabled = False
                    Link_objtype = "-1"
                    Link_Value = ""
                ElseIf Link_Value.ToString <> "" And Link_objtype.ToString.ToUpper = "OTIS_AN" Then
                    EditText17.Value = objaddon.objglobalmethods.GetNextDocentry_Value("@SMPR_OTIS")
                    objform.Items.Item("txttidate").Specific.string = DateTime.Now.ToString("dd/MM/yy")
                    objform.Items.Item("txtddate").Specific.string = DateTime.Now.ToString("dd/MM/yy")
                    EditText0.Value = Link_Value
                    objform.ActiveItem = "txttrzid"
                    Link_Value = "-1" : Link_objtype = "-1"
                Else
                    EditText17.Value = objaddon.objglobalmethods.GetNextDocentry_Value("@SMPR_OTIS")
                    objform.Items.Item("txttidate").Specific.string = DateTime.Now.ToString("dd/MM/yy")
                    objform.Items.Item("txtddate").Specific.string = DateTime.Now.ToString("dd/MM/yy")
                    objform.ActiveItem = "txttrzid"
                End If
                ComboBox0.Select(0, SAPbouiCOM.BoSearchKey.psk_Index)
                objform.EnableMenu("1283", False) 'Remove
                objform.EnableMenu("1284", False) 'Cancel
                objform.EnableMenu("1286", False) 'Close
                objform.EnableMenu("1285", False) 'Restore
                If objaddon.objcompany.UserName.ToString.ToUpper <> "MANAGER" Then objform.EnableMenu("6913", False) 'User Defined Field
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
        Private WithEvents StaticText1 As SAPbouiCOM.StaticText
        Private WithEvents EditText1 As SAPbouiCOM.EditText
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
        Private WithEvents EditText7 As SAPbouiCOM.EditText
        Private WithEvents StaticText7 As SAPbouiCOM.StaticText
        Private WithEvents EditText8 As SAPbouiCOM.EditText
        Private WithEvents StaticText8 As SAPbouiCOM.StaticText
        Private WithEvents EditText9 As SAPbouiCOM.EditText
        Private WithEvents StaticText9 As SAPbouiCOM.StaticText
        Private WithEvents EditText10 As SAPbouiCOM.EditText
        Private WithEvents StaticText10 As SAPbouiCOM.StaticText
        Private WithEvents EditText11 As SAPbouiCOM.EditText
        Private WithEvents StaticText11 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox0 As SAPbouiCOM.ComboBox
        Private WithEvents EditText13 As SAPbouiCOM.EditText
        Private WithEvents StaticText13 As SAPbouiCOM.StaticText
        Private WithEvents EditText14 As SAPbouiCOM.EditText
        Private WithEvents StaticText14 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox1 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText16 As SAPbouiCOM.StaticText
        Private WithEvents EditText15 As SAPbouiCOM.EditText
        Private WithEvents StaticText17 As SAPbouiCOM.StaticText
        Private WithEvents EditText16 As SAPbouiCOM.EditText
        Private WithEvents EditText17 As SAPbouiCOM.EditText
        Private WithEvents LinkedButton0 As SAPbouiCOM.LinkedButton
        Private WithEvents StaticText12 As SAPbouiCOM.StaticText
        Private WithEvents EditText12 As SAPbouiCOM.EditText
        Private WithEvents StaticText15 As SAPbouiCOM.StaticText
        Private WithEvents EditText18 As SAPbouiCOM.EditText
        Private WithEvents EditText19 As SAPbouiCOM.EditText
        Private WithEvents StaticText19 As SAPbouiCOM.StaticText
        Private WithEvents EditText20 As SAPbouiCOM.EditText
        Private WithEvents LinkedButton1 As SAPbouiCOM.LinkedButton
        Private WithEvents CheckBox0 As SAPbouiCOM.CheckBox
        Private WithEvents CheckBox1 As SAPbouiCOM.CheckBox
        Private WithEvents StaticText18 As SAPbouiCOM.StaticText
        Private WithEvents EditText21 As SAPbouiCOM.EditText
        Private WithEvents CheckBox2 As SAPbouiCOM.CheckBox

#End Region

        Private Sub ManageFields()
            Try
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txttidate", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txttrzid", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtempid", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtename", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cmbseries", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtdocno", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtddate", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cmbstatus", False, True, False)
                'objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "chkpay", True, True, False)
            Catch ex As Exception

            End Try
        End Sub

        Private Sub EditText0_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText0.ChooseFromListAfter
            Try
                pCFL = pVal
                If pCFL.SelectedObjects Is Nothing Then Exit Sub
                Try
                    EditText0.Value = pCFL.SelectedObjects.Columns.Item("U_ExtEmpNo").Cells.Item(0).Value
                Catch ex As Exception
                End Try
                Load_Emp_Details()
            Catch ex As Exception
                objaddon.objapplication.SetStatusBarMessage("Error in Choose From List Selection : " & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, True)
            End Try
        End Sub

        Private Sub EditText0_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText0.ChooseFromListBefore
            If EditText7.Value = "" Then
                objaddon.objapplication.SetStatusBarMessage("Please Select Ticket Issue Date")
                BubbleEvent = False
            End If
            Dim oCFL As SAPbouiCOM.ChooseFromList = objform.ChooseFromLists.Item("empdt")
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
        End Sub

        Private Sub Load_Emp_Details()
            Try
                If EditText0.Value = "" Or EditText7.Value = "" Then Exit Sub
                Dim objrs As SAPbobsCOM.Recordset
                strsql = " CALL ""MIPL_HR_GetEmpDetails_AirticketIssue"" ('" & EditText0.Value & "','" & EditText7.Value & "')"
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strsql)
                If objrs.RecordCount = 0 Then Exit Sub

                EditText2.Value = objrs.Fields.Item("U_empid").Value.ToString
                EditText1.Value = objrs.Fields.Item("Name").Value.ToString
                EditText3.Value = objrs.Fields.Item("Designation").Value.ToString
                EditText4.Value = objrs.Fields.Item("Department").Value.ToString
                objform.Items.Item("txtjdate").Specific.string = objrs.Fields.Item("JoiningDate").Value
                objform.Items.Item("txtlcdate").Specific.string = objrs.Fields.Item("Lastdate").Value.ToString
                objform.Items.Item("txtlcamt").Specific.string = objrs.Fields.Item("Lastclaimamt").Value
                objform.Items.Item("txteligi").Specific.string = objrs.Fields.Item("Eligibleamt").Value
                objform.Items.Item("txttpyear").Specific.string = objrs.Fields.Item("TcktPeryear").Value
                objform.Items.Item("txtnot").Specific.string = objrs.Fields.Item("nooftckt").Value

                objform.Items.Item("txtdays").Specific.string = objrs.Fields.Item("noofday").Value
                objform.Items.Item("txtamount").Specific.string = objrs.Fields.Item("TicketAmount").Value

                EditText12.Value = objrs.Fields.Item("Emptype").Value
                EditText18.Value = objrs.Fields.Item("Country").Value
            Catch ex As Exception

            End Try
        End Sub

        Private Sub EditText7_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText7.LostFocusAfter
            Try
                Load_Emp_Details()
            Catch ex As Exception

            End Try
        End Sub

        Private Sub LinkedButton0_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles LinkedButton0.ClickAfter
            If EditText0.Value = "" Then Exit Sub
            Link_Value = EditText2.Value : Link_objtype = "OHEM"
            Dim activeform As New frmEmployeeMaster
            activeform.Show()
        End Sub

        Private Sub ComboBox0_ComboSelectAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox0.ComboSelectAfter
            If ComboBox0.Selected Is Nothing Then Exit Sub
            EditText13.Value = objaddon.objglobalmethods.GetDocnum_BaseonSeries("OTIS")
        End Sub

        Private Sub LinkedButton1_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles LinkedButton1.ClickAfter
            Try
                If EditText20.Value <> "" And EditText19.Value = "OLSE" Then
                    Link_objtype = "OLSE"
                    Link_Value = EditText20.Value
                    Dim oactiveform As New FrmSettlment
                    oactiveform.Show()
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub EditText14_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText14.LostFocusAfter
            Try
                objaddon.objglobalmethods.LoadCombo_Series(objform, "cmbseries", "OTIS", IIf(EditText14.String = "", Now.Date, Date.ParseExact(EditText14.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)))
            Catch ex As Exception

            End Try
        End Sub

        Private Sub frmAirTicketIssue_DataLoadAfter(ByRef pVal As SAPbouiCOM.BusinessObjectInfo) Handles Me.DataLoadAfter
            Try
                objaddon.objglobalmethods.LoadCombo_SingleSeries_AfterFind(objform, "cmbseries", "OTIS", ComboBox0.Value)

                If objaddon.ApprovedUser() Then
                    If CheckBox0.Checked = True Or CheckBox1.Checked = True Then
                        CheckBox0.Item.Enabled = False
                        CheckBox1.Item.Enabled = False
                    Else
                        CheckBox0.Item.Enabled = True
                        CheckBox1.Item.Enabled = True
                    End If
                End If

                If ApprovedUser_Employee = False Or ComboBox1.Selected Is Nothing Then objform.EnableMenu("1284", False) : EditText15.Item.Enabled = False : Exit Sub

                If ComboBox1.Selected.Value.ToString.ToUpper = "C" Or ComboBox1.Selected.Value.ToString.ToUpper = "D" Then
                    objform.EnableMenu("1284", False) : EditText15.Item.Enabled = False : CheckBox2.Item.Enabled = False
                Else
                    objform.EnableMenu("1284", True) : EditText15.Item.Enabled = True : CheckBox2.Item.Enabled = True
                End If
                'Dim strsql As String = "select 1 from [@SMPR_OPRC] where isnull(U_process,'')='Y' and U_FromDate='" & EditText1.Value & "' and U_todate='" & EditText2.Value & "'"
                'objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                'objrs.DoQuery(strsql)
                'If objrs.RecordCount > 0 Then
                '    objform.EnableMenu("1284", False)
                'Else
                '    objform.EnableMenu("1284", True)
                'End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Button0_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button0.PressedAfter
            Try
                If pVal.ActionSuccess = True And objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                    If addupdate = True Then objaddon.objapplication.StatusBar.SetText("Air Ticket Claim Added.Document Sent for Approval", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                    EditText17.Value = objaddon.objglobalmethods.GetNextDocentry_Value("@SMPR_OTIS")
                    objform.Items.Item("txttidate").Specific.string = DateTime.Now.ToString("dd/MM/yy")
                    objform.Items.Item("txtddate").Specific.string = DateTime.Now.ToString("dd/MM/yy")
                    objform.ActiveItem = "txttrzid"
                ElseIf pVal.ActionSuccess = True And objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then
                    If addupdate = True Then objaddon.objapplication.StatusBar.SetText("Air Ticket Claim Updated.Document Sent for Approval", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                    objaddon.objapplication.Menus.Item("1304").Activate()
                End If
                CheckBox0.Item.Enabled = True
                CheckBox1.Item.Enabled = True

            Catch ex As Exception

            End Try
        End Sub

        Private Sub Button0_PressedBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button0.PressedBefore
            Try
                If (objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Or objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE) Then
                    addupdate = True
                Else
                    addupdate = False
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub CheckBox1_PressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles CheckBox1.PressedAfter
            If CheckBox1.Checked = True Then
                CheckBox0.Item.Enabled = False
                CheckBox1.Item.Enabled = False
            End If

        End Sub

        Private Sub CheckBox0_PressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles CheckBox0.PressedAfter
            If CheckBox0.Checked = True Then
                CheckBox1.Item.Enabled = False
                CheckBox0.Item.Enabled = False
            End If

        End Sub



        Private Sub CheckBox2_PressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles CheckBox2.PressedAfter
            If CheckBox2.Checked = True Then
                CheckBox2.Item.Enabled = False
            End If

        End Sub
    End Class
End Namespace
