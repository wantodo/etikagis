Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class QuestaoDAL
    Public Function RetornaStatusQuestao() As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@tipo", SqlDbType.VarChar, "Questao")}

            Return dal.GetDataSet("st_sgs_status_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RetornaSubcategoria(cd_categoria As Integer) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_categoria", SqlDbType.VarChar, cd_categoria)}

            Return dal.GetDataSet("st_sgs_subcategoria_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RetornaAspecto(cd_categoria As Integer) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_categoria", SqlDbType.VarChar, cd_categoria)}

            Return dal.GetDataSet("st_sgs_aspecto_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RetornaQuestao(codQuestao As Integer) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_questao", SqlDbType.Int, codQuestao)}

            Return dal.GetDataSet("st_sgs_questao_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListaQuestao(codCategoria As Integer) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_categoria", SqlDbType.Int, codCategoria)}

            Return dal.GetDataSet("st_sgs_questao_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub InsereQuestao(questao As MODEL.Questao)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_categoria", SqlDbType.Int, questao.categoria.cd_categoria), _
                     dal.CriarParametro("@cd_indicador", SqlDbType.Int, questao.indicador.cd_indicador), _
                     dal.CriarParametro("@dc_questao", SqlDbType.VarChar, questao.dc_questao), _
                     dal.CriarParametro("@cd_empresa", SqlDbType.Int, questao.empresa.cd_empresa), _
                     dal.CriarParametro("@cd_status", SqlDbType.Int, questao.cd_status), _
                     dal.CriarParametro("@xx_tipo", SqlDbType.Char, questao.xx_tipo), _
                     dal.CriarParametro("@no_userid", SqlDbType.VarChar, questao.no_userid)}

            dal.GetDataSet("st_sgs_questao_i", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub AlteraQuestao(questao As MODEL.Questao)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_questao", SqlDbType.Int, questao.cd_questao), _
                     dal.CriarParametro("@cd_categoria", SqlDbType.Int, questao.categoria.cd_categoria), _
                     dal.CriarParametro("@cd_indicador", SqlDbType.Int, questao.indicador.cd_indicador), _
                     dal.CriarParametro("@dc_questao", SqlDbType.VarChar, questao.dc_questao), _
                     dal.CriarParametro("@cd_empresa", SqlDbType.Int, questao.empresa.cd_empresa), _
                     dal.CriarParametro("@cd_status", SqlDbType.Int, questao.cd_status), _
                     dal.CriarParametro("@xx_tipo", SqlDbType.Char, questao.xx_tipo), _
                     dal.CriarParametro("@no_userid", SqlDbType.VarChar, questao.no_userid)}

            dal.GetDataSet("st_sgs_questao_u", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub InsereItemQuestao(itemQuestao As MODEL.ItemQuestao)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_questao", SqlDbType.Int, itemQuestao.questao.cd_questao), _
                     dal.CriarParametro("@dc_item_questao", SqlDbType.VarChar, itemQuestao.dc_item_questao), _
                     dal.CriarParametro("@no_userid", SqlDbType.VarChar, itemQuestao.no_userid)}

            dal.GetDataSet("st_sgs_item_questao_i", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub AlteraItemQuestao(itemQuestao As MODEL.ItemQuestao)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_item_questao", SqlDbType.Int, itemQuestao.cd_item_questao), _
                     dal.CriarParametro("@dc_item_questao", SqlDbType.VarChar, itemQuestao.dc_item_questao), _
                     dal.CriarParametro("@no_userid", SqlDbType.VarChar, itemQuestao.no_userid)}

            dal.GetDataSet("st_sgs_item_questao_u", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ExcluirItemQuestao(codItem As Integer)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_item_questao", SqlDbType.Int, codItem)}

            dal.ExecuteNonQuery("st_sgs_item_questao_d", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ExcluirQuestao(codQuestao As Integer)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_questao", SqlDbType.Int, codQuestao)}

            dal.ExecuteNonQuery("st_sgs_questao_d", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
