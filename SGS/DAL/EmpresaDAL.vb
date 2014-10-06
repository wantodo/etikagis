Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class EmpresaDAL
    Public Function ListaEmpresa() As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)

            Return dal.GetDataSet("st_sgs_empresa_s", CommandType.StoredProcedure)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RetornaStatusEmpresa() As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@tipo", SqlDbType.VarChar, "Empresa")}

            Return dal.GetDataSet("st_sgs_status_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub InsereEmpresa(objEmpresa As MODEL.Empresa)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@dc_razao_social", SqlDbType.VarChar, objEmpresa.dc_razao_social), _
                     dal.CriarParametro("@dc_nome_fantasia", SqlDbType.VarChar, objEmpresa.dc_nome_fantasia), _
                     dal.CriarParametro("@nm_cnpj", SqlDbType.VarChar, objEmpresa.nm_cnpj), _
                     dal.CriarParametro("@nm_inscricao_estadual", SqlDbType.VarChar, objEmpresa.nm_inscricao_estadual), _
                     dal.CriarParametro("@nm_inscricao_municipal", SqlDbType.VarChar, objEmpresa.nm_inscricao_municipal), _
                     dal.CriarParametro("@dc_email", SqlDbType.VarChar, objEmpresa.dc_email), _
                     dal.CriarParametro("@nome_cobranca", SqlDbType.VarChar, objEmpresa.nome_cobranca), _
                     dal.CriarParametro("@departamento", SqlDbType.VarChar, objEmpresa.departamento), _
                     dal.CriarParametro("@nm_cep_cobranca", SqlDbType.VarChar, objEmpresa.nm_cep_cobranca), _
                     dal.CriarParametro("@dc_endereco_cobranca", SqlDbType.VarChar, objEmpresa.dc_endereco_cobranca), _
                     dal.CriarParametro("@dc_cidade_cobranca", SqlDbType.VarChar, objEmpresa.dc_cidade_cobranca), _
                     dal.CriarParametro("@dc_uf_cobranca", SqlDbType.VarChar, objEmpresa.dc_uf_cobranca), _
                     dal.CriarParametro("@nm_telefone", SqlDbType.VarChar, objEmpresa.nm_telefone), _
                     dal.CriarParametro("@cd_status", SqlDbType.Int, objEmpresa.cd_status), _
                     dal.CriarParametro("@no_userid_cadastro", SqlDbType.VarChar, objEmpresa.no_userid)}

            dal.ExecuteNonQuery("st_sgs_empresa_i", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ALteraEmpresa(objEmpresa As MODEL.Empresa)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_empresa", SqlDbType.Int, objEmpresa.cd_empresa), _
                     dal.CriarParametro("@dc_razao_social", SqlDbType.VarChar, objEmpresa.dc_razao_social), _
                     dal.CriarParametro("@dc_nome_fantasia", SqlDbType.VarChar, objEmpresa.dc_nome_fantasia), _
                     dal.CriarParametro("@nm_cnpj", SqlDbType.VarChar, objEmpresa.nm_cnpj), _
                     dal.CriarParametro("@nm_inscricao_estadual", SqlDbType.VarChar, objEmpresa.nm_inscricao_estadual), _
                     dal.CriarParametro("@nm_inscricao_municipal", SqlDbType.VarChar, objEmpresa.nm_inscricao_municipal), _
                     dal.CriarParametro("@dc_email", SqlDbType.VarChar, objEmpresa.dc_email), _
                     dal.CriarParametro("@nome_cobranca", SqlDbType.VarChar, objEmpresa.nome_cobranca), _
                     dal.CriarParametro("@departamento", SqlDbType.VarChar, objEmpresa.departamento), _
                     dal.CriarParametro("@nm_cep_cobranca", SqlDbType.VarChar, objEmpresa.nm_cep_cobranca), _
                     dal.CriarParametro("@dc_endereco_cobranca", SqlDbType.VarChar, objEmpresa.dc_endereco_cobranca), _
                     dal.CriarParametro("@dc_cidade_cobranca", SqlDbType.VarChar, objEmpresa.dc_cidade_cobranca), _
                     dal.CriarParametro("@dc_uf_cobranca", SqlDbType.VarChar, objEmpresa.dc_uf_cobranca), _
                     dal.CriarParametro("@nm_telefone", SqlDbType.VarChar, objEmpresa.nm_telefone), _
                     dal.CriarParametro("@cd_status", SqlDbType.VarChar, objEmpresa.cd_status), _
                     dal.CriarParametro("@no_userid", SqlDbType.VarChar, objEmpresa.no_userid)}

            dal.ExecuteNonQuery("st_sgs_empresa_u", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ExcluirEmpresa(codEmpresa As Integer)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_empresa", SqlDbType.Int, codEmpresa)}

            dal.ExecuteNonQuery("st_sgs_empresa_d", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function FiltraEmpresa(filtro As String, valor As String) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@filtro", SqlDbType.VarChar, filtro), _
                     dal.CriarParametro("@valor", SqlDbType.VarChar, valor)}

            Return dal.GetDataSet("st_sgf_consulta_empresa_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
