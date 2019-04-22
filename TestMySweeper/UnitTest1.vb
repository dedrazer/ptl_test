Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports MySweeper

<TestClass()> Public Class UnitTest1

    <TestMethod()> Public Sub TestMethod1()
        Dim content As String() = {"...*.", ".....", ".*...", "....*", "....."}
        Dim expected As UShort(,) = {{0, 0, 1, 0, 1}, {1, 1, 2, 1, 1}, {1, 0, 1, 1, 1}, {1, 1, 1, 1, 0}, {0, 0, 0, 1, 1}}

        Dim m As Minefield = New Minefield(0, 5, 5, content)

        Dim actual As UShort(,) = m.Solve()._minefield

        For x As Integer = 0 To m._n - 1
            For y As Integer = 0 To m._m - 1
                Assert.AreEqual(expected(x, y), actual(x, y))
            Next
        Next
    End Sub

    <TestMethod()> Public Sub TestMethod2()
        Dim content As String() = {"...*...", ".......", ".*.....", "....*..", "......*"}
        Dim expected As UShort(,) = {{0, 0, 1, 0, 1, 0, 0}, {1, 1, 2, 1, 1, 0, 0}, {1, 0, 1, 1, 1, 0, 0}, {1, 1, 1, 1, 0, 1, 1}, {0, 0, 0, 1, 1, 1, 0}}

        Dim m As Minefield = New Minefield(0, 5, 5, content)

        Dim actual As UShort(,) = m.Solve()._minefield

        For x As Integer = 0 To m._n - 1
            For y As Integer = 0 To m._m - 1
                Assert.AreEqual(expected(x, y), actual(x, y))
            Next
        Next
    End Sub
End Class