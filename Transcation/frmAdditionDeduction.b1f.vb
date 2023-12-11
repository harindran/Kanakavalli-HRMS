Option Strict Off
Option Explicit On
Imports SAPbouiCOM.Framework
Namespace HRMS
    <FormAttribute("OPAD", "Transcation/frmAdditionDeduction.b1f")>
    Friend Class frmAdditionDeduction
        Inherits UserFormBase
        Private WithEvents objform As SAPbouiCOM.Form
        Dim FormCount As Integer = 0
        Dim objrs As SAPbobsCOM.Recordset
        Private WithEvents pCFL As SAPbouiCOM.ISBOChooseFromListEventArg
        Dim addupdate As Boolean = False
        Public Sub New()
        End Sub
        Public Overrides Sub OnInitializeComponent()
            Me.Button0 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.Button1 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.StaticText0 = CType(Me.GetItem("lblperiod").Specific, SAPbouiCOM.StaticText)
            Me.StaticText1 = CType(Me.GetItem("Item_4").Specific, SAPbouiCOM.StaticText)
            Me.EditText1 = CType(Me.GetItem("txtfdate").Specific, SAPbouiCOM.EditText)
            Me.EditText2 = CType(Me.GetItem("txttdate").Specific, SAPbouiCOM.EditText)
            Me.StaticText3 = CType(Me.GetItem("Item_8").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox0 = CType(Me.GetItem("cmbseries").Specific, SAPbouiCOM.ComboBox)
            Me.EditText3 = CType(Me.GetItem("txtdocno").Specific, SAPbouiCOM.EditText)
            Me.StaticText4 = CType(Me.GetItem("Item_11").Specific, SAPbouiCOM.StaticText)
            Me.EditText4 = CType(Me.GetItem("txtdate").Specific, SAPbouiCOM.EditText)
            Me.StaticText5 = CType(Me.GetItem("Item_13").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox1 = CType(Me.GetItem("cmbstatus").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText6 = CType(Me.GetItem("Item_15").Specific, SAPbouiCOM.StaticText)
            Me.EditText5 = CType(Me.GetItem("Item_16").Specific, SAPbouiCOM.EditText)
            Me.Matrix0 = CType(Me.GetItem("Item_17").Specific, SAPbouiCOM.Matrix)
            Me.EditText6 = CType(Me.GetItem("txtentry").Specific, SAPbouiCOM.EditText)
            Me.ComboBox2 = CType(Me.GetItem("cmbperiod").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText7 = CType(Me.GetItem("ltadd").Specific, SAPbouiCOM.StaticText)
            Me.EditText0 = CType(Me.GetItem("ttadd").Specific, SAPbouiCOM.EditText)
            Me.StaticText8 = CType(Me.GetItem("ltded").Specific, SAPbouiCOM.StaticText)
            Me.EditText7 = CType(Me.GetItem("ttded").Specific, SAPbouiCOM.EditText)
            Me.CheckBox0 = CType(Me.GetItem("Item_0").Specific, SAPbouiCOM.CheckBox)
            Me.CheckBox1 = CType(Me.GetItem("Item_1").Specific, SAPbouiCOM.CheckBox)
            Me.OnCustomInitialize()
        End Sub
        Public Overrides Sub OnInitializeFormEvents()
        End Sub
        Private Sub OnCustomInitialize()
            Try
                objform = objaddon.objapplication.Forms.GetForm("OPAD", Me.FormCount)
                objform = objaddon.objapplication.Forms.ActiveForm
                objform.Freeze(True)
                CheckBox0.Item.Height = CheckBox0.Item.Height + 3
                CheckBox1.Item.Height = CheckBox1.Item.Height + 3
                loadcombobox()
                objform.Items.Item("txtdate").Specific.string = DateTime.Now.ToString("dd/MM/yy")
                EditText6.Value = objaddon.objglobalmethods.GetNextDocentry_Value("@SMPR_OPAD")
                If objaddon.ApprovedUser() Then
                    CheckBox0.Item.Enabled = True
                    CheckBox1.Item.Enabled = True
                End If
                Matrix0.Columns.Item("empid").Visible = False
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "Item_17", True, False, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cmbperiod", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cmbseries", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtdate", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtdocno", False, True, False)
                objform.ActiveItem = "cmbperiod"
                objaddon.objapplication.Menus.Item("1300").Activate()
                ComboBox0.Select(0, SAPbouiCOM.BoSearchKey.psk_Index)
                objform.Settings.Enabled = True
                objform.EnableMenu("1292", False) 'Add row
                objform.EnableMenu("1293", False) 'Delete Row
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
        Private Sub loadcombobox()
            Try
                'objaddon.objglobalmethods.LoadCombo(ComboBox2, "select code,name from OFPR where isnull(U_HR,'N')='Y' ")
                Dim cmbdesignation As SAPbouiCOM.Column = Matrix0.Columns.Item("desig")
                Dim cmbdepartment As SAPbouiCOM.Column = Matrix0.Columns.Item("dept")
                Dim objrs As SAPbobsCOM.Recordset
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery("CALL ""MIPL_HRMS_EMPMASTER_COMBO_FILLING"" ('OPAD')")
                If objrs.RecordCount = 0 Then Exit Sub
                For i As Integer = 0 To objrs.RecordCount - 1
                    Try
                        Select Case objrs.Fields.Item("Type").Value.ToString.ToUpper
                            Case "POSITION" : cmbdesignation.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "DEPARTMENT" : cmbdepartment.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "PAYPERIOD" : ComboBox2.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                        End Select
                        objrs.MoveNext()
                    Catch ex As Exception
                        objrs.MoveNext()
                    End Try
                Next
            Catch ex As Exception
            End Try
        End Sub
#Region "Field Details"
        Private WithEvents Button0 As SAPbouiCOM.Button
        Private WithEvents Button1 As SAPbouiCOM.Button
        Private WithEvents StaticText0 As SAPbouiCOM.StaticText
        Private WithEvents StaticText1 As SAPbouiCOM.StaticText
        Private WithEvents EditText1 As SAPbouiCOM.EditText
        Private WithEvents EditText2 As SAPbouiCOM.EditText
        Private WithEvents StaticText3 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox0 As SAPbouiCOM.ComboBox
        Private WithEvents EditText3 As SAPbouiCOM.EditText
        Private WithEvents StaticText4 As SAPbouiCOM.StaticText
        Private WithEvents EditText4 As SAPbouiCOM.EditText
        Private WithEvents StaticText5 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox1 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText6 As SAPbouiCOM.StaticText
        Private WithEvents EditText5 As SAPbouiCOM.EditText
        Private WithEvents Matrix0 As SAPbouiCOM.Matrix
        Private WithEvents EditText6 As SAPbouiCOM.EditText
        Private WithEvents ComboBox2 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText7 As SAPbouiCOM.StaticText
        Private WithEvents EditText0 As SAPbouiCOM.EditText
        Private WithEvents StaticText8 As SAPbouiCOM.StaticText
        Private WithEvents EditText7 As SAPbouiCOM.EditText
        Private WithEvents CheckBox0 As SAPbouiCOM.CheckBox
        Private WithEvents CheckBox1 As SAPbouiCOM.CheckBox
#End Region
        Private Sub ComboBox2_ComboSelectAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox2.ComboSelectAfter
            If objform.Mode <> SAPbouiCOM.BoFormMode.fm_ADD_MODE Then Exit Sub
            If ComboBox2.Selected Is Nothing Then Exit Sub
            objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            objrs.DoQuery("select TO_VARCHAR(""F_RefDate"",'dd/MM/yy') ""F_RefDate"",TO_VARCHAR(""T_RefDate"",'dd/MM/yy') ""T_RefDate"" from OFPR where ""Code""='" & ComboBox2.Selected.Value & "'")
            If objrs.RecordCount > 0 Then
                objform.Items.Item("txtfdate").Specific.string = objrs.Fields.Item("F_RefDate").Value
                objform.Items.Item("txttdate").Specific.string = objrs.Fields.Item("T_RefDate").Value
            End If
            AddRow_IN_Matrix()
        End Sub
        Private Sub ComboBox0_ComboSelectAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox0.ComboSelectAfter
            If objform.Mode <> SAPbouiCOM.BoFormMode.fm_ADD_MODE Then Exit Sub
            If ComboBox0.Selected Is Nothing Then Exit Sub
            objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            objrs.DoQuery("Select ""NextNumber"" from Nnm1 where ""ObjectCode""='OPAD' and ""Series""='" & ComboBox0.Selected.Value & "'")
            If objrs.RecordCount > 0 Then
                EditText3.Value = objrs.Fields.Item(0).Value
            End If
        End Sub
        Private Sub AddRow_IN_Matrix()
            If Matrix0.VisualRowCount > 0 Then
                If Matrix0.Columns.Item("trzid").Cells.Item(Matrix0.VisualRowCount).Specific.string = "" Then Exit Sub
            End If
            Matrix0.AddRow(1)
            Matrix0.ClearRowData(Matrix0.VisualRowCount)
            Matrix0.Columns.Item("#").Cells.Item(Matrix0.VisualRowCount).Specific.string = Matrix0.VisualRowCount
            Matrix0.Columns.Item("date").Cells.Item(Matrix0.VisualRowCount).Specific.string = objform.Items.Item("txttdate").Specific.string
        End Sub
        Private Sub Matrix0_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.ChooseFromListAfter
            If pVal.ColUID = "trzid" And pVal.ActionSuccess = True Then
                Try
                    pCFL = pVal
                    If pCFL.SelectedObjects Is Nothing Then Exit Sub
                    Dim strEmpid As String = "#"
                    For i = 0 To pCFL.SelectedObjects.Rows.Count - 1
                        Try
                            strEmpid = strEmpid + pCFL.SelectedObjects.Columns.Item("U_ExtEmpNo").Cells.Item(i).Value + "#"
                        Catch ex As Exception
                        End Try
                    Next
                    If strEmpid <> "#" Then Load_employees(strEmpid, pVal.Row)
                    objaddon.objapplication.Menus.Item("1300").Activate()
                    Matrix0.Columns.Item("trzid").Cells.Item(pVal.Row).Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                Catch ex As Exception
                Finally
                    GC.Collect()
                    GC.WaitForPendingFinalizers()
                End Try
            ElseIf pVal.ColUID = "paycode" Then
                Try
                    pCFL = pVal
                    If pCFL.SelectedObjects Is Nothing Then Exit Sub
                    Try
                        Matrix0.Columns.Item("paycode").Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("Code").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                    Matrix0.Columns.Item("payname").Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("Name").Cells.Item(0).Value
                Catch ex As Exception
                End Try
            End If
        End Sub
        Private Sub Matrix0_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Matrix0.ChooseFromListBefore
            Dim oCFL As SAPbouiCOM.ChooseFromList
            Dim oConds As SAPbouiCOM.Conditions
            Dim oCond As SAPbouiCOM.Condition
            Dim oEmptyConds As New SAPbouiCOM.Conditions
            If pVal.ColUID = "trzid" Then
                Try
                    oCFL = objform.ChooseFromLists.Item("empde")
                    oCFL.SetConditions(oEmptyConds)
                    oConds = oCFL.GetConditions()
                    oCond = oConds.Add()
                    oCond.Alias = "U_status"
                    oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                    oCond.CondVal = "1"
                    oCFL.SetConditions(oConds)
                    'For i As Integer = 1 To Matrix0.VisualRowCount
                    '    If Matrix0.Columns.Item("trzid").Cells.Item(i).Specific.string = "" Then Continue For
                    '    If i = pVal.Row Then Continue For
                    '    If i <> 1 Then oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND
                    '    oCond = oConds.Add()
                    '    oCond.Alias = "U_ExtEmpNo"
                    '    oCond.Operation = SAPbouiCOM.BoConditionOperation.co_NOT_EQUAL
                    '    oCond.CondVal = Matrix0.Columns.Item("trzid").Cells.Item(i).Specific.string
                    'Next
                    oCFL.SetConditions(oConds)
                Catch ex As Exception
                End Try
            ElseIf pVal.ColUID = "paycode" Then
                Dim cmbtype As SAPbouiCOM.ComboBox = Matrix0.Columns.Item("type").Cells.Item(pVal.Row).Specific
                oCFL = objform.ChooseFromLists.Item("paytype")
                oCFL.SetConditions(oEmptyConds)
                oConds = oCFL.GetConditions()
                oCond = oConds.Add()
                oCond.Alias = "U_Active"
                oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                oCond.CondVal = "Y"
                oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND
                oCond = oConds.Add()
                oCond.Alias = "U_Type"
                oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                If cmbtype.Selected Is Nothing Then
                    oCond.CondVal = ""
                Else
                    oCond.CondVal = cmbtype.Selected.Value
                End If
                oCFL.SetConditions(oConds)
            End If
        End Sub
        Private Sub Load_employees(ByVal strempid As String, ByVal rowno As Integer)
            Try
                Dim ocombo As SAPbouiCOM.ComboBox
                objaddon.objapplication.SetStatusBarMessage("Loading Employee Details.Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Long, False)
                objform.Freeze(True)
                Dim strsql As String = " Do begin Declare EMPID nvarchar(5000); "
                strsql += vbCrLf + " EMPID :='" & strempid & "'; "
                strsql += vbCrLf + "Select T0.""U_empID"",T0.""U_ExtEmpNo"",T0.""U_firstNam"" ||' '||T0.""U_lastName"" ""empName"",T0.""U_dept"",T0.""U_position"" ""Desig""  from ""@SMPR_OHEM"" T0"
                'strsql += vbCrLf + " inner join (select ""Rowno"",""splitdata"" from ""fnSplitString""(:EMPID)) S on S.""splitdata""=T0.""U_ExtEmpNo"""
                strsql += vbCrLf + " where  :EMPID like '%#' || ""U_ExtEmpNo"" ||'#%' ;" ' Order by S.""Rowno""; "
                strsql += vbCrLf + "end;"
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strsql)
                ' objform.ActiveItem = "txtremarks"
                For i = 1 To objrs.RecordCount
                    Try
                        Matrix0.Columns.Item("trzid").Cells.Item(rowno).Specific.String = objrs.Fields.Item("U_ExtEmpNo").Value
                    Catch ex As Exception
                    End Try
                    Matrix0.Columns.Item("empid").Cells.Item(rowno).Specific.String = objrs.Fields.Item("U_empID").Value
                    Matrix0.Columns.Item("empname").Cells.Item(rowno).Specific.String = objrs.Fields.Item("empName").Value
                    If objrs.Fields.Item("U_dept").Value.ToString <> "" Then
                        ocombo = Matrix0.Columns.Item("dept").Cells.Item(rowno).Specific
                        ocombo.Select(objrs.Fields.Item("U_dept").Value.ToString, SAPbouiCOM.BoSearchKey.psk_ByValue)
                    End If
                    If objrs.Fields.Item("desig").Value.ToString <> "" Then
                        ocombo = Matrix0.Columns.Item("desig").Cells.Item(rowno).Specific
                        ocombo.Select(objrs.Fields.Item("Desig").Value.ToString, SAPbouiCOM.BoSearchKey.psk_ByValue)
                    End If
                    objrs.MoveNext()
                    If rowno = Matrix0.VisualRowCount Then
                        AddRow_IN_Matrix()
                    End If
                    rowno = rowno + 1
                Next
                objform.Freeze(False)
                objaddon.objapplication.SetStatusBarMessage("Employee Details Loaded Successfully.", SAPbouiCOM.BoMessageTime.bmt_Short, False)
            Catch ex As Exception
                objform.Freeze(False)
                'objaddon.objapplication.SetStatusBarMessage("Error While Loading Employee Details in Daily Attendance" & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, True)
            End Try
        End Sub
        Private Sub Matrix0_ComboSelectAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.ComboSelectAfter
            If pVal.ColUID = "type" Then load_total()
        End Sub
        Private Sub Matrix0_LinkPressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.LinkPressedAfter
            If pVal.ColUID = "trzid" Then
                If Matrix0.Columns.Item("empid").Cells.Item(pVal.Row).Specific.string = "" Then Exit Sub
                Link_Value = Matrix0.Columns.Item("empid").Cells.Item(pVal.Row).Specific.string : Link_objtype = "OHEM"
                Dim activeform As New frmEmployeeMaster
                activeform.Show()
            ElseIf pVal.ColUID = "paycode" Then
                If Matrix0.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Specific.string = "" Then Exit Sub
                Link_Value = Matrix0.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Specific.string : Link_objtype = "MSTRPAYE"
                Dim activeform As New frmPayElement
                activeform.Show()
            End If
        End Sub
        Private Sub Matrix0_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.LostFocusAfter
            If pVal.ColUID = "trzid" Then AddRow_IN_Matrix()
            If pVal.ColUID = "amount" Then load_total()
        End Sub
        Private Sub Button0_ClickBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button0.ClickBefore
            If objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE Or objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then Exit Sub
            objform.ActiveItem = "Item_16"
            If ComboBox2.Selected Is Nothing Then
                objaddon.objapplication.SetStatusBarMessage("Pay Period is Missing", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                objform.ActiveItem = "cmbperiod"
                BubbleEvent = False : Exit Sub
            End If
            If ComboBox0.Selected Is Nothing Then
                objaddon.objapplication.SetStatusBarMessage("Series is Missing", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                objform.ActiveItem = "cmbseries"
                BubbleEvent = False : Exit Sub
            End If
            If EditText4.Value = "" Then
                objaddon.objapplication.SetStatusBarMessage("Document Date is Missing", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                objform.ActiveItem = "txtdate"
                BubbleEvent = False : Exit Sub
            End If
            If Matrix0.VisualRowCount = 0 Then
                objaddon.objapplication.SetStatusBarMessage("Line Details is Missing", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                BubbleEvent = False : Exit Sub
            End If
            If Matrix0.Columns.Item("trzid").Cells.Item(Matrix0.VisualRowCount).Specific.String = "" And Matrix0.VisualRowCount > 1 Then Matrix0.DeleteRow(Matrix0.VisualRowCount)
            If Matrix0.VisualRowCount > 1 Then
                Dim deletedrow As Boolean = False
                For i As Integer = Matrix0.VisualRowCount To 1 Step -1
                    If Matrix0.Columns.Item("trzid").Cells.Item(i).Specific.String = "" Then
                        Matrix0.ClearRowData(i)
                        Matrix0.DeleteRow(i)
                        deletedrow = True
                    End If
                Next
                If deletedrow = True Then
                    For i As Integer = 1 To Matrix0.visualrowRowCount
                        Matrix0.Columns.Item("#").Cells.Item(i).Specific.String = i
                    Next
                End If
            End If
            load_total()
        End Sub
        Private Sub load_total()
            Dim tadd As Double = 0.0, tded As Double = 0.0
            Dim cmbtype As SAPbouiCOM.ComboBox
            For i As Integer = 1 To Matrix0.VisualRowCount
                cmbtype = Matrix0.Columns.Item("type").Cells.Item(i).Specific
                If cmbtype.Selected Is Nothing Then Continue For
                If cmbtype.Selected.Value = "A" Then tadd = tadd + Matrix0.Columns.Item("amount").Cells.Item(i).Specific.string
                If cmbtype.Selected.Value = "D" Then tded = tded + Matrix0.Columns.Item("amount").Cells.Item(i).Specific.string
            Next
            EditText0.Value = tadd
            EditText7.Value = tded
        End Sub
        Private Sub EditText4_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText4.LostFocusAfter
            Try
                objaddon.objglobalmethods.LoadCombo_Series(objform, "cmbseries", "OPAD", IIf(EditText4.String = "", Now.Date, Date.ParseExact(EditText4.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)))
            Catch ex As Exception
            End Try
        End Sub
        Private Sub frmAdditionDeduction_DataLoadAfter(ByRef pVal As SAPbouiCOM.BusinessObjectInfo) Handles Me.DataLoadAfter
            Try
                objaddon.objapplication.Menus.Item("1300").Activate()
                objaddon.objglobalmethods.LoadCombo_SingleSeries_AfterFind(objform, "cmbseries", "OPAD", ComboBox0.Value)
                If objaddon.ApprovedUser() Then
                    If CheckBox0.Checked = True Or CheckBox1.Checked = True Then
                        CheckBox0.Item.Enabled = False
                        CheckBox1.Item.Enabled = False
                    Else
                        CheckBox0.Item.Enabled = True
                        CheckBox1.Item.Enabled = True
                    End If
                End If
                If ApprovedUser_Employee = False Or ComboBox1.Selected Is Nothing Then objform.EnableMenu("1284", False) : EditText5.Item.Enabled = False : Exit Sub
                If ComboBox1.Selected.Value.ToString.ToUpper = "C" Or ComboBox1.Selected.Value.ToString.ToUpper = "D" Then
                    EditText5.Item.Enabled = False : objform.EnableMenu("1284", False) : Exit Sub
                End If
                Dim strsql As String = "select 1 from ""@SMPR_OPRC"" where ifnull(""U_Process"",'')='Y' and ""U_FromDate""='" & EditText1.Value & "' and ""U_todate""='" & EditText2.Value & "'"
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strsql)
                If objrs.RecordCount > 0 Then
                    objform.EnableMenu("1284", False) : EditText5.Item.Enabled = False
                Else
                    objform.EnableMenu("1284", True) : EditText5.Item.Enabled = True
                End If
            Catch ex As Exception
            End Try
        End Sub
        Private Sub Button0_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button0.PressedAfter
            Try
                If pVal.ActionSuccess = True And objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                    If addupdate = True Then objaddon.objapplication.StatusBar.SetText("Addition/Deduction Document Sent for Approval", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                    objform.Items.Item("txtdate").Specific.string = DateTime.Now.ToString("dd/MM/yy")
                    EditText6.Value = objaddon.objglobalmethods.GetNextDocentry_Value("@SMPR_OPAD")
                    objform.ActiveItem = "cmbperiod"
                ElseIf pVal.ActionSuccess = True And objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then
                    If addupdate = True Then objaddon.objapplication.StatusBar.SetText("Addition/Deduction Updated and Document Sent for Approval", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                    objaddon.objapplication.Menus.Item("1304").Activate()
                End If
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
        Private Sub CheckBox0_PressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles CheckBox0.PressedAfter
            If CheckBox0.Checked = True Then
                CheckBox1.Item.Enabled = False
                CheckBox0.Item.Enabled = False
            End If
        End Sub
        Private Sub CheckBox1_PressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles CheckBox1.PressedAfter
            If CheckBox1.Checked = True Then
                CheckBox1.Item.Enabled = False
                CheckBox0.Item.Enabled = False
            End If
        End Sub
    End Class
End Namespace
