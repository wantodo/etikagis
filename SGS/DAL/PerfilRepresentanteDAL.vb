Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class PerfilRepresentanteDAL

    Function RetornaStatusPerfil() As Object
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@tipo", SqlDbType.VarChar, "Perfil")}

            Return dal.GetDataSet("st_sgs_status_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub InserePerfil(objPerfil As MODEL.PerfilRepresentante)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@dc_perfil_representante", SqlDbType.VarChar, objPerfil.dc_perfil_representante), _
                     dal.CriarParametro("@cd_status", SqlDbType.Int, objPerfil.cd_status), _
                     dal.CriarParametro("@no_userid_cadastro", SqlDbType.VarChar, objPerfil.no_userid)}

            dal.ExecuteNonQuery("st_sgs_perfil_representante_i", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub AlteraPerfil(objPerfil As MODEL.PerfilRepresentante)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_perfil_representante", SqlDbType.Int, objPerfil.cd_perfil_representante), _
                     dal.CriarParametro("@dc_perfil_representante", SqlDbType.VarChar, objPerfil.dc_perfil_representante), _
                     dal.CriarParametro("@cd_status", SqlDbType.Int, objPerfil.cd_status), _
                     dal.CriarParametro("@no_userid", SqlDbType.VarChar, objPerfil.no_userid)}

            dal.ExecuteNonQuery("st_sgs_perfil_representante_u", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ExcluiPerfil(codPerfil As Integer)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_perfil_representante", SqlDbType.Int, codPerfil)}

            dal.ExecuteNonQuery("st_sgs_perfil_representante_d", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function ListaPerfilRepresentante() As Object
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)

            Return dal.GetDataSet("st_sgs_perfil_representante_s", CommandType.StoredProcedure)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
