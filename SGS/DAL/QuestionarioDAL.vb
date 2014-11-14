Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class QuestionarioDAL

    Sub InsereQuestionario(objQuestionario As MODEL.Questionario)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_questao", SqlDbType.Int, objQuestionario.questao.cd_questao), _
                     dal.CriarParametro("@cd_representante", SqlDbType.Int, objQuestionario.representante.cd_representante), _
                     dal.CriarParametro("@cd_status", SqlDbType.Int, objQuestionario.status.cd_status), _
                     dal.CriarParametro("@nm_ordem", SqlDbType.Int, objQuestionario.nm_ordem), _
                     dal.CriarParametro("@no_userid", SqlDbType.VarChar, objQuestionario.no_userid)}

            dal.ExecuteNonQuery("st_sgs_questionario_i", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ExcluiQuestionario(idRepresentante As Integer, idCategoria As Integer)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_representante", SqlDbType.Int, idRepresentante), _
                     dal.CriarParametro("@cd_categoria", SqlDbType.Int, idCategoria)}

            dal.ExecuteNonQuery("st_sgs_questionario_d", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function ListaQuestionario(codEmpresa As Integer, codCategoria As Integer, codRepresentante As Integer) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_empresa", SqlDbType.Int, codEmpresa), _
                     dal.CriarParametro("@cd_categoria", SqlDbType.Int, codCategoria), _
                     dal.CriarParametro("@cd_representante", SqlDbType.Int, codRepresentante)}

            Return dal.GetDataSet("st_sgs_questionario_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RetornaStatusQuestionario() As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@tipo", SqlDbType.VarChar, "Questão")}

            Return dal.GetDataSet("st_sgs_status_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RetornaPontoFocal(codEmpresa As Integer) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_empresa", SqlDbType.Int, codEmpresa)}

            Return dal.GetDataSet("st_sgs_ponto_focal_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub AlteraQuestionario(codQuestionario As Integer, codStatus As Integer, codRepresentante As Integer)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_status", SqlDbType.Int, codStatus), _
                     dal.CriarParametro("@cd_questionario", SqlDbType.Int, codQuestionario), _
                     dal.CriarParametro("@cd_representante", SqlDbType.Int, codRepresentante)}

            dal.GetDataSet("st_sgs_questionario_u", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function RetornaQuestionarioRepresentante(parametros As Array) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_perfil", SqlDbType.Int, parametros(0)), _
                     dal.CriarParametro("@cd_usuario", SqlDbType.Int, parametros(1)), _
                     dal.CriarParametro("@cd_empresa", SqlDbType.Int, parametros(2)), _
                     dal.CriarParametro("@cd_representante", SqlDbType.Int, parametros(3))}

            Return dal.GetDataSet("st_sgs_questionario_representante_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function RetornaStatusQuestionario(parametros As Array) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_empresa", SqlDbType.Int, parametros(0)), _
                     dal.CriarParametro("@cd_area", SqlDbType.Int, parametros(1)), _
                     dal.CriarParametro("@cd_indicador", SqlDbType.Int, parametros(2)), _
                     dal.CriarParametro("@cd_status", SqlDbType.Int, parametros(3))}

            Return dal.GetDataSet("st_sgs_relatorio_status_questionario_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub AlteraPrazo(cd_representante As Integer, Optional dt_prazo As Date = Nothing)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            If dt_prazo = Nothing Then
                param = {dal.CriarParametro("@cd_representante", SqlDbType.Int, cd_representante)}
            Else
                param = {dal.CriarParametro("@cd_representante", SqlDbType.Int, cd_representante), _
                     dal.CriarParametro("@dt_prazo", SqlDbType.DateTime, dt_prazo)}
            End If
            

            dal.GetDataSet("st_sgs_prazo_u", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
