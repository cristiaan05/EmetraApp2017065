Imports System.ComponentModel
Public Class MainWindowModelView
    Implements INotifyPropertyChanged, ICommand

#Region "Campos"
    Private _Model As MainWindowModelView

    Private _FlyoutVentanas As Boolean = True
#End Region

#Region "Propiedades"
    Public Property Model As MainWindowModelView
        Get
            Return _Model
        End Get
        Set(value As MainWindowModelView)
            _Model = value
            NotificarCambio("Model")
        End Set
    End Property

    Public Property FlyoutVentanas As Boolean
        Get
            Return _FlyoutVentanas
        End Get
        Set(value As Boolean)
            _FlyoutVentanas = value
            NotificarCambio("FlyoutVentanas")
        End Set
    End Property

#End Region

#Region "Constructor"
    Public Sub New()
        Me.Model = Me
    End Sub
#End Region

#Region "INotifyPropertyChanged"
    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Public Sub NotificarCambio(ByVal Propiedad As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(Propiedad))
    End Sub
#End Region

#Region "ICommand"
    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        Return True
    End Function

    Public Event CanExecuteChanged(sender As Object, e As EventArgs) Implements ICommand.CanExecuteChanged

    Public Sub Execute(parametro As Object) Implements ICommand.Execute
        If parametro.Equals("AgentWindow") Then
            Dim WindowAgent As New AgentView()
            WindowAgent.ShowDialog()
        ElseIf parametro.Equals("NeighborWindow") Then
            Dim WindowNeighbor As New NeighborView()
            WindowNeighbor.ShowDialog()
        ElseIf parametro.Equals("RemissionWindow") Then
            Dim WindowRemission As New RemissionView()
            WindowRemission.ShowDialog()
        ElseIf parametro.Equals("SanctionTypeWindow") Then
            Dim WinSanction As New SanctionTypeView()
            WinSanction.ShowDialog()
        ElseIf parametro.Equals("VehicleWindow") Then
            Dim WindowVehicle As New VehicleView()
            WindowVehicle.ShowDialog()
        ElseIf parametro.Equals("flyoutVentanas") Then
            If FlyoutVentanas = False Then
                FlyoutVentanas = True
            Else
                FlyoutVentanas = False
            End If
        End If
    End Sub
#End Region
End Class
