Imports System.Data.SqlClient

Public Class Connection
    Protected didPreviouslyConnect As Boolean = False
    Protected didCreateTable As Boolean = False
    Protected connectionString As String
    Protected serverName As String
    Private isTransacting As Boolean = False
    Private connection As SqlConnection

    Private Const dropSQL = "if exists(select 1 from master..sysdatabases where Name = 'ptl_test') begin drop database ptl_test; end"
    Private Const createDBSQL = "create database ptl_test;"
    Private Const createTableSQL = "create table dimensions (id int primary key identity(1,1), n int Not null, m int Not null, check(n > 0), check(m > 0), check(n <= 100), check(m <= 100)) create table row (id int primary key identity(1,1), dimensionsID int foreign key references dimensions(ID), row int not null, content nvarchar(max) not null, check(row >= 0))"

    Public Sub New(serverName As String)
        connectionString = "Server=" & serverName & ";" &
        "DataBase=master;" &
        "Integrated Security=SSPI"

        serverName = serverName

        CreateDatabase()
        Console.Out.WriteLine("Database has been created.")
    End Sub

    ' This routine executes a SQL statement that deletes the database (if it exists)  
    ' and then creates it.  
    Public Sub CreateDatabase()
        Try
            'Initialize SQL Connection
            connection = New SqlConnection(connectionString)

            'drop DB
            ExecuteNonQuery(dropSQL)

            'create DB
            ExecuteNonQuery(createDBSQL)

            'connect to new DB
            connectionString = "Server=" & serverName & ";" &
        "DataBase=ptl_test;" &
        "Integrated Security=SSPI"
            connection = New SqlConnection(connectionString)

            'create table
            ExecuteNonQuery(createTableSQL)

            'Data has been successfully submitted. 
            didPreviouslyConnect = True
            didCreateTable = True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly, "SQL Exception Error")
        Catch exc As Exception
            ' Unable to connect to SQL Server or MSDE
            MsgBox(exc.Message, MsgBoxStyle.OkOnly, "Exception Error")
        End Try
    End Sub

    Public Sub ExecuteNonQuery(SQL As String)
        isTransacting = True

        'An SqlCommand object is used to execute the SQL commands. 
        Dim Cmd As New SqlCommand(SQL, connection)

        'Execute SQL
        connection.Open()
        Cmd.ExecuteNonQuery()
        connection.Close()

        isTransacting = False
    End Sub

    Public Function ExecuteScalar(SQL As String)
        isTransacting = True

        'An SqlCommand object is used to execute the SQL commands. 
        Dim Cmd As New SqlCommand(SQL, connection)

        'Execute SQL
        connection.Open()
        Dim Result As Object = Cmd.ExecuteScalar()
        connection.Close()

        isTransacting = False

        Return Result
    End Function

    Public Function ExecuteQuery(SQL As String)
        Try
            If Not isTransacting Then
                isTransacting = True

                'An SqlCommand object is used to execute the SQL commands. 
                Dim Cmd As New SqlCommand(SQL, connection)

                'Execute SQL
                connection.Open()
                Dim reader As SqlDataReader = Cmd.ExecuteReader()

                Dim dataTable As DataTable = New DataTable
                dataTable.Load(reader)
                Dim dataTableReader As DataTableReader = dataTable.CreateDataReader()
                connection.Close()

                isTransacting = False

                Return dataTableReader
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "ExecuteQuery Error")
        End Try
        Return Nothing
    End Function
End Class
