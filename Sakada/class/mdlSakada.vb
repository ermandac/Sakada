Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports System.Configuration
Public Module mdlSakada
    Dim oConnection As SqlConnection
    Dim oCommand As SqlCommand
    Public strConStr = ConfigurationManager.ConnectionStrings.Item("SakadaDB").ToString()

    Public Sub SakadaOpenNewConnection()
        oConnection.Open()
    End Sub

    Public Function SakadaCallConnection() As SqlConnection
        Dim newConnection As New SqlConnection(strConStr)
        Return newConnection
    End Function

    Public Function SakadaCallCommand() As SqlCommand
        Dim newCommand As New SqlCommand()
        Return newCommand
    End Function

    Public Function SakadaCloseNewConnection()
        oConnection.Close()
        Return oConnection
    End Function

    Public Function SakadaExecReader(sQuery As String) As SqlDataReader
        Dim oReader As SqlDataReader = Nothing
        oConnection = New SqlConnection(strConStr)
        oCommand = New SqlCommand(sQuery, oConnection)
        Try
            SakadaOpenNewConnection()
            oReader = oCommand.ExecuteReader()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return oReader
    End Function

    Public Function SakadaExecNonQuery(sQuery As String) As Boolean
        Dim oReader As SqlDataReader = Nothing
        oConnection = New SqlConnection(strConStr)
        oCommand = New SqlCommand(sQuery, oConnection)
        Dim boolReturnVal = False
        Try
            oConnection.Open()
            oCommand.ExecuteNonQuery()
            boolReturnVal = True
        Catch ex As Exception
            boolReturnVal = False
            Console.WriteLine(ex.Message)
        Finally
            oConnection.Close()
        End Try
        Return boolReturnVal
    End Function
End Module
