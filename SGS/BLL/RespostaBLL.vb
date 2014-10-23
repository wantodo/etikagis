Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class RespostaBLL

    Function InsereResposta(objResposta As MODEL.Resposta) As Boolean
        Dim obj As New DAL.RespostaDAL

        Return obj.InsereResposta(objResposta)
    End Function

    Function AlteraResposta(objResposta As MODEL.Resposta) As Boolean
        Dim obj As New DAL.RespostaDAL

        Return obj.AlteraResposta(objResposta)
    End Function

    Function RetornaResposta(codQuestionario As Integer) As DataSet
        Dim obj As New DAL.RespostaDAL

        Return obj.RetornaResposta(codQuestionario)
    End Function

    Function ListaItemResposta(codQuestionario As Integer) As DataSet
        Dim obj As New DAL.RespostaDAL

        Return obj.ListaItemResposta(codQuestionario)
    End Function

End Class
