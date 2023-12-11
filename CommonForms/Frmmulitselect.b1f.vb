Option Strict Off
Option Explicit On

Imports SAPbouiCOM.Framework

Namespace HRMS
    <FormAttribute("MUSE", "CommonForms/Frmmulitselect.b1f")>
    Friend Class Frmmulitselect
        Inherits UserFormBase
        Dim FormCount As Integer = 0
        Private WithEvents objform As SAPbouiCOM.Form

        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.Button0 = CType(Me.GetItem("btnadd").Specific, SAPbouiCOM.Button)
            Me.Button1 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.Grid1 = CType(Me.GetItem("grd").Specific, SAPbouiCOM.Grid)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()

        End Sub

        Private Sub OnCustomInitialize()
            objform = objaddon.objapplication.Forms.GetForm("MUSE", FormCount)
            'objform = objaddon.objapplication.Forms.ActiveForm

            Try
                If Query_multiselect.ToString = "" Or multi_objtype.ToString = "-1" Then objform.Close()
                If multi_objtype.ToString.ToUpper = "OLCT" Then
                    objform.Title = "Select Location"
                End If

                LoadGrid(Query_multiselect)

                objform.Update()
                objform.Refresh()
            Catch ex As Exception

            End Try
        End Sub

        Private WithEvents Button0 As SAPbouiCOM.Button
        Private WithEvents Button1 As SAPbouiCOM.Button
        Private WithEvents Grid1 As SAPbouiCOM.Grid

        Private Sub LoadGrid(ByVal query As String)
            Try
                Grid1.DataTable.ExecuteQuery(query)

                Grid1.Columns.Item("Select").Type = SAPbouiCOM.BoGridColumnType.gct_CheckBox
                Grid1.Columns.Item("Code").Editable = False
                Grid1.Columns.Item("Location").Editable = False
                'Grid1.Columns.Item("#").Editable = False
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Button0_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button0.ClickAfter
            Dim code As String = "#"
            Dim name As String = ""
            For i As Integer = 0 To Grid1.Rows.Count - 1
                If Grid1.DataTable.GetValue("Select", i).ToString = "Y" Then
                    code = code + (Grid1.DataTable.GetValue(1, i).ToString + "#")
                    name = name + Grid1.DataTable.GetValue(2, i).ToString + ","
                End If
            Next
            If multi_objtype.ToString.ToUpper = "OLCT" Then
                frmmultiselectform.Items.Item("txtlocc").Specific.string = code
                frmmultiselectform.Items.Item("txtlocn").Specific.string = name
            End If
            frmmultiselectform = Nothing
            objform.Close()
        End Sub

        Private Sub Grid1_DoubleClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Grid1.DoubleClickAfter
            Try
                If pVal.Row <> -1 Then Exit Sub
                Dim input As String
                If Grid1.DataTable.GetValue("Select", 0).ToString = "Y" Then
                    input = "N"
                Else
                    input = "Y"
                End If
                objform.Freeze(True)
                For i As Integer = 0 To Grid1.Rows.Count - 1
                    Grid1.DataTable.SetValue("Select", i, input)
                Next
                objform.Freeze(False)
            Catch ex As Exception
                objform.Freeze(False)
            End Try

        End Sub
    End Class
End Namespace
