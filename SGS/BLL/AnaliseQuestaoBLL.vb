Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class AnaliseQuestaoBLL
    Public Function ListaAnaliseQuestao(cd_representante As Integer) As DataSet
        Dim obj As New DAL.AnaliseQuestaoDAL

        Return obj.ListaAnaliseQuestao(cd_representante)
    End Function

    Public Sub AlteraAnaliseQuestao(cd_questionario As Integer, cd_status As Integer)
        Dim obj As New DAL.AnaliseQuestaoDAL

        obj.AlteraAnaliseQuestao(cd_questionario, cd_status)
    End Sub
End Class
