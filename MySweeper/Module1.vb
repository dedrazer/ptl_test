Imports System.Data.SqlClient

Module Module1
    Sub Main()
        Dim data = New SQL("DESKTOP-HAJHNJ6")

        data.PopulateValidMinefields()
        'c.PopulateInvalidMinefields()
        Console.Out.WriteLine("Database has been populated.")

        Dim minefields As List(Of Minefield) = data.GetMinefields()

        Console.Out.WriteLine(minefields.Count & " minefields have been downloaded.")

        Console.Clear()

        For counter As Integer = 0 To minefields.Count - 1 Step 1
            Console.WriteLine("Field #" & counter)
            'solve minefield
            Dim minefield = minefields(counter)
            Dim solvedField = minefield.Solve()
            'print minefield

            For x As Integer = 0 To solvedField._n - 1 Step 1
                For y As Integer = 0 To solvedField._m - 1 Step 1
                    If minefield._minefield(x).Substring(y, 1).Equals("*") Then
                        Console.Write("*")
                    Else
                        Console.Write(solvedField._minefield(x, y))
                    End If
                Next
                Console.WriteLine()
            Next
            Console.WriteLine()
        Next

        Console.ReadKey()
    End Sub

End Module
