Imports System.Data.Entity 'Para accesar al entity framework,hacer conexion,obtener datos.
Imports System.Data.SqlClient 'Ejecuta codigo de SQL Server
Imports System.Data.Entity.ModelConfiguration.Conventions 'Respeta convenciones de Entity Framework
Namespace CristianHernandez.EmetraApp2017065.Model
    Public Class EmetraApp2017065DataContext
        Inherits DbContext
        Public Property Agents() As DbSet(Of Agent)
        Public Property Neighbors() As DbSet(Of Neighbor)
        Public Property Remissions() As DbSet(Of Remission)
        Public Property SanctionTypes() As DbSet(Of SanctionType)
        Public Property Vehicles() As DbSet(Of Vehicle)

    End Class
End Namespace

