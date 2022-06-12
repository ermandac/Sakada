Public Class clsLogin
    Private strUserName As String
    Private strLoginName As String
    Private strPassWord As String
    Private strAccessLevel As String
    Private strLoginID As String
    Private strReference As String

    Public Property LoginID As String
        Get
            Return strLoginID
        End Get
        Set(value As String)
            strLoginID = value
        End Set
    End Property

    Public Property UserName As String
        Get
            Return strUserName
        End Get
        Set(value As String)
            strUserName = value
        End Set
    End Property

    Public Property LoginName As String
        Get
            Return strLoginName
        End Get
        Set(value As String)
            strLoginName = value
        End Set
    End Property

    Public Property PassWord As String
        Get
            Return strPassWord
        End Get
        Set(value As String)
            strPassWord = value
        End Set
    End Property

    Public Property AccessLevel As String
        Get
            Return strAccessLevel
        End Get
        Set(value As String)
            strAccessLevel = value
        End Set
    End Property

    Public Property Reference As String
        Get
            Return strReference
        End Get
        Set(value As String)
            strReference = value
        End Set
    End Property

    Public Function GetLogin(Username As String, Userpass As String) As List(Of clsLogin)
        Dim sQuery As New StringBuilder
        sQuery.Append("SELECT username,user_login,user_pass,access_level FROM tblLogin WHERE user_login = '" + Username + "' AND user_pass = '" + Userpass + "' AND isDeleted <> 1")
        Dim lData As New List(Of clsLogin)

        Try
            Dim oReader = SakadaExecReader(sQuery.ToString())
            While oReader.Read()
                Dim obj As New clsLogin
                obj.UserName = oReader("username").ToString()
                obj.LoginName = oReader("user_login").ToString()
                obj.PassWord = oReader("user_pass").ToString()
                obj.AccessLevel = oReader("access_level").ToString()
                lData.Add(obj)
            End While
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -GetLogin")
        Finally
            SakadaCloseNewConnection()
        End Try
        Return lData
    End Function

    Public Function GetUser(UserLogin As String)
        Dim sQuery As New StringBuilder
        sQuery.Append("SELECT username FROM tblLogin WHERE user_login = '" + UserLogin + "'")
        Dim valName As String = ""
        Try
            Dim oReader = SakadaExecReader(sQuery.ToString())
            While oReader.Read()
                valName = oReader("username").ToString()
            End While
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -GetUser")
        Finally
            SakadaCloseNewConnection()
        End Try
        Return valName
    End Function

    Public Function GetAccessLevel(UserLogin As String)
        Dim sQuery As New StringBuilder
        sQuery.Append("SELECT access_level FROM tblLogin WHERE user_login = '" + UserLogin + "'")
        Dim valName As String = ""
        Try
            Dim oReader = SakadaExecReader(sQuery.ToString())
            While oReader.Read()
                valName = oReader("access_level").ToString()
            End While
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -GetUser")
        Finally
            SakadaCloseNewConnection()
        End Try
        Return valName
    End Function

    Public Function GetLoginDB() As List(Of clsLogin)
        Dim sQuery As New StringBuilder

        sQuery.Append("SELECT * FROM tblLogin WHERE access_level <> 'Admin' AND isDeleted <> 1")

        Dim lData As New List(Of clsLogin)

        Try
            Dim oReader = SakadaExecReader(sQuery.ToString())

            While oReader.Read()
                Dim obj As New clsLogin
                obj.LoginID = HttpContext.Current.Server.HtmlEncode(oReader("id").ToString()).Replace("&#160;", "")
                obj.UserName = HttpContext.Current.Server.HtmlEncode(oReader("username").ToString()).Replace("&#160;", "")
                obj.LoginName = HttpContext.Current.Server.HtmlEncode(oReader("user_login").ToString()).Replace("&#160;", "")
                obj.PassWord = HttpContext.Current.Server.HtmlEncode(oReader("user_pass").ToString()).Replace("&#160;", "")
                obj.AccessLevel = HttpContext.Current.Server.HtmlEncode(oReader("access_level").ToString()).Replace("&#160;", "")
                obj.Reference = HttpContext.Current.Server.HtmlEncode(oReader("reference").ToString()).Replace("&#160;", "")
                lData.Add(obj)
            End While
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -GetLoginDB")
        Finally
            SakadaCloseNewConnection()
        End Try
        Return lData
    End Function

    Public Function UpdateLogin(ID As String, obj As clsLogin) As Boolean
        Dim sQuery As New StringBuilder
        sQuery.Append("UPDATE tblLogin SET username = @UserName, user_login = @LoginName, user_pass = @PassWord, access_level = @AccessLevel, reference = @Reference WHERE id = '" + ID + "'")
        Dim boolReturnVal As Boolean = False
        Dim oConnection = SakadaCallConnection()
        Try
            oConnection.Open()
            Dim oCommand = SakadaCallCommand()
            oCommand = New SqlClient.SqlCommand(sQuery.ToString(), oConnection)
            oCommand.Parameters.AddWithValue("@UserName", obj.UserName)
            oCommand.Parameters.AddWithValue("@LoginName", obj.LoginName)
            oCommand.Parameters.AddWithValue("@PassWord", obj.PassWord)
            oCommand.Parameters.AddWithValue("@AccessLevel", obj.AccessLevel)
            oCommand.Parameters.AddWithValue("@Reference", obj.Reference)
            oCommand.ExecuteNonQuery()
            boolReturnVal = True
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -UpdateLogin")
        Finally
            oConnection.Close()
        End Try
        Return boolReturnVal
    End Function

    Public Function SaveLoginDetails(obj As clsLogin) As Boolean
        Dim oConnection = SakadaCallConnection()
        Dim resultVal As Boolean = False
        Dim sQuery As New StringBuilder
        sQuery.Append("INSERT INTO tblLogin VALUES (@UserName,@LoginName,@PassWord,@AccessLevel,@Reference)")
        Try
            Using (oConnection)
                Dim oCommand = SakadaCallCommand()
                oCommand.Connection = oConnection
                oCommand.CommandText = sQuery.ToString()
                oCommand.CommandType = CommandType.Text

                oCommand.Parameters.AddWithValue("@UserName", obj.UserName)
                oCommand.Parameters.AddWithValue("@LoginName", obj.LoginName)
                oCommand.Parameters.AddWithValue("@PassWord", obj.PassWord)
                oCommand.Parameters.AddWithValue("@AccessLevel", obj.AccessLevel)
                oCommand.Parameters.AddWithValue("@Reference", obj.Reference)
                oConnection.Open()
                oCommand.ExecuteNonQuery()
                resultVal = True
            End Using
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -SaveSupDetails")
        End Try
        Return resultVal
    End Function

    Public Function DeleteLoginRecord(SupID As String) As Boolean
        Dim sQuery As New StringBuilder
        sQuery.Append("UPDATE tblLogin SET isDeleted = 1 WHERE supID = '" + SupID + "'")
        Dim boolReturnVal As Boolean = False
        Dim oConnection = SakadaCallConnection()
        Try
            oConnection.Open()
            Dim oCommand = SakadaCallCommand()
            oCommand = New SqlClient.SqlCommand(sQuery.ToString(), oConnection)
            oCommand.ExecuteNonQuery()
            boolReturnVal = True
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -DeleteLoginRecord")
        Finally
            oConnection.Close()
        End Try
        Return boolReturnVal
    End Function
End Class
