Option Strict Off
Option Explicit On

Imports SAPbouiCOM.Framework

Namespace HRMS
    <FormAttribute("MIPT", "Master/ProfTax DeductMaster.b1f")>
    Friend Class ProfTax_DeductMaster

        Inherits UserFormBase
        Dim formcount As Integer = 0
        Private WithEvents objform As SAPbouiCOM.Form
        Private WithEvents pCFL As SAPbouiCOM.ISBOChooseFromListEventArg
        Dim objChk As SAPbouiCOM.CheckBox
        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.Button0 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.Button1 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.CheckBox0 = CType(Me.GetItem("chkact").Specific, SAPbouiCOM.CheckBox)
            Me.EditText3 = CType(Me.GetItem("txtcode").Specific, SAPbouiCOM.EditText)
            Me.Matrix0 = CType(Me.GetItem("mtxdata").Specific, SAPbouiCOM.Matrix)
            Me.StaticText7 = CType(Me.GetItem("lblcode").Specific, SAPbouiCOM.StaticText)
            Me.StaticText2 = CType(Me.GetItem("lfrmdate").Specific, SAPbouiCOM.StaticText)
            Me.EditText2 = CType(Me.GetItem("tfrmdate").Specific, SAPbouiCOM.EditText)
            Me.StaticText3 = CType(Me.GetItem("ltodate").Specific, SAPbouiCOM.StaticText)
            Me.EditText4 = CType(Me.GetItem("ttodate").Specific, SAPbouiCOM.EditText)
            Me.Folder0 = CType(Me.GetItem("fldproftax").Specific, SAPbouiCOM.Folder)
            Me.Folder1 = CType(Me.GetItem("fldpfesi").Specific, SAPbouiCOM.Folder)
            Me.Matrix1 = CType(Me.GetItem("mtxpfesi").Specific, SAPbouiCOM.Matrix)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()
            AddHandler DataLoadAfter, AddressOf Me.Form_DataLoadAfter

        End Sub

#Region "Fields"
        Private WithEvents Button0 As SAPbouiCOM.Button
        Private WithEvents Button1 As SAPbouiCOM.Button
        Private WithEvents CheckBox0 As SAPbouiCOM.CheckBox
        Private WithEvents EditText3 As SAPbouiCOM.EditText
        Private WithEvents Matrix0 As SAPbouiCOM.Matrix
        Private WithEvents StaticText7 As SAPbouiCOM.StaticText
        Private WithEvents StaticText2 As SAPbouiCOM.StaticText
        Private WithEvents EditText2 As SAPbouiCOM.EditText
        Private WithEvents StaticText3 As SAPbouiCOM.StaticText
        Private WithEvents EditText4 As SAPbouiCOM.EditText
        Private WithEvents Folder0 As SAPbouiCOM.Folder
        Private WithEvents Folder1 As SAPbouiCOM.Folder
        Private WithEvents Matrix1 As SAPbouiCOM.Matrix

#End Region
        Dim RecCount As String
        Private Sub OnCustomInitialize()
            Try
                objform = objaddon.objapplication.Forms.GetForm("MIPT", Me.formcount)
                objform = objaddon.objapplication.Forms.ActiveForm
                EditText3.Value = objaddon.objglobalmethods.GetNextDocentry_Value("@MIPL_PTM")
                objaddon.objglobalmethods.Matrix_Addrow(Matrix0, "BCode", "#")
                objaddon.objglobalmethods.Matrix_Addrow(Matrix1, "BCode", "#")
                CheckBox0.Item.Height = CheckBox0.Item.Height + 2
                Folder0.Item.Click()
                Field_Disable()
                objaddon.objapplication.Menus.Item("1300").Activate() 'Fit colum width
                'LoadComboDetails()
                'objform.ActiveItem = "txtsal"

                'RecCount = objaddon.objglobalmethods.getSingleValue("select Count(*) from ""@MIPL_PTAX"";")
                'If RecCount = "1" Then
                '    objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE
                '    EditText3.Item.Enabled = True
                '    EditText3.Value = "1"
                '    'objform.ActiveItem = "txtdoc"
                '    objform.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                '    'EditText3.Item.Enabled = False
                '    Exit Sub
                'End If
                'objform.Items.Item("txtcode").Visible = False
            Catch ex As Exception

            End Try

        End Sub

        Private Sub LoadComboDetails()
            Try
                Dim objrs As SAPbobsCOM.Recordset
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery("CALL ""MIPL_HRMS_EMPMASTER_COMBO_FILLING"" ('OHEM')")
                If objrs.RecordCount = 0 Then Exit Sub
                For i As Integer = 0 To objrs.RecordCount - 1
                    Try
                        Select Case objrs.Fields.Item("Type").Value.ToString.ToUpper
                            'Case "LOCATION" : ComboBox1.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                        End Select
                        objrs.MoveNext()
                    Catch ex As Exception
                        objrs.MoveNext()
                    End Try
                Next
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Form_DataLoadAfter(ByRef pVal As SAPbouiCOM.BusinessObjectInfo)
            Try
                EditText2.Item.Enabled = False
                Field_Disable()
                'EditText4.Item.Enabled = False 
                Matrix0.AutoResizeColumns()
            Catch ex As Exception

            End Try

        End Sub

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
        Private Sub Button0_ClickBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles Button0.ClickBefore
            Try
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE Then Exit Sub
                'If ComboBox1.Value.ToString = "" Then objaddon.objapplication.SetStatusBarMessage("Location is Missing, Please Update", SAPbouiCOM.BoMessageTime.bmt_Short, True) : BubbleEvent = False
                RemoveLastrow(Matrix0, "BCode")
                RemoveLastrow(Matrix1, "BCode")
                If EditText2.Value = "" Then 'FrmDate
                    objaddon.objapplication.SetStatusBarMessage("From Date is missing Please update...", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If
                If EditText4.Value = "" Then 'ToDate
                    objaddon.objapplication.SetStatusBarMessage("To Date is missing Please update...", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                    BubbleEvent = False : Exit Sub
                End If
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                    Dim FDate As Date = Date.ParseExact(EditText2.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                    Dim TDate As Date = Date.ParseExact(EditText4.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                    Dim Status As String = objaddon.objglobalmethods.getSingleValue("Select distinct 1 as ""Status"" from ""@MIPL_PTM"" where '" & FDate.ToString("yyyyMMdd") & "' between ""U_FromDate"" and ""U_ToDate"" or '" & TDate.ToString("yyyyMMdd") & "'  between ""U_FromDate"" and ""U_ToDate"" ")
                    If Status = "1" Then
                        objaddon.objapplication.SetStatusBarMessage("Entry already posted for this month Please check...", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                        BubbleEvent = False : Exit Sub
                    End If
                End If
                'End If
            Catch ex As Exception

            End Try

        End Sub



        Private Sub Matrix0_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Matrix0.ChooseFromListBefore
            If pVal.ActionSuccess = True Then Exit Sub
            Try
                If pVal.ColUID = "BCode" Then
                    Dim oCFL As SAPbouiCOM.ChooseFromList = objform.ChooseFromLists.Item("CFL_B")
                    Dim oConds As SAPbouiCOM.Conditions
                    Dim oCond As SAPbouiCOM.Condition
                    Dim oEmptyConds As New SAPbouiCOM.Conditions
                    oCFL.SetConditions(oEmptyConds)
                    oConds = oCFL.GetConditions()
                    'For i As Integer = 1 To Matrix0.VisualRowCount
                    '    oCond = oConds.Add()
                    '    oCond.Alias = "BPLId"
                    '    oCond.Operation = SAPbouiCOM.BoConditionOperation.co_NOT_EQUAL
                    '    If pVal.Row = i Then
                    '        oCond.CondVal = ""
                    '    Else
                    '        oCond.CondVal = Trim(Matrix0.Columns.Item("BCode").Cells.Item(i).Specific.string)
                    '    End If
                    '    If i <> Matrix0.VisualRowCount Then oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND
                    'Next
                    'oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND
                    oCond = oConds.Add()
                    oCond.Alias = "Disabled"
                    oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                    oCond.CondVal = "N"
                    oCFL.SetConditions(oConds)
                ElseIf pVal.ColUID = "LCode" Then
                    Dim oCFL As SAPbouiCOM.ChooseFromList = objform.ChooseFromLists.Item("CFL_L")
                    Dim oConds As SAPbouiCOM.Conditions
                    Dim oCond As SAPbouiCOM.Condition
                    Dim oEmptyConds As New SAPbouiCOM.Conditions
                    oCFL.SetConditions(oEmptyConds)
                    oConds = oCFL.GetConditions()
                    Dim objrs As SAPbobsCOM.Recordset
                    objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    If MultiBranch = "Y" Then
                        If Trim(Matrix0.Columns.Item("BCode").Cells.Item(pVal.Row).Specific.string) <> "" Then
                            objrs.DoQuery("Select T0.""BPLId"",T1.""Location"" from OBPL T0 join OWHS T1 on T0.""DflWhs""=T1.""WhsCode"" join OLCT T2 on T1.""Location""=T2.""Code"" where ifnull(T0.""Disabled"",'')='N' and T0.""BPLId""='" & Trim(Matrix0.Columns.Item("BCode").Cells.Item(pVal.Row).Specific.string) & "'")
                            If objrs.RecordCount > 0 Then
                                For i As Integer = 0 To objrs.RecordCount - 1
                                    If i = 0 Then
                                        oCond = oConds.Add()
                                        oCond.Alias = "Code"
                                        oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                                        oCond.CondVal = objrs.Fields.Item(1).Value.ToString
                                    Else
                                        oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND
                                        oCond = oConds.Add()
                                        oCond.Alias = "Code"
                                        oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                                        oCond.CondVal = objrs.Fields.Item(1).Value.ToString
                                    End If
                                Next
                            End If
                        Else
                            oCond = oConds.Add()
                            oCond.Alias = "Code"
                            oCond.Operation = SAPbouiCOM.BoConditionOperation.co_NONE
                            oCond.CondVal = ""
                        End If
                    Else

                    End If

                    oCFL.SetConditions(oConds)
                End If

            Catch ex As Exception
                SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage("Choose FromList Filter Pay Element Failed:" & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
            End Try


        End Sub

        Private Sub Matrix0_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.ChooseFromListAfter
            Try
                If pVal.ActionSuccess = False Then Exit Sub
                pCFL = pVal
                If pVal.ColUID = "BCode" And pVal.ActionSuccess = True Then
                    If Not pCFL.SelectedObjects Is Nothing Then
                        Try
                            Matrix0.Columns.Item("BCode").Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("BPLId").Cells.Item(0).Value
                        Catch ex As Exception
                        End Try
                        Try
                            Matrix0.Columns.Item("BName").Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("BPLName").Cells.Item(0).Value
                        Catch ex As Exception
                        End Try
                        objaddon.objglobalmethods.Matrix_Addrow(Matrix0, "BCode", "#")
                    End If
                ElseIf pVal.ColUID = "LCode" And pVal.ActionSuccess = True Then
                    If Not pCFL.SelectedObjects Is Nothing Then
                        Try
                            Matrix0.Columns.Item("LCode").Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("Code").Cells.Item(0).Value
                        Catch ex As Exception
                        End Try
                        Try
                            Matrix0.Columns.Item("LName").Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("Location").Cells.Item(0).Value
                        Catch ex As Exception
                        End Try
                        objaddon.objglobalmethods.Matrix_Addrow(Matrix0, "LCode", "#")
                    End If
                End If
            Catch ex As Exception
            Finally
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try

        End Sub

        Private Sub Button0_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button0.ClickAfter
            'Try
            '    If objform.Mode <> SAPbouiCOM.BoFormMode.fm_FIND_MODE Then
            '        If pVal.ActionSuccess Then
            '            If objaddon.HANA Then
            '                RecCount = objaddon.objglobalmethods.getSingleValue("select Count(*) +1 from ""@MIPL_PTAX""")
            '            Else
            '                RecCount = objaddon.objglobalmethods.getSingleValue("select Count(*) +1 from [@MIPL_PTAX]")
            '            End If
            '            If RecCount <> "2" Then
            '                objform.Close()
            '            End If
            '        End If
            '    End If

            'Catch ex As Exception
            'End Try

        End Sub

        Private Sub Button0_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button0.PressedAfter
            Try
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE Then Exit Sub
                If pVal.ActionSuccess = True And objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                    objform.Items.Item("txtcode").Specific.string = objaddon.objglobalmethods.GetNextDocentry_Value("@MIPL_PTM")
                    objaddon.objglobalmethods.Matrix_Addrow(Matrix0, "BCode")
                End If

            Catch ex As Exception

            End Try

        End Sub

        Private Sub Matrix0_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.LostFocusAfter
            Try
                If pVal.ColUID = "BCode" And pVal.ActionSuccess = True Then
                    objaddon.objglobalmethods.Matrix_Addrow(Matrix0, "BCode", "#")
                ElseIf pVal.ColUID = "LCode" And pVal.ActionSuccess = True Then
                    objaddon.objglobalmethods.Matrix_Addrow(Matrix0, "LCode", "#")
                End If
                Matrix1.AutoResizeColumns()
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Matrix0_ValidateBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Matrix0.ValidateBefore
            Try
                Dim flag As Boolean = False
                Dim Row As Integer = pVal.Row
                Dim FAmount, EFAmount, TAmount, ETAmount As Double
                'If MultiBranch = "Y" Then
                Select Case pVal.ColUID
                        Case "LCode", "Frmsal", "Tosal"
                            FAmount = CDbl(Matrix0.Columns.Item("Frmsal").Cells.Item(pVal.Row).Specific.string)
                            TAmount = CDbl(Matrix0.Columns.Item("Tosal").Cells.Item(pVal.Row).Specific.string)
                            If Matrix0.VisualRowCount > 1 Then
                            For i As Integer = Matrix0.VisualRowCount To 1 Step -1
                                If MultiBranch = "Y" Then
                                    If Matrix0.Columns.Item("BCode").Cells.Item(pVal.Row).Specific.string <> "" And Matrix0.Columns.Item("LCode").Cells.Item(pVal.Row).Specific.string <> "" Then
                                        EFAmount = CDbl(Matrix0.Columns.Item("Frmsal").Cells.Item(i).Specific.string)  'Existing From Amount
                                        ETAmount = CDbl(Matrix0.Columns.Item("Tosal").Cells.Item(i).Specific.string) 'Existing To Amount
                                        If Row <> i And ((FAmount >= EFAmount And FAmount <= ETAmount) Or (TAmount >= EFAmount And TAmount <= ETAmount)) And Matrix0.Columns.Item("BCode").Cells.Item(pVal.Row).Specific.string = Matrix0.Columns.Item("BCode").Cells.Item(i).Specific.string And Matrix0.Columns.Item("LCode").Cells.Item(pVal.Row).Specific.string = Matrix0.Columns.Item("LCode").Cells.Item(i).Specific.string Then
                                            flag = True
                                            objaddon.objapplication.StatusBar.SetSystemMessage("Duplicate data found on line " & i & " & " & Row & " Please check.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                                            Exit For
                                        End If
                                    End If
                                Else
                                    If Matrix0.Columns.Item("LCode").Cells.Item(pVal.Row).Specific.string <> "" Then
                                        EFAmount = CDbl(Matrix0.Columns.Item("Frmsal").Cells.Item(i).Specific.string)  'Existing From Amount
                                        ETAmount = CDbl(Matrix0.Columns.Item("Tosal").Cells.Item(i).Specific.string) 'Existing To Amount
                                        If Row <> i And ((FAmount >= EFAmount And FAmount <= ETAmount) Or (TAmount >= EFAmount And TAmount <= ETAmount)) And Matrix0.Columns.Item("LCode").Cells.Item(pVal.Row).Specific.string = Matrix0.Columns.Item("LCode").Cells.Item(i).Specific.string Then
                                            flag = True
                                            objaddon.objapplication.StatusBar.SetSystemMessage("Duplicate data found on line " & i & " & " & Row & " Please check.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next
                        End If
                            If flag = True Then
                                BubbleEvent = False : Exit Sub
                            End If
                    End Select
                'End If


            Catch ex As Exception

            End Try

        End Sub

        Private Sub Matrix1_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix1.LostFocusAfter
            Try
                If pVal.ColUID = "BCode" And pVal.ActionSuccess = True Then
                    objaddon.objglobalmethods.Matrix_Addrow(Matrix1, "BCode", "#")
                ElseIf pVal.ColUID = "LCode" And pVal.ActionSuccess = True Then
                    objaddon.objglobalmethods.Matrix_Addrow(Matrix1, "LCode", "#")
                End If
                Matrix1.AutoResizeColumns()
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Matrix1_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Matrix1.ChooseFromListBefore
            If pVal.ActionSuccess = True Then Exit Sub
            Try
                If pVal.ColUID = "BCode" Then
                    Dim oCFL As SAPbouiCOM.ChooseFromList = objform.ChooseFromLists.Item("CFL_B1")
                    Dim oConds As SAPbouiCOM.Conditions
                    Dim oCond As SAPbouiCOM.Condition
                    Dim oEmptyConds As New SAPbouiCOM.Conditions
                    oCFL.SetConditions(oEmptyConds)
                    oConds = oCFL.GetConditions()
                    oCond = oConds.Add()
                    oCond.Alias = "Disabled"
                    oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                    oCond.CondVal = "N"
                    oCFL.SetConditions(oConds)
                ElseIf pVal.ColUID = "LCode" Then
                    Dim oCFL As SAPbouiCOM.ChooseFromList = objform.ChooseFromLists.Item("CFL_L1")
                    Dim oConds As SAPbouiCOM.Conditions
                    Dim oCond As SAPbouiCOM.Condition
                    Dim oEmptyConds As New SAPbouiCOM.Conditions
                    oCFL.SetConditions(oEmptyConds)
                    oConds = oCFL.GetConditions()
                    Dim objrs As SAPbobsCOM.Recordset
                    objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    If MultiBranch = "Y" Then
                        If Trim(Matrix1.Columns.Item("BCode").Cells.Item(pVal.Row).Specific.string) <> "" Then
                            objrs.DoQuery("Select T0.""BPLId"",T1.""Location"" from OBPL T0 join OWHS T1 on T0.""DflWhs""=T1.""WhsCode"" join OLCT T2 on T1.""Location""=T2.""Code"" where ifnull(T0.""Disabled"",'')='N' and T0.""BPLId""='" & Trim(Matrix1.Columns.Item("BCode").Cells.Item(pVal.Row).Specific.string) & "'")
                            If objrs.RecordCount > 0 Then
                                For i As Integer = 0 To objrs.RecordCount - 1
                                    If i = 0 Then
                                        oCond = oConds.Add()
                                        oCond.Alias = "Code"
                                        oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                                        oCond.CondVal = objrs.Fields.Item(1).Value.ToString
                                    Else
                                        oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND
                                        oCond = oConds.Add()
                                        oCond.Alias = "Code"
                                        oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                                        oCond.CondVal = objrs.Fields.Item(1).Value.ToString
                                    End If
                                Next
                            End If
                        Else
                            oCond = oConds.Add()
                            oCond.Alias = "Code"
                            oCond.Operation = SAPbouiCOM.BoConditionOperation.co_NONE
                            oCond.CondVal = ""
                        End If
                    End If
                    oCFL.SetConditions(oConds)
                End If

            Catch ex As Exception
                SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage("Choose FromList Filter Pay Element Failed:" & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
            End Try

        End Sub

        Private Sub Matrix1_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix1.ChooseFromListAfter
            Try
                If pVal.ActionSuccess = False Then Exit Sub
                pCFL = pVal
                If pVal.ColUID = "BCode" And pVal.ActionSuccess = True Then
                    If Not pCFL.SelectedObjects Is Nothing Then
                        Try
                            Matrix1.Columns.Item("BCode").Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("BPLId").Cells.Item(0).Value
                        Catch ex As Exception
                        End Try
                        Try
                            Matrix1.Columns.Item("BName").Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("BPLName").Cells.Item(0).Value
                        Catch ex As Exception
                        End Try
                        objaddon.objglobalmethods.Matrix_Addrow(Matrix1, "BCode", "#")
                    End If
                ElseIf pVal.ColUID = "LCode" And pVal.ActionSuccess = True Then
                    If Not pCFL.SelectedObjects Is Nothing Then
                        Try
                            Matrix1.Columns.Item("LCode").Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("Code").Cells.Item(0).Value
                        Catch ex As Exception
                        End Try
                        Try
                            Matrix1.Columns.Item("LName").Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("Location").Cells.Item(0).Value
                        Catch ex As Exception
                        End Try
                        objaddon.objglobalmethods.Matrix_Addrow(Matrix1, "LCode", "#")
                    End If
                End If
            Catch ex As Exception
            Finally
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try

        End Sub

        Private Sub Matrix1_ValidateBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Matrix1.ValidateBefore
            Try
                Dim flag As Boolean = False
                Dim Row As Integer = pVal.Row
                Dim FAmount, EFAmount, TAmount, ETAmount As Double
                'If MultiBranch = "Y" Then
                Select Case pVal.ColUID
                        Case "LCode", "Frmsal", "Tosal"
                            FAmount = CDbl(Matrix1.Columns.Item("Frmsal").Cells.Item(pVal.Row).Specific.string)
                            TAmount = CDbl(Matrix1.Columns.Item("Tosal").Cells.Item(pVal.Row).Specific.string)
                            If Matrix1.VisualRowCount > 1 Then
                            For i As Integer = Matrix1.VisualRowCount To 1 Step -1
                                If MultiBranch = "Y" Then
                                    If Matrix1.Columns.Item("BCode").Cells.Item(pVal.Row).Specific.string <> "" And Matrix1.Columns.Item("LCode").Cells.Item(pVal.Row).Specific.string <> "" Then
                                        EFAmount = CDbl(Matrix1.Columns.Item("Frmsal").Cells.Item(i).Specific.string)  'Existing From Amount
                                        ETAmount = CDbl(Matrix1.Columns.Item("Tosal").Cells.Item(i).Specific.string) 'Existing To Amount
                                        If Row <> i And ((FAmount >= EFAmount And FAmount <= ETAmount) Or (TAmount >= EFAmount And TAmount <= ETAmount)) And Matrix1.Columns.Item("BCode").Cells.Item(pVal.Row).Specific.string = Matrix1.Columns.Item("BCode").Cells.Item(i).Specific.string And Matrix1.Columns.Item("LCode").Cells.Item(pVal.Row).Specific.string = Matrix1.Columns.Item("LCode").Cells.Item(i).Specific.string Then
                                            flag = True
                                            objaddon.objapplication.StatusBar.SetSystemMessage("Duplicate data found on line " & i & " Please check.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                                            Exit For
                                        End If
                                    End If
                                Else
                                    If Matrix1.Columns.Item("LCode").Cells.Item(pVal.Row).Specific.string <> "" Then
                                        EFAmount = CDbl(Matrix1.Columns.Item("Frmsal").Cells.Item(i).Specific.string)  'Existing From Amount
                                        ETAmount = CDbl(Matrix1.Columns.Item("Tosal").Cells.Item(i).Specific.string) 'Existing To Amount
                                        If Row <> i And ((FAmount >= EFAmount And FAmount <= ETAmount) Or (TAmount >= EFAmount And TAmount <= ETAmount)) And Matrix1.Columns.Item("LCode").Cells.Item(pVal.Row).Specific.string = Matrix1.Columns.Item("LCode").Cells.Item(i).Specific.string Then
                                            flag = True
                                            objaddon.objapplication.StatusBar.SetSystemMessage("Duplicate data found on line " & i & " Please check.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next
                        End If
                            If flag = True Then
                                BubbleEvent = False : Exit Sub
                            End If
                    End Select
                'End If


            Catch ex As Exception

            End Try


        End Sub

        Private Sub Folder0_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Folder0.PressedAfter
            Try
                objaddon.objglobalmethods.Matrix_Addrow(Matrix0, "BCode", "#")
            Catch ex As Exception
            End Try

        End Sub

        Private Sub Folder1_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Folder1.PressedAfter
            Try
                objaddon.objglobalmethods.Matrix_Addrow(Matrix1, "BCode", "#")
            Catch ex As Exception
            End Try

        End Sub

        Private Sub Matrix1_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix1.PressedAfter
            Try
                If pVal.ColUID = "chkpf" Then
                    objChk = Matrix1.Columns.Item("chkpf").Cells.Item(pVal.Row).Specific
                    If objChk.Checked = True Then
                        Matrix1.CommonSetting.SetCellEditable(pVal.Row, 8, False)
                        Matrix1.CommonSetting.SetCellEditable(pVal.Row, 9, False)
                        Matrix1.CommonSetting.SetCellEditable(pVal.Row, 10, True)
                        Matrix1.CommonSetting.SetCellEditable(pVal.Row, 11, True)
                    Else
                        Matrix1.CommonSetting.SetCellEditable(pVal.Row, 10, False)
                        Matrix1.CommonSetting.SetCellEditable(pVal.Row, 11, False)
                        Matrix1.CommonSetting.SetCellEditable(pVal.Row, 8, True)
                        Matrix1.CommonSetting.SetCellEditable(pVal.Row, 9, True)
                    End If

                End If
            Catch ex As Exception
            End Try

        End Sub

        Private Sub Field_Disable()
            Try
                For i As Integer = 1 To Matrix1.VisualRowCount
                    objChk = Matrix1.Columns.Item("chkpf").Cells.Item(i).Specific
                    If Val(Matrix1.Columns.Item("emppf").Cells.Item(i).Specific.String) > 0 Or Val(Matrix1.Columns.Item("eepf").Cells.Item(i).Specific.String) > 0 Then
                        'objChk.Checked = False
                        Matrix1.CommonSetting.SetCellEditable(i, 10, False)
                        Matrix1.CommonSetting.SetCellEditable(i, 11, False)
                        Matrix1.CommonSetting.SetCellEditable(i, 8, True)
                        Matrix1.CommonSetting.SetCellEditable(i, 9, True)
                    ElseIf Val(Matrix1.Columns.Item("emppflc").Cells.Item(i).Specific.String) > 0 Or Val(Matrix1.Columns.Item("eepflc").Cells.Item(i).Specific.String) > 0 Then
                        'objChk.Checked = True
                        Matrix1.CommonSetting.SetCellEditable(i, 8, False)
                        Matrix1.CommonSetting.SetCellEditable(i, 9, False)
                        Matrix1.CommonSetting.SetCellEditable(i, 10, True)
                        Matrix1.CommonSetting.SetCellEditable(i, 11, True)
                    End If
                Next
            Catch ex As Exception

            End Try
        End Sub
    End Class
End Namespace
