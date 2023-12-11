Option Strict Off
Option Explicit On

Imports SAPbouiCOM.Framework

Namespace HRMS
    <FormAttribute("OELM", "Master/FrmEmpLeaveMaster.b1f")>
    Friend Class FrmEmpLeaveMaster
        Inherits UserFormBase
        Private WithEvents objform As SAPbouiCOM.Form
        Dim formcount As Integer = 0
        Private WithEvents odbdsDetails As SAPbouiCOM.DBDataSource
        Dim strsql As String
        Dim objrs As SAPbobsCOM.Recordset
        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.Button3 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.Button4 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.StaticText3 = CType(Me.GetItem("lblbranch").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox2 = CType(Me.GetItem("cmbbranch").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText4 = CType(Me.GetItem("lblloc").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox3 = CType(Me.GetItem("cmbloc").Specific, SAPbouiCOM.ComboBox)
            Me.Button5 = CType(Me.GetItem("btndata").Specific, SAPbouiCOM.Button)
            Me.StaticText5 = CType(Me.GetItem("lblcode").Specific, SAPbouiCOM.StaticText)
            Me.EditText0 = CType(Me.GetItem("txtcode").Specific, SAPbouiCOM.EditText)
            Me.Matrix1 = CType(Me.GetItem("mtxdata").Specific, SAPbouiCOM.Matrix)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()

        End Sub
        Dim RecCount As String
        Private Sub OnCustomInitialize()
            Try
                objform = objaddon.objapplication.Forms.GetForm("OELM", Me.formcount)
                objform = objaddon.objapplication.Forms.ActiveForm
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                EditText0.Value = objaddon.objglobalmethods.GetNextCode_Value("@MIPL_OLM")
                LoadComboDetails()
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtcode", True, True, False)
                'strsql = "Select ""Code"",""Name"" ""ColName"",'U_'||""Code"" ""FieldName"" from ""@SMPR_OLVE"" where ""U_Active""='Y' and ""U_empmastr""='Y'"
                'objrs.DoQuery(strsql)
                'If objrs.RecordCount > 0 Then
                '    For Rec As Integer = 0 To objrs.RecordCount - 1
                '        If Dynamic_UDF(Matrix1, objrs.Fields.Item("Code").Value.ToString, "@MIPL_OLM1", objrs.Fields.Item("ColName").Value.ToString, objrs.Fields.Item("FieldName").Value.ToString) = False Then
                '            Exit For
                '        End If
                '        objrs.MoveNext()
                '    Next
                'End If
                objaddon.objapplication.Menus.Item("1300").Activate() 'Fit colum width
                RecCount = objaddon.objglobalmethods.getSingleValue("select Count(*) from ""@MIPL_OLM"";")
                If RecCount = "1" Then
                    objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE
                    EditText0.Item.Enabled = True
                    EditText0.Value = "1"
                    objform.ActiveItem = "cmbbranch"
                    objform.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                    EditText0.Item.Enabled = False
                    Exit Sub
                End If

            Catch ex As Exception
            End Try
        End Sub
#Region "Fields"
        Private WithEvents Button3 As SAPbouiCOM.Button
        Private WithEvents Button4 As SAPbouiCOM.Button
        Private WithEvents StaticText3 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox2 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText4 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox3 As SAPbouiCOM.ComboBox
        Private WithEvents Button5 As SAPbouiCOM.Button
        Private WithEvents StaticText5 As SAPbouiCOM.StaticText
        Private WithEvents EditText0 As SAPbouiCOM.EditText
        Private WithEvents Matrix1 As SAPbouiCOM.Matrix
#End Region

        Private Sub LoadComboDetails()
            Try
                ComboBox2.ValidValues.Add("-1", "All")
                ComboBox3.ValidValues.Add("-1", "All")
                Dim objrs As SAPbobsCOM.Recordset
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery("CALL ""MIPL_HRMS_EMPMASTER_COMBO_FILLING"" ('OHEM')")
                If objrs.RecordCount = 0 Then Exit Sub
                For i As Integer = 0 To objrs.RecordCount - 1
                    Try
                        Select Case objrs.Fields.Item("Type").Value.ToString.ToUpper
                            Case "BRANCH" : ComboBox2.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "LOCATION" : ComboBox3.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                        End Select
                        objrs.MoveNext()
                    Catch ex As Exception
                        objrs.MoveNext()
                    End Try
                Next
                ComboBox2.Select(0, SAPbouiCOM.BoSearchKey.psk_Index)
                ComboBox3.Select(0, SAPbouiCOM.BoSearchKey.psk_Index)
            Catch ex As Exception

            End Try
        End Sub

        Private Function Dynamic_UDF(ByVal MatrixID As SAPbouiCOM.Matrix, ByVal UID As String, ByVal TableName As String, ByVal Descr As String, ByVal FieldName As String) As Boolean
            Try
                If objaddon.HANA Then
                    strsql = objaddon.objglobalmethods.getSingleValue("SELECT COUNT(*) FROM CUFD WHERE ""TableID"" = '" & TableName & "' AND ""AliasID"" = '" & UID & "'")
                Else
                    strsql = objaddon.objglobalmethods.getSingleValue("SELECT COUNT(*) FROM CUFD WHERE TableID = '" & TableName & "' AND AliasID = '" & UID & "'")
                End If
                If strsql = 0 Then objaddon.objapplication.SetStatusBarMessage(UID & " UDF not found. Please create in SAP...", SAPbouiCOM.BoMessageTime.bmt_Short, True) : Return False

                MatrixID.Columns.Add(UID, SAPbouiCOM.BoFormItemTypes.it_EDIT)
                MatrixID.Columns.Item(UID).DataBind.SetBound(True, TableName, FieldName)
                MatrixID.Columns.Item(UID).Editable = True
                MatrixID.Columns.Item(UID).TitleObject.Caption = Descr
                Return True
            Catch ex As Exception
            End Try
        End Function

        Private Sub Button5_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button5.ClickAfter
            Try
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strsql = "select T0.""Code"" AS ""EmpId"", T0.""U_ExtEmpNo"" AS ""EmpCode"",case when T0.""U_lastName""<>'' then T0.""U_firstNam"" || ' ' || T0.""U_lastName"" else T0.""U_firstNam"" end AS ""EmpName"","
                strsql += vbCrLf + "Ifnull((Select ""U_EmpLvBal"" from ""@SMPR_HEM2"" where ""Code""=T0.""Code"" and ""U_LveCode""='EL'),0) as ""EarnLeaveBal"","
                strsql += vbCrLf + "Ifnull((Select ""U_EmpLvBal"" from ""@SMPR_HEM2"" where ""Code""=T0.""Code"" and ""U_LveCode""='CO'),0) as ""CompOffBal"","
                strsql += vbCrLf + "Ifnull((Select ""U_EmpLvBal"" from ""@SMPR_HEM2"" where ""Code""=T0.""Code"" and ""U_LveCode""='PH'),0) as ""PHBal"""
                strsql += vbCrLf + "from ""@SMPR_OHEM"" T0 where T0.""U_status""='1' "
                If ComboBox2.Selected.Value <> "-1" Then
                    strsql += vbCrLf + "and T0.""U_branch""='" & ComboBox2.Value & "' "
                End If
                If ComboBox3.Selected.Value <> "-1" Then
                    strsql += vbCrLf + "And T0.""U_location""='" & ComboBox3.Value & "'"
                End If
                objrs.DoQuery(strsql)
                odbdsDetails = objform.DataSources.DBDataSources.Item(CType(1, Object))
                odbdsDetails.Clear()
                Matrix1.LoadFromDataSource()
                If objrs.RecordCount = 0 Then objaddon.objapplication.SetStatusBarMessage("No records Found...", SAPbouiCOM.BoMessageTime.bmt_Short, True) : objform.Freeze(False) : Exit Sub
                odbdsDetails.InsertRecord(odbdsDetails.Size)
                objaddon.objapplication.SetStatusBarMessage("Loading Employee details. Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Short, False)

                For i As Integer = 0 To objrs.RecordCount - 1
                    odbdsDetails.SetValue("LineId", i, i + 1)
                    odbdsDetails.SetValue("U_EmpCode", i, objrs.Fields.Item("EmpCode").Value.ToString)
                    odbdsDetails.SetValue("U_EmpName", i, objrs.Fields.Item("EmpName").Value.ToString)
                    odbdsDetails.SetValue("U_Empid", i, objrs.Fields.Item("EmpId").Value.ToString)
                    'odbdsDetails.SetValue("U_PH", i, objrs.Fields.Item("PHBal").Value.ToString)
                    odbdsDetails.SetValue("U_CO", i, objrs.Fields.Item("CompOffBal").Value.ToString)
                    odbdsDetails.SetValue("U_EL", i, objrs.Fields.Item("EarnLeaveBal").Value.ToString)
                    objrs.MoveNext()
                    If i <> objrs.RecordCount - 1 Then odbdsDetails.InsertRecord(odbdsDetails.Size)
                Next
                Matrix1.LoadFromDataSource()
                objaddon.objapplication.StatusBar.SetText("Employee details Loaded...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Button5_ClickBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button5.ClickBefore
            Try
                If ComboBox2.Value = "" Then
                    objaddon.objapplication.SetStatusBarMessage("Branch is Missing", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If

                If ComboBox3.Value = "" Then
                    objaddon.objapplication.SetStatusBarMessage("Location is Missing", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If

            Catch ex As Exception

            End Try

        End Sub

        Private Sub Matrix1_LinkPressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix1.LinkPressedAfter
            Try
                If pVal.ColUID = "empid" Then
                    If Matrix1.Columns.Item("empid").Cells.Item(pVal.Row).Specific.string = "" Then Exit Sub
                    Link_Value = Matrix1.Columns.Item("empid").Cells.Item(pVal.Row).Specific.string : Link_objtype = "OHEM"
                    Dim activeform As New frmEmployeeMaster
                    activeform.Show()
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Matrix1_ValidateBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Matrix1.ValidateBefore
            Try
                If pVal.InnerEvent = True Then Exit Sub
                If pVal.ColUID = "empid" Then Exit Sub
                'If pVal.ItemChanged = False Then Exit Sub
                If Val(Matrix1.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Specific.string) <> 0 Then
                    strsql = "Select 1 as ""Status"" from ""@SMPR_OHEM"" T0 join ""@SMPR_HEM2"" T1 on T0.""Code""=T1.""Code"""
                    strsql += vbCrLf + "Where T1.""Code""='" & Matrix1.Columns.Item("empid").Cells.Item(pVal.Row).Specific.string & "' and T1.""U_LveName""='" & Matrix1.Columns.Item(pVal.ColUID).TitleObject.Caption & "'"
                    strsql = objaddon.objglobalmethods.getSingleValue(strsql)
                    If strsql = "" Then
                        objaddon.objapplication.StatusBar.SetText("Leave data not found. Please update in Employee Master...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                        'Matrix1.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Click()
                        BubbleEvent = False
                        Exit Sub
                    End If
                    If pVal.ItemChanged = False Then Exit Sub
                    objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    strsql = "Update ""@SMPR_HEM2"" set ""U_EmpLvBal""='" & Matrix1.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Specific.string & "' where ""Code""='" & Matrix1.Columns.Item("empid").Cells.Item(pVal.Row).Specific.string & "' and ""U_LveName""='" & Matrix1.Columns.Item(pVal.ColUID).TitleObject.Caption & "' "
                    objrs.DoQuery(strsql)
                    objaddon.objapplication.StatusBar.SetText(Matrix1.Columns.Item(pVal.ColUID).TitleObject.Caption & " updated in Employee Master...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                    'If objrs.RecordCount > 0 Then
                    '    objaddon.objapplication.StatusBar.SetText(Matrix1.Columns.Item(pVal.ColUID).TitleObject.Caption & " updated in Employee Master...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                    'Else
                    '    objaddon.objapplication.StatusBar.SetText(Matrix1.Columns.Item(pVal.ColUID).TitleObject.Caption & " not updated. Please update in Employee Master...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                    'End If
                End If

            Catch ex As Exception

            End Try

        End Sub

        Private Sub Button3_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button3.ClickAfter
            Try
                If objform.Mode <> SAPbouiCOM.BoFormMode.fm_FIND_MODE Then
                    If pVal.ActionSuccess Then
                        If objaddon.HANA Then
                            RecCount = objaddon.objglobalmethods.getSingleValue("select Count(*) +1 from ""@MIPL_OLM""")
                        Else
                            RecCount = objaddon.objglobalmethods.getSingleValue("select Count(*) +1 from [@MIPL_OLM]")
                        End If
                        If RecCount <> "2" Then
                            objform.Close()
                        End If
                    End If
                End If

            Catch ex As Exception
            End Try

        End Sub

        Private Sub Button3_ClickBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button3.ClickBefore
            Try
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE Then Exit Sub
                If Matrix1.VisualRowCount = 0 Then
                    objaddon.objapplication.SetStatusBarMessage("Line Details is Missing", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                    BubbleEvent = False : objform.Freeze(False) : Exit Sub
                End If
            Catch ex As Exception

            End Try

        End Sub
    End Class
End Namespace

