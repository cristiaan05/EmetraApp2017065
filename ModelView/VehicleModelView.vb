Imports MahApps.Metro.Controls.Dialogs
Imports MahApps.Metro.Controls
Imports System.ComponentModel
Imports EmetraApp
Imports EmetraApp.CristianHernandez.EmetraApp2017065.Model
Public Class VehicleModelView
    Implements INotifyPropertyChanged, ICommand, IDataErrorInfo
    Dim Dialogo As DialogCoordinator
#Region "Campos"
    Private _Validar As String

    Private _LicensePlate As String
    Private _Brand As String
    Private _Model As String
    Private _TypeOfVehicle As String

    Private _Neighbor As Neighbor
    Private _ListNeighbor As New List(Of Neighbor)

    Private _ListVehicle As New List(Of Vehicle)
    Private _Modelo As VehicleModelView
    Private _Element As Vehicle

    Private _BtnNew As Boolean = True
    Private _BtnSave As Boolean = False
    Private _BtnDelete As Boolean = True
    Private _BtnUpdate As Boolean = True
    Private _BtnCancel As Boolean = False
    Private _TextBox As Boolean = False

    Private DB As New EmetraApp2017065DataContext
#End Region
#Region "Propiedades"
    Public Property LicensePlate As String
        Get
            Return _LicensePlate
        End Get
        Set(value As String)
            _LicensePlate = value
            NotificarCambio("LicensePlate")
        End Set
    End Property
    Public Property Neighbor As Neighbor
        Get
            Return _Neighbor
        End Get
        Set(value As Neighbor)
            _Neighbor = value
            NotificarCambio("Neighbor")
        End Set
    End Property

    Public Property ListNeighbor As List(Of Neighbor)
        Get
            If _ListNeighbor.Count = 0 Then
                _ListNeighbor = (From n In DB.Neighbors Select n).ToList
            End If
            Return _ListNeighbor
        End Get
        Set(value As List(Of Neighbor))
            _ListNeighbor = value
            NotificarCambio("ListNeighbor")
        End Set
    End Property
    Public Property Brand As String
        Get
            Return _Brand
        End Get
        Set(value As String)
            _Brand = value
            NotificarCambio("Brand")
        End Set
    End Property

    Public Property Model As String
        Get
            Return _Model
        End Get
        Set(value As String)
            _Model = value
            NotificarCambio("Model")
        End Set
    End Property

    Public Property TypeOfVehicle As String
        Get
            Return _TypeOfVehicle
        End Get
        Set(value As String)
            _TypeOfVehicle = value
            NotificarCambio("TypeOfVehicle")
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

    Public Property Modelo As VehicleModelView
        Get
            Return _Modelo
        End Get
        Set(value As VehicleModelView)
            _Modelo = value
        End Set
    End Property

    Public Property Element As Vehicle
        Get
            Return _Element
        End Get
        Set(value As Vehicle)
            _Element = value
            NotificarCambio("Element")
            If value IsNot Nothing Then
                Me.LicensePlate = _Element.LicensePlate
                Me.Neighbor = _Element.Neighbor
                Me.Brand = _Element.Brand
                Me.Model = _Element.Model
                Me.TypeOfVehicle = _Element.TypeOfVehicle
            End If
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
        Me.Modelo = Me
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
            Me.LicensePlate = Nothing
            Me.Neighbor = Nothing
            Me.Brand = Nothing
            Me.Model = Nothing
            Me.TypeOfVehicle = Nothing
            _Validar = "Agregar"
        ElseIf parametro.Equals("Save") Then
            If _Validar.Equals("Agregar") Then
                Try
                    Dim registro As New Vehicle
                    registro.LicensePlate = Me.LicensePlate
                    registro.Neighbor = Me.Neighbor
                    registro.Brand = Me.Brand
                    registro.Model = Me.Model
                    registro.TypeOfVehicle = Me.TypeOfVehicle
                    If IsNumeric(registro.Model) Then
                        If String.IsNullOrEmpty(registro.LicensePlate) Or String.IsNullOrWhiteSpace(registro.LicensePlate) Then
                            Dialogo.ShowMessageAsync(Me, "Error :(", "El campo LicensePlate no puede estar vacio")
                        ElseIf String.IsNullOrEmpty(registro.Brand) Or String.IsNullOrWhiteSpace(registro.Brand) Then
                            Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Brand no puede estar vacio")
                        ElseIf String.IsNullOrEmpty(registro.Model) Or String.IsNullOrWhiteSpace(registro.Model) Then
                            Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Model no puede estar vacio")
                        ElseIf String.IsNullOrEmpty(registro.TypeOfVehicle) Or String.IsNullOrWhiteSpace(registro.TypeOfVehicle) Then
                            Dialogo.ShowMessageAsync(Me, "Error :( ", "El campo TypeOfVehicle no puede estar vacio")
                        Else
                            For Each c In ListVehicle
                                If c.LicensePlate = LicensePlate Then
                                    Dialogo.ShowMessageAsync(Me, "Error :(", "El LicensePlate ya existe")
                                    Exit Sub
                                End If
                            Next
                            DB.Vehicles.Add(registro)
                            DB.SaveChanges()
                            ListVehicle = (From N In DB.Vehicles Select N).ToList
                            Dialogo.ShowMessageAsync(Me, "Exito :) ", "Registro agregado exitosamente")
                            Me.BtnNew = True
                            Me.BtnSave = False
                            Me.BtnDelete = True
                            Me.BtnUpdate = True
                            Me.BtnCancel = False
                            Me.TextBox = False
                        End If
                    Else
                        Dialogo.ShowMessageAsync(Me, "Error :(", "El Modelo solo acepta numeros enteros")
                    End If
                Catch ex As Exception
                    Dialogo.ShowMessageAsync(Me, "Error", "Contacte a Soporte")
                End Try
            ElseIf _Validar.Equals("Modificar") Then
                Element.LicensePlate = LicensePlate
                Element.Neighbor = Neighbor
                Element.Brand = Brand
                Element.Model = Model
                Element.TypeOfVehicle = TypeOfVehicle
                If String.IsNullOrEmpty(Element.LicensePlate) Or String.IsNullOrWhiteSpace(Element.LicensePlate) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo LicensePlate no puede estar vacio")
                ElseIf String.IsNullOrEmpty(Element.Brand) Or String.IsNullOrWhiteSpace(Element.Brand) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Brand no puede estar vacio")
                ElseIf String.IsNullOrEmpty(Element.Model) Or String.IsNullOrWhiteSpace(Element.Model) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Model no puede estar vacio")
                ElseIf String.IsNullOrEmpty(Element.TypeOfVehicle) Or String.IsNullOrWhiteSpace(Element.TypeOfVehicle) Then
                    Dialogo.ShowMessageAsync(Me, "Error :( ", "El campo TypeOfVehicle no puede estar vacio")
                Else
                    For Each c In ListVehicle
                        If c.LicensePlate = LicensePlate Then
                            Dialogo.ShowMessageAsync(Me, "Error :(", "No puede modificar LicensePlate")
                            Exit Sub
                        End If
                    Next
                    DB.SaveChanges()
                    ListVehicle = (From N In DB.Vehicles Select N).ToList
                    Me.BtnNew = True
                    Me.BtnSave = False
                    Me.BtnDelete = True
                    Me.BtnUpdate = True
                    Me.BtnCancel = False
                    Me.TextBox = False
                End If
            End If
        ElseIf parametro.Equals("Delete") Then
            Try
                If Element IsNot Nothing Then
                    Dim result As Integer = MsgBox("Está seguro de eliminar?", MessageBoxButton.YesNo)
                    If result = MsgBoxResult.Yes Then
                        DB.Vehicles.Remove(Element)
                        DB.SaveChanges()
                        Dialogo.ShowMessageAsync(Me, "Exito", "Registro eliminado exitosamente")
                        ListVehicle = (From n In DB.Vehicles Select n).ToList
                        Me.LicensePlate = Nothing
                        Me.Neighbor = Nothing
                        Me.Brand = Nothing
                        Me.Model = Nothing
                        Me.TypeOfVehicle = Nothing
                    ElseIf result = MsgBoxResult.No Then
                    End If
                End If
            Catch ex As Exception
                Dialogo.ShowMessageAsync(Me, "Error :(", "El vehiculo tiene remisiones pendientes,eliminelas antes")
            End Try
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
        End If
    End Sub

    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        Return True
    End Function
#End Region
#Region "INotifyPropertyChanged"
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Sub NotificarCambio(ByRef Propiedad As String)
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
