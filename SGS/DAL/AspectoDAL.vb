Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class AspectoDAL
    Public Function ListaAspecto()
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)

            Return dal.GetDataSet("st_sgs_aspecto_s", CommandType.StoredProcedure)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RetornaStatusAspecto() As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@tipo", SqlDbType.VarChar, "Aspecto")}

            Return dal.GetDataSet("st_sgs_status_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ExcluirAspecto(codAspecto As Integer)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_aspecto", SqlDbType.Int, codAspecto)}

            dal.ExecuteNonQuery("st_sgs_aspecto_d", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub InsereAspecto(objAspecto As MODEL.Aspecto)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@dc_aspecto", SqlDbType.VarChar, objAspecto.dc_aspecto), _
                     dal.CriarParametro("@cd_categoria", SqlDbType.Int, objAspecto.cd_categoria), _
                     dal.CriarParametro("@cd_subcategoria", SqlDbType.Int, objAspecto.subcategoria.cd_subcategoria), _
                     dal.CriarParametro("@cd_status", SqlDbType.Int, objAspecto.cd_status), _
                     dal.CriarParametro("@no_userid", SqlDbType.VarChar, objAspecto.no_userid)}

            dal.ExecuteNonQuery("st_sgs_aspecto_i", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ALteraAspecto(objAspecto As MODEL.Aspecto)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_aspecto", SqlDbType.Int, objAspecto.cd_aspecto), _
                     dal.CriarParametro("@dc_aspecto", SqlDbType.VarChar, objAspecto.dc_aspecto), _
                     dal.CriarParametro("@cd_categoria", SqlDbType.Int, objAspecto.cd_categoria), _
                     dal.CriarParametro("@cd_subcategoria", SqlDbType.Int, objAspecto.subcategoria.cd_subcategoria), _
                     dal.CriarParametro("@cd_status", SqlDbType.Int, objAspecto.cd_status), _
                     dal.CriarParametro("@no_userid", SqlDbType.VarChar, objAspecto.no_userid)}

            dal.ExecuteNonQuery("st_sgs_aspecto_u", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
