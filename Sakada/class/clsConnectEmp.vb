Public Class clsConnectEmp
    Private strEmpID As String
    Private strFullName As String
    Private strFirstName As String
    Private strMiddleName As String
    Private strLastName As String
    Private strAge As String
    Private strBirthDate As String
    Private strAddress As String
    Private strMobileNo As String
    Private strEmailAdd As String
    Private strSupervisor As String


    Public Property EmpID As String
        Get
            Return strEmpID
        End Get
        Set(value As String)
            strEmpID = value
        End Set
    End Property

    Public Property FullName As String
        Get
            Return strFullName
        End Get
        Set(value As String)
            strFullName = value
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
    Public Property Age As String
        Get
            Return strAge
        End Get
        Set(value As String)
            strAge = value
        End Set
    End Property

    Public Property BirthDate As String
        Get
            Return strBirthDate
        End Get
        Set(value As String)
            strBirthDate = value
        End Set
    End Property

    Public Property Address As String
        Get
            Return strAddress
        End Get
        Set(value As String)
            strAddress = value
        End Set
    End Property

    Public Property MobileNo As String
        Get
            Return strMobileNo
        End Get
        Set(value As String)
            strMobileNo = value
        End Set
    End Property

    Public Property EmailAdd As String
        Get
            Return strEmailAdd
        End Get
        Set(value As String)
            strEmailAdd = value
        End Set
    End Property

    Public Property Supervisor As String
        Get
            Return strSupervisor
        End Get
        Set(value As String)
            strSupervisor = value
        End Set
    End Property

    Public Function GetEmpDB() As List(Of clsConnectEmp)
        Dim sQuery As New StringBuilder

        sQuery.Append("SELECT empID,empFirstName,empMiddleName,empLastName,empAddress,empMobileNo,empEmailAddress,empBirthday,empAge,CONCAT(B.supFirstName,' ',B.supMiddleName,' ',B.supLastName) AS empSupervisor,A.isDeleted FROM tblEmployee A ")
        sQuery.Append("INNER JOIN tblSupervisor B ON A.empSupervisor = B.ID WHERE A.isDeleted <> 1")

        Dim lData As New List(Of clsConnectEmp)

        Try
            Dim oReader = SakadaExecReader(sQuery.ToString())

            While oReader.Read()
                Dim obj As New clsConnectEmp
                obj.EmpID = HttpContext.Current.Server.HtmlEncode(oReader("empID").ToString()).Replace("&#160;", "")
                obj.FirstName = HttpContext.Current.Server.HtmlEncode(oReader("empFirstName").ToString()).Replace("&#160;", "")
                obj.MiddleName = HttpContext.Current.Server.HtmlEncode(oReader("empMiddleName").ToString()).Replace("&#160;", "")
                obj.LastName = HttpContext.Current.Server.HtmlEncode(oReader("empLastName").ToString()).Replace("&#160;", "")
                obj.FullName = obj.FirstName + " " + obj.MiddleName + " " + obj.LastName
                obj.Address = HttpContext.Current.Server.HtmlEncode(oReader("empAddress").ToString()).Replace("&#160;", "")
                obj.MobileNo = HttpContext.Current.Server.HtmlEncode(oReader("empMobileNo").ToString()).Replace("&#160;", "")
                obj.EmailAdd = HttpContext.Current.Server.HtmlEncode(oReader("empEmailAddress").ToString()).Replace("&#160;", "")
                obj.BirthDate = HttpContext.Current.Server.HtmlEncode(oReader("empBirthday").ToString()).Replace("&#160;", "")
                obj.Age = HttpContext.Current.Server.HtmlEncode(oReader("empAge").ToString()).Replace("&#160;", "")
                obj.Supervisor = HttpContext.Current.Server.HtmlEncode(oReader("empSupervisor").ToString()).Replace("&#160;", "")
                lData.Add(obj)
            End While
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -GetEmpDB")
        Finally
            SakadaCloseNewConnection()
        End Try
        Return lData
    End Function

    Public Function SaveEmpDetails(obj As clsConnectEmp) As Boolean
        Dim oConnection = SakadaCallConnection()
        Dim resultVal As Boolean = False
        Dim sQuery As New StringBuilder
        sQuery.Append("INSERT INTO tblEmployee (empFirstName, empMiddleName, empLastName, empAddress, empMobileNo, empEmailAddress, empBirthday, empAge, empSupervisor) VALUES (@FirstName,@MiddleName,@LastName,@Address,@MobileNo,@EmailAddress,@Birthday,@Age,@Supervisor)")
        Try
            Using (oConnection)
                Dim oCommand = SakadaCallCommand()
                oCommand.Connection = oConnection
                oCommand.CommandText = sQuery.ToString()
                oCommand.CommandType = CommandType.Text
                oCommand.Parameters.AddWithValue("@FirstName", obj.FirstName)
                oCommand.Parameters.AddWithValue("@MiddleName", obj.MiddleName)
                oCommand.Parameters.AddWithValue("@LastName", obj.LastName)
                oCommand.Parameters.AddWithValue("@Address", obj.Address)
                oCommand.Parameters.AddWithValue("@MobileNo", obj.MobileNo)
                oCommand.Parameters.AddWithValue("@EmailAddress", obj.EmailAdd)
                oCommand.Parameters.AddWithValue("@Birthday", obj.BirthDate)
                oCommand.Parameters.AddWithValue("@Age", obj.Age)
                oCommand.Parameters.AddWithValue("@Supervisor", obj.Supervisor)
                oConnection.Open()
                oCommand.ExecuteNonQuery()
                resultVal = True
            End Using
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -SaveEmpDetails")
        End Try
        Return resultVal
    End Function

    Public Function UpdateEmpRecord(EmpID As String, obj As clsConnectEmp) As Boolean
        Dim sQuery As New StringBuilder
        sQuery.Append("UPDATE tblEmployee SET empFirstName = @FirstName,empMiddleName = @MiddleName,empLastName = @LastName,empAddress = @Address,empMobileNo = @MobileNo, empEmailAddress = @EmailAddress, empBirthday = @Birthday, empAge = @Age, empSupervisor = @Supervisor WHERE empID = '" + EmpID + "'")
        Dim boolReturnVal As Boolean = False
        Dim oConnection = SakadaCallConnection()
        Try
            oConnection.Open()
            Dim oCommand = SakadaCallCommand()
            oCommand = New SqlClient.SqlCommand(sQuery.ToString(), oConnection)
            oCommand.Parameters.AddWithValue("@FirstName", obj.FirstName)
            oCommand.Parameters.AddWithValue("@MiddleName", obj.MiddleName)
            oCommand.Parameters.AddWithValue("@LastName", obj.LastName)
            oCommand.Parameters.AddWithValue("@Address", obj.Address)
            oCommand.Parameters.AddWithValue("@MobileNo", obj.MobileNo)
            oCommand.Parameters.AddWithValue("@EmailAddress", obj.EmailAdd)
            oCommand.Parameters.AddWithValue("@Birthday", obj.BirthDate)
            oCommand.Parameters.AddWithValue("@Age", obj.Age)
            oCommand.Parameters.AddWithValue("@Supervisor", obj.Supervisor)
            oCommand.ExecuteNonQuery()
            boolReturnVal = True
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -UpdateEmpRecord")
        Finally
            oConnection.Close()
        End Try
        Return boolReturnVal
    End Function

    Public Function DeleteEmpRecord(EmpID As String) As Boolean
        Dim sQuery As New StringBuilder
        sQuery.Append("UPDATE tblEmployee SET isDeleted = 1 WHERE empID = '" + EmpID + "'")
        Dim boolReturnVal As Boolean = False
        Dim oConnection = SakadaCallConnection()
        Try
            oConnection.Open()
            Dim oCommand = SakadaCallCommand()
            oCommand = New SqlClient.SqlCommand(sQuery.ToString(), oConnection)
            oCommand.ExecuteNonQuery()
            boolReturnVal = True
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -DeleteEmpRecord")
        Finally
            oConnection.Close()
        End Try
        Return boolReturnVal
    End Function

    Public Function GetEmployeeBySupName(SupName As String) As List(Of clsConnectEmp)
        Dim sQuery As New StringBuilder
        sQuery.Append("SELECT ID,empFirstName,empMiddleName,empLastName FROM tblEmployee WHERE empSupervisor = '" + SupName + "' AND isDeleted = 0")
        Dim lData As New List(Of clsConnectEmp)

        Try
            Dim oReader = SakadaExecReader(sQuery.ToString())
            While oReader.Read()
                Dim obj As New clsConnectEmp
                obj.EmpID = oReader("ID").ToString()
                obj.FirstName = oReader("empFirstName").ToString()
                obj.MiddleName = oReader("empMiddleName").ToString()
                obj.LastName = oReader("empLastName").ToString()
                obj.FullName = obj.FirstName + " " + obj.MiddleName + " " + obj.LastName
                lData.Add(obj)
            End While
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -GetEmployee")
        Finally
            SakadaCloseNewConnection()
        End Try
        Return lData
    End Function
End Class
