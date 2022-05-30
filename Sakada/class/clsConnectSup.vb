Public Class clsConnectSup
    Private strSupID As String
    Private strSupName As String
    Private strFirstName As String
    Private strMiddleName As String
    Private strLastName As String
    Private strSupAddress As String
    Private strSupMobileNo As String
    Private strSupEmailAdd As String
    Private strSupBirthday As String
    Private strSupAge As String

    Public Property SupID As String
        Get
            Return strSupID
        End Get
        Set(value As String)
            strSupID = value
        End Set
    End Property

    Public Property FirstName As String
        Get
            Return strFirstName
        End Get
        Set(value As String)
            strFirstName = value
        End Set
    End Property
    Public Property MiddleName As String
        Get
            Return strMiddleName
        End Get
        Set(value As String)
            strMiddleName = value
        End Set
    End Property
    Public Property LastName As String
        Get
            Return strLastName
        End Get
        Set(value As String)
            strLastName = value
        End Set
    End Property
    Public Property SupName As String
        Get
            Return strSupName
        End Get
        Set(value As String)
            strSupName = value
        End Set
    End Property

    Public Property SupAddress As String
        Get
            Return strSupAddress
        End Get
        Set(value As String)
            strSupAddress = value
        End Set
    End Property
    Public Property SupMobileNo As String
        Get
            Return strSupMobileNo
        End Get
        Set(value As String)
            strSupMobileNo = value
        End Set
    End Property

    Public Property SupEmailAdd As String
        Get
            Return strSupEmailAdd
        End Get
        Set(value As String)
            strSupEmailAdd = value
        End Set
    End Property

    Public Property SupBirthday As String
        Get
            Return strSupBirthday
        End Get
        Set(value As String)
            strSupBirthday = value
        End Set
    End Property

    Public Property SupAge As String
        Get
            Return strSupAge
        End Get
        Set(value As String)
            strSupAge = value
        End Set
    End Property

    Public Function GetSupDB(Search As String) As List(Of clsConnectSup)
        Dim sQuery As New StringBuilder

        sQuery.Append("SELECT * FROM tblSupervisor WHERE isDeleted <> 1 AND supID LIKE '%" + Search + "%'")

        Dim lData As New List(Of clsConnectSup)

        Try
            Dim oReader = SakadaExecReader(sQuery.ToString())

            While oReader.Read()
                Dim obj As New clsConnectSup
                obj.SupID = HttpContext.Current.Server.HtmlEncode(oReader("supID").ToString()).Replace("&#160;", "")
                obj.FirstName = HttpContext.Current.Server.HtmlEncode(oReader("supFirstName").ToString()).Replace("&#160;", "").Replace("&#241;", "ñ").Replace("&#209;", "Ñ")
                obj.MiddleName = HttpContext.Current.Server.HtmlEncode(oReader("supMiddleName").ToString()).Replace("&#160;", "").Replace("&#241;", "ñ").Replace("&#209;", "Ñ")
                obj.LastName = HttpContext.Current.Server.HtmlEncode(oReader("supLastName").ToString()).Replace("&#160;", "").Replace("&#241;", "ñ").Replace("&#209;", "Ñ")
                obj.SupName = obj.FirstName + " " + obj.MiddleName + " " + obj.LastName
                obj.SupAddress = HttpContext.Current.Server.HtmlEncode(oReader("supAddress").ToString()).Replace("&#160;", "")
                obj.SupMobileNo = HttpContext.Current.Server.HtmlEncode(oReader("supMobileNo").ToString()).Replace("&#160;", "")
                obj.SupEmailAdd = HttpContext.Current.Server.HtmlEncode(oReader("supEmailAddress").ToString()).Replace("&#160;", "")
                obj.SupBirthday = HttpContext.Current.Server.HtmlEncode(oReader("supBirthday").ToString()).Replace("&#160;", "")
                obj.SupAge = HttpContext.Current.Server.HtmlEncode(oReader("supAge").ToString()).Replace("&#160;", "")
                lData.Add(obj)
            End While
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -GetSupDB")
        Finally
            SakadaCloseNewConnection()
        End Try
        Return lData
    End Function

    Public Function SaveSupDetails(obj As clsConnectSup) As Boolean
        Dim oConnection = SakadaCallConnection()
        Dim resultVal As Boolean = False
        Dim sQuery As New StringBuilder
        sQuery.Append("INSERT INTO tblSupervisor (supFirstName,supMiddleName,supLastName,supAddress,supMobileNo,supEmailAddress,supBirthday,supAge) VALUES (@FirstName,@MiddleName,@LastName,@Address,@MobileNo,@EmailAddress,@Birthday,@Age)")
        Try
            Using (oConnection)
                Dim oCommand = SakadaCallCommand()
                oCommand.Connection = oConnection
                oCommand.CommandText = sQuery.ToString()
                oCommand.CommandType = CommandType.Text

                oCommand.Parameters.AddWithValue("@FirstName", obj.FirstName)
                oCommand.Parameters.AddWithValue("@MiddleName", obj.MiddleName)
                oCommand.Parameters.AddWithValue("@LastName", obj.LastName)
                oCommand.Parameters.AddWithValue("@Address", obj.SupAddress)
                oCommand.Parameters.AddWithValue("@MobileNo", obj.SupMobileNo)
                oCommand.Parameters.AddWithValue("@EmailAddress", obj.SupEmailAdd)
                oCommand.Parameters.AddWithValue("@Birthday", obj.SupBirthday)
                oCommand.Parameters.AddWithValue("@Age", obj.SupAge)
                oConnection.Open()
                oCommand.ExecuteNonQuery()
                resultVal = True
            End Using
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -SaveSupDetails")
        End Try
        Return resultVal
    End Function

    Public Function UpdateSup(SupID As String, obj As clsConnectSup) As Boolean
        Dim sQuery As New StringBuilder
        sQuery.Append("UPDATE tblSupervisor SET supFirstName = @FirstName, supMiddleName = @MiddleName, supLastName = @LastName, ")
        sQuery.Append("supAddress = @Address, supMobileNo = @MobileNo, supEmailAddress = @EmailAddress, supBirthday = @Birthday, supAge = @Age ")
        sQuery.Append("WHERE supID = '" + SupID + "'")
        Dim boolReturnVal As Boolean = False
        Dim oConnection = SakadaCallConnection()
        Try
            oConnection.Open()
            Dim oCommand = SakadaCallCommand()
            oCommand = New SqlClient.SqlCommand(sQuery.ToString(), oConnection)
            oCommand.Parameters.AddWithValue("@FirstName", obj.FirstName)
            oCommand.Parameters.AddWithValue("@MiddleName", obj.MiddleName)
            oCommand.Parameters.AddWithValue("@LastName", obj.LastName)
            oCommand.Parameters.AddWithValue("@Address", obj.SupAddress)
            oCommand.Parameters.AddWithValue("@MobileNo", obj.SupMobileNo)
            oCommand.Parameters.AddWithValue("@EmailAddress", obj.SupEmailAdd)
            oCommand.Parameters.AddWithValue("@Birthday", obj.SupBirthday)
            oCommand.Parameters.AddWithValue("@Age", obj.SupAge)
            oCommand.ExecuteNonQuery()
            boolReturnVal = True
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -UpdateEmpRecord")
        Finally
            oConnection.Close()
        End Try
        Return boolReturnVal
    End Function

    Public Function GetSupervisor() As List(Of clsConnectSup)
        Dim sQuery As New StringBuilder
        sQuery.Append("SELECT ID,supFirstName,supMiddleName,supLastName FROM tblSupervisor WHERE isDeleted <> 1 ")
        Dim lData As New List(Of clsConnectSup)

        Try
            Dim oReader = SakadaExecReader(sQuery.ToString())
            While oReader.Read()
                Dim obj As New clsConnectSup
                obj.SupID = oReader("ID").ToString()
                obj.FirstName = oReader("supFirstName").ToString()
                obj.MiddleName = oReader("supMiddleName").ToString()
                obj.LastName = oReader("supLastName").ToString()
                obj.SupName = obj.FirstName + " " + obj.MiddleName + " " + obj.LastName
                lData.Add(obj)
            End While
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -GetSupervisor")
        Finally
            SakadaCloseNewConnection()
        End Try
        Return lData
    End Function

    Public Function DeleteSupRecord(SupID As String) As Boolean
        Dim sQuery As New StringBuilder
        sQuery.Append("UPDATE tblSupervisor SET isDeleted = 1 WHERE supID = '" + SupID + "'")
        Dim boolReturnVal As Boolean = False
        Dim oConnection = SakadaCallConnection()
        Try
            oConnection.Open()
            Dim oCommand = SakadaCallCommand()
            oCommand = New SqlClient.SqlCommand(sQuery.ToString(), oConnection)
            oCommand.ExecuteNonQuery()
            boolReturnVal = True
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -DeleteSupRecord")
        Finally
            oConnection.Close()
        End Try
        Return boolReturnVal
    End Function
End Class
