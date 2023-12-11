Option Strict Off
Option Explicit On

Imports SAPbouiCOM.Framework

Namespace HRMS
    <FormAttribute("MSTRSK", "Master/FrmSkillMaster.b1f")>
    Friend Class FrmSkillMaster
        Inherits UserFormBase
        Dim formcount As Integer = 0
        Private WithEvents objform As SAPbouiCOM.Form
        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.StaticText0 = CType(Me.GetItem("Item_0").Specific, SAPbouiCOM.StaticText)
            Me.EditText0 = CType(Me.GetItem("txtcode").Specific, SAPbouiCOM.EditText)
            Me.StaticText1 = CType(Me.GetItem("Item_2").Specific, SAPbouiCOM.StaticText)
            Me.EditText1 = CType(Me.GetItem("txtname").Specific, SAPbouiCOM.EditText)
            Me.Button0 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.Button1 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()
            AddHandler DataLoadAfter, AddressOf Me.Form_DataLoadAfter

        End Sub
        Private WithEvents StaticText0 As SAPbouiCOM.StaticText

        Private Sub OnCustomInitialize()
            objform = objaddon.objapplication.Forms.GetForm("MSTRSK", Me.formcount)
        End Sub
        Private WithEvents EditText0 As SAPbouiCOM.EditText
        Private WithEvents StaticText1 As SAPbouiCOM.StaticText
        Private WithEvents EditText1 As SAPbouiCOM.EditText
        Private WithEvents Button0 As SAPbouiCOM.Button
        Private WithEvents Button1 As SAPbouiCOM.Button



        Private Sub Form_DataLoadAfter(ByRef pVal As SAPbouiCOM.BusinessObjectInfo)
            objform = objaddon.objapplication.Forms.GetForm("MSTRSK", Me.formcount)
            If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE And objform.Items.Item("txtcode").Enabled = True Then
                objform.Items.Item("txtname").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                objform.Items.Item("txtcode").Enabled = False
            End If

        End Sub
    End Class
End Namespace
