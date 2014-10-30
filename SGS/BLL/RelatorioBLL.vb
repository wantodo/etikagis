Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class RelatorioBLL
    Public Function RelatorioQuestao(cd_empresa As Integer) As DataSet
        Dim obj As New DAL.RelatorioDAL

        Return obj.RelatorioQuestao(cd_empresa)
    End Function

    Public Function RelatorioQuestaoItem(cd_questionario As Integer) As DataSet
        Dim obj As New DAL.RelatorioDAL

        Return obj.RelatorioQuestaoItem(cd_questionario)
    End Function
End Class
