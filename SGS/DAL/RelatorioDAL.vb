Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class RelatorioDAL
    Public Function RelatorioQuestao(cd_empresa As Integer, dt_competencia As String) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_empresa", SqlDbType.Int, cd_empresa), _
                     dal.CriarParametro("@dt_competencia", SqlDbType.VarChar, dt_competencia)}

            Return dal.GetDataSet("st_sgs_relatorio_questao_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RelatorioQuestaoItem(cd_questionario As Integer) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_questionario", SqlDbType.Int, cd_questionario)}

            Return dal.GetDataSet("st_sgs_relatorio_questao_item_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
