Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class QuestaoBLL
    Public Function RetornaStatusQuestao() As DataSet
        Dim obj As New DAL.QuestaoDAL

        Return obj.RetornaStatusQuestao
    End Function

    Public Function RetornaSubcategoria(cd_categoria As Integer) As DataSet
        Dim obj As New DAL.QuestaoDAL

        Return obj.RetornaSubcategoria(cd_categoria)
    End Function

    Public Function ListaQuestao(codCategoria As Integer) As DataSet
        Dim obj As New DAL.QuestaoDAL

        Return obj.ListaQuestao(codCategoria)
    End Function

    Public Function RetornaAspecto(cd_categoria As Integer) As DataSet
        Dim obj As New DAL.QuestaoDAL

        Return obj.RetornaAspecto(cd_categoria)
    End Function

    Public Function RetornaQuestao(codQuestao As Integer) As DataSet
        Dim obj As New DAL.QuestaoDAL

        Return obj.RetornaQuestao(codQuestao)
    End Function

    Public Sub InsereQuestao(questao As MODEL.Questao)
        Dim obj As New DAL.QuestaoDAL

        obj.InsereQuestao(questao)
    End Sub

    Public Sub AlteraQuestao(questao As MODEL.Questao)
        Dim obj As New DAL.QuestaoDAL

        obj.AlteraQuestao(questao)
    End Sub

    Public Sub InsereItemQuestao(itemQuestao As MODEL.ItemQuestao)
        Dim obj As New DAL.QuestaoDAL

        obj.InsereItemQuestao(itemQuestao)
    End Sub

    Public Function ListaItemQuestao(codQuestao As Integer) As DataSet
        Dim obj As New DAL.QuestaoDAL

        Return obj.ListaItemQuestao(codQuestao)
    End Function

    Public Sub AlteraItemQuestao(itemQuestao As MODEL.ItemQuestao)
        Dim obj As New DAL.QuestaoDAL

        obj.AlteraItemQuestao(itemQuestao)
    End Sub

    Public Sub ExcluirItemQuestao(codItem As Integer)
        Dim obj As New DAL.QuestaoDAL

        obj.ExcluirItemQuestao(codItem)
    End Sub

    Public Sub ExcluirQuestao(codQuestao As Integer)
        Dim obj As New DAL.QuestaoDAL

        obj.ExcluirQuestao(codQuestao)
    End Sub
End Class
