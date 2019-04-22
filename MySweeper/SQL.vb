Public Class SQL
    'Variable Declaration'
    Dim connection As Connection
    Dim r = New Random()

    Private Const SQLGetDimensions As String = "select * from dbo.dimensions"
    Private Const SQLGetRows As String = "select content from dbo.row where dimensionsID = "
    Private Const SQLOrderRows As String = " order by row asc"

    Private Sub InsertMinefield(n As Integer, m As Integer, minefield As String)
        Dim dimensionId As Integer =
            connection.ExecuteScalar("insert into dbo.dimensions (n, m) output Inserted.ID values (" & n & ", " & m & ")")

        For row As Integer = 0 To n - 1 Step 1
            Try
                Dim content = minefield.Substring(row * m, m)
                connection.ExecuteNonQuery("insert into dbo.row (dimensionsID, row, content) values (" & dimensionId & ", " & row & ", '" & content & "')")
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.OkOnly, "InsertMinefield Error")
            End Try
        Next
    End Sub

    Private Sub InsertValidMinefield(n As Integer, m As Integer)
        Dim minefield As String = ""
        'fill minefield with roughly 90% "." and the rest as "*"
        For counter As Integer = 0 To m * n Step 1
            If r.Next(10) < 2 Then
                minefield += "*"
            Else
                minefield += "."
            End If
        Next

        InsertMinefield(n, m, minefield)
    End Sub

    'Private Function CreateInvalidValuesStatement1(n As Integer, m As Integer)
    '    Dim minefield As String = ""
    '    'fill minefield with garbage
    '    For counter As Integer = 0 To m * n Step 1
    '        If r.Next(10) < 2 Then
    '            minefield += "x"
    '        Else
    '            minefield += "z"
    '        End If
    '    Next
    '    Return CreateValuesStatement(n, m, minefield)
    'End Function

    'Private Function CreateInvalidValuesStatement2(n As Integer, m As Integer)
    '    Dim minefield As String = ""
    '    'fill minefield with improperly sized grids
    '    For counter As Integer = 0 To m + n Step 1
    '        If r.Next(10) < 2 Then
    '            minefield += "*"
    '        Else
    '            minefield += "."
    '        End If
    '    Next
    '    Return CreateValuesStatement(n, m, minefield)
    'End Function

    Public Sub PopulateValidMinefields()
        Try
            'Populate 250 valid 10x10 minefields
            For counter As Integer = 1 To 250 Step 1
                InsertValidMinefield(10, 10)
            Next
            'Populate 250 valid 5x20 minefields
            For counter As Integer = 1 To 250 Step 1
                InsertValidMinefield(5, 20)
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "PopulateValidMinefields Error")
        End Try
    End Sub

    'Public Sub PopulateInvalidMinefields()
    '    Try
    '        'Initialize SQL Connection
    '        Dim connection As New SqlConnection(connectionString)

    '        Dim SQL = "insert into dbo.minefield values "

    '        'Populate 100 invalid minefields
    '        For counter As Integer = 0 To 50 Step 1
    '            SQL += CreateInvalidValuesStatement1(10, 10) & ","
    '        Next
    '        For counter As Integer = 0 To 50 Step 1
    '            SQL += CreateInvalidValuesStatement2(10, 10) & ","
    '        Next
    '        SQL += CreateInvalidValuesStatement1(10, 10)

    '        Dim Cmd As New SqlCommand(SQL, connection)

    '        connection.Open()
    '        Cmd.ExecuteNonQuery()
    '        connection.Close()
    '    Catch exc As Exception
    '        MsgBox(exc.Message, MsgBoxStyle.OkOnly, "Exception Error")
    '    End Try
    'End Sub

    Public Function GetMinefields()
        Try
            Dim dimensionsReader As DataTableReader =
                connection.ExecuteQuery(SQLGetDimensions)

            Dim minefields As List(Of Minefield) = New List(Of Minefield)

            While dimensionsReader.Read()
                Dim id As Integer = dimensionsReader.GetInt32(0)
                Dim n As Integer = dimensionsReader.GetInt32(1)
                Dim m As Integer = dimensionsReader.GetInt32(2)
                Dim rows As String() = GetRows(id, m)

                Dim minefield As Minefield = New Minefield(id, n, m, rows)

                minefields.Add(minefield)
            End While

            Return minefields
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "GetMinefields Error")
        End Try
        Return Nothing
    End Function

    Public Function GetRows(dimensionsId As Integer, m As Integer)
        Try
            Dim rowReader As DataTableReader =
                connection.ExecuteQuery(SQLGetRows & dimensionsId & SQLOrderRows)

            Dim rows(m - 1) As String

            Dim counter As Integer = 0
            While rowReader.Read() And counter < m
                Dim row As String = rowReader.GetString(0)
                rows(counter) = row
                counter += 1
            End While

            If rowReader.NextResult Then
                Throw New Exception("minefield has too many rows")
            End If

            Return rows
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "GetRows Error")
        End Try
        Return Nothing
    End Function

    Public Sub New(serverName As String)
        connection = New Connection(serverName)
    End Sub
End Class
