Public Class clsLogin
    Private strUserName As String
    Private strLoginName As String
    Private strPassWord As String
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

    Public Function GetLogin(Username As String, Userpass As String) As List(Of clsLogin)
        Dim sQuery As New StringBuilder
        sQuery.Append("SELECT * FROM tblLogin WHERE user_login = '" + Username + "' AND user_pass = '" + Userpass + "'")
        Dim lData As New List(Of clsLogin)

        Try
            Dim oReader = SakadaExecReader(sQuery.ToString())
            While oReader.Read()
                Dim obj As New clsLogin
                obj.UserName = oReader("username").ToString()
                obj.LoginName = oReader("user_login").ToString()
                obj.PassWord = oReader("user_pass").ToString()
                lData.Add(obj)
            End While
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -GetLogin")
        Finally
            SakadaCloseNewConnection()
        End Try
        Return lData
    End Function

    Public Function GetUser(Session As String)
        Dim sQuery As New StringBuilder
        sQuery.Append("SELECT username FROM tblLogin WHERE user_login = '" + Session + "'")
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
End Class
