Imports MahApps.Metro.Controls.Dialogs
Imports MahApps.Metro.Controls
Imports System.ComponentModel
Imports EmetraApp
Imports EmetraApp.CristianHernandez.EmetraApp2017065.Model

Public Class AgentModelView
    Implements INotifyPropertyChanged, ICommand, IDataErrorInfo
    Dim Dialogo As DialogCoordinator
#Region "Campos"
    Private _Validar As String

    Private _AgentID As Integer
    Private _AgentNumber As String
    Private _AgentName As String
    Private _AgentLastName As String
    Private _Charge As String
    Private _Salary As Decimal
    Private _Commission As Decimal

    Private _Model As AgentModelView
    Private _Element As Agent
    Private _ListAgent As New List(Of Agent)

    Private _BtnNew As Boolean = True
    Private _BtnSave As Boolean = False
    Private _BtnDelete As Boolean = True
    Private _BtnUpdate As Boolean = True
    Private _BtnCancel As Boolean = False
    Private _TextBox As Boolean = False

    Private DB As New EmetraApp2017065DataContext
#End Region
#Region "Propiedades"
    Public Property AgentID As Integer
        Get
            Return _AgentID
        End Get
        Set(value As Integer)
            _AgentID = value
            NotificarCambio("AgentID")
        End Set
    End Property

    Public Property AgentNumber As String
        Get
            Return _AgentNumber
        End Get
        Set(value As String)
            _AgentNumber = value
            NotificarCambio("AgentNumber")
        End Set
    End Property

    Public Property AgentName As String
        Get
            Return _AgentName
        End Get
        Set(value As String)
            _AgentName = value
            NotificarCambio("AgentName")
        End Set
    End Property

    Public Property AgentLastName As String
        Get
            Return _AgentLastName
        End Get
        Set(value As String)
            _AgentLastName = value
            NotificarCambio("AgentLastName")
        End Set
    End Property

    Public Property Charge As String
        Get
            Return _Charge
        End Get
        Set(value As String)
            _Charge = value
            NotificarCambio("Charge")
        End Set
    End Property

    Public Property Salary As Decimal
        Get
            Return _Salary
        End Get
        Set(value As Decimal)
            _Salary = value
            NotificarCambio("Salary")
        End Set
    End Property

    Public Property Commission As Decimal
        Get
            Return _Commission
        End Get
        Set(value As Decimal)
            _Commission = value
            NotificarCambio("Comission")
        End Set
    End Property

    Public Property Model As AgentModelView
        Get
            Return _Model
        End Get
        Set(value As AgentModelView)
            _Model = value
        End Set
    End Property

    Public Property Element As Agent
        Get
            Return _Element
        End Get
        Set(value As Agent)
            _Element = value
            NotificarCambio("Element")
            If value IsNot Nothing Then
                Me.AgentID = _Element.AgentID
                Me.AgentNumber = _Element.AgentNumber
                Me.AgentName = _Element.AgentName
                Me.AgentLastName = _Element.AgentLastName
                Me.Salary = _Element.Salary
                Me.Charge = _Element.Charge
                Me.Commission = _Element.Commission
            End If
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
            Me.AgentID = Me.AgentID + 1
            Me.AgentNumber = Nothing
            Me.AgentName = Nothing
            Me.AgentLastName = Nothing
            Me.Salary = Nothing
            Me.Charge = Nothing
            Me.Commission = Nothing
            _Validar = "Agregar"
        ElseIf parametro.Equals("Save") Then
            If _Validar.Equals("Agregar") Then
                Dim registro As New Agent
                registro.AgentID = Me.AgentID
                registro.AgentNumber = Me.AgentNumber
                registro.AgentName = Me.AgentName
                registro.AgentLastName = Me.AgentLastName
                registro.Salary = Me.Salary
                registro.Charge = Me.Charge
                registro.Commission = Me.Commission
                If IsNumeric(registro.AgentNumber) Then
                    If String.IsNullOrEmpty(registro.AgentNumber) Or String.IsNullOrWhiteSpace(registro.AgentNumber) Then
                        Dialogo.ShowMessageAsync(Me, "Error :(", "El campo AgentNumber no puede ir vacio")
                    ElseIf String.IsNullOrEmpty(registro.AgentName) Or String.IsNullOrWhiteSpace(registro.AgentName) Then
                        Dialogo.ShowMessageAsync(Me, "Error :(", "El campo AgentName no puede ir vacio")
                    ElseIf String.IsNullOrEmpty(registro.AgentLastName) Or String.IsNullOrWhiteSpace(registro.AgentLastName) Then
                        Dialogo.ShowMessageAsync(Me, "Error :(", "El campo AgentLastName no puede ir vacio")
                    ElseIf String.IsNullOrEmpty(registro.Salary) Or String.IsNullOrWhiteSpace(registro.Salary) Then
                        Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Salary no puede ir vacio")
                    ElseIf String.IsNullOrEmpty(registro.Charge) Or String.IsNullOrWhiteSpace(registro.Charge) Then
                        Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Change no puede ir vacio")
                    ElseIf String.IsNullOrEmpty(registro.Commission) Or String.IsNullOrWhiteSpace(registro.Commission) Then
                        Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Commission no puede ir vacio")
                    Else
                        DB.Agents.Add(registro)
                        DB.SaveChanges()
                        ListAgent = (From N In DB.Agents Select N).ToList
                        Dialogo.ShowMessageAsync(Me, "Exito :) ", "Registro agregado exitosamente")
                        Me.BtnNew = True
                        Me.BtnSave = False
                        Me.BtnDelete = True
                        Me.BtnUpdate = True
                        Me.BtnCancel = False
                        Me.TextBox = False
                    End If
                Else
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo AgentNumber  solo acepta numeros enteros")
                End If
            ElseIf _Validar.Equals("Modificar") Then
                    Element.AgentID = AgentID
                Element.AgentNumber = AgentNumber
                Element.AgentName = AgentName
                Element.AgentLastName = AgentLastName
                Element.Salary = Salary
                Element.Charge = Charge
                Element.Commission = Commission
                If String.IsNullOrEmpty(Element.AgentNumber) Or String.IsNullOrWhiteSpace(Element.AgentNumber) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo AgentNumber no puede ir vacio")
                ElseIf String.IsNullOrEmpty(Element.AgentName) Or String.IsNullOrWhiteSpace(Element.AgentName) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo AgentName no puede ir vacio")
                ElseIf String.IsNullOrEmpty(Element.AgentLastName) Or String.IsNullOrWhiteSpace(Element.AgentLastName) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo AgentLastName no puede ir vacio")
                ElseIf String.IsNullOrEmpty(Element.Salary) Or String.IsNullOrWhiteSpace(Element.Salary) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Salary no puede ir vacio")
                ElseIf String.IsNullOrEmpty(Element.Charge) Or String.IsNullOrWhiteSpace(Element.Charge) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Change no puede ir vacio")
                ElseIf String.IsNullOrEmpty(Element.Commission) Or String.IsNullOrWhiteSpace(Element.Commission) Then
                    Dialogo.ShowMessageAsync(Me, "Error :(", "El campo Commission no puede ir vacio")
                Else
                    DB.SaveChanges()
                    ListAgent = (From N In DB.Agents Select N).ToList
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
                        DB.Agents.Remove(Element)
                        DB.SaveChanges()
                    Dialogo.ShowMessageAsync(Me, "Exito", "Elemento eliminado exitosamente")
                    ListAgent = (From n In DB.Agents Select n).ToList
                    Me.AgentID = Nothing
                    Me.AgentNumber = Nothing
                    Me.AgentName = Nothing
                        Me.AgentLastName = Nothing
                        Me.Salary = Nothing
                        Me.Charge = Nothing
                        Me.Commission = Nothing
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
            If _Validar = "Agregar" Then
                Me.AgentID = Me.AgentID - 1
            Else _Validar = "Modificar"
                Me.AgentID = Me.AgentID
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
