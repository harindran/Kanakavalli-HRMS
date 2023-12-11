Imports SAPbouiCOM.Framework
Imports System.IO

Namespace HRMS
    Public Class clsAddon
        Public WithEvents objapplication As SAPbouiCOM.Application
        Public objcompany As SAPbobsCOM.Company
        Dim objmenuevent As clsMenuEvent
        Dim objrightclickevent As clsRightClickEvent
        Public objglobalmethods As clsGlobalMethods
        Dim objform As SAPbouiCOM.Form
        Dim strsql As String = ""
        Dim objrs As SAPbobsCOM.Recordset
        Dim print_close As Boolean = False
        Public HANA As Boolean = True

        Public HWKEY() As String = New String() {"R1574408489", "D0872452844", "E0892770025", "L1653539483", "V0913316776", "M0090876837", "A0061802481", "F0637636550"}

        Public Sub Intialize(ByVal args() As String)
            Try
                Dim oapplication As Application
                If (args.Length < 1) Then oapplication = New Application Else oapplication = New Application(args(0))
                objapplication = Application.SBO_Application
                If isValidLicense() Then
                    objapplication.StatusBar.SetText("Establishing Company Connection Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Long, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
                    objcompany = Application.SBO_Application.Company.GetDICompany()
                    Create_Objects() 'Object Creation Part
                    Create_DatabaseFields() 'UDF & UDO Creation Part

                    'LicInf.GetHardwareKey(HKEY)
                    GetPayrollEnabledIndia = objglobalmethods.getSingleValue("Select CASE WHEN IFNULL(""U_PayInd"", '') = ('Y') THEN True ELSE False END as ""Status"" from OADM")
                    Menu() 'Menu Creation Part
                    MultiBranch = objaddon.objglobalmethods.getSingleValue("select ""MltpBrnchs"" from OADM")
                    ApprovedUser_Employee = ApprovedUser()
                    If GetPayrollEnabledIndia = True Then
                        objapplication.StatusBar.SetText("Payroll Addon Connected Successfully..!!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                    Else
                        objapplication.StatusBar.SetText("HRMS Addon Connected Successfully..!!!", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                    End If
                    oapplication.Run()
                Else
                    objapplication.StatusBar.SetText("Addon Disconnected due to license mismatch..!!!", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                    Exit Sub
                End If
                'System.Windows.Forms.Application.Run()
            Catch ex As Exception
                objaddon.objapplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)
            End Try
        End Sub

        Function isValidLicense() As Boolean
            Try
                Try
                    If objapplication.Forms.ActiveForm.TypeCount > 0 Then
                        For i As Integer = 0 To objapplication.Forms.ActiveForm.TypeCount - 1
                            objapplication.Forms.ActiveForm.Close()
                        Next
                    End If
                Catch ex As Exception
                End Try

                objapplication.Menus.Item("257").Activate()
                Dim CrrHWKEY As String = objapplication.Forms.ActiveForm.Items.Item("79").Specific.Value.ToString.Trim
                objapplication.Forms.ActiveForm.Close()

                For i As Integer = 0 To HWKEY.Length - 1
                    If HWKEY(i).Trim = CrrHWKEY.Trim Then
                        Return True
                    End If
                Next
                MsgBox("Installing Add-On failed due to License mismatch", MsgBoxStyle.OkOnly, "License Management")
                Return False
            Catch ex As Exception
                objaddon.objapplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                'MsgBox(ex.Message)
            End Try
            Return True
        End Function

        'Function isValidLicense() As Boolean
        '    Try
        '        objApplication.Menus.Item("257").Activate()
        '        Dim CrrHWKEY As String = objApplication.Forms.ActiveForm.Items.Item("79").Specific.Value.ToString.Trim
        '        objApplication.Forms.ActiveForm.Close()

        '        For i As Integer = 0 To HWKEY.Length - 1
        '            If HWKEY(i).Trim = CrrHWKEY.Trim Then
        '                Return True
        '            End If
        '        Next
        '        MsgBox("Installing Add-On failed due to License mismatch", MsgBoxStyle.OkOnly, "License Management")
        '        Return False
        '    Catch ex As Exception
        '        MsgBox(ex.Message)
        '    End Try
        '    Return True
        'End Function
        Public Function ApprovedUser()

            'If objapplication.Company.UserName.ToString.ToUpper <> "MANAGER" Then Return True

            'strsql = "Select U_approved from [@smpr_OHEM] T0 inner join OUSR  T1 on T0.U_userid=T1.USERID  where USER_CODE='" & objapplication.Company.UserName & "'"
            strsql = "Select ""U_approved"" from ""@SMPR_OHEM"" T0 inner join OUSR  T1 on T0.""U_userid""=T1.""USERID""  where ""USER_CODE""='" & objapplication.Company.UserName & "' and ifnull(""U_approved"",'')='Y'"
            objrs = objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            objrs.DoQuery(strsql)
            If objrs.RecordCount > 0 Then If objrs.Fields.Item("U_approved").Value.ToString.ToUpper = "Y" Then Return True

            Return False

        End Function

        Private Sub Create_Objects()
            objmenuevent = New clsMenuEvent
            objrightclickevent = New clsRightClickEvent
            objglobalmethods = New clsGlobalMethods
        End Sub

        Private Sub Create_DatabaseFields()
            'If objapplication.Company.UserName.ToString.ToUpper <> "MANAGER" Then

            'If objapplication.MessageBox("Do you want to execute the field Creations?", 2, "Yes", "No") <> 1 Then Exit Sub
            objapplication.StatusBar.SetText("Creating Database Fields.Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Long, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
            Dim objtable As New clsTable
            objtable.HRMS_FieldCreation()
            'End If

        End Sub

#Region "Menu Creation Details"

        Private Sub Menu()
            Dim Menucount As Integer = 0
            'GetPayrollEnabled = objglobalmethods.getSingleValue("Select CASE WHEN IFNULL(""U_PayInd"", '') = ('Y') THEN True ELSE False END as ""Status"" from OADM")
            If GetPayrollEnabledIndia = True Then
                CreateMenu("", Menucount, "Payroll", SAPbouiCOM.BoMenuType.mt_POPUP, "HRMS", "43544") : Menucount += 1
                Menucount = 1 'Menu Inside Customized HRMS Main Folder
                CreateMenu("", Menucount, "Master", SAPbouiCOM.BoMenuType.mt_POPUP, "MSTR", "HRMS") : Menucount += 1
                CreateMenu("", Menucount, "Transcation", SAPbouiCOM.BoMenuType.mt_POPUP, "TRNS", "HRMS") : Menucount += 1
                Menucount = 1 'Menu Inside Master Folder
                CreateMenu("", Menucount, "ID Card Type Master", SAPbouiCOM.BoMenuType.mt_STRING, "IDMS", "MSTR") : Menucount += 1
                CreateMenu("", Menucount, "Skill Master", SAPbouiCOM.BoMenuType.mt_STRING, "SKMS", "MSTR") : Menucount += 1
                CreateMenu("", Menucount, "Pay Element", SAPbouiCOM.BoMenuType.mt_STRING, "PAEL", "MSTR") : Menucount += 1
                CreateMenu("", Menucount, "Leave Master", SAPbouiCOM.BoMenuType.mt_STRING, "LAVE", "MSTR") : Menucount += 1
                CreateMenu("", Menucount, "Shift Master", SAPbouiCOM.BoMenuType.mt_STRING, "SHFT", "MSTR") : Menucount += 1
                CreateMenu("", Menucount, "Employee Master", SAPbouiCOM.BoMenuType.mt_STRING, "EMPM", "MSTR") : Menucount += 1
                'CreateMenu("", Menucount, "Calendar Master", SAPbouiCOM.BoMenuType.mt_STRING, "OCAL", "MSTR") : Menucount += 1
                'CreateMenu("", Menucount, "GL Master", SAPbouiCOM.BoMenuType.mt_STRING, "OGLM", "MSTR") : Menucount += 1
                CreateMenu("", Menucount, "Deduction Master", SAPbouiCOM.BoMenuType.mt_STRING, "MIPT", "MSTR") : Menucount += 1
                CreateMenu("", Menucount, "Employee Leave Master", SAPbouiCOM.BoMenuType.mt_STRING, "MILM", "MSTR") : Menucount += 1
                'If objapplication.Company.UserName.ToString.ToUpper <> "MANAGER" Then
                'End If
                CreateMenu("", Menucount, "Account Mapping", SAPbouiCOM.BoMenuType.mt_STRING, "ACCT", "MSTR") : Menucount += 1
                Menucount = 1 'Menu Inside Transcation Folder
                CreateMenu("", Menucount, "Daily Attendance", SAPbouiCOM.BoMenuType.mt_STRING, "ODAS", "TRNS") : Menucount += 1
                CreateMenu("", Menucount, "Attendance Upload", SAPbouiCOM.BoMenuType.mt_STRING, "VATT", "TRNS") : Menucount += 1
                CreateMenu("", Menucount, "Payroll Calculation", SAPbouiCOM.BoMenuType.mt_STRING, "OPII", "TRNS") : Menucount += 1
            Else
                CreateMenu("", Menucount, "HRMS", SAPbouiCOM.BoMenuType.mt_POPUP, "HRMS", "43544") : Menucount += 1
                Menucount = 0 'Menu Inside Customized HRMS Main Folder
                CreateMenu("", Menucount, "Master", SAPbouiCOM.BoMenuType.mt_POPUP, "MSTR", "HRMS") : Menucount += 1
                CreateMenu("", Menucount, "Transcation", SAPbouiCOM.BoMenuType.mt_POPUP, "TRNS", "HRMS") : Menucount += 1
                Menucount = 0 'Menu Inside Master Folder
                CreateMenu("", Menucount, "ID Card Type Master", SAPbouiCOM.BoMenuType.mt_STRING, "IDMS", "MSTR") : Menucount += 1
                CreateMenu("", Menucount, "Grade Master", SAPbouiCOM.BoMenuType.mt_STRING, "GRMS", "MSTR") : Menucount += 1
                CreateMenu("", Menucount, "Skill Master", SAPbouiCOM.BoMenuType.mt_STRING, "SKMS", "MSTR") : Menucount += 1
                CreateMenu("", Menucount, "Loan Master", SAPbouiCOM.BoMenuType.mt_STRING, "LNMS", "MSTR") : Menucount += 1
                CreateMenu("", Menucount, "Pay Element", SAPbouiCOM.BoMenuType.mt_STRING, "PAEL", "MSTR") : Menucount += 1
                CreateMenu("", Menucount, "Leave Master", SAPbouiCOM.BoMenuType.mt_STRING, "LAVE", "MSTR") : Menucount += 1
                CreateMenu("", Menucount, "Shift Master", SAPbouiCOM.BoMenuType.mt_STRING, "SHFT", "MSTR") : Menucount += 1
                CreateMenu("", Menucount, "Employee Master", SAPbouiCOM.BoMenuType.mt_STRING, "EMPM", "MSTR") : Menucount += 1
                CreateMenu("", Menucount, "Account Mapping", SAPbouiCOM.BoMenuType.mt_STRING, "ACCT", "MSTR") : Menucount += 1
                Menucount = 0 'Menu Inside Transcation Folder
                CreateMenu("", Menucount, "Leave Application", SAPbouiCOM.BoMenuType.mt_STRING, "OLVA", "TRNS") : Menucount += 1
                CreateMenu("", Menucount, "Loan Application", SAPbouiCOM.BoMenuType.mt_STRING, "OLOA", "TRNS") : Menucount += 1
                CreateMenu("", Menucount, "Daily Attendance", SAPbouiCOM.BoMenuType.mt_STRING, "ODAS", "TRNS") : Menucount += 1
                CreateMenu("", Menucount, "Payroll Addition/Deduction", SAPbouiCOM.BoMenuType.mt_STRING, "OPAD", "TRNS") : Menucount += 1
                CreateMenu("", Menucount, "Air Ticket Claim", SAPbouiCOM.BoMenuType.mt_STRING, "OTIS", "TRNS") : Menucount += 1
                CreateMenu("", Menucount, "Settlement", SAPbouiCOM.BoMenuType.mt_STRING, "SETL", "TRNS") : Menucount += 1
                CreateMenu("", Menucount, "Payroll Process", SAPbouiCOM.BoMenuType.mt_STRING, "OPRC", "TRNS") : Menucount += 1
                CreateMenu("", Menucount, "Provision Accurals", SAPbouiCOM.BoMenuType.mt_STRING, "PROV", "TRNS") : Menucount += 1
            End If


        End Sub

        Private Sub CreateMenu(ByVal ImagePath As String, ByVal Position As Int32, ByVal DisplayName As String, ByVal MenuType As SAPbouiCOM.BoMenuType, ByVal UniqueID As String, ByVal ParentMenuID As String)
            Try
                Dim oMenuPackage As SAPbouiCOM.MenuCreationParams
                Dim parentmenu As SAPbouiCOM.MenuItem
                parentmenu = objapplication.Menus.Item(ParentMenuID)
                If parentmenu.SubMenus.Exists(UniqueID.ToString) Then parentmenu.SubMenus.RemoveEx(UniqueID.ToString)
                'Exit Sub
                oMenuPackage = objapplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)
                oMenuPackage.Image = ImagePath
                oMenuPackage.Position = Position
                oMenuPackage.Type = MenuType
                oMenuPackage.UniqueID = UniqueID
                oMenuPackage.String = DisplayName
                parentmenu.SubMenus.AddEx(oMenuPackage)

            Catch ex As Exception
                objapplication.StatusBar.SetText("Menu Already Exists", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_None)
            End Try
            'Return ParentMenu.SubMenus.Item(UniqueID)
        End Sub

#End Region

#Region "ItemEvent_Link Button"

        Private Sub objapplication_ItemEvent(FormUID As String, ByRef pVal As SAPbouiCOM.ItemEvent, ByRef BubbleEvent As Boolean) Handles objapplication.ItemEvent
            Try
                If pVal.BeforeAction Then
                    Select Case pVal.EventType
                        Case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED
                            If pVal.FormTypeEx = "STA" And pVal.ItemUID = "10" Then 'Approval Screen Link Pressed
                                AppovalLink_ApprovalScreen(FormUID, pVal, BubbleEvent)
                            End If
                    End Select
                Else
                    Select Case pVal.EventType
                        Case SAPbouiCOM.BoEventTypes.et_FORM_ACTIVATE
                            If pVal.FormTypeEx = "410000100" And pVal.BeforeAction = False Then
                                Try
                                    Dim oform = objaddon.objapplication.Forms.ActiveForm
                                    If oform.Title.Contains("Leave Application") Or oform.Title.Contains("Loan Application") Or oform.Title.Contains("Settlement") Then
                                        If print_close = True Then oform.Close() : print_close = False
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                    End Select
                End If

            Catch ex As Exception

            End Try
        End Sub

        Private Sub AppovalLink_ApprovalScreen(FormUID As String, ByRef pVal As SAPbouiCOM.ItemEvent, ByRef BubbleEvent As Boolean)
            Try
                objform = objaddon.objapplication.Forms.ActiveForm
                Select Case objform.Items.Item("DocObj").Specific.string.ToString.ToUpper
                    Case "OLVA"
                        Link_objtype = objform.Items.Item("DocObj").Specific.string
                        Link_Value = objform.Items.Item("t_Entry").Specific.string
                        Dim activeform As New frmLeaveApplicaiton
                        activeform.Show()
                    Case "OLOA"
                        Link_objtype = objform.Items.Item("DocObj").Specific.string
                        Link_Value = objform.Items.Item("t_Entry").Specific.string
                        Dim activeform As New frmLoanApplication
                        activeform.Show()
                    Case "OLSE"
                        Link_objtype = objform.Items.Item("DocObj").Specific.string
                        Link_Value = objform.Items.Item("t_Entry").Specific.string
                        Dim activeform As New FrmSettlment
                        activeform.Show()
                    Case "OTIS"
                        Link_objtype = objform.Items.Item("DocObj").Specific.string
                        Link_Value = objform.Items.Item("t_Entry").Specific.string
                        Dim activeform As New frmAirTicketIssue
                        activeform.Show()
                End Select
            Catch ex As Exception

            End Try
        End Sub

#End Region

#Region "Menu Event"

        Public Sub SBO_Application_MenuEvent(ByRef pVal As SAPbouiCOM.MenuEvent, ByRef BubbleEvent As Boolean) Handles objapplication.MenuEvent
            Try
                If pVal.BeforeAction Then
                    Select Case pVal.MenuUID
                        Case "1283"
                            If objaddon.objapplication.MessageBox("Removal of an entry cannot be reversed.Do you want to Continue?", 1, "Yes", "No") <> 1 Then BubbleEvent = False : Exit Sub
                    End Select
                End If
                Select Case pVal.MenuUID
                    Case "1281", "1282", "1285", "1284", "1286", "1287", "1288", "1289", "1290", "1291", "1304", "1292", "1293", "NXTM", "NEWM", "CPY", "HD", "DROW", "AROW", "SP", "OSLE", "FHD", "SHD", "ELV", "ELN", "EAI", "EST", "PJE", "PRJE"
                        objmenuevent.MenuEvent_For_StandardMenu(pVal, BubbleEvent)
                    Case "LNMS", "PAEL", "IDMS", "LAVE", "SHFT", "EMPM", "OLVA", "OLOA", "ODAS", "OGLM", "OPAD", "OTIS", "SETL", "OPRC", "ACCT", "PROV", "OPII", "EPSS", "MIPT", "MIPF", "MIESI", "ATUP", "OCAL", "VATT", "GRMS", "SKMS", "MILM"
                        MenuEvent_For_FormOpening(pVal, BubbleEvent)
                        'Case "1293"
                        '    BubbleEvent = False
                    Case "519"
                        MenuEvent_For_Preview(pVal, BubbleEvent)
                End Select
            Catch ex As Exception
                objaddon.objapplication.SetStatusBarMessage("Error in SBO_Application MenuEvent" + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, True)
            End Try
        End Sub

        Public Sub MenuEvent_For_Preview(ByRef pval As SAPbouiCOM.MenuEvent, ByRef bubbleevent As Boolean)
            Dim oform = objaddon.objapplication.Forms.ActiveForm()
            If pval.BeforeAction Then
                If oform.TypeEx = "TRANOLVA" Then MenuEvent_For_PrintPreview(oform, "8f481d5cf08e494f9a83e1e46ab2299e", "txtentry") : bubbleevent = False
                If oform.TypeEx = "TRANOLAP" Then MenuEvent_For_PrintPreview(oform, "f15ee526ac514070a9d546cda7f94daf", "txtentry") : bubbleevent = False
                If oform.TypeEx = "OLSE" Then MenuEvent_For_PrintPreview(oform, "e47ed373e0cc48efb47c9773fba64fc3", "txtentry") : bubbleevent = False
            End If
        End Sub

        Private Sub MenuEvent_For_PrintPreview(ByVal oform As SAPbouiCOM.Form, ByVal Menuid As String, ByVal Docentry_field As String)
            Try
                Dim Docentry_Est As String = oform.Items.Item(Docentry_field).Specific.String
                If Docentry_Est = "" Then Exit Sub
                print_close = False
                objaddon.objapplication.Menus.Item(Menuid).Activate()
                oform = objaddon.objapplication.Forms.ActiveForm()
                oform.Items.Item("1000003").Specific.string = Docentry_Est
                oform.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                print_close = True
            Catch ex As Exception
            End Try
        End Sub

        Public Sub MenuEvent_For_FormOpening(ByRef pVal As SAPbouiCOM.MenuEvent, ByRef BubbleEvent As Boolean)
            Try
                If pVal.BeforeAction = False Then
                    Select Case pVal.MenuUID
                        Case "LNMS"
                            Dim activeform As New frmLoanMaster
                            activeform.Show()
                        Case "PAEL"
                            Dim activeform As New frmPayElement
                            activeform.Show()
                        Case "IDMS"
                            Dim activeform As New frmIDCardMaster
                            activeform.Show()
                        Case "GRMS"
                            Dim activeform As New FrmGradeMaster
                            activeform.Show()
                        Case "SKMS"
                            Dim activeform As New FrmSkillMaster
                            activeform.Show()
                        Case "LAVE"
                            Dim activeform As New frmLeaveMaster
                            activeform.Show()
                        Case "SHFT"
                            Dim activeform As New frmShiftMaster
                            activeform.Show()
                            'Dim activeform1 As New TESTFORM
                            'activeform1.Show()
                        Case "EMPM"
                            Dim activeform As New frmEmployeeMaster
                            activeform.Show()
                        Case "OLVA"
                            Dim activeform As New frmLeaveApplicaiton
                            activeform.Show()
                        Case "OLOA"
                            Dim activeform As New frmLoanApplication
                            activeform.Show()
                        Case "ODAS"
                            Dim activeform As New frmDailyAttendance
                            activeform.Show()
                        Case "OPAD"
                            Dim activeform As New frmAdditionDeduction
                            activeform.Show()
                        Case "OTIS"
                            Dim activeform As New frmAirTicketIssue
                            activeform.Show()
                        Case "SETL"
                            Dim activeform As New FrmSettlment
                            activeform.Show()
                        Case "OPRC"
                            Try
                                Dim activeform As New frmPayrollProcess
                                activeform.Show()
                            Catch ex As Exception

                            End Try
                        Case "ACCT"
                            Dim activeform As New frmAccountMapping
                            activeform.Show()
                        Case "OGLM"
                            Dim activeform As New FrmGLMapping
                            activeform.Show()
                        Case "PROV"
                            Dim activeform As New frmProvision
                            activeform.Show()
                        Case "OPII"
                            If Not FormExist("OPII") Then
                                Dim activeform As New FrmPayrollProcessInINDIA
                                activeform.Show()
                            End If
                            'If Not FormExist("OPAY") Then
                            '    Dim activeform1 As New FrmPayrollCalculation
                            '    activeform1.Show()
                            'End If
                        Case "MILM"
                            Dim activeform As New FrmEmpLeaveMaster
                            activeform.Show()
                        Case "MIPT"
                            Dim activeform As New ProfTax_DeductMaster
                            activeform.Show()
                        Case "MIESI"
                            Dim activeform As New ESI_DeductMaster
                            activeform.Show()
                        Case "ATUP"
                            'Dim activeform As New AttenUpload
                            'activeform.Show()
                        Case "OCAL"
                            Dim activeform As New CalendarMaster
                            activeform.Show()
                        Case "VATT"
                            Dim activeform As New ViewAttendance
                            activeform.Show()
                    End Select

                End If
            Catch ex As Exception
                'objaddon.objapplication.SetStatusBarMessage("Error in Form Opening MenuEvent" + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, True)
            End Try
        End Sub

        Public Function FormExist(ByVal FormID As String) As Boolean
            Try
                FormExist = False
                For Each uid As SAPbouiCOM.Form In objaddon.objapplication.Forms
                    If uid.UniqueID = FormID Then
                        FormExist = True
                        Exit For
                    End If
                Next
                If FormExist Then
                    objaddon.objapplication.Forms.Item(FormID).Visible = True
                    objaddon.objapplication.Forms.Item(FormID).Select()
                End If
            Catch ex As Exception

            End Try

        End Function
#End Region

#Region "LayoutKeyEvent"

        Public Sub SBO_Application_LayoutKeyEvent(ByRef eventInfo As SAPbouiCOM.LayoutKeyInfo, ByRef BubbleEvent As Boolean) Handles objapplication.LayoutKeyEvent
            'Dim oForm_Layout As SAPbouiCOM.Form = Nothing
            'If SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.BusinessObject.Type = "NJT_CES" Then
            '    oForm_Layout = SAPbouiCOM.Framework.Application.SBO_Application.Forms.Item(eventInfo.FormUID)
            'End If
        End Sub

#End Region

#Region "Application Event"

        Public Sub SBO_Application_AppEvent(EventType As SAPbouiCOM.BoAppEventTypes) Handles objapplication.AppEvent
            Select Case EventType
                Case SAPbouiCOM.BoAppEventTypes.aet_ShutDown, SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged, SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition
                    DisConnect_Addon("HRMS")
                    'Try
                    '    If objcompany.Connected Then objcompany.Disconnect()
                    '    System.Runtime.InteropServices.Marshal.ReleaseComObject(objcompany)
                    '    System.Runtime.InteropServices.Marshal.ReleaseComObject(objapplication)
                    '    objcompany = Nothing
                    '    'objapplication = Nothing
                    '    GC.Collect()
                    '    End
                    'Catch ex As Exception
                    'End Try
                    'Case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged
                    '    End
                    'Case SAPbouiCOM.BoAppEventTypes.aet_FontChanged
                    '    End
                    'Case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged
                    '    End
                    'Case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition
                    '    End
            End Select
        End Sub

        Private Sub DisConnect_Addon(ByVal MenuID As String)
            Try
                If objaddon.objapplication.Forms.Count > 0 Then
                    Try
                        For frm As Integer = objaddon.objapplication.Forms.Count - 1 To 0 Step -1
                            If objaddon.objapplication.Forms.Item(frm).IsSystem = True Then Continue For
                            objaddon.objapplication.Forms.Item(frm).Close()
                        Next
                    Catch ex As Exception
                    End Try

                    If objapplication.Menus.Item("43520").SubMenus.Exists(MenuID) Then objapplication.Menus.Item("43520").SubMenus.RemoveEx(MenuID)
                End If
                If objcompany.Connected Then objcompany.Disconnect()
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objcompany)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objapplication)
                objcompany = Nothing
                GC.Collect()
                System.Windows.Forms.Application.Exit()
                End
            Catch ex As Exception

            End Try
        End Sub
#End Region

#Region "Right Click Event"

        Private Sub objapplication_RightClickEvent(ByRef eventInfo As SAPbouiCOM.ContextMenuInfo, ByRef BubbleEvent As Boolean) Handles objapplication.RightClickEvent
            Try
                Select Case objaddon.objapplication.Forms.ActiveForm.TypeEx
                    Case "MSTREMPL", "TRANOLAP", "TRANOLVA", "ODAS", "OTIS", "OLSE", "OPAD", "OPRC", "PROV", "VATT", "MIPT", "ACCT", "OPPII"
                        objrightclickevent.RightClickEvent(eventInfo, BubbleEvent)
                End Select
            Catch ex As Exception

            End Try
        End Sub

#End Region



    End Class
End Namespace
