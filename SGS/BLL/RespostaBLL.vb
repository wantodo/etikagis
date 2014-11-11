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

    Function ListaItemRespondido(codQuestionario As Integer) As DataSet
        Dim obj As New DAL.RespostaDAL

        Return obj.ListaItemRespondido(codQuestionario)
    End Function

    Function InsereItemResposta(objItemResposta As MODEL.ItemResposta) As Boolean
        Dim obj As New DAL.RespostaDAL

        Return obj.InsereItemResposta(objItemResposta)
    End Function

    Function AlteraItemResposta(objItemResposta As MODEL.ItemResposta) As Boolean
        Dim obj As New DAL.RespostaDAL

        Return obj.AlteraItemResposta(objItemResposta)
    End Function

    Sub ExcluirItemQuestao(codItem As Integer)
        Dim obj As New DAL.RespostaDAL

        obj.ExcluirItemQuestao(codItem)
    End Sub

    Public Function EditaListaItem(codQuestionario As Integer, codGrupoItemResposta As Integer) As DataSet
        Dim obj As New DAL.RespostaDAL

        Return obj.EditaListaItem(codQuestionario, codGrupoItemResposta)
    End Function

End Class
