Imports MahApps.Metro.Controls
Imports MahApps.Metro.Controls.Dialogs
Public Class NeighborView
    Inherits MetroWindow
    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Me.DataContext = New NeighborModelView(DialogCoordinator.Instance)
    End Sub
End Class
