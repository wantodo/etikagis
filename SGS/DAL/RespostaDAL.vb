Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class RespostaDAL

    Sub InsereResposta(objResposta As MODEL.Resposta)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_questionario", SqlDbType.Int, objResposta.questionario.cd_questionario), _
                     dal.CriarParametro("@cd_item", SqlDbType.Int, objResposta.item.cd_item_questao), _
                     dal.CriarParametro("@dc_resposta", SqlDbType.VarChar, objResposta.dc_resposta), _
                     dal.CriarParametro("@no_userid_cadastro", SqlDbType.VarChar, objResposta.no_userid)}

            dal.ExecuteNonQuery("st_sgs_resposta_i", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function RetornaResposta(codQuestionario As Integer) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_questionario", SqlDbType.Int, codQuestionario)}

            Return dal.GetDataSet("st_sgs_resposta_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
