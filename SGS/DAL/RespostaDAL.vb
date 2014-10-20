Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class RespostaDAL
    Public Function ListaResposta(resposta As MODEL.Resposta) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_acesso", SqlDbType.Int, resposta.cd_acesso), _
                     dal.CriarParametro("@cd_usuario", SqlDbType.Int, resposta.cd_usuario), _
                     dal.CriarParametro("@cd_empresa", SqlDbType.Int, resposta.cd_empresa)}

            Return dal.GetDataSet("st_sgs_resposta_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
