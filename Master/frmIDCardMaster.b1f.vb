Option Strict Off
Option Explicit On

Imports SAPbouiCOM.Framework

Namespace HRMS
    <FormAttribute("MSTRIDCD", "Master/frmIDCardMaster.b1f")>
    Friend Class frmIDCardMaster
        Inherits UserFormBase
        Dim formcount As Integer = 0
        Private WithEvents objform As SAPbouiCOM.Form

        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.StaticText0 = CType(Me.GetItem("lblcode").Specific, SAPbouiCOM.StaticText)
            Me.EditText0 = CType(Me.GetItem("txtcode").Specific, SAPbouiCOM.EditText)
            Me.StaticText1 = CType(Me.GetItem("txtname").Specific, SAPbouiCOM.StaticText)
            Me.EditText1 = CType(Me.GetItem("Item_3").Specific, SAPbouiCOM.EditText)
            Me.Button0 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.Button1 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()

        End Sub

        Private Sub OnCustomInitialize()
            objform = objaddon.objapplication.Forms.GetForm("MSTRIDCD", Me.formcount)
            objform.EnableMenu("1283", False) 'Remove menu
            objform.EnableMenu("1284", False) 'Cancel Menu
            If objaddon.objcompany.UserName.ToString.ToUpper <> "MANAGER" Then objform.EnableMenu("6913", False) 'User Defined Field
        End Sub

#Region "Fields"
        Private WithEvents StaticText0 As SAPbouiCOM.StaticText
        Private WithEvents EditText0 As SAPbouiCOM.EditText
        Private WithEvents StaticText1 As SAPbouiCOM.StaticText
        Private WithEvents EditText1 As SAPbouiCOM.EditText
        Private WithEvents Button0 As SAPbouiCOM.Button
        Private WithEvents Button1 As SAPbouiCOM.Button
#End Region

        'Private Sub frmIDCardMaster_ActivateAfter(pVal As SAPbouiCOM.SBOItemEventArg) Handles Me.ActivateAfter
        '    objform = objaddon.objapplication.Forms.ActiveForm
        '    If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE And objform.Items.Item("txtcode").Enabled = True Then
        '        objform.Items.Item("txtname").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
        '        objform.Items.Item("txtcode").Enabled = False
        '    End If
        'End Sub

        Private Sub frmIDCardMaster_DataLoadAfter(ByRef pVal As SAPbouiCOM.BusinessObjectInfo) Handles Me.DataLoadAfter
            objform = objaddon.objapplication.Forms.GetForm("MSTRIDCD", Me.formcount)
            If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE And objform.Items.Item("txtcode").Enabled = True Then
                objform.Items.Item("txtname").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                objform.Items.Item("txtcode").Enabled = False
            End If
        End Sub

    End Class
End Namespace
