﻿Imports System.Data.OleDb
Imports System.Data
Partial Class PRG_ANNTY_POLY_CONVERT
    Inherits System.Web.UI.Page
    Protected FirstMsg As String
    Protected PageLinks As String

    Protected STRMENU_TITLE As String

    Protected strStatus As String
    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean

    Protected strF_ID As String
    Protected strQ_ID As String
    Protected strP_ID As String

    Dim strREC_ID As String
    Protected strOPT As String = "0"

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Dim strTmp_Value As String = ""

    Dim myarrData() As String

    Dim strErrMsg As String
    Protected strUpdate_Sw As String
    Dim Pol_Eff_Date

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strTableName = "TBIL_ANN_POLICY_DET"

        STRMENU_TITLE = UCase("+++ Convert Proposal to Policy +++ ")

        Try
            strF_ID = CType(Request.QueryString("optfileid"), String)
        Catch ex As Exception
            strF_ID = ""
        End Try

        Try
            strQ_ID = CType(Request.QueryString("optquotid"), String)
        Catch ex As Exception
            strQ_ID = ""
        End Try

        Try
            strP_ID = CType(Request.QueryString("optpolid"), String)
        Catch ex As Exception
            strP_ID = ""
        End Try

        If Not (Page.IsPostBack) Then
            Call Proc_DoNew()

            Me.cmdFileNum.Enabled = True
            Me.BUT_OK.Enabled = False
            If strF_ID = "" Then
                Me.txtPro_Pol_Num.Text = "QI/2014/1501/E/E003/I/0000001"
            Else
                txtPro_Pol_Num.Text = RTrim(strQ_ID)
            End If
            If strQ_ID = "" Then
                Me.txtFileNum.Text = "6004025"
            Else
                txtFileNum.Text = RTrim(strF_ID)
            End If

            Me.txtPro_Pol_Num.Enabled = True
            Me.txtPro_Pol_Num.Focus()
        End If


        If Me.txtAction.Text = "New" Then
            Me.txtPro_Pol_Num.Text = ""
            Call Proc_DoNew()
            Me.txtAction.Text = ""
            Me.lblMsg.Text = "New Entry..."

            Me.txtPro_Pol_Num.Enabled = True
            Me.txtPro_Pol_Num.Focus()
        End If
    End Sub

    Protected Sub cmdFileNum_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFileNum.Click
        Dim xc As Integer = 0

        If Trim(Me.txtPro_Pol_Num.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblPro_Pol_Num.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        End If

        For xc = 1 To Len(LTrim(RTrim(Me.txtPro_Pol_Num.Text)))
            If Mid(LTrim(RTrim(Me.txtPro_Pol_Num.Text)), xc, 1) = ";" Or Mid(LTrim(RTrim(Me.txtPro_Pol_Num.Text)), xc, 1) = ":" Then
                Me.lblMsg.Text = "Invalid character found in input field - " & Me.lblPro_Pol_Num.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
                Exit Sub
            End If
        Next

        If Trim(Me.txtFileNum.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblFileNum.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        End If

        For xc = 1 To Len(LTrim(RTrim(Me.txtFileNum.Text)))
            If Mid(LTrim(RTrim(Me.txtFileNum.Text)), xc, 1) = ";" Or Mid(LTrim(RTrim(Me.txtFileNum.Text)), xc, 1) = ":" Then
                Me.lblMsg.Text = "Invalid character found in input field - " & Me.lblFileNum.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
                Exit Sub
            End If
        Next

        Me.lblPWD.Enabled = False
        Me.txtPWD.Enabled = False
        Me.BUT_OK.Enabled = False

        blnStatus = Proc_DoGet_Record(RTrim("PRO"), Trim(Me.txtPro_Pol_Num.Text), RTrim(Me.txtFileNum.Text))
        If blnStatus = True Then
            Me.chkAccept.Enabled = True
            'Me.BUT_OK.Enabled = True
            Exit Sub
        Else
            Me.chkAccept.Enabled = False
            Me.chkAccept.Checked = False
            Me.lblPWD.Enabled = False
            Me.txtPWD.Enabled = False
            Me.BUT_OK.Enabled = False
            Exit Sub
        End If
    End Sub

    Protected Sub chkAccept_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAccept.CheckedChanged
        If Me.chkAccept.Checked = True Then
            Me.lblPWD.Enabled = True
            Me.txtPWD.Enabled = True
            Me.BUT_OK.Enabled = True
        Else
            Me.lblPWD.Enabled = False
            Me.txtPWD.Enabled = False
            Me.BUT_OK.Enabled = False
        End If
    End Sub

    Protected Sub BUT_OK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_OK.Click
        Call Proc_DoConvert()
    End Sub
    Private Sub Proc_DoConvert()
        Dim xc As Integer = 0


        If Trim(Me.txtPro_Pol_Num.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblPro_Pol_Num.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        End If

        For xc = 1 To Len(LTrim(RTrim(Me.txtPro_Pol_Num.Text)))
            If Mid(LTrim(RTrim(Me.txtPro_Pol_Num.Text)), xc, 1) = ";" Or Mid(LTrim(RTrim(Me.txtPro_Pol_Num.Text)), xc, 1) = ":" Then
                Me.lblMsg.Text = "Invalid character found in input field - " & Me.lblPro_Pol_Num.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
                Exit Sub
            End If
        Next

        If Trim(Me.txtFileNum.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblFileNum.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        End If

        For xc = 1 To Len(LTrim(RTrim(Me.txtFileNum.Text)))
            If Mid(LTrim(RTrim(Me.txtFileNum.Text)), xc, 1) = ";" Or Mid(LTrim(RTrim(Me.txtFileNum.Text)), xc, 1) = ":" Then
                Me.lblMsg.Text = "Invalid character found in input field - " & Me.lblFileNum.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
                Exit Sub
            End If
        Next


        Dim strMyMsg As String = ""
        Dim dteTrans As Date = Now

        Dim strMyYear As String = ""
        Dim strMyMth As String = ""
        Dim strMyDay As String = ""

        Dim strMyDte As String = ""
        Dim strMyDteX As String = ""

        Dim dteStart As Date = Now
        Dim dteEnd As Date = Now

        Dim dteStart_RW As Date = Now
        Dim dteEnd_RW As Date = Now

        Dim mydteX As String = ""
        Dim mydte As Date = Now

        'Dim dteDOB As Date = Now

        'Dim lngDOB_ANB As Integer = 0
        'Dim Dte_Proposal As Date = Now
        'Dim Dte_Commence As Date = Now
        'Dim Dte_DOB As Date = Now
        'Dim Dte_Maturity As Date = Now

        Dim myYear As Integer

        'Validate date
        Dim str() As String
        str = DoDate_Process(txtTrans_Date.Text, txtTrans_Date)
        If (str(2) = Nothing) Then
            Dim errMsg = str(0).Insert(18, " receipt date, ")
            lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            lblMsg.Visible = True
            txtTrans_Date.Focus()
            ' ErrorInd = "Y"
            Exit Sub
        Else
            txtTrans_Date.Text = str(2).ToString()
        End If
        'Obtain Year from transaction date
        strMyYear = Year(Convert.ToDateTime(DoConvertToDbDateFormat(txtTrans_Date.Text)))
        If Val(strMyYear) < 1999 Then
            Me.lblMsg.Text = "Error. Receipt year date is less than 1999 ..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        dteTrans = Convert.ToDateTime(DoConvertToDbDateFormat(txtTrans_Date.Text))
        If dteTrans > Now Then
            lblMsg.Text = "Receipt date must not be greater than today"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            Exit Sub
        End If


        If RTrim(Me.txtTrans_Num.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblTrans_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            Exit Sub
        End If

        Call MOD_GEN.gnInitialize_Numeric(Me.txtTrans_Amt)
        If Val(Me.txtTrans_Amt.Text) = 0 Then
            Me.lblMsg.Text = "Missing " & Me.lblTrans_Amt.Text & " or Value is zero..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            Exit Sub
        End If

        Dim premiumAmount = GetPremiumPayment(txtPro_Pol_Num.Text)

        If premiumAmount <> Val(txtTrans_Amt.Text) Then
            Me.lblMsg.Text = "Transaction Amount must be equal to Premium Payment"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            txtTrans_Amt.Focus()
            Exit Sub
        End If

        If Trim(Me.txtPWD.Text) <> "pro_to_pol" Then
            Me.lblMsg.Text = "Invalid Access or Password code..."
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        End If

        'Me.lblMsg.Text = "About to save data into database..."
        'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
        'Exit Sub

        '   Trim(myYear)
        '   RTrim(Me.txtBraNum.Text)
        '   RTrim(Me.txtProductClass.Text)
        '   RTrim(Me.txtProduct_Num.Text)
        '   
        myYear = Trim(Me.txtYear.Text)
        If Trim(txtPol_Num.Text) = "" Then
            'Me.txtPol_Num.Text = MOD_GEN.gnGet_Serial_File_Proposal_Policy(RTrim("GET_SN_AN_FIL_PRO_POL"), RTrim("POL"), Trim(myYear), RTrim("IL"), RTrim(Me.txtBraNum.Text), RTrim(Me.txtProductClass.Text), RTrim(Me.txtProduct_Num.Text), RTrim("I"), RTrim(""), RTrim(""))
            Me.txtPol_Num.Text = MOD_GEN.gnGet_Serial_File_Proposal_Policy(RTrim("GET_SN_AN_FIL_PRO_POL"), RTrim("POL"), Trim(myYear), RTrim("AN"), RTrim(Me.txtBraNum.Text), RTrim(Me.txtProductClass.Text), RTrim(Me.txtProduct_Num.Text), RTrim("A"), RTrim(""), RTrim(""))
        End If

        If Trim(txtPol_Num.Text) = "" Or Trim(Me.txtPol_Num.Text) = "0" Or Trim(Me.txtPol_Num.Text) = "*" Then
            Me.txtPol_Num.Text = ""
            Me.lblMsg.Text = "Sorry!. Unable to get the next POLICY NO. Please contact your service provider..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.lblMsg.Text = "Status:"
            Exit Sub
        ElseIf Trim(Me.txtPol_Num.Text) = "PARAM_ERR" Then
            Me.txtPol_Num.Text = ""
            Me.lblMsg.Text = "Sorry!. Unable to get the next POLICY NO - INVALID PARAMETER(S)..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.lblMsg.Text = "Status:"
            Exit Sub
        ElseIf Trim(Me.txtPol_Num.Text) = "DB_ERR" Then
            Me.txtPol_Num.Text = ""
            Me.lblMsg.Text = "Sorry!. Unable to connect to database. Please contact your service provider..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.lblMsg.Text = "Status:"
            Exit Sub
        ElseIf Trim(Me.txtPol_Num.Text) = "ERR_ERR" Then
            Me.txtPol_Num.Text = ""
            Me.lblMsg.Text = "Sorry!. Unable to get connection object. Please contact your service provider..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.lblMsg.Text = "Status:"
            Exit Sub
        End If

        Dim myUserIDX As String = ""
        Try
            myUserIDX = CType(Session("MyUserIDX"), String)
        Catch ex As Exception
            myUserIDX = ""
        End Try

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection()
        objOLEConn.ConnectionString = mystrCONN

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn = Nothing
            Exit Sub
        End Try


        Dim intRC As Integer = 0

        Dim objOLETran As OleDbTransaction
        ' Start a local transaction.
        objOLETran = objOLEConn.BeginTransaction

        Try

            Dim objOLECmd As OleDbCommand = Nothing
            ' update policy table
            strTable = strTableName
            strSQL = ""
            strSQL = "UPDATE " & strTable
            strSQL = strSQL & " SET TBIL_ANN_POLY_POLICY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            strSQL = strSQL & " ,TBIL_ANN_POLY_PROPSL_ACCPT_STATUS = 'A'"
            ' 'strSQL = strSQL & " ,TBIL_ANN_POLY_PROPSL_ACCPT_DT = '" & CDate(Format(Now, "MM/dd/yyyy")) & "'"
            strSQL = strSQL & " ,TBIL_ANN_POLY_PROPSL_ACCPT_DT = '" & Format(Now, "MM/dd/yyyy") & "'"
            strSQL = strSQL & " ,TBIL_ANN_POLY_PRPSAL_RECD_DT = '" & Format(Now, "MM/dd/yyyy") & "'"
            strSQL = strSQL & " ,TBIL_POLICY_ISSUE_DT = '" & Format(Now, "MM/dd/yyyy") & "'"
            'strSQL = strSQL & " ,TBIL_POLICY_EFF_DT = '" & Pol_Eff_Date & "'"
            'strSQL = strSQL & " ,TBIL_POLICY_EFF_DT = '" & Format(CDate(Me.txtPol_Eff_Date.Text), "MM/dd/yyyy") & "'"
            strSQL = strSQL & " ,TBIL_POLICY_EFF_DT = '" & Convert.ToDateTime(DoConvertToDbDateFormat(Me.txtPol_Eff_Date.Text)) & "'"
            strSQL = strSQL & " WHERE TBIL_ANN_POLY_PROPSAL_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            strSQL = strSQL & " AND TBIL_ANN_POLY_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"

            objOLECmd = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            objOLECmd.CommandType = CommandType.Text
            intRC = objOLECmd.ExecuteNonQuery()
            objOLECmd.Dispose()
            objOLECmd = Nothing

            Dim objOLECmd_X As OleDbCommand = Nothing

            ' '' update premium information table
            strTable = strTableName
            strTable = "TBIL_ANN_POLICY_PREM_INFO"
            strSQL = ""
            strSQL = "UPDATE " & strTable
            strSQL = strSQL & " SET TBIL_ANN_POL_PRM_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            strSQL = strSQL & " WHERE TBIL_ANN_POL_PRM_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            strSQL = strSQL & " AND TBIL_ANN_POL_PRM_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            objOLECmd_X.CommandType = CommandType.Text
            intRC = objOLECmd_X.ExecuteNonQuery()
            objOLECmd_X.Dispose()
            objOLECmd_X = Nothing

            ' update beneficiary table
            strTable = strTableName
            strTable = "TBIL_ANN_POLICY_BENEFRY"
            strSQL = ""
            strSQL = "UPDATE " & strTable
            strSQL = strSQL & " SET TBIL_ANN_BENF_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            strSQL = strSQL & " WHERE TBIL_ANN_BENF_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            strSQL = strSQL & " AND TBIL_ANN_BENF_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            objOLECmd_X.CommandType = CommandType.Text


            intRC = objOLECmd_X.ExecuteNonQuery()
            objOLECmd_X.Dispose()
            objOLECmd_X = Nothing


            '' '' update funeral table
            'strTable = strTableName
            'strTable = "TBIL_FUNERAL_SA_TAB"
            'strSQL = ""
            'strSQL = "UPDATE " & strTable
            'strSQL = strSQL & " SET TBIL_FUN_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            'strSQL = strSQL & " WHERE TBIL_FUN_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            'strSQL = strSQL & " AND TBIL_FUN_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            'objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            'objOLECmd_X.CommandType = CommandType.Text
            'intRC = objOLECmd_X.ExecuteNonQuery()
            'objOLECmd_X.Dispose()
            'objOLECmd_X = Nothing


            '' '' update additional cover table
            'strTable = strTableName
            'strTable = "TBIL_POLICY_ADD_PREM"
            'strSQL = ""
            'strSQL = "UPDATE " & strTable
            'strSQL = strSQL & " SET TBIL_POL_ADD_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            'strSQL = strSQL & " WHERE TBIL_POL_ADD_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            'strSQL = strSQL & " AND TBIL_POL_ADD_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            'objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            'objOLECmd_X.CommandType = CommandType.Text
            'intRC = objOLECmd_X.ExecuteNonQuery()
            'objOLECmd_X.Dispose()
            'objOLECmd_X = Nothing


            '' '' update medical information table
            'strTable = strTableName
            'strTable = "TBIL_POLICY_MEDIC_EXAM"
            'strSQL = ""
            'strSQL = "UPDATE " & strTable
            'strSQL = strSQL & " SET TBIL_POL_MED_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            'strSQL = strSQL & " WHERE TBIL_POL_MED_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            'strSQL = strSQL & " AND TBIL_POL_MED_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            'objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            'objOLECmd_X.CommandType = CommandType.Text
            'intRC = objOLECmd_X.ExecuteNonQuery()
            'objOLECmd_X.Dispose()
            'objOLECmd_X = Nothing


            '' '' update medical illness table
            'strTable = strTableName
            'strTable = "TBIL_POLICY_ILLNESS"
            'strSQL = ""
            'strSQL = "UPDATE " & strTable
            'strSQL = strSQL & " SET TBIL_POL_ILL_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            'strSQL = strSQL & " WHERE TBIL_POL_ILL_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            'strSQL = strSQL & " AND TBIL_POL_ILL_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            'objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            'objOLECmd_X.CommandType = CommandType.Text
            'intRC = objOLECmd_X.ExecuteNonQuery()
            'objOLECmd_X.Dispose()
            'objOLECmd_X = Nothing


            '' '' update policy charges table
            'strTable = strTableName
            'strTable = "TBIL_POLICY_CHG_DTLS"
            'strSQL = ""
            'strSQL = "UPDATE " & strTable
            'strSQL = strSQL & " SET TBIL_POLY_CHG_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            'strSQL = strSQL & " WHERE TBIL_POLY_CHG_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            'strSQL = strSQL & " AND TBIL_POLY_CHG_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            'objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            'objOLECmd_X.CommandType = CommandType.Text
            'intRC = objOLECmd_X.ExecuteNonQuery()
            'objOLECmd_X.Dispose()
            'objOLECmd_X = Nothing


            '' '' update policy loading and discount table
            'strTable = strTableName
            'strTable = "TBIL_POLICY_DISCT_LOAD"
            'strSQL = ""
            'strSQL = "UPDATE " & strTable
            'strSQL = strSQL & " SET TBIL_POL_DISC_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            'strSQL = strSQL & " WHERE TBIL_POL_DISC_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            'strSQL = strSQL & " AND TBIL_POL_DISC_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            'objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            'objOLECmd_X.CommandType = CommandType.Text
            'intRC = objOLECmd_X.ExecuteNonQuery()
            'objOLECmd_X.Dispose()
            'objOLECmd_X = Nothing


            '' '' update premium calculation details table
            'strTable = strTableName
            'strTable = "TBIL_POLICY_PREM_DETAILS"
            'strSQL = ""
            'strSQL = "UPDATE " & strTable
            'strSQL = strSQL & " SET TBIL_POL_PRM_DTL_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            'strSQL = strSQL & " WHERE TBIL_POL_PRM_DTL_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            'strSQL = strSQL & " AND TBIL_POL_PRM_DTL_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            'objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            'objOLECmd_X.CommandType = CommandType.Text
            'intRC = objOLECmd_X.ExecuteNonQuery()
            'objOLECmd_X.Dispose()
            'objOLECmd_X = Nothing


            '' '' update premium calculation details table
            'strTable = strTableName
            'strTable = "TBIL_POLICY_DOC_ITEMS"
            'strSQL = ""
            'strSQL = "UPDATE " & strTable
            'strSQL = strSQL & " SET TBIL_POL_ITEM_POLY_NO = '" & RTrim(txtPol_Num.Text) & "'"
            'strSQL = strSQL & " WHERE TBIL_POL_ITEM_PROP_NO = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            'strSQL = strSQL & " AND TBIL_POL_ITEM_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            'objOLECmd_X = New OleDbCommand(strSQL, objOLEConn, objOLETran)
            'objOLECmd_X.CommandType = CommandType.Text
            'intRC = objOLECmd_X.ExecuteNonQuery()
            'objOLECmd_X.Dispose()
            'objOLECmd_X = Nothing


            '-----------------------------------------------------------------------
            'START SAVE RECEIPT DATA
            '-----------------------------------------------------------------------

            strTable = strTableName
            strTable = "TBIL_POLICY_RECEIPT"

            strSQL = ""
            strSQL = "SELECT TOP 1 * FROM " & strTable
            strSQL = strSQL & " WHERE TBIL_RCT_FILE_NUM = '" & RTrim(txtFileNum.Text) & "'"
            strSQL = strSQL & " AND TBIL_RCT_PROPOSAL_NUM = '" & RTrim(txtPro_Pol_Num.Text) & "'"
            'strSQL = strSQL & " AND TBIL_RCT_POLICY_NUM = '" & RTrim(txtPol_Num.Text) & "'"

            Dim objDA As System.Data.OleDb.OleDbDataAdapter
            objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)
            'objDA.SelectCommand.Connection = objOLEConn
            objDA.SelectCommand.Transaction = objOLETran
            'objDA.SelectCommand.CommandType = CommandType.Text
            'objDA.SelectCommand.CommandText = strSQL
            'or
            'objDA.SelectCommand = New System.Data.OleDb.OleDbCommand(strSQL, objOleConn)

            Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
            m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

            Dim obj_DT As New System.Data.DataTable
            'Dim m_rwContact As System.Data.DataRow
            Dim intC As Integer = 0

            objDA.Fill(obj_DT)

            If obj_DT.Rows.Count = 0 Then
                '   Creating a new record
                Dim drNewRow As System.Data.DataRow
                drNewRow = obj_DT.NewRow()

                drNewRow("TBIL_RCT_MDLE") = RTrim("A")
                drNewRow("TBIL_RCT_FILE_NUM") = RTrim(Me.txtFileNum.Text)
                drNewRow("TBIL_RCT_PROPOSAL_NUM") = RTrim(Me.txtPro_Pol_Num.Text)
                drNewRow("TBIL_RCT_POLICY_NUM") = RTrim(Me.txtPol_Num.Text)

                drNewRow("TBIL_RCT_DATE") = dteTrans
                drNewRow("TBIL_RCT_NUM") = RTrim(Me.txtTrans_Num.Text)
                drNewRow("TBIL_RCT_AMT") = RTrim(Me.txtTrans_Amt.Text)

                drNewRow("TBIL_RCT_FLAG") = "A"
                drNewRow("TBIL_RCT_OPERID") = CType(myUserIDX, String)
                drNewRow("TBIL_RCT_KEYDTE") = Now

                obj_DT.Rows.Add(drNewRow)
                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                drNewRow = Nothing
                Me.lblMsg.Text = "New Record Saved to Database Successfully."
            Else

                With obj_DT
                    .Rows(0)("TBIL_RCT_MDLE") = RTrim("A")

                    .Rows(0)("TBIL_RCT_FILE_NUM") = RTrim(Me.txtFileNum.Text)
                    .Rows(0)("TBIL_RCT_PROPOSAL_NUM") = RTrim(Me.txtPro_Pol_Num.Text)
                    .Rows(0)("TBIL_RCT_POLICY_NUM") = RTrim(Me.txtPol_Num.Text)

                    .Rows(0)("TBIL_RCT_DATE") = dteTrans
                    .Rows(0)("TBIL_RCT_NUM") = RTrim(Me.txtTrans_Num.Text)
                    .Rows(0)("TBIL_RCT_AMT") = RTrim(Me.txtTrans_Amt.Text)

                End With

                obj_DT.Rows(0)("TBIL_RCT_FLAG") = "C"
                'obj_DT.Rows(0)("TBIL_RCT_OPERID") = CType(myUserIDX, String)
                'obj_DT.Rows(0)("TBIL_RCT_KEYDTE") = Now

                intC = objDA.Update(obj_DT)

                Me.lblMsg.Text = "Record Saved to Database Successfully."
            End If

            obj_DT.Dispose()
            obj_DT = Nothing

            m_cbCommandBuilder.Dispose()
            m_cbCommandBuilder = Nothing


            '-----------------------------------------------------------------------
            'END SAVE RECEIPT DATA
            '-----------------------------------------------------------------------

            ' Commit the transaction.
            objOLETran.Commit()


            If objDA.SelectCommand.Connection.State = ConnectionState.Open Then
                objDA.SelectCommand.Connection.Close()
            End If
            objDA.Dispose()
            objDA = Nothing



            Me.lblMsg.Text = "Proposal record converted to policy. Please note the Policy No generated..."
            strMyMsg = "Proposal record successfully converted to policy. \n\nPolicy No: " & Me.txtPol_Num.Text & "\n\nPlease note down the Policy No generated..."
            Me.chkAccept.Enabled = False
            Me.chkAccept.Checked = False
            Me.lblPWD.Enabled = False
            Me.txtPWD.Enabled = False
            Me.BUT_OK.Enabled = False

            Me.txtPro_Pol_Num.Enabled = False
            Me.txtFileNum.Enabled = False
            Me.cmdFileNum.Enabled = False
            strUpdate_Sw = "Y"

        Catch ex As Exception
            'Console.WriteLine(ex.Message)
            'Me.lblMsg.Text = "Proposal record conversion not successful..."
            Me.lblMsg.Text = "Error Occured. Reason: " & ex.Message.ToString
            strMyMsg = "Error Occured. Reason: " & ex.Message.ToString

            ' Try to rollback the transaction
            Try
                objOLETran.Rollback()
            Catch
                ' Do nothing here; transaction is not active.
            End Try

            strUpdate_Sw = "N"


        End Try


        objOLETran.Dispose()
        objOLETran = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing


        If UCase(strUpdate_Sw) = "Y" Then
            'Me.lblMsg.Text = "Proposal record converted to policy. Please note the Policy No generated..."
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & strMyMsg & "');", True)
        Else
            'Me.lblMsg.Text = "Proposal record conversion not successful..."
            FirstMsg = "Javascript:alert('" & strMyMsg & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & strMyMsg & "');", True)
        End If

    End Sub

    Private Function Proc_DoGet_Record(ByVal pvCode As String, ByVal pvProNo As String, ByVal pvFileNo As String) As Boolean

        blnStatusX = False

        Dim mystrCONN_Chk As String = ""

        Dim objOLEConn_Chk As OleDbConnection = Nothing
        Dim objOLECmd_Chk As OleDbCommand = Nothing
        Dim objOLEDR_Chk As OleDbDataReader

        Dim myTmp_Chk As String
        Dim myTmp_Ref As String
        myTmp_Chk = "N"
        myTmp_Ref = ""


        mystrCONN_Chk = CType(Session("connstr"), String)
        objOLEConn_Chk = New OleDbConnection()
        objOLEConn_Chk.ConnectionString = mystrCONN_Chk

        Try
            'open connection to database
            objOLEConn_Chk.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn_Chk = Nothing
            blnStatusX = False
            Return blnStatusX
            Exit Function
        End Try


        strREC_ID = pvProNo

        Try

            strTable = strTableName

            strSQL = ""
            strSQL = "SPAN_SELECT_ANN_PROPOSAL"

            objOLECmd_Chk = New OleDbCommand(strSQL, objOLEConn_Chk)
            ''objOLECmd_Chk.CommandTimeout = 180
            objOLECmd_Chk.CommandType = CommandType.StoredProcedure

            objOLECmd_Chk.Parameters.Add("p01", OleDbType.VarChar, 40).Value = LTrim(RTrim(strREC_ID))
            objOLECmd_Chk.Parameters.Add("p02", OleDbType.VarChar, 40).Value = LTrim(RTrim(pvFileNo))
            objOLECmd_Chk.Parameters.Add("p03", OleDbType.VarChar, 3).Value = RTrim("A")

            objOLEDR_Chk = objOLECmd_Chk.ExecuteReader()
            If (objOLEDR_Chk.Read()) Then

                Me.txtFileNum.Text = RTrim(CType(objOLEDR_Chk("TBIL_ANN_POLY_FILE_NO") & vbNullString, String))
                Me.txtPol_Num.Text = RTrim(CType(objOLEDR_Chk("TBIL_ANN_POLY_POLICY_NO") & vbNullString, String))
                Me.txtAssured_Name.Text = RTrim(CType(objOLEDR_Chk("TBIL_INSRD_SURNAME") & vbNullString, String)) & " " & _
                   RTrim(CType(objOLEDR_Chk("TBIL_INSRD_FIRSTNAME") & vbNullString, String))

                If IsDate(objOLEDR_Chk("TBIL_ANN_POL_PRM_FROM")) Then
                    '' Me.txtPol_Eff_Date.Text = Format(CType(objOLEDR_Chk("TBIL_ANN_POL_PRM_FROM"), DateTime), "MM/dd/yyyy")
                    Me.txtPol_Eff_Date.Text = Format(objOLEDR_Chk("TBIL_ANN_POL_PRM_FROM"), "dd/MM/yyyy")
                Else
                    Me.txtPol_Eff_Date.Text = Format(Now, "dd/MM/yyyy")
                    'Pol_Eff_Date = Format(Now, "MM/dd/yyyy")
                End If

                Me.txtYear.Text = RTrim(CType(objOLEDR_Chk("TBIL_ANN_POLY_UNDW_YR") & vbNullString, String))
                Me.txtBraNum.Text = RTrim(CType(objOLEDR_Chk("TBIL_ANN_POLY_BRANCH_CD") & vbNullString, String))
                Me.txtProductClass.Text = RTrim(CType(objOLEDR_Chk("TBIL_PRDCT_DTL_CAT") & vbNullString, String))
                Me.txtProduct_Num.Text = RTrim(CType(objOLEDR_Chk("TBIL_ANN_POLY_PRDCT_CD") & vbNullString, String))
                Me.txtProduct_Name.Text = Trim(CType(objOLEDR_Chk("TBIL_PRDCT_DTL_DESC") & vbNullString, String))

                If IsDate(objOLEDR_Chk("TBIL_RCT_DATE")) Then
                    Me.txtTrans_Date.Text = Format(CType(objOLEDR_Chk("TBIL_RCT_DATE"), DateTime), "dd/MM/yyyy")
                Else
                    Me.txtTrans_Date.Text = ""
                End If
                Me.txtTrans_Num.Text = Trim(CType(objOLEDR_Chk("TBIL_RCT_NUM") & vbNullString, String))
                Me.txtTrans_Amt.Text = RTrim(CType(objOLEDR_Chk("TBIL_RCT_AMT") & vbNullString, String))

                ' check for existence of premium information, premium calculation details
                ' also check for policy status
                If RTrim(CType(objOLEDR_Chk("TBIL_ANN_POL_PRM_FILE_NO") & vbNullString, String)) = "" Or _
                   RTrim(CType(objOLEDR_Chk("TBIL_ANN_POL_PRM_PROP_NO") & vbNullString, String)) = "" Then
                    myTmp_Chk = "N"
                    blnStatusX = False
                    Me.lblMsg.Text = "Sorry! Premium information must be captured before this conversion."
                    'ElseIf RTrim(CType(objOLEDR_Chk("TBIL_POL_PRM_DTL_FILE_NO") & vbNullString, String)) = "" Or _
                    '       RTrim(CType(objOLEDR_Chk("TBIL_POL_PRM_DTL_PROP_NO") & vbNullString, String)) = "" Then
                    '    myTmp_Chk = "N"
                    '    blnStatusX = False
                    '    Me.lblMsg.Text = "Sorry! Premium calculation is yet to be done and saved before this conversion."
                Else
                    If RTrim(CType(objOLEDR_Chk("TBIL_ANN_POLY_PROPSL_ACCPT_STATUS") & vbNullString, String)) = "P" Then
                        myTmp_Chk = "Y"
                        blnStatusX = True
                        Me.lblMsg.Text = "Proposal No: " & Me.txtPro_Pol_Num.Text
                    Else
                        myTmp_Chk = "N"
                        blnStatusX = False
                        Me.lblMsg.Text = "Warning! The record you requested for has already been converted."
                    End If
                End If
                '

            Else
                myTmp_Chk = "N"
                blnStatusX = False
                Me.lblMsg.Text = "Record not found for Proposal No: " & Me.txtPro_Pol_Num.Text
            End If

        Catch ex As Exception
            myTmp_Chk = "N"
            blnStatusX = False

            Me.lblMsg.Text = "Error has occured. Reason: " & ex.Message.ToString()
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)

        End Try

        objOLEDR_Chk = Nothing

        objOLECmd_Chk.Dispose()
        objOLECmd_Chk = Nothing

        If objOLEConn_Chk.State = ConnectionState.Open Then
            objOLEConn_Chk.Close()
        End If
        objOLEConn_Chk = Nothing


        If myTmp_Chk = "N" Then
            'Me.lblMsg.Text = "Record not found for Proposal No: " & Me.txtPro_Pol_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
        End If

        Return blnStatusX

    End Function
    Private Sub Proc_DoNew()

        Me.cmdNew_ASP.Enabled = True
        Me.cmdFileNum.Enabled = True

        Me.txtPro_Pol_Num.Text = ""
        Me.txtPro_Pol_Num.Enabled = True
        Me.txtFileNum.Text = ""
        Me.txtFileNum.Enabled = True

        Me.txtPol_Num.Text = ""
        Me.txtAssured_Name.Text = ""

        Me.txtProductClass.Text = ""
        Me.txtProduct_Num.Text = ""
        Me.txtProduct_Name.Text = ""

        Me.txtBraNum.Text = ""
        Me.txtPol_Eff_Date.Text = ""

        Me.txtTrans_Date.Text = ""
        Me.txtTrans_Num.Text = ""
        Me.txtTrans_Amt.Text = ""

        Me.chkAccept.Enabled = False
        Me.chkAccept.Checked = False

        Me.lblPWD.Enabled = False
        Me.txtPWD.Enabled = False
        Me.txtPWD.Text = ""

        Me.BUT_OK.Enabled = False

    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            Call gnProc_Populate_Box("AN_ASSURED_HELP_SP", "001", Me.cboSearch, RTrim(Me.txtSearch.Value))
        End If
    End Sub
    Private Function GetPremiumPayment(ByVal proposalNo As String) As Double
        Dim PremiumPayment As Double = 0.0
        Dim OverridingCommTotal As Double = 0.0
        Dim PaymentMode As String
        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection()
        objOLEConn.ConnectionString = mystrCONN

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            lblMsg.Visible = True
            objOLEConn = Nothing
            Exit Function
        End Try

        strSQL = "SELECT  * FROM TBIL_ANN_POLICY_PREM_INFO"
        strSQL = strSQL & " WHERE TBIL_ANN_POL_PRM_PROP_NO = '" & RTrim(proposalNo) & "'"

        Dim objDA As System.Data.OleDb.OleDbDataAdapter
        objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)
        Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
        m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

        Dim obj_DT As New System.Data.DataTable
        Dim intC As Integer = 0

        Try

            objDA.Fill(obj_DT)
            Dim dr As System.Data.DataRow
            If obj_DT.Rows.Count > 0 Then
                For i = 0 To obj_DT.Rows.Count - 1
                    dr = obj_DT.Rows(i)
                    If Not IsDBNull(dr("TBIL_ANN_POL_PRM_MODE_PAYT")) Then
                        PaymentMode = dr("TBIL_ANN_POL_PRM_MODE_PAYT")

                        If PaymentMode = "Y" Then
                            'PremiumPayment = dr("TBIL_ANN_POL_PRM_ANN_CONTRIB_LC")
                        ElseIf PaymentMode = "M" Then
                            'PremiumPayment = dr("TBIL_ANN_POL_PRM_MTH_CONTRIB_LC")
                        ElseIf PaymentMode = "Q" Then
                            'PremiumPayment = dr("TBIL_ANN_POL_PRM_MTH_CONTRIB_LC") * 3
                            'ElseIf PaymentMode = "W" Then
                            '    PremiumPayment = dr("TBIL_ANN_POL_PRM_MTH_CONTRIB_LC") / 4
                            'ElseIf PaymentMode = "H" Then
                            '    PremiumPayment = dr("TBIL_ANN_POL_PRM_MTH_CONTRIB_LC") * 3
                            'ElseIf PaymentMode = "S" Then
                            '    PremiumPayment = dr("TBIL_ANN_POL_PRM_MTH_CONTRIB_LC") * 3
                        Else
                            'PremiumPayment = dr("TBIL_ANN_POL_PRM_MTH_CONTRIB_LC")
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            lblMsg.Visible = True
            Exit Function
        End Try

        obj_DT.Dispose()
        obj_DT = Nothing

        m_cbCommandBuilder.Dispose()
        m_cbCommandBuilder = Nothing

        If objDA.SelectCommand.Connection.State = ConnectionState.Open Then
            objDA.SelectCommand.Connection.Close()
        End If
        objDA.Dispose()
        objDA = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing
        Return PremiumPayment
    End Function

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then
                'Me.txtPolicyNumber.Text = ""
                'Me.txtSearch.Value = ""
            Else
                Dim selectedText = Me.cboSearch.SelectedItem.Text
                Dim ReturnText = Split(selectedText, "-")
                txtPro_Pol_Num.Text = Trim(ReturnText(2))
                txtFileNum.Text = cboSearch.SelectedValue

                blnStatus = Proc_DoGet_Record(RTrim("PRO"), Trim(Me.txtPro_Pol_Num.Text), RTrim(Me.txtFileNum.Text))
                If blnStatus = True Then
                    Me.chkAccept.Enabled = True
                    'Me.BUT_OK.Enabled = True
                    Exit Sub
                Else
                    Me.chkAccept.Enabled = False
                    Me.chkAccept.Checked = False
                    Me.lblPWD.Enabled = False
                    Me.txtPWD.Enabled = False
                    Me.BUT_OK.Enabled = False
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
            lblMsg.Visible = True
        End Try
    End Sub

    Protected Sub cmdNew_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew_ASP.Click
        Proc_DoNew()
    End Sub

    Protected Sub cmdPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrev.Click
        Dim pvURL As String = ""
        pvURL = "PRG_ANNUITY_POLY_BENEFRY.aspx?optfileid=" & Trim(Me.txtFileNum.Text)
        pvURL = pvURL & "&optpolid=" & Trim(Me.txtPol_Num.Text)
        pvURL = pvURL & "&optquotid=" & Trim(txtPro_Pol_Num.Text)
        Response.Redirect(pvURL)
    End Sub
End Class
