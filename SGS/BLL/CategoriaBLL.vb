Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class CategoriaBLL
    Public Function ListaCategoria() As DataSet
        Dim obj As New DAL.CategoriaDAL

        Return obj.ListaCategoria
    End Function

    Public Function RetornaStatusCategoria() As DataSet
        Dim obj As New DAL.CategoriaDAL

        Return obj.RetornaStatusCategoria
    End Function

    Public Sub InsereCategoria(objCategoria As MODEL.CategoriaIndicador)
        Dim obj As New DAL.CategoriaDAL

        obj.InsereCategoria(objCategoria)
    End Sub

    Public Sub ALteraCategoria(objCategoria As MODEL.CategoriaIndicador)
        Dim obj As New DAL.CategoriaDAL

        obj.ALteraCategoria(objCategoria)
    End Sub

    Public Sub ExcluirCategoria(codCategoria As Integer)
        Dim obj As New DAL.CategoriaDAL

        obj.ExcluirCategoria(codCategoria)
    End Sub


End Class
