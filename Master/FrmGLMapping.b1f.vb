Option Strict Off
Option Explicit On

Imports SAPbouiCOM.Framework

Namespace HRMS
    <FormAttribute("OGLM", "Master/FrmGLMapping.b1f")>
    Friend Class FrmGLMapping
        Inherits UserFormBase
        Private WithEvents objform As SAPbouiCOM.Form
        Private WithEvents pCFL As SAPbouiCOM.ISBOChooseFromListEventArg
        Dim RecCount As String
        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.Button0 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.Button1 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.StaticText0 = CType(Me.GetItem("lpf").Specific, SAPbouiCOM.StaticText)
            Me.EditText0 = CType(Me.GetItem("tpfc").Specific, SAPbouiCOM.EditText)
            Me.EditText1 = CType(Me.GetItem("tpfn").Specific, SAPbouiCOM.EditText)
            Me.StaticText1 = CType(Me.GetItem("lpt").Specific, SAPbouiCOM.StaticText)
            Me.EditText2 = CType(Me.GetItem("tptc").Specific, SAPbouiCOM.EditText)
            Me.EditText3 = CType(Me.GetItem("tptn").Specific, SAPbouiCOM.EditText)
            Me.StaticText2 = CType(Me.GetItem("lesi").Specific, SAPbouiCOM.StaticText)
            Me.EditText4 = CType(Me.GetItem("tesic").Specific, SAPbouiCOM.EditText)
            Me.EditText5 = CType(Me.GetItem("tesin").Specific, SAPbouiCOM.EditText)
            Me.StaticText3 = CType(Me.GetItem("ltds").Specific, SAPbouiCOM.StaticText)
            Me.EditText6 = CType(Me.GetItem("ttdsc").Specific, SAPbouiCOM.EditText)
            Me.EditText7 = CType(Me.GetItem("ttdsn").Specific, SAPbouiCOM.EditText)
            Me.StaticText4 = CType(Me.GetItem("lloan").Specific, SAPbouiCOM.StaticText)
            Me.EditText8 = CType(Me.GetItem("tloanc").Specific, SAPbouiCOM.EditText)
            Me.EditText9 = CType(Me.GetItem("tloann").Specific, SAPbouiCOM.EditText)
            Me.StaticText5 = CType(Me.GetItem("lsalpay").Specific, SAPbouiCOM.StaticText)
            Me.EditText10 = CType(Me.GetItem("tsalc").Specific, SAPbouiCOM.EditText)
            Me.EditText11 = CType(Me.GetItem("tsaln").Specific, SAPbouiCOM.EditText)
            Me.StaticText6 = CType(Me.GetItem("lbasic").Specific, SAPbouiCOM.StaticText)
            Me.EditText12 = CType(Me.GetItem("tbasicc").Specific, SAPbouiCOM.EditText)
            Me.EditText13 = CType(Me.GetItem("tbasicn").Specific, SAPbouiCOM.EditText)
            Me.StaticText7 = CType(Me.GetItem("lcode").Specific, SAPbouiCOM.StaticText)
            Me.EditText14 = CType(Me.GetItem("txtcode").Specific, SAPbouiCOM.EditText)
            Me.StaticText8 = CType(Me.GetItem("Item_0").Specific, SAPbouiCOM.StaticText)
            Me.EditText15 = CType(Me.GetItem("tbonusc").Specific, SAPbouiCOM.EditText)
            Me.EditText16 = CType(Me.GetItem("tbonusn").Specific, SAPbouiCOM.EditText)
            Me.StaticText9 = CType(Me.GetItem("Item_3").Specific, SAPbouiCOM.StaticText)
            Me.EditText17 = CType(Me.GetItem("Item_4").Specific, SAPbouiCOM.EditText)
            Me.EditText18 = CType(Me.GetItem("Item_5").Specific, SAPbouiCOM.EditText)
            Me.StaticText10 = CType(Me.GetItem("Item_6").Specific, SAPbouiCOM.StaticText)
            Me.EditText19 = CType(Me.GetItem("Item_7").Specific, SAPbouiCOM.EditText)
            Me.EditText20 = CType(Me.GetItem("Item_8").Specific, SAPbouiCOM.EditText)
            Me.StaticText11 = CType(Me.GetItem("Item_9").Specific, SAPbouiCOM.StaticText)
            Me.EditText21 = CType(Me.GetItem("Item_10").Specific, SAPbouiCOM.EditText)
            Me.EditText22 = CType(Me.GetItem("Item_11").Specific, SAPbouiCOM.EditText)
            Me.StaticText12 = CType(Me.GetItem("Item_1").Specific, SAPbouiCOM.StaticText)
            Me.EditText23 = CType(Me.GetItem("Item_2").Specific, SAPbouiCOM.EditText)
            Me.EditText24 = CType(Me.GetItem("Item_12").Specific, SAPbouiCOM.EditText)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()

        End Sub

        Private WithEvents Button0 As SAPbouiCOM.Button

        Private Sub OnCustomInitialize()
            Try
                objform = objaddon.objapplication.Forms.GetForm("OGLM", 0)
                objform = objaddon.objapplication.Forms.ActiveForm
                objform.Freeze(True)
                If objaddon.HANA Then
                    objform.Items.Item("txtcode").Specific.String = objaddon.objglobalmethods.GetNextCode_Value("@MIPL_GL")
                Else
                    objform.Items.Item("txtcode").Specific.String = objaddon.objglobalmethods.GetNextCode_Value("[@MIPL_GL]")
                End If
                If objaddon.HANA Then
                    RecCount = objaddon.objglobalmethods.getSingleValue("select Count(*) from ""@MIPL_GL"";")
                Else
                    RecCount = objaddon.objglobalmethods.getSingleValue("select Count(*) from [@MIPL_GL]")
                End If
                objform.EnableMenu("1282", False)
                If RecCount = "1" Then
                    objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE
                    EditText14.Item.Enabled = True
                    EditText14.Value = "1"
                    'objform.ActiveItem = "txtdoc"
                    objform.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                    EditText14.Item.Enabled = False
                    Exit Sub
                End If
                objform.Freeze(False)
            Catch ex As Exception
            Finally
                objform.Freeze(False)
            End Try
        End Sub

        Private WithEvents Button1 As SAPbouiCOM.Button
        Private WithEvents StaticText0 As SAPbouiCOM.StaticText
        Private WithEvents EditText0 As SAPbouiCOM.EditText
        Private WithEvents EditText1 As SAPbouiCOM.EditText
        Private WithEvents StaticText1 As SAPbouiCOM.StaticText
        Private WithEvents EditText2 As SAPbouiCOM.EditText
        Private WithEvents EditText3 As SAPbouiCOM.EditText
        Private WithEvents StaticText2 As SAPbouiCOM.StaticText
        Private WithEvents EditText4 As SAPbouiCOM.EditText
        Private WithEvents EditText5 As SAPbouiCOM.EditText
        Private WithEvents StaticText3 As SAPbouiCOM.StaticText
        Private WithEvents EditText6 As SAPbouiCOM.EditText
        Private WithEvents EditText7 As SAPbouiCOM.EditText
        Private WithEvents StaticText4 As SAPbouiCOM.StaticText
        Private WithEvents EditText8 As SAPbouiCOM.EditText
        Private WithEvents EditText9 As SAPbouiCOM.EditText
        Private WithEvents StaticText5 As SAPbouiCOM.StaticText
        Private WithEvents EditText10 As SAPbouiCOM.EditText
        Private WithEvents EditText11 As SAPbouiCOM.EditText
        Private WithEvents StaticText6 As SAPbouiCOM.StaticText
        Private WithEvents EditText12 As SAPbouiCOM.EditText
        Private WithEvents EditText13 As SAPbouiCOM.EditText
        Private WithEvents StaticText7 As SAPbouiCOM.StaticText
        Private WithEvents EditText14 As SAPbouiCOM.EditText

        Private Sub Button0_ClickBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button0.ClickBefore
            Try
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE Then Exit Sub
                If EditText0.Value = "" Or EditText2.Value = "" Or EditText4.Value = "" Or EditText6.Value = "" Or EditText8.Value = "" Or EditText10.Value = "" Or EditText12.Value = "" Or EditText15.Value = "" Or EditText17.Value = "" Or EditText19.Value = "" Or EditText21.Value = "" Or EditText23.Value = "" Then
                    objaddon.objapplication.StatusBar.SetText("Please update all the GL Codes...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                    BubbleEvent = False : Exit Sub
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Button0_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button0.ClickAfter
            Try
                If objform.Mode <> SAPbouiCOM.BoFormMode.fm_FIND_MODE Then
                    If pVal.ActionSuccess Then
                        If objaddon.HANA Then
                            RecCount = objaddon.objglobalmethods.getSingleValue("select Count(*) +1 from ""@MIPL_GL"";")
                        Else
                            RecCount = objaddon.objglobalmethods.getSingleValue("select Count(*) +1 from [@MIPL_GL]")
                        End If
                        If RecCount <> "2" Then
                            objform.Close()
                        End If
                    End If
                End If

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

        Private Sub EditText0_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText0.ChooseFromListBefore
            'GL1
            Try
                CFLcondition(pVal, "CFL_0")
            Catch ex As Exception
            End Try

        End Sub

        Private Sub EditText0_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText0.ChooseFromListAfter
            'GL1
            Try
                ChooseFromList_AfterAction_AccountSelection(pVal, EditText0, EditText1)
            Catch ex As Exception
            End Try

        End Sub

        Private Sub EditText2_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText2.ChooseFromListBefore
            'GL2
            Try
                CFLcondition(pVal, "CFL_1")
            Catch ex As Exception
            End Try
        End Sub

        Private Sub EditText2_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText2.ChooseFromListAfter
            'GL2
            Try
                ChooseFromList_AfterAction_AccountSelection(pVal, EditText2, EditText3)
            Catch ex As Exception
            End Try
        End Sub

        Private Sub EditText4_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText4.ChooseFromListBefore
            'GL3
            Try
                CFLcondition(pVal, "CFL_2")
            Catch ex As Exception
            End Try

        End Sub

        Private Sub EditText4_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText4.ChooseFromListAfter
            'GL3
            Try
                ChooseFromList_AfterAction_AccountSelection(pVal, EditText4, EditText5)
            Catch ex As Exception
            End Try
        End Sub

        Private Sub EditText6_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText6.ChooseFromListBefore
            'GL4
            Try
                CFLcondition(pVal, "CFL_3")
            Catch ex As Exception
            End Try

        End Sub

        Private Sub EditText6_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText6.ChooseFromListAfter
            'GL4
            Try
                ChooseFromList_AfterAction_AccountSelection(pVal, EditText6, EditText7)
            Catch ex As Exception
            End Try

        End Sub

        Private Sub EditText8_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText8.ChooseFromListBefore
            'GL5
            Try
                CFLcondition(pVal, "CFL_4")
            Catch ex As Exception
            End Try

        End Sub

        Private Sub EditText8_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText8.ChooseFromListAfter
            'GL5
            Try
                ChooseFromList_AfterAction_AccountSelection(pVal, EditText8, EditText9)
            Catch ex As Exception
            End Try

        End Sub

        Private Sub EditText10_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText10.ChooseFromListBefore
            'GL6
            Try
                CFLcondition(pVal, "CFL_5")
            Catch ex As Exception
            End Try

        End Sub

        Private Sub EditText10_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText10.ChooseFromListAfter
            'GL6
            Try
                ChooseFromList_AfterAction_AccountSelection(pVal, EditText10, EditText11)
            Catch ex As Exception
            End Try
        End Sub

        Private Sub EditText12_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText12.ChooseFromListBefore
            'GL7
            Try
                CFLcondition(pVal, "CFL_6")
            Catch ex As Exception
            End Try

        End Sub

        Private Sub EditText12_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText12.ChooseFromListAfter
            'GL7
            Try
                ChooseFromList_AfterAction_AccountSelection(pVal, EditText12, EditText13)
            Catch ex As Exception
            End Try

        End Sub

        Private WithEvents StaticText8 As SAPbouiCOM.StaticText
        Private WithEvents EditText15 As SAPbouiCOM.EditText
        Private WithEvents EditText16 As SAPbouiCOM.EditText
        Private WithEvents StaticText9 As SAPbouiCOM.StaticText
        Private WithEvents EditText17 As SAPbouiCOM.EditText
        Private WithEvents EditText18 As SAPbouiCOM.EditText
        Private WithEvents StaticText10 As SAPbouiCOM.StaticText
        Private WithEvents EditText19 As SAPbouiCOM.EditText
        Private WithEvents EditText20 As SAPbouiCOM.EditText
        Private WithEvents StaticText11 As SAPbouiCOM.StaticText
        Private WithEvents EditText21 As SAPbouiCOM.EditText
        Private WithEvents EditText22 As SAPbouiCOM.EditText

        Private Sub EditText15_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText15.ChooseFromListBefore
            'GL8
            Try
                CFLcondition(pVal, "CFL_7")
            Catch ex As Exception
            End Try
        End Sub

        Private Sub EditText15_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText15.ChooseFromListAfter
            'GL8
            Try
                ChooseFromList_AfterAction_AccountSelection(pVal, EditText15, EditText16)
            Catch ex As Exception
            End Try
        End Sub

        Private Sub EditText17_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText17.ChooseFromListBefore
            'GL9
            Try
                CFLcondition(pVal, "CFL_8")
            Catch ex As Exception
            End Try

        End Sub

        Private Sub EditText17_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText17.ChooseFromListAfter
            'GL9
            Try
                ChooseFromList_AfterAction_AccountSelection(pVal, EditText17, EditText18)
            Catch ex As Exception
            End Try

        End Sub

        Private Sub EditText19_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText19.ChooseFromListBefore
            'GL10
            Try
                CFLcondition(pVal, "CFL_9")
            Catch ex As Exception
            End Try

        End Sub

        Private Sub EditText19_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText19.ChooseFromListAfter
            'GL10
            Try
                ChooseFromList_AfterAction_AccountSelection(pVal, EditText19, EditText20)
            Catch ex As Exception
            End Try

        End Sub

        Private Sub EditText21_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText21.ChooseFromListBefore
            'GL11
            Try
                CFLcondition(pVal, "CFL_10")
            Catch ex As Exception
            End Try

        End Sub

        Private Sub EditText21_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText21.ChooseFromListAfter
            'GL11
            Try
                ChooseFromList_AfterAction_AccountSelection(pVal, EditText21, EditText22)
            Catch ex As Exception
            End Try

        End Sub

        Private WithEvents StaticText12 As SAPbouiCOM.StaticText
        Private WithEvents EditText23 As SAPbouiCOM.EditText
        Private WithEvents EditText24 As SAPbouiCOM.EditText

        Private Sub EditText23_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText23.ChooseFromListBefore
            'GL12
            Try
                CFLcondition(pVal, "CFL_11")
            Catch ex As Exception
            End Try

        End Sub

        Private Sub EditText23_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText23.ChooseFromListAfter
            'GL12
            Try
                ChooseFromList_AfterAction_AccountSelection(pVal, EditText23, EditText24)
            Catch ex As Exception
            End Try

        End Sub
    End Class
End Namespace
