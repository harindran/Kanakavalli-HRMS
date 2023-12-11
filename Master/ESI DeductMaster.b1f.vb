Option Strict Off
Option Explicit On

Imports SAPbouiCOM.Framework

Namespace HRMS
    <FormAttribute("MIESI", "Master/ESI DeductMaster.b1f")>
    Friend Class ESI_DeductMaster
        Inherits UserFormBase
        Dim formcount As Integer = 0
        Private WithEvents objform As SAPbouiCOM.Form

        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.Button0 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.Button1 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.StaticText0 = CType(Me.GetItem("Item_2").Specific, SAPbouiCOM.StaticText)
            Me.StaticText1 = CType(Me.GetItem("Item_3").Specific, SAPbouiCOM.StaticText)
            Me.EditText0 = CType(Me.GetItem("Item_4").Specific, SAPbouiCOM.EditText)
            Me.StaticText2 = CType(Me.GetItem("Item_5").Specific, SAPbouiCOM.StaticText)
            Me.EditText1 = CType(Me.GetItem("Item_6").Specific, SAPbouiCOM.EditText)
            Me.CheckBox0 = CType(Me.GetItem("Item_7").Specific, SAPbouiCOM.CheckBox)
            Me.StaticText3 = CType(Me.GetItem("Item_8").Specific, SAPbouiCOM.StaticText)
            Me.EditText2 = CType(Me.GetItem("Item_9").Specific, SAPbouiCOM.EditText)
            Me.StaticText4 = CType(Me.GetItem("Item_10").Specific, SAPbouiCOM.StaticText)
            Me.EditText3 = CType(Me.GetItem("txtcode").Specific, SAPbouiCOM.EditText)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()

        End Sub
        Private WithEvents Button0 As SAPbouiCOM.Button

        Private Sub OnCustomInitialize()
            objform = objaddon.objapplication.Forms.GetForm("MIESI", Me.formcount)
        End Sub
        Private WithEvents Button1 As SAPbouiCOM.Button
        Private WithEvents StaticText0 As SAPbouiCOM.StaticText
        Private WithEvents StaticText1 As SAPbouiCOM.StaticText
        Private WithEvents EditText0 As SAPbouiCOM.EditText
        Private WithEvents StaticText2 As SAPbouiCOM.StaticText
        Private WithEvents EditText1 As SAPbouiCOM.EditText
        Private WithEvents CheckBox0 As SAPbouiCOM.CheckBox
        Private WithEvents StaticText3 As SAPbouiCOM.StaticText
        Private WithEvents EditText2 As SAPbouiCOM.EditText
        Private WithEvents StaticText4 As SAPbouiCOM.StaticText
        Private WithEvents EditText3 As SAPbouiCOM.EditText
    End Class
End Namespace
