Imports System.Web.Security
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Partial Class LoginP
    Inherits System.Web.UI.Page
    Protected strCopyRight As String
    Protected dteMydate As String = CType(Format(Now, "dd-MMM-yyyy"), String)
    Dim strSQL As String
    Protected Structure TabItem
        Dim TabText As String
        Dim TabKey As String
    End Structure

    Protected MenuItems As New ArrayList()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cstype As Type = Me.GetType()
        strCopyRight = "Copyright &copy;" & Year(Now)

        If Not (Page.IsPostBack) Then
            Me.txtUserID.Enabled = True
            Me.txtUserID.Focus()
        End If
    End Sub

    Protected Sub LoginBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoginBtn.Click
        lblMessage.Text = ""
        Dim mystrCONN_Chk As String = ""
        Dim LoginDate As Date
        Dim PassWordExpiryDate As Date
        Dim PasswordExpireDaysLeft As Integer
        Dim status As String = ""
        LoginDate = Convert.ToDateTime(DoConvertToDbDateFormat(Format(DateTime.Now, "dd/MM/yyyy")))

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
            lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn_Chk = Nothing
            Exit Sub
        End Try

        Try
            Dim User_Login = Trim(txtUserID.Text)
            Dim User_Password = Trim(EncryptNew(txtUser_PWD.Text))
            strSQL = "SELECT * FROM SEC_USER_LIFE_DETAIL WHERE SEC_USER_LOGIN='" & User_Login & "' " & _
                   "and SEC_USER_PASSWORD='" & User_Password & "' "
            objOLECmd_Chk = New OleDbCommand(strSQL, objOLEConn_Chk)
            objOLECmd_Chk.CommandType = CommandType.Text
            objOLEDR_Chk = objOLECmd_Chk.ExecuteReader()
            If (objOLEDR_Chk.Read()) Then
                Session("MyUserIDX") = Trim(Me.txtUserID.Text)
                'Session("MyUserName") = UCase(Me.txtUserName.Text)
                Session("MyUserName") = objOLEDR_Chk("SEC_USER_NAME")
                Session("MyUserRole") = objOLEDR_Chk("SEC_USER_ROLE")
                status = objOLEDR_Chk("SEC_USER_FLAG")
                PasswordExpireDaysLeft = DateDiff(DateInterval.Day, LoginDate, CType(objOLEDR_Chk("passwordexpirydate"), DateTime))
                PassWordExpiryDate = Convert.ToDateTime(DoConvertToDbDateFormat(Format(objOLEDR_Chk("passwordexpirydate"), "dd/MM/yyyy")))
                If Request.QueryString("Goto") <> "" Then
                    Response.Redirect(Request.QueryString("Goto"))
                ElseIf status = "X" Then
                    MsgBox("You have been deactivated, please contact administrator.", 0, "User activation status")
                    'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                ElseIf Trim(objOLEDR_Chk("SEC_USER_PASSWORD") & vbNullString) = Trim(objOLEDR_Chk("firstpassword") & vbNullString) Then
                    Response.Redirect("SEC/PRG_SEC_USER_CHG_PASS.aspx")
                ElseIf PassWordExpiryDate = LoginDate Then
                    'Update user details table
                    UpdatePasswordAtExpiry(txtUserID.Text, Trim(objOLEDR_Chk("SEC_USER_PASSWORD") & vbNullString))
                    MsgBox("Password expired, please change password.", 0, "Password Expiry Notification")
                    Response.Redirect("SEC/PRG_SEC_USER_CHG_PASS.aspx")
                ElseIf PasswordExpireDaysLeft < 3 Then
                    MsgBox("Password will expire in less than " & PasswordExpireDaysLeft & " day(s), please kindly change your password.", 0, "Password Expiry Notification")
                    Response.Redirect("MENU_AN.aspx?menu=HOME")
                Else
                    Response.Redirect("MENU_AN.aspx?menu=HOME")
                End If
            Else
                Me.lblMessage.Text = "Login information is not correct. Enter Valid User ID and Password..."
                Me.txtUserID.Enabled = True
                Me.txtUserID.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            Me.lblMessage.Text = "Error has occured. Reason: " & ex.Message.ToString()
        End Try
        objOLEDR_Chk = Nothing
        objOLECmd_Chk.Dispose()
        objOLECmd_Chk = Nothing
        If objOLEConn_Chk.State = ConnectionState.Open Then
            objOLEConn_Chk.Close()
        End If
        objOLEConn_Chk = Nothing
    End Sub

    Protected Sub txtUserID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserID.TextChanged
        lblMessage.Text = ""
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
            lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn_Chk = Nothing
            Exit Sub
        End Try

        Try
            Dim User_Login = Trim(txtUserID.Text)
            strSQL = "SELECT * FROM SEC_USER_LIFE_DETAIL WHERE SEC_USER_LOGIN='" & User_Login & "'"
            objOLECmd_Chk = New OleDbCommand(strSQL, objOLEConn_Chk)
            objOLECmd_Chk.CommandType = CommandType.Text
            objOLEDR_Chk = objOLECmd_Chk.ExecuteReader()
            If (objOLEDR_Chk.Read()) Then
                Session("MyUserIDX") = Trim(Me.txtUserID.Text)
                txtUserName.Text = objOLEDR_Chk("SEC_USER_NAME")
            Else
                Me.lblMessage.Text = "User ID does not exist"
                txtUserName.Text = ""
                Me.txtUserID.Enabled = True
                Me.txtUserID.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            Me.lblMessage.Text = "Error has occured. Reason: " & ex.Message.ToString()
        End Try
        objOLEDR_Chk = Nothing
        objOLECmd_Chk.Dispose()
        objOLECmd_Chk = Nothing
        If objOLEConn_Chk.State = ConnectionState.Open Then
            objOLEConn_Chk.Close()
        End If
        objOLEConn_Chk = Nothing
    End Sub

    Public Sub UpdatePasswordAtExpiry(ByVal LoginId As String, ByVal NormalPassword As String)
        Dim intC As Long = 0
        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection()
        objOLEConn.ConnectionString = mystrCONN

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn = Nothing
            Exit Sub
        End Try

        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM SEC_USER_LIFE_DETAIL"
        strSQL = strSQL & " WHERE SEC_USER_LOGIN = '" & RTrim(LoginId) & "'"

        Dim objDA As System.Data.OleDb.OleDbDataAdapter
        objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)


        Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
        m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

        Dim obj_DT As New System.Data.DataTable

        Try

            objDA.Fill(obj_DT)

            If obj_DT.Rows.Count > 0 Then
                '   Update existing record
                With obj_DT
                    .Rows(0)("firstpassword") = NormalPassword
                End With
                intC = objDA.Update(obj_DT)
            End If

        Catch ex As Exception
            Me.lblMessage.Text = ex.Message.ToString
            Exit Sub
        End Try

        m_cbCommandBuilder.Dispose()
        m_cbCommandBuilder = Nothing

        obj_DT.Dispose()
        obj_DT = Nothing

        objDA.Dispose()
        objDA = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing
    End Sub
End Class
