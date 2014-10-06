Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class StatusDAL

    Function ListaStatus() As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)

            Return dal.GetDataSet("st_sgs_status_s", CommandType.StoredProcedure)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub InsereStatus(objStatus As MODEL.Status)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@dc_status", SqlDbType.VarChar, objStatus.dc_status), _
                     dal.CriarParametro("@dc_tipo", SqlDbType.VarChar, objStatus.dc_tipo), _
                     dal.CriarParametro("@no_userid", SqlDbType.VarChar, objStatus.no_userid)}

            dal.ExecuteNonQuery("st_sgs_status_i", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub AlteraStatus(objStatus As MODEL.Status)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_status", SqlDbType.VarChar, objStatus.cd_status), _
                     dal.CriarParametro("@dc_status", SqlDbType.VarChar, objStatus.dc_status), _
                     dal.CriarParametro("@dc_tipo", SqlDbType.VarChar, objStatus.dc_tipo), _
                     dal.CriarParametro("@no_userid", SqlDbType.VarChar, objStatus.no_userid)}

            dal.ExecuteNonQuery("st_sgs_status_u", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ExcluiStatus(codStatus As Integer)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_status", SqlDbType.Int, codStatus)}

            dal.ExecuteNonQuery("st_sgs_status_d", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
