Option Strict Off
Option Explicit On
Imports SAPbouiCOM.Framework

Namespace HRMS
    <FormAttribute("MSTRLOAN", "Master/frmLoanMaster.b1f")>
    Friend Class frmLoanMaster
        Inherits UserFormBase
        Dim FormCount As Integer = 0
        Private WithEvents objform As SAPbouiCOM.Form

        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.Button2 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.CheckBox0 = CType(Me.GetItem("chkactive").Specific, SAPbouiCOM.CheckBox)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()
            AddHandler DataLoadAfter, AddressOf Me.Data_loadafter
            AddHandler DataLoadAfter, AddressOf Me.Data_loadafter

        End Sub

        Private Sub OnCustomInitialize()
            objform = objaddon.objapplication.Forms.GetForm("MSTRLOAN", Me.FormCount)
            objform.Items.Item("txtCode").Specific.string = objaddon.objglobalmethods.GetNextCode_Value("@SMPR_OLON")
            objform.EnableMenu("1283", False) 'Remove menu
            objform.EnableMenu("1284", False) 'Cancel Menu
            If objaddon.objcompany.UserName.ToString.ToUpper <> "MANAGER" Then objform.EnableMenu("6913", False) 'User Defined Field
            objform = objaddon.objapplication.Forms.ActiveForm
            If Link_Value.ToString <> "-1" And Link_objtype.ToString.ToUpper = "MSTRLOAN" Then
                objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE
                objform.Items.Item("txtCode").Specific.string = Link_Value
                objform.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                Link_Value = "-1" : Link_objtype = "-1"
            End If
            objform.Items.Item("txtname").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
            objform.Items.Item("txtCode").Enabled = False
            objform.Items.Item("txtname").Click(SAPbouiCOM.BoCellClickType.ct_Regular)

            CheckBox0.Item.Width = CheckBox0.Item.Width + 20
            CheckBox0.Item.Height = CheckBox0.Item.Height + 10
        End Sub

        'Private Sub frmLoanMaster_ActivateAfter(pVal As SAPbouiCOM.SBOItemEventArg) Handles Me.ActivateAfter
        '    objform = objaddon.objapplication.Forms.GetForm("MSTRLOAN", Me.FormCount)
        '    If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE And objform.Items.Item("txtCode").Enabled = True Then
        '        objform = objaddon.objapplication.Forms.GetForm("MSTRLOAN", Me.FormCount)
        '        objform.Items.Item("txtname").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
        '        objform.Items.Item("txtCode").Enabled = False
        '        objform.Items.Item("txtname").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
        '    End If
        'End Sub

        Public Sub Data_loadafter(ByRef pVal As SAPbouiCOM.BusinessObjectInfo) Handles Me.DataLoadAfter
            objform = objaddon.objapplication.Forms.GetForm("MSTRLOAN", Me.FormCount)
            If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE And objform.Items.Item("txtCode").Enabled = True Then
                objform = objaddon.objapplication.Forms.GetForm("MSTRLOAN", Me.FormCount)
                objform.Items.Item("txtname").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                objform.Items.Item("txtCode").Enabled = False
                objform.Items.Item("txtname").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
            End If
        End Sub

#Region "Field Details"
        Private WithEvents lblcode As SAPbouiCOM.StaticText
        Private WithEvents txtCode As SAPbouiCOM.EditText
        Private WithEvents lblname As SAPbouiCOM.StaticText
        Private WithEvents txtname As SAPbouiCOM.EditText
        Private WithEvents lblmaxamt As SAPbouiCOM.StaticText
        Private WithEvents txtmaxamt As SAPbouiCOM.EditText
        Private WithEvents lblroi As SAPbouiCOM.StaticText
        Private WithEvents txtroi As SAPbouiCOM.EditText
        Private WithEvents lblminpay As SAPbouiCOM.StaticText
        Private WithEvents txtminpay As SAPbouiCOM.EditText
        Private WithEvents lblmaxinst As SAPbouiCOM.StaticText
        Private WithEvents txtmaxinst As SAPbouiCOM.EditText
        Private WithEvents lblremarks As SAPbouiCOM.StaticText
        Private WithEvents Button0 As SAPbouiCOM.Button
        Private WithEvents Button1 As SAPbouiCOM.Button
        Private WithEvents txtremarks As SAPbouiCOM.EditText
        Private WithEvents Button2 As SAPbouiCOM.Button
        Private WithEvents StaticText0 As SAPbouiCOM.StaticText
        Private WithEvents CheckBox0 As SAPbouiCOM.CheckBox
#End Region

    End Class
End Namespace
