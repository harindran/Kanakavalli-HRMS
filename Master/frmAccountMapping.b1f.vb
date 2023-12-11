Option Strict Off
Option Explicit On

Imports SAPbouiCOM
Imports SAPbouiCOM.Framework

Namespace HRMS
    <FormAttribute("ACCT", "Master/frmAccountMapping.b1f")>
    Friend Class frmAccountMapping
        Inherits UserFormBase
        Private WithEvents objform As SAPbouiCOM.Form
        Dim formcount As Integer = 0
        Private WithEvents pCFL As SAPbouiCOM.ISBOChooseFromListEventArg
        Private WithEvents objcombo As SAPbouiCOM.ComboBox

        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.Button0 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.Button1 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.StaticText0 = CType(Me.GetItem("lblempty").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox0 = CType(Me.GetItem("cmbtype").Specific, SAPbouiCOM.ComboBox)
            Me.Matrix0 = CType(Me.GetItem("mtloan").Specific, SAPbouiCOM.Matrix)
            Me.Folder0 = CType(Me.GetItem("fldpay").Specific, SAPbouiCOM.Folder)
            Me.Folder1 = CType(Me.GetItem("fldloan").Specific, SAPbouiCOM.Folder)
            Me.Folder2 = CType(Me.GetItem("fldgen").Specific, SAPbouiCOM.Folder)
            Me.Matrix1 = CType(Me.GetItem("mtad").Specific, SAPbouiCOM.Matrix)
            Me.StaticText1 = CType(Me.GetItem("Item_2").Specific, SAPbouiCOM.StaticText)
            Me.EditText0 = CType(Me.GetItem("txtfdate").Specific, SAPbouiCOM.EditText)
            Me.StaticText2 = CType(Me.GetItem("Item_4").Specific, SAPbouiCOM.StaticText)
            Me.EditText1 = CType(Me.GetItem("Item_6").Specific, SAPbouiCOM.EditText)
            Me.EditText2 = CType(Me.GetItem("lsaldc").Specific, SAPbouiCOM.EditText)
            Me.EditText3 = CType(Me.GetItem("lsaldn").Specific, SAPbouiCOM.EditText)
            Me.EditText4 = CType(Me.GetItem("lsalcc").Specific, SAPbouiCOM.EditText)
            Me.EditText5 = CType(Me.GetItem("lsalcn").Specific, SAPbouiCOM.EditText)
            Me.StaticText5 = CType(Me.GetItem("Item_13").Specific, SAPbouiCOM.StaticText)
            Me.EditText6 = CType(Me.GetItem("lencdc").Specific, SAPbouiCOM.EditText)
            Me.EditText7 = CType(Me.GetItem("Item_16").Specific, SAPbouiCOM.EditText)
            Me.EditText8 = CType(Me.GetItem("lenccc").Specific, SAPbouiCOM.EditText)
            Me.EditText9 = CType(Me.GetItem("lenccn").Specific, SAPbouiCOM.EditText)
            Me.StaticText8 = CType(Me.GetItem("Item_20").Specific, SAPbouiCOM.StaticText)
            Me.EditText10 = CType(Me.GetItem("airtdc").Specific, SAPbouiCOM.EditText)
            Me.EditText11 = CType(Me.GetItem("airtdn").Specific, SAPbouiCOM.EditText)
            Me.EditText12 = CType(Me.GetItem("airtcc").Specific, SAPbouiCOM.EditText)
            Me.EditText13 = CType(Me.GetItem("airtcn").Specific, SAPbouiCOM.EditText)
            Me.StaticText11 = CType(Me.GetItem("Item_27").Specific, SAPbouiCOM.StaticText)
            Me.EditText14 = CType(Me.GetItem("advsdc").Specific, SAPbouiCOM.EditText)
            Me.EditText15 = CType(Me.GetItem("advsdn").Specific, SAPbouiCOM.EditText)
            Me.EditText16 = CType(Me.GetItem("advscc").Specific, SAPbouiCOM.EditText)
            Me.EditText17 = CType(Me.GetItem("advscn").Specific, SAPbouiCOM.EditText)
            Me.StaticText14 = CType(Me.GetItem("Item_34").Specific, SAPbouiCOM.StaticText)
            Me.EditText18 = CType(Me.GetItem("gratdc").Specific, SAPbouiCOM.EditText)
            Me.EditText19 = CType(Me.GetItem("gratdn").Specific, SAPbouiCOM.EditText)
            Me.EditText20 = CType(Me.GetItem("gratcc").Specific, SAPbouiCOM.EditText)
            Me.EditText21 = CType(Me.GetItem("gratcn").Specific, SAPbouiCOM.EditText)
            Me.StaticText17 = CType(Me.GetItem("Item_41").Specific, SAPbouiCOM.StaticText)
            Me.StaticText18 = CType(Me.GetItem("Item_42").Specific, SAPbouiCOM.StaticText)
            Me.StaticText19 = CType(Me.GetItem("Item_43").Specific, SAPbouiCOM.StaticText)
            Me.Folder3 = CType(Me.GetItem("fldad").Specific, SAPbouiCOM.Folder)
            Me.Matrix2 = CType(Me.GetItem("mtpay").Specific, SAPbouiCOM.Matrix)
            Me.EditText23 = CType(Me.GetItem("txtcode").Specific, SAPbouiCOM.EditText)
            Me.LinkedButton0 = CType(Me.GetItem("Item_7").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton1 = CType(Me.GetItem("Item_8").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton2 = CType(Me.GetItem("Item_9").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton3 = CType(Me.GetItem("Item_10").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton4 = CType(Me.GetItem("Item_11").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton5 = CType(Me.GetItem("Item_12").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton6 = CType(Me.GetItem("Item_14").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton7 = CType(Me.GetItem("Item_15").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton8 = CType(Me.GetItem("Item_17").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton9 = CType(Me.GetItem("Item_18").Specific, SAPbouiCOM.LinkedButton)
            Me.EditText22 = CType(Me.GetItem("otdc").Specific, SAPbouiCOM.EditText)
            Me.EditText24 = CType(Me.GetItem("otdn").Specific, SAPbouiCOM.EditText)
            Me.EditText25 = CType(Me.GetItem("otcc").Specific, SAPbouiCOM.EditText)
            Me.EditText26 = CType(Me.GetItem("otcn").Specific, SAPbouiCOM.EditText)
            Me.StaticText3 = CType(Me.GetItem("Item_21").Specific, SAPbouiCOM.StaticText)
            Me.EditText27 = CType(Me.GetItem("tadc").Specific, SAPbouiCOM.EditText)
            Me.EditText28 = CType(Me.GetItem("tadn").Specific, SAPbouiCOM.EditText)
            Me.EditText29 = CType(Me.GetItem("tacc").Specific, SAPbouiCOM.EditText)
            Me.EditText30 = CType(Me.GetItem("tacn").Specific, SAPbouiCOM.EditText)
            Me.StaticText4 = CType(Me.GetItem("Item_26").Specific, SAPbouiCOM.StaticText)
            Me.LinkedButton10 = CType(Me.GetItem("Item_28").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton11 = CType(Me.GetItem("Item_29").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton12 = CType(Me.GetItem("Item_30").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton13 = CType(Me.GetItem("Item_31").Specific, SAPbouiCOM.LinkedButton)
            Me.Folder4 = CType(Me.GetItem("fldProv").Specific, SAPbouiCOM.Folder)
            Me.EditText43 = CType(Me.GetItem("lprdc").Specific, SAPbouiCOM.EditText)
            Me.EditText44 = CType(Me.GetItem("lprdn").Specific, SAPbouiCOM.EditText)
            Me.EditText45 = CType(Me.GetItem("lprcc").Specific, SAPbouiCOM.EditText)
            Me.EditText46 = CType(Me.GetItem("lprcn").Specific, SAPbouiCOM.EditText)
            Me.StaticText13 = CType(Me.GetItem("Item_56").Specific, SAPbouiCOM.StaticText)
            Me.EditText47 = CType(Me.GetItem("aprdc").Specific, SAPbouiCOM.EditText)
            Me.EditText48 = CType(Me.GetItem("aprdn").Specific, SAPbouiCOM.EditText)
            Me.EditText49 = CType(Me.GetItem("aprcc").Specific, SAPbouiCOM.EditText)
            Me.EditText50 = CType(Me.GetItem("aprcn").Specific, SAPbouiCOM.EditText)
            Me.StaticText15 = CType(Me.GetItem("Item_61").Specific, SAPbouiCOM.StaticText)
            Me.EditText51 = CType(Me.GetItem("gprdc").Specific, SAPbouiCOM.EditText)
            Me.EditText52 = CType(Me.GetItem("gprdn").Specific, SAPbouiCOM.EditText)
            Me.EditText53 = CType(Me.GetItem("gprcc").Specific, SAPbouiCOM.EditText)
            Me.EditText54 = CType(Me.GetItem("gprcn").Specific, SAPbouiCOM.EditText)
            Me.StaticText16 = CType(Me.GetItem("Item_66").Specific, SAPbouiCOM.StaticText)
            Me.StaticText20 = CType(Me.GetItem("Item_67").Specific, SAPbouiCOM.StaticText)
            Me.StaticText21 = CType(Me.GetItem("Item_68").Specific, SAPbouiCOM.StaticText)
            Me.LinkedButton20 = CType(Me.GetItem("Item_69").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton21 = CType(Me.GetItem("Item_70").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton22 = CType(Me.GetItem("Item_71").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton23 = CType(Me.GetItem("Item_72").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton24 = CType(Me.GetItem("Item_73").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton25 = CType(Me.GetItem("Item_74").Specific, SAPbouiCOM.LinkedButton)
            Me.StaticText6 = CType(Me.GetItem("Item_0").Specific, SAPbouiCOM.StaticText)
            Me.EditText31 = CType(Me.GetItem("Item_1").Specific, SAPbouiCOM.EditText)
            Me.Folder5 = CType(Me.GetItem("Settings").Specific, SAPbouiCOM.Folder)
            Me.StaticText9 = CType(Me.GetItem("Item_19").Specific, SAPbouiCOM.StaticText)
            Me.EditText33 = CType(Me.GetItem("Item_22").Specific, SAPbouiCOM.EditText)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()

        End Sub

        Private Sub OnCustomInitialize()
            objform = objaddon.objapplication.Forms.GetForm("ACCT", formcount)
            objform = objaddon.objapplication.Forms.ActiveForm
            Try
                Loadcombo()
                If GetPayrollEnabledIndia = True Then
                    Folder1.Item.Visible = False
                    Folder2.Item.Visible = False
                    Folder4.Item.Visible = False
                Else
                    Folder5.Item.Visible = False
                End If
                Folder0.Item.Click()
                EditText23.Value = objaddon.objglobalmethods.GetNextCode_Value("@SMPR_ACCT")
                objform.ActiveItem = "cmbtype"
                objform.EnableMenu("1283", False) 'Remove Menu
                objform.Settings.Enabled = True
                EditText3.Item.FontSize = 9
                'EditText3.Item.TextStyle = FontStyle.Bold
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Loadcombo()
            Try
                Dim cmbloan As SAPbouiCOM.Column = Matrix0.Columns.Item("loancode")
                Dim cmbad As SAPbouiCOM.Column = Matrix1.Columns.Item("andncode")
                Dim cmbpay As SAPbouiCOM.Column = Matrix2.Columns.Item("paycode")
                Dim objrs As SAPbobsCOM.Recordset
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery("CALL ""MIPL_HRMS_EMPMASTER_COMBO_FILLING"" ('ACCT')")
                If objrs.RecordCount = 0 Then Exit Sub
                For i As Integer = 0 To objrs.RecordCount - 1
                    Try
                        Select Case objrs.Fields.Item("Type").Value.ToString.ToUpper
                            Case "EMPTYPE" : ComboBox0.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "LOAN" : cmbloan.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "SETTYPE" : cmbad.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "PAY" : cmbpay.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                        End Select
                        objrs.MoveNext()
                    Catch ex As Exception
                        objrs.MoveNext()
                    End Try
                Next
            Catch ex As Exception

            End Try
        End Sub

#Region "Field Details"
        Private WithEvents Button0 As SAPbouiCOM.Button
        Private WithEvents Button1 As SAPbouiCOM.Button
        Private WithEvents StaticText0 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox0 As SAPbouiCOM.ComboBox
        Private WithEvents Matrix0 As SAPbouiCOM.Matrix
        Private WithEvents Folder0 As SAPbouiCOM.Folder
        Private WithEvents Folder1 As SAPbouiCOM.Folder
        Private WithEvents Folder2 As SAPbouiCOM.Folder
        Private WithEvents Matrix1 As SAPbouiCOM.Matrix
        Private WithEvents StaticText1 As SAPbouiCOM.StaticText
        Private WithEvents EditText0 As SAPbouiCOM.EditText
        Private WithEvents StaticText2 As SAPbouiCOM.StaticText
        Private WithEvents EditText1 As SAPbouiCOM.EditText
        Private WithEvents EditText2 As SAPbouiCOM.EditText
        Private WithEvents EditText3 As SAPbouiCOM.EditText
        Private WithEvents EditText4 As SAPbouiCOM.EditText
        Private WithEvents EditText5 As SAPbouiCOM.EditText
        Private WithEvents StaticText5 As SAPbouiCOM.StaticText
        Private WithEvents EditText6 As SAPbouiCOM.EditText
        Private WithEvents EditText7 As SAPbouiCOM.EditText
        Private WithEvents EditText8 As SAPbouiCOM.EditText
        Private WithEvents EditText9 As SAPbouiCOM.EditText
        Private WithEvents StaticText8 As SAPbouiCOM.StaticText
        Private WithEvents EditText10 As SAPbouiCOM.EditText
        Private WithEvents EditText11 As SAPbouiCOM.EditText
        Private WithEvents EditText12 As SAPbouiCOM.EditText
        Private WithEvents EditText13 As SAPbouiCOM.EditText
        Private WithEvents StaticText11 As SAPbouiCOM.StaticText
        Private WithEvents EditText14 As SAPbouiCOM.EditText
        Private WithEvents EditText15 As SAPbouiCOM.EditText
        Private WithEvents EditText16 As SAPbouiCOM.EditText
        Private WithEvents EditText17 As SAPbouiCOM.EditText
        Private WithEvents StaticText14 As SAPbouiCOM.StaticText
        Private WithEvents EditText18 As SAPbouiCOM.EditText
        Private WithEvents EditText19 As SAPbouiCOM.EditText
        Private WithEvents EditText20 As SAPbouiCOM.EditText
        Private WithEvents EditText21 As SAPbouiCOM.EditText
        Private WithEvents StaticText17 As SAPbouiCOM.StaticText
        Private WithEvents StaticText18 As SAPbouiCOM.StaticText
        Private WithEvents StaticText19 As SAPbouiCOM.StaticText
        Private WithEvents Folder3 As SAPbouiCOM.Folder
        Private WithEvents Matrix2 As SAPbouiCOM.Matrix
        Private WithEvents EditText23 As SAPbouiCOM.EditText
        Private WithEvents LinkedButton0 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton1 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton2 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton3 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton4 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton5 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton6 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton7 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton8 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton9 As SAPbouiCOM.LinkedButton

        Private WithEvents EditText22 As SAPbouiCOM.EditText
        Private WithEvents EditText24 As SAPbouiCOM.EditText
        Private WithEvents EditText25 As SAPbouiCOM.EditText
        Private WithEvents EditText26 As SAPbouiCOM.EditText
        Private WithEvents StaticText3 As SAPbouiCOM.StaticText
        Private WithEvents EditText27 As SAPbouiCOM.EditText
        Private WithEvents EditText28 As SAPbouiCOM.EditText
        Private WithEvents EditText29 As SAPbouiCOM.EditText
        Private WithEvents EditText30 As SAPbouiCOM.EditText
        Private WithEvents StaticText4 As SAPbouiCOM.StaticText
        Private WithEvents LinkedButton10 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton11 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton12 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton13 As SAPbouiCOM.LinkedButton

        Private WithEvents Folder4 As SAPbouiCOM.Folder
        Private WithEvents EditText43 As SAPbouiCOM.EditText
        Private WithEvents EditText44 As SAPbouiCOM.EditText
        Private WithEvents EditText45 As SAPbouiCOM.EditText
        Private WithEvents EditText46 As SAPbouiCOM.EditText
        Private WithEvents StaticText13 As SAPbouiCOM.StaticText
        Private WithEvents EditText47 As SAPbouiCOM.EditText
        Private WithEvents EditText48 As SAPbouiCOM.EditText
        Private WithEvents EditText49 As SAPbouiCOM.EditText
        Private WithEvents EditText50 As SAPbouiCOM.EditText
        Private WithEvents StaticText15 As SAPbouiCOM.StaticText
        Private WithEvents EditText51 As SAPbouiCOM.EditText
        Private WithEvents EditText52 As SAPbouiCOM.EditText
        Private WithEvents EditText53 As SAPbouiCOM.EditText
        Private WithEvents EditText54 As SAPbouiCOM.EditText
        Private WithEvents StaticText16 As SAPbouiCOM.StaticText
        Private WithEvents StaticText20 As SAPbouiCOM.StaticText
        Private WithEvents StaticText21 As SAPbouiCOM.StaticText
        Private WithEvents LinkedButton20 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton21 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton22 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton23 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton24 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton25 As SAPbouiCOM.LinkedButton
        Private WithEvents StaticText6 As SAPbouiCOM.StaticText
        Private WithEvents EditText31 As SAPbouiCOM.EditText
#End Region

#Region "Folder Pressed After"

        Private Sub Folder1_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Folder1.PressedAfter
            Try
                objaddon.objapplication.Menus.Item("1300").Activate()
                If Matrix0.RowCount = 0 Then Matrix0.AddRow(1) : Matrix0.Columns.Item("#").Cells.Item(1).Specific.string = 1
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Folder2_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Folder2.PressedAfter
            Try
                objaddon.objapplication.Menus.Item("1300").Activate()
                If Matrix1.RowCount = 0 Then Matrix1.AddRow(1) : Matrix1.Columns.Item("#").Cells.Item(1).Specific.string = 1
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Folder3_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Folder3.PressedAfter
            Try
                objaddon.objapplication.Menus.Item("1300").Activate()
                If Matrix2.RowCount = 0 Then Matrix2.AddRow(1) : Matrix2.Columns.Item("#").Cells.Item(1).Specific.string = 1
                If Matrix1.RowCount = 0 Then Matrix1.AddRow(1) : Matrix1.Columns.Item("#").Cells.Item(1).Specific.string = 1
            Catch ex As Exception

            End Try
        End Sub
#End Region

#Region "Choose From List Events"

        Private Sub ChooseFromList_AfterAction_AccountSelection(ByVal pVal As SAPbouiCOM.SBOItemEventArg, ByVal editext_acctcode As SAPbouiCOM.EditText, ByVal editext_acctname As SAPbouiCOM.EditText)
            Try
                If pVal.ActionSuccess = False Then Exit Sub
                pCFL = pVal
                If Not pCFL.SelectedObjects Is Nothing Then
                    Try
                        editext_acctcode.Value = pCFL.SelectedObjects.Columns.Item("AcctCode").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                    Try
                        editext_acctname.Value = pCFL.SelectedObjects.Columns.Item("AcctName").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                End If
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE
            Catch ex As Exception
            Finally
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try
        End Sub

        Private Sub ChooseFromList_AfterAction_AccountSelection_Matrix(ByVal pVal As SAPbouiCOM.SBOItemEventArg, ByVal MatrixName As SAPbouiCOM.Matrix, ByVal colname_acctcode As String, ByVal colname_acctname As String)
            Try
                If pVal.ActionSuccess = False Then Exit Sub
                pCFL = pVal
                If Not pCFL.SelectedObjects Is Nothing Then
                    Try
                        MatrixName.Columns.Item(colname_acctcode).Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("AcctCode").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                    Try
                        MatrixName.Columns.Item(colname_acctname).Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("AcctName").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                End If
            Catch ex As Exception
            Finally
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try
        End Sub

        Private Sub EditText2_ChooseFromListAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText2.ChooseFromListAfter
            ChooseFromList_AfterAction_AccountSelection(pVal, EditText2, EditText3)
        End Sub

        Private Sub EditText4_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText4.ChooseFromListAfter
            ChooseFromList_AfterAction_AccountSelection(pVal, EditText4, EditText5)
        End Sub

        Private Sub EditText6_ChooseFromListAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText6.ChooseFromListAfter
            ChooseFromList_AfterAction_AccountSelection(pVal, EditText6, EditText7)
        End Sub

        Private Sub EditText8_ChooseFromListAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText8.ChooseFromListAfter
            ChooseFromList_AfterAction_AccountSelection(pVal, EditText8, EditText9)
        End Sub

        Private Sub EditText10_ChooseFromListAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText10.ChooseFromListAfter
            ChooseFromList_AfterAction_AccountSelection(pVal, EditText10, EditText11)
        End Sub

        Private Sub EditText12_ChooseFromListAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText12.ChooseFromListAfter
            ChooseFromList_AfterAction_AccountSelection(pVal, EditText12, EditText13)
        End Sub

        Private Sub EditText14_ChooseFromListAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText14.ChooseFromListAfter
            ChooseFromList_AfterAction_AccountSelection(pVal, EditText14, EditText15)
        End Sub

        Private Sub EditText16_ChooseFromListAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText16.ChooseFromListAfter
            ChooseFromList_AfterAction_AccountSelection(pVal, EditText16, EditText17)
        End Sub

        Private Sub EditText18_ChooseFromListAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText18.ChooseFromListAfter
            ChooseFromList_AfterAction_AccountSelection(pVal, EditText18, EditText19)
        End Sub

        Private Sub EditText20_ChooseFromListAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText20.ChooseFromListAfter
            ChooseFromList_AfterAction_AccountSelection(pVal, EditText20, EditText21)
        End Sub

        Private Sub EditText22_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText22.ChooseFromListAfter
            ChooseFromList_AfterAction_AccountSelection(pVal, EditText22, EditText24)
        End Sub

        Private Sub EditText25_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText25.ChooseFromListAfter
            ChooseFromList_AfterAction_AccountSelection(pVal, EditText25, EditText26)
        End Sub

        Private Sub EditText27_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText27.ChooseFromListAfter
            ChooseFromList_AfterAction_AccountSelection(pVal, EditText27, EditText28)
        End Sub

        Private Sub EditText29_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText29.ChooseFromListAfter
            ChooseFromList_AfterAction_AccountSelection(pVal, EditText29, EditText30)
        End Sub

        Private Sub EditText43_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText43.ChooseFromListAfter
            ChooseFromList_AfterAction_AccountSelection(pVal, EditText43, EditText44)
        End Sub

        Private Sub EditText45_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText45.ChooseFromListAfter
            ChooseFromList_AfterAction_AccountSelection(pVal, EditText45, EditText46)
        End Sub

        Private Sub EditText47_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText47.ChooseFromListAfter
            ChooseFromList_AfterAction_AccountSelection(pVal, EditText47, EditText48)
        End Sub

        Private Sub EditText49_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText49.ChooseFromListAfter
            ChooseFromList_AfterAction_AccountSelection(pVal, EditText49, EditText50)
        End Sub

        Private Sub EditText51_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText51.ChooseFromListAfter
            ChooseFromList_AfterAction_AccountSelection(pVal, EditText51, EditText52)
        End Sub

        Private Sub EditText53_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText53.ChooseFromListAfter
            ChooseFromList_AfterAction_AccountSelection(pVal, EditText53, EditText54)
        End Sub

        Private Sub Matrix0_ChooseFromListAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.ChooseFromListAfter
            If pVal.ColUID = "loandc" Then ChooseFromList_AfterAction_AccountSelection_Matrix(pVal, Matrix0, "loandc", "loandn")
            If pVal.ColUID = "loancc" Then ChooseFromList_AfterAction_AccountSelection_Matrix(pVal, Matrix0, "loancc", "loancn")
            objaddon.objglobalmethods.Matrix_Addrow(Matrix0, "loandc", "#")
        End Sub

        Private Sub Matrix1_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix1.ChooseFromListAfter
            If pVal.ColUID = "adddeddc" Then ChooseFromList_AfterAction_AccountSelection_Matrix(pVal, Matrix1, "adddeddc", "adddeddn")
            If pVal.ColUID = "adddedcc" Then ChooseFromList_AfterAction_AccountSelection_Matrix(pVal, Matrix1, "adddedcc", "adddedcn")
            objaddon.objglobalmethods.Matrix_Addrow(Matrix1, "adddeddc", "#")
        End Sub

        Private Sub Matrix2_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix2.ChooseFromListAfter
            If pVal.ColUID = "payeledc" Then ChooseFromList_AfterAction_AccountSelection_Matrix(pVal, Matrix2, "payeledc", "payeledn")
            If pVal.ColUID = "payelecc" Then ChooseFromList_AfterAction_AccountSelection_Matrix(pVal, Matrix2, "payelecc", "payelecn")
            objaddon.objglobalmethods.Matrix_Addrow(Matrix2, "payeledc", "#")
        End Sub

#End Region

#Region "Combo Select After Events"

        Private Sub Matrix0_ComboSelectAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.ComboSelectAfter
            Try
                objcombo = Matrix0.Columns.Item("loancode").Cells.Item(Matrix0.VisualRowCount).Specific
                If Not objcombo.Selected Is Nothing Then
                    'Matrix0.AddRow(1) : Matrix0.Columns.Item("#").Cells.Item(Matrix0.VisualRowCount).Specific.string = Matrix0.VisualRowCount
                    objaddon.objglobalmethods.Matrix_Addrow(Matrix0, "loandc", "#")
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Matrix1_ComboSelectAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix1.ComboSelectAfter
            Try
                objcombo = Matrix1.Columns.Item("andncode").Cells.Item(Matrix1.VisualRowCount).Specific
                If Not objcombo.Selected Is Nothing Then
                    'Matrix1.AddRow(1) : Matrix1.Columns.Item("#").Cells.Item(Matrix1.VisualRowCount).Specific.String = Matrix1.VisualRowCount
                    objaddon.objglobalmethods.Matrix_Addrow(Matrix1, "adddeddc", "#")
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Matrix2_ComboSelectAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix2.ComboSelectAfter
            Try
                objcombo = Matrix2.Columns.Item("paycode").Cells.Item(Matrix2.VisualRowCount).Specific
                If Not objcombo.Selected Is Nothing Then
                    'Matrix2.AddRow(1) : Matrix2.Columns.Item("#").Cells.Item(Matrix2.VisualRowCount).Specific.string = Matrix2.VisualRowCount
                    objaddon.objglobalmethods.Matrix_Addrow(Matrix2, "payeledc", "#")
                End If
            Catch ex As Exception

            End Try
        End Sub

#End Region

        Private Sub Button0_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button0.PressedAfter
            If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                EditText23.Value = objaddon.objglobalmethods.GetNextCode_Value("@SMPR_ACCT")
                objform.ActiveItem = "cmbtype"
            End If
        End Sub

        Private Sub EditText1_LostFocusAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText1.LostFocusAfter
            Try
                objaddon.objapplication.Menus.Item("1300").Activate()
                If Matrix2.RowCount = 0 Then Matrix2.AddRow(1) : Matrix2.Columns.Item("#").Cells.Item(1).Specific.string = 1
            Catch ex As Exception

            End Try
        End Sub

        Private Sub CFLcondition(ByVal pVal As SAPbouiCOM.SBOItemEventArg, ByVal CFLID As String)
            If pVal.ActionSuccess = True Then Exit Sub
            Try
                Dim oCFL As SAPbouiCOM.ChooseFromList = objform.ChooseFromLists.Item(CFLID)
                Dim oConds As SAPbouiCOM.Conditions
                Dim oCond As SAPbouiCOM.Condition
                Dim oEmptyConds As New SAPbouiCOM.Conditions
                oCFL.SetConditions(oEmptyConds)
                oConds = oCFL.GetConditions()

                oCond = oConds.Add()
                oCond.Alias = "Postable"
                oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                oCond.CondVal = "Y"
                oCFL.SetConditions(oConds)
            Catch ex As Exception
                SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage("Choose FromList Filter Pay Element Failed:" & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
            End Try
        End Sub
        Private Sub Matrix1_ChooseFromListBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles Matrix1.ChooseFromListBefore
            CFLcondition(pVal, "CFL_adddeddc")
            CFLcondition(pVal, "CFL_adddedcc")
        End Sub

        Private Sub Matrix2_ChooseFromListBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles Matrix2.ChooseFromListBefore
            CFLcondition(pVal, "CFL_payeledc")
            CFLcondition(pVal, "CFL_payelecc")
        End Sub
        Private WithEvents StaticText7 As SAPbouiCOM.StaticText
        Private WithEvents EditText32 As SAPbouiCOM.EditText

        Private Sub Matrix0_ChooseFromListBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles Matrix0.ChooseFromListBefore
            CFLcondition(pVal, "CFL_loandc")
            CFLcondition(pVal, "CFL_loancc")
        End Sub

        Private Sub EditText22_ChooseFromListBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles EditText22.ChooseFromListBefore
            CFLcondition(pVal, "CFL_otdc")
        End Sub

        Private Sub EditText25_ChooseFromListBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles EditText25.ChooseFromListBefore
            CFLcondition(pVal, "CFL_otcc")
        End Sub

        Private Sub EditText27_ChooseFromListBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles EditText27.ChooseFromListBefore
            CFLcondition(pVal, "CFL_tadc")
        End Sub

        Private Sub EditText29_ChooseFromListBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles EditText29.ChooseFromListBefore
            CFLcondition(pVal, "CFL_tacc")
        End Sub

        Private WithEvents Folder5 As SAPbouiCOM.Folder
        Private WithEvents StaticText9 As SAPbouiCOM.StaticText
        Private WithEvents EditText33 As SAPbouiCOM.EditText

        Private Sub Matrix2_ValidateAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix2.ValidateAfter
            Try
                If Matrix2.Columns.Item("payeledc").Cells.Item(pVal.Row).Specific.string = "" Then Matrix2.Columns.Item("payeledn").Cells.Item(pVal.Row).Specific.string = ""
                If Matrix2.Columns.Item("payelecc").Cells.Item(pVal.Row).Specific.string = "" Then Matrix2.Columns.Item("payelecn").Cells.Item(pVal.Row).Specific.string = ""
            Catch ex As Exception
            End Try

        End Sub

        Private Sub Matrix0_ValidateAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.ValidateAfter
            Try
                If Matrix0.Columns.Item("loandc").Cells.Item(pVal.Row).Specific.string = "" Then Matrix0.Columns.Item("loandn").Cells.Item(pVal.Row).Specific.string = ""
                If Matrix0.Columns.Item("loancc").Cells.Item(pVal.Row).Specific.string = "" Then Matrix0.Columns.Item("loancn").Cells.Item(pVal.Row).Specific.string = ""
            Catch ex As Exception
            End Try

        End Sub

        Private Sub Matrix1_ValidateAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix1.ValidateAfter
            Try
                If Matrix1.Columns.Item("adddeddc").Cells.Item(pVal.Row).Specific.string = "" Then Matrix1.Columns.Item("adddeddn").Cells.Item(pVal.Row).Specific.string = ""
                If Matrix1.Columns.Item("adddedcc").Cells.Item(pVal.Row).Specific.string = "" Then Matrix1.Columns.Item("adddedcn").Cells.Item(pVal.Row).Specific.string = ""
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
        Private Sub Button0_ClickBefore(sboObject As Object, pVal As SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button0.ClickBefore
            Try
                RemoveLastrow(Matrix2, "payeledc")
                RemoveLastrow(Matrix1, "adddeddc")
                RemoveLastrow(Matrix0, "loandc")
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Folder0_PressedAfter(sboObject As Object, pVal As SBOItemEventArg) Handles Folder0.PressedAfter
            Try
                Matrix2.AutoResizeColumns()
            Catch ex As Exception

            End Try

        End Sub
    End Class
End Namespace
