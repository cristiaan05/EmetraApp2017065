Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace CristianHernandez.EmetraApp2017065.Model
    Public Class Neighbor
#Region "Campos"
        Private _Nit As String
        Private _DPI As String
        Private _FirstName As String
        Private _LastName As String
        Private _Address As String
        Private _Municipality As String
        Private _PostalCode As Integer
        Private _Telephone As String
#End Region
#Region "Propiedades"
        Public Overridable Property Vehicles() As ICollection(Of Vehicle)
        <Key>
        Public Property Nit As String
            Get
                Return _Nit
            End Get
            Set(value As String)
                _Nit = value
            End Set
        End Property

        Public Property DPI As String
            Get
                Return _DPI
            End Get
            Set(value As String)
                _DPI = value
            End Set
        End Property

        Public Property FirstName As String
            Get
                Return _FirstName
            End Get
            Set(value As String)
                _FirstName = value
            End Set
        End Property

        Public Property LastName As String
            Get
                Return _LastName
            End Get
            Set(value As String)
                _LastName = value
            End Set
        End Property

        Public Property Address As String
            Get
                Return _Address
            End Get
            Set(value As String)
                _Address = value
            End Set
        End Property

        Public Property Municipality As String
            Get
                Return _Municipality
            End Get
            Set(value As String)
                _Municipality = value
            End Set
        End Property

        Public Property PostalCode As Integer
            Get
                Return _PostalCode
            End Get
            Set(value As Integer)
                _PostalCode = value
            End Set
        End Property

        Public Property Telephone As String
            Get
                Return _Telephone
            End Get
            Set(value As String)
                _Telephone = value
            End Set
        End Property
#End Region


    End Class
End Namespace

