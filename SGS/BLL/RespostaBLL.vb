Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class RespostaBLL

    Sub InsereResposta(objResposta As MODEL.Resposta)
        Dim obj As New DAL.RespostaDAL

        obj.InsereResposta(objResposta)
    End Sub

    Sub AlteraResposta(objResposta As MODEL.Resposta)
        Dim obj As New DAL.RespostaDAL

        obj.AlteraResposta(objResposta)
    End Sub

    Function RetornaResposta(codQuestionario As Integer) As DataSet
        Dim obj As New DAL.RespostaDAL

        Return obj.RetornaResposta(codQuestionario)
    End Function

    Function ListaItemResposta(codQuestionario As Integer) As DataSet
        Dim obj As New DAL.RespostaDAL

        Return obj.ListaItemResposta(codQuestionario)
    End Function

End Class
