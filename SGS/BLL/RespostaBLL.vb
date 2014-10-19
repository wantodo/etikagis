Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class RespostaBLL
    Public Function ListaResposta(resposta As MODEL.Resposta) As DataSet
        Dim obj As New DAL.RespostaDAL

        Return obj.ListaResposta(resposta)
    End Function
End Class
