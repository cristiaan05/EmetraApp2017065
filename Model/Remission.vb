Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Namespace CristianHernandez.EmetraApp2017065.Model
    Public Class Remission
#Region "Campos"
        Private _OrderNumber As Integer
        Private _RemissionDate As DateTime
        Private _Hour As String
        Private _Place As String
        Private _RemissionID As Integer
        Private _AgentID As Integer
        Private _LicensePlate As String
#End Region
#Region "Propiedades"
        Public Overridable Property Vehicle() As Vehicle
        Public Overridable Property Agent() As Agent
        Public Overridable Property SanctionType() As SanctionType
        <Key>
        Public Property OrderNumber As Integer
            Get
                Return _OrderNumber
            End Get
            Set(value As Integer)
                _OrderNumber = value
            End Set
        End Property

        Public Property RemissionDate As DateTime
            Get
                Return _RemissionDate
            End Get
            Set(value As DateTime)
                _RemissionDate = value
            End Set
        End Property

        Public Property Hour As String
            Get
                Return _Hour
            End Get
            Set(value As String)
                _Hour = value
            End Set
        End Property

        Public Property Place As String
            Get
                Return _Place
            End Get
            Set(value As String)
                _Place = value
            End Set
        End Property
        <ForeignKey("SanctionType")>
        Public Property RemissionID As Integer
            Get
                Return _RemissionID
            End Get
            Set(value As Integer)
                _RemissionID = value
            End Set
        End Property

        Public Property AgentID As Integer
            Get
                Return _AgentID
            End Get
            Set(value As Integer)
                _AgentID = value
            End Set
        End Property
        <ForeignKey("Vehicle")>
        Public Property LicensePlate As String
            Get
                Return _LicensePlate
            End Get
            Set(value As String)
                _LicensePlate = value
            End Set
        End Property
#End Region
    End Class
End Namespace

