Imports System.ComponentModel
Imports EmetraApp.CristianHernandez.EmetraApp2017065.Model
Imports EmetraApp
Imports MahApps.Metro.Controls
Imports MahApps.Metro.Controls.Dialogs

Public Class NeighborModelView
    Implements INotifyPropertyChanged, ICommand, IDataErrorInfo
    Dim Dialogo As DialogCoordinator
#Region "Campos"
    Private _Validar As String

    Private _Nit As String
    Private _DPI As String
    Private _FirstName As String
    Private _LastName As String
    Private _Address As String
    Private _Municipality As String
    Private _PostalCode As Integer
    Private _Telephone As String

    Private _ListNeighbor As New List(Of Neighbor)
    Private _Model As NeighborModelView
    Private _Element As Neighbor

    Private _BtnNew As Boolean = True
    Private _BtnSave As Boolean = False
    Private _BtnDelete As Boolean = True
    Private _BtnUpdate As Boolean = True
    Private _BtnCancel As Boolean = False
    Private _TextBox As Boolean = False

    Private DB As New EmetraApp2017065DataContext


#End Region
#Region "Propiedades"
    Public Property Nit As String
        Get
            Return _Nit
        End Get
        Set(value As String)
            _Nit = value
            NotificarCambio("Nit")
        End Set
    End Property

    Public Property DPI As String
        Get
            Return _DPI
        End Get
        Set(value As String)
            _DPI = value
            NotificarCambio("DPI")
        End Set
    End Property

    Public Property FirstName As String
        Get
            Return _FirstName
        End Get
        Set(value As String)
            _FirstName = value
            NotificarCambio("FirstName")
        End Set
    End Property

    Public Property LastName As String
        Get
            Return _LastName
        End Get
        Set(value As String)
            _LastName = value
            NotificarCambio("LastName")
        End Set
    End Property

    Public Property Address As String
        Get
            Return _Address
        End Get
        Set(value As String)
            _Address = value
            NotificarCambio("Address")
        End Set
    End Property

    Public Property Municipality As String
        Get
            Return _Municipality
        End Get
        Set(value As String)
            _Municipality = value
            NotificarCambio("Municipality")
        End Set
    End Property

    Public Property PostalCode As Integer
        Get
            Return _PostalCode
        End Get
        Set(value As Integer)
            _PostalCode = value
            NotificarCambio("PostalCode")
        End Set
    End Property

    Public Property Telephone As String
        Get
            Return _Telephone
        End Get
        Set(value As String)
            _Telephone = value
            NotificarCambio("Telephone")
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

    Public Property Model As NeighborModelView
        Get
            Return _Model
        End Get
        Set(value As NeighborModelView)
            _Model = value
        End Set
    End Property

    Public Property Element As Neighbor
        Get
            Return _Element
        End Get
        Set(value As Neighbor)
            _Element = value
            NotificarCambio("Element")
            If value IsNot Nothing Then
                Me.Nit = _Element.Nit
                Me.DPI = _Element.DPI
                Me.FirstName = _Element.FirstName
                Me.LastName = _Element.LastName
                Me.Address = _Element.Address
                Me.Municipality = _Element.Municipality
                Me.PostalCode = _Element.PostalCode
                Me.Telephone = _Element.Telephone
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
            Me.Nit = Nothing
            Me.DPI = Nothing
            Me.FirstName = Nothing
            Me.LastName = Nothing
            Me.Address = Nothing
            Me.Municipality = Nothing
            Me.Telephone = Nothing
            _Validar = "Agregar"
        ElseIf parametro.Equals("Save") Then
            If _Validar.Equals("Agregar") Then
                Dim registro As New Neighbor
                registro.Nit = Me.Nit
                registro.DPI = Me.DPI
                registro.FirstName = Me.FirstName
                registro.LastName = Me.LastName
                registro.Address = Me.Address
                registro.Municipality = Me.Municipality
                registro.PostalCode = Me.PostalCode
                registro.Telephone = Me.Telephone
                If IsNumeric(registro.PostalCode) Then
                    If registro.Telephone.Contains("_") Then
                        Dialogo.ShowMessageAsync(Me, "Error :(", "El numero no puede llevar espacios en blanco")
                    ElseIf String.IsNullOrEmpty(registro.Nit) Or String.IsNullOrWhiteSpace(registro.Nit) Then
                        Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Nit no puede ir vacio")
                    ElseIf String.IsNullOrEmpty(registro.DPI) Or String.IsNullOrWhiteSpace(registro.DPI) Then
                        Dialogo.ShowMessageAsync(Me, "Error :(", "El campo DPI no puede ir vacio")
                    ElseIf String.IsNullOrEmpty(registro.FirstName) Or String.IsNullOrWhiteSpace(registro.FirstName) Then
                        Dialogo.ShowMessageAsync(Me, "Error :(", "El campo FirstName no puede ir vacio")
                    ElseIf String.IsNullOrEmpty(registro.LastName) Or String.IsNullOrWhiteSpace(registro.LastName) Then
                        Dialogo.ShowMessageAsync(Me, "Error :(", "El campo LastName no puede ir vacio")
                    ElseIf String.IsNullOrEmpty(registro.Address) Or String.IsNullOrWhiteSpace(registro.Address) Then
                        Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Address no puede ir vacio")
                    ElseIf String.IsNullOrEmpty(registro.Municipality) Or String.IsNullOrWhiteSpace(registro.Municipality) Then
                        Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Municipality no puede ir vacio")
                    ElseIf String.IsNullOrEmpty(registro.PostalCode) Or String.IsNullOrWhiteSpace(registro.PostalCode) Then
                        Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Postal Code no puede ir vacio")
                    Else
                        For Each x In ListNeighbor
                            If x.Nit = Nit Then
                                Dialogo.ShowMessageAsync(Me, "Error :(", "El Nit ya existe")
                                Exit Sub
                            ElseIf x.DPI = DPI Then
                                Dialogo.ShowMessageAsync(Me, "Error :(", "El DPI ya existe")
                            End If
                        Next
                        DB.Neighbors.Add(registro)
                        DB.SaveChanges()
                        ListNeighbor = (From N In DB.Neighbors Select N).ToList
                        Dialogo.ShowMessageAsync(Me, "Exito :) ", "Registro agregado exitosamente")
                        Me.BtnNew = True
                        Me.BtnSave = False
                        Me.BtnDelete = True
                        Me.BtnUpdate = True
                        Me.BtnCancel = False
                        Me.TextBox = False
                    End If
                Else
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El PostalCode solo acepta numeros enteros")
                End If
            ElseIf _Validar.Equals("Modificar") Then
                    Element.Nit = Nit
                Element.DPI = DPI
                Element.FirstName = FirstName
                Element.LastName = LastName
                Element.Address = Address
                Element.Municipality = Municipality
                Element.PostalCode = PostalCode
                Element.Telephone = Telephone
                If Element.Telephone.Contains("_") Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El numero no puede llevar espacios en blanco")
                ElseIf String.IsNullOrEmpty(Element.Nit) Or String.IsNullOrWhiteSpace(Element.Nit) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Nit no puede ir vacio")
                ElseIf String.IsNullOrEmpty(Element.DPI) Or String.IsNullOrWhiteSpace(Element.DPI) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo DPI no puede ir vacio")
                ElseIf String.IsNullOrEmpty(Element.FirstName) Or String.IsNullOrWhiteSpace(Element.FirstName) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo FirstName no puede ir vacio")
                ElseIf String.IsNullOrEmpty(Element.LastName) Or String.IsNullOrWhiteSpace(Element.LastName) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo LastName no puede ir vacio")
                ElseIf String.IsNullOrEmpty(Element.Address) Or String.IsNullOrWhiteSpace(Element.Address) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Address no puede ir vacio")
                ElseIf String.IsNullOrEmpty(Element.Municipality) Or String.IsNullOrWhiteSpace(Element.Municipality) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Municipality no puede ir vacio")
                ElseIf String.IsNullOrEmpty(Element.PostalCode) Or String.IsNullOrWhiteSpace(Element.PostalCode) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Postal Code no puede ir vacio")
                Else
                    For Each x In ListNeighbor
                        If x.Nit = Nit Then
                            Dialogo.ShowMessageAsync(Me, "Error :(", "El Nit no puede ser moficado")
                            Exit Sub
                        End If
                    Next
                    DB.SaveChanges()
                    ListNeighbor = (From N In DB.Neighbors Select N).ToList
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
                        DB.Neighbors.Remove(Element)
                        DB.SaveChanges()
                        Dialogo.ShowMessageAsync(Me, "Exito", "Registro eliminado exitosamente")
                        ListNeighbor = (From n In DB.Neighbors Select n).ToList
                        Me.Nit = Nothing
                        Me.DPI = Nothing
                        Me.FirstName = Nothing
                        Me.LastName = Nothing
                        Me.Address = Nothing
                        Me.Municipality = Nothing
                        Me.PostalCode = Me.PostalCode
                        Me.Telephone = Me.Telephone
                        Me.Nit = Nothing
                        Me.DPI = Nothing
                        Me.FirstName = Nothing
                        Me.LastName = Nothing
                        Me.Address = Nothing
                        Me.Municipality = Nothing
                        Me.PostalCode = Nothing
                        Me.Telephone = Nothing
                    ElseIf result = MsgBoxResult.No Then
                    End If
                End If
            Catch ex As Exception
                Dialogo.ShowMessageAsync(Me, "Error :(", "Debe eliminar antes el Vehicle que pertene al Neighbor")
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
