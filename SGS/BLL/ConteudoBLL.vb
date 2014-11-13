Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class ConteudoBLL
    Public Function ListaConteudo(codEmpresa As Integer) As DataSet
        Dim obj As New DAL.ConteudoDAL

        Return obj.ListaConteudo(codEmpresa)
    End Function

    Public Sub AlteraConteudo(codEmpresa As Integer, descricao As String)
        Dim obj As New DAL.ConteudoDAL

        obj.AlteraConteudo(codEmpresa, descricao)
    End Sub
End Class
