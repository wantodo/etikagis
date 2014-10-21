Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class RespostaBLL
    Public Function ListaResposta(parametros As Array) As DataSet
        Dim obj As New DAL.RespostaDAL

        Return obj.ListaResposta(parametros)
    End Function

    Sub InsereResposta(objResposta As MODEL.Resposta)
        Dim obj As New DAL.RespostaDAL

        obj.InsereResposta(objResposta)
    End Sub

End Class
