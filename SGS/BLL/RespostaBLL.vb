Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class RespostaBLL

    Sub InsereResposta(objResposta As MODEL.Resposta)
        Dim obj As New DAL.RespostaDAL

        obj.InsereResposta(objResposta)
    End Sub

    Function RetornaResposta(codQuestionario As Integer) As DataSet
        Dim obj As New DAL.RespostaDAL

        Return obj.RetornaResposta(codQuestionario)
    End Function

End Class
