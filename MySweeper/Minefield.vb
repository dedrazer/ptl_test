Imports MySweeper

Public Class Minefield
    Implements IMinefield
    Public Property _id As Integer
    Public Property _n As Integer
    Public Property _m As Integer
    Public Property _minefield As String()

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
        _minefield = minefield
        'End If
    End Sub

    'solve minefield
    Public Function Solve()
        Return New NumericMinefield(_id, _n, _m, _minefield)
    End Function
End Class
