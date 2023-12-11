Option Strict Off
Option Explicit On

Imports System.IO
Imports System.Net.Mail
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports SAPbouiCOM.Framework

Namespace HRMS
    <FormAttribute("OPRC", "Transcation/frmPayrollProcess.b1f")>
    Friend Class frmPayrollProcess
        Inherits UserFormBase
        Public WithEvents objform As SAPbouiCOM.Form
        Dim FormCount As Integer = 0
        Dim strsql As String
        Dim objrs As SAPbobsCOM.Recordset
        Private WithEvents odbdsDetails As SAPbouiCOM.DBDataSource
        Public WithEvents cmbdesignation, cmbdepartment As SAPbouiCOM.Column, cmbpaymode As SAPbouiCOM.Column
        Dim addupdate As Boolean = False
        Dim lastrowid As Integer = 1

        Public Sub New()
            Try

            Catch ex As Exception

            End Try
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.Button0 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.Button1 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.StaticText0 = CType(Me.GetItem("lblpay").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox0 = CType(Me.GetItem("cmbpay").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText1 = CType(Me.GetItem("lblfrom").Specific, SAPbouiCOM.StaticText)
            Me.EditText1 = CType(Me.GetItem("txtfrom").Specific, SAPbouiCOM.EditText)
            Me.StaticText2 = CType(Me.GetItem("lblto").Specific, SAPbouiCOM.StaticText)
            Me.EditText2 = CType(Me.GetItem("txtto").Specific, SAPbouiCOM.EditText)
            Me.StaticText4 = CType(Me.GetItem("Item_12").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox2 = CType(Me.GetItem("cmbesta").Specific, SAPbouiCOM.ComboBox)
            Me.Matrix0 = CType(Me.GetItem("Item_14").Specific, SAPbouiCOM.Matrix)
            Me.ComboBox1 = CType(Me.GetItem("cmbseries").Specific, SAPbouiCOM.ComboBox)
            Me.Button2 = CType(Me.GetItem("btnpay").Specific, SAPbouiCOM.Button)
            Me.EditText0 = CType(Me.GetItem("txtdocno").Specific, SAPbouiCOM.EditText)
            Me.CheckBox0 = CType(Me.GetItem("chkfinal").Specific, SAPbouiCOM.CheckBox)
            Me.StaticText5 = CType(Me.GetItem("Item_6").Specific, SAPbouiCOM.StaticText)
            Me.EditText3 = CType(Me.GetItem("txtlocc").Specific, SAPbouiCOM.EditText)
            Me.EditText4 = CType(Me.GetItem("txtlocn").Specific, SAPbouiCOM.EditText)
            Me.EditText5 = CType(Me.GetItem("txtdate").Specific, SAPbouiCOM.EditText)
            Me.LinkedButton1 = CType(Me.GetItem("lnkloc").Specific, SAPbouiCOM.LinkedButton)
            Me.CheckBox1 = CType(Me.GetItem("chkpay").Specific, SAPbouiCOM.CheckBox)
            Me.Button3 = CType(Me.GetItem("btnloan").Specific, SAPbouiCOM.Button)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()

        End Sub

        Private Sub OnCustomInitialize()
            objform = objaddon.objapplication.Forms.GetForm("OPRC", FormCount)
            objform = objaddon.objapplication.Forms.ActiveForm

            Try
                objform.Freeze(True)
                odbdsDetails = objform.DataSources.DBDataSources.Item(CType(1, Object))
                cmbdesignation = Matrix0.Columns.Item("desig")
                cmbdepartment = Matrix0.Columns.Item("dept")
                cmbpaymode = Matrix0.Columns.Item("paymode")
                'Matrix0.Columns.Item("ASum").Visible = False
                Comboload()
                ManageAttributes()
                If objaddon.ApprovedUser() Then
                    CheckBox0.Item.Enabled = True
                    CheckBox1.Item.Enabled = True
                End If
                If Link_objtype.ToString.ToUpper = "OPRC" And Link_Value.ToString <> "" Then
                    objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE
                    objform.Items.Item("txtentry").Enabled = True
                    objform.Items.Item("txtentry").Specific.string = Link_Value
                    Button0.Item.Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                    objform.Items.Item("txtentry").Enabled = False
                    Link_objtype = "-1" : Link_Value = "-1"
                    Matrix_Field_Setup()
                Else
                    objform.Items.Item("txtdate").Specific.string = Now.Date.ToString("yyyyMMdd")
                    CheckBox0.Item.Height = CheckBox0.Item.Height + 3
                    CheckBox1.Item.Height = CheckBox1.Item.Height + 3
                    CheckBox1.Item.Width = CheckBox1.Item.Width + 10
                    EditText3.Value = "#"
                    objform.Items.Item("txtentry").Specific.string = objaddon.objglobalmethods.GetNextDocentry_Value("@SMPR_OPRC")
                    ComboBox2.Select("1", SAPbouiCOM.BoSearchKey.psk_ByValue)
                    objform.ActiveItem = "cmbpay"
                End If
                ComboBox1.Select(0, SAPbouiCOM.BoSearchKey.psk_Index)
                objform.EnableMenu("1283", False) 'Remove
                objform.EnableMenu("1284", False) 'Cancel
                objform.EnableMenu("1286", False) 'close
                objform.Settings.Enabled = True
                If objaddon.objcompany.UserName.ToString.ToUpper <> "MANAGER" Then objform.EnableMenu("6913", False) 'User Defined Field

                objform.Freeze(False)
            Catch ex As Exception
                objform.Freeze(False)
            End Try
        End Sub

        Private Sub ManageAttributes()
            Try
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cmbpay", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtfrom", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtto", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cmbesta", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cmbseries", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtdocno", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtdate", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Visible(objform, "lnkloc", True, False, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "btnloan", True, False, True)
                CheckBox0.Item.Enabled = True
                CheckBox1.Item.Enabled = False
                Matrix0.Columns.Item("adjloan").Visible = False
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Comboload()
            Try
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery("CALL ""MIPL_HRMS_EMPMASTER_COMBO_FILLING"" ('OPRC')")
                If objrs.RecordCount = 0 Then Exit Sub
                For i As Integer = 0 To objrs.RecordCount - 1
                    Try
                        Select Case objrs.Fields.Item("Type").Value.ToString.ToUpper
                            Case "PAYPERIOD" : ComboBox0.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "STATUS" : ComboBox2.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "POSITION" : cmbdesignation.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "DEPARTMENT" : cmbdepartment.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "PAYMODE" : cmbpaymode.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
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
        Private WithEvents StaticText1 As SAPbouiCOM.StaticText
        Private WithEvents EditText1 As SAPbouiCOM.EditText
        Private WithEvents StaticText2 As SAPbouiCOM.StaticText
        Private WithEvents EditText2 As SAPbouiCOM.EditText
        Private WithEvents StaticText4 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox2 As SAPbouiCOM.ComboBox
        Private WithEvents Matrix0 As SAPbouiCOM.Matrix
        Private WithEvents ComboBox1 As SAPbouiCOM.ComboBox
        Private WithEvents Button2 As SAPbouiCOM.Button
        Private WithEvents EditText0 As SAPbouiCOM.EditText
        Private WithEvents CheckBox0 As SAPbouiCOM.CheckBox
        Private WithEvents StaticText5 As SAPbouiCOM.StaticText
        Private WithEvents EditText3 As SAPbouiCOM.EditText
        Private WithEvents EditText4 As SAPbouiCOM.EditText
        Private WithEvents EditText5 As SAPbouiCOM.EditText
        Private WithEvents LinkedButton1 As SAPbouiCOM.LinkedButton
        Private WithEvents CheckBox1 As SAPbouiCOM.CheckBox
#End Region

#Region "Form Events"

        Private Sub frmPayrollProcess_DataLoadAfter(ByRef pVal As SAPbouiCOM.BusinessObjectInfo) Handles Me.DataLoadAfter
            Try
                Dim Flag As Boolean = False
                If objaddon.ApprovedUser() Then
                    If CheckBox0.Checked = True Then
                        CheckBox0.Item.Enabled = False
                    Else
                        CheckBox0.Item.Enabled = True
                    End If
                    If CheckBox1.Checked = True Then
                        CheckBox1.Item.Enabled = False
                    Else
                        CheckBox1.Item.Enabled = True
                    End If
                End If

                objaddon.objglobalmethods.LoadCombo_SingleSeries_AfterFind(objform, "cmbseries", "OPRC", ComboBox1.Value)
                Matrix_Field_Setup()

                For i As Integer = 0 To odbdsDetails.Size - 1
                    If odbdsDetails.GetValue("U_GrossAmt", i) = 0 And odbdsDetails.GetValue("U_NetAmt", i) < 0 Then
                        Flag = True
                        Exit For
                    End If
                Next
                If Flag = True Then
                    Button3.Item.Enabled = True
                Else
                    Button3.Item.Enabled = False
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub frmPayrollProcess_ResizeAfter(pVal As SAPbouiCOM.SBOItemEventArg) Handles Me.ResizeAfter
            Try
                objform = objaddon.objapplication.Forms.GetForm("OPRC", FormCount)
                objform.Freeze(True)
                objaddon.objapplication.Menus.Item("1300").Activate()
                objform.Freeze(False)
            Catch ex As Exception
                objform.Freeze(False)
            End Try
        End Sub

#End Region

        Private Sub ComboBox1_ComboSelectAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox1.ComboSelectAfter
            If objform.Mode <> SAPbouiCOM.BoFormMode.fm_ADD_MODE Then Exit Sub
            If ComboBox1.Selected Is Nothing Then Exit Sub
            objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            objrs.DoQuery("Select ""NextNumber"" from Nnm1 where ""ObjectCode""='OPRC' and ""Series""='" & ComboBox1.Selected.Value & "'")
            If objrs.RecordCount > 0 Then
                EditText0.Value = objrs.Fields.Item(0).Value
            End If
        End Sub

        Private Sub ComboBox0_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox0.ClickAfter
            Try
                If objform.Mode <> SAPbouiCOM.BoFormMode.fm_ADD_MODE Then Exit Sub
                If ComboBox0.Selected Is Nothing Then Exit Sub
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery("select TO_VARCHAR(""F_RefDate"",'yyyyMMdd') ""F_RefDate"",TO_VARCHAR(""T_RefDate"",'yyyyMMdd') ""T_RefDate"" from OFPR where ""Code""='" & ComboBox0.Selected.Value & "'")
                'objrs.DoQuery("select ""F_RefDate"" ""F_RefDate"",""T_RefDate"" ""T_RefDate"" from OFPR where ""Code""='" & ComboBox0.Selected.Value & "'")
                If objrs.RecordCount > 0 Then
                    objform.Items.Item("txtfrom").Specific.string = objrs.Fields.Item("F_RefDate").Value.ToString
                    objform.Items.Item("txtto").Specific.string = objrs.Fields.Item("T_RefDate").Value.ToString
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Button2_ClickBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles Button2.ClickBefore
            If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Or objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE Then Exit Sub
            If ComboBox0.Selected Is Nothing Then
                objaddon.objapplication.SetStatusBarMessage("Pay period is Missing", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                BubbleEvent = False
                Exit Sub
            End If
            If ComboBox2.Selected Is Nothing Then
                objaddon.objapplication.SetStatusBarMessage("Employee Status is Missing", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                BubbleEvent = False
                Exit Sub
            End If
            'If EditText3.Value = "#" Then
            '    objaddon.objapplication.SetStatusBarMessage("Location is Missing", SAPbouiCOM.BoMessageTime.bmt_Short, True)
            '    BubbleEvent = False
            '    Exit Sub
            'End If

        End Sub

        Private Sub Button2_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button2.ClickAfter
            Dim Status As String = "", Location As String = ""
            Try
                If CheckBox0.Checked = True Then
                    objaddon.objapplication.SetStatusBarMessage("Payroll Already Finalized.", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                    Exit Sub
                End If
                Dim FDate As Date = Date.ParseExact(EditText1.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                'Status = objaddon.objglobalmethods.getSingleValue("select 1 as ""Status"" from ""@SMPR_OPRC"" where '" & FDate.ToString("yyyyMMdd") & "' between ""U_FromDate"" and ""U_ToDate"" and ifnull(""U_Process"",'')='Y'")
                'If Status = "1" Then
                '    objaddon.objapplication.SetStatusBarMessage("Payroll Already Finalized for the Month.", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                '    Exit Sub
                'End If
                objaddon.objapplication.SetStatusBarMessage("Calculating Payroll Details. Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Long, False)
                objform.Freeze(True)

                odbdsDetails.Clear()
                Matrix0.LoadFromDataSource()
                If EditText3.Value.ToString = "#" Then
                    Location = "-1"
                Else
                    Location = EditText3.Value.ToString
                End If

                strsql = "CALL ""MIPL_HRMS_PayrollProcess"" ('" & Location & "','" & ComboBox0.Selected.Value & "','" & ComboBox2.Selected.Value & "','" & objform.Items.Item("txtentry").Specific.string & "')"
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strsql)

                If objrs.RecordCount = 0 Then objaddon.objapplication.SetStatusBarMessage("No records Found", SAPbouiCOM.BoMessageTime.bmt_Short, True) : objform.Freeze(False) : Exit Sub

                odbdsDetails.InsertRecord(odbdsDetails.Size)

                objaddon.objapplication.SetStatusBarMessage("Filling Payroll Details. Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Long, False)

                For i As Integer = 0 To objrs.RecordCount - 1
                    odbdsDetails.SetValue("LineId", i, i + 1)
                    odbdsDetails.SetValue("U_empID", i, objrs.Fields.Item("U_empid").Value.ToString)

                    odbdsDetails.SetValue("U_IDNo", i, objrs.Fields.Item("EmpCode").Value.ToString)
                    odbdsDetails.SetValue("U_empName", i, objrs.Fields.Item("EmpName").Value.ToString)
                    odbdsDetails.SetValue("U_Designat", i, objrs.Fields.Item("Designation").Value.ToString)
                    odbdsDetails.SetValue("U_Dept", i, objrs.Fields.Item("DeptCode").Value.ToString)
                    odbdsDetails.SetValue("U_EmpLoc", i, objrs.Fields.Item("EmpLoc").Value.ToString)
                    odbdsDetails.SetValue("U_PayMode", i, objrs.Fields.Item("PayMode").Value.ToString)

                    odbdsDetails.SetValue("U_TotalDays", i, objrs.Fields.Item("TotalDays").Value.ToString)
                    odbdsDetails.SetValue("U_TDayWrkd", i, objrs.Fields.Item("WorkedDays").Value.ToString)
                    odbdsDetails.SetValue("U_HoliDays", i, objrs.Fields.Item("PHDays").Value.ToString)
                    odbdsDetails.SetValue("U_WODays", i, objrs.Fields.Item("WODays").Value.ToString)
                    odbdsDetails.SetValue("U_LveDays", i, objrs.Fields.Item("LveDays").Value.ToString)
                    odbdsDetails.SetValue("U_LOPDays", i, objrs.Fields.Item("LopDays").Value.ToString)
                    odbdsDetails.SetValue("U_PaidDays", i, objrs.Fields.Item("PayableDays").Value.ToString)

                    odbdsDetails.SetValue("U_A1", i, objrs.Fields.Item("A1").Value.ToString)
                    odbdsDetails.SetValue("U_A2", i, objrs.Fields.Item("A2").Value.ToString)
                    odbdsDetails.SetValue("U_A3", i, objrs.Fields.Item("A3").Value.ToString)
                    odbdsDetails.SetValue("U_A4", i, objrs.Fields.Item("A4").Value.ToString)
                    odbdsDetails.SetValue("U_A5", i, objrs.Fields.Item("A5").Value.ToString)
                    odbdsDetails.SetValue("U_A6", i, objrs.Fields.Item("A6").Value.ToString)
                    odbdsDetails.SetValue("U_A7", i, objrs.Fields.Item("A7").Value.ToString)
                    odbdsDetails.SetValue("U_A8", i, objrs.Fields.Item("A8").Value.ToString)
                    odbdsDetails.SetValue("U_A9", i, objrs.Fields.Item("A9").Value.ToString)
                    odbdsDetails.SetValue("U_A10", i, objrs.Fields.Item("A10").Value.ToString)
                    odbdsDetails.SetValue("U_A11", i, objrs.Fields.Item("A11").Value.ToString)
                    odbdsDetails.SetValue("U_A12", i, objrs.Fields.Item("A12").Value.ToString)
                    odbdsDetails.SetValue("U_A13", i, objrs.Fields.Item("A13").Value.ToString)
                    odbdsDetails.SetValue("U_A14", i, objrs.Fields.Item("A14").Value.ToString)
                    odbdsDetails.SetValue("U_A15", i, objrs.Fields.Item("A15").Value.ToString)
                    odbdsDetails.SetValue("U_A16", i, objrs.Fields.Item("A16").Value.ToString)
                    odbdsDetails.SetValue("U_A17", i, objrs.Fields.Item("A17").Value.ToString)
                    odbdsDetails.SetValue("U_A18", i, objrs.Fields.Item("A18").Value.ToString)
                    odbdsDetails.SetValue("U_A19", i, objrs.Fields.Item("A19").Value.ToString)
                    odbdsDetails.SetValue("U_A20", i, objrs.Fields.Item("A20").Value.ToString)


                    odbdsDetails.SetValue("U_Basic", i, objrs.Fields.Item("TotalBasic").Value.ToString)
                    odbdsDetails.SetValue("U_totsal", i, objrs.Fields.Item("TotalSalary").Value.ToString)
                    odbdsDetails.SetValue("U_TotalOTHrs", i, objrs.Fields.Item("OTHrs").Value.ToString)
                    odbdsDetails.SetValue("U_OTPHR", i, objrs.Fields.Item("TotalOT_Perhour").Value.ToString)
                    odbdsDetails.SetValue("U_TotalOTAmt", i, objrs.Fields.Item("OTAmt").Value.ToString)
                    odbdsDetails.SetValue("U_GrossAmt", i, objrs.Fields.Item("GrossSalary").Value.ToString)

                    odbdsDetails.SetValue("U_Addition", i, objrs.Fields.Item("TotalAddition").Value.ToString)
                    odbdsDetails.SetValue("U_FA1", i, objrs.Fields.Item("AirTicekt_Addition").Value.ToString)
                    odbdsDetails.SetValue("U_FA2", i, objrs.Fields.Item("TripAllowance_Addition").Value.ToString)
                    odbdsDetails.SetValue("U_AB1", i, objrs.Fields.Item("AB1").Value.ToString)
                    odbdsDetails.SetValue("U_AB2", i, objrs.Fields.Item("AB2").Value.ToString)
                    odbdsDetails.SetValue("U_AB3", i, objrs.Fields.Item("AB3").Value.ToString)
                    odbdsDetails.SetValue("U_AB4", i, objrs.Fields.Item("AB4").Value.ToString)
                    odbdsDetails.SetValue("U_AB5", i, objrs.Fields.Item("AB5").Value.ToString)
                    odbdsDetails.SetValue("U_AB6", i, objrs.Fields.Item("AB6").Value.ToString)
                    odbdsDetails.SetValue("U_AB7", i, objrs.Fields.Item("AB7").Value.ToString)
                    odbdsDetails.SetValue("U_AB8", i, objrs.Fields.Item("AB8").Value.ToString)
                    odbdsDetails.SetValue("U_AB9", i, objrs.Fields.Item("AB9").Value.ToString)
                    odbdsDetails.SetValue("U_AB10", i, objrs.Fields.Item("AB10").Value.ToString)
                    odbdsDetails.SetValue("U_AB11", i, objrs.Fields.Item("AB11").Value.ToString)
                    odbdsDetails.SetValue("U_AB12", i, objrs.Fields.Item("AB12").Value.ToString)
                    odbdsDetails.SetValue("U_AB13", i, objrs.Fields.Item("AB13").Value.ToString)
                    odbdsDetails.SetValue("U_AB14", i, objrs.Fields.Item("AB14").Value.ToString)
                    odbdsDetails.SetValue("U_AB15", i, objrs.Fields.Item("AB15").Value.ToString)
                    odbdsDetails.SetValue("U_AB16", i, objrs.Fields.Item("AB16").Value.ToString)
                    odbdsDetails.SetValue("U_AB17", i, objrs.Fields.Item("AB17").Value.ToString)
                    odbdsDetails.SetValue("U_AB18", i, objrs.Fields.Item("AB18").Value.ToString)
                    odbdsDetails.SetValue("U_AB19", i, objrs.Fields.Item("AB19").Value.ToString)
                    odbdsDetails.SetValue("U_AB20", i, objrs.Fields.Item("AB20").Value.ToString)

                    odbdsDetails.SetValue("U_Deduction", i, objrs.Fields.Item("TotalDeduction").Value.ToString)
                    odbdsDetails.SetValue("U_FD1", i, objrs.Fields.Item("LoanDeduction").Value.ToString)
                    odbdsDetails.SetValue("U_FD2", i, objrs.Fields.Item("AL_Settled_Deduction").Value.ToString)
                    odbdsDetails.SetValue("U_FD3", i, objrs.Fields.Item("AdvanceSal_Settlement_Deduction").Value.ToString)
                    odbdsDetails.SetValue("U_DB1", i, objrs.Fields.Item("DB1").Value.ToString)
                    odbdsDetails.SetValue("U_DB2", i, objrs.Fields.Item("DB2").Value.ToString)
                    odbdsDetails.SetValue("U_DB3", i, objrs.Fields.Item("DB3").Value.ToString)
                    odbdsDetails.SetValue("U_DB4", i, objrs.Fields.Item("DB4").Value.ToString)
                    odbdsDetails.SetValue("U_DB5", i, objrs.Fields.Item("DB5").Value.ToString)
                    odbdsDetails.SetValue("U_DB6", i, objrs.Fields.Item("DB6").Value.ToString)
                    odbdsDetails.SetValue("U_DB7", i, objrs.Fields.Item("DB7").Value.ToString)
                    odbdsDetails.SetValue("U_DB8", i, objrs.Fields.Item("DB8").Value.ToString)
                    odbdsDetails.SetValue("U_DB9", i, objrs.Fields.Item("DB9").Value.ToString)
                    odbdsDetails.SetValue("U_DB10", i, objrs.Fields.Item("DB10").Value.ToString)
                    odbdsDetails.SetValue("U_DB11", i, objrs.Fields.Item("DB11").Value.ToString)
                    odbdsDetails.SetValue("U_DB12", i, objrs.Fields.Item("DB12").Value.ToString)
                    odbdsDetails.SetValue("U_DB13", i, objrs.Fields.Item("DB13").Value.ToString)
                    odbdsDetails.SetValue("U_DB14", i, objrs.Fields.Item("DB14").Value.ToString)
                    odbdsDetails.SetValue("U_DB15", i, objrs.Fields.Item("DB15").Value.ToString)
                    odbdsDetails.SetValue("U_DB16", i, objrs.Fields.Item("DB16").Value.ToString)
                    odbdsDetails.SetValue("U_DB17", i, objrs.Fields.Item("DB17").Value.ToString)
                    odbdsDetails.SetValue("U_DB18", i, objrs.Fields.Item("DB18").Value.ToString)
                    odbdsDetails.SetValue("U_DB19", i, objrs.Fields.Item("DB19").Value.ToString)
                    odbdsDetails.SetValue("U_DB20", i, objrs.Fields.Item("DB20").Value.ToString)
                    odbdsDetails.SetValue("U_ASum", i, objrs.Fields.Item("ASum").Value.ToString)

                    'Dim ass As String = objrs.Fields.Item("TA1").Value.ToString
                    odbdsDetails.SetValue("U_TA1", i, objrs.Fields.Item("TA1").Value.ToString)
                    odbdsDetails.SetValue("U_TA2", i, objrs.Fields.Item("TA2").Value.ToString)
                    odbdsDetails.SetValue("U_TA3", i, objrs.Fields.Item("TA3").Value.ToString)
                    odbdsDetails.SetValue("U_TA4", i, objrs.Fields.Item("TA4").Value.ToString)
                    odbdsDetails.SetValue("U_TA5", i, objrs.Fields.Item("TA5").Value.ToString)
                    odbdsDetails.SetValue("U_TA6", i, objrs.Fields.Item("TA6").Value.ToString)
                    odbdsDetails.SetValue("U_TA7", i, objrs.Fields.Item("TA7").Value.ToString)
                    odbdsDetails.SetValue("U_TA8", i, objrs.Fields.Item("TA8").Value.ToString)
                    odbdsDetails.SetValue("U_TA9", i, objrs.Fields.Item("TA9").Value.ToString)
                    odbdsDetails.SetValue("U_TA10", i, objrs.Fields.Item("TA10").Value.ToString)
                    odbdsDetails.SetValue("U_TA11", i, objrs.Fields.Item("TA11").Value.ToString)
                    odbdsDetails.SetValue("U_TA12", i, objrs.Fields.Item("TA12").Value.ToString)
                    odbdsDetails.SetValue("U_TA13", i, objrs.Fields.Item("TA13").Value.ToString)
                    odbdsDetails.SetValue("U_TA14", i, objrs.Fields.Item("TA14").Value.ToString)
                    odbdsDetails.SetValue("U_TA15", i, objrs.Fields.Item("TA15").Value.ToString)
                    odbdsDetails.SetValue("U_TA16", i, objrs.Fields.Item("TA16").Value.ToString)
                    odbdsDetails.SetValue("U_TA17", i, objrs.Fields.Item("TA17").Value.ToString)
                    odbdsDetails.SetValue("U_TA18", i, objrs.Fields.Item("TA18").Value.ToString)
                    odbdsDetails.SetValue("U_TA19", i, objrs.Fields.Item("TA19").Value.ToString)
                    odbdsDetails.SetValue("U_TA20", i, objrs.Fields.Item("TA20").Value.ToString)

                    odbdsDetails.SetValue("U_RoundOff", i, objrs.Fields.Item("Roundoff").Value.ToString)
                    odbdsDetails.SetValue("U_NetAmt", i, objrs.Fields.Item("NetSalary").Value.ToString)
                    objrs.MoveNext()
                    If i <> objrs.RecordCount - 1 Then odbdsDetails.InsertRecord(odbdsDetails.Size)
                Next
                Matrix0.LoadFromDataSource()
                objaddon.objapplication.Menus.Item("1300").Activate()
                Matrix_Field_Setup()
                objaddon.objapplication.StatusBar.SetText("Payroll Details Loaded successfully", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE
                objform.Freeze(False)
            Catch ex As Exception
                objaddon.objapplication.SetStatusBarMessage("Error While Loading Payroll Details : " & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)
                objform.Freeze(False)
            End Try
        End Sub

        Private Sub Button0_ClickBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button0.ClickBefore

            If objform.Mode <> SAPbouiCOM.BoFormMode.fm_ADD_MODE Then Exit Sub

            If ComboBox0.Selected Is Nothing Then
                objaddon.objapplication.SetStatusBarMessage("Pay period is Missing", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                BubbleEvent = False
                Exit Sub
            End If
            If ComboBox2.Selected Is Nothing Then
                objaddon.objapplication.SetStatusBarMessage("Employee Status is Missing", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                BubbleEvent = False
                Exit Sub
            End If
            If EditText3.Value = "#" Then
                objaddon.objapplication.SetStatusBarMessage("Location is Missing", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                BubbleEvent = False
                Exit Sub
            End If
            If ComboBox1.Selected Is Nothing Then
                objaddon.objapplication.SetStatusBarMessage("Series is Missing", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                BubbleEvent = False
                Exit Sub
            End If

            If Matrix0.RowCount = 0 Then
                objaddon.objapplication.SetStatusBarMessage("Payroll Details is Missing", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                BubbleEvent = False
                Exit Sub
            End If
        End Sub

        Private Sub Matrix0_LinkPressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.LinkPressedAfter
            If pVal.ColUID = "Empid" And pVal.Row <> -1 Then
                Try
                    If Matrix0.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Specific.string = "" Then Exit Sub
                    Link_Value = Matrix0.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Specific.string
                    Link_objtype = "OHEM"
                    Dim oactiveform As New frmEmployeeMaster
                    oactiveform.Show()
                Catch ex As Exception

                End Try
            End If

        End Sub

        Private Sub EditText5_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText5.LostFocusAfter
            Try
                objaddon.objglobalmethods.LoadCombo_Series(objform, "cmbseries", "OPRC", IIf(EditText5.String = "", Now.Date, Date.ParseExact(EditText5.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)))
            Catch ex As Exception
            End Try
        End Sub

        Private Sub Matrix0_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.ClickAfter
            If pVal.Row <= 0 Then Exit Sub
            Try
                'Matrix0.Columns.Item("#").Cells.Item(pVal.Row).Click(SAPbouiCOM.BoCellClickType.ct_Regular)

                Matrix0.CommonSetting.SetRowBackColor(lastrowid, Matrix0.Item.BackColor)
                Matrix0.CommonSetting.SetRowBackColor(pVal.Row, Color.PaleGoldenrod.ToArgb)

                lastrowid = pVal.Row
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Matrix_Field_Setup()
            Try
                Matrix0.Columns.Item("U_A1").Visible = False
                Matrix0.Columns.Item("U_A2").Visible = False
                Matrix0.Columns.Item("U_A3").Visible = False
                Matrix0.Columns.Item("U_A4").Visible = False
                Matrix0.Columns.Item("U_A5").Visible = False
                Matrix0.Columns.Item("U_A6").Visible = False
                Matrix0.Columns.Item("U_A7").Visible = False
                Matrix0.Columns.Item("U_A8").Visible = False
                Matrix0.Columns.Item("U_A9").Visible = False
                Matrix0.Columns.Item("U_A10").Visible = False
                Matrix0.Columns.Item("U_A11").Visible = False
                Matrix0.Columns.Item("U_A12").Visible = False
                Matrix0.Columns.Item("U_A13").Visible = False
                Matrix0.Columns.Item("U_A14").Visible = False
                Matrix0.Columns.Item("U_A15").Visible = False
                Matrix0.Columns.Item("U_A16").Visible = False
                Matrix0.Columns.Item("U_A17").Visible = False
                Matrix0.Columns.Item("U_A18").Visible = False
                Matrix0.Columns.Item("U_A19").Visible = False
                Matrix0.Columns.Item("U_A20").Visible = False

                Matrix0.Columns.Item("U_AB1").Visible = False
                Matrix0.Columns.Item("U_AB2").Visible = False
                Matrix0.Columns.Item("U_AB3").Visible = False
                Matrix0.Columns.Item("U_AB4").Visible = False
                Matrix0.Columns.Item("U_AB5").Visible = False
                Matrix0.Columns.Item("U_AB6").Visible = False
                Matrix0.Columns.Item("U_AB7").Visible = False
                Matrix0.Columns.Item("U_AB8").Visible = False
                Matrix0.Columns.Item("U_AB9").Visible = False
                Matrix0.Columns.Item("U_AB10").Visible = False
                Matrix0.Columns.Item("U_AB11").Visible = False
                Matrix0.Columns.Item("U_AB12").Visible = False
                Matrix0.Columns.Item("U_AB13").Visible = False
                Matrix0.Columns.Item("U_AB14").Visible = False
                Matrix0.Columns.Item("U_AB15").Visible = False
                Matrix0.Columns.Item("U_AB16").Visible = False
                Matrix0.Columns.Item("U_AB17").Visible = False
                Matrix0.Columns.Item("U_AB18").Visible = False
                Matrix0.Columns.Item("U_AB19").Visible = False
                Matrix0.Columns.Item("U_AB20").Visible = False

                Matrix0.Columns.Item("U_DB1").Visible = False
                Matrix0.Columns.Item("U_DB2").Visible = False
                Matrix0.Columns.Item("U_DB3").Visible = False
                Matrix0.Columns.Item("U_DB4").Visible = False
                Matrix0.Columns.Item("U_DB5").Visible = False
                Matrix0.Columns.Item("U_DB6").Visible = False
                Matrix0.Columns.Item("U_DB7").Visible = False
                Matrix0.Columns.Item("U_DB8").Visible = False
                Matrix0.Columns.Item("U_DB9").Visible = False
                Matrix0.Columns.Item("U_DB10").Visible = False
                Matrix0.Columns.Item("U_DB11").Visible = False
                Matrix0.Columns.Item("U_DB12").Visible = False
                Matrix0.Columns.Item("U_DB13").Visible = False
                Matrix0.Columns.Item("U_DB14").Visible = False
                Matrix0.Columns.Item("U_DB15").Visible = False
                Matrix0.Columns.Item("U_DB16").Visible = False
                Matrix0.Columns.Item("U_DB17").Visible = False
                Matrix0.Columns.Item("U_DB18").Visible = False
                Matrix0.Columns.Item("U_DB19").Visible = False
                Matrix0.Columns.Item("U_DB20").Visible = False

                Matrix0.Columns.Item("U_TA1").Visible = False
                Matrix0.Columns.Item("U_TA2").Visible = False
                Matrix0.Columns.Item("U_TA3").Visible = False
                Matrix0.Columns.Item("U_TA4").Visible = False
                Matrix0.Columns.Item("U_TA5").Visible = False
                Matrix0.Columns.Item("U_TA6").Visible = False
                Matrix0.Columns.Item("U_TA7").Visible = False
                Matrix0.Columns.Item("U_TA8").Visible = False
                Matrix0.Columns.Item("U_TA9").Visible = False
                Matrix0.Columns.Item("U_TA10").Visible = False
                Matrix0.Columns.Item("U_TA11").Visible = False
                Matrix0.Columns.Item("U_TA12").Visible = False
                Matrix0.Columns.Item("U_TA13").Visible = False
                Matrix0.Columns.Item("U_TA14").Visible = False
                Matrix0.Columns.Item("U_TA15").Visible = False
                Matrix0.Columns.Item("U_TA16").Visible = False
                Matrix0.Columns.Item("U_TA17").Visible = False
                Matrix0.Columns.Item("U_TA18").Visible = False
                Matrix0.Columns.Item("U_TA19").Visible = False
                Matrix0.Columns.Item("U_TA20").Visible = False

                Matrix0.Columns.Item("basic").Visible = False
                Matrix0.Columns.Item("totsal").Visible = False

                strsql = "select 'U_'||""U_Sequence"" ""ColName"",""Name"" from ""@SMPR_OPYE"" where ""U_Sequence""<>'' "
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strsql)
                If objrs.RecordCount > 0 Then
                    For i As Integer = 0 To objrs.RecordCount - 1
                        Matrix0.Columns.Item(objrs.Fields.Item("ColName").Value.ToString).Visible = True
                        Matrix0.Columns.Item(objrs.Fields.Item("ColName").Value.ToString).TitleObject.Caption = objrs.Fields.Item("Name").Value.ToString
                        Matrix0.Columns.Item(objrs.Fields.Item("ColName").Value.ToString).RightJustified = True
                        Matrix0.Columns.Item(objrs.Fields.Item("ColName").Value.ToString).ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                        objrs.MoveNext()
                    Next
                End If

                Matrix0.Columns.Item("netsal").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("totsal").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("totamt").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("Gsal").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("tadd").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("atick").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("trip").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("TD").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("loan").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("alsal").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("advsal").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                Matrix0.Columns.Item("round").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
                'Matrix0.Columns.Item("ASum").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto

                'Matrix0.Columns.Item("netsal").BackColor = Color.SeaGreen.ToArgb
                Matrix0.Columns.Item("netsal").ForeColor = Color.Red.ToArgb
                Matrix0.Columns.Item("netsal").TextStyle = FontStyle.Bold
                Matrix0.Columns.Item("pdays").ForeColor = Color.Green.ToArgb
                Matrix0.Columns.Item("pdays").TextStyle = FontStyle.Bold
                Matrix0.Columns.Item("totamt").ForeColor = Color.DarkOrange.ToArgb
                Matrix0.Columns.Item("totamt").TextStyle = FontStyle.Bold
                Matrix0.Columns.Item("Gsal").ForeColor = 150
                Matrix0.Columns.Item("Gsal").TextStyle = FontStyle.Bold
                Matrix0.Columns.Item("tadd").ForeColor = Color.Brown.ToArgb
                Matrix0.Columns.Item("tadd").TextStyle = FontStyle.Bold
                Matrix0.Columns.Item("TD").ForeColor = Color.DarkMagenta.ToArgb
                Matrix0.Columns.Item("TD").TextStyle = FontStyle.Bold

                Matrix0.CommonSetting.FixedColumnsCount = 9
                Matrix0.AutoResizeColumns()
                'objaddon.objapplication.Menus.Item("1300").Activate()
            Catch ex As Exception

            End Try
        End Sub

        Private Sub LinkedButton1_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles LinkedButton1.ClickAfter
            Try
                frmmultiselectform = objaddon.objapplication.Forms.ActiveForm
                Query_multiselect = "select 'Y' ""Select"",""Code"",""Location"" from olct where ifnull(""U_HR"",'')='Y' and '" & EditText3.Value & "' like '%#'||cast(""Code"" as varchar) ||'#%' union all"
                Query_multiselect += vbCrLf + "select 'N' ""Select"",""Code"",""Location"" from olct where ifnull(""U_HR"",'')='Y' and '" & EditText3.Value & "' not like '%#'||cast(""Code"" as varchar) ||'#%' order by ""Code"""
                multi_objtype = "OLCT"
                Dim activeform As New Frmmulitselect
                activeform.Show()
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Button0_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button0.PressedAfter
            Try
                If pVal.ActionSuccess = True And objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                    If addupdate = True Then objaddon.objapplication.StatusBar.SetText("Payroll Process Added and Document Sent for Approval", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                    objform.Items.Item("txtdate").Specific.string = Now.Date.ToString("yyyyMMdd")
                    EditText3.Value = "#"
                    objform.Items.Item("txtentry").Specific.string = objaddon.objglobalmethods.GetNextDocentry_Value("@SMPR_OPRC")
                    ComboBox2.Select("1", SAPbouiCOM.BoSearchKey.psk_ByValue)
                    objform.ActiveItem = "cmbpay"
                ElseIf pVal.ActionSuccess = True And objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then
                    If addupdate = True Then objaddon.objapplication.StatusBar.SetText("Payroll Process Updated and Document Sent for Approval", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                    objaddon.objapplication.Menus.Item("1304").Activate()
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Button0_PressedBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button0.PressedBefore
            Try
                If (objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Or objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE) Then
                    addupdate = True
                Else
                    addupdate = False
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub CheckBox0_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles CheckBox0.PressedAfter
            If CheckBox0.Checked = True Then
                CheckBox0.Item.Enabled = False
            End If

        End Sub

        Private Sub CheckBox1_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles CheckBox1.PressedAfter
            If CheckBox1.Checked = True Then
                CheckBox1.Item.Enabled = False
            End If

        End Sub

        Private Sub CheckBox1_PressedBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles CheckBox1.PressedBefore

            If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then BubbleEvent = False
        End Sub
        Dim posted_entryno As String
        Dim lretcode

        Private Sub PayrollJEPosting()
            Dim TotalAdd As Double = 0.0, TotalDed As Double = 0.0, TotalAmount As Double = 0.0
            Dim strsql As String = "", StrQuery As String = ""
            Dim objrecset As SAPbobsCOM.Recordset
            Try
                For i As Integer = 1 To Matrix0.RowCount
                    TotalAdd += CDbl(Matrix0.Columns.Item("ASum").Cells.Item(i).Specific.String) + CDbl(Matrix0.Columns.Item("totamt").Cells.Item(i).Specific.String) + CDbl(Matrix0.Columns.Item("tadd").Cells.Item(i).Specific.String) ' + CDbl(Matrix0.Columns.Item("atick").Cells.Item(i).Specific.String)
                    TotalDed += CDbl(Matrix0.Columns.Item("TD").Cells.Item(i).Specific.String) '+ CDbl(Matrix0.Columns.Item("alsal").Cells.Item(i).Specific.String) + CDbl(Matrix0.Columns.Item("advsal").Cells.Item(i).Specific.String) ' CDbl(Matrix0.Columns.Item("loan").Cells.Item(i).Specific.String) + 
                Next
                'TotalAmount = TotalAdd + TotalDed
                Try
                    objrecset = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    Dim FDate As Date = Date.ParseExact(objform.Items.Item("txtdate").Specific.String, "dd/MM/yy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                    Dim PPDate As Date = Date.ParseExact(objform.Items.Item("txtfrom").Specific.String, "dd/MM/yy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                    objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    strsql = "Select distinct 'OPRC' ""Transcode"",'Payroll Process For the Month of " & CStr(MonthName(PPDate.Month, True)) & " " & CStr(PPDate.Year) & "' ""Memo"", 'Payroll Process For the Month' ""Narration"","
                    strsql += vbCrLf + " 'Payroll Entry No :" & objform.Items.Item("txtentry").Specific.string & " Doc No :" & objform.Items.Item("txtdocno").Specific.string & "' ""Ref1"",'PayrollProcess' ""Ref2"",'" & objform.Items.Item("txtdocno").Specific.string & "' ""Ref3"""
                    strsql += vbCrLf + " from dummy"
                    objrs.DoQuery(strsql)

                    StrQuery = "Select Top 1 'Salary' ""Type"",T3.""U_payeledc"" ""DebitCode"",T3.""U_payeledn"" ""DebitName"",T3.""U_payelecc"" ""CreditCode"",T3.""U_payelecn"" ""CreditName"""
                    StrQuery += vbCrLf + " from ""@SMPR_ACCT3"" T3 inner join ""@SMPR_ACCT"" T2 on T2.""Code""=T3.""Code"" where '" & FDate.ToString("yyyyMMdd") & "' between T2.""U_fromdate"" and T2.""U_todate"" "
                    StrQuery += vbCrLf + "  union all "
                    StrQuery += vbCrLf + " Select 'Deduction' ""Type"",T2.""U_adddeddc"",T2.""U_adddeddn"",T2.""U_adddedcc"",T2.""U_adddedcn"" "
                    StrQuery += vbCrLf + " from  ""@SMPR_ACCT2"" T2 inner join ""@SMPR_ACCT"" T3 on T2.""Code""=T3.""Code"" where '" & FDate.ToString("yyyyMMdd") & "' between T3.""U_fromdate"" and T3.""U_todate"" ;"
                    objrecset.DoQuery(StrQuery)
                    If objrecset.RecordCount = 0 Then objaddon.objapplication.SetStatusBarMessage("Account Mapping Required for this month", SAPbouiCOM.BoMessageTime.bmt_Short, True) : objform.Freeze(False) : Exit Sub
                    Dim stat As Boolean = False
                    For i As Integer = 0 To objrecset.RecordCount - 1
                        If objrecset.Fields.Item("DebitCode").Value = "" Or objrecset.Fields.Item("CreditCode").Value = "" Then
                            objaddon.objapplication.SetStatusBarMessage("Account Mapping Required for the Payroll Type : " & objrecset.Fields.Item("Type").Value, SAPbouiCOM.BoMessageTime.bmt_Long, True)
                            stat = True
                        End If
                        objrecset.MoveNext()
                    Next
                    If stat = True Then Exit Sub
                    objrecset.MoveFirst()
                    If Not objaddon.objcompany.InTransaction Then objaddon.objcompany.StartTransaction()
                    Dim oPayrollJV As SAPbobsCOM.JournalVouchers
                    oPayrollJV = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalVouchers)

                    oPayrollJV.JournalEntries.ReferenceDate = FDate.ToString("yyyy/MM/dd") 'Now.ToString("yyyy/MM/dd")  'objrs.Fields.Item("Date").Value
                    oPayrollJV.JournalEntries.DueDate = FDate.ToString("yyyy/MM/dd") 'Now.ToString("yyyy/MM/dd")  'objrs.Fields.Item("Date").Value
                    oPayrollJV.JournalEntries.TaxDate = FDate.ToString("yyyy/MM/dd") 'Now.ToString("yyyy/MM/dd") ' 'objrs.Fields.Item("Date").Value
                    'oPayrollJV.JournalEntries.TransactionCode = objrs.Fields.Item("Transcode").Value.ToString
                    'oPayrollJV.Memo = "Payroll Process for the month -" & CStr(MonthName(FDate.Month, True))
                    oPayrollJV.JournalEntries.Memo = objrs.Fields.Item("Memo").Value.ToString
                    oPayrollJV.JournalEntries.UserFields.Fields.Item("U_Narration").Value = objrs.Fields.Item("Narration").Value.ToString

                    oPayrollJV.JournalEntries.Reference = objrs.Fields.Item("Ref1").Value.ToString
                    oPayrollJV.JournalEntries.Reference2 = objrs.Fields.Item("Ref2").Value.ToString
                    oPayrollJV.JournalEntries.Reference3 = objrs.Fields.Item("Ref3").Value.ToString
                    For i As Integer = 0 To objrecset.RecordCount - 1
                        If objrecset.RecordCount > 0 Then
                            Select Case objrecset.Fields.Item("Type").Value.ToString.ToUpper
                                Case "SALARY"
                                    oPayrollJV.JournalEntries.Lines.AccountCode = objrecset.Fields.Item("DebitCode").Value
                                    oPayrollJV.JournalEntries.Lines.Debit = TotalAdd 'objrs.Fields.Item("DebitAmount").Value
                                    oPayrollJV.JournalEntries.Lines.Credit = 0 ' objrs.Fields.Item("CreditAmount").Value
                                    oPayrollJV.JournalEntries.Lines.Add()
                                    oPayrollJV.JournalEntries.Lines.AccountCode = objrecset.Fields.Item("CreditCode").Value
                                    oPayrollJV.JournalEntries.Lines.Debit = 0 'objrs.Fields.Item("DebitAmount").Value
                                    oPayrollJV.JournalEntries.Lines.Credit = TotalAdd ' objrs.Fields.Item("CreditAmount").Value

                                Case "DEDUCTION"
                                    oPayrollJV.JournalEntries.Lines.AccountCode = objrecset.Fields.Item("DebitCode").Value
                                    oPayrollJV.JournalEntries.Lines.Debit = TotalDed 'objrs.Fields.Item("DebitAmount").Value
                                    oPayrollJV.JournalEntries.Lines.Credit = 0 ' objrs.Fields.Item("CreditAmount").Value
                                    oPayrollJV.JournalEntries.Lines.Add()
                                    oPayrollJV.JournalEntries.Lines.AccountCode = objrecset.Fields.Item("CreditCode").Value
                                    oPayrollJV.JournalEntries.Lines.Debit = 0 'objrs.Fields.Item("DebitAmount").Value
                                    oPayrollJV.JournalEntries.Lines.Credit = TotalDed ' objrs.Fields.Item("CreditAmount").Value
                            End Select
                            oPayrollJV.JournalEntries.Lines.Add()
                            objrecset.MoveNext()
                        End If
                    Next
                    'oPayrollJV.JournalEntries.Lines.AccountCode = objrs.Fields.Item("DebitCode").Value
                    'oPayrollJV.JournalEntries.Lines.Debit = TotalAmount 'objrs.Fields.Item("DebitAmount").Value
                    'oPayrollJV.JournalEntries.Lines.Credit = 0 'objrs.Fields.Item("CreditAmount").Value

                    'oPayrollJV.JournalEntries.Lines.Add()
                    'oPayrollJV.JournalEntries.Lines.AccountCode = objrs.Fields.Item("CreditCode").Value
                    'oPayrollJV.JournalEntries.Lines.Debit = 0 'objrs.Fields.Item("DebitAmount").Value
                    'oPayrollJV.JournalEntries.Lines.Credit = TotalAmount 'objrs.Fields.Item("CreditAmount").Value
                    'oPayrollJV.JournalEntries.Lines.Add()

                    oPayrollJV.JournalEntries.Add()
                    lretcode = oPayrollJV.Add()
                    If lretcode <> 0 Then
                        If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                        objaddon.objapplication.SetStatusBarMessage(objaddon.objcompany.GetLastErrorDescription, SAPbouiCOM.BoMessageTime.bmt_Long, True)
                        objaddon.objglobalmethods.status_Update("OPRC", objform.Items.Item("txtentry").Specific.string, 0, objaddon.objcompany.GetLastErrorDescription, -1)
                    Else
                        posted_entryno = objaddon.objcompany.GetNewObjectKey()
                        objaddon.objglobalmethods.status_Update("OPRC", objform.Items.Item("txtentry").Specific.string, 1, "Success", posted_entryno.ToString)
                        If objaddon.objglobalmethods.Update_query("update ""@SMPR_OPRC"" set ""U_jeno""='" & posted_entryno & "' where ""DocEntry""='" & objform.Items.Item("txtentry").Specific.string & "'") Then
                            If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                            objaddon.objapplication.SetStatusBarMessage("Journal Entry Successfully Posted..." & posted_entryno, SAPbouiCOM.BoMessageTime.bmt_Long, False)
                        Else
                            If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                        End If
                    End If

                Catch ex As Exception
                    If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                    objaddon.objglobalmethods.status_Update("OPRC", objform.Items.Item("txtentry").Specific.string, 0, ex.Message.ToString, -1)
                End Try

            Catch ex As Exception
                objaddon.objglobalmethods.status_Update("OPRC", objform.Items.Item("txtentry").Specific.string, 0, ex.Message.ToString, -1)
            End Try
        End Sub

        Private Sub Button0_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button0.ClickAfter
            'If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Or objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE Then
            '    '    If CheckBox0.Checked = True Then
            '    '        If ApprovedUser_Employee Then
            '    '            PayrollJEPosting()
            '    '        Else
            '    '            objaddon.objapplication.SetStatusBarMessage("You are not authorized to post JE", SAPbouiCOM.BoMessageTime.bmt_Long, True)
            '    '            Exit Sub
            '    '        End If
            '    '    Else
            '    '        'objaddon.objapplication.SetStatusBarMessage("Please Tick the Finalize to Post JE", SAPbouiCOM.BoMessageTime.bmt_Long, False)
            '    '        Exit Sub
            '    '    End If
            'End If
        End Sub



        Public Sub Payslip_AutoEmail_NewTest()
            Try
                Dim FromMail_id As String = "", FromMail_Password As String = "", Mail_Host As String = "", Mail_Port As String = ""
                Dim strquery, Foldername, Filename As String
                Dim objrs As SAPbobsCOM.Recordset
                Dim objrsupdate As SAPbobsCOM.Recordset
                Dim Mailbody, ServerName, CompanyDb, DBUserName, DbPassword As String
                Dim Payroll_Report_FileName = System.Windows.Forms.Application.StartupPath & "\" & "PaySlip_OEC.rpt"
                Dim initialpath As String = objaddon.objglobalmethods.getSingleValue("select ""AttachPath"" from OADP")
                Foldername = initialpath + "Payroll\RptFile"
                If Directory.Exists(Foldername) Then
                Else
                    Directory.CreateDirectory(Foldername)
                End If
                Filename = Foldername & "\PaySlip1.rpt"
                Filename = "E:\Chitra\Common Payroll\Dec 16\BackUp Source Payroll\April 27 2022\TestReport.rpt"
                ServerName = "WATSON.TMICLOUD.NET:30013" '"WAT@WATSON.TMICLOUD.NET:30013"
                CompanyDb = "OEC_TEST" '"KANAKAVALLI_LIVE"
                DBUserName = "OECDBBR" '"KANASA" '"OECDBBR"
                DbPassword = "India@1947" '"R&s$!a#f%ru$456" ' "India@1947"

                FromMail_id = "saptech18@mukeshinfoserve.com"
                FromMail_Password = "D@rloo@30895"
                Mail_Host = "smtp-mail.outlook.com"
                Mail_Port = "587"
                If FromMail_id = "" Or FromMail_Password = "" Or Mail_Host = "" Or Mail_Port = "" Then Exit Sub
                'MsgBox(Payroll_Report_FileName)   
                Dim cryRpt As New ReportDocument
                cryRpt.Load(Filename)
                cryRpt.DataSourceConnections(0).SetConnection(objaddon.objcompany.Server, CompanyDb, False) 'objaddon.objcompany.CompanyDB
                cryRpt.DataSourceConnections(0).SetLogon(DBUserName, DbPassword)


                For i As Integer = 1 To Matrix0.VisualRowCount
                    'If objrs.Fields.Item("ToEmail").Value.ToString = "" Then Continue For

                    Dim Email As New System.Net.Mail.MailMessage
                    Dim MailServer As New System.Net.Mail.SmtpClient()

                    Try
                        MailServer.Host = Mail_Host
                        MailServer.Port = Mail_Port
                        MailServer.Credentials = New System.Net.NetworkCredential(FromMail_id.ToString.Trim, FromMail_Password.ToString.Trim)
                        MailServer.EnableSsl = True
                        Email.From = New System.Net.Mail.MailAddress(FromMail_id.ToString.Trim)

                        Email.To.Add(New System.Net.Mail.MailAddress("saptech18@mukeshinfoserve.com"))
                        Email.Subject = "Pay Slip - " ' & objrs.Fields.Item("ToName").Value.ToString & " - " & objrs.Fields.Item("MonthName").Value.ToString & " - " & objrs.Fields.Item("Year").Value.ToString

                        Mailbody = "Dear Chitra, " '& objrs.Fields.Item("ToName").Value.ToString & ","
                        Mailbody += vbCrLf + " "
                        Mailbody += vbCrLf + " Please Find the Attached Payslip for the Month of " '& objrs.Fields.Item("Period").Value.ToString & "."
                        Mailbody += vbCrLf + " "
                        Mailbody += vbCrLf + "With Regards,"
                        Mailbody += vbCrLf + "HR Team"
                        Mailbody += vbCrLf + " "
                        Mailbody += vbCrLf + " "
                        Mailbody += "This is Auto generated E-Mail from SAP Business One . Please do not reply to this message. Thank you! "

                        Email.Body = Mailbody
                        Email.Priority = Net.Mail.MailPriority.High

                        'cryRpt.SetParameterValue("Emp@select empid,FIRSTNAME+'  '+LASTNAME from ohem order by Firstname", objrs.Fields.Item("Empid").Value.ToString)
                        'cryRpt.SetParameterValue("Month", objrs.Fields.Item("Month").Value.ToString)
                        'cryRpt.SetParameterValue("year@select distinct year(T0.u_todate) year from [@SMPR_OPRC] T0", objrs.Fields.Item("Year").Value.ToString)
                        'cryRpt.SetParameterValue("OTTA", "N")


                        'cryRpt.SetParameterValue("Month", "MARCH")
                        'cryRpt.SetParameterValue("Year@select year(current_date) from dummy union all select year(current_date)-1 from dummy union all select year(current_date)-2 from dummy", "2020")
                        'cryRpt.SetParameterValue("Emp@select Distinct T1.""U_empID"",T1.""U_empName"" from ""@MIPL_PPI1"" T1 where ifnull(T1.""U_empID"",'')<>''", "EMP/KVL/00016") 'objrs.Fields.Item("U_ExtEmpNo").Value.ToString

                        'Dim attachment As System.Net.Mail.Attachment
                        'attachment = New System.Net.Mail.Attachment("E:\Chitra\Common Payroll\Dec 16\Employee master - Insert query.txt")
                        'Email.Attachments.Add(attachment)
                        'Email.Attachments.Add(cryRpt.ExportToStream(ExportFormatType.PortableDocFormat, ""))
                        'cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, "E:\Chitra\Common Payroll\Dec 16\test.pdf")

                        Email.Attachments.Add(New Attachment(cryRpt.ExportToStream(ExportFormatType.PortableDocFormat), "Pay Slip - .PDF"))

                        MailServer.Send(Email)

                        'strsql = "Update ""@SMPR_PRC1"" set ""U_Payslip""='Y' where ""DocEntry""='" & objrs.Fields.Item("DocEntry").Value.ToString & "' and ""U_empid""='" & objrs.Fields.Item("Empid").Value.ToString & "'"
                        'objrsupdate = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                        'objrsupdate.DoQuery(strsql)

                    Catch ex As Exception
                    Finally
                        If Not Email Is Nothing Then Email.Dispose()
                        MailServer = Nothing
                    End Try

                    'objrs.MoveNext()
                Next

            Catch ex As Exception
                MsgBox(ex.Message.ToString)
            End Try

        End Sub

        'Private Sub Button3_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button3.ClickAfter
        '    Try
        '        Payslip_AutoEmail()
        '    Catch ex As Exception

        '    End Try

        'End Sub

        Public Sub Payslip_AutoEmail()
            Try
                Dim FromMail_id As String = "", FromMail_Password As String = "", Mail_Host As String = "", Mail_Port As String = ""
                Dim strquery, Foldername, Filename As String
                Dim objrs As SAPbobsCOM.Recordset
                Dim objrsupdate As SAPbobsCOM.Recordset
                Dim Mailbody, ServerName, CompanyDb, DBUserName, DbPassword As String
                Dim Payroll_Report_FileName = System.Windows.Forms.Application.StartupPath & "\" & "PaySlip_Ymh_Oec.rpt"
                Dim initialpath As String = objaddon.objglobalmethods.getSingleValue("select ""AttachPath"" from OADP")
                Foldername = initialpath + "Payroll\RptFile"
                If Directory.Exists(Foldername) Then
                Else
                    Directory.CreateDirectory(Foldername)
                End If
                Filename = Foldername & "\PaySlip1.rpt"

                ServerName = "WATSON.TMICLOUD.NET:30013" '"WAT@WATSON.TMICLOUD.NET:30013"
                CompanyDb = "OEC_TEST"
                DBUserName = "OECDBBR"
                DbPassword = "India@1947"
                'strsql = " Select T0.DocEntry,Datepart(MM,T0.U_fromdate)[Month],Datepart(yyyy,T0.U_Fromdate)[Year],DateName(Month,T0.U_fromdate)+' - '+Convert(varchar,Datepart(yyyy,T0.U_Fromdate))[Period],"
                'strsql += vbCrLf + " T2.U_empid[Empid],T2.U_ExtEmpNo,isnull(T2.U_firstNam,'')+' '+isnull(T2.U_lastName,'')[ToName],isnull(T2.U_Email,'')[ToEmail],'N'[OTTA]"
                'strsql += vbCrLf + " from [@SMPR_OPRC] T0 inner join [@SMPR_PRC1] T1 on T0.DOcentry=T1.DocEntry Inner join [@SMPR_OHEM] T2 on T1.U_Empid=T2.U_empid"
                'strsql += vbCrLf + " Where T0.U_Fromdate=(Select Max(U_Fromdate) from [@SMPR_OPRC] Where U_process='Y') and isnull(T2.U_payslip,'')='Y' and isnull(T2.U_Email,'')<>''"
                'strsql += vbCrLf + " and isnull(T1.U_payslip,'N')='N' and isnull(T0.U_Apayslip,'N')='Y'"

                strquery = "Select T0.""DocEntry"",MONTHNAME(T0.""U_FromDate"") ""MonthName"", MONTH(T0.""U_FromDate"") AS ""Month"", YEAR(T0.""U_FromDate"") AS ""Year"", MONTH(T0.""U_FromDate"") || ' - ' || CAST(YEAR(T0.""U_FromDate"") AS varchar) AS ""Period"","
                strquery += vbCrLf + " T2.""U_empID"" AS ""Empid"", T2.""U_ExtEmpNo"", IFNULL(T2.""U_firstNam"", '') || ' ' || IFNULL(T2.""U_lastName"", '') AS ""ToName"", IFNULL(T2.""U_email"", '') AS ""ToEmail"","
                strquery += vbCrLf + " 'N' AS ""OTTA"" FROM ""@SMPR_OPRC"" T0 INNER JOIN ""@SMPR_PRC1"" T1 ON T0.""DocEntry"" = T1.""DocEntry"" INNER Join ""@SMPR_OHEM"" T2 ON T1.""U_empID"" = T2.""U_empID"" "
                strquery += vbCrLf + " WHERE T0.""U_FromDate"" = (SELECT MAX(""U_FromDate"") FROM ""@SMPR_OPRC"" WHERE ""U_Process"" = 'Y') AND IFNULL(T2.""U_PaySlip"", '') = 'Y' "
                strquery += vbCrLf + " And IFNULL(T2.""U_email"", '') <> '' AND IFNULL(T1.""U_PaySlip"", 'N') = 'N' AND IFNULL(T0.""U_APayslip"", 'N') = 'Y'"


                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strquery)
                If objrs.RecordCount = 0 Then Exit Sub
                FromMail_id = "saptech18@mukeshinfoserve.com"
                FromMail_Password = "D@rloo@30895"
                Mail_Host = "smtp-mail.outlook.com"
                Mail_Port = "587"
                If FromMail_id = "" Or FromMail_Password = "" Or Mail_Host = "" Or Mail_Port = "" Then Exit Sub
                'MsgBox(Payroll_Report_FileName)   
                Dim cryRpt As New ReportDocument
                cryRpt.Load(Payroll_Report_FileName)
                cryRpt.DataSourceConnections(0).SetConnection(objaddon.objcompany.Server, objaddon.objcompany.CompanyDB, False)
                cryRpt.DataSourceConnections(0).SetLogon(DBUserName, DbPassword)


                For i As Integer = 0 To objrs.RecordCount - 1
                    If objrs.Fields.Item("ToEmail").Value.ToString = "" Then Continue For

                    Dim Email As New System.Net.Mail.MailMessage
                    Dim MailServer As New System.Net.Mail.SmtpClient()

                    Try
                        MailServer.Host = Mail_Host
                        MailServer.Port = Mail_Port
                        MailServer.Credentials = New System.Net.NetworkCredential(FromMail_id.ToString.Trim, FromMail_Password.ToString.Trim)
                        MailServer.EnableSsl = True
                        Email.From = New System.Net.Mail.MailAddress(FromMail_id.ToString.Trim)

                        Email.To.Add(New System.Net.Mail.MailAddress(objrs.Fields.Item("ToEmail").Value.ToString))
                        Email.Subject = "Pay Slip - " & objrs.Fields.Item("ToName").Value.ToString & " - " & objrs.Fields.Item("MonthName").Value.ToString & " - " & objrs.Fields.Item("Year").Value.ToString

                        Mailbody = "Dear " & objrs.Fields.Item("ToName").Value.ToString & ","
                        Mailbody += vbCrLf + " "
                        Mailbody += vbCrLf + " Please Find the Attached Payslip for the Month of " & objrs.Fields.Item("Period").Value.ToString & "."
                        Mailbody += vbCrLf + " "
                        Mailbody += vbCrLf + "With Regards,"
                        Mailbody += vbCrLf + "HR Team"
                        Mailbody += vbCrLf + " "
                        Mailbody += vbCrLf + " "
                        Mailbody += "This is Auto generated E-Mail from SAP Business One . Please do not reply to this message. Thank you! "

                        Email.Body = Mailbody
                        Email.Priority = Net.Mail.MailPriority.High

                        'cryRpt.SetParameterValue("Emp@select empid,FIRSTNAME+'  '+LASTNAME from ohem order by Firstname", objrs.Fields.Item("Empid").Value.ToString)
                        'cryRpt.SetParameterValue("Month", objrs.Fields.Item("Month").Value.ToString)
                        'cryRpt.SetParameterValue("year@select distinct year(T0.u_todate) year from [@SMPR_OPRC] T0", objrs.Fields.Item("Year").Value.ToString)
                        'cryRpt.SetParameterValue("OTTA", "N")

                        cryRpt.SetParameterValue("@DocKey", objrs.Fields.Item("DocEntry").Value.ToString)
                        'cryRpt.SetParameterValue("Month", objrs.Fields.Item("MonthName").Value.ToString)
                        'cryRpt.SetParameterValue("Year@select year(current_date) from dummy union all select year(current_date)-1 from dummy union all select year(current_date)-2 from dummy", objrs.Fields.Item("Year").Value.ToString)
                        'cryRpt.SetParameterValue("Emp@select Distinct T1.""U_empID"",T1.""U_empName"" from ""@MIPL_PPI1"" T1 where ifnull(T1.""U_empID"",'')<>''", "EMP/KVL/00016") 'objrs.Fields.Item("U_ExtEmpNo").Value.ToString

                        'Dim attachment As System.Net.Mail.Attachment
                        'attachment = New System.Net.Mail.Attachment("E:\Chitra\Common Payroll\Dec 16\Employee master - Insert query.txt")
                        'Email.Attachments.Add(attachment)
                        'Email.Attachments.Add(cryRpt.ExportToStream(ExportFormatType.PortableDocFormat, ""))
                        'cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, "E:\Chitra\Common Payroll\Dec 16\test.pdf")

                        Email.Attachments.Add(New Attachment(cryRpt.ExportToStream(ExportFormatType.PortableDocFormat), "Pay Slip - " & objrs.Fields.Item("ToName").Value.ToString & " - " & objrs.Fields.Item("Period").Value.ToString & ".PDF"))

                        MailServer.Send(Email)

                        strsql = "Update ""@SMPR_PRC1"" set ""U_PaySlip""='Y' where ""DocEntry""='" & objrs.Fields.Item("DocEntry").Value.ToString & "' and ""U_empid""='" & objrs.Fields.Item("Empid").Value.ToString & "'"
                        objrsupdate = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                        objrsupdate.DoQuery(strsql)

                    Catch ex As Exception
                    Finally
                        If Not Email Is Nothing Then Email.Dispose()
                        MailServer = Nothing
                    End Try

                    objrs.MoveNext()
                Next

            Catch ex As Exception
                MsgBox(ex.Message.ToString)
            End Try

        End Sub

        Public Sub Payslip_AutoEmail_New()
            Try
                Dim FromMail_id As String = "", FromMail_Password As String = "", Mail_Host As String = "", Mail_Port As String = ""
                Dim strquery, Foldername, Filename As String
                Dim objrs As SAPbobsCOM.Recordset
                Dim objrsupdate As SAPbobsCOM.Recordset
                Dim Mailbody, ServerName, CompanyDb, DBUserName, DbPassword As String
                Dim Payroll_Report_FileName = System.Windows.Forms.Application.StartupPath & "\" & "PaySlip_OEC.rpt"
                Dim initialpath As String = objaddon.objglobalmethods.getSingleValue("select ""AttachPath"" from OADP")
                Foldername = initialpath + "Payroll\RptFile"
                If Directory.Exists(Foldername) Then
                Else
                    Directory.CreateDirectory(Foldername)
                End If
                Filename = Foldername & "\PaySlip1.rpt"
                Filename = "E:\Chitra\Common Payroll\Dec 16\HRMS_Posting\HRMS_Posting\bin\x64\Debug\PaySlip_YMH.rpt"
                '"E:\Chitra\Common Payroll\Dec 16\BackUp Source Payroll\April 27 2022\TestReport.rpt"
                ServerName = "WATSON.TMICLOUD.NET:30013" '"WAT@WATSON.TMICLOUD.NET:30013"
                CompanyDb = "OEC_TEST" '"KANAKAVALLI_LIVE"
                DBUserName = "OECDBBR" '"KANASA" '"OECDBBR"
                DbPassword = "India@1947" '"R&s$!a#f%ru$456" ' "India@1947"

                FromMail_id = "saptech18@mukeshinfoserve.com"
                FromMail_Password = "tyntwdjfwlztgrns"
                Mail_Host = "smtp-mail.outlook.com"
                Mail_Port = "587"
                If FromMail_id = "" Or FromMail_Password = "" Or Mail_Host = "" Or Mail_Port = "" Then Exit Sub
                'MsgBox(Payroll_Report_FileName)   
                Dim cryRpt As New ReportDocument
                cryRpt.Load(Filename)
                cryRpt.DataSourceConnections(0).SetConnection(ServerName, CompanyDb, False) 'objaddon.objcompany.CompanyDB
                cryRpt.DataSourceConnections(0).SetLogon(DBUserName, DbPassword)


                'For i As Integer = 0 To objrs.RecordCount - 1
                'If objrs.Fields.Item("ToEmail").Value.ToString = "" Then Continue For

                Dim Email As New System.Net.Mail.MailMessage
                Dim MailServer As New System.Net.Mail.SmtpClient()

                Try
                    MailServer.Host = Mail_Host
                    MailServer.Port = Mail_Port
                    MailServer.Credentials = New System.Net.NetworkCredential(FromMail_id.ToString.Trim, FromMail_Password.ToString.Trim)
                    MailServer.EnableSsl = True
                    Email.From = New System.Net.Mail.MailAddress(FromMail_id.ToString.Trim)

                    Email.To.Add(New System.Net.Mail.MailAddress("saptech18@mukeshinfoserve.com"))
                    Email.Subject = "Pay Slip - " ' & objrs.Fields.Item("ToName").Value.ToString & " - " & objrs.Fields.Item("MonthName").Value.ToString & " - " & objrs.Fields.Item("Year").Value.ToString

                    Mailbody = "Dear Chitra, " '& objrs.Fields.Item("ToName").Value.ToString & ","
                    Mailbody += vbCrLf + " "
                    Mailbody += vbCrLf + " Please Find the Attached Payslip for the Month of " '& objrs.Fields.Item("Period").Value.ToString & "."
                    Mailbody += vbCrLf + " "
                    Mailbody += vbCrLf + "With Regards,"
                    Mailbody += vbCrLf + "HR Team"
                    Mailbody += vbCrLf + " "
                    Mailbody += vbCrLf + " "
                    Mailbody += "This is Auto generated E-Mail from SAP Business One . Please do not reply to this message. Thank you! "

                    Email.Body = Mailbody
                    Email.Priority = Net.Mail.MailPriority.High

                    'cryRpt.SetParameterValue("Emp@select empid,FIRSTNAME+'  '+LASTNAME from ohem order by Firstname", objrs.Fields.Item("Empid").Value.ToString)
                    'cryRpt.SetParameterValue("Month", objrs.Fields.Item("Month").Value.ToString)
                    'cryRpt.SetParameterValue("year@select distinct year(T0.u_todate) year from [@SMPR_OPRC] T0", objrs.Fields.Item("Year").Value.ToString)
                    'cryRpt.SetParameterValue("OTTA", "N")

                    cryRpt.SetParameterValue("Month", "MARCH") '"MARCH"
                    cryRpt.SetParameterValue("Year@select year(current_date) from dummy union all select year(current_date)-1 from dummy union all select year(current_date)-2 from dummy", 2022) '2022 Convert.ToInt32(objrs.Fields.Item("Year").Value.ToString)
                    cryRpt.SetParameterValue("Emp@select Distinct T1.""U_IDNo"",T1.""U_empName"" from ""@SMPR_PRC1"" T1 where ifnull(T1.""U_IDNo"",'')<>''", "EMP/KVL/00007") ' "EMP/KVL/00007" CStr(Trim(objrs.Fields.Item("U_ExtEmpNo").Value.ToString))

                    Email.Attachments.Add(New Attachment(cryRpt.ExportToStream(ExportFormatType.PortableDocFormat), "Pay Slip - .PDF"))

                    MailServer.Send(Email)



                Catch ex As Exception
                Finally
                    If Not Email Is Nothing Then Email.Dispose()
                    MailServer = Nothing
                End Try

                'objrs.MoveNext()
                'Next

            Catch ex As Exception
                MsgBox(ex.Message.ToString)
            End Try

        End Sub

        Private Sub Button3_ClickBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button3.ClickBefore
            Try
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE Then BubbleEvent = False : Exit Sub
                If Button3.Item.Enabled = False Then BubbleEvent = False : Exit Sub 'objform.Mode <> SAPbouiCOM.BoFormMode.fm_ADD_MODE
                'If CheckBox0.Checked = False Then objaddon.objapplication.StatusBar.SetText("Finalize the Payroll Entry..!!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error) : BubbleEvent = False : Exit Sub
                If Matrix0.VisualRowCount = 0 Then BubbleEvent = False : objaddon.objapplication.StatusBar.SetText("Row Data is Missing..!!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error) : Exit Sub
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Button3_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button3.ClickAfter
            Try
                If objaddon.objapplication.MessageBox("Do you want to adjust the loan deduction?", 2, "Yes", "No") <> 1 Then Exit Sub
                Dim flag As Boolean = False
                objform.Freeze(True)
                'Matrix0.LoadFromDataSource()
                For i As Integer = 0 To odbdsDetails.Size - 1
                    If odbdsDetails.GetValue("U_GrossAmt", i) = 0 And odbdsDetails.GetValue("U_NetAmt", i) < 0 Then
                        odbdsDetails.SetValue("U_Adjloan", i, "Y")
                        odbdsDetails.SetValue("U_NetAmt", i, "0")
                        odbdsDetails.SetValue("U_FD1", i, "0")
                        odbdsDetails.SetValue("U_Deduction", i, "0")
                        flag = True
                    End If
                Next
                Matrix0.LoadFromDataSource()
                If flag = True Then
                    If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE
                    objaddon.objapplication.StatusBar.SetText("Loan deduction adjusted successfully!!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                Else
                    objaddon.objapplication.StatusBar.SetText("No data found to adjust the loan!!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
                End If
                objform.Freeze(False)
            Catch ex As Exception
                objform.Freeze(False)
            End Try

        End Sub

        Public Sub Payslip_AutoEmail_Updated()
            Try
                Dim cryRpt As New ReportDocument
                Dim FromMail_id As String = "", FromMail_Password As String = "", Mail_Host As String = "", Mail_Port As String = ""
                Dim strquery As String ', Foldername, Filename 
                Dim objrs As SAPbobsCOM.Recordset
                Dim objrsupdate As SAPbobsCOM.Recordset
                Dim Mailbody, ServerName, CompanyDb, DBUserName, DbPassword As String
                Dim Payroll_Report_FileName = System.Windows.Forms.Application.StartupPath & "\" & "PaySlip_YMH.rpt" 'PaySlip_YMH
                Payroll_Report_FileName = "E:\Chitra\Common Payroll\Dec 16\HRMS_Posting\PaySlip.rpt"
                Payroll_Report_FileName = "E:\Chitra\Common Payroll\Dec 16\HRMS_Posting\HRMS_Posting\bin\Debug\PaySlip_Ymh_Oec.rpt"


                ServerName = "WATSON.TMICLOUD.NET:30015" ' Getvalue_webconfig("SAPServername") ' "WATSON.TMICLOUD.NET:30013" '"WAT@WATSON.TMICLOUD.NET:30013"
                CompanyDb = "OEC_TEST" 'Getvalue_webconfig("database") '"KANAKAVALLI_DB"
                DBUserName = "OECDBBR" 'Getvalue_webconfig("SQLUserName") '"KANASA"
                DbPassword = "India@1947" 'Getvalue_webconfig("SQLPassword") ' "R&s$!a#f%ru$456"
                'strsql = " Select T0.DocEntry,Datepart(MM,T0.U_fromdate)[Month],Datepart(yyyy,T0.U_Fromdate)[Year],DateName(Month,T0.U_fromdate)+' - '+Convert(varchar,Datepart(yyyy,T0.U_Fromdate))[Period],"
                'strsql += vbCrLf + " T2.U_empid[Empid],T2.U_ExtEmpNo,isnull(T2.U_firstNam,'')+' '+isnull(T2.U_lastName,'')[ToName],isnull(T2.U_Email,'')[ToEmail],'N'[OTTA]"
                'strsql += vbCrLf + " from [@SMPR_OPRC] T0 inner join [@SMPR_PRC1] T1 on T0.DOcentry=T1.DocEntry Inner join [@SMPR_OHEM] T2 on T1.U_Empid=T2.U_empid"
                'strsql += vbCrLf + " Where T0.U_Fromdate=(Select Max(U_Fromdate) from [@SMPR_OPRC] Where U_process='Y') and isnull(T2.U_payslip,'')='Y' and isnull(T2.U_Email,'')<>''"
                'strsql += vbCrLf + " and isnull(T1.U_payslip,'N')='N' and isnull(T0.U_Apayslip,'N')='Y'"

                strquery = "Select T0.""DocEntry"",MONTHNAME(T0.""U_FromDate"") ""MonthName"", MONTH(T0.""U_FromDate"") AS ""Month"", YEAR(T0.""U_FromDate"") AS ""Year"", MONTH(T0.""U_FromDate"") || ' - ' || CAST(YEAR(T0.""U_FromDate"") AS varchar) AS ""Period"","
                strquery += vbCrLf + " T2.""U_empID"" AS ""Empid"", T2.""U_ExtEmpNo"", IFNULL(T2.""U_firstNam"", '') || ' ' || IFNULL(T2.""U_lastName"", '') AS ""ToName"", IFNULL(T2.""U_email"", '') AS ""ToEmail"","
                strquery += vbCrLf + " 'N' AS ""OTTA"" FROM ""@SMPR_OPRC"" T0 INNER JOIN ""@SMPR_PRC1"" T1 ON T0.""DocEntry"" = T1.""DocEntry"" INNER Join ""@SMPR_OHEM"" T2 ON T1.""U_empID"" = T2.""U_empID"" "
                strquery += vbCrLf + " WHERE T0.""U_FromDate"" = (SELECT MAX(""U_FromDate"") FROM ""@SMPR_OPRC"" WHERE ""U_Process"" = 'Y') AND IFNULL(T2.""U_PaySlip"", '') = 'Y' "
                strquery += vbCrLf + " And IFNULL(T2.""U_email"", '') <> '' AND IFNULL(T1.""U_PaySlip"", 'N') = 'N' AND IFNULL(T0.""U_APayslip"", 'N') = 'Y'"


                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strquery)
                If objrs.RecordCount = 0 Then Exit Sub
                FromMail_id = "chitra.k@altrockstech.com" 'Getvalue_webconfig("FromMail_Id") '"saptech18@mukeshinfoserve.com"
                FromMail_Password = "tkhdnbxccnmgzcld" ' Getvalue_webconfig("FromMail_Password") ' "D@rloo@30895"
                Mail_Host = "smtp-mail.outlook.com" 'Getvalue_webconfig("Mail_Host") '"smtp-mail.outlook.com"
                Mail_Port = "587" 'Getvalue_webconfig("Mail_Port") ' "587"
                If FromMail_id = "" Or FromMail_Password = "" Or Mail_Host = "" Or Mail_Port = "" Then Exit Sub
                'MsgBox(Payroll_Report_FileName)   

                cryRpt.Load(Payroll_Report_FileName)
                cryRpt.DataSourceConnections(0).SetConnection(Trim(ServerName), Trim(CompanyDb), False)
                cryRpt.DataSourceConnections(0).SetLogon(Trim(DBUserName), Trim(DbPassword))
                'cryRpt.SetDatabaseLogon(DBUserName, DbPassword)
                Try
                    cryRpt.Refresh()
                    cryRpt.VerifyDatabase()
                Catch ex As Exception
                End Try

                For i As Integer = 0 To objrs.RecordCount - 1
                    If objrs.Fields.Item("ToEmail").Value.ToString = "" Then Continue For

                    Dim Email As New System.Net.Mail.MailMessage
                    Dim MailServer As New System.Net.Mail.SmtpClient()

                    Try
                        MailServer.Host = Mail_Host
                        MailServer.Port = Mail_Port
                        MailServer.Credentials = New System.Net.NetworkCredential(FromMail_id.ToString.Trim, FromMail_Password.ToString.Trim)
                        MailServer.EnableSsl = True
                        Email.From = New System.Net.Mail.MailAddress(FromMail_id.ToString.Trim)

                        Email.To.Add(New System.Net.Mail.MailAddress(objrs.Fields.Item("ToEmail").Value.ToString))
                        Email.Subject = "Pay Slip - " & objrs.Fields.Item("ToName").Value.ToString & " - " & objrs.Fields.Item("MonthName").Value.ToString & " - " & objrs.Fields.Item("Year").Value.ToString

                        Mailbody = "Dear " & objrs.Fields.Item("ToName").Value.ToString & ","
                        Mailbody += vbCrLf + " "
                        Mailbody += vbCrLf + " Please Find the Attached Payslip for the Month of " & objrs.Fields.Item("MonthName").Value.ToString & " - " & objrs.Fields.Item("Year").Value.ToString & "."
                        Mailbody += vbCrLf + " "
                        Mailbody += vbCrLf + "With Regards,"
                        Mailbody += vbCrLf + "HR Team"
                        Mailbody += vbCrLf + " "
                        Mailbody += vbCrLf + " "
                        Mailbody += "This is Auto generated E-Mail from SAP Business One . Please do not reply to this message. Thank you! "

                        Email.Body = Mailbody
                        Email.Priority = Net.Mail.MailPriority.High

                        cryRpt.SetParameterValue("@DocKey", "1")
                        'cryRpt.SetParameterValue("Year@select year(current_date) from dummy union all select year(current_date)-1 from dummy union all select year(current_date)-2 from dummy", Convert.ToInt32(objrs.Fields.Item("Year").Value.ToString)) '2022 
                        'cryRpt.SetParameterValue("Month", CStr(objrs.Fields.Item("MonthName").Value.ToString)) '"MARCH"
                        'cryRpt.SetParameterValue("Emp@select Distinct T1.""U_IDNo"",T1.""U_empName"" from ""@SMPR_PRC1"" T1 where ifnull(T1.""U_IDNo"",'')<>''", CStr(Trim(objrs.Fields.Item("U_ExtEmpNo").Value.ToString))) ' "EMP/KVL/00007" CStr(Trim(objrs.Fields.Item("U_ExtEmpNo").Value.ToString))
                        'cryRpt.SetParameterValue("Emp@select Distinct T1.""U_empID"",T1.""U_empName"" from ""@MIPL_PPI1"" T1 where ifnull(T1.""U_empID"",'')<>''", CStr(Trim(objrs.Fields.Item("U_ExtEmpNo").Value.ToString))) ' "EMP/KVL/00007" CStr(Trim(objrs.Fields.Item("U_ExtEmpNo").Value.ToString))

                        Email.Attachments.Add(New Attachment(cryRpt.ExportToStream(ExportFormatType.PortableDocFormat), "Pay Slip - .PDF"))


                        'cryRpt.Dispose()
                        'cryRpt.Refresh()
                        MailServer.Send(Email)
                        Dim strsql As String
                        strsql = "Update ""@SMPR_PRC1"" set ""U_PaySlip""='Y' where ""DocEntry""='" & objrs.Fields.Item("DocEntry").Value.ToString & "' and ""U_empID""='" & objrs.Fields.Item("Empid").Value.ToString & "'"
                        objrsupdate = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                        objrsupdate.DoQuery(strsql)

                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    Finally
                        If Not Email Is Nothing Then Email.Dispose()
                        MailServer = Nothing
                    End Try
                    objrs.MoveNext()
                Next
                'MsgBox("Mail Sent")
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End Sub

        Private WithEvents Button3 As SAPbouiCOM.Button
    End Class
End Namespace
