Public Class clsConnectCA
    Private strCAID As String
    Private strSupervisor As String
    Private strEmployee As String
    Private strDate As String
    Private strStatus As String
    Private strAmount As Decimal
    Private strEmpID As String
    Public Property CAID As String
        Get
            Return strCAID
        End Get
        Set(value As String)
            strCAID = value
        End Set
    End Property

    Public Property CASupervisor As String
        Get
            Return strSupervisor
        End Get
        Set(value As String)
            strSupervisor = value
        End Set
    End Property

    Public Property CAEmployee As String
        Get
            Return strEmployee
        End Get
        Set(value As String)
            strEmployee = value
        End Set
    End Property

    Public Property CADate As String
        Get
            Return strDate
        End Get
        Set(value As String)
            strDate = value
        End Set
    End Property

    Public Property CAStatus As String
        Get
            Return strStatus
        End Get
        Set(value As String)
            strStatus = value
        End Set
    End Property

    Public Property CAAmount As Decimal
        Get
            Return strAmount
        End Get
        Set(value As Decimal)
            strAmount = value
        End Set
    End Property

    Public Property CAEmpID As String
        Get
            Return strEmpID
        End Get
        Set(value As String)
            strEmpID = value
        End Set
    End Property

    Public Function GetCADB(Search As String) As List(Of clsConnectCA)
        Dim sQuery As New StringBuilder

        sQuery.Append("SELECT caID, CONCAT(B.supFirstName,' ',B.supMiddleName,' ',B.supLastName) AS caSupervisor, ")
        sQuery.Append("CONCAT(C.empFirstName,' ',C.empMiddleName,' ',C.empLastName) AS caEmployee, caDate, caStatus, caAmount, A.empID ")
        sQuery.Append("FROM tblCashAdvance A INNER JOIN tblSupervisor B ON A.caSupervisor = B.ID ")
        sQuery.Append("INNER JOIN tblEmployee C ON A.caEmployee = C.ID WHERE A.isDeleted <> 1 AND caID LIKE '%" + Search + "%'")
        Dim lData As New List(Of clsConnectCA)
        Try
            Dim oReader = SakadaExecReader(sQuery.ToString())

            While oReader.Read()
                Dim obj As New clsConnectCA
                obj.CAID = HttpContext.Current.Server.HtmlEncode(oReader("caID").ToString()).Replace("&#160;", "")
                obj.CASupervisor = HttpContext.Current.Server.HtmlEncode(oReader("caSupervisor").ToString()).Replace("&#160;", "").Replace("&#241;", "ñ").Replace("&#209;", "Ñ")
                obj.CAEmployee = HttpContext.Current.Server.HtmlEncode(oReader("caEmployee").ToString()).Replace("&#160;", "").Replace("&#241;", "ñ").Replace("&#209;", "Ñ")
                obj.CAStatus = HttpContext.Current.Server.HtmlEncode(oReader("caStatus").ToString()).Replace("&#160;", "")
                obj.CADate = HttpContext.Current.Server.HtmlEncode(oReader("caDate").ToString()).Replace("&#160;", "")
                Dim Amount = HttpContext.Current.Server.HtmlEncode(oReader("caAmount").ToString()).Replace("&#160;", "")
                obj.CAAmount = Decimal.Parse(Amount)
                obj.CAEmpID = HttpContext.Current.Server.HtmlEncode(oReader("empID").ToString()).Replace("&#160;", "")
                lData.Add(obj)
            End While
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -GetCADB")
        End Try

        Return lData
    End Function

    Public Function GetCAHistory(EmpID As String) As List(Of clsConnectCA)
        Dim sQuery As New StringBuilder

        sQuery.Append("SELECT CONCAT(B.supFirstName,' ',B.supMiddleName,' ',B.supLastName) AS caSupervisor,caStatus,caDate,caAmount FROM tblCashAdvance A ")
        sQuery.Append("INNER JOIN tblSupervisor B ON A.caSupervisor = B.ID INNER JOIN tblEmployee C ON A.caEmployee = B.ID WHERE A.isDeleted <> 1 AND C.empID = '" + EmpID + "'")

        Dim lData As New List(Of clsConnectCA)
        Try
            Dim oReader = SakadaExecReader(sQuery.ToString())

            While oReader.Read()
                Dim obj As New clsConnectCA
                obj.CASupervisor = HttpContext.Current.Server.HtmlEncode(oReader("caSupervisor").ToString()).Replace("&#160;", "")
                obj.CAStatus = HttpContext.Current.Server.HtmlEncode(oReader("caStatus").ToString()).Replace("&#160;", "")
                obj.CADate = HttpContext.Current.Server.HtmlEncode(oReader("caDate").ToString()).Replace("&#160;", "")
                obj.CAAmount = HttpContext.Current.Server.HtmlEncode(oReader("caAmount").ToString()).Replace("&#160;", "")
                lData.Add(obj)
            End While
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -GetCADB")
        End Try

        Return lData
    End Function

    Public Function SaveCADetails(obj As clsConnectCA) As Boolean
        Dim oConnection = SakadaCallConnection()
        Dim resultVal As Boolean = False
        Dim sQuery As New StringBuilder
        sQuery.Append("INSERT INTO tblCashAdvance (caSupervisor,caEmployee,caDate,caAmount,empID) VALUES (@Supervisor,@Employee,@Date,@Amount,@EmpID)")
        Try
            Using (oConnection)
                Dim oCommand = SakadaCallCommand()
                oCommand.Connection = oConnection
                oCommand.CommandText = sQuery.ToString()
                oCommand.CommandType = CommandType.Text
                oCommand.Parameters.AddWithValue("@Supervisor", obj.CASupervisor)
                oCommand.Parameters.AddWithValue("@Employee", obj.CAEmployee)
                oCommand.Parameters.AddWithValue("@Date", obj.CADate)
                oCommand.Parameters.AddWithValue("@Amount", obj.CAAmount)
                oCommand.Parameters.AddWithValue("@EmpID", obj.CAEmpID)
                oConnection.Open()
                oCommand.ExecuteNonQuery()
                resultVal = True
            End Using
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -SaveSupDetails")
        End Try
        Return resultVal
    End Function

    Public Function UpdateCADetails(caID As String, obj As clsConnectCA)
        Dim sQuery As New StringBuilder
        sQuery.Append("UPDATE tblCashAdvance SET caSupervisor = @Supervisor, caEmployee = @Employee, caDate = @Date, caAmount = @Amount WHERE caID = '" + caID + "'")
        Dim boolReturnVal As Boolean = False
        Dim oConnection = SakadaCallConnection()
        Try
            oConnection.Open()
            Dim oCommand = SakadaCallCommand()
            oCommand = New SqlClient.SqlCommand(sQuery.ToString(), oConnection)
            oCommand.Parameters.AddWithValue("@Supervisor", obj.CASupervisor)
            oCommand.Parameters.AddWithValue("@Employee", obj.CAEmployee)
            oCommand.Parameters.AddWithValue("@Date", obj.CADate)
            oCommand.Parameters.AddWithValue("@Amount", obj.CAAmount)
            oCommand.ExecuteNonQuery()
            boolReturnVal = True
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -UpdateCADetails")
        Finally
            oConnection.Close()
        End Try
        Return boolReturnVal
    End Function

    Public Function SetStatusApprove(caID As String)
        Dim sQuery As New StringBuilder
        sQuery.Append("UPDATE tblCashAdvance SET caStatus = 'Approve' WHERE caID = '" + caID + "'")
        Dim boolReturnVal As Boolean = False
        Dim oConnection = SakadaCallConnection()
        Try
            oConnection.Open()
            Dim oCommand = SakadaCallCommand()
            oCommand = New SqlClient.SqlCommand(sQuery.ToString(), oConnection)
            oCommand.ExecuteNonQuery()
            boolReturnVal = True
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -SetStatusApprove")
        Finally
            oConnection.Close()
        End Try
        Return boolReturnVal
    End Function

    Public Function SetStatusDisapprove(caID As String)
        Dim sQuery As New StringBuilder
        sQuery.Append("UPDATE tblCashAdvance SET caStatus = 'Disapprove' WHERE caID = '" + caID + "'")
        Dim boolReturnVal As Boolean = False
        Dim oConnection = SakadaCallConnection()
        Try
            oConnection.Open()
            Dim oCommand = SakadaCallCommand()
            oCommand = New SqlClient.SqlCommand(sQuery.ToString(), oConnection)
            oCommand.ExecuteNonQuery()
            boolReturnVal = True
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -SetStatusApprove")
        Finally
            oConnection.Close()
        End Try
        Return boolReturnVal
    End Function

    Public Function DeleteCARecord(caID As String)
        Dim sQuery As New StringBuilder
        sQuery.Append("UPDATE tblCashAdvance SET isDeleted = 1 WHERE caID = '" + caID + "'")
        Dim boolReturnVal As Boolean = False
        Dim oConnection = SakadaCallConnection()
        Try
            oConnection.Open()
            Dim oCommand = SakadaCallCommand()
            oCommand = New SqlClient.SqlCommand(sQuery.ToString(), oConnection)
            oCommand.ExecuteNonQuery()
            boolReturnVal = True
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message & " -DeleteCARecord")
        Finally
            oConnection.Close()
        End Try
        Return boolReturnVal
    End Function
End Class
