Imports MySweeper

Public Class NumericMinefield
    Implements IMinefield
    Public Property _id As Integer
    Public Property _n As Integer
    Public Property _m As Integer
    Public Property _minefield As UShort(,)

#Region "Properties"
    Private Property intMinefield__id As Integer Implements IMinefield._id
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Private Property intMinefield__n As Integer Implements IMinefield._n
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Private Property intMinefield__m As Integer Implements IMinefield._m
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Private Property intMinefield__minefield As Object Implements IMinefield._minefield
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Object)
            Throw New NotImplementedException()
        End Set
    End Property
#End Region

    Public Sub New(id As Integer, n As Integer, m As Integer, minefield As String())
        'validate and instantiate
        _id = id
        If n < 1 Or n > 100 Then
            Throw New InvalidConstraintException("n must be inclusively between 1 and 100")
        Else
            _n = n
        End If

        If m < 1 Or m > 100 Then
            Throw New InvalidConstraintException("m must be inclusively between 1 and 100")
        Else
            _m = m
        End If

        'If minefield.Length <> (m * n) + n Then
        '    Throw New InvalidConstraintException("minefield length must be equal to the product of rows and columns")
        'Else
        ReDim _minefield(_n - 1, _m - 1)

        For y As Integer = 0 To _m - 1
            For x As Integer = 0 To _n - 1
                If minefield(x).Substring(y, 1).Equals("*") Then
                    IncrementPerimeter(x, y)
                End If
            Next
        Next
        'End If
    End Sub

    'increment 
    Private Sub IncrementPerimeter(x As Integer, y As Integer)
        If x > 0 And y > 0 Then
            _minefield(x - 1, y - 1) += 1
        End If

        If y > 0 Then
            _minefield(x, y - 1) += 1
        End If

        If x < _n - 1 And y > 0 Then
            _minefield(x + 1, y - 1) += 1
        End If


        If x > 0 Then
            _minefield(x - 1, y) += 1
        End If

        If x < _n - 1 Then
            _minefield(x + 1, y) += 1
        End If


        If x > 0 And y < _m - 1 Then
            _minefield(x - 1, y + 1) += 1
        End If

        If y < _m - 1 Then
            _minefield(x, y + 1) += 1
        End If

        If x < _n - 1 And y < _m - 1 Then
            _minefield(x + 1, y + 1) += 1
        End If
    End Sub
End Class
