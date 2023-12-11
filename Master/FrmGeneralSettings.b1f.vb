Option Strict Off
Option Explicit On

Imports SAPbouiCOM.Framework

Namespace HRMS
    <FormAttribute("138", "Master/FrmGeneralSettings.b1f")>
    Friend Class FrmGeneralSettings
        Inherits SystemFormBase
        Private WithEvents objform As SAPbouiCOM.Form
        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.CheckBox0 = CType(Me.GetItem("EnPayroll").Specific, SAPbouiCOM.CheckBox)
            Me.StaticText0 = CType(Me.GetItem("lblPayroll").Specific, SAPbouiCOM.StaticText)
            Me.StaticText1 = CType(Me.GetItem("lblPaySet").Specific, SAPbouiCOM.StaticText)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()

        End Sub

        Private Sub OnCustomInitialize()
            objform = objaddon.objapplication.Forms.GetForm("138", 0)
            HeaderLabel_Color(StaticText1.Item, 10, Color.Red.ToArgb, 13)

        End Sub
        Private WithEvents CheckBox0 As SAPbouiCOM.CheckBox
        Private WithEvents StaticText0 As SAPbouiCOM.StaticText
        Private WithEvents StaticText1 As SAPbouiCOM.StaticText

        Private Sub HeaderLabel_Color(ByVal item As SAPbouiCOM.Item, ByVal fontsize As Integer, ByVal forecolor As Integer, ByVal height As Integer, Optional ByVal width As Integer = 0)
            item.TextStyle = FontStyle.Underline
            item.TextStyle = FontStyle.Bold
            item.FontSize = fontsize
            item.ForeColor = forecolor
            item.Height = height

        End Sub
    End Class
End Namespace
