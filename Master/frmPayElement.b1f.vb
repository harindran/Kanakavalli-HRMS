Option Strict Off
Option Explicit On
Imports SAPbouiCOM.Framework

Namespace HRMS

    <FormAttribute("MSTRPAYE", "Master/frmPayElement.b1f")>
    Friend Class frmPayElement
        Inherits UserFormBase
        Dim FormCount As Integer = 0
        Private WithEvents objform As SAPbouiCOM.Form

        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.StaticText0 = CType(Me.GetItem("lblcode").Specific, SAPbouiCOM.StaticText)
            Me.EditText0 = CType(Me.GetItem("txtcode").Specific, SAPbouiCOM.EditText)
            Me.StaticText1 = CType(Me.GetItem("lblname").Specific, SAPbouiCOM.StaticText)
            Me.EditText1 = CType(Me.GetItem("txtname").Specific, SAPbouiCOM.EditText)
            Me.StaticText2 = CType(Me.GetItem("ltype").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox0 = CType(Me.GetItem("cType").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText3 = CType(Me.GetItem("lcategory").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox1 = CType(Me.GetItem("cCategory").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText4 = CType(Me.GetItem("lsequence").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox2 = CType(Me.GetItem("csequence").Specific, SAPbouiCOM.ComboBox)
            Me.Button0 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.Button1 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.StaticText5 = CType(Me.GetItem("Item_0").Specific, SAPbouiCOM.StaticText)
            Me.EditText2 = CType(Me.GetItem("Item_1").Specific, SAPbouiCOM.EditText)
            Me.CheckBox0 = CType(Me.GetItem("chkactive").Specific, SAPbouiCOM.CheckBox)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()
            AddHandler DataLoadAfter, AddressOf Me.frmPayElement_DataLoadAfter
            AddHandler DataLoadAfter, AddressOf Me.frmPayElement_DataLoadAfter
            AddHandler DataLoadAfter, AddressOf Me.frmPayElement_DataLoadAfter

        End Sub

        Private Sub OnCustomInitialize()
            objform = objaddon.objapplication.Forms.GetForm("MSTRPAYE", Me.FormCount)
            objform.EnableMenu("1283", False) 'Remove menu
            objform.EnableMenu("1284", False) 'Cancel Menu
            If objaddon.objcompany.UserName.ToString.ToUpper <> "MANAGER" Then objform.EnableMenu("6913", False) 'User Defined Field
            objform = objaddon.objapplication.Forms.ActiveForm
            If Link_objtype.ToString.ToUpper = "MSTRPAYE" And Link_Value.ToString <> "-1" Then
                objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE
                EditText0.Value = Link_Value
                objform.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                Link_Value = "-1" : Link_objtype = "-1"
            End If
            CheckBox0.Item.Width = CheckBox0.Item.Width + 20
            CheckBox0.Item.Height = CheckBox0.Item.Height + 10
            objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cType", True, True, False)
            objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "csequence", True, True, False)
        End Sub

#Region "Fields"

        Private WithEvents StaticText0 As SAPbouiCOM.StaticText
        Private WithEvents EditText0 As SAPbouiCOM.EditText
        Private WithEvents StaticText1 As SAPbouiCOM.StaticText
        Private WithEvents EditText1 As SAPbouiCOM.EditText
        Private WithEvents StaticText2 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox0 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText3 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox1 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText4 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox2 As SAPbouiCOM.ComboBox
        Private WithEvents Button0 As SAPbouiCOM.Button
        Private WithEvents Button1 As SAPbouiCOM.Button
        Private WithEvents StaticText5 As SAPbouiCOM.StaticText
        Private WithEvents EditText2 As SAPbouiCOM.EditText
        Private WithEvents CheckBox0 As SAPbouiCOM.CheckBox
#End Region

        'Private Sub frmPayElement_ActivateAfter(pVal As SAPbouiCOM.SBOItemEventArg) Handles Me.ActivateAfter
        '    objform = objaddon.objapplication.Forms.ActiveForm
        '    If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE And objform.Items.Item("txtCode").Enabled = True Then
        '        objform.Items.Item("txtname").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
        '        objform.Items.Item("txtcode").Enabled = False
        '        If objform.Menu.Exists("1283") = True Then objform.Menu.Item("1283").Enabled = False
        '        If objform.Menu.Exists("1285") = True Then objform.Menu.Item("1285").Enabled = False
        '    End If
        'End Sub

        Public Sub frmPayElement_DataLoadAfter(ByRef pVal As SAPbouiCOM.BusinessObjectInfo) Handles Me.DataLoadAfter
            objform = objaddon.objapplication.Forms.GetForm("MSTRPAYE", Me.FormCount)
            If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then
                objform.Items.Item("txtname").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                objform.Items.Item("txtcode").Enabled = False
                If objform.Menu.Exists("1283") = True Then objform.Menu.Item("1283").Enabled = False
                If objform.Menu.Exists("1285") = True Then objform.Menu.Item("1285").Enabled = False
            End If
        End Sub

        Private Sub ComboBox0_ComboSelectAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox0.ComboSelectAfter
            Try
                If ComboBox0.Selected Is Nothing Then Exit Sub

                If ComboBox2.ValidValues.Count > 0 Then
                    For i As Integer = ComboBox2.ValidValues.Count - 1 To 0 Step -1 : ComboBox2.ValidValues.Remove(i, SAPbouiCOM.BoSearchKey.psk_Index) : Next
                End If
                Dim strsql As String = ""
                Dim objrs As SAPbobsCOM.Recordset
                If ComboBox0.Selected.Value.ToString.ToUpper = "S" Then
                    For i As Integer = 1 To 20
                        ComboBox2.ValidValues.Add("A" + i.ToString, "A" + i.ToString)
                    Next
                    strsql = "select Distinct ""U_Sequence"" from ""@SMPR_OPYE"" Where ""U_Type""='S'"
                ElseIf ComboBox0.Selected.Value.ToString.ToUpper = "A" Then
                    For i As Integer = 1 To 20
                        ComboBox2.ValidValues.Add("AB" + i.ToString, "AB" + i.ToString)
                    Next
                    strsql = "select Distinct ""U_Sequence"" from ""@SMPR_OPYE"" Where ""U_Type""='A'"
                ElseIf ComboBox0.Selected.Value.ToString.ToUpper = "D" Then
                    For i As Integer = 1 To 20
                        ComboBox2.ValidValues.Add("DB" + i.ToString, "DB" + i.ToString)
                    Next
                    strsql = "select Distinct ""U_Sequence"" from ""@SMPR_OPYE"" Where ""U_Type""='D'"
                End If

                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strsql)
                If objrs.RecordCount = 0 Then Exit Sub

                For i As Integer = 0 To objrs.RecordCount - 1
                    ComboBox2.ValidValues.Remove(objrs.Fields.Item(0).Value.ToString, SAPbouiCOM.BoSearchKey.psk_ByValue)
                    objrs.MoveNext()
                Next
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Button0_ClickBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button0.ClickBefore
            Try
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE Then Exit Sub
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                    If ComboBox2.Selected Is Nothing Then
                        objaddon.objapplication.StatusBar.SetText("Please select the Sequence...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
                        BubbleEvent = False : Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try

        End Sub
    End Class
End Namespace
