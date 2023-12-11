Option Strict Off
Option Explicit On

Imports SAPbouiCOM
Imports SAPbouiCOM.Framework

Namespace HRMS

    <FormAttribute("TRANOLAP", "Transcation/frmLoanApplication.b1f")>
    Friend Class frmLoanApplication
        Inherits UserFormBase
        Dim FormCount As Integer = 0
        Private WithEvents objform As SAPbouiCOM.Form
        Private WithEvents pCFL As SAPbouiCOM.ISBOChooseFromListEventArg
        Dim objrs As SAPbobsCOM.Recordset
        Dim strsql As String
        Private WithEvents objmatrix As SAPbouiCOM.Matrix
        Private WithEvents ocombo As SAPbouiCOM.ComboBox
        Dim addupdate As Boolean = False

        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.StaticText0 = CType(Me.GetItem("lblempid").Specific, SAPbouiCOM.StaticText)
            Me.EditText0 = CType(Me.GetItem("txtiempid").Specific, SAPbouiCOM.EditText)
            Me.StaticText1 = CType(Me.GetItem("lblempname").Specific, SAPbouiCOM.StaticText)
            Me.EditText1 = CType(Me.GetItem("txtempname").Specific, SAPbouiCOM.EditText)
            Me.EditText2 = CType(Me.GetItem("txtempid").Specific, SAPbouiCOM.EditText)
            Me.StaticText2 = CType(Me.GetItem("lbldesig").Specific, SAPbouiCOM.StaticText)
            Me.EditText3 = CType(Me.GetItem("txtdesi").Specific, SAPbouiCOM.EditText)
            Me.StaticText3 = CType(Me.GetItem("lbllcode").Specific, SAPbouiCOM.StaticText)
            Me.EditText4 = CType(Me.GetItem("txtlcode").Specific, SAPbouiCOM.EditText)
            Me.EditText5 = CType(Me.GetItem("txtlname").Specific, SAPbouiCOM.EditText)
            Me.StaticText5 = CType(Me.GetItem("lbllamt").Specific, SAPbouiCOM.StaticText)
            Me.EditText7 = CType(Me.GetItem("txtlamt").Specific, SAPbouiCOM.EditText)
            Me.StaticText6 = CType(Me.GetItem("lblnoins").Specific, SAPbouiCOM.StaticText)
            Me.EditText8 = CType(Me.GetItem("txtnoins").Specific, SAPbouiCOM.EditText)
            Me.StaticText7 = CType(Me.GetItem("lblapm").Specific, SAPbouiCOM.StaticText)
            Me.EditText9 = CType(Me.GetItem("txtapm").Specific, SAPbouiCOM.EditText)
            Me.StaticText8 = CType(Me.GetItem("lbldocno").Specific, SAPbouiCOM.StaticText)
            Me.EditText11 = CType(Me.GetItem("txtdocno").Specific, SAPbouiCOM.EditText)
            Me.ComboBox0 = CType(Me.GetItem("cmbseri").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText9 = CType(Me.GetItem("lbldocdt").Specific, SAPbouiCOM.StaticText)
            Me.EditText12 = CType(Me.GetItem("txtdocdt").Specific, SAPbouiCOM.EditText)
            Me.StaticText10 = CType(Me.GetItem("lblstatus").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox1 = CType(Me.GetItem("cmbstatus").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText11 = CType(Me.GetItem("lbleffdt").Specific, SAPbouiCOM.StaticText)
            Me.EditText14 = CType(Me.GetItem("txteffdt").Specific, SAPbouiCOM.EditText)
            Me.StaticText12 = CType(Me.GetItem("lblpend").Specific, SAPbouiCOM.StaticText)
            Me.EditText15 = CType(Me.GetItem("txtpend").Specific, SAPbouiCOM.EditText)
            Me.StaticText13 = CType(Me.GetItem("lblpno").Specific, SAPbouiCOM.StaticText)
            Me.EditText16 = CType(Me.GetItem("txtpno").Specific, SAPbouiCOM.EditText)
            Me.EditText17 = CType(Me.GetItem("txtpdt").Specific, SAPbouiCOM.EditText)
            Me.Matrix0 = CType(Me.GetItem("mtloan").Specific, SAPbouiCOM.Matrix)
            Me.Button0 = CType(Me.GetItem("btnfill").Specific, SAPbouiCOM.Button)
            Me.StaticText17 = CType(Me.GetItem("Item_34").Specific, SAPbouiCOM.StaticText)
            Me.EditText21 = CType(Me.GetItem("Item_35").Specific, SAPbouiCOM.EditText)
            Me.Button1 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.Button2 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.EditText22 = CType(Me.GetItem("txtpen").Specific, SAPbouiCOM.EditText)
            Me.StaticText18 = CType(Me.GetItem("lblprpe").Specific, SAPbouiCOM.StaticText)
            Me.EditText23 = CType(Me.GetItem("txtprpe").Specific, SAPbouiCOM.EditText)
            Me.StaticText19 = CType(Me.GetItem("lbltpaid").Specific, SAPbouiCOM.StaticText)
            Me.EditText24 = CType(Me.GetItem("txttpaid").Specific, SAPbouiCOM.EditText)
            Me.EditText25 = CType(Me.GetItem("txtentry").Specific, SAPbouiCOM.EditText)
            Me.CheckBox0 = CType(Me.GetItem("chkcan").Specific, SAPbouiCOM.CheckBox)
            Me.CheckBox1 = CType(Me.GetItem("chkapp").Specific, SAPbouiCOM.CheckBox)
            Me.LinkedButton0 = CType(Me.GetItem("Item_0").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton1 = CType(Me.GetItem("Item_1").Specific, SAPbouiCOM.LinkedButton)
            Me.StaticText4 = CType(Me.GetItem("Item_2").Specific, SAPbouiCOM.StaticText)
            Me.EditText6 = CType(Me.GetItem("Item_3").Specific, SAPbouiCOM.EditText)
            Me.LinkedButton2 = CType(Me.GetItem("Item_4").Specific, SAPbouiCOM.LinkedButton)
            Me.StaticText15 = CType(Me.GetItem("lbljeno").Specific, SAPbouiCOM.StaticText)
            Me.EditText10 = CType(Me.GetItem("txtjeno").Specific, SAPbouiCOM.EditText)
            Me.LinkedButton3 = CType(Me.GetItem("lnkje").Specific, SAPbouiCOM.LinkedButton)
            Me.CheckBox2 = CType(Me.GetItem("chkdedu").Specific, SAPbouiCOM.CheckBox)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()
            AddHandler DataLoadAfter, AddressOf Me.frmLoanApplication_DataLoadAfter

        End Sub

        Private Sub OnCustomInitialize()
            Try
                objform = objaddon.objapplication.Forms.GetForm("TRANOLAP", FormCount)
                objform = objaddon.objapplication.Forms.ActiveForm
                If objform.TypeEx = "-TRANOLAP" Then objform.Close() : objform = objaddon.objapplication.Forms.ActiveForm
                objform.Freeze(True)

                CheckBox0.Item.Height = CheckBox0.Item.Height + 3
                CheckBox1.Item.Height = CheckBox1.Item.Height + 3
                CheckBox2.Item.Height = CheckBox2.Item.Height + 3
                CheckBox2.Item.Width = CheckBox2.Item.Width + 20
                CheckBox0.Item.Width = CheckBox0.Item.Width - 10
                Manage_Attributes()
                If objaddon.ApprovedUser() Then
                    CheckBox0.Item.Enabled = True
                    CheckBox1.Item.Enabled = True
                End If
                If Link_Value.ToString <> "" And Link_objtype.ToString.ToUpper = "OLOA" Then
                    objform = objaddon.objapplication.Forms.ActiveForm
                    objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE
                    EditText25.Item.Enabled = True
                    EditText25.Value = Link_Value
                    objform.Items.Item("1").Click(BoCellClickType.ct_Regular)
                    EditText0.Item.Click(BoCellClickType.ct_Regular)
                    EditText25.Item.Enabled = False
                    Link_Value = "-1" : Link_objtype = "-1"
                Else
                    EditText25.Value = objaddon.objglobalmethods.GetNextDocentry_Value("@SMPR_OLOA")
                    objform.Items.Item("txtdocdt").Specific.string = Now.Date.ToString("yyyyMMdd")
                    EditText0.Item.Click(BoCellClickType.ct_Regular)
                End If
                'If ApprovedUser_Employee Then
                '    CheckBox0.Item.Enabled = True
                '    CheckBox1.Item.Enabled = True
                'End If
                ComboBox0.Select(0, SAPbouiCOM.BoSearchKey.psk_Index)
                objform.EnableMenu("1283", False) 'Remove menu
                objform.EnableMenu("1284", False) 'Cancel menu
                objform.EnableMenu("1286", False) 'close Menu
                objform.Settings.Enabled = True
                If objaddon.objcompany.UserName.ToString.ToUpper <> "MANAGER" Then objform.EnableMenu("6913", False) 'User Defined Field

                objaddon.objapplication.Menus.Item("1300").Activate()

                objform.Freeze(False)
            Catch ex As Exception
                objform.Freeze(False)
            End Try
        End Sub

        Private Sub Manage_Attributes()
            Try
                SetAutomanagedattribute("txtiempid", True, True, False)
                SetAutomanagedattribute("txtempid", False, True, False)
                SetAutomanagedattribute("txtempname", False, True, False)
                SetAutomanagedattribute("cmbstatus", False, True, False)
                SetAutomanagedattribute("txtdocno", False, True, False)

                SetAutomanagedattribute("txtlcode", True, True, False)
                SetAutomanagedattribute("txtlamt", True, True, False)
                SetAutomanagedattribute("txteffdt", True, True, False)
                SetAutomanagedattribute("txtnoins", True, True, False)
                SetAutomanagedattribute("txtapm", True, True, False)

                SetAutomanagedattribute("cmbseri", True, True, False)
                SetAutomanagedattribute("txtdocdt", True, True, False)

                SetAutomanagedattribute("btnfill", True, False, False)
                SetAutomanagedattribute("chkdedu", True, True, False)
            Catch ex As Exception
                objaddon.objapplication.SetStatusBarMessage("Close User Defined Fields Screen and then proceed", BoMessageTime.bmt_Short, True)
                objform.Close()
            End Try

        End Sub

        Private Sub SetAutomanagedattribute(ByVal fieldname As String, ByVal add As Boolean, ByVal find As Boolean, ByVal update As Boolean)

            If add = True Then
                objform.Items.Item(fieldname).SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, SAPbouiCOM.BoAutoFormMode.afm_Add, SAPbouiCOM.BoModeVisualBehavior.mvb_True)
            Else
                objform.Items.Item(fieldname).SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, SAPbouiCOM.BoAutoFormMode.afm_Add, SAPbouiCOM.BoModeVisualBehavior.mvb_False)
            End If

            If find = True Then
                objform.Items.Item(fieldname).SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, SAPbouiCOM.BoAutoFormMode.afm_Find, SAPbouiCOM.BoModeVisualBehavior.mvb_True)
            Else
                objform.Items.Item(fieldname).SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, SAPbouiCOM.BoAutoFormMode.afm_Find, SAPbouiCOM.BoModeVisualBehavior.mvb_False)
            End If

            If update Then
                objform.Items.Item(fieldname).SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, SAPbouiCOM.BoAutoFormMode.afm_Ok, SAPbouiCOM.BoModeVisualBehavior.mvb_True)
            Else
                objform.Items.Item(fieldname).SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, SAPbouiCOM.BoAutoFormMode.afm_Ok, SAPbouiCOM.BoModeVisualBehavior.mvb_False)
            End If

        End Sub

#Region "Field Details"

        Private WithEvents StaticText0 As SAPbouiCOM.StaticText
        Private WithEvents EditText0 As SAPbouiCOM.EditText
        Private WithEvents StaticText1 As SAPbouiCOM.StaticText
        Private WithEvents EditText1 As SAPbouiCOM.EditText
        Private WithEvents EditText2 As SAPbouiCOM.EditText
        Private WithEvents StaticText2 As SAPbouiCOM.StaticText
        Private WithEvents EditText3 As SAPbouiCOM.EditText
        Private WithEvents StaticText3 As SAPbouiCOM.StaticText
        Private WithEvents EditText4 As SAPbouiCOM.EditText
        Private WithEvents EditText5 As SAPbouiCOM.EditText
        Private WithEvents StaticText5 As SAPbouiCOM.StaticText
        Private WithEvents EditText7 As SAPbouiCOM.EditText
        Private WithEvents StaticText6 As SAPbouiCOM.StaticText
        Private WithEvents EditText8 As SAPbouiCOM.EditText
        Private WithEvents StaticText7 As SAPbouiCOM.StaticText
        Private WithEvents EditText9 As SAPbouiCOM.EditText
        Private WithEvents StaticText8 As SAPbouiCOM.StaticText
        Private WithEvents EditText11 As SAPbouiCOM.EditText
        Private WithEvents ComboBox0 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText9 As SAPbouiCOM.StaticText
        Private WithEvents EditText12 As SAPbouiCOM.EditText
        Private WithEvents StaticText10 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox1 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText11 As SAPbouiCOM.StaticText
        Private WithEvents EditText14 As SAPbouiCOM.EditText
        Private WithEvents StaticText12 As SAPbouiCOM.StaticText
        Private WithEvents EditText15 As SAPbouiCOM.EditText
        Private WithEvents StaticText13 As SAPbouiCOM.StaticText
        Private WithEvents EditText16 As SAPbouiCOM.EditText
        Private WithEvents EditText17 As SAPbouiCOM.EditText
        Private WithEvents Matrix0 As SAPbouiCOM.Matrix
        Private WithEvents Button0 As SAPbouiCOM.Button
        Private WithEvents StaticText17 As SAPbouiCOM.StaticText
        Private WithEvents EditText21 As SAPbouiCOM.EditText
        Private WithEvents Button1 As SAPbouiCOM.Button
        Private WithEvents Button2 As SAPbouiCOM.Button
        Private WithEvents EditText22 As SAPbouiCOM.EditText
        Private WithEvents StaticText18 As SAPbouiCOM.StaticText
        Private WithEvents EditText23 As SAPbouiCOM.EditText
        Private WithEvents StaticText19 As SAPbouiCOM.StaticText
        Private WithEvents EditText24 As SAPbouiCOM.EditText
        Private WithEvents EditText25 As SAPbouiCOM.EditText
        Private WithEvents CheckBox0 As SAPbouiCOM.CheckBox
        Private WithEvents CheckBox1 As SAPbouiCOM.CheckBox
        Private WithEvents LinkedButton0 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton1 As SAPbouiCOM.LinkedButton
        Private WithEvents StaticText4 As SAPbouiCOM.StaticText
        Private WithEvents EditText6 As SAPbouiCOM.EditText
        Private WithEvents LinkedButton2 As SAPbouiCOM.LinkedButton
        Private WithEvents StaticText15 As SAPbouiCOM.StaticText
        Private WithEvents EditText10 As SAPbouiCOM.EditText
        Private WithEvents LinkedButton3 As SAPbouiCOM.LinkedButton
#End Region

        Private Sub EditText0_ChooseFromListAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText0.ChooseFromListAfter
            Try
                If pVal.ActionSuccess = False Then Exit Sub
                pCFL = pVal
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                If Not pCFL.SelectedObjects Is Nothing Then
                    Try
                        EditText0.Value = pCFL.SelectedObjects.Columns.Item("U_ExtEmpNo").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                    Try
                        EditText2.Value = pCFL.SelectedObjects.Columns.Item("U_empID").Cells.Item(0).Value
                        EditText1.Value = pCFL.SelectedObjects.Columns.Item("U_firstNam").Cells.Item(0).Value + " " + pCFL.SelectedObjects.Columns.Item("U_lastName").Cells.Item(0).Value

                        Dim strsql As String = " select ""U_ExtEmpNo"",ifnull(""U_jobTitle"",'') Desig,T1.""Balamt"",cast(ifnull(T2.""Eigible"",0) as integer) ""Eigible""  from ""@SMPR_OHEM"" T0 Left join "
                        strsql += vbCrLf + "  (Select ""U_IDNo"",sum(T1.""U_Amount"") ""Balamt""  from ""@SMPR_OLOA"" T0 inner join  ""@SMPR_LOA1"" T1 on T0.""DocEntry""=T1.""DocEntry"" where T1.""U_Status""='O' group by ""U_IDNo"") T1 on T0.""U_ExtEmpNo""=T1.""U_IDNo""  "
                        strsql += vbCrLf + "  left join (select T0.""Code"",sum(T0.""U_Amount"" * ifnull(T1.""U_Loanper"",0)) ""Eigible"" from ""@SMPR_HEM1"" T0 inner join ""@SMPR_OPYE"" T1 on T0.""U_PayElCod""=T1.""Code"" Group by T0.""Code"")T2 on T2.""Code""=T0.""Code"" "
                        strsql += vbCrLf + " where T0.""U_ExtEmpNo""='" & EditText0.Value & "'"

                        objrs.DoQuery(strsql)
                        If objrs.RecordCount > 0 Then
                            EditText3.Value = objrs.Fields.Item("Desig").Value.ToString
                            EditText23.Value = objrs.Fields.Item("Balamt").Value.ToString
                            EditText6.Value = objrs.Fields.Item("Eigible").Value.ToString
                            objmatrix = objform.Items.Item("mtloan").Specific
                            If objmatrix.VisualRowCount = 0 Then objmatrix.AddRow(1) : objmatrix.Columns.Item("#").Cells.Item(1).Specific.string = 1
                            EditText4.Item.Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                        End If
                    Catch ex As Exception
                    End Try
                End If
            Catch ex As Exception
            Finally
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try

        End Sub

        Private Sub EditText4_ChooseFromListAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText4.ChooseFromListAfter
            Try
                If pVal.ActionSuccess = False Then Exit Sub
                pCFL = pVal
                If Not pCFL.SelectedObjects Is Nothing Then
                    Try
                        EditText4.Value = pCFL.SelectedObjects.Columns.Item("Code").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                    Try
                        EditText5.Value = pCFL.SelectedObjects.Columns.Item("Name").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                End If
            Catch ex As Exception
            Finally
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try

        End Sub

        Private Sub ComboBox0_ComboSelectAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox0.ComboSelectAfter
            If ComboBox0.Selected Is Nothing Then Exit Sub
            Dim strsql As String = "Select ""NextNumber"" from nnm1 where ""ObjectCode""='OLOA' and ""Series""='" & ComboBox0.Selected.Value.ToString & "'"
            objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            objrs.DoQuery(strsql)
            If objrs.RecordCount = 0 Then Exit Sub
            EditText11.Value = objrs.Fields.Item(0).Value.ToString
        End Sub

        Private Sub frmLoanApplication_DataLoadAfter(ByRef pVal As SAPbouiCOM.BusinessObjectInfo) Handles Me.DataLoadAfter
            Try
                If pVal.ActionSuccess = False Then Exit Sub
                objaddon.objapplication.Menus.Item("1300").Activate()
                objaddon.objglobalmethods.LoadCombo_SingleSeries_AfterFind(objform, "cmbseri", "OLOA", ComboBox0.Value)

                objmatrix = objform.Items.Item("mtloan").Specific
                If objaddon.ApprovedUser() Then
                    If CheckBox0.Checked = True Or CheckBox1.Checked = True Then
                        CheckBox0.Item.Enabled = False
                        CheckBox1.Item.Enabled = False
                    Else
                        CheckBox0.Item.Enabled = True
                        CheckBox1.Item.Enabled = True
                    End If
                End If
                objform.Title = "Loan Application"
                If CheckBox0.Checked = True Then objform.Title = "Loan Application - Cancelled" : GoTo matrixdetails
                If CheckBox1.Checked = True Then objform.Title = "Loan Application - Approved" : GoTo matrixdetails

                If ComboBox1.Selected Is Nothing Then
                    objform.Title = "Loan Application"
                ElseIf ComboBox1.Selected.Value.ToString.ToUpper = "D" Then
                    objform.Title = "Loan Application - Waiting for Approval"
                ElseIf ComboBox1.Selected.Value.ToString.ToUpper = "R" Then
                    objform.Title = "Loan Application - Rejected"
                Else
                    objform.Title = "Loan Application"
                End If

matrixdetails:

                If ComboBox1.Selected.Value.ToString.ToUpper = "C" Then
                    For i As Integer = 0 To objmatrix.Columns.Count - 1
                        objmatrix.Columns.Item(i).Editable = False
                    Next
                    EditText21.Item.Enabled = False
                    Exit Sub
                End If

                If ApprovedUser_Employee And (ComboBox1.Selected.Value.ToString.ToUpper = "O" Or ComboBox1.Selected.Value.ToString.ToUpper = "R") Then
                    For i As Integer = 1 To objmatrix.RowCount
                        ocombo = objmatrix.Columns.Item("status").Cells.Item(i).Specific
                        If ocombo.Value.ToString.ToUpper = "O" Or ocombo.Value.ToString = "" Then
                            objmatrix.CommonSetting.SetRowEditable(i, True)
                        Else
                            objmatrix.CommonSetting.SetRowEditable(i, False)
                        End If
                    Next

                    For i As Integer = 0 To objmatrix.Columns.Count - 1
                        If objmatrix.Columns.Item(i).UniqueID = "date" Or objmatrix.Columns.Item(i).UniqueID = "amount" Or objmatrix.Columns.Item(i).UniqueID = "chkded" Then
                        Else
                            objmatrix.Columns.Item(i).Editable = False
                        End If
                    Next
                Else
                    For i As Integer = 0 To objmatrix.Columns.Count - 1
                        objmatrix.Columns.Item(i).Editable = False
                    Next
                End If

            Catch ex As Exception

            End Try
        End Sub

#Region "Matrix Control Not Editable"

        Private Sub Matrix0_ClickBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Matrix0.ClickBefore
            MatrixControl_NotEditable(sboObject, pVal, BubbleEvent)
        End Sub

        Private Sub Matrix0_KeyDownBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Matrix0.KeyDownBefore
            MatrixControl_NotEditable(sboObject, pVal, BubbleEvent)
        End Sub

        Private Sub Matrix0_PickerClickedBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Matrix0.PickerClickedBefore
            MatrixControl_NotEditable(sboObject, pVal, BubbleEvent)
        End Sub

        Private Sub MatrixControl_NotEditable(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean)
            objmatrix = objform.Items.Item("mtloan").Specific
            If pVal.Row = 0 Then Exit Sub
            ocombo = objmatrix.Columns.Item("status").Cells.Item(pVal.Row).Specific
            If ocombo.Value.ToString = "" Then Exit Sub
            If ocombo.Value.ToString.ToUpper <> "O" Then
                BubbleEvent = False
            End If
        End Sub

#End Region

        Private Sub Button0_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button0.ClickAfter
            Try
                objform.Freeze(True)
                Dim chkded As SAPbouiCOM.CheckBox
                objmatrix = objform.Items.Item("mtloan").Specific
                objaddon.objapplication.SetStatusBarMessage("Splitting the loan details.Please wait...", BoMessageTime.bmt_Short, False)
                Dim instamt As Double = 0, noofins As Integer = 0, effdate As Date

                ' effdate = EditText14.String
                effdate = Date.ParseExact(EditText14.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                If Val(EditText9.Value) <= 0 Then
                    noofins = EditText8.Value
                    EditText9.Value = Math.Truncate(EditText7.Value / noofins)
                Else
                    noofins = EditText7.Value / EditText9.Value
                    If noofins * EditText9.Value < EditText7.Value Then noofins = noofins + 1
                End If

                If objmatrix.VisualRowCount > 1 Then If objaddon.objapplication.MessageBox("This Split will clear the previous splited details? Do you want to continue", 1, "Yes", "No") = 2 Then Exit Sub

                objmatrix.Clear()
                instamt = 0
                For i As Integer = 1 To noofins
                    objmatrix.AddRow(1)
                    objmatrix.Columns.Item("#").Cells.Item(i).Specific.string = i
                    objmatrix.Columns.Item("date").Cells.Item(i).Specific.string = effdate.ToString("dd/MM/yy") 'EditText14.Value
                    effdate = effdate.AddMonths(1)
                    If i = noofins Then
                        objmatrix.Columns.Item("amount").Cells.Item(i).Specific.string = Math.Round(EditText7.Value - instamt, 2)
                    Else
                        objmatrix.Columns.Item("amount").Cells.Item(i).Specific.string = EditText9.Value
                        instamt = instamt + EditText9.Value
                    End If
                    ocombo = objmatrix.Columns.Item("status").Cells.Item(i).Specific
                    ocombo.Select("O", BoSearchKey.psk_ByValue)
                    chkded = objmatrix.Columns.Item("chkded").Cells.Item(i).Specific
                    chkded.Checked = True
                Next
                EditText8.Value = objmatrix.RowCount
                objaddon.objapplication.SetStatusBarMessage("Splitted Successfully.", BoMessageTime.bmt_Short, False)
                EditText21.Item.Click(BoCellClickType.ct_Regular)
                objmatrix.Columns.Item("amount").ColumnSetting.SumType = BoColumnSumType.bst_Auto
                objform.Freeze(False)
            Catch ex As Exception
                objform.Freeze(False)
                objaddon.objapplication.SetStatusBarMessage("Error in Splitting.Please verify it manually.", BoMessageTime.bmt_Short, False)
            End Try
        End Sub

        Private Sub Button0_ClickBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button0.ClickBefore
            Try
                If Button0.Item.Enabled = False Then BubbleEvent = False : Exit Sub
                If objform.Mode <> BoFormMode.fm_ADD_MODE Then
                    objaddon.objapplication.SetStatusBarMessage("Split is not allowed after posting", BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If

                If Val(EditText7.Value) <= 0 Then
                    objaddon.objapplication.SetStatusBarMessage("Loan Amount is Missing", BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If

                If Val(EditText8.Value) = 0 And Val(EditText9.Value) = 0 Then
                    objaddon.objapplication.SetStatusBarMessage("Either No of Installment or Amount Per Month needed to split", BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strsql = "Select ""DocNum"" from  ""@SMPR_OLOA"" where ""U_IDNo""='" & EditText0.Value & "' and ""Status""='O'"
                objrs.DoQuery(strsql)
                If objrs.RecordCount > 0 Then If objaddon.objapplication.MessageBox("Open Loan Application found for this Employee. Do you want to Create a New Loan?", 2, "Yes", "No") <> 1 Then BubbleEvent = False : Exit Sub

            Catch ex As Exception

            End Try

        End Sub

        Private Sub Button1_ClickBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button1.ClickBefore
            Try


                If objform.Mode = BoFormMode.fm_FIND_MODE Then Exit Sub
                If EditText0.Value = "" Or EditText1.Value = "" Or EditText2.Value = "" Then
                    objaddon.objapplication.SetStatusBarMessage("Employee Details Missing", BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If

                If EditText4.Value = "" Then
                    objaddon.objapplication.SetStatusBarMessage("Loan Type Details Missing", BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If

                If Val(EditText7.Value) <= 0 Then
                    objaddon.objapplication.SetStatusBarMessage("Loan Amount Should be Greater than Zero", BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If

                If EditText14.Value = "" Then
                    objaddon.objapplication.SetStatusBarMessage("Effective Date is Missing", BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If

                If Val(EditText8.Value) <= 0 Or Val(EditText9.Value) <= 0 Then
                    objaddon.objapplication.SetStatusBarMessage("No of Installment/Max Amount/Month Should be Greater than Zero", BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If

                If EditText11.Value = "" Then
                    objaddon.objapplication.SetStatusBarMessage("Doc No is Missing", BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If

                If EditText12.Value = "" Then
                    objaddon.objapplication.SetStatusBarMessage("Doc Date is Missing", BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If

                objmatrix = Matrix0 'objform.Items.Item("mtloan").Specific

                If objmatrix.VisualRowCount = 0 Then
                    objaddon.objapplication.SetStatusBarMessage("Installment Details is Missing", BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If

                For i As Integer = 1 To objmatrix.VisualRowCount
                    If objmatrix.Columns.Item("date").Cells.Item(i).Specific.string = "" Then
                        objaddon.objapplication.SetStatusBarMessage("Installment Dete Missing in Line No : " & i.ToString, BoMessageTime.bmt_Short, True)
                        BubbleEvent = False : Exit Sub
                    End If

                    ocombo = objmatrix.Columns.Item("status").Cells.Item(i).Specific
                    If ocombo.Selected Is Nothing Then
                        objaddon.objapplication.SetStatusBarMessage("Status is Missing in Line No : " & i.ToString, BoMessageTime.bmt_Short, True)
                        BubbleEvent = False : Exit Sub
                    End If
                    If ocombo.Selected.Value.ToString.ToUpper <> "C" And Val(objmatrix.Columns.Item("amount").Cells.Item(i).Specific.string) <= 0 Then
                        objaddon.objapplication.SetStatusBarMessage("Installment Amount is Missing in Line No : " & i.ToString, BoMessageTime.bmt_Short, True)
                        BubbleEvent = False : Exit Sub
                    End If
                Next
                objmatrix.Columns.Item("amount").ColumnSetting.SumType=BoColumnSumType.bst_Auto
                Dim ss As String = Val(objmatrix.Columns.Item("amount").ColumnSetting.SumValue)
                If Val(EditText7.Value) <> Val(objmatrix.Columns.Item("amount").ColumnSetting.SumValue) Then
                    objaddon.objapplication.SetStatusBarMessage("Loan Amount is Not Matching with the total installment amount...", BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If
            Catch ex As Exception
                objaddon.objapplication.StatusBar.SetText("Exception: " + ex.Message.ToString, BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error)
            End Try
        End Sub

        Private Sub LinkedButton0_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles LinkedButton0.ClickAfter
            If EditText2.Value.ToString = "" Then Exit Sub
            Link_Value = EditText2.Value : Link_objtype = "OHEM"
            Dim activeform As New frmEmployeeMaster
            activeform.Show()

        End Sub

        Private Sub LinkedButton1_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles LinkedButton1.ClickAfter
            If EditText4.Value.ToString = "" Then Exit Sub
            Link_Value = EditText4.Value : Link_objtype = "MSTRLOAN"
            Dim activeform As New frmLoanMaster
            activeform.Show()
        End Sub

        Private Sub Matrix0_LinkPressedBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles Matrix0.LinkPressedBefore
            Try
                If pVal.ColUID = "docno" Then
                    If Matrix0.Columns.Item("docno").Cells.Item(pVal.Row).Specific.string <> "" Then
                        If Matrix0.Columns.Item("trtype").Cells.Item(pVal.Row).Specific.string = "OLSE" Then
                            Link_objtype = "OLSE"
                            Link_Value = Matrix0.Columns.Item("docno").Cells.Item(pVal.Row).Specific.string
                            Dim oactiveform As New FrmSettlment
                            oactiveform.Show()
                            BubbleEvent = False
                        ElseIf Matrix0.Columns.Item("trtype").Cells.Item(pVal.Row).Specific.string = "OPRC" Then
                            Link_objtype = "OPRC"
                            Link_Value = Matrix0.Columns.Item("docno").Cells.Item(pVal.Row).Specific.string
                            Dim oactiveform As New frmPayrollProcess
                            oactiveform.Show()
                            BubbleEvent = False
                        End If
                    End If
                ElseIf pVal.ColUID = "pje" Then
                    If Matrix0.Columns.Item("pje").Cells.Item(pVal.Row).Specific.string = "" Or Matrix0.Columns.Item("pje").Cells.Item(pVal.Row).Specific.string = "-1" Then
                        BubbleEvent = False
                    End If
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub EditText12_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText12.LostFocusAfter
            Try
                objaddon.objglobalmethods.LoadCombo_Series(objform, "cmbseri", "OLOA", IIf(EditText12.String = "", Now.Date, Date.ParseExact(EditText12.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)))
            Catch ex As Exception
            End Try
        End Sub

        Private Sub Button1_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button1.PressedAfter
            Try
                If pVal.ActionSuccess = True And objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                    If addupdate = True Then objaddon.objapplication.StatusBar.SetText("Loan Application Added and Document Sent for Approval", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Success)
                    EditText25.Value = objaddon.objglobalmethods.GetNextDocentry_Value("@SMPR_OLOA")
                    objform.Items.Item("txtdocdt").Specific.string = Now.Date.ToString("yyyyMMdd")
                    EditText0.Item.Click(BoCellClickType.ct_Regular)
                ElseIf pVal.ActionSuccess = True And objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then
                    If addupdate = True Then objaddon.objapplication.StatusBar.SetText("Loan Application Updated and Document Sent for Approval", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Success)
                    objaddon.objapplication.Menus.Item("1304").Activate()
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Button1_PressedBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button1.PressedBefore
            Try
                If (objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Or objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE) Then
                    addupdate = True
                Else
                    addupdate = False
                End If
            Catch ex As Exception

            End Try
        End Sub
        Private WithEvents CheckBox2 As SAPbouiCOM.CheckBox

        Private Sub EditText4_ChooseFromListBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles EditText4.ChooseFromListBefore
            Try
                Dim oCFL As SAPbouiCOM.ChooseFromList
                Dim oConds As SAPbouiCOM.Conditions
                Dim oCond As SAPbouiCOM.Condition
                Dim oEmptyConds As New SAPbouiCOM.Conditions

                oCFL = objform.ChooseFromLists.Item("cflloan")
                oCFL.SetConditions(oEmptyConds)
                oConds = oCFL.GetConditions()

                oCond = oConds.Add()
                oCond.Alias = "U_Active"
                oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                oCond.CondVal = "Y"
                'oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND

                oCFL.SetConditions(oConds)
            Catch ex As Exception

            End Try
        End Sub
        Dim posted_entryno As String
        Dim lretcode
        Private Sub LoanApplication()
            Dim strsql As String = ""
            Try

                Try
                    Dim FDate As Date = Date.ParseExact(objform.Items.Item("txtdocdt").Specific.String, "dd/MM/yy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                    objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    strsql = "select 'OLOA' ""Transcode"",'Loan Application " & objform.Items.Item("txtiempid").Specific.string & "' ""Memo"", 'Loan Application - Entry No : " & objform.Items.Item("txtentry").Specific.string & " Appl No:" & objform.Items.Item("txtdocno").Specific.string & " Loan Type :' ""Narration"",	"
                    strsql += vbCrLf + " 'Employee No :" & objform.Items.Item("txtiempid").Specific.string & " ID :" & objform.Items.Item("txtempid").Specific.string & "' ""Ref1"",'" & objform.Items.Item("txtempname").Specific.string & "' ""Ref2"",T3.""Name"" ""Ref3"","
                    strsql += vbCrLf + " T1.""U_loancode"",T1.""U_loandc"" ""DebitCode"",T1.""U_loancc"" ""CreditCode"" from ""@SMPR_ACCT"" T0 inner join ""@SMPR_ACCT1"" T1  on T0.""Code""=T1.""Code"" "
                    strsql += vbCrLf + " inner join ""@SMPR_OLOA"" T2 on T2.""U_LoanCode""=T1.""U_loancode"" inner join ""@SMPR_OLON"" T3 on T2.""U_LoanCode"" =T3.""Code"" where '" & FDate.ToString("yyyy/MM/dd") & "' between T0.""U_fromdate"" and T0.""U_todate"""
                    strsql += vbCrLf + " and ifnull(T1.""U_loancc"",'')<>'' and ifnull(T1.""U_loandc"",'')<>''"
                    objrs.DoQuery(strsql)
                    If CheckBox1.Checked = True And CheckBox0.Checked = False Then
                        If Not objaddon.objcompany.InTransaction Then objaddon.objcompany.StartTransaction()
                        'Dim oloanjv As SAPbobsCOM.JournalVouchers
                        'oloanjv = objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalVouchers)

                        Dim oloanjv As SAPbobsCOM.JournalEntries
                        oloanjv = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries)

                        oloanjv.ReferenceDate = FDate.ToString("yyyy/MM/dd") 'objrs.Fields.Item("Date").Value
                        oloanjv.DueDate = FDate.ToString("yyyy/MM/dd") 'objrs.Fields.Item("Date").Value
                        oloanjv.TaxDate = FDate.ToString("yyyy/MM/dd") 'objrs.Fields.Item("Date").Value
                        'If objrs.Fields.Item("Transcode").Value.ToString <> "" Then oloanjv.TransactionCode = objrs.Fields.Item("Transcode").Value.ToString

                        If objrs.Fields.Item("Memo").Value.ToString <> "" Then oloanjv.Memo = objrs.Fields.Item("Memo").Value.ToString
                        If objrs.Fields.Item("Narration").Value.ToString <> "" Then oloanjv.UserFields.Fields.Item("U_Narration").Value = objrs.Fields.Item("Narration").Value.ToString

                        If objrs.Fields.Item("Ref1").Value.ToString <> "" Then oloanjv.Reference = objrs.Fields.Item("Ref1").Value.ToString
                        If objrs.Fields.Item("Ref2").Value.ToString <> "" Then oloanjv.Reference2 = objrs.Fields.Item("Ref2").Value.ToString
                        If objrs.Fields.Item("Ref3").Value.ToString <> "" Then oloanjv.Reference3 = objrs.Fields.Item("Ref3").Value.ToString

                        oloanjv.Lines.AccountCode = objrs.Fields.Item("DebitCode").Value
                        oloanjv.Lines.Debit = CDbl(objform.Items.Item("txtlamt").Specific.string) 'objrs.Fields.Item("DebitCode").Value
                        'If objrs.Fields.Item("Ocrcode1").Value.ToString <> "" Then oloanjv.Lines.CostingCode = objrs.Fields.Item("Ocrcode1").Value
                        'If objrs.Fields.Item("Ocrcode2").Value.ToString <> "" Then oloanjv.Lines.CostingCode2 = objrs.Fields.Item("Ocrcode2").Value
                        'If objrs.Fields.Item("Ocrcode3").Value.ToString <> "" Then oloanjv.Lines.CostingCode3 = objrs.Fields.Item("Ocrcode3").Value
                        'If objrs.Fields.Item("Ocrcode4").Value.ToString <> "" Then oloanjv.Lines.CostingCode4 = objrs.Fields.Item("Ocrcode4").Value
                        'If objrs.Fields.Item("Ocrcode5").Value.ToString <> "" Then oloanjv.Lines.CostingCode5 = objrs.Fields.Item("Ocrcode5").Value
                        oloanjv.Lines.Add()

                        oloanjv.Lines.AccountCode = objrs.Fields.Item("CreditCode").Value
                        oloanjv.Lines.Credit = CDbl(objform.Items.Item("txtlamt").Specific.string) 'objrs.Fields.Item("Amount").Value
                        'If objrs.Fields.Item("Ocrcode1").Value.ToString <> "" Then oloanjv.Lines.CostingCode = objrs.Fields.Item("Ocrcode1").Value
                        'If objrs.Fields.Item("Ocrcode2").Value.ToString <> "" Then oloanjv.Lines.CostingCode2 = objrs.Fields.Item("Ocrcode2").Value
                        'If objrs.Fields.Item("Ocrcode3").Value.ToString <> "" Then oloanjv.Lines.CostingCode3 = objrs.Fields.Item("Ocrcode3").Value
                        'If objrs.Fields.Item("Ocrcode4").Value.ToString <> "" Then oloanjv.Lines.CostingCode4 = objrs.Fields.Item("Ocrcode4").Value
                        'If objrs.Fields.Item("Ocrcode5").Value.ToString <> "" Then oloanjv.Lines.CostingCode5 = objrs.Fields.Item("Ocrcode5").Value
                        oloanjv.Lines.Add()

                        'oloanjv.JournalEntries.Add()

                        lretcode = oloanjv.Add()
                        If lretcode <> 0 Then
                            If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                            objaddon.objglobalmethods.status_Update("OLOA", objform.Items.Item("txtentry").Specific.string, 0, objaddon.objcompany.GetLastErrorDescription, -1)
                        Else
                            posted_entryno = objaddon.objcompany.GetNewObjectKey()
                            objaddon.objglobalmethods.status_Update("OLOA", objform.Items.Item("txtentry").Specific.string, 1, "Success", posted_entryno.ToString)
                            If objaddon.objglobalmethods.Update_query("update ""@SMPR_OLOA"" set ""U_jeno""='" & posted_entryno & "' where ""DocEntry""='" & objform.Items.Item("txtentry").Specific.string & "'") Then

                                If EditText10.Value <> "" Then
                                    If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                                Else
                                    If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                                    objform.Items.Item("txtjeno").Specific.string = posted_entryno
                                    objaddon.objapplication.SetStatusBarMessage("Journal Entry Successfully Posted..." & posted_entryno, SAPbouiCOM.BoMessageTime.bmt_Long, False)
                                End If
                            Else
                                If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                            End If
                        End If
                    Else
                        Exit Sub
                    End If
                Catch ex As Exception
                    If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                    objaddon.objglobalmethods.status_Update("OLOA", objform.Items.Item("txtentry").Specific.string, 0, objaddon.objcompany.GetLastErrorDescription, -1)
                End Try
                objrs.MoveNext()
                'Next
                'End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Button1_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button1.ClickAfter

            'Try
            '    If objform.Mode = BoFormMode.fm_ADD_MODE Or objform.Mode = BoFormMode.fm_UPDATE_MODE Then
            '        If CheckBox1.Checked = True And CheckBox0.Checked = False Then
            '            If ApprovedUser_Employee Then
            '                LoanApplication()
            '            Else
            '                objaddon.objapplication.SetStatusBarMessage("You are not authorized to post JE", SAPbouiCOM.BoMessageTime.bmt_Long, True)
            '                Exit Sub
            '            End If
            '        Else
            '            'objaddon.objapplication.SetStatusBarMessage("Please approve to Post JE", SAPbouiCOM.BoMessageTime.bmt_Long, False)
            '            Exit Sub
            '        End If
            '    End If
            'Catch ex As Exception

            'End Try

        End Sub

        Private Sub EditText0_ChooseFromListBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles EditText0.ChooseFromListBefore
            Try

                Dim oCFL As SAPbouiCOM.ChooseFromList = objform.ChooseFromLists.Item("cflemp")
                Dim oConds As SAPbouiCOM.Conditions
                Dim oCond As SAPbouiCOM.Condition
                Dim oEmptyConds As New SAPbouiCOM.Conditions
                oCFL.SetConditions(oEmptyConds)
                oConds = oCFL.GetConditions()
                oCond = oConds.Add()
                oCond.Alias = "U_Status"
                oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                oCond.CondVal = "1"
                oCond.Relationship = BoConditionRelationship.cr_AND
                oCond = oConds.Add()
                oCond.Alias = "U_loanelgi"
                oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                oCond.CondVal = "Y"
                oCFL.SetConditions(oConds)

            Catch ex As Exception

            End Try
        End Sub

        Private Sub CheckBox0_PressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles CheckBox0.PressedAfter
            If CheckBox0.Checked = True Then
                CheckBox1.Item.Enabled = False
                CheckBox0.Item.Enabled = False
            End If

        End Sub

        Private Sub CheckBox1_PressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles CheckBox1.PressedAfter
            If CheckBox1.Checked = True Then
                CheckBox0.Item.Enabled = False
                CheckBox1.Item.Enabled = False
            End If
        End Sub


    End Class
End Namespace
