Option Strict Off
Option Explicit On

Imports SAPbouiCOM.Framework
Imports System.IO
Imports System.Linq

Namespace HRMS
    <FormAttribute("MSTREMPL", "Master/frmEmployeeMaster.b1f")>
    Friend Class frmEmployeeMaster
        Inherits UserFormBase
        Dim FormCount As Integer = 0
        Private WithEvents objform As SAPbouiCOM.Form
        Private WithEvents pCFL As SAPbouiCOM.ISBOChooseFromListEventArg
        Dim objrs As SAPbobsCOM.Recordset
        Private WithEvents odbdsSalDetails As SAPbouiCOM.DBDataSource
        Public Sub New()
        End Sub

        Public Overrides Sub OnInitializeComponent()
            Me.Button0 = CType(Me.GetItem("1").Specific, SAPbouiCOM.Button)
            Me.Button1 = CType(Me.GetItem("2").Specific, SAPbouiCOM.Button)
            Me.StaticText0 = CType(Me.GetItem("lblempid").Specific, SAPbouiCOM.StaticText)
            Me.EditText0 = CType(Me.GetItem("txtiempid").Specific, SAPbouiCOM.EditText)
            Me.StaticText1 = CType(Me.GetItem("lblfname").Specific, SAPbouiCOM.StaticText)
            Me.EditText1 = CType(Me.GetItem("txtfname").Specific, SAPbouiCOM.EditText)
            Me.StaticText2 = CType(Me.GetItem("lbllname").Specific, SAPbouiCOM.StaticText)
            Me.EditText2 = CType(Me.GetItem("txtlname").Specific, SAPbouiCOM.EditText)
            Me.EditText3 = CType(Me.GetItem("txtempid").Specific, SAPbouiCOM.EditText)
            Me.StaticText3 = CType(Me.GetItem("lblegroup").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox0 = CType(Me.GetItem("cmbegroup").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText4 = CType(Me.GetItem("lbldesign").Specific, SAPbouiCOM.StaticText)
            Me.EditText4 = CType(Me.GetItem("txtdesig").Specific, SAPbouiCOM.EditText)
            Me.StaticText5 = CType(Me.GetItem("lblposi").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox1 = CType(Me.GetItem("cmbposi").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText6 = CType(Me.GetItem("lbldept").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox2 = CType(Me.GetItem("cmbdept").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText7 = CType(Me.GetItem("lblbranch").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox3 = CType(Me.GetItem("cmbbranch").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText8 = CType(Me.GetItem("lblmanager").Specific, SAPbouiCOM.StaticText)
            Me.EditText5 = CType(Me.GetItem("txtmgrname").Specific, SAPbouiCOM.EditText)
            Me.StaticText9 = CType(Me.GetItem("lbluserid").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox4 = CType(Me.GetItem("cmbuserid").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText10 = CType(Me.GetItem("lblslp").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox5 = CType(Me.GetItem("cmbslp").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText11 = CType(Me.GetItem("lbloffph").Specific, SAPbouiCOM.StaticText)
            Me.EditText6 = CType(Me.GetItem("txtoffph").Specific, SAPbouiCOM.EditText)
            Me.StaticText12 = CType(Me.GetItem("lbloffext").Specific, SAPbouiCOM.StaticText)
            Me.EditText7 = CType(Me.GetItem("txtoffext").Specific, SAPbouiCOM.EditText)
            Me.StaticText13 = CType(Me.GetItem("lblmobile").Specific, SAPbouiCOM.StaticText)
            Me.EditText8 = CType(Me.GetItem("txtmobile").Specific, SAPbouiCOM.EditText)
            Me.StaticText14 = CType(Me.GetItem("lblnative").Specific, SAPbouiCOM.StaticText)
            Me.EditText9 = CType(Me.GetItem("txtnative").Specific, SAPbouiCOM.EditText)
            Me.StaticText15 = CType(Me.GetItem("lblhp").Specific, SAPbouiCOM.StaticText)
            Me.EditText10 = CType(Me.GetItem("txthp").Specific, SAPbouiCOM.EditText)
            Me.StaticText16 = CType(Me.GetItem("lblpe1").Specific, SAPbouiCOM.StaticText)
            Me.EditText11 = CType(Me.GetItem("txtpe1").Specific, SAPbouiCOM.EditText)
            Me.StaticText17 = CType(Me.GetItem("lblemail").Specific, SAPbouiCOM.StaticText)
            Me.EditText12 = CType(Me.GetItem("txtemail").Specific, SAPbouiCOM.EditText)
            Me.Folder0 = CType(Me.GetItem("fldper").Specific, SAPbouiCOM.Folder)
            Me.Folder1 = CType(Me.GetItem("fldcon").Specific, SAPbouiCOM.Folder)
            Me.StaticText18 = CType(Me.GetItem("lblstreet").Specific, SAPbouiCOM.StaticText)
            Me.EditText13 = CType(Me.GetItem("txtwst").Specific, SAPbouiCOM.EditText)
            Me.PictureBox1 = CType(Me.GetItem("img").Specific, SAPbouiCOM.PictureBox)
            Me.StaticText19 = CType(Me.GetItem("lblworkadd").Specific, SAPbouiCOM.StaticText)
            Me.StaticText20 = CType(Me.GetItem("lblnatadd").Specific, SAPbouiCOM.StaticText)
            Me.EditText14 = CType(Me.GetItem("txtnst").Specific, SAPbouiCOM.EditText)
            Me.StaticText21 = CType(Me.GetItem("lblstno").Specific, SAPbouiCOM.StaticText)
            Me.EditText15 = CType(Me.GetItem("txtwstno").Specific, SAPbouiCOM.EditText)
            Me.EditText16 = CType(Me.GetItem("txtnstno").Specific, SAPbouiCOM.EditText)
            Me.StaticText22 = CType(Me.GetItem("lblBlock").Specific, SAPbouiCOM.StaticText)
            Me.EditText17 = CType(Me.GetItem("txtwblock").Specific, SAPbouiCOM.EditText)
            Me.EditText18 = CType(Me.GetItem("txtnblock").Specific, SAPbouiCOM.EditText)
            Me.StaticText23 = CType(Me.GetItem("lblbld").Specific, SAPbouiCOM.StaticText)
            Me.EditText19 = CType(Me.GetItem("txtwbld").Specific, SAPbouiCOM.EditText)
            Me.EditText20 = CType(Me.GetItem("txtnbld").Specific, SAPbouiCOM.EditText)
            Me.StaticText24 = CType(Me.GetItem("lblzip").Specific, SAPbouiCOM.StaticText)
            Me.EditText21 = CType(Me.GetItem("txtwzip").Specific, SAPbouiCOM.EditText)
            Me.EditText22 = CType(Me.GetItem("txtnzip").Specific, SAPbouiCOM.EditText)
            Me.StaticText25 = CType(Me.GetItem("lblcity").Specific, SAPbouiCOM.StaticText)
            Me.EditText23 = CType(Me.GetItem("txtwcity").Specific, SAPbouiCOM.EditText)
            Me.EditText24 = CType(Me.GetItem("txtncity").Specific, SAPbouiCOM.EditText)
            Me.StaticText26 = CType(Me.GetItem("lblcounty").Specific, SAPbouiCOM.StaticText)
            Me.EditText25 = CType(Me.GetItem("txtwcounty").Specific, SAPbouiCOM.EditText)
            Me.EditText26 = CType(Me.GetItem("txtncounty").Specific, SAPbouiCOM.EditText)
            Me.StaticText27 = CType(Me.GetItem("lblstate").Specific, SAPbouiCOM.StaticText)
            Me.StaticText28 = CType(Me.GetItem("lblcntry").Specific, SAPbouiCOM.StaticText)
            Me.StaticText29 = CType(Me.GetItem("lblheader").Specific, SAPbouiCOM.StaticText)
            Me.Folder3 = CType(Me.GetItem("fldadd").Specific, SAPbouiCOM.Folder)
            Me.StaticText30 = CType(Me.GetItem("lblbirth").Specific, SAPbouiCOM.StaticText)
            Me.EditText31 = CType(Me.GetItem("txtbirth").Specific, SAPbouiCOM.EditText)
            Me.StaticText31 = CType(Me.GetItem("lblcounb").Specific, SAPbouiCOM.StaticText)
            Me.StaticText32 = CType(Me.GetItem("lblcitizen").Specific, SAPbouiCOM.StaticText)
            Me.StaticText33 = CType(Me.GetItem("lblmarst").Specific, SAPbouiCOM.StaticText)
            Me.StaticText34 = CType(Me.GetItem("lblnoofch").Specific, SAPbouiCOM.StaticText)
            Me.EditText35 = CType(Me.GetItem("txtnoofch").Specific, SAPbouiCOM.EditText)
            Me.StaticText35 = CType(Me.GetItem("lblgender").Specific, SAPbouiCOM.StaticText)
            Me.StaticText36 = CType(Me.GetItem("lblbldgr").Specific, SAPbouiCOM.StaticText)
            Me.EditText37 = CType(Me.GetItem("txtbldgr").Specific, SAPbouiCOM.EditText)
            Me.StaticText37 = CType(Me.GetItem("lblreligon").Specific, SAPbouiCOM.StaticText)
            Me.EditText38 = CType(Me.GetItem("txtreligon").Specific, SAPbouiCOM.EditText)
            Me.ComboBox6 = CType(Me.GetItem("cmbcounb").Specific, SAPbouiCOM.ComboBox)
            Me.ComboBox7 = CType(Me.GetItem("cmbcitizen").Specific, SAPbouiCOM.ComboBox)
            Me.ComboBox8 = CType(Me.GetItem("cmbgender").Specific, SAPbouiCOM.ComboBox)
            Me.ComboBox9 = CType(Me.GetItem("cmbmarst").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText38 = CType(Me.GetItem("lblpassno").Specific, SAPbouiCOM.StaticText)
            Me.EditText39 = CType(Me.GetItem("txtpassno").Specific, SAPbouiCOM.EditText)
            Me.StaticText42 = CType(Me.GetItem("lblpassexd").Specific, SAPbouiCOM.StaticText)
            Me.EditText40 = CType(Me.GetItem("txtpassexd").Specific, SAPbouiCOM.EditText)
            Me.StaticText44 = CType(Me.GetItem("lblpassidt").Specific, SAPbouiCOM.StaticText)
            Me.EditText41 = CType(Me.GetItem("txtpassidt").Specific, SAPbouiCOM.EditText)
            Me.StaticText45 = CType(Me.GetItem("lblpassisr").Specific, SAPbouiCOM.StaticText)
            Me.EditText42 = CType(Me.GetItem("txtpassisr").Specific, SAPbouiCOM.EditText)
            Me.Folder2 = CType(Me.GetItem("fldgeneral").Specific, SAPbouiCOM.Folder)
            Me.Folder14 = CType(Me.GetItem("fldpreemp").Specific, SAPbouiCOM.Folder)
            Me.StaticText39 = CType(Me.GetItem("lblstartdt").Specific, SAPbouiCOM.StaticText)
            Me.EditText32 = CType(Me.GetItem("txtstartdt").Specific, SAPbouiCOM.EditText)
            Me.StaticText41 = CType(Me.GetItem("lblstatus").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox10 = CType(Me.GetItem("cmbstatus").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText43 = CType(Me.GetItem("lblpbnmn").Specific, SAPbouiCOM.StaticText)
            Me.EditText34 = CType(Me.GetItem("txtpbnmn").Specific, SAPbouiCOM.EditText)
            Me.StaticText46 = CType(Me.GetItem("lblpbndt").Specific, SAPbouiCOM.StaticText)
            Me.EditText36 = CType(Me.GetItem("txtpbndt").Specific, SAPbouiCOM.EditText)
            Me.StaticText47 = CType(Me.GetItem("lblpbnedt").Specific, SAPbouiCOM.StaticText)
            Me.EditText43 = CType(Me.GetItem("txtpbnedt").Specific, SAPbouiCOM.EditText)
            Me.StaticText48 = CType(Me.GetItem("lblntpd").Specific, SAPbouiCOM.StaticText)
            Me.EditText44 = CType(Me.GetItem("txtntpd").Specific, SAPbouiCOM.EditText)
            Me.StaticText49 = CType(Me.GetItem("lblconend").Specific, SAPbouiCOM.StaticText)
            Me.EditText45 = CType(Me.GetItem("txtconend").Specific, SAPbouiCOM.EditText)
            Me.StaticText50 = CType(Me.GetItem("lblresgdt").Specific, SAPbouiCOM.StaticText)
            Me.EditText46 = CType(Me.GetItem("txtresgdt").Specific, SAPbouiCOM.EditText)
            Me.StaticText51 = CType(Me.GetItem("lbltermdt").Specific, SAPbouiCOM.StaticText)
            Me.EditText47 = CType(Me.GetItem("txttermdt").Specific, SAPbouiCOM.EditText)
            Me.StaticText52 = CType(Me.GetItem("lbltermre").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox11 = CType(Me.GetItem("txttermre").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText53 = CType(Me.GetItem("lblemexty").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox12 = CType(Me.GetItem("cmbemexty").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText54 = CType(Me.GetItem("Item_27").Specific, SAPbouiCOM.StaticText)
            Me.StaticText55 = CType(Me.GetItem("lblpaymode").Specific, SAPbouiCOM.StaticText)
            Me.StaticText56 = CType(Me.GetItem("lblloc").Specific, SAPbouiCOM.StaticText)
            Me.Folder4 = CType(Me.GetItem("fldleave").Specific, SAPbouiCOM.Folder)
            Me.StaticText57 = CType(Me.GetItem("lblbcode").Specific, SAPbouiCOM.StaticText)
            Me.EditText50 = CType(Me.GetItem("txtbbr").Specific, SAPbouiCOM.EditText)
            Me.StaticText58 = CType(Me.GetItem("lblbbr").Specific, SAPbouiCOM.StaticText)
            Me.EditText51 = CType(Me.GetItem("txtbacc").Specific, SAPbouiCOM.EditText)
            Me.StaticText59 = CType(Me.GetItem("lblbacc").Specific, SAPbouiCOM.StaticText)
            Me.EditText52 = CType(Me.GetItem("txtiban").Specific, SAPbouiCOM.EditText)
            Me.StaticText60 = CType(Me.GetItem("lbliban").Specific, SAPbouiCOM.StaticText)
            Me.StaticText61 = CType(Me.GetItem("lblbank").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox16 = CType(Me.GetItem("cmbloc").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText62 = CType(Me.GetItem("lblshift").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox17 = CType(Me.GetItem("cmbshift").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText63 = CType(Me.GetItem("lblot").Specific, SAPbouiCOM.StaticText)
            Me.StaticText64 = CType(Me.GetItem("lblgrade").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox18 = CType(Me.GetItem("cmbgrade").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText65 = CType(Me.GetItem("lblsgrade1").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox19 = CType(Me.GetItem("cmbsgrade1").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText66 = CType(Me.GetItem("lblsgrade2").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox20 = CType(Me.GetItem("cmbsgrade2").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText67 = CType(Me.GetItem("lblfinal").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox21 = CType(Me.GetItem("cmbfinal").Specific, SAPbouiCOM.ComboBox)
            Me.EditText54 = CType(Me.GetItem("txtcamp").Specific, SAPbouiCOM.EditText)
            Me.StaticText68 = CType(Me.GetItem("lblcamp").Specific, SAPbouiCOM.StaticText)
            Me.EditText55 = CType(Me.GetItem("txtroom").Specific, SAPbouiCOM.EditText)
            Me.StaticText69 = CType(Me.GetItem("lblroom").Specific, SAPbouiCOM.StaticText)
            Me.EditText56 = CType(Me.GetItem("txtdesti").Specific, SAPbouiCOM.EditText)
            Me.StaticText70 = CType(Me.GetItem("lbldesti").Specific, SAPbouiCOM.StaticText)
            Me.CheckBox0 = CType(Me.GetItem("chkloan").Specific, SAPbouiCOM.CheckBox)
            Me.CheckBox1 = CType(Me.GetItem("chkappr").Specific, SAPbouiCOM.CheckBox)
            Me.EditText59 = CType(Me.GetItem("Item_43").Specific, SAPbouiCOM.EditText)
            Me.StaticText73 = CType(Me.GetItem("lblbfname").Specific, SAPbouiCOM.StaticText)
            Me.EditText60 = CType(Me.GetItem("txtblname").Specific, SAPbouiCOM.EditText)
            Me.StaticText74 = CType(Me.GetItem("lblblname").Specific, SAPbouiCOM.StaticText)
            Me.EditText61 = CType(Me.GetItem("txtppfile").Specific, SAPbouiCOM.EditText)
            Me.StaticText76 = CType(Me.GetItem("lblppfile").Specific, SAPbouiCOM.StaticText)
            Me.StaticText77 = CType(Me.GetItem("Item_51").Specific, SAPbouiCOM.StaticText)
            Me.EditText63 = CType(Me.GetItem("Item_52").Specific, SAPbouiCOM.EditText)
            Me.StaticText79 = CType(Me.GetItem("Item_54").Specific, SAPbouiCOM.StaticText)
            Me.Matrix0 = CType(Me.GetItem("mLeave").Specific, SAPbouiCOM.Matrix)
            Me.StaticText80 = CType(Me.GetItem("lblphoto").Specific, SAPbouiCOM.StaticText)
            Me.EditText64 = CType(Me.GetItem("txtphoto").Specific, SAPbouiCOM.EditText)
            Me.EditText48 = CType(Me.GetItem("txtentry").Specific, SAPbouiCOM.EditText)
            Me.EditText49 = CType(Me.GetItem("txtcode").Specific, SAPbouiCOM.EditText)
            Me.ComboBox13 = CType(Me.GetItem("cmbwstate").Specific, SAPbouiCOM.ComboBox)
            Me.ComboBox22 = CType(Me.GetItem("cmbwcntry").Specific, SAPbouiCOM.ComboBox)
            Me.ComboBox23 = CType(Me.GetItem("cmbnstate").Specific, SAPbouiCOM.ComboBox)
            Me.ComboBox24 = CType(Me.GetItem("cmbncntry").Specific, SAPbouiCOM.ComboBox)
            Me.Folder5 = CType(Me.GetItem("fldbank").Specific, SAPbouiCOM.Folder)
            Me.ComboBox25 = CType(Me.GetItem("cmbot").Specific, SAPbouiCOM.ComboBox)
            Me.Folder6 = CType(Me.GetItem("fldsalary").Specific, SAPbouiCOM.Folder)
            Me.Matrix1 = CType(Me.GetItem("mSalary").Specific, SAPbouiCOM.Matrix)
            Me.Folder7 = CType(Me.GetItem("fldair").Specific, SAPbouiCOM.Folder)
            Me.Matrix2 = CType(Me.GetItem("mID").Specific, SAPbouiCOM.Matrix)
            Me.Folder8 = CType(Me.GetItem("fldid").Specific, SAPbouiCOM.Folder)
            Me.Matrix3 = CType(Me.GetItem("mskill").Specific, SAPbouiCOM.Matrix)
            Me.Folder9 = CType(Me.GetItem("fldskill").Specific, SAPbouiCOM.Folder)
            Me.Folder10 = CType(Me.GetItem("fldtrain").Specific, SAPbouiCOM.Folder)
            Me.Folder11 = CType(Me.GetItem("fldfamily").Specific, SAPbouiCOM.Folder)
            Me.Folder12 = CType(Me.GetItem("fldedu").Specific, SAPbouiCOM.Folder)
            Me.Matrix4 = CType(Me.GetItem("mtraining").Specific, SAPbouiCOM.Matrix)
            Me.Matrix5 = CType(Me.GetItem("mfamily").Specific, SAPbouiCOM.Matrix)
            Me.Matrix6 = CType(Me.GetItem("meducation").Specific, SAPbouiCOM.Matrix)
            Me.Matrix7 = CType(Me.GetItem("mpreemp").Specific, SAPbouiCOM.Matrix)
            Me.EditText27 = CType(Me.GetItem("txtmgrcode").Specific, SAPbouiCOM.EditText)
            Me.LinkedButton0 = CType(Me.GetItem("lkmanager").Specific, SAPbouiCOM.LinkedButton)
            Me.LinkedButton1 = CType(Me.GetItem("lksapuser").Specific, SAPbouiCOM.LinkedButton)
            Me.StaticText75 = CType(Me.GetItem("lblpass").Specific, SAPbouiCOM.StaticText)
            Me.StaticText78 = CType(Me.GetItem("lblvisasp").Specific, SAPbouiCOM.StaticText)
            Me.EditText28 = CType(Me.GetItem("txtvisasp").Specific, SAPbouiCOM.EditText)
            Me.StaticText81 = CType(Me.GetItem("Item_4").Specific, SAPbouiCOM.StaticText)
            Me.EditText29 = CType(Me.GetItem("Item_5").Specific, SAPbouiCOM.EditText)
            Me.StaticText82 = CType(Me.GetItem("lblbfnam").Specific, SAPbouiCOM.StaticText)
            Me.EditText30 = CType(Me.GetItem("txtbfnam").Specific, SAPbouiCOM.EditText)
            Me.Folder13 = CType(Me.GetItem("flddash").Specific, SAPbouiCOM.Folder)
            Me.Matrix8 = CType(Me.GetItem("mair").Specific, SAPbouiCOM.Matrix)
            Me.EditText53 = CType(Me.GetItem("Item_11").Specific, SAPbouiCOM.EditText)
            Me.StaticText83 = CType(Me.GetItem("Item_12").Specific, SAPbouiCOM.StaticText)
            Me.StaticText71 = CType(Me.GetItem("Item_1").Specific, SAPbouiCOM.StaticText)
            Me.EditText57 = CType(Me.GetItem("Item_2").Specific, SAPbouiCOM.EditText)
            Me.EditText58 = CType(Me.GetItem("txtbcode").Specific, SAPbouiCOM.EditText)
            Me.StaticText85 = CType(Me.GetItem("Item_7").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox15 = CType(Me.GetItem("cmbothcc").Specific, SAPbouiCOM.ComboBox)
            Me.StaticText72 = CType(Me.GetItem("Item_3").Specific, SAPbouiCOM.StaticText)
            Me.EditText62 = CType(Me.GetItem("Item_6").Specific, SAPbouiCOM.EditText)
            Me.EditText65 = CType(Me.GetItem("Item_8").Specific, SAPbouiCOM.EditText)
            Me.StaticText84 = CType(Me.GetItem("Item_9").Specific, SAPbouiCOM.StaticText)
            Me.EditText67 = CType(Me.GetItem("txtmedin").Specific, SAPbouiCOM.EditText)
            Me.CheckBox3 = CType(Me.GetItem("ChkPF").Specific, SAPbouiCOM.CheckBox)
            Me.CheckBox4 = CType(Me.GetItem("ChkESI").Specific, SAPbouiCOM.CheckBox)
            Me.CheckBox5 = CType(Me.GetItem("ChkPT").Specific, SAPbouiCOM.CheckBox)
            Me.CheckBox6 = CType(Me.GetItem("ChkTDS").Specific, SAPbouiCOM.CheckBox)
            Me.ComboBox14 = CType(Me.GetItem("cmbpaymode").Specific, SAPbouiCOM.ComboBox)
            Me.CheckBox8 = CType(Me.GetItem("cpayslip").Specific, SAPbouiCOM.CheckBox)
            Me.CheckBox2 = CType(Me.GetItem("chkpslip").Specific, SAPbouiCOM.CheckBox)
            Me.StaticText86 = CType(Me.GetItem("emcontname").Specific, SAPbouiCOM.StaticText)
            Me.EditText66 = CType(Me.GetItem("econtname").Specific, SAPbouiCOM.EditText)
            Me.StaticText87 = CType(Me.GetItem("emcontno").Specific, SAPbouiCOM.StaticText)
            Me.EditText68 = CType(Me.GetItem("econtno").Specific, SAPbouiCOM.EditText)
            Me.EditText69 = CType(Me.GetItem("tpanno").Specific, SAPbouiCOM.EditText)
            Me.StaticText88 = CType(Me.GetItem("lblpan").Specific, SAPbouiCOM.StaticText)
            Me.EditText70 = CType(Me.GetItem("tuanno").Specific, SAPbouiCOM.EditText)
            Me.StaticText89 = CType(Me.GetItem("lbluan").Specific, SAPbouiCOM.StaticText)
            Me.StaticText90 = CType(Me.GetItem("lptded").Specific, SAPbouiCOM.StaticText)
            Me.ComboBox26 = CType(Me.GetItem("tptded").Specific, SAPbouiCOM.ComboBox)
            Me.OnCustomInitialize()

        End Sub

        Public Overrides Sub OnInitializeFormEvents()
            AddHandler DataLoadAfter, AddressOf Me.Form_DataLoadAfter

        End Sub

        Private Sub OnCustomInitialize()
            Try
                objform = objaddon.objapplication.Forms.GetForm("MSTREMPL", Me.FormCount)
                objaddon.objapplication.SetStatusBarMessage("Loading Employee Master Screen Please Wait...", SAPbouiCOM.BoMessageTime.bmt_Short, False)
                objform = objaddon.objapplication.Forms.ActiveForm
                'If Not ApprovedUser_Employee Then CheckBox1.Item.Visible = False
                objform.Freeze(True)
                Form_Load()
                If Link_Value.ToString <> "-1" And Link_objtype.ToString.ToUpper = "OHEM" Then
                    objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE
                    EditText3.Item.Enabled = True
                    EditText0.Item.Enabled = True
                    If Link_Value.ToString.Contains("TRZ") Then
                        EditText0.Value = Link_Value
                    Else
                        EditText3.Value = Link_Value
                    End If
                    objform.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                    EditText3.Item.Enabled = False
                    EditText0.Item.Enabled = False
                    Link_Value = "-1" : Link_objtype = "-1"
                Else
                    'Fill New Employee Code
                    Dim GetValue As String = objaddon.objglobalmethods.getSingleValue("select ""U_AGCode"" from ""@SMPR_ACCT""")
                    If GetValue <> "" Then
                        EditText0.Value = GetValue + objaddon.objglobalmethods.GetDigitCode(objaddon.objglobalmethods.GetNextCode_Value("@SMPR_OHEM"))
                    End If
                    EditText48.Value = objaddon.objglobalmethods.GetNextCode_Value("@SMPR_OHEM")
                    EditText49.Value = EditText48.Value
                    EditText3.Value = EditText48.Value
                End If
                If GetPayrollEnabledIndia = False Then
                    Folder5.Item.Click()
                    Folder0.Item.Click()
                    StaticText90.Item.Visible = False
                    ComboBox26.Item.Visible = False
                Else
                    StaticText90.Item.Visible = True
                    ComboBox26.Item.Visible = True
                End If
                FieldDisable()
                'objform.Items.Item("Item_0").Visible = False
                'objform.Items.Item("Item_32").Visible = False
                objform.EnableMenu("1283", False) 'Remove menu
                objform.EnableMenu("1284", False) 'Cancel Menu
                objform.Settings.Enabled = True
                If objaddon.objcompany.UserName.ToString.ToUpper <> "MANAGER" Then objform.EnableMenu("6913", False) 'User Defined Field
                'objform.EnableMenu("1287", True)'Duplicate Menu
                'PictureBox1.Picture = "D:\Praveen\Personal\Scan Documents\Praveen 001.jpg"
                objform.Freeze(False)
                objaddon.objapplication.StatusBar.SetText("Employee Master Screen Loaded Successfully.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
            Catch ex As Exception
                objform.Freeze(False)
            End Try
        End Sub

#Region "Form Load Details"

        Public Sub Form_Load()
            'Coloring Header and Folder details
            FormFields_coloring()

            'loading Values in Combobox Fields
            LoadComboDeatils()

            'Add Lines in Matrix
            If Matrix0.VisualRowCount = 0 Then Matrix0.AddRow(1) : Matrix0.Columns.Item("#").Cells.Item(1).Specific.string = 1
            If Matrix1.VisualRowCount = 0 Then Matrix1.AddRow(1) : Matrix1.Columns.Item("#").Cells.Item(1).Specific.string = 1
            If Matrix2.VisualRowCount = 0 Then Matrix2.AddRow(1) : Matrix2.Columns.Item("#").Cells.Item(1).Specific.string = 1
            If Matrix3.VisualRowCount = 0 Then Matrix3.AddRow(1) : Matrix3.Columns.Item("#").Cells.Item(1).Specific.string = 1
            If Matrix4.VisualRowCount = 0 Then Matrix4.AddRow(1) : Matrix4.Columns.Item("#").Cells.Item(1).Specific.string = 1
            If Matrix5.VisualRowCount = 0 Then Matrix5.AddRow(1) : Matrix5.Columns.Item("#").Cells.Item(1).Specific.string = 1
            If Matrix6.VisualRowCount = 0 Then Matrix6.AddRow(1) : Matrix6.Columns.Item("#").Cells.Item(1).Specific.string = 1
            If Matrix7.VisualRowCount = 0 Then Matrix7.AddRow(1) : Matrix7.Columns.Item("#").Cells.Item(1).Specific.string = 1
            If Matrix8.VisualRowCount = 0 Then Matrix8.AddRow(1) : Matrix8.Columns.Item("#").Cells.Item(1).Specific.string = 1

            Matrix1.Columns.Item("pyamount").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto

            Folder0.Item.Click(SAPbouiCOM.BoCellClickType.ct_Regular)
            Folder2.Item.Click(SAPbouiCOM.BoCellClickType.ct_Regular)

            'If objaddon.objapplication.Company.UserName.ToString.ToUpper <> "MANAGER" Then Folder13.Item.Visible = False
            'If Not ApprovedUser_Employee Then Folder6.Item.Visible = False
            objform.Items.Item("txtempid").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
        End Sub

        Private Sub LoadComboDeatils()
            Try
                Dim cmbeducation As SAPbouiCOM.Column = Matrix6.Columns.Item("edtype")
                Dim objrs As SAPbobsCOM.Recordset
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                objrs.DoQuery("CALL ""MIPL_HRMS_EMPMASTER_COMBO_FILLING"" ('OHEM')")
                If objrs.RecordCount = 0 Then Exit Sub
                For i As Integer = 0 To objrs.RecordCount - 1
                    Try
                        Select Case objrs.Fields.Item("Type").Value.ToString.ToUpper
                            Case "BRANCH" : ComboBox3.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "DEPARTMENT" : ComboBox2.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "GRADE" : ComboBox18.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "LOCATION" : ComboBox16.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "POSITION" : ComboBox1.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "SALESEMPLOYEE" : ComboBox5.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "SAPUSER" : ComboBox4.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "SHIFT" : ComboBox17.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "STATUS" : ComboBox10.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "TERMINATIONREASON" : ComboBox11.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "EDUCATION" : cmbeducation.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "COUNTRY"
                                ComboBox6.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                                ComboBox7.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                                ComboBox22.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                                ComboBox24.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "STATE"
                                ComboBox13.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                                ComboBox23.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "BANK" : ComboBox15.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "SUBGRADE1" : ComboBox19.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "SUBGRADE2" : ComboBox20.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                            Case "OTHERSCC" : ComboBox15.ValidValues.Add(objrs.Fields.Item("Code").Value, objrs.Fields.Item("Name").Value)
                        End Select
                        objrs.MoveNext()
                    Catch ex As Exception
                        objrs.MoveNext()
                    End Try
                Next
            Catch ex As Exception

            End Try
        End Sub

        Private Sub FormFields_coloring()
            HeaderLabel_Color(StaticText29.Item, 11, Color.Red.ToArgb, 15)
            HeaderLabel_Color(StaticText54.Item, 10, Color.Red.ToArgb, 15)
            HeaderLabel_Color(StaticText75.Item, 10, Color.Red.ToArgb, 15)
            HeaderLabel_Color(StaticText79.Item, 10, Color.Red.ToArgb, 15)
            HeaderLabel_Color(StaticText61.Item, 10, Color.Red.ToArgb, 15)
            HeaderLabel_Color(StaticText19.Item, 10, Color.Red.ToArgb, 15)
            HeaderLabel_Color(StaticText20.Item, 10, Color.Red.ToArgb, 15)


            HeaderLabel_Color(Folder0.Item, 10, 150, Folder0.Item.Height, Folder0.Item.Width + 10)
            HeaderLabel_Color(Folder1.Item, 10, 150, Folder0.Item.Height, Folder0.Item.Width + 10)
            HeaderLabel_Color(Folder3.Item, 10, 150, Folder0.Item.Height, Folder0.Item.Width + 10)
            HeaderLabel_Color(Folder5.Item, 10, 150, Folder0.Item.Height, Folder0.Item.Width + 10)
            HeaderLabel_Color(Folder6.Item, 10, 150, Folder0.Item.Height, Folder0.Item.Width + 10)
            HeaderLabel_Color(Folder7.Item, 10, 150, Folder0.Item.Height, Folder0.Item.Width + 10)
            HeaderLabel_Color(Folder8.Item, 10, 150, Folder0.Item.Height, Folder0.Item.Width + 10)
            HeaderLabel_Color(Folder9.Item, 10, 150, Folder0.Item.Height, Folder0.Item.Width + 10)
            HeaderLabel_Color(Folder10.Item, 10, 150, Folder0.Item.Height, Folder0.Item.Width + 10)
            HeaderLabel_Color(Folder11.Item, 10, 150, Folder0.Item.Height, Folder0.Item.Width + 10)
            HeaderLabel_Color(Folder12.Item, 10, 150, Folder0.Item.Height, Folder0.Item.Width + 10)
            HeaderLabel_Color(Folder13.Item, 10, 150, Folder0.Item.Height, Folder0.Item.Width + 10)
            HeaderLabel_Color(Folder14.Item, 10, 150, Folder0.Item.Height, Folder0.Item.Width + 10)

            HeaderLabel_Color(Folder2.Item, 10, 150, Folder0.Item.Height)
            HeaderLabel_Color(Folder4.Item, 10, 150, Folder0.Item.Height)

            CheckBox1.Item.Width = 120 : CheckBox1.Item.Height = 16
            CheckBox0.Item.Width = 120 : CheckBox0.Item.Height = 16
            CheckBox8.Item.Width = 120 : CheckBox8.Item.Height = 16
        End Sub

        Private Sub HeaderLabel_Color(ByVal item As SAPbouiCOM.Item, ByVal fontsize As Integer, ByVal forecolor As Integer, ByVal height As Integer, Optional ByVal width As Integer = 0)
            item.TextStyle = FontStyle.Bold
            item.FontSize = fontsize
            item.ForeColor = forecolor
            item.Height = height
            'If width <> 0 Then item.Width = width
        End Sub

        Private Sub FieldDisable()
            Try
                CheckBox1.Item.Height = CheckBox1.Item.Height + 3
                CheckBox1.Item.Width = CheckBox1.Item.Width + 20
                If GetPayrollEnabledIndia Then
                    CheckBox8.Item.Visible = False
                    Matrix1.Columns.Item("pyot").Visible = False
                    Matrix1.Columns.Item("pylvst").Visible = False
                    Matrix1.Columns.Item("pyfaf").Visible = False
                    Matrix5.Columns.Item("feid").Visible = False
                    CheckBox0.Item.Visible = False
                    Matrix0.Columns.Item("lvobdt").Visible = False
                    Matrix0.Columns.Item("lvobdy").Visible = False
                    'Matrix0.Columns.Item("lvmaxcary").Visible = False
                    Matrix0.Columns.Item("Compoff").Visible = False
                    Folder7.Item.Visible = False
                    StaticText64.Item.Visible = False
                    ComboBox18.Item.Visible = False
                    StaticText65.Item.Visible = False
                    ComboBox19.Item.Visible = False
                    StaticText66.Item.Visible = False
                    ComboBox20.Item.Visible = False
                    StaticText68.Item.Visible = False
                    EditText54.Item.Visible = False
                    StaticText69.Item.Visible = False
                    EditText55.Item.Visible = False
                    'StaticText6.Item.Visible = False
                    'ComboBox2.Item.Visible = False
                    CheckBox3.Item.Height = CheckBox3.Item.Height + 2
                    CheckBox4.Item.Height = CheckBox4.Item.Height + 2
                    CheckBox5.Item.Height = CheckBox5.Item.Height + 2
                    CheckBox6.Item.Height = CheckBox6.Item.Height + 2
                    Dim Fsize As Size
                    Fsize = TextRenderer.MeasureText(CheckBox3.Caption, New Font("Arial", 12.0F))
                    CheckBox3.Item.Width = Fsize.Width + 30
                    Fsize = TextRenderer.MeasureText(CheckBox4.Caption, New Font("Arial", 12.0F))
                    CheckBox4.Item.Width = Fsize.Width + 30
                    Fsize = TextRenderer.MeasureText(CheckBox5.Caption, New Font("Arial", 12.0F))
                    CheckBox5.Item.Width = Fsize.Width + 30
                    Fsize = TextRenderer.MeasureText(CheckBox6.Caption, New Font("Arial", 12.0F))
                    CheckBox6.Item.Width = Fsize.Width + 30
                Else
                    CheckBox3.Item.Visible = False
                    CheckBox4.Item.Visible = False
                    CheckBox5.Item.Visible = False
                    CheckBox6.Item.Visible = False
                    Matrix0.Columns.Item("LeaveBal").Visible = False
                    Matrix0.Columns.Item("LeaveTak").Visible = False
                    Matrix0.Columns.Item("Compoff").Visible = False
                    Matrix0.Columns.Item("CurLeave").Visible = False
                    Matrix1.Columns.Item("PF").Visible = False
                    StaticText90.Item.Visible = False
                    ComboBox26.Item.Visible = False
                End If
            Catch ex As Exception

            End Try
        End Sub
#End Region

#Region "Form Events"
        Private Sub Form_DataLoadAfter(ByRef pVal As SAPbouiCOM.BusinessObjectInfo)
            Try
                'If pVal.BeforeAction = True Then Exit Sub
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then
                    'EditText1.Item.Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                    EditText0.Item.Enabled = False
                    EditText3.Item.Enabled = False

                    'Manager Name Loading
                    If EditText27.Value.ToString <> "" Then
                        objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                        objrs.DoQuery("SELECT ""firstName"" || ' ' || ""lastName"" FROM ""OHEM"" WHERE ""empID""='" & EditText27.Value.ToString & "'")
                        If objrs.RecordCount > 0 Then EditText5.Value = objrs.Fields.Item(0).Value
                    Else
                        EditText5.Value = ""
                    End If
                    Dim state As String = ""
                    If Not ComboBox24.Selected Is Nothing Then
                        If Not ComboBox23.Selected Is Nothing Then state = ComboBox23.Selected.Value
                        objaddon.objglobalmethods.LoadCombo(ComboBox23, "SELECT ""Code"", ""Name"" FROM ""OCST"" WHERE ""Country""='" & ComboBox24.Selected.Value & "'", Nothing)
                        If state.ToString <> "" Then ComboBox23.Select(state, SAPbouiCOM.BoSearchKey.psk_ByValue)
                    End If
                    state = ""
                    If Not ComboBox22.Selected Is Nothing Then
                        If Not ComboBox13.Selected Is Nothing Then state = ComboBox13.Selected.Value
                        objaddon.objglobalmethods.LoadCombo(ComboBox13, "SELECT ""Code"", ""Name"" FROM ""OCST"" WHERE ""Country""='" & ComboBox22.Selected.Value & "'", Nothing)
                        If state.ToString <> "" Then ComboBox13.Select(state, SAPbouiCOM.BoSearchKey.psk_ByValue)
                    End If
                End If
                If GetPayrollEnabledIndia = False Then
                    Salary_Tab_Restriction()
                End If
            Catch ex As Exception
            End Try
        End Sub


#End Region

#Region "Field Details"

        Private WithEvents Button0 As SAPbouiCOM.Button
        Private WithEvents Button1 As SAPbouiCOM.Button
        Private WithEvents StaticText0 As SAPbouiCOM.StaticText
        Private WithEvents EditText0 As SAPbouiCOM.EditText
        Private WithEvents StaticText1 As SAPbouiCOM.StaticText
        Private WithEvents EditText1 As SAPbouiCOM.EditText
        Private WithEvents StaticText2 As SAPbouiCOM.StaticText
        Private WithEvents EditText2 As SAPbouiCOM.EditText
        Private WithEvents EditText3 As SAPbouiCOM.EditText
        Private WithEvents StaticText3 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox0 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText4 As SAPbouiCOM.StaticText
        Private WithEvents EditText4 As SAPbouiCOM.EditText
        Private WithEvents StaticText5 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox1 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText6 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox2 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText7 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox3 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText8 As SAPbouiCOM.StaticText
        Private WithEvents EditText5 As SAPbouiCOM.EditText
        Private WithEvents StaticText9 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox4 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText10 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox5 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText11 As SAPbouiCOM.StaticText
        Private WithEvents EditText6 As SAPbouiCOM.EditText
        Private WithEvents StaticText12 As SAPbouiCOM.StaticText
        Private WithEvents EditText7 As SAPbouiCOM.EditText
        Private WithEvents StaticText13 As SAPbouiCOM.StaticText
        Private WithEvents EditText8 As SAPbouiCOM.EditText
        Private WithEvents StaticText14 As SAPbouiCOM.StaticText
        Private WithEvents EditText9 As SAPbouiCOM.EditText
        Private WithEvents StaticText15 As SAPbouiCOM.StaticText
        Private WithEvents EditText10 As SAPbouiCOM.EditText
        Private WithEvents StaticText16 As SAPbouiCOM.StaticText
        Private WithEvents EditText11 As SAPbouiCOM.EditText
        Private WithEvents StaticText17 As SAPbouiCOM.StaticText
        Private WithEvents EditText12 As SAPbouiCOM.EditText
        Private WithEvents Folder0 As SAPbouiCOM.Folder
        Private WithEvents Folder1 As SAPbouiCOM.Folder
        Private WithEvents StaticText18 As SAPbouiCOM.StaticText
        Private WithEvents EditText13 As SAPbouiCOM.EditText
        Private WithEvents PictureBox1 As SAPbouiCOM.PictureBox
        Private WithEvents StaticText19 As SAPbouiCOM.StaticText
        Private WithEvents StaticText20 As SAPbouiCOM.StaticText
        Private WithEvents EditText14 As SAPbouiCOM.EditText
        Private WithEvents StaticText21 As SAPbouiCOM.StaticText
        Private WithEvents EditText15 As SAPbouiCOM.EditText
        Private WithEvents EditText16 As SAPbouiCOM.EditText
        Private WithEvents StaticText22 As SAPbouiCOM.StaticText
        Private WithEvents EditText17 As SAPbouiCOM.EditText
        Private WithEvents EditText18 As SAPbouiCOM.EditText
        Private WithEvents StaticText23 As SAPbouiCOM.StaticText
        Private WithEvents EditText19 As SAPbouiCOM.EditText
        Private WithEvents EditText20 As SAPbouiCOM.EditText
        Private WithEvents StaticText24 As SAPbouiCOM.StaticText
        Private WithEvents EditText21 As SAPbouiCOM.EditText
        Private WithEvents EditText22 As SAPbouiCOM.EditText
        Private WithEvents StaticText25 As SAPbouiCOM.StaticText
        Private WithEvents EditText23 As SAPbouiCOM.EditText
        Private WithEvents EditText24 As SAPbouiCOM.EditText
        Private WithEvents StaticText26 As SAPbouiCOM.StaticText
        Private WithEvents EditText25 As SAPbouiCOM.EditText
        Private WithEvents EditText26 As SAPbouiCOM.EditText
        Private WithEvents StaticText27 As SAPbouiCOM.StaticText
        Private WithEvents StaticText28 As SAPbouiCOM.StaticText
        Private WithEvents StaticText29 As SAPbouiCOM.StaticText
        Private WithEvents Folder3 As SAPbouiCOM.Folder
        Private WithEvents StaticText30 As SAPbouiCOM.StaticText
        Private WithEvents EditText31 As SAPbouiCOM.EditText
        Private WithEvents StaticText31 As SAPbouiCOM.StaticText
        Private WithEvents StaticText32 As SAPbouiCOM.StaticText
        Private WithEvents StaticText33 As SAPbouiCOM.StaticText
        Private WithEvents StaticText34 As SAPbouiCOM.StaticText
        Private WithEvents EditText35 As SAPbouiCOM.EditText
        Private WithEvents StaticText35 As SAPbouiCOM.StaticText
        Private WithEvents StaticText36 As SAPbouiCOM.StaticText
        Private WithEvents EditText37 As SAPbouiCOM.EditText
        Private WithEvents StaticText37 As SAPbouiCOM.StaticText
        Private WithEvents EditText38 As SAPbouiCOM.EditText
        Private WithEvents ComboBox6 As SAPbouiCOM.ComboBox
        Private WithEvents ComboBox7 As SAPbouiCOM.ComboBox
        Private WithEvents ComboBox8 As SAPbouiCOM.ComboBox
        Private WithEvents ComboBox9 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText38 As SAPbouiCOM.StaticText
        Private WithEvents EditText39 As SAPbouiCOM.EditText
        Private WithEvents StaticText42 As SAPbouiCOM.StaticText
        Private WithEvents EditText40 As SAPbouiCOM.EditText
        Private WithEvents StaticText44 As SAPbouiCOM.StaticText
        Private WithEvents EditText41 As SAPbouiCOM.EditText
        Private WithEvents StaticText45 As SAPbouiCOM.StaticText
        Private WithEvents EditText42 As SAPbouiCOM.EditText
        Private WithEvents Folder2 As SAPbouiCOM.Folder
        Private WithEvents StaticText39 As SAPbouiCOM.StaticText
        Private WithEvents EditText32 As SAPbouiCOM.EditText
        Private WithEvents StaticText40 As SAPbouiCOM.StaticText
        Private WithEvents EditText33 As SAPbouiCOM.EditText
        Private WithEvents StaticText41 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox10 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText43 As SAPbouiCOM.StaticText
        Private WithEvents EditText34 As SAPbouiCOM.EditText
        Private WithEvents StaticText46 As SAPbouiCOM.StaticText
        Private WithEvents EditText36 As SAPbouiCOM.EditText
        Private WithEvents StaticText47 As SAPbouiCOM.StaticText
        Private WithEvents EditText43 As SAPbouiCOM.EditText
        Private WithEvents StaticText48 As SAPbouiCOM.StaticText
        Private WithEvents EditText44 As SAPbouiCOM.EditText
        Private WithEvents StaticText49 As SAPbouiCOM.StaticText
        Private WithEvents EditText45 As SAPbouiCOM.EditText
        Private WithEvents StaticText50 As SAPbouiCOM.StaticText
        Private WithEvents EditText46 As SAPbouiCOM.EditText
        Private WithEvents StaticText51 As SAPbouiCOM.StaticText
        Private WithEvents EditText47 As SAPbouiCOM.EditText
        Private WithEvents StaticText52 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox11 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText53 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox12 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText54 As SAPbouiCOM.StaticText
        Private WithEvents StaticText55 As SAPbouiCOM.StaticText
        Private WithEvents StaticText56 As SAPbouiCOM.StaticText
        Private WithEvents Folder4 As SAPbouiCOM.Folder
        Private WithEvents StaticText57 As SAPbouiCOM.StaticText
        Private WithEvents EditText50 As SAPbouiCOM.EditText
        Private WithEvents StaticText58 As SAPbouiCOM.StaticText
        Private WithEvents EditText51 As SAPbouiCOM.EditText
        Private WithEvents StaticText59 As SAPbouiCOM.StaticText
        Private WithEvents EditText52 As SAPbouiCOM.EditText
        Private WithEvents StaticText60 As SAPbouiCOM.StaticText
        Private WithEvents StaticText61 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox16 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText62 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox17 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText63 As SAPbouiCOM.StaticText
        Private WithEvents StaticText64 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox18 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText65 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox19 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText66 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox20 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText67 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox21 As SAPbouiCOM.ComboBox
        Private WithEvents EditText54 As SAPbouiCOM.EditText
        Private WithEvents StaticText68 As SAPbouiCOM.StaticText
        Private WithEvents EditText55 As SAPbouiCOM.EditText
        Private WithEvents StaticText69 As SAPbouiCOM.StaticText
        Private WithEvents EditText56 As SAPbouiCOM.EditText
        Private WithEvents StaticText70 As SAPbouiCOM.StaticText
        Private WithEvents CheckBox0 As SAPbouiCOM.CheckBox
        Private WithEvents CheckBox1 As SAPbouiCOM.CheckBox
        Private WithEvents EditText59 As SAPbouiCOM.EditText
        Private WithEvents StaticText73 As SAPbouiCOM.StaticText
        Private WithEvents EditText60 As SAPbouiCOM.EditText
        Private WithEvents StaticText74 As SAPbouiCOM.StaticText
        Private WithEvents EditText61 As SAPbouiCOM.EditText
        Private WithEvents StaticText76 As SAPbouiCOM.StaticText
        Private WithEvents StaticText77 As SAPbouiCOM.StaticText
        Private WithEvents EditText63 As SAPbouiCOM.EditText
        Private WithEvents StaticText79 As SAPbouiCOM.StaticText
        Private WithEvents Matrix0 As SAPbouiCOM.Matrix
        Private WithEvents StaticText80 As SAPbouiCOM.StaticText
        Private WithEvents EditText64 As SAPbouiCOM.EditText
        Private WithEvents EditText48 As SAPbouiCOM.EditText
        Private WithEvents EditText49 As SAPbouiCOM.EditText
        Private WithEvents ComboBox13 As SAPbouiCOM.ComboBox
        Private WithEvents ComboBox22 As SAPbouiCOM.ComboBox
        Private WithEvents ComboBox23 As SAPbouiCOM.ComboBox
        Private WithEvents ComboBox24 As SAPbouiCOM.ComboBox
        Private WithEvents Folder5 As SAPbouiCOM.Folder
        Private WithEvents ComboBox25 As SAPbouiCOM.ComboBox
        Private WithEvents Folder6 As SAPbouiCOM.Folder
        Private WithEvents Matrix1 As SAPbouiCOM.Matrix
        Private WithEvents Folder7 As SAPbouiCOM.Folder
        Private WithEvents Matrix2 As SAPbouiCOM.Matrix
        Private WithEvents Folder8 As SAPbouiCOM.Folder
        Private WithEvents Matrix3 As SAPbouiCOM.Matrix
        Private WithEvents Folder9 As SAPbouiCOM.Folder
        Private WithEvents Folder10 As SAPbouiCOM.Folder
        Private WithEvents Folder11 As SAPbouiCOM.Folder
        Private WithEvents Folder12 As SAPbouiCOM.Folder
        Private WithEvents Matrix4 As SAPbouiCOM.Matrix
        Private WithEvents Matrix5 As SAPbouiCOM.Matrix
        Private WithEvents Matrix6 As SAPbouiCOM.Matrix
        Private WithEvents Matrix7 As SAPbouiCOM.Matrix
        Private WithEvents EditText27 As SAPbouiCOM.EditText
        Private WithEvents LinkedButton0 As SAPbouiCOM.LinkedButton
        Private WithEvents LinkedButton1 As SAPbouiCOM.LinkedButton
        Private WithEvents StaticText75 As SAPbouiCOM.StaticText
        Private WithEvents StaticText78 As SAPbouiCOM.StaticText
        Private WithEvents EditText28 As SAPbouiCOM.EditText
        Private WithEvents StaticText81 As SAPbouiCOM.StaticText
        Private WithEvents EditText29 As SAPbouiCOM.EditText
        Private WithEvents StaticText82 As SAPbouiCOM.StaticText
        Private WithEvents EditText30 As SAPbouiCOM.EditText
        Private WithEvents Folder13 As SAPbouiCOM.Folder
        Private WithEvents Folder14 As SAPbouiCOM.Folder
        Private WithEvents Matrix8 As SAPbouiCOM.Matrix
        Private WithEvents EditText53 As SAPbouiCOM.EditText
        Private WithEvents StaticText83 As SAPbouiCOM.StaticText
        Private WithEvents StaticText71 As SAPbouiCOM.StaticText
        Private WithEvents EditText57 As SAPbouiCOM.EditText
        Private WithEvents EditText58 As SAPbouiCOM.EditText
        Private WithEvents StaticText85 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox15 As SAPbouiCOM.ComboBox
        Private WithEvents StaticText72 As SAPbouiCOM.StaticText
        Private WithEvents EditText62 As SAPbouiCOM.EditText
        Private WithEvents EditText65 As SAPbouiCOM.EditText
        Private WithEvents StaticText84 As SAPbouiCOM.StaticText
        Private WithEvents EditText67 As SAPbouiCOM.EditText
        Private WithEvents CheckBox3 As SAPbouiCOM.CheckBox
        Private WithEvents CheckBox4 As SAPbouiCOM.CheckBox
        Private WithEvents CheckBox5 As SAPbouiCOM.CheckBox
        Private WithEvents CheckBox6 As SAPbouiCOM.CheckBox
        Private WithEvents ComboBox14 As SAPbouiCOM.ComboBox
        Private WithEvents CheckBox8 As SAPbouiCOM.CheckBox
        Private WithEvents CheckBox2 As SAPbouiCOM.CheckBox
        Private WithEvents Button3 As SAPbouiCOM.Button
        Private WithEvents StaticText86 As SAPbouiCOM.StaticText
        Private WithEvents EditText66 As SAPbouiCOM.EditText
        Private WithEvents StaticText87 As SAPbouiCOM.StaticText
        Private WithEvents EditText68 As SAPbouiCOM.EditText
        Private WithEvents EditText69 As SAPbouiCOM.EditText
        Private WithEvents StaticText88 As SAPbouiCOM.StaticText
        Private WithEvents EditText70 As SAPbouiCOM.EditText
        Private WithEvents StaticText89 As SAPbouiCOM.StaticText
        Private WithEvents StaticText90 As SAPbouiCOM.StaticText
        Private WithEvents ComboBox26 As SAPbouiCOM.ComboBox

#End Region

#Region "Folder Pressed After"

        Private Sub Folder4_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Folder4.PressedAfter
            objform.Settings.MatrixUID = "mLeave"
            Activate_FitcolumWidth() : objaddon.objglobalmethods.Matrix_Addrow(Matrix0, "lvcode", "#")
        End Sub

        Private Sub Folder6_PressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Folder6.PressedAfter
            Try
                objform.Settings.MatrixUID = "mSalary"
                Activate_FitcolumWidth() : objaddon.objglobalmethods.Matrix_Addrow(Matrix1, "pycode", "#")
                FieldDisable()
                If GetPayrollEnabledIndia = False Then
                    Salary_Tab_Restriction()
                End If
            Catch ex As Exception
                objaddon.objapplication.StatusBar.SetText("Folder: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
            End Try



        End Sub

        Private Sub Folder7_PressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Folder7.PressedAfter
            objform.Settings.MatrixUID = "mair"
            Activate_FitcolumWidth() : objaddon.objglobalmethods.Matrix_Addrow(Matrix8, "frdate", "#")
        End Sub

        Private Sub Folder8_PressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Folder8.PressedAfter
            objform.Settings.MatrixUID = "mID"
            Activate_FitcolumWidth() : objaddon.objglobalmethods.Matrix_Addrow(Matrix2, "idcode", "#")
        End Sub

        Private Sub Folder9_PressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Folder9.PressedAfter
            objform.Settings.MatrixUID = "mskill"
            Activate_FitcolumWidth() : objaddon.objglobalmethods.Matrix_Addrow(Matrix3, "skcode", "#")
        End Sub

        Private Sub Folder10_PressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Folder10.PressedAfter
            objform.Settings.MatrixUID = "mtraining"
            Activate_FitcolumWidth() : objaddon.objglobalmethods.Matrix_Addrow(Matrix4, "trname", "#")
        End Sub

        Private Sub Folder11_PressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Folder11.PressedAfter
            objform.Settings.MatrixUID = "mfamily"
            Activate_FitcolumWidth() : objaddon.objglobalmethods.Matrix_Addrow(Matrix5, "fname", "#")
        End Sub

        Private Sub Folder12_PressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Folder12.PressedAfter
            objform.Settings.MatrixUID = "meducation"
            Activate_FitcolumWidth() : objaddon.objglobalmethods.Matrix_Addrow(Matrix6, "edfrdt", "#")
        End Sub

        Private Sub Folder14_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Folder14.PressedAfter
            objform.Settings.MatrixUID = "mpreemp"
            Activate_FitcolumWidth() : objaddon.objglobalmethods.Matrix_Addrow(Matrix7, "emfrom", "#")
        End Sub

#End Region

#Region "Probation Date Calculation"

        Private Sub EditText34_LostFocusAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText34.LostFocusAfter
            Prodation_date_calculation()
        End Sub

        Private Sub EditText32_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText32.LostFocusAfter
            Prodation_date_calculation()
        End Sub

        Private Sub Prodation_date_calculation()
            Try
                If EditText32.String = "" Or Val(EditText34.Value) = 0 Then Exit Sub
                Dim probdate As Date = EditText32.String
                probdate = DateAdd(DateInterval.Month, Convert.ToInt16(EditText34.Value), probdate)
                EditText36.String = probdate.ToString("dd/MM/yy")
            Catch ex As Exception

            End Try

        End Sub

#End Region

#Region "Choose From List Events"

        Private Sub EditText27_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText27.ChooseFromListAfter
            Try
                If pVal.ActionSuccess = False Then Exit Sub
                pCFL = pVal
                If Not pCFL.SelectedObjects Is Nothing Then
                    Try
                        EditText27.Value = pCFL.SelectedObjects.Columns.Item("empID").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                    Try
                        EditText5.Value = pCFL.SelectedObjects.Columns.Item("firstName").Cells.Item(0).Value + " " + pCFL.SelectedObjects.Columns.Item("lastName").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                End If
            Catch ex As Exception
            Finally
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try
        End Sub

        Private Sub Matrix0_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Matrix0.ChooseFromListBefore
            If pVal.ActionSuccess = True Then Exit Sub
            Try
                Dim oCFL As SAPbouiCOM.ChooseFromList = objform.ChooseFromLists.Item("CFL_Leave")
                Dim oConds As SAPbouiCOM.Conditions
                Dim oCond As SAPbouiCOM.Condition
                Dim oEmptyConds As New SAPbouiCOM.Conditions
                oCFL.SetConditions(oEmptyConds)
                oConds = oCFL.GetConditions()

                For i As Integer = 1 To Matrix0.VisualRowCount
                    oCond = oConds.Add()
                    oCond.Alias = "Code"
                    oCond.Operation = SAPbouiCOM.BoConditionOperation.co_NOT_EQUAL
                    If pVal.Row = i Then
                        oCond.CondVal = ""
                    Else
                        oCond.CondVal = Trim(Matrix0.Columns.Item("lvcode").Cells.Item(i).Specific.string)
                    End If
                    'If i <> Matrix0.VisualRowCount Then oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND
                    oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND
                Next

                oCond = oConds.Add()
                oCond.Alias = "U_empmastr"
                oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                oCond.CondVal = "Y"

                oCFL.SetConditions(oConds)
            Catch ex As Exception
                SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage("Choose FromList Filter Leave Details Failed:" & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
            End Try
        End Sub

        Private Sub Matrix0_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.ChooseFromListAfter
            Try
                If pVal.ActionSuccess = False Then Exit Sub
                Dim chk As SAPbouiCOM.CheckBox
                pCFL = pVal
                If Not pCFL.SelectedObjects Is Nothing Then
                    Try
                        Matrix0.Columns.Item("lvcode").Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("Code").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                    Try
                        Matrix0.Columns.Item("lvname").Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("Name").Cells.Item(0).Value
                        Matrix0.Columns.Item("lvtle").Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("U_TotalLve").Cells.Item(0).Value
                        Matrix0.Columns.Item("lvcary").Editable = True
                        chk = Matrix0.Columns.Item("lvcary").Cells.Item(pVal.Row).Specific
                        If pCFL.SelectedObjects.Columns.Item("U_FwdNxtYr").Cells.Item(0).Value.ToString.ToUpper = "Y" Then
                            chk.Checked = True
                        Else
                            chk.Checked = False
                        End If
                        Matrix0.Columns.Item("lvcary").Editable = False
                        Matrix0.Columns.Item("lvmaxcary").Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("U_MxLveFwd").Cells.Item(0).Value
                    Catch ex As Exception

                    End Try
                    objaddon.objglobalmethods.Matrix_Addrow(Matrix0, "lvcode", "#")
                End If
                Activate_FitcolumWidth()
            Catch ex As Exception
            Finally
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try
        End Sub

        Private Sub Matrix1_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Matrix1.ChooseFromListBefore
            If pVal.ActionSuccess = True Then Exit Sub
            Try
                Dim oCFL As SAPbouiCOM.ChooseFromList = objform.ChooseFromLists.Item("CFL_pay")
                Dim oConds As SAPbouiCOM.Conditions
                Dim oCond As SAPbouiCOM.Condition
                Dim oEmptyConds As New SAPbouiCOM.Conditions
                oCFL.SetConditions(oEmptyConds)
                oConds = oCFL.GetConditions()

                If GetPayrollEnabledIndia Then
                    oCond = oConds.Add()
                    oCond.Alias = "U_Type"
                    oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                    oCond.CondVal = "S"
                    oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND
                Else
                    'oCond = oConds.Add()
                    'oCond.Alias = "U_Type"
                    'oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                    'oCond.CondVal = "S"
                    'oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_OR
                    'oCond = oConds.Add()
                    'oCond.Alias = "U_Type"
                    'oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                    'oCond.CondVal = "D"
                    'oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND
                End If
                oCond = oConds.Add()
                oCond.Alias = "U_Active"
                oCond.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                oCond.CondVal = "Y"
                'oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND

                'For i As Integer = 1 To Matrix1.VisualRowCount
                '    oCond = oConds.Add()
                '    oCond.Alias = "Code"
                '    oCond.Operation = SAPbouiCOM.BoConditionOperation.co_NOT_EQUAL
                '    If pVal.Row = i Then
                '        oCond.CondVal = ""
                '    Else
                '        oCond.CondVal = Trim(Matrix1.Columns.Item("pycode").Cells.Item(i).Specific.string)
                '    End If
                '    If i <> Matrix1.VisualRowCount Then oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND
                'Next

                oCFL.SetConditions(oConds)
            Catch ex As Exception
                SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage("Choose FromList Filter Pay Element Failed:" & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
            End Try
        End Sub

        Private Sub Matrix1_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix1.ChooseFromListAfter
            Try
                If pVal.ActionSuccess = False Then Exit Sub
                Dim ocombo As SAPbouiCOM.ComboBox
                pCFL = pVal
                odbdsSalDetails = objform.DataSources.DBDataSources.Item("@SMPR_HEM1")
                If Not pCFL.SelectedObjects Is Nothing Then
                    Try
                        Matrix1.FlushToDataSource()
                        odbdsSalDetails.SetValue("U_PayElCod", pVal.Row - 1, pCFL.SelectedObjects.Columns.Item("Code").Cells.Item(0).Value)
                        odbdsSalDetails.SetValue("U_PayElNam", pVal.Row - 1, pCFL.SelectedObjects.Columns.Item("Name").Cells.Item(0).Value)
                        odbdsSalDetails.SetValue("U_PaidCate", pVal.Row - 1, pCFL.SelectedObjects.Columns.Item("U_PaidCate").Cells.Item(0).Value)
                        odbdsSalDetails.SetValue("U_PayType", pVal.Row - 1, pCFL.SelectedObjects.Columns.Item("U_Type").Cells.Item(0).Value)
                        odbdsSalDetails.SetValue("U_EffDate", pVal.Row - 1, EditText32.Value)
                        Matrix1.LoadFromDataSource()
                        'Matrix1.Columns.Item("pycode").Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("Code").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                    Try
                        'Matrix1.Columns.Item("pyname").Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("Name").Cells.Item(0).Value
                        'ocombo = Matrix1.Columns.Item("pypaidcat").Cells.Item(pVal.Row).Specific
                        'ocombo.Select(pCFL.SelectedObjects.Columns.Item("U_PaidCate").Cells.Item(0).Value, SAPbouiCOM.BoSearchKey.psk_ByValue)
                        'ocombo = Matrix1.Columns.Item("pypaidty").Cells.Item(pVal.Row).Specific
                        'ocombo.Select(pCFL.SelectedObjects.Columns.Item("U_Type").Cells.Item(0).Value, SAPbouiCOM.BoSearchKey.psk_ByValue)
                        'Matrix1.Columns.Item("pyeff").Cells.Item(pVal.Row).Specific.string = EditText32.Value
                        Matrix1.Columns.Item("pycode").Cells.Item(pVal.Row).Click()
                    Catch ex As Exception

                    End Try
                    objaddon.objglobalmethods.Matrix_Addrow(Matrix1, "pycode", "#")
                End If
                Activate_FitcolumWidth()
            Catch ex As Exception
            Finally
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try
        End Sub

        Private Sub Matrix2_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Matrix2.ChooseFromListBefore
            If pVal.ActionSuccess = True Then Exit Sub
            Try
                Dim oCFL As SAPbouiCOM.ChooseFromList = objform.ChooseFromLists.Item("CFL_ID")
                Dim oConds As SAPbouiCOM.Conditions
                Dim oCond As SAPbouiCOM.Condition
                Dim oEmptyConds As New SAPbouiCOM.Conditions
                oCFL.SetConditions(oEmptyConds)
                oConds = oCFL.GetConditions()

                For i As Integer = 1 To Matrix2.VisualRowCount
                    oCond = oConds.Add()
                    oCond.Alias = "Code"
                    oCond.Operation = SAPbouiCOM.BoConditionOperation.co_NOT_EQUAL
                    If pVal.Row = i Then
                        oCond.CondVal = ""
                    Else
                        oCond.CondVal = Trim(Matrix2.Columns.Item("idcode").Cells.Item(i).Specific.string)
                    End If
                    If i <> Matrix2.VisualRowCount Then oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND
                Next

                oCFL.SetConditions(oConds)
            Catch ex As Exception
                SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage("Choose FromList Filter Pay Element Failed:" & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
            End Try
        End Sub

        Private Sub Matrix2_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix2.ChooseFromListAfter
            Try
                If pVal.ActionSuccess = False Then Exit Sub
                pCFL = pVal
                If Not pCFL.SelectedObjects Is Nothing Then
                    Try
                        Matrix2.Columns.Item("idcode").Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("Code").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                    Try
                        Matrix2.Columns.Item("idname").Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("Name").Cells.Item(0).Value
                    Catch ex As Exception

                    End Try
                    objaddon.objglobalmethods.Matrix_Addrow(Matrix2, "idcode", "#")
                End If
                Activate_FitcolumWidth()
            Catch ex As Exception
            Finally
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try
        End Sub

        Private Sub Matrix3_ChooseFromListBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Matrix3.ChooseFromListBefore
            If pVal.ActionSuccess = True Then Exit Sub
            Try
                Dim oCFL As SAPbouiCOM.ChooseFromList = objform.ChooseFromLists.Item("CFL_skill")
                Dim oConds As SAPbouiCOM.Conditions
                Dim oCond As SAPbouiCOM.Condition
                Dim oEmptyConds As New SAPbouiCOM.Conditions
                oCFL.SetConditions(oEmptyConds)
                oConds = oCFL.GetConditions()

                For i As Integer = 1 To Matrix3.VisualRowCount
                    oCond = oConds.Add()
                    oCond.Alias = "Code"
                    oCond.Operation = SAPbouiCOM.BoConditionOperation.co_NOT_EQUAL
                    If pVal.Row = i Then
                        oCond.CondVal = ""
                    Else
                        oCond.CondVal = Trim(Matrix3.Columns.Item("skcode").Cells.Item(i).Specific.string)
                    End If
                    If i <> Matrix3.VisualRowCount Then oCond.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND
                Next

                oCFL.SetConditions(oConds)
            Catch ex As Exception
                SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage("Choose FromList Filter Pay Element Failed:" & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
            End Try
        End Sub

        Private Sub Matrix3_ChooseFromListAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix3.ChooseFromListAfter
            Try
                If pVal.ActionSuccess = False Then Exit Sub
                pCFL = pVal
                If Not pCFL.SelectedObjects Is Nothing Then
                    Try
                        Matrix3.Columns.Item("skcode").Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("Code").Cells.Item(0).Value
                    Catch ex As Exception
                    End Try
                    Try
                        Matrix3.Columns.Item("skname").Cells.Item(pVal.Row).Specific.string = pCFL.SelectedObjects.Columns.Item("Name").Cells.Item(0).Value
                    Catch ex As Exception

                    End Try
                    objaddon.objglobalmethods.Matrix_Addrow(Matrix3, "skcode", "#")
                End If
                Activate_FitcolumWidth()
            Catch ex As Exception
            Finally
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try
        End Sub
#End Region

#Region "Link Button Event"

        Private Sub LinkedButton0_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles LinkedButton0.ClickAfter
            If EditText27.Value.ToString = "" Then Exit Sub
            Link_Value = EditText27.Value : Link_objtype = "OHEM"
            Dim activeform As New frmEmployeeMaster
            activeform.Show()
        End Sub

#End Region

        Private Sub Activate_FitcolumWidth()
            Try
                objaddon.objapplication.Menus.Item("1300").Activate()
            Catch ex As Exception
            End Try
        End Sub

        Private Sub Matrix4_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix4.LostFocusAfter
            If pVal.ColUID = "trname" Then objaddon.objglobalmethods.Matrix_Addrow(Matrix4, "trname", "#")
        End Sub

        Private Sub Matrix5_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix5.LostFocusAfter
            If pVal.ColUID = "fname" Then objaddon.objglobalmethods.Matrix_Addrow(Matrix5, "fname", "#")
        End Sub

        Private Sub Matrix6_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix6.LostFocusAfter
            If pVal.ColUID = "edfrdt" Then objaddon.objglobalmethods.Matrix_Addrow(Matrix6, "edfrdt", "#")
        End Sub

        Private Sub Matrix7_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix7.LostFocusAfter
            If pVal.ColUID = "emfrom" Then objaddon.objglobalmethods.Matrix_Addrow(Matrix7, "emfrom", "#")
        End Sub

        Private Sub Matrix8_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix8.LostFocusAfter
            If pVal.ColUID = "frdate" Then objaddon.objglobalmethods.Matrix_Addrow(Matrix8, "frdate", "#")
        End Sub

        Private Sub Button0_ClickBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Button0.ClickBefore
            Try
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                    EditText49.Value = EditText3.Value
                    EditText48.Value = EditText3.Value
                End If
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Or objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE Then
                    If EditText0.Value.ToString = "" Then objaddon.objapplication.SetStatusBarMessage("Employee ID is Missing, Please Update", SAPbouiCOM.BoMessageTime.bmt_Short, True) : BubbleEvent = False : Exit Sub
                    If EditText1.Value.ToString = "" Then objaddon.objapplication.SetStatusBarMessage("Employee FirstName is Missing, Please Update", SAPbouiCOM.BoMessageTime.bmt_Short, True) : BubbleEvent = False : Exit Sub
                    If EditText32.Value.ToString = "" Then objaddon.objapplication.SetStatusBarMessage("Employee Joining Date is Missing, Please Update", SAPbouiCOM.BoMessageTime.bmt_Short, True) : BubbleEvent = False : Exit Sub
                    'If ComboBox3.Value.ToString = "" Then objaddon.objapplication.SetStatusBarMessage("Branch is Missing, Please Update", SAPbouiCOM.BoMessageTime.bmt_Short, True) : BubbleEvent = False
                    If ComboBox16.Value.ToString = "" Then objaddon.objapplication.SetStatusBarMessage("Location is Missing, Please Update", SAPbouiCOM.BoMessageTime.bmt_Short, True) : BubbleEvent = False : Exit Sub
                    If ComboBox10.Value.ToString = "" Then objaddon.objapplication.SetStatusBarMessage("Status is Missing, Please Update", SAPbouiCOM.BoMessageTime.bmt_Short, True) : BubbleEvent = False : Exit Sub
                    If GetPayrollEnabledIndia = False Then If ComboBox2.Value.ToString = "" Then objaddon.objapplication.SetStatusBarMessage("Department is Missing, Please Update", SAPbouiCOM.BoMessageTime.bmt_Short, True) : BubbleEvent = False : Exit Sub
                    If ComboBox0.Value.ToString = "" Then objaddon.objapplication.SetStatusBarMessage("Employee Group is Missing, Please Update", SAPbouiCOM.BoMessageTime.bmt_Short, True) : BubbleEvent = False : Exit Sub
                    If ComboBox17.Value.ToString = "" Then objaddon.objapplication.SetStatusBarMessage("Shift Code is Missing, Please Update", SAPbouiCOM.BoMessageTime.bmt_Short, True) : BubbleEvent = False : Exit Sub


                End If
                RemoveLastrow(Matrix0, "lvcode")
                RemoveLastrow(Matrix1, "pycode")
                RemoveLastrow(Matrix2, "idcode")
                RemoveLastrow(Matrix3, "skcode")
                RemoveLastrow(Matrix4, "trname")
                RemoveLastrow(Matrix5, "fname")
                RemoveLastrow(Matrix6, "edfrdt")
                RemoveLastrow(Matrix7, "emfrom")
                RemoveLastrow(Matrix8, "frdate")
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                    EditText49.Value = EditText3.Value
                    EditText48.Value = EditText3.Value
                End If
                If GetPayrollEnabledIndia = False Then
                    Dim oDT As New DataTable
                    oDT.Columns.Add(Matrix1.Columns.Item("pyeff").UniqueID, GetType(Date))
                    oDT.Columns.Add(Matrix1.Columns.Item("pycode").UniqueID, GetType(String))
                    For iRow As Integer = 1 To Matrix1.VisualRowCount
                        If Matrix1.Columns.Item("pyeff").Cells.Item(iRow).Specific.string <> "" Then
                            Dim oRow As DataRow = oDT.NewRow
                            Dim EFDate As Date = Date.ParseExact(Matrix1.Columns.Item("pyeff").Cells.Item(iRow).Specific.value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                            oRow.Item(Matrix1.Columns.Item("pyeff").UniqueID) = EFDate
                            oRow.Item(Matrix1.Columns.Item("pycode").UniqueID) = Matrix1.Columns.Item("pycode").Cells.Item(iRow).Specific.value
                            If oRow("pyeff").ToString.Trim = "" Then Continue For
                            oDT.Rows.Add(oRow)
                        End If
                    Next
                    Dim GetDTNew = From dr In oDT.AsEnumerable()
                                   Group dr By Ph = New With {Key .pycode = dr.Field(Of String)("pycode"), Key .pyeff = dr.Field(Of Date)("pyeff")} Into drg = Group
                                   Where drg.Count() > 1
                                   Select New With {
    .EffDate = drg.Count(),
    .PyCode = Ph.ToString
    }
                    If GetDTNew.Count > 0 Then
                        objaddon.objapplication.SetStatusBarMessage("Duplicate Pay Element Found...", SAPbouiCOM.BoMessageTime.bmt_Short, True) : BubbleEvent = False
                    End If
                End If
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

        Private Sub Matrix0_LinkPressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix0.LinkPressedAfter
            If Matrix0.Columns.Item("lvcode").Cells.Item(pVal.Row).Specific.string <> "" Then
                Link_Value = Matrix0.Columns.Item("lvcode").Cells.Item(pVal.Row).Specific.string : Link_objtype = "MSTRLEVE"
                Dim activeform As New frmLeaveMaster
                activeform.Show()
            End If
        End Sub

        Private Sub Matrix1_LinkPressedAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix1.LinkPressedAfter
            If Matrix1.Columns.Item("pycode").Cells.Item(pVal.Row).Specific.string <> "" Then
                Link_Value = Matrix1.Columns.Item("pycode").Cells.Item(pVal.Row).Specific.string : Link_objtype = "MSTRPAYE"
                Dim activeform As New frmPayElement
                activeform.Show()
            End If
        End Sub

        Private Sub ComboBox22_ComboSelectAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox22.ComboSelectAfter
            If ComboBox22.Selected Is Nothing Then Exit Sub
            objaddon.objglobalmethods.LoadCombo(ComboBox13, "SELECT ""Code"", ""Name"" FROM ""OCST"" WHERE ""Country""='" & ComboBox22.Selected.Value & "'", Nothing)
        End Sub

        Private Sub ComboBox24_ComboSelectAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox24.ComboSelectAfter
            If ComboBox24.Selected Is Nothing Then Exit Sub
            objaddon.objglobalmethods.LoadCombo(ComboBox23, "SELECT ""Code"", ""Name"" FROM ""OCST"" WHERE ""Country""='" & ComboBox24.Selected.Value & "'", Nothing)
        End Sub

        Private Sub ComboBox3_ComboSelectAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles ComboBox3.ComboSelectAfter
            Try
                If GetPayrollEnabledIndia Then
                    Dim Location As String = ""
                    Location = objaddon.objglobalmethods.getSingleValue(" select T1.""Location"" from OBPL T0 join OWHS T1 on T0.""DflWhs""=T1.""WhsCode"" where T0.""BPLId""='" & ComboBox3.Selected.Value & "'")
                    If Location <> "" Then
                        ComboBox16.Select(Location, SAPbouiCOM.BoSearchKey.psk_ByValue)
                        objform.ActiveItem = "txtmgrcode"
                        ComboBox16.Item.Enabled = False
                    Else
                        Exit Sub
                    End If
                End If
            Catch ex As Exception

            End Try
        End Sub


#Region "Employee Profile Photo"

        Private Sub PictureBox1_DoubleClickBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles PictureBox1.DoubleClickBefore
            Try
                'If PictureBox1.Picture <> "" Then PictureBox1.Picture = ""
                Try
                    Dim ShowFolderBrowserThread As New Threading.Thread(AddressOf ShowFolderBrowser)
                    If ShowFolderBrowserThread.ThreadState = System.Threading.ThreadState.Unstarted Then
                        ShowFolderBrowserThread.SetApartmentState(Threading.ApartmentState.STA)
                        ShowFolderBrowserThread.Start()
                    ElseIf ShowFolderBrowserThread.ThreadState = System.Threading.ThreadState.Stopped Then
                        ShowFolderBrowserThread.Start()
                        ShowFolderBrowserThread.Join()
                    End If
                Catch ex As Exception
                End Try
                BubbleEvent = False
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Button0_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button0.PressedAfter
            Try
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE Then Exit Sub
                If pVal.ActionSuccess = True And objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                    Dim GetValue As String = objaddon.objglobalmethods.getSingleValue("select ""U_AGCode"" from ""@SMPR_ACCT""")
                    If GetValue <> "" Then
                        EditText0.Value = GetValue + objaddon.objglobalmethods.GetDigitCode(objaddon.objglobalmethods.GetNextCode_Value("@SMPR_OHEM"))
                    End If
                    EditText48.Value = objaddon.objglobalmethods.GetNextCode_Value("@SMPR_OHEM")
                    EditText49.Value = EditText48.Value
                    EditText3.Value = EditText48.Value

                End If
                If PictureBox1.Picture = "" Then LoadImage("DefaultImage.jpg")
            Catch ex As Exception

            End Try
        End Sub

        'Private Sub Button2_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button2.ClickAfter
        '    Try
        '        Try
        '            Dim ShowFolderBrowserThread As New Threading.Thread(AddressOf ShowFolderBrowser)
        '            If ShowFolderBrowserThread.ThreadState = System.Threading.ThreadState.Unstarted Then
        '                ShowFolderBrowserThread.SetApartmentState(Threading.ApartmentState.STA)
        '                ShowFolderBrowserThread.Start()
        '            ElseIf ShowFolderBrowserThread.ThreadState = System.Threading.ThreadState.Stopped Then
        '                ShowFolderBrowserThread.Start()
        '                ShowFolderBrowserThread.Join()
        '            End If
        '        Catch ex As Exception
        '        End Try
        '    Catch ex As Exception
        '    End Try
        'End Sub

        Public Sub ShowFolderBrowser()
            Dim oform As SAPbouiCOM.Form
            oform = objaddon.objapplication.Forms.GetForm("MSTREMPL", FormCount)
            Try
                Dim nws As New NativeWindow()
                Dim opendialog As New OpenFileDialog()
                Dim FileName As String = Nothing
                nws.AssignHandle(System.Diagnostics.Process.GetProcessesByName("SAP Business One")(0).MainWindowHandle)

                If opendialog.ShowDialog(nws) = System.Windows.Forms.DialogResult.OK Then FileName = opendialog.FileName
                If FileName = "" Then Exit Sub

                MoveFile(FileName)

                If oform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then oform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE
            Catch ex As Exception
            Finally
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oform)
                GC.WaitForPendingFinalizers()
                GC.Collect()
            End Try
        End Sub

        Private Sub LoadImage(ByVal imagename As String)
            Try
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                'objrs.DoQuery("select U_emppath from OADM")
                objrs.DoQuery("select ""BitmapPath"" from OADP")
                If objrs.RecordCount = 0 Then Exit Sub
                If objrs.Fields.Item(0).Value.ToString = "" Then Exit Sub
                PictureBox1.Picture = objrs.Fields.Item(0).Value.ToString + imagename.ToString
            Catch ex As Exception

            End Try
        End Sub

        Private Sub MoveFile(ByVal SourceImagename As String)
            Try
                Dim despath As String
                despath = ""
                objrs = objaddon.objcompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                'objrs.DoQuery("select U_emppath from OADM")
                objrs.DoQuery("select ""BitmapPath"" from OADP")
                If objrs.RecordCount = 0 Then Exit Sub
                If objrs.Fields.Item(0).Value.ToString = "" Then Exit Sub
                despath = objrs.Fields.Item(0).Value.ToString + Path.GetFileName(SourceImagename)
                Try
                    My.Computer.FileSystem.CopyFile(SourceImagename.ToString, despath.ToString)
                    LoadImage(Path.GetFileName(Path.GetFileName(SourceImagename)))
                Catch ex As Exception
                    'objaddon.objapplication.SetStatusBarMessage("Image Name Already Exists. Please Give new Name.", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                    If PictureBox1.Picture = "" Then LoadImage("DefaultImage.jpg")
                End Try
                'My.Computer.FileSystem.DeleteFile(sourpath.ToString)
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Button3_ClickAfter(sboObject As System.Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Button3.ClickAfter
            Try
                If PictureBox1.Picture.Contains("DefaultImage") Then

                End If
                ' objaddon.objapplication.SetStatusBarMessage("Default Image Cannot be removed.Please Check it", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                If objaddon.objapplication.MessageBox("Removing Image Will remove the Image from the attachement Folder Also.To Proceed Press Yes", 1, "Yes", "No") = 1 Then
                    My.Computer.FileSystem.DeleteFile(PictureBox1.Picture)
                    LoadImage("DefaultImage.jpg")
                    If objform.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then objform.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub EditText1_LostFocusAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles EditText1.LostFocusAfter
            Try
                If objform.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE Or objform.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then Exit Sub
                Dim GetValue As String = objaddon.objglobalmethods.getSingleValue("select ""U_AGCode"" from ""@SMPR_ACCT""")
                If GetValue <> "" Then
                    EditText0.Value = GetValue + objaddon.objglobalmethods.GetDigitCode(Trim(EditText48.Value))
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Folder5_PressedAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Folder5.PressedAfter
            Try 'Bank
                If GetPayrollEnabledIndia = False Then
                    StaticText88.Item.Visible = False
                    EditText69.Item.Visible = False
                    StaticText89.Item.Visible = False
                    EditText70.Item.Visible = False
                End If

            Catch ex As Exception

            End Try

        End Sub

        Private Sub Matrix1_ValidateAfter(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg) Handles Matrix1.ValidateAfter
            Try
                If pVal.InnerEvent = True Then Exit Sub
                If pVal.ItemChanged = False Then Exit Sub
                If GetPayrollEnabledIndia = False Then
                    Select Case pVal.ColUID
                        Case "pyeff"
                            Salary_Tab_Restriction()
                            '    Dim EffDate As Date
                            '    Dim oDT As New DataTable
                            '    oDT.Columns.Add(Matrix1.Columns.Item("pyeff").UniqueID, GetType(Date))
                            '    For iRow As Integer = 1 To Matrix1.VisualRowCount
                            '        If Matrix1.Columns.Item("pyeff").Cells.Item(iRow).Specific.string <> "" Then
                            '            Dim oRow As DataRow = oDT.NewRow
                            '            Dim EFDate As Date = Date.ParseExact(Matrix1.Columns.Item("pyeff").Cells.Item(iRow).Specific.value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                            '            oRow.Item(Matrix1.Columns.Item("pyeff").UniqueID) = EFDate 'Matrix1.Columns.Item("pyeff").Cells.Item(iRow).Specific.string
                            '            If oRow("pyeff").ToString.Trim = "" Then Continue For
                            '            oDT.Rows.Add(oRow)
                            '        End If
                            '    Next
                            '    If oDT.Rows.Count > 0 Then
                            '        Dim GetDT = From dr In oDT.AsEnumerable()
                            '                    Group dr By Ph = New With {Key .pyeff = dr.Field(Of Date)("pyeff")} Into drg = Group
                            '                    Order By drg.Max(Function(dr) dr.Field(Of Date)("pyeff")) Descending
                            '                    Select New With {
                            '.EffDate = drg.Max(Function(dr) dr.Field(Of Date)("pyeff"))
                            '}
                            '        For Each RowID In GetDT
                            '            EffDate = RowID.EffDate
                            '            Exit For
                            '        Next
                            '        Dim pyEffDate As String = EffDate.ToString("yyyyMMdd")
                            '        For i As Integer = Matrix1.VisualRowCount To 1 Step -1
                            '            If Matrix1.Columns.Item("pyeff").Cells.Item(i).Specific.string <> "" Then
                            '                Dim FDate As Date = Date.ParseExact(Matrix1.Columns.Item("pyeff").Cells.Item(i).Specific.value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                            '                Dim FDate1 As Date = Date.ParseExact(pyEffDate, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                            '                If FDate < FDate1 Then
                            '                    'Matrix1.CommonSetting.SetRowEditable(i, False)
                            '                    Matrix1.CommonSetting.SetCellEditable(i, 1, False)
                            '                    Matrix1.CommonSetting.SetCellEditable(i, 3, False)
                            '                    Matrix1.CommonSetting.SetCellEditable(i, 6, False)
                            '                Else
                            '                    'Matrix1.CommonSetting.SetRowEditable(i, True)
                            '                    Matrix1.CommonSetting.SetCellEditable(i, 1, True)
                            '                    Matrix1.CommonSetting.SetCellEditable(i, 3, True)
                            '                    Matrix1.CommonSetting.SetCellEditable(i, 6, True)
                            '                End If
                            '            Else
                            '                Matrix1.CommonSetting.SetCellEditable(i, 1, True)
                            '                Matrix1.CommonSetting.SetCellEditable(i, 3, True)
                            '                Matrix1.CommonSetting.SetCellEditable(i, 6, True)
                            '            End If
                            '        Next
                            '    Else
                            '        Matrix1.CommonSetting.SetCellEditable(i, 1, True)
                            '        Matrix1.CommonSetting.SetCellEditable(i, 3, True)
                            '        Matrix1.CommonSetting.SetCellEditable(i, 6, True)
                            '    End If
                    End Select
                End If

            Catch ex As Exception
                objaddon.objapplication.StatusBar.SetText("Matrix: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
            End Try

        End Sub

        Private Sub Salary_Tab_Restriction()
            Try
                Dim EffDate As Date
                Dim PayCode As String
                Dim oDT As New DataTable
                oDT.Columns.Add(Matrix1.Columns.Item("pyeff").UniqueID, GetType(Date))
                oDT.Columns.Add(Matrix1.Columns.Item("pycode").UniqueID, GetType(String))
                For iRow As Integer = 1 To Matrix1.VisualRowCount
                    If Matrix1.Columns.Item("pyeff").Cells.Item(iRow).Specific.string <> "" Then
                        Dim oRow As DataRow = oDT.NewRow
                        Dim EFDate As Date = Date.ParseExact(Matrix1.Columns.Item("pyeff").Cells.Item(iRow).Specific.value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                        PayCode = Matrix1.Columns.Item("pycode").Cells.Item(iRow).Specific.value
                        oRow.Item(Matrix1.Columns.Item("pyeff").UniqueID) = EFDate
                        oRow.Item(Matrix1.Columns.Item("pycode").UniqueID) = PayCode
                        If oRow("pyeff").ToString.Trim = "" Then Continue For
                        oDT.Rows.Add(oRow)
                    End If
                Next
                If oDT.Rows.Count > 0 Then
                    '        Dim GetDT = From dr In oDT.AsEnumerable()
                    '                    Group dr By Ph = New With {Key .pycode = dr.Field(Of String)("pycode")} Into drg = Group 'New With {Key .pyeff = dr.Field(Of Date)("pyeff")} Into drg = Group
                    '                    Order By drg.Max(Function(dr) dr.Field(Of Date)("pyeff")) Descending
                    '                    Select New With {
                    '.EffDate = drg.Max(Function(dr) dr.Field(Of Date)("pyeff")),
                    '.PyCode = Ph.ToString
                    '}



                    For i As Integer = 1 To Matrix1.VisualRowCount
                        If Matrix1.Columns.Item("pyeff").Cells.Item(i).Specific.string <> "" Then
                            Dim ss As SAPbouiCOM.IEditText = Matrix1.GetCellSpecific(1, i)
                            Dim FDate As Date = Date.ParseExact(Matrix1.Columns.Item("pyeff").Cells.Item(i).Specific.value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                            '                Dim GetDTNew = From dr In oDT.AsEnumerable()
                            '                               Group dr By Ph = New With {Key .pycode = dr.Field(Of String)("pycode")} Into drg = Group 'New With {Key .pyeff = dr.Field(Of Date)("pyeff")} Into drg = Group , Key .pyeff = dr.Field(Of Date)("pyeff")
                            '                               Where Ph.pycode = ss.Value And drg.Count() > 1
                            '                               Select New With {
                            '.EffDate = drg.Min(Function(dr) dr.Field(Of Date)("pyeff")),
                            '.PyCode = Ph.ToString
                            '}
                            Dim GetDTNew = From dr In oDT.AsEnumerable()
                                           Group dr By Ph = New With {Key .pycode = dr.Field(Of String)("pycode")} Into drg = Group 'New With {Key .pyeff = dr.Field(Of Date)("pyeff")} Into drg = Group , Key .pyeff = dr.Field(Of Date)("pyeff")
                                           Where Ph.pycode = ss.Value And Not FDate.Equals(drg.Max(Function(dr) dr.Field(Of Date)("pyeff"))) 'And drg.Count() > 1
                                           Select New With {
            .EffDate = drg.Max(Function(dr) dr.Field(Of Date)("pyeff")),
            .PyCode = Ph.ToString
            }
                            If GetDTNew.Count > 0 Then
                                EffDate = GetDTNew.ElementAt(0).EffDate 'RowID.EffDate
                                Dim pyEffDate As String = EffDate.ToString("yyyyMMdd")

                                Dim FDate1 As Date = Date.ParseExact(pyEffDate, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                                If FDate < FDate1 Then
                                    Matrix1.CommonSetting.SetCellEditable(i, 1, False) : Matrix1.CommonSetting.SetCellEditable(i, 3, False) : Matrix1.CommonSetting.SetCellEditable(i, 6, False)
                                Else
                                    Matrix1.CommonSetting.SetCellEditable(i, 1, True) : Matrix1.CommonSetting.SetCellEditable(i, 3, True) : Matrix1.CommonSetting.SetCellEditable(i, 6, True)
                                End If
                            Else
                                Matrix1.CommonSetting.SetCellEditable(i, 1, True) : Matrix1.CommonSetting.SetCellEditable(i, 3, True) : Matrix1.CommonSetting.SetCellEditable(i, 6, True)
                            End If
                        End If
                    Next

                    'For Each RowID In GetDT
                    '    EffDate = RowID.EffDate
                    '    Exit For
                    'Next
                    'Dim pyEffDate As String = EffDate.ToString("yyyyMMdd")
                    'For i As Integer = Matrix1.VisualRowCount To 1 Step -1
                    '    If Matrix1.Columns.Item("pyeff").Cells.Item(i).Specific.string <> "" Then
                    '        Dim FDate As Date = Date.ParseExact(Matrix1.Columns.Item("pyeff").Cells.Item(i).Specific.value, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                    '        Dim FDate1 As Date = Date.ParseExact(pyEffDate, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                    '        If FDate <FDate1 Then
                    '            Matrix1.CommonSetting.SetCellEditable(i, 1, False)
                            '            Matrix1.CommonSetting.SetCellEditable(i, 3, False)
                            '            Matrix1.CommonSetting.SetCellEditable(i, 6, False)
                            '        Else
                            '            Matrix1.CommonSetting.SetCellEditable(i, 1, True)
                            '            Matrix1.CommonSetting.SetCellEditable(i, 3, True)
                            '            Matrix1.CommonSetting.SetCellEditable(i, 6, True)
                            '        End If
                            '    Else
                            '        Matrix1.CommonSetting.SetCellEditable(i, 1, True)
                            '        Matrix1.CommonSetting.SetCellEditable(i, 3, True)
                            '        Matrix1.CommonSetting.SetCellEditable(i, 6, True)
                            '    End If
                            'Next

                        Else
                    For i As Integer = 1 To Matrix1.VisualRowCount
                        Matrix1.CommonSetting.SetCellEditable(i, 1, True) : Matrix1.CommonSetting.SetCellEditable(i, 3, True) : Matrix1.CommonSetting.SetCellEditable(i, 6, True)
                    Next

                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Matrix1_ValidateBefore(sboObject As Object, pVal As SAPbouiCOM.SBOItemEventArg, ByRef BubbleEvent As Boolean) Handles Matrix1.ValidateBefore
            Try
                If pVal.InnerEvent = True Then Exit Sub
                If pVal.ColUID <> "pyamount" Then Exit Sub
                If Matrix1.Columns.Item("pycode").Cells.Item(pVal.Row).Specific.string <> "" And Val(Matrix1.Columns.Item("pyamount").Cells.Item(pVal.Row).Specific.String) = 0 Then
                    objaddon.objapplication.StatusBar.SetText("Amount is Missing on Line: " & pVal.Row, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
                    Matrix1.Columns.Item("pyamount").Cells.Item(pVal.Row).Click() : BubbleEvent = False
                End If
            Catch ex As Exception

            End Try

        End Sub

#End Region
    End Class
End Namespace
