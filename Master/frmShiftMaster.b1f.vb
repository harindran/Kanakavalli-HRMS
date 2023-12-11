Option Strict Off
Option Explicit On

Imports SAPbouiCOM.Framework

Namespace HRMS
    <FormAttribute("MSTRSHFT", "Master/frmShiftMaster.b1f")>
    Friend Class frmShiftMaster
        Inherits UserFormBase
        Dim FormCount As Integer = 0
        Private WithEvents objform As SAPbouiCOM.Form
        Private WithEvents oDBDSHeader As SAPbouiCOM.DBDataSource

        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.StaticText0 = CType(Me.GetItem("lblcode").Specific, SAPbouiCOM.StaticText)
            Me.EditText0 = CType(Me.GetItem("txtcode").Specific, SAPbouiCOM.EditText)
            Me.StaticText1 = CType(Me.GetItem("Item_2").Specific, SAPbouiCOM.StaticText)
            Me.StaticText2 = CType(Me.GetItem("lbltype").Specific, SAPbouiCOM.StaticText)
            Me.StaticText3 = CType(Me.GetItem("lblsstime").Specific, SAPbouiCOM.StaticText)
            Me.EditText3 = CType(Me.GetItem("txtsstime").Specific, SAPbouiCOM.EditText)
            Me.StaticText4 = CType(Me.GetItem("lblsetime").Specific, SAPbouiCOM.StaticText)
            Me.EditText4 = CType(Me.GetItem("txtsetime").Specific, SAPbouiCOM.EditText)
            Me.StaticText5 = CType(Me.GetItem("lbllstime").Specific, SAPbouiCOM.StaticText)
            Me.EditText5 = CType(Me.GetItem("txtlstime").Specific, SAPbouiCOM.EditText)
            Me.StaticText6 = CType(Me.GetItem("lblletime").Specific, SAPbouiCOM.StaticText)
            Me.EditText6 = CType(Me.GetItem("txtletime").Specific, SAPbouiCOM.EditText)
            Me.StaticText7 = CType(Me.GetItem("lblstotal").Specific, SAPbouiCOM.StaticText)
            Me.EditText7 = CType(Me.GetItem("txtstotal").Specific, SAPbouiCOM.EditText)
            Me.StaticText8 = CType(Me.GetItem("lblltotal").Specific, SAPbouiCOM.StaticText)
            Me.EditText8 = CType(Me.GetItem("txtltotal").Specific, SAPbouiCOM.EditText)
            Me.StaticText9 = CType(Me.GetItem("lblgrace").Specific, SAPbouiCOM.StaticText)
            Me.EditText9 = CType(Me.GetItem("txtgrace").Specific, SAPbouiCOM.EditText)
            Me.StaticText10 = CType(Me.GetItem("lblremarks").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox0 = CType(Me.GetItem("cmbtype").Specific, SAPbouiCOM.ComboBox)
            Me.EditText11 = CType(Me.GetItem("txtremarks").Specific, SAPbouiCOM.EditText)
            Me.CheckBox0 = CType(Me.GetItem("chklunch").Specific, SAPbouiCOM.CheckBox)
            Me.CheckBox1 = CType(Me.GetItem("chkactive").Specific, SAPbouiCOM.CheckBox)
            Me.Button0 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.Button1 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.EditText12 = CType(Me.GetItem("txtname").Specific, SAPbouiCOM.EditText)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()
        End Sub

        Private Sub OnCustomInitialize()
            objform = objaddon.objapplication.Forms.GetForm("MSTRSHFT", Me.FormCount)
            'Me.oDBDSHeader = objform.DataSources.DBDataSources.Item(CType(0, Object))
            'oDBDSHeader.SetValue("Code", objaddon.objglobalmethods.GetNextCode_Value("[@SMHR_OSFT]"), 0)
            If Link_objtype = "OSFT" And Link_Value <> "" Then
                objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE
                objform.Items.Item("txtcode").Specific.string = Link_Value
                objform.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                Link_Value = "-1" : Link_objtype = "-1"
            Else
                objform.Items.Item("txtcode").Specific.string = objaddon.objglobalmethods.GetNextCode_Value("@SMHR_OSFT")
            End If
            objform.Items.Item("txtname").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
            objform.Items.Item("txtcode").Enabled = False
            objform.EnableMenu("1283", False) 'Remove menu
            objform.EnableMenu("1284", False) 'Cancel Menu
            If objaddon.objcompany.UserName.ToString.ToUpper <> "MANAGER" Then objform.EnableMenu("6913", False) 'User Defined Field
            CheckBox0.Item.Height = CheckBox0.Item.Height + 5
            CheckBox1.Item.Height = CheckBox1.Item.Height + 5
        End Sub

#Region "Field Details"
        Private WithEvents StaticText0 As SAPbouiCOM.StaticText
        Private WithEvents EditText0 As SAPbouiCOM.EditText
        Private WithEvents StaticText1 As SAPbouiCOM.StaticText
        Private WithEvents StaticText2 As SAPbouiCOM.StaticText
        Private WithEvents StaticText3 As SAPbouiCOM.StaticText
        Private WithEvents EditText3 As SAPbouiCOM.EditText
        Private WithEvents StaticText4 As SAPbouiCOM.StaticText
        Private WithEvents EditText4 As SAPbouiCOM.EditText
        Private WithEvents StaticText5 As SAPbouiCOM.StaticText
        Private WithEvents EditText5 As SAPbouiCOM.EditText
        Private WithEvents StaticText6 As SAPbouiCOM.StaticText
        Private WithEvents EditText6 As SAPbouiCOM.EditText
        Private WithEvents StaticText7 As SAPbouiCOM.StaticText
        Private WithEvents EditText7 As SAPbouiCOM.EditText
        Private WithEvents StaticText8 As SAPbouiCOM.StaticText
        Private WithEvents EditText8 As SAPbouiCOM.EditText
        Private WithEvents StaticText9 As SAPbouiCOM.StaticText
        Private WithEvents EditText9 As SAPbouiCOM.EditText
        Private WithEvents StaticText10 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox0 As SAPbouiCOM.ComboBox
        Private WithEvents EditText11 As SAPbouiCOM.EditText
        Private WithEvents CheckBox0 As SAPbouiCOM.CheckBox
        Private WithEvents CheckBox1 As SAPbouiCOM.CheckBox
        Private WithEvents Button0 As SAPbouiCOM.Button
        Private WithEvents Button1 As SAPbouiCOM.Button
        Private WithEvents EditText12 As SAPbouiCOM.EditText

#End Region

        Private Sub frmShiftMaster_DataLoadAfter(ByRef pVal As SAPbouiCOM.BusinessObjectInfo) Handles Me.DataLoadAfter
            objform = objaddon.objapplication.Forms.GetForm("MSTRSHFT", Me.FormCount)
            objform.Items.Item("txtname").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
            objform.Items.Item("txtcode").Enabled = False
        End Sub

        'Lunch Start Time
        Private Sub EditText5_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText5.LostFocusAfter
            Calculation_lunchtime()
        End Sub

        'Lunch End Time
        Private Sub EditText6_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText6.LostFocusAfter
            Calculation_lunchtime()
        End Sub

        Private Sub Calculation_lunchtime()
            If objform.Items.Item("txtlstime").Specific.String = "" Or objform.Items.Item("txtletime").Specific.String = "" Then
                objform.Items.Item("txtltotal").Specific.String = "0.00"
            Else
                Dim timee As String = objaddon.objglobalmethods.GetDuration_BetWeenTime(objform.Items.Item("txtlstime").Specific.String, objform.Items.Item("txtletime").Specific.String)
                objform.Items.Item("txtltotal").Specific.String = objaddon.objglobalmethods.GetDuration_BetWeenTime(objform.Items.Item("txtlstime").Specific.String, objform.Items.Item("txtletime").Specific.String)
            End If
            Calculation_shifttime()
        End Sub

        'Lunch Start Time
        Private Sub EditText5_ValidateBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText5.ValidateBefore
            BubbleEvent = Validation_lunchtime()
        End Sub

        'Lunch End Time
        Private Sub EditText6_ValidateBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText6.ValidateBefore
            BubbleEvent = Validation_lunchtime()
        End Sub

        Private Function Validation_lunchtime()
            If objform.Items.Item("txtlstime").Specific.String = "" Or objform.Items.Item("txtletime").Specific.String = "" Then Return True
            If objaddon.objglobalmethods.Validation_From_To_Time(objform.Items.Item("txtlstime").Specific.String, objform.Items.Item("txtletime").Specific.String) Then Return True
            objaddon.objapplication.SetStatusBarMessage("Enter a Valid Lunch Start and End Time", SAPbouiCOM.BoMessageTime.bmt_Short, True) : Return False
        End Function

        'Shift Start Time
        Private Sub EditText3_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText3.LostFocusAfter
            Calculation_shifttime()
        End Sub

        'Shift End Time
        Private Sub EditText4_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText4.LostFocusAfter
            Calculation_shifttime()
        End Sub

        Private Sub Calculation_shifttime()
            Try
                If objform.Items.Item("txtsstime").Specific.String = "" Or objform.Items.Item("txtsetime").Specific.String = "" Then
                    objform.Items.Item("txtstotal").Specific.String = "0.00"
                Else
                    objform.Items.Item("txtstotal").Specific.String = objaddon.objglobalmethods.GetDuration_BetWeenTime(objform.Items.Item("txtsstime").Specific.String, objform.Items.Item("txtsetime").Specific.String)
                End If

                Dim ochkexclude As SAPbouiCOM.CheckBox
                ochkexclude = objform.Items.Item("chklunch").Specific
                If ochkexclude.Checked = True Then
                    Dim arr() As String = Split(objform.Items.Item("txtstotal").Specific.string, ":")
                    Dim shift_mins As Double = arr(0) * 60 + arr(1) 'Int(objform.Items.Item("txtstotal").Specific.string) * 60 + (objform.Items.Item("txtstotal").Specific.string - Int(objform.Items.Item("txtstotal").Specific.string))
                    arr = Split(objform.Items.Item("txtltotal").Specific.string, ":")
                    shift_mins = shift_mins - (arr(0) * 60 + arr(1))
                    Try
                        If (Int(shift_mins / 60).ToString + ":" + Int(shift_mins - Int(shift_mins / 60) * 60).ToString).ToString <= 0 Then
                            objform.Items.Item("txtstotal").Specific.string = 0
                        Else
                            objform.Items.Item("txtstotal").Specific.string = (Int(shift_mins / 60).ToString + ":" + Int(shift_mins - Int(shift_mins / 60) * 60).ToString).ToString
                        End If
                    Catch ex As Exception
                        objform.Items.Item("txtstotal").Specific.string = (Int(shift_mins / 60).ToString + ":" + Int(shift_mins - Int(shift_mins / 60) * 60).ToString).ToString
                    End Try

                End If
            Catch ex As Exception
                'objaddon.objapplication.SetStatusBarMessage("Error in Shift Hour Calculation", SAPbouiCOM.BoMessageTime.bmt_Short, True)
            End Try
        End Sub

        'Shift Start Time
        Private Sub EditText3_ValidateBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText3.ValidateBefore
            BubbleEvent = Validation_Shifttime()
        End Sub

        'Shift End Time
        Private Sub EditText4_ValidateBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText4.ValidateBefore
            BubbleEvent = Validation_Shifttime()
        End Sub

        Private Function Validation_Shifttime()
            If objform.Items.Item("txtsstime").Specific.String = "" Or objform.Items.Item("txtsetime").Specific.String = "" Then Return True
            If objaddon.objglobalmethods.Validation_From_To_Time(objform.Items.Item("txtsstime").Specific.String, objform.Items.Item("txtsetime").Specific.String) Then Return True
            objaddon.objapplication.SetStatusBarMessage("Enter a Valid Shift Start and End Time", SAPbouiCOM.BoMessageTime.bmt_Short, True) : Return False
        End Function

        'Exculde Lunch Time Check box
        Private Sub CheckBox0_PressedBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles CheckBox0.PressedBefore
            Calculation_shifttime()
        End Sub

        Private Sub Button0_ClickBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles Button0.ClickBefore
            Try

                If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Or objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE Then

                    If objform.Items.Item("txtname").Specific.String = "" Then
                        objaddon.objapplication.SetStatusBarMessage("Shift Name should not be Empty", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                        BubbleEvent = False : Exit Sub
                    End If
                    If Val(objform.Items.Item("txtstotal").Specific.String) <= 0 Then
                        objaddon.objapplication.SetStatusBarMessage("Total Shift Hours should be Greater than Zero", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                        BubbleEvent = False : Exit Sub
                    End If
                    If Left(objform.Items.Item("txtltotal").Specific.String, 2) And Right(objform.Items.Item("txtltotal").Specific.String, 2) <= 0 Then
                        objaddon.objapplication.SetStatusBarMessage("Total Lunch Hours should be Greater than Zero", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                        BubbleEvent = False : Exit Sub
                    End If
                    If Val(objform.Items.Item("txtgrace").Specific.String) < 0 Then
                        objaddon.objapplication.SetStatusBarMessage("Grace Time should be Greater than or equal to Zero", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                        BubbleEvent = False : Exit Sub
                    End If

                    Dim arr() As String
                    arr = Split(objform.Items.Item("txtgrace").Specific.String, ":")
                    If arr.Length <> 2 Then
                        objaddon.objapplication.SetStatusBarMessage("Grace Time Format is not valid", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                        BubbleEvent = False : Exit Sub
                    End If
                    If arr(0) > 24 Then
                        objaddon.objapplication.SetStatusBarMessage("Grace Time Format is not valid", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                        BubbleEvent = False : Exit Sub
                    End If
                    If arr(1) > 59 Then
                        objaddon.objapplication.SetStatusBarMessage("Grace Time Format is not valid", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                        BubbleEvent = False : Exit Sub
                    End If
                End If
            Catch ex As Exception
                objaddon.objapplication.SetStatusBarMessage("Error in Validation", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                BubbleEvent = False
            End Try
        End Sub

    End Class
End Namespace
