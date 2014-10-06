Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class UsuarioDAL

    Function RetornaStatusUsuario() As Object
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@tipo", SqlDbType.VarChar, "Usuario")}

            Return dal.GetDataSet("st_sgs_status_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function ListaUsuario() As Object
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)

            Return dal.GetDataSet("st_sgs_usuario_s", CommandType.StoredProcedure)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub ExcluiUsuario(codUsuario As Integer)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_usuario", SqlDbType.Int, codUsuario)}

            dal.ExecuteNonQuery("st_sgs_usuario_d", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub AlteraUsuario(objUsuario As MODEL.Usuario)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_usuario", SqlDbType.VarChar, objUsuario.codigo), _
                     dal.CriarParametro("@no_usuario", SqlDbType.VarChar, objUsuario.nomeUsuario), _
                     dal.CriarParametro("@no_senha", SqlDbType.VarChar, objUsuario.senha), _
                     dal.CriarParametro("@cd_status", SqlDbType.Int, objUsuario.status), _
                     dal.CriarParametro("@cd_acesso", SqlDbType.Int, objUsuario.acesso), _
                     dal.CriarParametro("@dc_email", SqlDbType.VarChar, objUsuario.email)}

            dal.ExecuteNonQuery("st_sgs_usuario_u", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub InsereUsuario(objUsuario As MODEL.Usuario)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@no_usuario", SqlDbType.VarChar, objUsuario.nomeUsuario), _
                     dal.CriarParametro("@no_senha", SqlDbType.VarChar, objUsuario.senha), _
                     dal.CriarParametro("@cd_status", SqlDbType.Int, objUsuario.status), _
                     dal.CriarParametro("@cd_acesso", SqlDbType.Int, objUsuario.acesso), _
                     dal.CriarParametro("@dc_email", SqlDbType.VarChar, objUsuario.email)}

            dal.ExecuteNonQuery("st_sgs_usuario_i", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
