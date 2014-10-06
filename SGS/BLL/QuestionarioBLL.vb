
Public Class QuestionarioBLL

    Public Function ListaQuestionario(codEmpresa As Integer, codCategoria As Integer, codRepresentante As Integer) As DataSet
        Dim obj As New DAL.QuestionarioDAL

        Return obj.ListaQuestionario(codEmpresa, codCategoria, codRepresentante)
    End Function

    Public Sub InsereQuestionario(objQuestionario As MODEL.Questionario)
        Dim obj As New DAL.QuestionarioDAL

        obj.InsereQuestionario(objQuestionario)
    End Sub

    Public Sub ExcluiQuestionario(idRepresentante As Integer)
        Dim obj As New DAL.QuestionarioDAL

        obj.ExcluiQuestionario(idRepresentante)
    End Sub

    Public Function RetornaStatusQuestionario() As DataSet
        Dim obj As New DAL.QuestionarioDAL

        Return obj.RetornaStatusQuestionario
    End Function
End Class
