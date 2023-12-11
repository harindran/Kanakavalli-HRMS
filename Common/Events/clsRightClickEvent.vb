Namespace HRMS

    Public Class clsRightClickEvent
        Dim objform As SAPbouiCOM.Form
        Dim objglobalmethods As New clsGlobalMethods
        Dim ocombo As SAPbouiCOM.ComboBox
        Dim objmatrix As SAPbouiCOM.Matrix
        Dim strsql As String
        Dim objrs As SAPbobsCOM.Recordset

        Public Sub RightClickEvent(ByRef eventInfo As SAPbouiCOM.ContextMenuInfo, ByRef BubbleEvent As Boolean)
            Try
                Select Case objaddon.objapplication.Forms.ActiveForm.TypeEx
                    Case "TRANOLAP"
                        LoanApplication_RightClickEvent(eventInfo, BubbleEvent)
                    Case "TRANOLVA"
                        LeaveApplication_RightClickEvent(eventInfo, BubbleEvent)
                    Case "OTIS"
                        AirTicketIssue_RightClickEvent(eventInfo, BubbleEvent)
                    Case "OLSE"
                        Settlement_RightClickEvent(eventInfo, BubbleEvent)
                    Case "MSTREMPL"
                        EmployeeMaster_RightClickEvent(eventInfo, BubbleEvent)
                    Case "OPAD"
                        Addition_deuction_RightClickEvent(eventInfo, BubbleEvent)
                    Case "OPRC"
                        PayrollProcess_RightClickEvent(eventInfo, BubbleEvent)
                    Case "PROV"
                        ProvisionProcess_RightClickEvent(eventInfo, BubbleEvent)
                    Case "VATT"
                        ViewAttendance_RightClickEvent(eventInfo, BubbleEvent)
                    Case "ACCT"
                        AccountMapping_RightClickEvent(eventInfo, BubbleEvent)
                    Case "MIPT"
                        ProfTax_RightClickEvent(eventInfo, BubbleEvent)
                    Case "OPPII"
                        PayrollCalc_Indian_RightClickEvent(eventInfo, BubbleEvent)
                End Select
            Catch ex As Exception
            End Try
        End Sub

        Private Sub RightClickMenu_Add(ByVal MainMenu As String, ByVal NewMenuID As String, ByVal NewMenuName As String, ByVal position As Integer)
            Dim omenus As SAPbouiCOM.Menus
            Dim omenuitem As SAPbouiCOM.MenuItem
            Dim oCreationPackage As SAPbouiCOM.MenuCreationParams
            oCreationPackage = objaddon.objapplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)
            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING
            omenuitem = objaddon.objapplication.Menus.Item(MainMenu) 'Data'
            If Not omenuitem.SubMenus.Exists(NewMenuID) Then
                oCreationPackage.UniqueID = NewMenuID
                oCreationPackage.String = NewMenuName
                oCreationPackage.Position = position
                oCreationPackage.Enabled = True
                omenus = omenuitem.SubMenus
                omenus.AddEx(oCreationPackage)
            End If
        End Sub

        Private Sub RightClickMenu_Delete(ByVal MainMenu As String, ByVal NewMenuID As String)
            Dim omenuitem As SAPbouiCOM.MenuItem
            omenuitem = objaddon.objapplication.Menus.Item(MainMenu) 'Data'
            If omenuitem.SubMenus.Exists(NewMenuID) Then
                objaddon.objapplication.Menus.RemoveEx(NewMenuID)
            End If
        End Sub

#Region "Loan Application"

        Private Sub LoanApplication_RightClickEvent(ByRef eventInfo As SAPbouiCOM.ContextMenuInfo, ByRef BubbleEvent As Boolean)
            Try
                Dim objform As SAPbouiCOM.Form
                objform = objaddon.objapplication.Forms.ActiveForm
                If eventInfo.BeforeAction Then
                    If eventInfo.ItemUID = "mtloan" Then
                        If eventInfo.Row = 0 Then Exit Sub
                        objmatrix = objform.Items.Item("mtloan").Specific
                        ocombo = objmatrix.Columns.Item("status").Cells.Item(eventInfo.Row).Specific
                        If ocombo.Value.ToString = "" Then objform.EnableMenu("1293", True) 'Delete Row
                        If ocombo.Value.ToString.ToUpper <> "O" Then BubbleEvent = False : Exit Sub
                        ocombo = objform.Items.Item("cmbstatus").Specific
                        If ocombo.Value.ToString.ToUpper = "O" Or ocombo.Value.ToString.ToUpper = "R" Then
                            Current_Lineid = eventInfo.Row
                            If ApprovedUser_Employee Then
                                RightClickMenu_Add("1280", "NXTM", "Move To Next Month", 0)
                                RightClickMenu_Add("1280", "NEWM", "Move To New Month", 1)
                                'RightClickMenu_Add("1280", "CPY", "Cash Payment", 2)
                                objform.EnableMenu("1292", True) 'Add Row
                                objform.EnableMenu("1293", True) 'Delete Row
                            End If
                        End If
                    Else
                        If ApprovedUser_Employee Then
                            If objform.Items.Item("cmbstatus").Specific.Selected.Value = "O" And objform.Items.Item("chkapp").Specific.Checked = True Then objform.EnableMenu("1286", True) 'Close
                        Else
                            objform.EnableMenu("1286", False) 'Close
                        End If

                        If objform.Items.Item("txtempid").Specific.String <> "" Then RightClickMenu_Add("1280", "FHD", "Full History Details", 0)
                        If objform.Items.Item("txtempid").Specific.String <> "" And objform.Items.Item("txtlcode").Specific.String <> "" Then RightClickMenu_Add("1280", "SHD", "History Details - " + objform.Items.Item("txtlname").Specific.string, 1)
                    End If
                    If objform.Items.Item("chkapp").Specific.Checked = False And objform.Items.Item("chkcan").Specific.Checked = False Then
                        objform.EnableMenu("1283", True)  'Remove
                    End If
                Else
                    RightClickMenu_Delete("1280", "FHD")
                    RightClickMenu_Delete("1280", "SHD")
                    RightClickMenu_Delete("1280", "NXTM")
                    RightClickMenu_Delete("1280", "NEWM")
                    'RightClickMenu_Delete("1280", "CPY")
                    objform.EnableMenu("1292", False) 'Add Row
                    objform.EnableMenu("1293", False) 'Delete Row
                    objform.EnableMenu("1283", False)  'Remove
                    objform.EnableMenu("1286", False) 'Close
                End If
            Catch ex As Exception
            End Try
        End Sub

#End Region

#Region "Leave Application"

        Private Sub LeaveApplication_RightClickEvent(ByRef eventInfo As SAPbouiCOM.ContextMenuInfo, ByRef BubbleEvent As Boolean)
            Try
                Dim objform As SAPbouiCOM.Form
                Dim ocombo As SAPbouiCOM.ComboBox
                objform = objaddon.objapplication.Forms.ActiveForm
                If eventInfo.BeforeAction Then
                    If objform.Items.Item("txtempid").Specific.String <> "" Then RightClickMenu_Add("1280", "HD", "Full History Details", 0)
                    ocombo = objform.Items.Item("cmbstatus").Specific
                    If (ocombo.Selected.Value = "O" Or ocombo.Selected.Value = "R") And ApprovedUser_Employee And objform.Mode <> SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                        strsql = "select 1 from ""@SMPR_OLVA"" where ""U_empID""='" & objform.Items.Item("txtempid").Specific.String & "' and  ""DocEntry""='" & objform.Items.Item("txtentry").Specific.String & "' and ""U_FromDate""<=(select max(""U_ToDate"") from ""@SMPR_OPRC"" where IFNULL(""U_Process"",'')='Y')"
                        objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                        objrs.DoQuery(strsql)
                        If objrs.RecordCount > 0 Then
                            objform.EnableMenu("1284", False) 'Cancel Menu
                        Else
                            objform.EnableMenu("1284", True) 'Cancel Menu
                        End If
                        If objform.Items.Item("chkapp").Specific.Checked = False And objform.Items.Item("chkcan").Specific.Checked = False Then
                            objform.EnableMenu("1283", True)  'Remove
                        End If
                    Else
                        objform.EnableMenu("1283", False)
                        objform.EnableMenu("1284", False) 'Cancel Menu
                    End If

                Else
                    RightClickMenu_Delete("1280", "HD")
                    objform.EnableMenu("1284", False)
                    objform.EnableMenu("1283", False)
                End If
            Catch ex As Exception
            End Try
        End Sub

#End Region

#Region "Air Ticket Issue"

        Private Sub AirTicketIssue_RightClickEvent(ByRef eventInfo As SAPbouiCOM.ContextMenuInfo, ByRef BubbleEvent As Boolean)
            Try
                Dim objform As SAPbouiCOM.Form
                objform = objaddon.objapplication.Forms.ActiveForm
                If eventInfo.BeforeAction Then
                    If objform.Items.Item("txttrzid").Specific.String <> "" Then
                        RightClickMenu_Add("1280", "HD", "History Details", 0)
                        RightClickMenu_Add("1280", "SP", "Eligible Amount Details", 1)
                    End If
                Else
                    RightClickMenu_Delete("1280", "HD")
                    RightClickMenu_Delete("1280", "SP")
                End If
            Catch ex As Exception
            End Try
        End Sub

#End Region

#Region "Settlement"

        Private Sub Settlement_RightClickEvent(ByRef eventInfo As SAPbouiCOM.ContextMenuInfo, ByRef BubbleEvent As Boolean)
            Try
                Dim objform As SAPbouiCOM.Form
                Dim omatrix As SAPbouiCOM.Matrix
                objform = objaddon.objapplication.Forms.ActiveForm
                omatrix = objform.Items.Item("mtaddded").Specific
                If eventInfo.BeforeAction Then
                    If eventInfo.ItemUID = "mtaddded" And omatrix.VisualRowCount > 0 Then objform.EnableMenu("1293", True) 'Delete Row
                    objform.EnableMenu("1286", False)
                Else
                    objform.EnableMenu("1293", False) 'Delete Row
                    objform.EnableMenu("1286", False)
                End If
            Catch ex As Exception
            End Try
        End Sub

#End Region

        Private Sub ViewAttendance_RightClickEvent(ByRef eventInfo As SAPbouiCOM.ContextMenuInfo, ByRef BubbleEvent As Boolean)
            Try
                Dim objform As SAPbouiCOM.Form
                objform = objaddon.objapplication.Forms.ActiveForm
                objmatrix = objform.Items.Item("matattd").Specific
                If eventInfo.BeforeAction Then
                    objform.EnableMenu("1283", False)
                    objform.EnableMenu("1284", False)
                    objform.EnableMenu("8801", False)
                    If eventInfo.ItemUID = "matattd" Then
                        Current_Lineid = eventInfo.Row
                        If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                            If eventInfo.Row <> 0 Then
                                objform.EnableMenu("1293", True) 'Remove Row Menu
                            Else
                                objform.EnableMenu("1293", False)
                            End If
                        Else
                            objform.EnableMenu("1293", False)
                        End If
                        objform.EnableMenu("784", True)
                    Else
                        objform.EnableMenu("1293", False)
                        objform.EnableMenu("784", False)
                    End If
                Else

                    objform.EnableMenu("784", False)
                    'If objform.Mode <> SAPbouiCOM.BoFormMode.fm_UPDATE_MODE And objform.Mode <> SAPbouiCOM.BoFormMode.fm_OK_MODE Then
                    '    objform.EnableMenu("1293", True) 'Remove Row Menu
                    'Else
                    '    objform.EnableMenu("1293", False)
                    'End If
                    'objform.EnableMenu("1292", True) 'Add Row Menu

                    'objform.EnableMenu("1283", False)
                End If
            Catch ex As Exception
            End Try
        End Sub

        Private Sub ProfTax_RightClickEvent(ByRef eventInfo As SAPbouiCOM.ContextMenuInfo, ByRef BubbleEvent As Boolean)
            Try
                Dim objform As SAPbouiCOM.Form
                objform = objaddon.objapplication.Forms.ActiveForm
                If eventInfo.BeforeAction Then
                    objform.EnableMenu("1283", False)
                    objform.EnableMenu("1284", False)
                    If eventInfo.ItemUID = "mtxdata" And eventInfo.Row <> 0 Then
                        objform.EnableMenu("1293", True) 'Remove Row Menu
                        'If objform.Mode <> SAPbouiCOM.BoFormMode.fm_UPDATE_MODE And objform.Mode <> SAPbouiCOM.BoFormMode.fm_OK_MODE Then

                        'Else
                        '    objform.EnableMenu("1293", False)
                        'End If
                    ElseIf eventInfo.ItemUID = "mtxpfesi" And eventInfo.Row <> 0 Then
                        objform.EnableMenu("1293", True) 'Remove Row Menu
                    End If
                    objform.EnableMenu("784", True)
                Else
                    'If objform.Mode <> SAPbouiCOM.BoFormMode.fm_UPDATE_MODE And objform.Mode <> SAPbouiCOM.BoFormMode.fm_OK_MODE Then
                    '    objform.EnableMenu("1293", True) 'Remove Row Menu
                    'Else

                    'End If
                    objform.EnableMenu("1293", False)
                End If
            Catch ex As Exception
            End Try
        End Sub

        Private Sub PayrollCalc_Indian_RightClickEvent(ByRef eventInfo As SAPbouiCOM.ContextMenuInfo, ByRef BubbleEvent As Boolean)
            Try
                Dim objform As SAPbouiCOM.Form
                objform = objaddon.objapplication.Forms.ActiveForm
                If eventInfo.BeforeAction Then

                    objform.EnableMenu("1283", False)
                    objform.EnableMenu("1284", False)
                    If eventInfo.ItemUID = "mtxpayroll" Then
                        objform.EnableMenu("784", True)
                    End If
                    'If eventInfo.ItemUID <> "" And eventInfo.Row <> -1 Then
                    Try
                        If eventInfo.ItemUID = "" Then Exit Try
                        Try
                            If objform.Items.Item(eventInfo.ItemUID).Specific.String <> "" Then
                                objform.EnableMenu("772", True)  'Copy
                            ElseIf objform.Items.Item(eventInfo.ItemUID).Specific.String = "" Then
                                objform.EnableMenu("773", True)  'Paste
                            End If
                        Catch ex As Exception
                            If objform.Items.Item(eventInfo.ItemUID).Specific.Selected.Value <> "" Then
                                objform.EnableMenu("772", True)  'Copy
                            ElseIf objform.Items.Item(eventInfo.ItemUID).Selected.Value = "" Then
                                objform.EnableMenu("773", True)  'Paste
                            End If
                        End Try

                    Catch ex As Exception
                            objmatrix = objform.Items.Item(eventInfo.ItemUID).Specific
                            If eventInfo.Row <= 0 Then If objform.Mode <> SAPbouiCOM.BoFormMode.fm_FIND_MODE Then objform.EnableMenu("772", True) : objform.EnableMenu("784", True) : Exit Try
                            If objmatrix.Columns.Item(eventInfo.ColUID).Cells.Item(eventInfo.Row).Specific.String <> "" Then
                                objform.EnableMenu("772", True)  'Copy
                            ElseIf objmatrix.Columns.Item(eventInfo.ColUID).Cells.Item(eventInfo.Row).Specific.String = "" Then
                                objform.EnableMenu("773", True)  'Paste
                            End If
                        End Try
                    'Else
                    '    If eventInfo.ItemUID <> "" Then If objform.Items.Item(eventInfo.ItemUID).Specific.String <> "" Then objform.EnableMenu("772", True) Else objform.EnableMenu("772", False)
                    'End If
                Else
                    'objform.EnableMenu("1283", False)
                    objform.EnableMenu("772", False)
                End If
            Catch ex As Exception
            End Try
        End Sub

        Private Sub AccountMapping_RightClickEvent(ByRef eventInfo As SAPbouiCOM.ContextMenuInfo, ByRef BubbleEvent As Boolean)
            Try
                Dim objform As SAPbouiCOM.Form
                objform = objaddon.objapplication.Forms.ActiveForm
                If eventInfo.BeforeAction Then
                    If eventInfo.ItemUID = "mtad" And eventInfo.Row <> 0 Then
                        objform.EnableMenu("1293", True)
                        objform.EnableMenu("1292", True) 'Add Row Menu
                    ElseIf eventInfo.ItemUID = "mtpay" And eventInfo.Row <> 0 Then
                        objform.EnableMenu("1293", True)
                        objform.EnableMenu("1292", True) 'Add Row Menu
                    End If
                Else
                    objform.EnableMenu("1293", False) 'Delete Row Menu
                    objform.EnableMenu("1292", False) 'Add Row Menu
                    'objform.EnableMenu("1283", False)
                End If
            Catch ex As Exception
            End Try
        End Sub

#Region "Employee Master Data"

        Private Sub EmployeeMaster_RightClickEvent(ByRef eventInfo As SAPbouiCOM.ContextMenuInfo, ByRef BubbleEvent As Boolean)
            Try
                Dim objform As SAPbouiCOM.Form
                objform = objaddon.objapplication.Forms.ActiveForm
                If eventInfo.BeforeAction Then
                    objform.ActiveItem = eventInfo.ItemUID
                    If eventInfo.ItemUID = "mLeave" Or eventInfo.ItemUID = "mSalary" Or eventInfo.ItemUID = "mair" Or eventInfo.ItemUID = "mID" Or eventInfo.ItemUID = "mskill" Or eventInfo.ItemUID = "mtraining" Or eventInfo.ItemUID = "mfamily" Or eventInfo.ItemUID = "meducation" Or eventInfo.ItemUID = "mpreemp" Then
                        objform.EnableMenu("1292", True) 'Add Row Menu
                        objform.EnableMenu("1293", True) 'Remove Row Menu
                    End If
                    'If objform.Items.Item("txtiempid").Specific.String <> "" Then
                    '    RightClickMenu_Add("1280", "ELV", "Leave Details", 0)
                    '    RightClickMenu_Add("1280", "ELN", "Loan Details", 0)
                    '    RightClickMenu_Add("1280", "EAI", "Air Ticket Issue Details", 0)
                    '    RightClickMenu_Add("1280", "EST", "Settlement Details", 0)
                    'End If
                Else
                    objform.EnableMenu("1292", False) 'Add Row Menu
                    objform.EnableMenu("1293", False) 'Delete Row
                    RightClickMenu_Delete("1280", "ELV")
                    RightClickMenu_Delete("1280", "ELN")
                    RightClickMenu_Delete("1280", "EAI")
                    RightClickMenu_Delete("1280", "EST")
                End If
            Catch ex As Exception
            End Try
        End Sub

#End Region

#Region "Addition/Deduction"

        Private Sub Addition_deuction_RightClickEvent(ByRef eventInfo As SAPbouiCOM.ContextMenuInfo, ByRef BubbleEvent As Boolean)
            Try
                Dim objform As SAPbouiCOM.Form
                objform = objaddon.objapplication.Forms.ActiveForm
                If eventInfo.BeforeAction Then
                    If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE And eventInfo.ItemUID = "Item_17" Then
                        objform.EnableMenu("1292", True) 'Add row
                        objform.EnableMenu("1293", True) 'Delete Row
                    End If
                Else
                    objform.EnableMenu("1292", False) 'Add row
                    objform.EnableMenu("1293", False) 'Delete Row
                End If
            Catch ex As Exception
            End Try
        End Sub

#End Region

#Region "Payroll Process"

        Private Sub PayrollProcess_RightClickEvent(ByRef eventInfo As SAPbouiCOM.ContextMenuInfo, ByRef BubbleEvent As Boolean)
            Try
                Dim objform As SAPbouiCOM.Form
                objform = objaddon.objapplication.Forms.ActiveForm
                Dim odbdsDetails As SAPbouiCOM.DBDataSource

                odbdsDetails = objform.DataSources.DBDataSources.Item("@SMPR_OPRC")
                If eventInfo.BeforeAction Then
                    If ApprovedUser_Employee Then
                        If objform.Items.Item("chkfinal").Specific.Checked = False And odbdsDetails.GetValue("Status", 0) = "O" Then If eventInfo.ItemUID = "" Then objform.EnableMenu("1286", True) 'Close
                    Else
                        objform.EnableMenu("1286", False) 'Close
                    End If
                    strsql = "select 1 from ojdt where ""BatchNum"" =(SELECT left(""U_jeno"", LOCATE(""U_jeno"", '	') - 1) FROM ""@SMPR_OPRC"" where ""DocEntry""='" & objform.Items.Item("txtentry").Specific.string & "')"
                    objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    objrs.DoQuery(strsql)
                    If objrs.RecordCount = 0 Then Exit Sub
                    RightClickMenu_Add("1280", "PJE", "Provision JE Details", 0)
                Else
                    RightClickMenu_Delete("1280", "PJE")
                    objform.EnableMenu("1286", False) 'Close
                End If
            Catch ex As Exception
            End Try
        End Sub

#End Region

#Region "Provision"

        Private Sub ProvisionProcess_RightClickEvent(ByRef eventInfo As SAPbouiCOM.ContextMenuInfo, ByRef BubbleEvent As Boolean)
            Try
                Dim objform As SAPbouiCOM.Form
                objform = objaddon.objapplication.Forms.ActiveForm
                Dim ogrid As SAPbouiCOM.Grid
                ogrid = objform.Items.Item("grd").Specific
                If eventInfo.BeforeAction Then
                    If ogrid.DataTable.GetValue(ogrid.DataTable.Columns.Count - 1, 0) = "" Then Exit Sub
                    strsql = "select 1 from ojdt  where BatchNum =left('" & ogrid.DataTable.GetValue(ogrid.DataTable.Columns.Count - 1, 0).ToString & "',CHARINDEX('	','" & ogrid.DataTable.GetValue(ogrid.DataTable.Columns.Count - 1, 0).ToString & "')-1)"
                    objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    objrs.DoQuery(strsql)
                    If objrs.RecordCount = 0 Then Exit Sub
                    RightClickMenu_Add("1280", "PRJE", "Provision JE Details", 0)
                Else
                    RightClickMenu_Delete("1280", "PRJE")
                End If
            Catch ex As Exception
            End Try
        End Sub

#End Region
    End Class

End Namespace
