Option Strict Off
Option Explicit On

Imports SAPbouiCOM.Framework

Namespace HRMS
    <FormAttribute("OLSE", "Transcation/FrmSettlment.b1f")>
    Friend Class FrmSettlment
        Inherits UserFormBase
        Private WithEvents objform As SAPbouiCOM.Form
        Dim FormCount As Integer = 0
        Private WithEvents pCFL As SAPbouiCOM.ISBOChooseFromListEventArg
        Dim objrs As SAPbobsCOM.Recordset
        Dim strsql As String
        Dim addupdate As Boolean = False

        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.Button0 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.Button1 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.StaticText0 = CType(Me.GetItem("lbldate").Specific, SAPbouiCOM.StaticText)
            Me.EditText0 = CType(Me.GetItem("txtdate").Specific, SAPbouiCOM.EditText)
            Me.StaticText1 = CType(Me.GetItem("lblempid").Specific, SAPbouiCOM.StaticText)
            Me.EditText1 = CType(Me.GetItem("txttrzid").Specific, SAPbouiCOM.EditText)
            Me.EditText2 = CType(Me.GetItem("txtempid").Specific, SAPbouiCOM.EditText)
            Me.StaticText2 = CType(Me.GetItem("lblename").Specific, SAPbouiCOM.StaticText)
            Me.EditText3 = CType(Me.GetItem("txtename").Specific, SAPbouiCOM.EditText)
            Me.StaticText3 = CType(Me.GetItem("lbldept").Specific, SAPbouiCOM.StaticText)
            Me.EditText4 = CType(Me.GetItem("txtdept").Specific, SAPbouiCOM.EditText)
            Me.StaticText4 = CType(Me.GetItem("lbldesig").Specific, SAPbouiCOM.StaticText)
            Me.EditText5 = CType(Me.GetItem("txtdesig").Specific, SAPbouiCOM.EditText)
            Me.StaticText5 = CType(Me.GetItem("lbletype").Specific, SAPbouiCOM.StaticText)
            Me.StaticText6 = CType(Me.GetItem("lblcount").Specific, SAPbouiCOM.StaticText)
            Me.EditText7 = CType(Me.GetItem("txtcount").Specific, SAPbouiCOM.EditText)
            Me.StaticText7 = CType(Me.GetItem("lbllsdate").Specific, SAPbouiCOM.StaticText)
            Me.EditText8 = CType(Me.GetItem("txtlsdate").Specific, SAPbouiCOM.EditText)
            Me.Folder0 = CType(Me.GetItem("fld1").Specific, SAPbouiCOM.Folder)
            Me.Folder1 = CType(Me.GetItem("fldsalary").Specific, SAPbouiCOM.Folder)
            Me.Folder2 = CType(Me.GetItem("fldloan").Specific, SAPbouiCOM.Folder)
            Me.Folder3 = CType(Me.GetItem("fldaddded").Specific, SAPbouiCOM.Folder)
            Me.Folder4 = CType(Me.GetItem("fldgra").Specific, SAPbouiCOM.Folder)
            Me.StaticText8 = CType(Me.GetItem("lblad").Specific, SAPbouiCOM.StaticText)
            Me.StaticText9 = CType(Me.GetItem("lblled").Specific, SAPbouiCOM.StaticText)
            Me.StaticText10 = CType(Me.GetItem("lblati").Specific, SAPbouiCOM.StaticText)
            Me.StaticText11 = CType(Me.GetItem("lbllano").Specific, SAPbouiCOM.StaticText)
            Me.EditText9 = CType(Me.GetItem("txtlappno").Specific, SAPbouiCOM.EditText)
            Me.StaticText12 = CType(Me.GetItem("lblfdate").Specific, SAPbouiCOM.StaticText)
            Me.EditText10 = CType(Me.GetItem("txtfdate").Specific, SAPbouiCOM.EditText)
            Me.StaticText13 = CType(Me.GetItem("lbltdate").Specific, SAPbouiCOM.StaticText)
            Me.EditText11 = CType(Me.GetItem("txttdate").Specific, SAPbouiCOM.EditText)
            Me.StaticText14 = CType(Me.GetItem("lblldays").Specific, SAPbouiCOM.StaticText)
            Me.EditText12 = CType(Me.GetItem("txtldays").Specific, SAPbouiCOM.EditText)
            Me.StaticText15 = CType(Me.GetItem("lblrjdate").Specific, SAPbouiCOM.StaticText)
            Me.EditText13 = CType(Me.GetItem("txtrjdate").Specific, SAPbouiCOM.EditText)
            Me.StaticText16 = CType(Me.GetItem("lblltot").Specific, SAPbouiCOM.StaticText)
            Me.EditText14 = CType(Me.GetItem("txtltotal").Specific, SAPbouiCOM.EditText)
            Me.StaticText17 = CType(Me.GetItem("lblencash").Specific, SAPbouiCOM.StaticText)
            Me.EditText15 = CType(Me.GetItem("txtencash").Specific, SAPbouiCOM.EditText)
            Me.StaticText18 = CType(Me.GetItem("lbleligi").Specific, SAPbouiCOM.StaticText)
            Me.EditText16 = CType(Me.GetItem("txteligi").Specific, SAPbouiCOM.EditText)
            Me.StaticText19 = CType(Me.GetItem("lbledays").Specific, SAPbouiCOM.StaticText)
            Me.EditText17 = CType(Me.GetItem("txtedays").Specific, SAPbouiCOM.EditText)
            Me.StaticText20 = CType(Me.GetItem("lblbdays").Specific, SAPbouiCOM.StaticText)
            Me.EditText18 = CType(Me.GetItem("txtbdays").Specific, SAPbouiCOM.EditText)
            Me.StaticText21 = CType(Me.GetItem("Item_48").Specific, SAPbouiCOM.StaticText)
            Me.EditText19 = CType(Me.GetItem("Item_49").Specific, SAPbouiCOM.EditText)
            Me.StaticText22 = CType(Me.GetItem("lblatamt").Specific, SAPbouiCOM.StaticText)
            Me.EditText20 = CType(Me.GetItem("txtatamt").Specific, SAPbouiCOM.EditText)
            Me.StaticText23 = CType(Me.GetItem("lblatno").Specific, SAPbouiCOM.StaticText)
            Me.EditText21 = CType(Me.GetItem("txtatno").Specific, SAPbouiCOM.EditText)
            Me.StaticText24 = CType(Me.GetItem("lblcdate").Specific, SAPbouiCOM.StaticText)
            Me.EditText22 = CType(Me.GetItem("txtcdate").Specific, SAPbouiCOM.EditText)
            Me.StaticText25 = CType(Me.GetItem("lblcdays").Specific, SAPbouiCOM.StaticText)
            Me.EditText23 = CType(Me.GetItem("txtcdays").Specific, SAPbouiCOM.EditText)
            Me.StaticText26 = CType(Me.GetItem("lblaelgi").Specific, SAPbouiCOM.StaticText)
            Me.EditText24 = CType(Me.GetItem("txtaelgi").Specific, SAPbouiCOM.EditText)
            Me.StaticText27 = CType(Me.GetItem("lblsfdt").Specific, SAPbouiCOM.StaticText)
            Me.EditText25 = CType(Me.GetItem("txtsfdt").Specific, SAPbouiCOM.EditText)
            Me.StaticText28 = CType(Me.GetItem("lblstdt").Specific, SAPbouiCOM.StaticText)
            Me.EditText26 = CType(Me.GetItem("txtstdt").Specific, SAPbouiCOM.EditText)
            Me.Matrix0 = CType(Me.GetItem("mtasal").Specific, SAPbouiCOM.Matrix)
            Me.Matrix1 = CType(Me.GetItem("mtaddded").Specific, SAPbouiCOM.Matrix)
            Me.Matrix2 = CType(Me.GetItem("mtloan").Specific, SAPbouiCOM.Matrix)
            Me.StaticText29 = CType(Me.GetItem("Item_67").Specific, SAPbouiCOM.StaticText)
            Me.EditText27 = CType(Me.GetItem("txttads").Specific, SAPbouiCOM.EditText)
            Me.StaticText30 = CType(Me.GetItem("lbladvde").Specific, SAPbouiCOM.StaticText)
            Me.EditText28 = CType(Me.GetItem("txtadvde").Specific, SAPbouiCOM.EditText)
            Me.StaticText31 = CType(Me.GetItem("lbltadde").Specific, SAPbouiCOM.StaticText)
            Me.EditText29 = CType(Me.GetItem("txttadde").Specific, SAPbouiCOM.EditText)
            Me.StaticText32 = CType(Me.GetItem("lblgra").Specific, SAPbouiCOM.StaticText)
            Me.EditText30 = CType(Me.GetItem("txtGratu").Specific, SAPbouiCOM.EditText)
            Me.StaticText33 = CType(Me.GetItem("lblpay").Specific, SAPbouiCOM.StaticText)
            Me.EditText31 = CType(Me.GetItem("txtpay").Specific, SAPbouiCOM.EditText)
            Me.StaticText34 = CType(Me.GetItem("Item_77").Specific, SAPbouiCOM.StaticText)
            Me.StaticText35 = CType(Me.GetItem("Item_78").Specific, SAPbouiCOM.StaticText)
            Me.EditText32 = CType(Me.GetItem("Item_79").Specific, SAPbouiCOM.EditText)
            Me.StaticText36 = CType(Me.GetItem("Item_80").Specific, SAPbouiCOM.StaticText)
            Me.EditText33 = CType(Me.GetItem("Item_81").Specific, SAPbouiCOM.EditText)
            Me.StaticText37 = CType(Me.GetItem("Item_82").Specific, SAPbouiCOM.StaticText)
            Me.EditText34 = CType(Me.GetItem("Item_83").Specific, SAPbouiCOM.EditText)
            Me.StaticText38 = CType(Me.GetItem("lblremar").Specific, SAPbouiCOM.StaticText)
            Me.EditText35 = CType(Me.GetItem("Item_85").Specific, SAPbouiCOM.EditText)
            Me.StaticText39 = CType(Me.GetItem("lbldocdt").Specific, SAPbouiCOM.StaticText)
            Me.EditText36 = CType(Me.GetItem("txtdocdt").Specific, SAPbouiCOM.EditText)
            Me.StaticText40 = CType(Me.GetItem("lbldocno").Specific, SAPbouiCOM.StaticText)
            Me.EditText38 = CType(Me.GetItem("txtdocno").Specific, SAPbouiCOM.EditText)
            Me.ComboBox0 = CType(Me.GetItem("cmbseries").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText41 = CType(Me.GetItem("lblsta").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox1 = CType(Me.GetItem("cmbstatus").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText42 = CType(Me.GetItem("lbljdate").Specific, SAPbouiCOM.StaticText)
            Me.EditText39 = CType(Me.GetItem("txtjdate").Specific, SAPbouiCOM.EditText)
            Me.StaticText43 = CType(Me.GetItem("lblpayno").Specific, SAPbouiCOM.StaticText)
            Me.EditText40 = CType(Me.GetItem("txtpayno").Specific, SAPbouiCOM.EditText)
            Me.StaticText44 = CType(Me.GetItem("Item_100").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox2 = CType(Me.GetItem("cmbtype").Specific, SAPbouiCOM.ComboBox)
            Me.EditText41 = CType(Me.GetItem("txtlappen").Specific, SAPbouiCOM.EditText)
            Me.EditText42 = CType(Me.GetItem("txtpaydate").Specific, SAPbouiCOM.EditText)
            Me.EditText44 = CType(Me.GetItem("txtentry").Specific, SAPbouiCOM.EditText)
            Me.EditText45 = CType(Me.GetItem("txtpayen").Specific, SAPbouiCOM.EditText)
            Me.ComboBox3 = CType(Me.GetItem("cmbempty").Specific, SAPbouiCOM.ComboBox)
            Me.EditText46 = CType(Me.GetItem("txtaten").Specific, SAPbouiCOM.EditText)
            Me.LinkedButton0 = CType(Me.GetItem("lnkemp").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton1 = CType(Me.GetItem("lnkla").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton2 = CType(Me.GetItem("lnkair").Specific, SAPbouiCOM.LinkedButton)
            Me.EditText47 = CType(Me.GetItem("txtpdsal").Specific, SAPbouiCOM.EditText)
            Me.Button2 = CType(Me.GetItem("btnsplit").Specific, SAPbouiCOM.Button)
            Me.EditText48 = CType(Me.GetItem("txtpmsal").Specific, SAPbouiCOM.EditText)
            Me.Button4 = CType(Me.GetItem("Item_115").Specific, SAPbouiCOM.Button)
            Me.Button5 = CType(Me.GetItem("Item_116").Specific, SAPbouiCOM.Button)
            Me.Button6 = CType(Me.GetItem("btnnewlap").Specific, SAPbouiCOM.Button)
            Me.Button7 = CType(Me.GetItem("btnnewat").Specific, SAPbouiCOM.Button)
            Me.StaticText45 = CType(Me.GetItem("lbltdays").Specific, SAPbouiCOM.StaticText)
            Me.EditText49 = CType(Me.GetItem("txttdays").Specific, SAPbouiCOM.EditText)
            Me.StaticText46 = CType(Me.GetItem("lblop").Specific, SAPbouiCOM.StaticText)
            Me.EditText50 = CType(Me.GetItem("txtlop").Specific, SAPbouiCOM.EditText)
            Me.StaticText47 = CType(Me.GetItem("lbltwdays").Specific, SAPbouiCOM.StaticText)
            Me.EditText51 = CType(Me.GetItem("txttwdays").Specific, SAPbouiCOM.EditText)
            Me.StaticText48 = CType(Me.GetItem("lblgdays").Specific, SAPbouiCOM.StaticText)
            Me.EditText52 = CType(Me.GetItem("txtgdays").Specific, SAPbouiCOM.EditText)
            Me.StaticText49 = CType(Me.GetItem("lblpday").Specific, SAPbouiCOM.StaticText)
            Me.EditText53 = CType(Me.GetItem("txtpdbas").Specific, SAPbouiCOM.EditText)
            Me.StaticText50 = CType(Me.GetItem("lblgrat").Specific, SAPbouiCOM.StaticText)
            Me.EditText54 = CType(Me.GetItem("txtgra").Specific, SAPbouiCOM.EditText)
            Me.StaticText51 = CType(Me.GetItem("lblgrema").Specific, SAPbouiCOM.StaticText)
            Me.EditText55 = CType(Me.GetItem("txtgrema").Specific, SAPbouiCOM.EditText)
            Me.Folder5 = CType(Me.GetItem("fldpay").Specific, SAPbouiCOM.Folder)
            Me.Matrix3 = CType(Me.GetItem("mtpay").Specific, SAPbouiCOM.Matrix)
            Me.CheckBox0 = CType(Me.GetItem("Item_0").Specific, SAPbouiCOM.CheckBox)
            Me.LinkedButton3 = CType(Me.GetItem("lnkpay").Specific, SAPbouiCOM.LinkedButton)
            Me.StaticText52 = CType(Me.GetItem("lblpje").Specific, SAPbouiCOM.StaticText)
            Me.EditText6 = CType(Me.GetItem("txtpje").Specific, SAPbouiCOM.EditText)
            Me.LinkedButton4 = CType(Me.GetItem("lnkpje").Specific, SAPbouiCOM.LinkedButton)
            Me.StaticText53 = CType(Me.GetItem("Item_1").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox4 = CType(Me.GetItem("cmbpay").Specific, SAPbouiCOM.ComboBox)
            Me.EditText37 = CType(Me.GetItem("txtbacct").Specific, SAPbouiCOM.EditText)
            Me.StaticText54 = CType(Me.GetItem("Item_4").Specific, SAPbouiCOM.StaticText)
            Me.EditText43 = CType(Me.GetItem("txtbiban").Specific, SAPbouiCOM.EditText)
            Me.StaticText55 = CType(Me.GetItem("Item_6").Specific, SAPbouiCOM.StaticText)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()
            'AddHandler DataLoadAfter, AddressOf Me.FrmSettlment_DataLoadAfter
        End Sub

        Private Sub OnCustomInitialize()

            objform = objaddon.objapplication.Forms.GetForm("OLSE", Me.FormCount)
            objform = objaddon.objapplication.Forms.ActiveForm
            Try
                objform.Freeze(True)

                Field_Design()
                ManagerAttribute()
                COmbo_Load()
                CheckBox0.Item.Height = CheckBox0.Item.Height + 3
                If objaddon.ApprovedUser() Then
                    CheckBox0.Item.Enabled = True
                End If
                If Link_objtype.ToString.ToUpper = "OLSE" And Link_Value.ToString <> "" Then
                    objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE
                    EditText44.Item.Enabled = True
                    EditText44.Value = Link_Value
                    Button0.Item.Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                    EditText44.Item.Enabled = False
                    Link_objtype = "-1" : Link_Value = "-1"
                Else
                    ComboBox2.Select("LS", SAPbouiCOM.BoSearchKey.psk_ByValue)
                    objform.Items.Item("txtdate").Specific.string = Now.Date.ToString("yyyyMMdd")
                    objform.Items.Item("txtdocdt").Specific.string = Now.Date.ToString("yyyyMMdd")
                    EditText44.Value = objaddon.objglobalmethods.GetNextDocentry_Value("@SMPR_OLSE")
                    objform.ActiveItem = "txttrzid"
                End If
                EditText38.Value = objaddon.objglobalmethods.GetDocnum_BaseonSeries("OLSE")
                ComboBox0.Select(0, SAPbouiCOM.BoSearchKey.psk_Index)
                If ApprovedUser_Employee Then
                    CheckBox0.Item.Enabled = True
                End If
                objform.EnableMenu("1283", False) 'Remove
                objform.EnableMenu("1284", False) 'Cancel
                If objaddon.objcompany.UserName.ToString.ToUpper <> "MANAGER" Then objform.EnableMenu("6913", False) 'User Defined Field

                objform.Freeze(False)
            Catch ex As Exception
                objform.Freeze(False)
            End Try
        End Sub

        Private Sub Field_Design()
            HeaderLabel_Color(StaticText8.Item, 11, Color.Red.ToArgb, 15)
            HeaderLabel_Color(StaticText9.Item, 11, Color.Red.ToArgb, 15)
            HeaderLabel_Color(StaticText10.Item, 11, Color.Red.ToArgb, 15)
            HeaderLabel_Color(StaticText34.Item, 11, Color.Red.ToArgb, 15)

            HeaderLabel_Color(Folder0.Item, 11, 150, Folder0.Item.Height, Folder0.Item.Width + 20)
            HeaderLabel_Color(Folder1.Item, 11, 150, Folder0.Item.Height, Folder0.Item.Width + 20)
            HeaderLabel_Color(Folder2.Item, 11, 150, Folder0.Item.Height, Folder0.Item.Width + 20)
            HeaderLabel_Color(Folder3.Item, 11, 150, Folder0.Item.Height, Folder0.Item.Width + 20)
            HeaderLabel_Color(Folder4.Item, 11, 150, Folder0.Item.Height, Folder0.Item.Width + 20)
            HeaderLabel_Color(Folder5.Item, 11, 150, Folder0.Item.Height, Folder0.Item.Width + 20)
            Folder0.Item.Click(SAPbouiCOM.BoCellClickType.ct_Regular)
        End Sub

        Private Sub ManagerAttribute()
            Try
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtdocno", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtempid", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtename", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cmbstatus", False, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cmbtype", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtdate", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txttrzid", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "cmbseries", True, True, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Editable(objform, "txtdocdt", True, True, False)

                objaddon.objglobalmethods.SetAutomanagedattribute_Visible(objform, "btnnewlap", True, False, False)
                objaddon.objglobalmethods.SetAutomanagedattribute_Visible(objform, "btnnewat", True, False, False)
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Combo_Load()
            Try
                Dim ocombo As SAPbouiCOM.Column
                ocombo = Matrix1.Columns.Item("type")

                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery("CALL ""MIPL_HRMS_EMPMASTER_COMBO_FILLING"" ('OLSE')")
                If objrs.RecordCount = 0 Then Exit Sub
                For i As Integer = 1 To objrs.RecordCount
                    Try
                        Select Case objrs.Fields.Item("Type").Value.ToString.ToUpper
                            Case "EMPTYPE"
                                ComboBox3.ValidValues.Add(objrs.Fields.Item("Code").Value.ToString, objrs.Fields.Item("Name").Value.ToString)
                            Case "SETTYPE"
                                ocombo.ValidValues.Add(objrs.Fields.Item("Code").Value.ToString, objrs.Fields.Item("Name").Value.ToString)
                        End Select
                        objrs.MoveNext()
                    Catch ex As Exception
                        objrs.MoveNext()
                    End Try
                Next
            Catch ex As Exception

            End Try
        End Sub

        Private Sub DefaultSeries()
            Try
                Dim dftlseries As String = objaddon.objglobalmethods.default_series("OLSE", IIf(EditText36.String = "", Now.Date, EditText36.String))
                Dim ocomboseries As SAPbouiCOM.ComboBox = objform.Items.Item("cmbseries").Specific
                If dftlseries.ToString <> "" Then ocomboseries.Select(dftlseries.ToString, SAPbouiCOM.BoSearchKey.psk_ByValue)
            Catch ex As Exception
            End Try
        End Sub

        Private Sub HeaderLabel_Color(ByVal item As SAPbouiCOM.Item, ByVal fontsize As Integer, ByVal forecolor As Integer, ByVal height As Integer, Optional ByVal width As Integer = 0, Optional ByVal left As Integer = 0)
            item.TextStyle = FontStyle.Bold
            item.FontSize = fontsize
            item.ForeColor = forecolor
            item.Height = height
            If width <> 0 Then item.Width = width
        End Sub

#Region "Field Details"

        Private WithEvents Button0 As SAPbouiCOM.Button
        Private WithEvents Button1 As SAPbouiCOM.Button
        Private WithEvents StaticText0 As SAPbouiCOM.StaticText
        Private WithEvents EditText0 As SAPbouiCOM.EditText
        Private WithEvents StaticText1 As SAPbouiCOM.StaticText
        Private WithEvents EditText1 As SAPbouiCOM.EditText
        Private WithEvents EditText2 As SAPbouiCOM.EditText
        Private WithEvents StaticText2 As SAPbouiCOM.StaticText
        Private WithEvents EditText3 As SAPbouiCOM.EditText
        Private WithEvents StaticText3 As SAPbouiCOM.StaticText
        Private WithEvents EditText4 As SAPbouiCOM.EditText
        Private WithEvents StaticText4 As SAPbouiCOM.StaticText
        Private WithEvents EditText5 As SAPbouiCOM.EditText
        Private WithEvents StaticText5 As SAPbouiCOM.StaticText
        Private WithEvents StaticText6 As SAPbouiCOM.StaticText
        Private WithEvents EditText7 As SAPbouiCOM.EditText
        Private WithEvents StaticText7 As SAPbouiCOM.StaticText
        Private WithEvents EditText8 As SAPbouiCOM.EditText
        Private WithEvents Folder0 As SAPbouiCOM.Folder
        Private WithEvents Folder1 As SAPbouiCOM.Folder
        Private WithEvents Folder2 As SAPbouiCOM.Folder
        Private WithEvents Folder3 As SAPbouiCOM.Folder
        Private WithEvents Folder4 As SAPbouiCOM.Folder
        Private WithEvents StaticText8 As SAPbouiCOM.StaticText
        Private WithEvents StaticText9 As SAPbouiCOM.StaticText
        Private WithEvents StaticText10 As SAPbouiCOM.StaticText
        Private WithEvents StaticText11 As SAPbouiCOM.StaticText
        Private WithEvents EditText9 As SAPbouiCOM.EditText
        Private WithEvents StaticText12 As SAPbouiCOM.StaticText
        Private WithEvents EditText10 As SAPbouiCOM.EditText
        Private WithEvents StaticText13 As SAPbouiCOM.StaticText
        Private WithEvents EditText11 As SAPbouiCOM.EditText
        Private WithEvents StaticText14 As SAPbouiCOM.StaticText
        Private WithEvents EditText12 As SAPbouiCOM.EditText
        Private WithEvents StaticText15 As SAPbouiCOM.StaticText
        Private WithEvents EditText13 As SAPbouiCOM.EditText
        Private WithEvents StaticText16 As SAPbouiCOM.StaticText
        Private WithEvents EditText14 As SAPbouiCOM.EditText
        Private WithEvents StaticText17 As SAPbouiCOM.StaticText
        Private WithEvents EditText15 As SAPbouiCOM.EditText
        Private WithEvents StaticText18 As SAPbouiCOM.StaticText
        Private WithEvents EditText16 As SAPbouiCOM.EditText
        Private WithEvents StaticText19 As SAPbouiCOM.StaticText
        Private WithEvents EditText17 As SAPbouiCOM.EditText
        Private WithEvents StaticText20 As SAPbouiCOM.StaticText
        Private WithEvents EditText18 As SAPbouiCOM.EditText
        Private WithEvents StaticText21 As SAPbouiCOM.StaticText
        Private WithEvents EditText19 As SAPbouiCOM.EditText
        Private WithEvents StaticText22 As SAPbouiCOM.StaticText
        Private WithEvents EditText20 As SAPbouiCOM.EditText
        Private WithEvents StaticText23 As SAPbouiCOM.StaticText
        Private WithEvents EditText21 As SAPbouiCOM.EditText
        Private WithEvents StaticText24 As SAPbouiCOM.StaticText
        Private WithEvents EditText22 As SAPbouiCOM.EditText
        Private WithEvents StaticText25 As SAPbouiCOM.StaticText
        Private WithEvents EditText23 As SAPbouiCOM.EditText
        Private WithEvents StaticText26 As SAPbouiCOM.StaticText
        Private WithEvents EditText24 As SAPbouiCOM.EditText
        Private WithEvents StaticText27 As SAPbouiCOM.StaticText
        Private WithEvents EditText25 As SAPbouiCOM.EditText
        Private WithEvents StaticText28 As SAPbouiCOM.StaticText
        Private WithEvents EditText26 As SAPbouiCOM.EditText
        Private WithEvents Matrix0 As SAPbouiCOM.Matrix
        Private WithEvents Matrix1 As SAPbouiCOM.Matrix
        Private WithEvents Matrix2 As SAPbouiCOM.Matrix
        Private WithEvents StaticText29 As SAPbouiCOM.StaticText
        Private WithEvents EditText27 As SAPbouiCOM.EditText
        Private WithEvents StaticText30 As SAPbouiCOM.StaticText
        Private WithEvents EditText28 As SAPbouiCOM.EditText
        Private WithEvents StaticText31 As SAPbouiCOM.StaticText
        Private WithEvents EditText29 As SAPbouiCOM.EditText
        Private WithEvents StaticText32 As SAPbouiCOM.StaticText
        Private WithEvents EditText30 As SAPbouiCOM.EditText
        Private WithEvents StaticText33 As SAPbouiCOM.StaticText
        Private WithEvents EditText31 As SAPbouiCOM.EditText
        Private WithEvents StaticText34 As SAPbouiCOM.StaticText
        Private WithEvents StaticText35 As SAPbouiCOM.StaticText
        Private WithEvents EditText32 As SAPbouiCOM.EditText
        Private WithEvents StaticText36 As SAPbouiCOM.StaticText
        Private WithEvents EditText33 As SAPbouiCOM.EditText
        Private WithEvents StaticText37 As SAPbouiCOM.StaticText
        Private WithEvents EditText34 As SAPbouiCOM.EditText
        Private WithEvents StaticText38 As SAPbouiCOM.StaticText
        Private WithEvents EditText35 As SAPbouiCOM.EditText
        Private WithEvents StaticText39 As SAPbouiCOM.StaticText
        Private WithEvents EditText36 As SAPbouiCOM.EditText
        Private WithEvents StaticText40 As SAPbouiCOM.StaticText
        Private WithEvents EditText38 As SAPbouiCOM.EditText
        Private WithEvents ComboBox0 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText41 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox1 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText42 As SAPbouiCOM.StaticText
        Private WithEvents EditText39 As SAPbouiCOM.EditText
        Private WithEvents StaticText43 As SAPbouiCOM.StaticText
        Private WithEvents EditText40 As SAPbouiCOM.EditText
        Private WithEvents StaticText44 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox2 As SAPbouiCOM.ComboBox
        Private WithEvents EditText41 As SAPbouiCOM.EditText
        Private WithEvents EditText42 As SAPbouiCOM.EditText
        Private WithEvents EditText44 As SAPbouiCOM.EditText
        Private WithEvents EditText45 As SAPbouiCOM.EditText
        Private WithEvents ComboBox3 As SAPbouiCOM.ComboBox
        Private WithEvents EditText46 As SAPbouiCOM.EditText
        Private WithEvents LinkedButton0 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton1 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton2 As SAPbouiCOM.LinkedButton
        Private WithEvents EditText47 As SAPbouiCOM.EditText
        Private WithEvents Button2 As SAPbouiCOM.Button
        Private WithEvents EditText48 As SAPbouiCOM.EditText
        Private WithEvents Button4 As SAPbouiCOM.Button
        Private WithEvents Button5 As SAPbouiCOM.Button
        Private WithEvents StaticText45 As SAPbouiCOM.StaticText
        Private WithEvents EditText49 As SAPbouiCOM.EditText
        Private WithEvents StaticText46 As SAPbouiCOM.StaticText
        Private WithEvents EditText50 As SAPbouiCOM.EditText
        Private WithEvents StaticText47 As SAPbouiCOM.StaticText
        Private WithEvents EditText51 As SAPbouiCOM.EditText
        Private WithEvents StaticText48 As SAPbouiCOM.StaticText
        Private WithEvents EditText52 As SAPbouiCOM.EditText
        Private WithEvents StaticText49 As SAPbouiCOM.StaticText
        Private WithEvents EditText53 As SAPbouiCOM.EditText
        Private WithEvents StaticText50 As SAPbouiCOM.StaticText
        Private WithEvents EditText54 As SAPbouiCOM.EditText
        Private WithEvents StaticText51 As SAPbouiCOM.StaticText
        Private WithEvents EditText55 As SAPbouiCOM.EditText
        Private WithEvents Button6 As SAPbouiCOM.Button
        Private WithEvents Button7 As SAPbouiCOM.Button
        Private WithEvents Folder5 As SAPbouiCOM.Folder
        Private WithEvents Matrix3 As SAPbouiCOM.Matrix
        Private WithEvents CheckBox0 As SAPbouiCOM.CheckBox

#End Region

#Region "Folder Click"

        Private Sub Folder1_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Folder1.PressedAfter
            Try
                objaddon.objapplication.Menus.Item("1300").Activate()
                EditText25.Item.Click(SAPbouiCOM.BoCellClickType.ct_Regular)
            Catch ex As Exception
            End Try
        End Sub

        Private Sub Folder2_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Folder2.PressedAfter
            Try
                objaddon.objapplication.Menus.Item("1300").Activate()
                If Matrix2.VisualRowCount > 0 Then Matrix2.SetCellFocus(1, 1)
            Catch ex As Exception
            End Try
        End Sub

        Private Sub Folder3_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Folder3.PressedAfter
            Try
                objaddon.objapplication.Menus.Item("1300").Activate()
                If EditText2.Value.ToString = "" Then Exit Sub
                If Matrix1.VisualRowCount = 0 Then
                    Matrix1.AddRow(1)
                    Matrix1.Columns.Item("#").Cells.Item(Matrix1.VisualRowCount).Specific.string = Matrix1.VisualRowCount
                    If Matrix1.VisualRowCount > 0 Then Matrix1.SetCellFocus(1, 1)
                Else
                    If Matrix1.Columns.Item("amount").Cells.Item(Matrix1.VisualRowCount).Specific.ToString = 0 Then
                        Matrix1.AddRow(1)
                        Matrix1.Columns.Item("#").Cells.Item(Matrix1.VisualRowCount).Specific.string = Matrix1.VisualRowCount
                        If Matrix1.VisualRowCount > 0 Then Matrix1.SetCellFocus(1, 1)
                    End If
                End If

            Catch ex As Exception
            End Try
        End Sub

        Private Sub Folder5_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Folder5.PressedAfter
            Try
                objaddon.objapplication.Menus.Item("1300").Activate()
            Catch ex As Exception
            End Try
        End Sub

#End Region

        Private Sub ComboBox2_ComboSelectAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox2.ComboSelectAfter
            Try
                If ComboBox2.Selected Is Nothing Then Exit Sub
                If ComboBox2.Selected.Value = "LS" Then
                    objform.Freeze(True)
                    ClearAll()
                    Folder4.Item.Visible = False
                    StaticText32.Item.Visible = False
                    EditText30.Item.Visible = False
                    EditText0.Item.Enabled = True
                    EditText15.Item.Enabled = True
                    objform.Items.Item("txtdate").Specific.string = Now.Date.ToString("yyyyMMdd")
                    objform.Items.Item("txtdocdt").Specific.string = Now.Date.ToString("yyyyMMdd")
                    objform.Title = "Leave Settlement"
                    objform.ActiveItem = "txtdate"
                    objform.Freeze(False)
                Else
                    objform.Freeze(True)
                    ClearAll()
                    Folder4.Item.Visible = True
                    StaticText32.Item.Visible = True
                    EditText30.Item.Visible = True
                    EditText0.Item.Enabled = False
                    EditText15.Item.Enabled = False
                    objform.Items.Item("txtdate").Specific.string = Now.Date.ToString("yyyyMMdd")
                    objform.Items.Item("txtdocdt").Specific.string = Now.Date.ToString("yyyyMMdd")
                    objform.Title = "Final Settlement"
                    objform.ActiveItem = "txttrzid"
                    objform.Freeze(False)
                End If

            Catch ex As Exception
                objform.Freeze(False)
            End Try
        End Sub

        Private Sub ComboBox0_ComboSelectAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox0.ComboSelectAfter
            If ComboBox0.Selected Is Nothing Then Exit Sub
            EditText38.Value = objaddon.objglobalmethods.GetDocnum_BaseonSeries("OLSE")
        End Sub

        Private Sub ClearAll()
            Try
                EditText0.Value = ""
                EditText1.Value = ""
                Clear_empdetails()
                EditText9.Value = "" : Clear_LeaveAppDetails()
                objform.Items.Item("txtencash").Specific.string = ""
                EditText21.Value = "" : Clear_Airticketissue_details()
                Advance_SalaryDetails_Clearing()
                Addition_Deduction_ClearingData()
                loanDetails_Clearing()
                Clear_Gratuity()
                EditText31.Value = 0.0
                objform.Update()
                objform.Refresh()
            Catch ex As Exception

            End Try
        End Sub

#Region "Employee Details Loading "

        Private Sub EditText1_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText1.ChooseFromListBefore
            If pVal.ActionSuccess = True Then Exit Sub
            Try
                If ComboBox2.Selected Is Nothing Then BubbleEvent = False : Exit Sub
                Dim oCFL As SAPbouiCOM.ChooseFromList = objform.ChooseFromLists.Item("empde")
                Dim oConds As SAPbouiCOM.Conditions
                Dim oCond As SAPbouiCOM.Condition
                Dim oEmptyConds As New SAPbouiCOM.Conditions
                oCFL.SetConditions(oEmptyConds)
                oConds = oCFL.GetConditions()

                'oCond = oConds.Add()
                'oCond.Alias = "U_empID"
                'oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                'oCond.CondVal = EditText2.Value.ToString

                'oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND

                'oCond = oConds.Add()
                'oCond.Alias = "U_Approved"
                'oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                'oCond.CondVal = "Y"

                If ComboBox2.Selected.Value = "FF" Then
                    strsql = "select ""U_empID"" from ""@SMPR_OHEM"" where ""U_termdate"" is not null and ""U_empID"" not in (select ""U_EmpID"" from ""@SMPR_OLSE"" where ""U_setltype""='FF')"
                    'strsql = "select U_empid from [@SMPR_OHEM] where U_termdate is not null and U_empid not in (select U_empid from [@SMPR_OLSE] where U_setltype='FF' union all select U_empId from [@SMPR_OFST])"
                    objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    objrs.DoQuery(strsql)
                    For i As Integer = 0 To objrs.RecordCount - 1
                        oCond = oConds.Add()
                        oCond.Alias = "U_empID"
                        oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                        oCond.CondVal = objrs.Fields.Item("U_empid").Value.ToString
                        objrs.MoveNext()
                        If i <> objrs.RecordCount - 1 Then oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_OR
                    Next
                Else
                    oCond = oConds.Add()
                    oCond.Alias = "U_Status"
                    oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                    oCond.CondVal = "1"
                End If

                oCFL.SetConditions(oConds)
            Catch ex As Exception
                SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage("Choose FromList Filter Leave Details Failed:" & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
            End Try
        End Sub

        Private Sub EditText1_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText1.ChooseFromListAfter
            Try
                If pVal.ActionSuccess = False Then Exit Sub
                pCFL = pVal
                If Not pCFL.SelectedObjects Is Nothing Then
                    Try
                        EditText1.Value = pCFL.SelectedObjects.Columns.Item("U_ExtEmpNo").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                    Load_employeeDetails()
                End If
            Catch ex As Exception
            Finally
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try
        End Sub

        Private Sub Load_employeeDetails()

            Try
                If EditText1.Value.ToString = "" Then Exit Sub
                objaddon.objapplication.SetStatusBarMessage("Loading Employee Details. Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Medium, False)
                strsql = "CALL ""MIPL_HRMS_GetEmpDetails_Settlement"" ('" & EditText1.Value.ToString & "')"
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strsql)
                If objrs.RecordCount = 0 Then Exit Sub
                EditText2.Value = objrs.Fields.Item("U_empid").Value.ToString
                EditText3.Value = objrs.Fields.Item("Name").Value.ToString
                EditText4.Value = objrs.Fields.Item("Department").Value.ToString
                EditText5.Value = objrs.Fields.Item("Designation").Value.ToString
                ComboBox3.Select(objrs.Fields.Item("Emptype").Value.ToString, SAPbouiCOM.BoSearchKey.psk_ByValue)
                EditText7.Value = objrs.Fields.Item("Country").Value.ToString
                objform.Items.Item("txtjdate").Specific.String = objrs.Fields.Item("JoiningDate").Value.ToString
                objform.Items.Item("txtlsdate").Specific.String = objrs.Fields.Item("Leavesettleddate").Value.ToString
                EditText47.Value = objrs.Fields.Item("PerDaySalary_lvst").Value.ToString
                EditText48.Value = objrs.Fields.Item("Salary_month").Value
                Try
                    ComboBox4.Select(objrs.Fields.Item("Paymode").Value.ToString, SAPbouiCOM.BoSearchKey.psk_ByValue)
                Catch ex As Exception

                End Try
                EditText37.Value = objrs.Fields.Item("BankAcct").Value
                EditText43.Value = objrs.Fields.Item("Bankiban").Value

                If Not ComboBox2.Selected Is Nothing Then
                    If ComboBox2.Selected.Value = "FF" Then
                        objform.Items.Item("txtdate").Specific.string = objrs.Fields.Item("termDate").Value.ToString
                        Load_Gratuity() 'Gratutiy Calculation
                        objform.Items.Item("txtencash").Specific.string = objform.Items.Item("txtdate").Specific.string
                        Encashment_eligibilty_Leave_Calcualtion()
                    End If
                End If

                'Leave Application Docentry Filling
                Try
                    If objrs.Fields.Item("LeaveAppentry").Value.ToString <> 0 Then EditText9.Value = objrs.Fields.Item("LeaveAppentry").Value.ToString Else EditText9.Value = ""
                Catch ex As Exception
                End Try

                'Air Ticket Issue Docentry Filling
                Try
                    If objrs.Fields.Item("Airticket").Value.ToString <> 0 Then EditText21.Value = objrs.Fields.Item("Airticket").Value.ToString Else EditText21.Value = ""
                Catch ex As Exception
                End Try

                Advance_SalaryDetails_Clearing() 'Advance Matrix Details Clearing

                If EditText9.Value.ToString = "" Then Clear_LeaveAppDetails() Else LeaveApplication_Filling() 'Leave App Details

                If EditText21.Value.ToString = "" Then Clear_Airticketissue_details() Else AirTicketIssue_Details_Filling() 'Air Ticket Issue Details

                loanDetails_loading() 'Pending Loan Details FIlling in matrix

                Addition_Deduction_ClearingData() 'Clearing Addition Deduction Matrix

                Final_total()

                EditText9.Item.Click(SAPbouiCOM.BoCellClickType.ct_Regular)

                Load_Paydetails()

                objform.Update()
                objform.Refresh()

                objaddon.objapplication.SetStatusBarMessage("Employee Details Loaded Succcessfully.", SAPbouiCOM.BoMessageTime.bmt_Short, False)
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Clear_empdetails()
            Try
                EditText2.Value = ""
                EditText3.Value = ""
                EditText4.Value = ""
                EditText5.Value = ""
                'ComboBox3.Select(objrs.Fields.Item("Emptype").Value.ToString, SAPbouiCOM.BoSearchKey.psk_ByValue)
                EditText7.Value = ""
                objform.Items.Item("txtjdate").Specific.String = ""
                EditText47.Value = ""
                EditText48.Value = ""
                Clear_Paydetails()
            Catch ex As Exception

            End Try
        End Sub

#End Region

#Region "Leave Application Part"

        Private Sub EditText9_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText9.ChooseFromListBefore
            If pVal.ActionSuccess = True Then Exit Sub
            Try
                If EditText2.Value.ToString = "" Then BubbleEvent = False

                Dim oCFL As SAPbouiCOM.ChooseFromList = objform.ChooseFromLists.Item("leaveapp")
                Dim oConds As SAPbouiCOM.Conditions
                Dim oCond As SAPbouiCOM.Condition
                Dim oEmptyConds As New SAPbouiCOM.Conditions
                oCFL.SetConditions(oEmptyConds)
                oConds = oCFL.GetConditions()

                oCond = oConds.Add()
                oCond.Alias = "U_empID"
                oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                oCond.CondVal = EditText2.Value.ToString

                oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND

                oCond = oConds.Add()
                oCond.Alias = "U_Approved"
                oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                oCond.CondVal = "Y"

                oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND

                oCond = oConds.Add()
                oCond.Alias = "Status"
                oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                oCond.CondVal = "O"

                oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND

                oCond = oConds.Add()
                oCond.Alias = "U_Payable"
                oCond.Operation = SAPbouiCOM.BoConditionOperation.co_NOT_EQUAL
                oCond.CondVal = "Y"

                oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND

                oCond = oConds.Add()
                oCond.Alias = "U_LveCode"
                oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                oCond.CondVal = "AL"

                oCFL.SetConditions(oConds)
            Catch ex As Exception
                SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage("Choose FromList Filter Leave Details Failed:" & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
            End Try
        End Sub

        Private Sub EditText9_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText9.ChooseFromListAfter
            Try
                If pVal.ActionSuccess = False Then Exit Sub
                pCFL = pVal
                If Not pCFL.SelectedObjects Is Nothing Then
                    Try
                        EditText9.Value = pCFL.SelectedObjects.Columns.Item("DocNum").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try

                    If EditText9.Value.ToString = "" Then Clear_LeaveAppDetails() Else LeaveApplication_Filling()

                End If
            Catch ex As Exception
            Finally
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try
        End Sub

        Private Sub EditText9_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText9.LostFocusAfter
            If EditText9.Value.ToString = "" Then Clear_LeaveAppDetails() Else LeaveApplication_Filling()
        End Sub

        Private Sub LeaveApplication_Filling()
            Try
                Try
                    objform.ActiveItem = "Item_85"
                Catch ex As Exception

                End Try
                EditText15.Item.Enabled = False
                EditText26.Item.Enabled = False
                strsql = "select ""DocEntry"",""DocNum"",TO_VARCHAR(""U_FromDate"",'dd/MM/yy') ""FromDate"",TO_VARCHAR(""U_Todate"",'dd/MM/yy') ""ToDate"", ""U_NoDayLve"",TO_VARCHAR(ADD_DAYS(""U_FromDate"",-1),'dd/MM/yy') ""encashdate"","
                strsql += vbCrLf + " TO_VARCHAR(""U_RejoinDt"",'dd/MM/yy') ""Rejoin"" from ""@SMPR_OLVA"" where ""DocEntry""='" & EditText9.Value & "' and ""U_Approved""='Y' and ""Status""='O'"
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strsql)
                If objrs.RecordCount = 0 Then Exit Sub
                EditText41.Value = objrs.Fields.Item("DocNum").Value.ToString
                objform.Items.Item("txtfdate").Specific.string = objrs.Fields.Item("FromDate").Value.ToString
                objform.Items.Item("txttdate").Specific.string = objrs.Fields.Item("ToDate").Value.ToString
                EditText12.Value = objrs.Fields.Item("U_NoDayLve").Value.ToString
                objform.Items.Item("txtencash").Specific.string = objrs.Fields.Item("encashdate").Value.ToString
                objform.Items.Item("txtstdt").Specific.string = objrs.Fields.Item("encashdate").Value.ToString
                objform.Items.Item("txtrjdate").Specific.String = objrs.Fields.Item("Rejoin").Value.ToString
                EditText19.Value = SalaryCalculation_from_todate(EditText10.String, EditText11.String)
                Encashment_eligibilty_Leave_Calcualtion()

                Final_total()

                objform.Update()
                objform.Refresh()
            Catch ex As Exception

            End Try
        End Sub

        Private Function SalaryCalculation_from_todate(ByVal Fromdate As Date, ByVal Todate As Date) As Double
            Try
                Dim LastDayInMonthDate As Date
                Dim total As Double = 0.0
                For i As Integer = 0 To 100
                    LastDayInMonthDate = New Date(Fromdate.Year, Fromdate.Month, Date.DaysInMonth(Fromdate.Year, Fromdate.Month))
                    If LastDayInMonthDate > Todate Then LastDayInMonthDate = Todate
                    total = total + ((DateDiff(DateInterval.Day, Fromdate, LastDayInMonthDate) + 1) * (EditText48.Value / Date.DaysInMonth(Fromdate.Year, Fromdate.Month)))
                    Fromdate = DateAdd(DateInterval.Day, 1, LastDayInMonthDate)
                    If Fromdate > Todate Then Exit For
                Next
                Return total
            Catch ex As Exception
                Return 0.0
            End Try
        End Function

        Private Sub Clear_LeaveAppDetails()
            Try

                EditText41.Value = ""
                objform.Items.Item("txtfdate").Specific.string = ""
                objform.Items.Item("txttdate").Specific.string = ""
                objform.Items.Item("txtrjdate").Specific.String = ""
                EditText12.Value = ""
                If Not ComboBox2.Selected Is Nothing Then
                    If ComboBox2.Selected.Value <> "FF" Then
                        objform.Items.Item("txtencash").Specific.string = "" : EditText16.Value = 0.0
                        EditText15.Item.Enabled = True
                    Else
                        EditText15.Item.Enabled = False
                    End If
                Else
                    objform.Items.Item("txtencash").Specific.string = "" : EditText16.Value = 0.0
                    EditText15.Item.Enabled = True
                End If

                EditText19.Value = 0.0
                EditText17.Value = 0.0
                objform.Items.Item("txtstdt").Specific.string = ""
                EditText26.Item.Enabled = True

                objform.Update()
                objform.Refresh()
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Button6_ClickBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles Button6.ClickBefore
            Try
                If EditText1.Value.ToString = "" Then Exit Sub
                Link_objtype = "OLVA_AN"
                Link_Value = EditText1.Value
                Dim activeform As New frmLeaveApplicaiton
                activeform.Show()
            Catch ex As Exception

            End Try
        End Sub
#End Region

#Region "Leave Encashment Part"

        Private Sub EditText15_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText15.LostFocusAfter
            Encashment_eligibilty_Leave_Calcualtion()
        End Sub

        Private Sub Encashment_eligibilty_Leave_Calcualtion()
            If EditText2.Value.ToString = "" Or EditText15.Value.ToString = "" Then
                EditText16.Value = 0
                Exit Sub
            End If
            Dim objrs1 As SAPbobsCOM.Recordset
            strsql = "CALL ""MIPL_HRMS_LeaveApplication_Balance"" ('" & EditText15.Value & "','" & EditText2.Value & "','AL')"
            objrs1 = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            objrs1.DoQuery(strsql)
            If objrs1.RecordCount > 0 Then
                EditText16.Value = objrs1.Fields.Item("Available_Leave").Value
                EditText17.Value = 0
                'If Val(EditText12.Value) > 0 Then
                '    EditText16.Value = objrs.Fields.Item("Available_Leave").Value - EditText12.Value
                'Else
                '    EditText16.Value = objrs.Fields.Item("Available_Leave").Value
                'End If
            End If

            Encashment_balance_amount_calculation()
        End Sub

        Private Sub EditText17_ValidateBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText17.ValidateBefore
            If Val(EditText17.Value) = 0 Then Exit Sub
            If Val(EditText17.Value) > Val(EditText16.Value) Then
                objaddon.objapplication.SetStatusBarMessage("Encash Days should be less then or equal to Eligible leave Days.", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                BubbleEvent = False
            End If
        End Sub

        Private Sub EditText17_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText17.LostFocusAfter
            Encashment_balance_amount_calculation()
        End Sub

        Private Sub Encashment_balance_amount_calculation()
            Try
                If Val(EditText17.Value) <> 0 And Val(EditText16.Value) <> 0 Then
                    EditText18.Value = EditText16.Value - EditText17.Value
                    EditText14.Value = EditText47.Value * EditText17.Value
                Else
                    EditText18.Value = 0
                    EditText14.Value = 0
                End If

                Final_total()

                objform.Update()
                objform.Refresh()
            Catch ex As Exception

            End Try
        End Sub

#End Region

#Region "Air Ticket Part"

        Private Sub EditText21_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles EditText21.ChooseFromListBefore
            If pVal.ActionSuccess = True Then Exit Sub
            Try
                If EditText2.Value.ToString = "" Then BubbleEvent = False

                Dim oCFL As SAPbouiCOM.ChooseFromList = objform.ChooseFromLists.Item("Ticketis")
                Dim oConds As SAPbouiCOM.Conditions
                Dim oCond As SAPbouiCOM.Condition
                Dim oEmptyConds As New SAPbouiCOM.Conditions
                oCFL.SetConditions(oEmptyConds)
                oConds = oCFL.GetConditions()

                oCond = oConds.Add()
                oCond.Alias = "U_empID"
                oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                oCond.CondVal = EditText2.Value.ToString

                oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND

                oCond = oConds.Add()
                oCond.Alias = "U_Approved"
                oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                oCond.CondVal = "Y"

                oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND

                oCond = oConds.Add()
                oCond.Alias = "Status"
                oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                oCond.CondVal = "O"

                oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND

                oCond = oConds.Add()
                oCond.Alias = "U_payroll"
                oCond.Operation = SAPbouiCOM.BoConditionOperation.co_NOT_EQUAL
                oCond.CondVal = "Y"

                oCFL.SetConditions(oConds)
            Catch ex As Exception
                SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage("Choose FromList Filter Leave Details Failed:" & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
            End Try
        End Sub

        Private Sub EditText21_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText21.ChooseFromListAfter
            Try
                If pVal.ActionSuccess = False Then Exit Sub
                pCFL = pVal
                If Not pCFL.SelectedObjects Is Nothing Then
                    Try
                        EditText21.Value = pCFL.SelectedObjects.Columns.Item("DocEntry").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                    If EditText21.Value.ToString = "" Then Clear_Airticketissue_details() Else AirTicketIssue_Details_Filling()
                End If
            Catch ex As Exception
            Finally
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try
        End Sub

        Private Sub EditText21_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText21.LostFocusAfter
            If EditText21.Value = "" Then Clear_Airticketissue_details()
        End Sub

        Private Sub AirTicketIssue_Details_Filling()
            Try
                If EditText21.Value.ToString = "" Then Clear_Airticketissue_details() : Exit Sub

                strsql = "Select ""DocNum"",TO_VARCHAR(""U_TickDate"",'dd/MM/yy')""TicketDate"",""U_eligiamt"",""U_noofday"",""U_Total"" from ""@SMPR_OTIS"" where ""DocEntry""='" & EditText21.Value & "'"
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strsql)
                If objrs.RecordCount = 0 Then Exit Sub
                EditText46.Value = objrs.Fields.Item("DocNum").Value.ToString
                objform.Items.Item("txtcdate").Specific.string = objrs.Fields.Item("TicketDate").Value.ToString
                EditText23.Value = objrs.Fields.Item("U_noofday").Value.ToString
                EditText24.Value = objrs.Fields.Item("U_eligiamt").Value.ToString
                EditText20.Value = objrs.Fields.Item("U_Total").Value.ToString

                Final_total()
                objform.Update()
                objform.Refresh()
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Clear_Airticketissue_details()
            EditText46.Value = ""
            objform.Items.Item("txtcdate").Specific.string = ""
            EditText23.Value = 0
            EditText24.Value = 0
            EditText20.Value = 0

            EditText34.Value = 0.0
            Final_total()
        End Sub

        Private Sub Button7_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button7.ClickAfter
            Try
                If EditText1.Value.ToString = "" Then Exit Sub
                Link_objtype = "OTIS_AN"
                Link_Value = EditText1.Value
                Dim activeform As New frmAirTicketIssue
                activeform.Show()
            Catch ex As Exception

            End Try
        End Sub

#End Region

#Region "LinK Button CLick"

        Private Sub LinkedButton0_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles LinkedButton0.ClickAfter
            Try
                If EditText2.Value = "" Then Exit Sub
                Link_Value = EditText2.Value
                Link_objtype = "OHEM"
                Dim activeform As New frmEmployeeMaster
                activeform.Show()
            Catch ex As Exception
            End Try
        End Sub

        Private Sub LinkedButton1_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles LinkedButton1.ClickAfter
            Try
                If EditText9.Value = "" Then Exit Sub
                Link_Value = EditText9.Value
                Link_objtype = "OLVA"
                Dim activeform As New frmLeaveApplicaiton
                activeform.Show()
            Catch ex As Exception

            End Try
        End Sub

        Private Sub LinkedButton2_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles LinkedButton2.ClickAfter
            Try
                If EditText21.Value = "" Then Exit Sub
                Link_Value = EditText21.Value
                Link_objtype = "OTIS"
                Dim activeform As New frmAirTicketIssue
                activeform.Show()
            Catch ex As Exception

            End Try
        End Sub

#End Region

#Region "Advance Salary Splitup"

        Private Sub Button2_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button2.ClickAfter
            Try
                If EditText2.Value.ToString = "" Then objaddon.objapplication.SetStatusBarMessage("Employee Details is Missing", SAPbouiCOM.BoMessageTime.bmt_Medium, True) : Exit Sub
                If Val(EditText48.Value) <= 0 Then objaddon.objapplication.SetStatusBarMessage("Salary Details Missing for the Selected Employee.Please Check...", SAPbouiCOM.BoMessageTime.bmt_Medium, True) : Exit Sub
                If objform.Items.Item("txtsfdt").Specific.string = "" Or objform.Items.Item("txtstdt").Specific.string = "" Then objaddon.objapplication.SetStatusBarMessage("From or To Date is Missing.", SAPbouiCOM.BoMessageTime.bmt_Medium, True) : Exit Sub

                Dim Fromdate As Date = Date.ParseExact(EditText25.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo) 'EditText25.String
                Dim todate As Date = Date.ParseExact(EditText26.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo) 'EditText26.String
                Dim LastDayInMonthDate As Date
                Dim total As Double = 0.0
                If Fromdate > todate Then objaddon.objapplication.SetStatusBarMessage("From Date should be less than or equal to To Date", SAPbouiCOM.BoMessageTime.bmt_Medium, True) : Exit Sub

                strsql = "SELECT IFNULL(CAST(MAX(""U_ToDate"") AS varchar), '') AS ""Todate"" FROM ""@SMPR_OPRC"" WHERE IFNULL(""U_Process"", 'N') = 'Y'and ""U_ToDate"">= '" & EditText25.Value & "'"
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strsql)
                If objrs.RecordCount = 0 Then
                    objaddon.objapplication.SetStatusBarMessage("Already Payroll proceesed for the selected Period", SAPbouiCOM.BoMessageTime.bmt_Short, True) : Exit Sub
                Else
                    If objrs.Fields.Item("Todate").Value.ToString <> "" Then objaddon.objapplication.SetStatusBarMessage("Already Payroll proceesed upto : " & objrs.Fields.Item("Todate").Value.ToString, SAPbouiCOM.BoMessageTime.bmt_Short, True) : Exit Sub
                End If

                objform.Freeze(True)

                EditText27.Value = 0
                For i As Integer = Matrix0.VisualRowCount To 1 Step -1 : Matrix0.DeleteRow(i) : Next

                For i As Integer = 0 To 100
                    Matrix0.AddRow(1)
                    LastDayInMonthDate = New Date(Fromdate.Year, Fromdate.Month, Date.DaysInMonth(Fromdate.Year, Fromdate.Month))
                    If LastDayInMonthDate > todate Then LastDayInMonthDate = todate
                    Matrix0.Columns.Item("#").Cells.Item(Matrix0.VisualRowCount).Specific.string = Matrix0.VisualRowCount
                    Matrix0.Columns.Item("fromdt").Cells.Item(Matrix0.VisualRowCount).Specific.string = Fromdate.ToString("dd/MM/yy")

                    Matrix0.Columns.Item("todt").Cells.Item(Matrix0.VisualRowCount).Specific.string = LastDayInMonthDate.ToString("dd/MM/yy")
                    Matrix0.Columns.Item("noofday").Cells.Item(Matrix0.VisualRowCount).Specific.string = DateDiff(DateInterval.Day, Fromdate, LastDayInMonthDate) + 1
                    Matrix0.Columns.Item("amount").Cells.Item(Matrix0.VisualRowCount).Specific.string = ((DateDiff(DateInterval.Day, Fromdate, LastDayInMonthDate) + 1) * (EditText48.Value / Date.DaysInMonth(Fromdate.Year, Fromdate.Month)))
                    total = total + ((DateDiff(DateInterval.Day, Fromdate, LastDayInMonthDate) + 1) * (EditText48.Value / Date.DaysInMonth(Fromdate.Year, Fromdate.Month)))
                    Fromdate = DateAdd(DateInterval.Day, 1, LastDayInMonthDate)
                    If Fromdate > todate Then Exit For
                Next
                EditText27.Value = total
                Final_total()

                Matrix0.Item.Update()

                objform.Update()
                objform.Refresh()
                objform.Freeze(False)
            Catch ex As Exception
                objform.Freeze(False)
            End Try
        End Sub

        Private Sub Advance_SalaryDetails_Clearing()
            Try
                EditText25.Value = ""
                EditText26.Value = ""
                EditText27.Value = 0.0
                For i As Integer = Matrix0.VisualRowCount To 1 Step -1 : Matrix0.DeleteRow(i) : Next

            Catch ex As Exception

            End Try
        End Sub
#End Region

#Region "Loan Details Part"

        Private Sub loanDetails_Clearing()
            Try
                For i As Integer = Matrix2.VisualRowCount To 1 Step -1 : Matrix2.DeleteRow(i) : Next
                EditText28.Value = 0.0
            Catch ex As Exception

            End Try
        End Sub

        Private Sub loanDetails_loading()
            Try

                If EditText2.Value.ToString = "" Then Exit Sub

                loanDetails_Clearing()

                'strsql = "Select T0.DocEntry,T0.Docnum,T1.LineId,dateName(MM,T1.U_date)Month,Datepart(yyyy,T1.U_Date)year,Replace(Convert(varchar,T1.U_date,103),'/','.')[Date],T1.U_Amount amount  "
                'strsql += vbCrLf + " from [@SMPR_OLOA] T0 inner join [@SMPR_LOA1] T1 on T0.DOcentry=T1.DOcentry "
                'strsql += vbCrLf + " where T0.U_empid='" & EditText2.Value.ToString & "' and U_approved='Y' and T0.canceled='N' and T1.U_status='O' and T0.status='O'"
                strsql = "SELECT T0.""DocEntry"", T0.""DocNum"", T1.""LineId"", MONTH(T1.""U_Date"") AS ""Month"",YEAR(T1.""U_Date"") AS ""year"", To_varchar(T1.""U_Date"",'yyyyMMdd') AS ""Date"",T1.""U_Amount"" AS ""amount""   "
                strsql += vbCrLf + " FROM ""@SMPR_OLOA"" T0 INNER JOIN ""@SMPR_LOA1"" T1 ON T0.""DocEntry"" = T1.""DocEntry"" "
                strsql += vbCrLf + " WHERE T0.""U_empID"" ='" & EditText2.Value.ToString & "' and ""U_Approved"" = 'Y' and T1.""U_Status"" = 'O' "
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery(strsql)

                If objrs.RecordCount = 0 Then Exit Sub

                For i As Integer = 1 To objrs.RecordCount
                    Matrix2.AddRow(1)
                    Matrix2.Columns.Item("#").Cells.Item(Matrix2.VisualRowCount).Specific.string = Matrix2.VisualRowCount

                    Matrix2.Columns.Item("loanno").Cells.Item(Matrix2.VisualRowCount).Specific.string = objrs.Fields.Item("Docnum").Value.ToString
                    Matrix2.Columns.Item("loanent").Cells.Item(Matrix2.VisualRowCount).Specific.string = objrs.Fields.Item("DocEntry").Value.ToString
                    Matrix2.Columns.Item("lineno").Cells.Item(Matrix2.VisualRowCount).Specific.string = objrs.Fields.Item("LineId").Value.ToString
                    Matrix2.Columns.Item("month").Cells.Item(Matrix2.VisualRowCount).Specific.string = objrs.Fields.Item("Month").Value.ToString
                    Matrix2.Columns.Item("year").Cells.Item(Matrix2.VisualRowCount).Specific.string = objrs.Fields.Item("year").Value.ToString
                    Dim FDate As Date = Date.ParseExact(objrs.Fields.Item("Date").Value.ToString, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                    Matrix2.Columns.Item("date").Cells.Item(Matrix2.VisualRowCount).Specific.string = FDate.ToString("dd/MM/yy") 'objrs.Fields.Item("Date").Value.ToString
                    Matrix2.Columns.Item("amount").Cells.Item(Matrix2.VisualRowCount).Specific.string = objrs.Fields.Item("amount").Value.ToString

                    objrs.MoveNext()
                Next
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Matrix2_ClickAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix2.ClickAfter
            Try
                Dim chkselect As SAPbouiCOM.CheckBox
                If pVal.ColUID = "chkselect" Then
                    Dim loan_total As Double = 0.0
                    For i As Integer = 1 To Matrix2.VisualRowCount
                        chkselect = Matrix2.Columns.Item("chkselect").Cells.Item(i).Specific
                        If chkselect.Checked = True Then loan_total = loan_total + Matrix2.Columns.Item("amount").Cells.Item(i).Specific.string
                    Next
                    EditText28.Value = 0.0 - loan_total
                    Final_total()
                    objform.Update()
                    objform.Refresh()
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Matrix2_KeyDownAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix2.KeyDownAfter
            Try
                Dim chkselect As SAPbouiCOM.CheckBox
                If pVal.ColUID = "chkselect" Then
                    Dim loan_total As Double = 0.0
                    For i As Integer = 1 To Matrix2.VisualRowCount
                        chkselect = Matrix2.Columns.Item("chkselect").Cells.Item(i).Specific
                        If chkselect.Checked = True Then loan_total = loan_total + Matrix2.Columns.Item("amount").Cells.Item(i).Specific.string
                    Next
                    EditText28.Value = 0.0 - loan_total
                    Final_total()
                    objform.Update()
                    objform.Refresh()
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Button4_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button4.ClickAfter
            'mtloan
            Try
                If objaddon.objapplication.MessageBox("All Unselected Lines will be Removed from the List. Press Yes to Continue", 1, "Yes", "No") = 2 Then Exit Sub
                objform.Freeze(True)
                objaddon.objapplication.SetStatusBarMessage("Removing un Selected Loan Details. Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Long, False)

                Remove_unselected_loandetails()

                objform.Freeze(False)
                objaddon.objapplication.SetStatusBarMessage("UnSelected Loan Details Removed Successfully.", SAPbouiCOM.BoMessageTime.bmt_Long, False)
            Catch ex As Exception
                objform.Freeze(False)
            End Try
        End Sub

        Private Sub Remove_unselected_loandetails()
            Dim chkselect As SAPbouiCOM.CheckBox
            Dim loan_total As Double = 0.0
            For i As Integer = Matrix2.VisualRowCount To 1 Step -1
                chkselect = Matrix2.Columns.Item("chkselect").Cells.Item(i).Specific
                If chkselect.Checked = True Then
                    loan_total = loan_total + Matrix2.Columns.Item("amount").Cells.Item(i).Specific.string
                Else
                    Matrix2.DeleteRow(i)
                End If
            Next
            EditText28.Value = 0.0 - loan_total
            Final_total()

            For i As Integer = 1 To Matrix2.VisualRowCount : Matrix2.Columns.Item("#").Cells.Item(i).Specific.String = i : Next

            objform.Update()
            objform.Refresh()
        End Sub

        Private Sub Button5_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button5.ClickAfter
            Try
                If Matrix2.VisualRowCount > 1 Then If objaddon.objapplication.MessageBox("Reload will clear the previous Selected details? Press Yes to continue", 1, "Yes", "No") = 2 Then Exit Sub
                objform.Freeze(True)
                objaddon.objapplication.SetStatusBarMessage("Refresing Loan Details. Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Long, False)
                loanDetails_loading()
                objform.Freeze(False)
                objaddon.objapplication.SetStatusBarMessage("Loan Details Loaded Successfully.", SAPbouiCOM.BoMessageTime.bmt_Long, False)
            Catch ex As Exception
                objform.Freeze(False)
            End Try
        End Sub

#End Region

#Region "Addition & Deduction Part"

        Private Sub calculate_total_Addition_Deduction()
            Try

                Dim addded_total As Double = 0.0
                Dim cmbtype As SAPbouiCOM.ComboBox
                For i As Integer = 1 To Matrix1.VisualRowCount
                    cmbtype = Matrix1.Columns.Item("mode").Cells.Item(i).Specific
                    If cmbtype.Selected Is Nothing Then Continue For
                    If cmbtype.Selected.Value.ToString.ToUpper = "A" Then
                        addded_total = addded_total + Matrix1.Columns.Item("amount").Cells.Item(i).Specific.string
                    Else
                        addded_total = addded_total - Matrix1.Columns.Item("amount").Cells.Item(i).Specific.string
                    End If
                Next
                EditText29.Value = addded_total
                Final_total()

                objform.Update()
                objform.Refresh()
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Matrix1_ComboSelectAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix1.ComboSelectAfter
            'If pVal.ColUID = "mode" Then
            '    Dim objcombotypte As SAPbouiCOM.ComboBox
            '    Dim objcombomode As SAPbouiCOM.ComboBox
            '    objcombotypte = Matrix1.Columns.Item("type").Cells.Item(pVal.Row).Specific
            '    objcombomode = Matrix1.Columns.Item("mode").Cells.Item(pVal.Row).Specific

            '    If objcombotypte.ValidValues.Count > 0 Then
            '        For i As Integer = objcombotypte.ValidValues.Count - 1 To 0 Step -1 : objcombotypte.ValidValues.Remove(i, SAPbouiCOM.BoSearchKey.psk_Index) : Next
            '    End If

            '    strsql = "select Distinct U_Sequence from [@SMPR_OPYE] Where U_Type='" & objcombomode.Selected.Value & "'"
            '    objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            '    objrs.DoQuery(strsql)
            '    If objrs.RecordCount = 0 Then Exit Sub
            '    For i As Integer = 0 To objrs.RecordCount - 1
            '        objcombotypte.ValidValues.Add(objrs.Fields.Item(0).Value.ToString, objrs.Fields.Item(0).Value.ToString)
            '        objrs.MoveNext()
            '    Next

            'End If
        End Sub

        Private Sub Matrix1_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix1.LostFocusAfter
            If pVal.ColUID = "amount" Or pVal.ColUID = "mode" Then
                Dim cmbtype As SAPbouiCOM.ComboBox
                calculate_total_Addition_Deduction()
                cmbtype = Matrix1.Columns.Item("mode").Cells.Item(Matrix1.VisualRowCount).Specific
                If cmbtype.Selected Is Nothing Then Exit Sub
                If Val(Matrix1.Columns.Item("amount").Cells.Item(Matrix1.VisualRowCount).Specific.string) = 0 Then Exit Sub
                Matrix1.AddRow(1)
                Matrix1.ClearRowData(Matrix1.VisualRowCount)
                Matrix1.Columns.Item("#").Cells.Item(Matrix1.VisualRowCount).Specific.string = Matrix1.VisualRowCount
            End If
        End Sub

        Private Sub Addition_Deduction_ClearingData()

            For i As Integer = Matrix1.VisualRowCount To 1 Step -1 : Matrix1.DeleteRow(i) : Next
            EditText29.Value = 0.0

        End Sub

#End Region

#Region "Gratuity"

        Private Sub Load_Gratuity()
            Try
                Dim objrs1 As SAPbobsCOM.Recordset
                If EditText0.Value = "" Or EditText2.Value = "" Then Exit Sub

                strsql = "CALL ""MIPL_HRMS_Grauity_Settlement"" ('" & EditText0.Value & "','" & EditText2.Value & "')"
                objrs1 = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs1.DoQuery(strsql)
                If objrs1.RecordCount = 0 Then Exit Sub
                EditText49.Value = objrs1.Fields.Item("Totaldays").Value
                EditText50.Value = objrs1.Fields.Item("LOP").Value
                EditText51.Value = objrs1.Fields.Item("Working Days").Value
                EditText53.Value = objrs1.Fields.Item("basic").Value
                EditText52.Value = objrs1.Fields.Item("Gratuity Days").Value
                EditText54.Value = objrs1.Fields.Item("Gratuity Amount").Value

                objform.Update()
                objform.Refresh()

            Catch ex As Exception

            End Try
        End Sub

        Private Sub Clear_Gratuity()
            Try
                EditText49.Value = 0.0
                EditText50.Value = 0.0
                EditText51.Value = 0.0
                EditText53.Value = 0.0
                EditText52.Value = 0.0
                EditText54.Value = 0.0
                EditText30.Value = 0.0
            Catch ex As Exception

            End Try
        End Sub

        Private Sub EditText0_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText0.LostFocusAfter
            If Not ComboBox2.Selected Is Nothing Then
                If ComboBox2.Selected.Value = "FF" Then Load_Gratuity() 'Gratutiy Calculation
            End If
        End Sub
#End Region

#Region "Pay Details"

        Private Sub Load_Paydetails()
            Try
                If EditText2.Value = "" Then Exit Sub

                For i As Integer = Matrix3.VisualRowCount To 1 Step -1 : Matrix3.DeleteRow(i) : Next

                Dim objpay As SAPbobsCOM.Recordset
                strsql = "  SELECT T1.""U_PayElCod"", T1.""U_PayElNam"", T1.""U_Amount"" FROM ""@SMPR_OHEM"" T0 INNER JOIN ""@SMPR_HEM1"" T1 ON T0.""Code"" = T1.""Code"" WHERE IFNULL(""U_LveSettlement"", 'N') = 'Y' AND IFNULL(T0.""U_empID"", '')='" & EditText2.Value & "'"
                objpay = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objpay.DoQuery(strsql)
                If objpay.RecordCount = 0 Then Exit Sub

                For i As Integer = 1 To objpay.RecordCount
                    Matrix3.AddRow(1)
                    Matrix3.Columns.Item("#").Cells.Item(i).Specific.string = i
                    Matrix3.Columns.Item("pcode").Cells.Item(i).Specific.string = objpay.Fields.Item("U_PayElCod").Value.ToString
                    Matrix3.Columns.Item("pname").Cells.Item(i).Specific.string = objpay.Fields.Item("U_PayElNam").Value.ToString
                    Matrix3.Columns.Item("amount").Cells.Item(i).Specific.string = objpay.Fields.Item("U_Amount").Value.ToString
                    objpay.MoveNext()
                Next
                Matrix3.Columns.Item("amount").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Clear_Paydetails()
            Try
                For i As Integer = Matrix3.VisualRowCount To 1 Step -1 : Matrix3.DeleteRow(i) : Next
            Catch ex As Exception

            End Try
        End Sub
#End Region

        Private Sub Final_total()
            Try
                EditText31.Value = Math.Round((Val(EditText19.Value) + Val(EditText14.Value) + Val(EditText20.Value) + Val(EditText27.Value) + Val(EditText28.Value) + Val(EditText29.Value) + Val(EditText54.Value)), 2)
                objform.Update()
                objform.Refresh()
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Button0_ClickBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button0.ClickBefore
            Try
                'If objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE Then objaddon.objapplication.SetStatusBarMessage("Update Not Allowed", SAPbouiCOM.BoMessageTime.bmt_Short, True) : BubbleEvent = False : Exit Sub
                If objform.Mode <> SAPbouiCOM.BoFormMode.fm_ADD_MODE Then Exit Sub
                objform.Freeze(True)
                Remove_unselected_loandetails()
                Dim ocombo As SAPbouiCOM.ComboBox
                If Matrix1.RowCount > 0 Then
                    Folder3.Item.Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                    Matrix1.SetCellFocus(1, 3)
                    objform.ActiveItem = "Item_85"
                    ocombo = Matrix1.Columns.Item("mode").Cells.Item(Matrix1.VisualRowCount).Specific
                    If Val(Matrix1.Columns.Item("amount").Cells.Item(Matrix1.VisualRowCount).Specific.string) = 0 And ocombo.Selected Is Nothing Then
                        Matrix1.DeleteRow(Matrix1.VisualRowCount)
                    End If
                    Folder0.Item.Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                End If
                objform.Freeze(False)
            Catch ex As Exception
                objform.Freeze(False)
                BubbleEvent = False
            End Try
        End Sub

        Private Sub Matrix2_LinkPressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix2.LinkPressedAfter
            If pVal.ColUID = "loanent" Then
                If Matrix2.Columns.Item("loanent").Cells.Item(pVal.Row).Specific.String <> "" Then
                    Link_objtype = "OLOA"
                    Link_Value = Matrix2.Columns.Item("loanent").Cells.Item(pVal.Row).Specific.String
                    Dim oactiveform As New frmLoanApplication
                    oactiveform.Show()
                End If
            End If

        End Sub

        Private Sub FrmSettlment_DataLoadAfter(ByRef pVal As SAPbouiCOM.BusinessObjectInfo) Handles Me.DataLoadAfter
            Try
                objaddon.objglobalmethods.LoadCombo_SingleSeries_AfterFind(objform, "cmbseries", "OLSE", ComboBox0.Value)
                If objaddon.ApprovedUser() Then
                    If CheckBox0.Checked = True Then
                        CheckBox0.Item.Enabled = False
                    Else
                        CheckBox0.Item.Enabled = True
                    End If
                End If
                If ComboBox2.Selected Is Nothing Then Exit Sub
                If objform.Mode <> SAPbouiCOM.BoFormMode.fm_ADD_MODE Then Exit Sub
                If ComboBox2.Selected.Value = "LS" Then
                    objform.Freeze(True)
                    Folder4.Item.Visible = False
                    StaticText32.Item.Visible = False
                    EditText30.Item.Visible = False
                    EditText0.Item.Enabled = True
                    EditText15.Item.Enabled = True
                    objform.Title = "Leave Settlement"
                    objform.Freeze(False)
                Else
                    objform.Freeze(True)
                    Folder4.Item.Visible = True
                    StaticText32.Item.Visible = True
                    EditText30.Item.Visible = True
                    EditText0.Item.Enabled = False
                    EditText15.Item.Enabled = False
                    objform.Title = "Final Settlement"
                    objform.Freeze(False)
                End If

            Catch ex As Exception
                objform.Freeze(False)
            End Try
        End Sub

        Private Sub EditText36_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText36.LostFocusAfter
            Try
                objaddon.objglobalmethods.LoadCombo_Series(objform, "cmbseries", "OLSE", IIf(EditText36.String = "", Now.Date, Date.ParseExact(EditText36.Value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)))
            Catch ex As Exception
            End Try
        End Sub

        Private Sub Button0_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button0.PressedAfter
            Try
                If pVal.ActionSuccess = True And objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                    If addupdate = True Then objaddon.objapplication.StatusBar.SetText("Settlement Document Added and Document Sent for Approval", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                    ComboBox2.Select("LS", SAPbouiCOM.BoSearchKey.psk_ByValue)
                    objform.Items.Item("txtdate").Specific.string = Now.Date.ToString("yyyyMMdd")
                    objform.Items.Item("txtdocdt").Specific.string = Now.Date.ToString("yyyyMMdd")
                    EditText44.Value = objaddon.objglobalmethods.GetNextDocentry_Value("@SMPR_OLSE")
                    objform.ActiveItem = "txttrzid"
                ElseIf pVal.ActionSuccess = True And objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then
                    If addupdate = True Then objaddon.objapplication.StatusBar.SetText("Settlement Document Updated and Document Sent for Approval", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
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
        Private WithEvents LinkedButton3 As SAPbouiCOM.LinkedButton
        Private WithEvents StaticText52 As SAPbouiCOM.StaticText
        Private WithEvents EditText6 As SAPbouiCOM.EditText
        Private WithEvents LinkedButton4 As SAPbouiCOM.LinkedButton

        Private Sub LinkedButton3_ClickBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles LinkedButton3.ClickBefore
            If EditText45.Value = "" Or EditText45.Value.ToString = "-1" Then BubbleEvent = False
        End Sub

        Private Sub LinkedButton4_ClickBefore(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As System.Boolean) Handles LinkedButton4.ClickBefore
            If EditText6.Value = "" Or EditText6.Value.ToString = "-1" Then BubbleEvent = False
        End Sub
        Private WithEvents StaticText53 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox4 As SAPbouiCOM.ComboBox
        Private WithEvents EditText37 As SAPbouiCOM.EditText
        Private WithEvents StaticText54 As SAPbouiCOM.StaticText
        Private WithEvents EditText43 As SAPbouiCOM.EditText
        Private WithEvents StaticText55 As SAPbouiCOM.StaticText

        Dim posted_entryno As String
        Dim lretcode
        Private Sub LeaveSettlement()
            Try
                Dim objrsheader As SAPbobsCOM.Recordset
                objrsheader = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                Dim strquery As String = ""
                Dim FDate As Date = Date.ParseExact(objform.Items.Item("txtdocdt").Specific.String, "dd/MM/yy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                'For intheader As Integer = 0 To objrsheader.RecordCount - 1
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                If EditText6.Value <> "" Then Exit Sub
                strsql = "Select 'OLSE' ""Transcode"",'" & ComboBox2.Selected.Description & "' ""Memo"","
                strsql += vbCrLf + "'" & ComboBox2.Selected.Description & " - Entry No : " & objform.Items.Item("txtentry").Specific.string & " Appl No: " & objform.Items.Item("txtdocno").Specific.string & " : Remarks:" & objform.Items.Item("Item_85").Specific.string & " ' ""Narration"","
                strsql += vbCrLf + "'Employee No : " & objform.Items.Item("txtempid").Specific.string & " ID : " & objform.Items.Item("txttrzid").Specific.string & " ' ""Ref1"",'" & objform.Items.Item("txtename").Specific.string & "' ""Ref2"",'" & ComboBox2.Selected.Description & "' ""Ref3"" from dummy"
                objrs.DoQuery(strsql)

                strquery = "CALL ""MIPL_GetSettlementJEAccount"" ('" & FDate.ToString("yyyyMMdd") & "') "
                objrsheader.DoQuery(strquery)
                If objrs.RecordCount > 0 Then
                    Try
                        If objrsheader.RecordCount = 0 Then objaddon.objapplication.SetStatusBarMessage("Account Mapping Required for this month", SAPbouiCOM.BoMessageTime.bmt_Short, True) : objform.Freeze(False) : Exit Sub
                        Dim stat As Boolean = False
                        For i As Integer = 0 To objrsheader.RecordCount - 1
                            If objrsheader.Fields.Item("DebitCode").Value = "" Or objrsheader.Fields.Item("CreditCode").Value = "" Then
                                objaddon.objapplication.SetStatusBarMessage("Account Mapping Required for the settlement Type : " & objrsheader.Fields.Item("Type").Value, SAPbouiCOM.BoMessageTime.bmt_Long, True)
                                stat = True
                            End If
                            objrsheader.MoveNext()
                        Next
                        If stat = True Then Exit Sub
                        objrsheader.MoveFirst()
                        If Not objaddon.objcompany.InTransaction Then objaddon.objcompany.StartTransaction()

                        'Dim osettlementjv As SAPbobsCOM.JournalVouchers
                        'osettlementjv = objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalVouchers)
                        Dim osettlementjv As SAPbobsCOM.JournalEntries
                        osettlementjv = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries)

                        osettlementjv.ReferenceDate = FDate.ToString("yyyy/MM/dd") 'objrs.Fields.Item("Date").Value
                        osettlementjv.DueDate = FDate.ToString("yyyy/MM/dd") 'objrs.Fields.Item("Date").Value
                        osettlementjv.TaxDate = FDate.ToString("yyyy/MM/dd") 'objrs.Fields.Item("Date").Value
                        'If objrs.Fields.Item("Transcode").Value.ToString <> "" Then osettlementjv.TransactionCode = objrs.Fields.Item("Transcode").Value.ToString

                        If objrs.Fields.Item("Memo").Value.ToString <> "" Then osettlementjv.Memo = objrs.Fields.Item("Memo").Value.ToString
                        If objrs.Fields.Item("Narration").Value.ToString <> "" Then osettlementjv.UserFields.Fields.Item("U_Narration").Value = objrs.Fields.Item("Narration").Value.ToString

                        If objrs.Fields.Item("Ref1").Value.ToString <> "" Then osettlementjv.Reference = objrs.Fields.Item("Ref1").Value.ToString
                        If objrs.Fields.Item("Ref2").Value.ToString <> "" Then osettlementjv.Reference2 = objrs.Fields.Item("Ref2").Value.ToString
                        If objrs.Fields.Item("Ref3").Value.ToString <> "" Then osettlementjv.Reference3 = objrs.Fields.Item("Ref3").Value.ToString

                        For i As Integer = 0 To objrsheader.RecordCount - 1
                            If objrsheader.RecordCount > 0 Then
                                Select Case objrsheader.Fields.Item("Type").Value.ToString.ToUpper
                                    Case "LEAVE SALARY"
                                        If CDbl(objform.Items.Item("Item_79").Specific.string) <> 0 Then
                                            osettlementjv.Lines.AccountCode = objrsheader.Fields.Item("DebitCode").Value
                                            osettlementjv.Lines.Debit = CDbl(objform.Items.Item("Item_79").Specific.string) 'objrs.Fields.Item("DebitAmount").Value
                                            osettlementjv.Lines.Credit = 0 ' objrs.Fields.Item("CreditAmount").Value
                                            osettlementjv.Lines.Add()
                                            osettlementjv.Lines.AccountCode = objrsheader.Fields.Item("CreditCode").Value
                                            osettlementjv.Lines.Debit = 0 'objrs.Fields.Item("DebitAmount").Value
                                            osettlementjv.Lines.Credit = CDbl(objform.Items.Item("Item_79").Specific.string) ' objrs.Fields.Item("CreditAmount").Value
                                        End If
                                    Case "LEAVE ENCASHMENT"
                                        If CDbl(objform.Items.Item("Item_81").Specific.string) <> 0 Then
                                            osettlementjv.Lines.AccountCode = objrsheader.Fields.Item("DebitCode").Value
                                            osettlementjv.Lines.Debit = CDbl(objform.Items.Item("Item_81").Specific.string) 'objrs.Fields.Item("DebitAmount").Value
                                            osettlementjv.Lines.Credit = 0 ' objrs.Fields.Item("CreditAmount").Value
                                            osettlementjv.Lines.Add()
                                            osettlementjv.Lines.AccountCode = objrsheader.Fields.Item("CreditCode").Value
                                            osettlementjv.Lines.Debit = 0 'objrs.Fields.Item("DebitAmount").Value
                                            osettlementjv.Lines.Credit = CDbl(objform.Items.Item("Item_81").Specific.string) ' objrs.Fields.Item("CreditAmount").Value
                                        End If
                                    Case "AIR TICKET CLAIM"
                                        If CDbl(objform.Items.Item("Item_83").Specific.string) <> 0 Then
                                            osettlementjv.Lines.AccountCode = objrsheader.Fields.Item("DebitCode").Value
                                            osettlementjv.Lines.Debit = CDbl(objform.Items.Item("Item_83").Specific.string) 'objrs.Fields.Item("DebitAmount").Value
                                            osettlementjv.Lines.Credit = 0 ' objrs.Fields.Item("CreditAmount").Value
                                            osettlementjv.Lines.Add()
                                            osettlementjv.Lines.AccountCode = objrsheader.Fields.Item("CreditCode").Value
                                            osettlementjv.Lines.Debit = 0 'objrs.Fields.Item("DebitAmount").Value
                                            osettlementjv.Lines.Credit = CDbl(objform.Items.Item("Item_83").Specific.string) ' objrs.Fields.Item("CreditAmount").Value
                                        End If
                                    Case "ADVANCE SALARY"
                                        If CDbl(objform.Items.Item("txttads").Specific.string) <> 0 Then
                                            osettlementjv.Lines.AccountCode = objrsheader.Fields.Item("DebitCode").Value
                                            osettlementjv.Lines.Debit = CDbl(objform.Items.Item("txttads").Specific.string) 'objrs.Fields.Item("DebitAmount").Value
                                            osettlementjv.Lines.Credit = 0 ' objrs.Fields.Item("CreditAmount").Value
                                            osettlementjv.Lines.Add()
                                            osettlementjv.Lines.AccountCode = objrsheader.Fields.Item("CreditCode").Value
                                            osettlementjv.Lines.Debit = 0 'objrs.Fields.Item("DebitAmount").Value
                                            osettlementjv.Lines.Credit = CDbl(objform.Items.Item("txttads").Specific.string) ' objrs.Fields.Item("CreditAmount").Value
                                        End If
                                    Case "GRATUITY"
                                        If CDbl(objform.Items.Item("txtGratu").Specific.string) <> 0 Then
                                            osettlementjv.Lines.AccountCode = objrsheader.Fields.Item("DebitCode").Value
                                            osettlementjv.Lines.Debit = CDbl(objform.Items.Item("txtGratu").Specific.string) 'objrs.Fields.Item("DebitAmount").Value
                                            osettlementjv.Lines.Credit = 0 ' objrs.Fields.Item("CreditAmount").Value
                                            osettlementjv.Lines.Add()
                                            osettlementjv.Lines.AccountCode = objrsheader.Fields.Item("CreditCode").Value
                                            osettlementjv.Lines.Debit = 0 'objrs.Fields.Item("DebitAmount").Value
                                            osettlementjv.Lines.Credit = CDbl(objform.Items.Item("txtGratu").Specific.string) ' objrs.Fields.Item("CreditAmount").Value
                                        End If
                                    Case "LOAN DEDUCTION"
                                        If CDbl(objform.Items.Item("txtadvde").Specific.string) <> 0 Then
                                            osettlementjv.Lines.AccountCode = objrsheader.Fields.Item("DebitCode").Value
                                            osettlementjv.Lines.Debit = CDbl(objform.Items.Item("txtadvde").Specific.string)
                                            osettlementjv.Lines.Credit = 0 ' objrs.Fields.Item("CreditAmount").Value
                                            osettlementjv.Lines.Add()
                                            osettlementjv.Lines.AccountCode = objrsheader.Fields.Item("CreditCode").Value
                                            osettlementjv.Lines.Debit = 0 'objrs.Fields.Item("DebitAmount").Value
                                            osettlementjv.Lines.Credit = CDbl(objform.Items.Item("txtadvde").Specific.string)
                                        End If
                                    Case "ADDITION/DEDUCTION"
                                        If CDbl(objform.Items.Item("txttadde").Specific.string) <> 0 Then
                                            osettlementjv.Lines.AccountCode = objrsheader.Fields.Item("DebitCode").Value
                                            osettlementjv.Lines.Debit = CDbl(objform.Items.Item("txttadde").Specific.string)
                                            osettlementjv.Lines.Credit = 0 ' objrs.Fields.Item("CreditAmount").Value
                                            osettlementjv.Lines.Add()
                                            osettlementjv.Lines.AccountCode = objrsheader.Fields.Item("CreditCode").Value
                                            osettlementjv.Lines.Debit = 0 'objrs.Fields.Item("DebitAmount").Value
                                            osettlementjv.Lines.Credit = CDbl(objform.Items.Item("txttadde").Specific.string)
                                        End If
                                End Select

                                'osettlementjv.Lines.Reference1 = objrs.Fields.Item("Lref1").Value
                                'osettlementjv.Lines.Reference2 = objrs.Fields.Item("Lref2").Value
                                'osettlementjv.Lines.AdditionalReference = objrs.Fields.Item("Lref3").Value

                                'If objrs.Fields.Item("Ocrcode1").Value.ToString <> "" Then osettlementjv.Lines.CostingCode = objrs.Fields.Item("Ocrcode1").Value
                                'If objrs.Fields.Item("Ocrcode2").Value.ToString <> "" Then osettlementjv.Lines.CostingCode2 = objrs.Fields.Item("Ocrcode2").Value
                                'If objrs.Fields.Item("Ocrcode3").Value.ToString <> "" Then osettlementjv.Lines.CostingCode3 = objrs.Fields.Item("Ocrcode3").Value
                                'If objrs.Fields.Item("Ocrcode4").Value.ToString <> "" Then osettlementjv.Lines.CostingCode4 = objrs.Fields.Item("Ocrcode4").Value
                                'If objrs.Fields.Item("Ocrcode5").Value.ToString <> "" Then osettlementjv.Lines.CostingCode5 = objrs.Fields.Item("Ocrcode5").Value

                                osettlementjv.Lines.Add()
                                objrsheader.MoveNext()
                            End If
                        Next
                        'osettlementjv.JournalEntries.Add()

                        lretcode = osettlementjv.Add()
                        If lretcode <> 0 Then
                            If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                            objaddon.objglobalmethods.status_Update("OLSE", objform.Items.Item("txtentry").Specific.string, 0, objaddon.objcompany.GetLastErrorDescription, -1)
                        Else
                            posted_entryno = objaddon.objcompany.GetNewObjectKey
                            objaddon.objglobalmethods.status_Update("OLSE", objform.Items.Item("txtentry").Specific.string, 1, "Success", posted_entryno.ToString)
                            If objaddon.objglobalmethods.Update_query("update ""@SMPR_OLSE"" set ""U_jeno""='" & posted_entryno & "' where ""DocEntry""='" & objform.Items.Item("txtentry").Specific.string & "'") Then
                                If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                                objaddon.objapplication.SetStatusBarMessage("Journal Entry Successfully Posted..." & posted_entryno, SAPbouiCOM.BoMessageTime.bmt_Long, False)
                                EditText6.Value = posted_entryno
                            Else
                                If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                            End If
                        End If

                    Catch ex As Exception
                        If objaddon.objcompany.InTransaction Then objaddon.objcompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                        objaddon.objglobalmethods.status_Update("OLSE", objform.Items.Item("txtentry").Specific.string, 0, objaddon.objcompany.GetLastErrorDescription, -1)
                    End Try

                End If
                objrsheader.MoveNext()

            Catch ex As Exception

            End Try
        End Sub

        Private Sub Button0_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button0.ClickAfter
            'Try
            '    If CheckBox0.Checked = True Then
            '        If ApprovedUser_Employee Then
            '            LeaveSettlement()
            '        Else
            '            objaddon.objapplication.SetStatusBarMessage("You are not authorized to post JE", SAPbouiCOM.BoMessageTime.bmt_Long, True)
            '            Exit Sub
            '        End If
            '    Else
            '        'objaddon.objapplication.SetStatusBarMessage("Please Tick the Finalize to Post JE", SAPbouiCOM.BoMessageTime.bmt_Long, False)
            '        Exit Sub
            '    End If
            'Catch ex As Exception

            'End Try
        End Sub


        Private Sub CheckBox0_PressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles CheckBox0.PressedAfter
            If CheckBox0.Checked = True Then
                CheckBox0.Item.Enabled = False
            End If

        End Sub


    End Class
End Namespace

