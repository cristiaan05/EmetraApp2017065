Imports MahApps.Metro.Controls.Dialogs
Imports MahApps.Metro.Controls
Imports System.ComponentModel
Imports EmetraApp
Imports EmetraApp.CristianHernandez.EmetraApp2017065.Model
Public Class RemissionModelView
    Implements INotifyPropertyChanged, ICommand, IDataErrorInfo
    Dim Dialogo As DialogCoordinator
#Region "Campos"
    Private _Validar As String

    Private _OrderNumber As Integer
    Private _RemissionDate As DateTime
    Private _Hour As String
    Private _Place As String

    Private _ListRemission As New List(Of Remission)
    Private _Model As RemissionModelView
    Private _Element As Remission

    Private _SanctionType As SanctionType
    Private _ListSanctionType As New List(Of SanctionType)
    Private _Agent As Agent
    Private _ListAgent As New List(Of Agent)
    Private _Vehicle As Vehicle
    Private _ListVehicle As New List(Of Vehicle)

    Private _BtnNew As Boolean = True
    Private _BtnSave As Boolean = False
    Private _BtnDelete As Boolean = True
    Private _BtnUpdate As Boolean = True
    Private _BtnCancel As Boolean = False
    Private _TextBox As Boolean = False

    Private DB As New EmetraApp2017065DataContext
#End Region
#Region "Propiedades"
    Public Property OrderNumber As Integer
        Get
            Return _OrderNumber
        End Get
        Set(value As Integer)
            _OrderNumber = value
        End Set
    End Property

    Public Property RemissionDate As Date
        Get
            Return _RemissionDate
        End Get
        Set(value As Date)
            _RemissionDate = value
            NotificarCambio("RemissionDate")
        End Set
    End Property

    Public Property Hour As String
        Get
            Return _Hour
        End Get
        Set(value As String)
            _Hour = value
            NotificarCambio("Hour")
        End Set
    End Property

    Public Property Place As String
        Get
            Return _Place
        End Get
        Set(value As String)
            _Place = value
            NotificarCambio("Place")
        End Set
    End Property

    Public Property ListRemission As List(Of Remission)
        Get
            If _ListRemission.Count = 0 Then
                _ListRemission = (From n In DB.Remissions Select n).ToList
            End If
            Return _ListRemission
        End Get
        Set(value As List(Of Remission))
            _ListRemission = value
            NotificarCambio("ListRemission")
        End Set
    End Property

    Public Property Model As RemissionModelView
        Get
            Return _Model
        End Get
        Set(value As RemissionModelView)
            _Model = value
        End Set
    End Property

    Public Property Element As Remission
        Get
            Return _Element
        End Get
        Set(value As Remission)
            _Element = value
            If value IsNot Nothing Then
                Me.OrderNumber = _Element.OrderNumber
                Me.RemissionDate = _Element.RemissionDate
                Me.Hour = _Element.Hour
                Me.Place = _Element.Place
                Me.SanctionType = _Element.SanctionType
                Me.Agent = _Element.Agent
                Me.Vehicle = _Element.Vehicle
            End If
        End Set
    End Property

    Public Property SanctionType As SanctionType
        Get
            Return _SanctionType
        End Get
        Set(value As SanctionType)
            _SanctionType = value
            NotificarCambio("SanctionType")
        End Set
    End Property

    Public Property ListSanctionType As List(Of SanctionType)
        Get
            If _ListSanctionType.Count = 0 Then
                _ListSanctionType = (From n In DB.SanctionTypes Select n).ToList
            End If
            Return _ListSanctionType
        End Get
        Set(value As List(Of SanctionType))
            _ListSanctionType = value
            NotificarCambio("ListSanctionType")
        End Set
    End Property

    Public Property Agent As Agent
        Get
            Return _Agent
        End Get
        Set(value As Agent)
            _Agent = value
            NotificarCambio("Agent")
        End Set
    End Property

    Public Property ListAgent As List(Of Agent)
        Get
            If _ListAgent.Count = 0 Then
                _ListAgent = (From n In DB.Agents Select n).ToList
            End If
            Return _ListAgent
        End Get
        Set(value As List(Of Agent))
            _ListAgent = value
            NotificarCambio("ListAgent")
        End Set
    End Property

    Public Property Vehicle As Vehicle
        Get
            Return _Vehicle
        End Get
        Set(value As Vehicle)
            _Vehicle = value
            NotificarCambio("Vehicle")
        End Set
    End Property

    Public Property ListVehicle As List(Of Vehicle)
        Get
            If _ListVehicle.Count = 0 Then
                _ListVehicle = (From n In DB.Vehicles Select n).ToList
            End If
            Return _ListVehicle
        End Get
        Set(value As List(Of Vehicle))
            _ListVehicle = value
            NotificarCambio("ListVehicle")
        End Set
    End Property

    Public Property BtnNew As Boolean
        Get
            Return _BtnNew
        End Get
        Set(value As Boolean)
            _BtnNew = value
            NotificarCambio("BtnNew")
        End Set
    End Property

    Public Property BtnSave As Boolean
        Get
            Return _BtnSave
        End Get
        Set(value As Boolean)
            _BtnSave = value
            NotificarCambio("BtnSave")
        End Set
    End Property

    Public Property BtnDelete As Boolean
        Get
            Return _BtnDelete
        End Get
        Set(value As Boolean)
            _BtnDelete = value
            NotificarCambio("BtnDelete")
        End Set
    End Property

    Public Property BtnUpdate As Boolean
        Get
            Return _BtnUpdate
        End Get
        Set(value As Boolean)
            _BtnUpdate = value
            NotificarCambio("BtnUpdate")
        End Set
    End Property

    Public Property BtnCancel As Boolean
        Get
            Return _BtnCancel
        End Get
        Set(value As Boolean)
            _BtnCancel = value
            NotificarCambio("BtnCancel")
        End Set
    End Property

    Public Property TextBox As Boolean
        Get
            Return _TextBox
        End Get
        Set(value As Boolean)
            _TextBox = value
            NotificarCambio("TextBox")
        End Set
    End Property
#End Region
#Region "Constructor"
    Public Sub New(ByVal Instancia As DialogCoordinator)
        Me.Model = Me
        Dialogo = Instancia
    End Sub
#End Region

#Region "ICommand"
    Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

    Public Sub Execute(parametro As Object) Implements ICommand.Execute
        If parametro.Equals("New") Then
            Me.BtnNew = False
            Me.BtnSave = True
            Me.BtnDelete = False
            Me.BtnUpdate = False
            Me.BtnCancel = True
            Me.TextBox = True
            Me.OrderNumber = Me.OrderNumber + 1
            Me.RemissionDate = #01/01/2018#
            Me.Hour = Nothing
            Me.Place = Nothing
            Me.SanctionType = Nothing
            Me.Agent = Nothing
            Me.Vehicle = Nothing
            _Validar = "Agregar"
        ElseIf parametro.Equals("Save") Then
            If _Validar.Equals("Agregar") Then
                Dim registro As New Remission
                registro.OrderNumber = Me.OrderNumber
                registro.RemissionDate = Me.RemissionDate
                registro.Hour = Me.Hour
                registro.Place = Me.Place
                registro.SanctionType = Me.SanctionType
                registro.Agent = Me.Agent
                registro.Vehicle = Me.Vehicle
                If String.IsNullOrEmpty(registro.Hour) Or String.IsNullOrWhiteSpace(registro.Hour) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo FirstName no puede ir vacio")
                ElseIf String.IsNullOrEmpty(registro.Place) Or String.IsNullOrWhiteSpace(registro.Place) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo LastName no puede ir vacio")
                Else
                    DB.Remissions.Add(registro)
                    DB.SaveChanges()
                    ListRemission = (From N In DB.Remissions Select N).ToList
                    Dialogo.ShowMessageAsync(Me, "Exito :) ", "Registro agregado exitosamente")
                    Me.BtnNew = True
                    Me.BtnSave = False
                    Me.BtnDelete = True
                    Me.BtnUpdate = True
                    Me.BtnCancel = False
                    Me.TextBox = False
                End If
            ElseIf _Validar.Equals("Modificar") Then
                Element.OrderNumber = OrderNumber
                Element.RemissionDate = RemissionDate
                Element.Hour = Hour
                Element.Place = Place
                Element.SanctionType = SanctionType
                Element.Agent = Agent
                Element.Vehicle = Vehicle
                If String.IsNullOrEmpty(Element.Hour) Or String.IsNullOrWhiteSpace(Element.Hour) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo FirstName no puede ir vacio")
                ElseIf String.IsNullOrEmpty(Element.Place) Or String.IsNullOrWhiteSpace(Element.Place) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo LastName no puede ir vacio")
                Else
                    DB.SaveChanges()
                    ListRemission = (From N In DB.Remissions Select N).ToList
                    Me.BtnNew = True
                    Me.BtnSave = False
                    Me.BtnDelete = True
                    Me.BtnUpdate = True
                    Me.BtnCancel = False
                    Me.TextBox = False
                End If
            End If
        ElseIf parametro.Equals("Delete") Then
            If Element IsNot Nothing Then
                Dim result As Integer = MsgBox("Está seguro de eliminar?", MessageBoxButton.YesNo)
                If result = MsgBoxResult.Yes Then
                    DB.Remissions.Remove(Element)
                    DB.SaveChanges()
                    Dialogo.ShowMessageAsync(Me, "Exito", "Registro eliminado exitosamente")
                    ListRemission = (From n In DB.Remissions Select n).ToList
                    Me.OrderNumber = Nothing
                    Me.RemissionDate = #01/01/2018#
                    Me.Hour = Nothing
                    Me.Place = Nothing
                    Me.SanctionType = Nothing
                    Me.Agent = Nothing
                    Me.Vehicle = Nothing
                ElseIf result = MsgBoxResult.No Then
                End If
            End If
        ElseIf parametro.Equals("Update") Then
            If Element IsNot Nothing Then
                Me.BtnNew = False
                Me.BtnSave = True
                Me.BtnDelete = False
                Me.BtnUpdate = False
                Me.BtnCancel = True
                Me.TextBox = True
                _Validar = "Modificar"
            End If
        ElseIf parametro.Equals("Cancel") Then
            Me.BtnNew = True
            Me.BtnSave = False
            Me.BtnDelete = True
            Me.BtnUpdate = True
            Me.BtnCancel = False
            Me.TextBox = False
            Me.TextBox = Nothing
            If _Validar = "Agregar" Then
                Me.OrderNumber = Me.OrderNumber - 1
            Else _Validar = "Modificar"
                Me.OrderNumber = Me.OrderNumber
            End If
        End If
    End Sub

    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        Return True
    End Function
#End Region
#Region "INotifyPropertyChanged"
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Sub NotificarCambio(ByVal Propiedad As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(Propiedad))
    End Sub

#End Region
#Region "IDataErrorInfo"
    Public ReadOnly Property [Error] As String Implements IDataErrorInfo.Error
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Default Public ReadOnly Property Item(columnName As String) As String Implements IDataErrorInfo.Item
        Get
            Throw New NotImplementedException()
        End Get
    End Property

#End Region


End Class
