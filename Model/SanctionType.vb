Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Namespace CristianHernandez.EmetraApp2017065.Model
    Public Class SanctionType
#Region "Campos"
        Private _RemissionID As Integer
        Private _Description As String
        Private _Import As Decimal
        Private _Recurrent As String
#End Region
#Region "Propiedades"
        Public Overridable Property Remissions() As ICollection(Of Remission)
        <Key>
        Public Property RemissionID As Integer
            Get
                Return _RemissionID
            End Get
            Set(value As Integer)
                _RemissionID = value
            End Set
        End Property

        Public Property Description As String
            Get
                Return _Description
            End Get
            Set(value As String)
                _Description = value
            End Set
        End Property

        Public Property Import As Decimal
            Get
                Return _Import
            End Get
            Set(value As Decimal)
                _Import = value
            End Set
        End Property

        Public Property Recurrent As String
            Get
                Return _Recurrent
            End Get
            Set(value As String)
                _Recurrent = value
            End Set
        End Property
#End Region
    End Class
End Namespace
