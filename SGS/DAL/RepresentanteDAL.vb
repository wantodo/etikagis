Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class RepresentanteDAL

    Function RetornaRepresentante(Optional ByRef codRepresentante As Integer = 0, Optional ByVal codUsuario As Integer = 0) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)

            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_representante", SqlDbType.Int, codRepresentante), _
                     dal.CriarParametro("@cd_usuario", SqlDbType.Int, codUsuario)}

            Return dal.GetDataSet("st_sgs_representante_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function ListaRepresentante() As Object
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)

            Return dal.GetDataSet("st_sgs_representante_s", CommandType.StoredProcedure)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function RetornaStatusRepresentante() As Object
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@tipo", SqlDbType.VarChar, "Representante")}

            Return dal.GetDataSet("st_sgs_status_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub ExcluiRepresentante(codRepresentante As Integer)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_representante", SqlDbType.Int, codRepresentante)}

            dal.ExecuteNonQuery("st_sgs_representante_d", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub InsereRepresentante(objRepresentante As MODEL.Representante)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_empresa", SqlDbType.Int, objRepresentante.empresa.cd_empresa), _
                     dal.CriarParametro("@cd_perfil", SqlDbType.Int, objRepresentante.perfil.cd_perfil_representante), _
                     dal.CriarParametro("@no_representante", SqlDbType.VarChar, objRepresentante.no_representante), _
                     dal.CriarParametro("@dc_usuario", SqlDbType.VarChar, objRepresentante.dc_usuario), _
                     dal.CriarParametro("@dc_senha", SqlDbType.VarChar, objRepresentante.dc_senha), _
                     dal.CriarParametro("@dc_cargo", SqlDbType.VarChar, objRepresentante.dc_cargo), _
                     dal.CriarParametro("@dc_area", SqlDbType.VarChar, objRepresentante.dc_area), _
                     dal.CriarParametro("@nm_telefone", SqlDbType.VarChar, objRepresentante.nm_telefone), _
                     dal.CriarParametro("@dc_email", SqlDbType.VarChar, objRepresentante.dc_email), _
                     dal.CriarParametro("@cd_status", SqlDbType.Int, objRepresentante.cd_status), _
                     dal.CriarParametro("@no_userid_cadastro", SqlDbType.VarChar, objRepresentante.no_userid)}

            dal.ExecuteNonQuery("st_sgs_representante_i", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub AlteraRepresentante(objRepresentante As MODEL.Representante)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_representante", SqlDbType.Int, objRepresentante.cd_representante), _
                     dal.CriarParametro("@cd_empresa", SqlDbType.Int, objRepresentante.empresa.cd_empresa), _
                     dal.CriarParametro("@cd_perfil", SqlDbType.Int, objRepresentante.perfil.cd_perfil_representante), _
                     dal.CriarParametro("@no_representante", SqlDbType.VarChar, objRepresentante.no_representante), _
                     dal.CriarParametro("@dc_usuario", SqlDbType.VarChar, objRepresentante.dc_usuario), _
                     dal.CriarParametro("@dc_senha", SqlDbType.VarChar, objRepresentante.dc_senha), _
                     dal.CriarParametro("@dc_cargo", SqlDbType.VarChar, objRepresentante.dc_cargo), _
                     dal.CriarParametro("@dc_area", SqlDbType.VarChar, objRepresentante.dc_area), _
                     dal.CriarParametro("@nm_telefone", SqlDbType.VarChar, objRepresentante.nm_telefone), _
                     dal.CriarParametro("@dc_email", SqlDbType.VarChar, objRepresentante.dc_email), _
                     dal.CriarParametro("@cd_status", SqlDbType.Int, objRepresentante.cd_status), _
                     dal.CriarParametro("@no_userid", SqlDbType.VarChar, objRepresentante.no_userid)}

            dal.ExecuteNonQuery("st_sgs_representante_u", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function ListaArea(cd_empresa As Integer) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_empresa", SqlDbType.Int, cd_empresa)}

            Return dal.GetDataSet("st_sgs_area_representante_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
