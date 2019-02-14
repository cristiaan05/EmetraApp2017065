Imports MahApps.Metro.Controls.Dialogs
Imports MahApps.Metro.Controls
Imports System.ComponentModel
Imports EmetraApp
Imports EmetraApp.CristianHernandez.EmetraApp2017065.Model
Public Class SanctionTypeModelView
    Implements INotifyPropertyChanged, ICommand, IDataErrorInfo
    Dim Dialogo As DialogCoordinator

#Region "Campos"
    Private _Validar As String

    Private _RemissionID As Integer
    Private _Description As String
    Private _Import As Decimal
    Private _Recurrent As String

    Private _ListSanctionType As New List(Of SanctionType)
    Private _Model As SanctionTypeModelView
    Private _Element As SanctionType

    Private _BtnNew As Boolean = True
    Private _BtnSave As Boolean = False
    Private _BtnDelete As Boolean = True
    Private _BtnUpdate As Boolean = True
    Private _BtnCancel As Boolean = False
    Private _TextBox As Boolean = False

    Private DB As New EmetraApp2017065DataContext
#End Region
#Region "Propiedades"
    Public Property RemissionID As Integer
        Get
            Return _RemissionID
        End Get
        Set(value As Integer)
            _RemissionID = value
            NotificarCambio("RemissionID")
        End Set
    End Property

    Public Property Description As String
        Get
            Return _Description
        End Get
        Set(value As String)
            _Description = value
            NotificarCambio("Description")
        End Set
    End Property

    Public Property Import As Decimal
        Get
            Return _Import
        End Get
        Set(value As Decimal)
            _Import = value
            NotificarCambio("Import")
        End Set
    End Property

    Public Property Recurrent As String
        Get
            Return _Recurrent
        End Get
        Set(value As String)
            _Recurrent = value
            NotificarCambio("Recurrent")
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

    Public Property Model As SanctionTypeModelView
        Get
            Return _Model
        End Get
        Set(value As SanctionTypeModelView)
            _Model = value
        End Set
    End Property

    Public Property Element As SanctionType
        Get
            Return _Element
        End Get
        Set(value As SanctionType)
            _Element = value
            NotificarCambio("Element")
            If value IsNot Nothing Then
                Me.RemissionID = _Element.RemissionID
                Me.Description = _Element.Description
                Me.Import = _Element.Import
                Me.Recurrent = _Element.Recurrent
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
            Me.RemissionID = Me.RemissionID + 1
            Me.Description = Nothing
            Me.Import = Nothing
            Me.Recurrent = Nothing
            _Validar = "Agregar"
        ElseIf parametro.Equals("Save") Then
            If _Validar.Equals("Agregar") Then
                Dim registro As New SanctionType
                registro.RemissionID = Me.RemissionID
                registro.Description = Me.Description
                registro.Import = Me.Import
                registro.Recurrent = Me.Recurrent
                If String.IsNullOrEmpty(registro.Description) Or String.IsNullOrWhiteSpace(registro.Description) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Description no puede ir vacio")
                ElseIf String.IsNullOrEmpty(registro.Import) Or String.IsNullOrWhiteSpace(registro.Import) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Import no puede ir vacio")
                ElseIf String.IsNullOrEmpty(registro.Recurrent) Or String.IsNullOrWhiteSpace(registro.Recurrent) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Recurrent no puede ir vacio")
                Else
                    DB.SanctionTypes.Add(registro)
                    DB.SaveChanges()
                    ListSanctionType = (From N In DB.SanctionTypes Select N).ToList
                    Dialogo.ShowMessageAsync(Me, "Exito :) ", "Registro agregado exitosamente")
                    Me.BtnNew = True
                    Me.BtnSave = False
                    Me.BtnDelete = True
                    Me.BtnUpdate = True
                    Me.BtnCancel = False
                    Me.TextBox = False
                End If
            ElseIf _Validar.Equals("Modificar") Then
                Element.RemissionID = RemissionID
                Element.Description = Description
                Element.Import = Import
                Element.Recurrent = Recurrent
                If String.IsNullOrEmpty(Element.Description) Or String.IsNullOrWhiteSpace(Element.Description) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Description no puede ir vacio")
                ElseIf String.IsNullOrEmpty(Element.Import) Or String.IsNullOrWhiteSpace(Element.Import) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Import no puede ir vacio")
                ElseIf String.IsNullOrEmpty(Element.Recurrent) Or String.IsNullOrWhiteSpace(Element.Recurrent) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Recurrent no puede ir vacio")
                Else
                    DB.SaveChanges()
                    ListSanctionType = (From N In DB.SanctionTypes Select N).ToList
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
                    DB.SanctionTypes.Remove(Element)
                    DB.SaveChanges()
                    Dialogo.ShowMessageAsync(Me, "Exito :) ", "Se ha eliminado la Sanction y las Remission pertenecientes")
                    ListSanctionType = (From n In DB.SanctionTypes Select n).ToList
                    Me.RemissionID = Nothing
                    Me.Description = Nothing
                    Me.Import = Nothing
                    Me.Recurrent = Nothing
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
                Me.RemissionID = Me.RemissionID - 1
            Else _Validar = "Modificar"
                Me.RemissionID = Me.RemissionID
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
