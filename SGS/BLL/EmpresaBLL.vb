Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class EmpresaBLL
    Public Function ListaEmpresa() As DataSet
        Dim obj As New DAL.EmpresaDAL

        Return obj.ListaEmpresa
    End Function

    Public Function RetornaStatusEmpresa() As DataSet
        Dim obj As New DAL.EmpresaDAL

        Return obj.RetornaStatusEmpresa
    End Function

    Public Sub InsereEmpresa(objEmpresa As MODEL.Empresa)
        Dim obj As New DAL.EmpresaDAL

        obj.InsereEmpresa(objEmpresa)
    End Sub

    Public Sub ALteraEmpresa(objEmpresa As MODEL.Empresa)
        Dim obj As New DAL.EmpresaDAL

        obj.ALteraEmpresa(objEmpresa)
    End Sub

    Public Sub ExcluirEmpresa(codEmpresa As Integer)
        Dim obj As New DAL.EmpresaDAL

        obj.ExcluirEmpresa(codEmpresa)
    End Sub

    Public Function FiltraEmpresa(filtro As String, valor As String) As DataSet
        Dim obj As New DAL.EmpresaDAL

        Return obj.FiltraEmpresa(filtro, valor)
    End Function
End Class
