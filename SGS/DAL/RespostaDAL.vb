Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class RespostaDAL

    Function InsereResposta(objResposta As MODEL.Resposta) As Boolean
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_questionario", SqlDbType.Int, objResposta.questionario.cd_questionario), _
                     dal.CriarParametro("@dc_resposta", SqlDbType.VarChar, objResposta.dc_resposta), _
                     dal.CriarParametro("@no_userid_cadastro", SqlDbType.VarChar, objResposta.no_userid)}

            dal.ExecuteNonQuery("st_sgs_resposta_i", CommandType.StoredProcedure, param)

            InsereResposta = True

        Catch ex As Exception
            Throw ex

            InsereResposta = False
        End Try
    End Function

    Function AlteraResposta(objResposta As MODEL.Resposta) As Boolean
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_resposta", SqlDbType.Int, objResposta.cd_resposta), _
                     dal.CriarParametro("@dc_resposta", SqlDbType.VarChar, objResposta.dc_resposta), _
                     dal.CriarParametro("@no_userid", SqlDbType.VarChar, objResposta.no_userid)}

            dal.ExecuteNonQuery("st_sgs_resposta_u", CommandType.StoredProcedure, param)

            AlteraResposta = True

        Catch ex As Exception
            Throw ex

            AlteraResposta = False
        End Try
    End Function

    Public Function ListaItemRespondido(codQuestionario As Integer) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_questionario", SqlDbType.Int, codQuestionario)}

            Return dal.GetDataSet("st_sgs_item_respondido_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListaItemResposta(codQuestionario As Integer) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_questionario", SqlDbType.Int, codQuestionario)}

            Return dal.GetDataSet("st_sgs_relatorio_questao_item_s", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function EditaListaItem(codQuestionario As Integer, codGrupoItemResposta As Integer) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_questionario", SqlDbType.Int, codQuestionario), _
                     dal.CriarParametro("@cd_grupo_item_resposta", SqlDbType.Int, codGrupoItemResposta)}

            Return dal.GetDataSet("st_sgs_item_resposta_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

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

    Function InsereItemResposta(objItemResposta As MODEL.ItemResposta) As Boolean
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_grupo_item_resposta", SqlDbType.Int, objItemResposta.cd_grupo_item_resposta), _
                     dal.CriarParametro("@cd_questionario", SqlDbType.Int, objItemResposta.questionario.cd_questionario), _
                     dal.CriarParametro("@cd_item_questao", SqlDbType.VarChar, objItemResposta.itemQuestao.cd_item_questao), _
                     dal.CriarParametro("@dc_resposta_item", SqlDbType.VarChar, objItemResposta.dc_resposta_item), _
                     dal.CriarParametro("@no_userid", SqlDbType.VarChar, objItemResposta.no_userid)}

            dal.ExecuteNonQuery("st_sgs_item_resposta_i", CommandType.StoredProcedure, param)

            InsereItemResposta = True

        Catch ex As Exception
            Throw ex

            InsereItemResposta = False
        End Try
    End Function

    Function AlteraItemResposta(objItemResposta As MODEL.ItemResposta) As Boolean
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_item_resposta", SqlDbType.Int, objItemResposta.cd_item_resposta),
                     dal.CriarParametro("@cd_item_questao", SqlDbType.Int, objItemResposta.itemQuestao.cd_item_questao), _
                     dal.CriarParametro("@dc_resposta_item", SqlDbType.VarChar, objItemResposta.dc_resposta_item), _
                     dal.CriarParametro("@no_userid", SqlDbType.VarChar, objItemResposta.no_userid)}

            dal.ExecuteNonQuery("st_sgs_item_resposta_u", CommandType.StoredProcedure, param)

            AlteraItemResposta = True

        Catch ex As Exception
            Throw ex

            AlteraItemResposta = False
        End Try
    End Function

    Sub ExcluirItemQuestao(codItem As Integer)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_item_resposta", SqlDbType.Int, codItem)}

            dal.ExecuteNonQuery("st_sgs_item_resposta_d", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
