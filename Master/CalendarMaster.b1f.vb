Option Strict Off
Option Explicit On

Imports SAPbouiCOM.Framework

Namespace HRMS
    <FormAttribute("CALM", "Master/CalendarMaster.b1f")>
    Friend Class CalendarMaster
        Inherits UserFormBase
        Private WithEvents objform As SAPbouiCOM.Form
        Dim FormCount As Integer = 0
        Private WithEvents pCFL As SAPbouiCOM.ISBOChooseFromListEventArg
        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.Button0 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.Button1 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.EditText0 = CType(Me.GetItem("txtentry").Specific, SAPbouiCOM.EditText)
            Me.StaticText5 = CType(Me.GetItem("Item_13").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox0 = CType(Me.GetItem("Item_14").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText9 = CType(Me.GetItem("Item_0").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox1 = CType(Me.GetItem("Item_1").Specific, SAPbouiCOM.ComboBox)
            Me.Matrix0 = CType(Me.GetItem("MtxData").Specific, SAPbouiCOM.Matrix)
            Me.StaticText0 = CType(Me.GetItem("lentry").Specific, SAPbouiCOM.StaticText)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()
            AddHandler DataLoadAfter, AddressOf Me.Form_DataLoadAfter

        End Sub
        Private WithEvents Button0 As SAPbouiCOM.Button
        Public DocEntry As String = ""
        Private Sub OnCustomInitialize()
            objform = objaddon.objapplication.Forms.GetForm("CALM", Me.FormCount)
            DocEntry = objaddon.objglobalmethods.GetNextDocentry_Value("@MIPL_OCAL")
            objaddon.objglobalmethods.Matrix_Addrow(Matrix0, "BCode", "#")
            EditText0.Value = DocEntry
            'objform.ActiveItem = "txtwdays"
            For i As Integer = -1 To 5
                Dim date1 As String = DateTime.Now.AddYears(i).Year.ToString
                ComboBox1.ValidValues.Add(date1, date1)
            Next
            'objform.Items.Item("txtcode").Visible = False
        End Sub
        Private WithEvents Button1 As SAPbouiCOM.Button
        Private WithEvents EditText0 As SAPbouiCOM.EditText
        Private WithEvents EditText2 As SAPbouiCOM.EditText
        Private WithEvents StaticText5 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox0 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText9 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox1 As SAPbouiCOM.ComboBox

        Private Sub ComboBox0_ComboSelectAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox0.ComboSelectAfter

        End Sub

        Private Sub ComboBox1_LostFocusAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox1.LostFocusAfter
            EditText0.Value = objaddon.objglobalmethods.GetNextDocentry_Value("@MIPL_OCAL")
        End Sub

        Private Sub Button0_ClickBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles Button0.ClickBefore
            'If EditText6.Value = "" Then
            '    objaddon.objapplication.SetStatusBarMessage("Please add data... ", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
            '    BubbleEvent = False : objform.Freeze(False) : Exit Sub
            'End If

            'If EditText6.Value > 31 Then
            '    objaddon.objapplication.SetStatusBarMessage("Total Working days should not exceed 31 days... ", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
            '    BubbleEvent = False : objform.Freeze(False) : Exit Sub
            'End If
            Try
                RemoveLastrow(Matrix0, "BCode")
                If Matrix0.VisualRowCount = 0 Then
                    objaddon.objapplication.SetStatusBarMessage("Line Data is missing... ", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                    BubbleEvent = False : objform.Freeze(False) : Exit Sub
                End If
                For i As Integer = 1 To Matrix0.VisualRowCount
                    If Matrix0.Columns.Item("BCode").Cells.Item(i).Specific.string <> "" Then
                        If CDbl(Matrix0.Columns.Item("TWD").Cells.Item(i).Specific.string) = 0 Then
                            objaddon.objapplication.SetStatusBarMessage("Please update the total working days...Line  " & i, SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                            BubbleEvent = False : objform.Freeze(False) : Exit Sub
                        End If
                    End If
                Next
            Catch ex As Exception

            End Try

        End Sub
        Private WithEvents Matrix0 As SAPbouiCOM.Matrix
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

        Private Sub Matrix0_ChooseFromListBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles Matrix0.ChooseFromListBefore
            If pVal.ActionSuccess = True Then Exit Sub
            Try
                Dim oCFL As SAPbouiCOM.ChooseFromList = objform.ChooseFromLists.Item("CFL_B")
                Dim oConds As SAPbouiCOM.Conditions
                Dim oCond As SAPbouiCOM.Condition
                Dim oEmptyConds As New SAPbouiCOM.Conditions
                oCFL.SetConditions(oEmptyConds)
                oConds = oCFL.GetConditions()

                For i As Integer = 1 To Matrix0.VisualRowCount
                    oCond = oConds.Add()
                    oCond.Alias = "BPLId"
                    oCond.Operation = SAPbouiCOM.BoConditionOperation.co_NOT_EQUAL
                    If pVal.Row = i Then
                        oCond.CondVal = ""
                    Else
                        oCond.CondVal = Trim(Matrix0.Columns.Item("BCode").Cells.Item(i).Specific.string)
                    End If
                    If i <> Matrix0.VisualRowCount Then oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND
                Next

                oCFL.SetConditions(oConds)
            Catch ex As Exception
                SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage("Choose FromList Filter Pay Element Failed:" & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
            End Try

        End Sub
        Private WithEvents StaticText0 As SAPbouiCOM.StaticText

        Private Sub Matrix0_ChooseFromListAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.ChooseFromListAfter
            Try
                If pVal.ActionSuccess = False Then Exit Sub
                'Dim ocombo As SAPbouiCOM.ComboBox
                pCFL = pVal
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

            Catch ex As Exception
            Finally
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try

        End Sub

        Private Sub Matrix0_LostFocusAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.LostFocusAfter
            Try
                If pVal.ActionSuccess = False Then Exit Sub
                If pVal.ColUID = "BCode" Then objaddon.objglobalmethods.Matrix_Addrow(Matrix0, "BCode", "#")
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Form_DataLoadAfter(ByRef pVal As SAPbouiCOM.BusinessObjectInfo)
            Try
                'objform.Mode = SAPbouiCOM.BoFormMode.fm_VIEW_MODE
                objaddon.objapplication.Menus.Item("1300").Activate()
            Catch ex As Exception

            End Try

        End Sub
    End Class
End Namespace
