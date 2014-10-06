Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class AspectoBLL
    Public Function ListaAspecto()
        Dim obj As New DAL.AspectoDAL

        Return obj.ListaAspecto
    End Function

    Public Function RetornaStatusAspecto() As DataSet
        Dim obj As New DAL.AspectoDAL

        Return obj.RetornaStatusAspecto
    End Function

    Public Sub ExcluirAspecto(codAspecto As Integer)
        Dim obj As New DAL.AspectoDAL

        obj.ExcluirAspecto(codAspecto)
    End Sub

    Public Sub InsereAspecto(objAspecto As MODEL.Aspecto)
        Dim obj As New DAL.AspectoDAL

        obj.InsereAspecto(objAspecto)
    End Sub

    Public Sub ALteraAspecto(objAspecto As MODEL.Aspecto)
        Dim obj As New DAL.AspectoDAL

        obj.ALteraAspecto(objAspecto)
    End Sub
End Class
