Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class AnaliseQuestaoBLL
    Public Function ListaAnaliseQuestao(cd_representante As Integer) As DataSet
        Dim obj As New DAL.AnaliseQuestaoDAL

        Return obj.ListaAnaliseQuestao(cd_representante)
    End Function
End Class
