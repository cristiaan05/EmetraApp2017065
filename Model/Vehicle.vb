Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace CristianHernandez.EmetraApp2017065.Model
    Public Class Vehicle
#Region "Campos"
        Private _LicensePlate As String
        Private _NIT As String
        Private _Brand As String
        Private _Model As String
        Private _TypeOfVehicle As String
#End Region
#Region "Propiedades"
        Public Overridable Property Neighbor() As Neighbor
        <Key>
        Public Property LicensePlate As String
            Get
                Return _LicensePlate
            End Get
            Set(value As String)
                _LicensePlate = value
            End Set
        End Property
        <ForeignKey("Neighbor")>
        Public Property NIT As String
            Get
                Return _NIT
            End Get
            Set(value As String)
                _NIT = value
            End Set
        End Property

        Public Property Brand As String
            Get
                Return _Brand
            End Get
            Set(value As String)
                _Brand = value
            End Set
        End Property

        Public Property Model As String
            Get
                Return _Model
            End Get
            Set(value As String)
                _Model = value
            End Set
        End Property

        Public Property TypeOfVehicle As String
            Get
                Return _TypeOfVehicle
            End Get
            Set(value As String)
                _TypeOfVehicle = value
            End Set
        End Property
#End Region
    End Class
End Namespace

