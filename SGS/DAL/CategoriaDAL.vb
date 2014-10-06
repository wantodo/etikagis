Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class CategoriaDAL
    Public Function ListaCategoria() As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)

            Return dal.GetDataSet("st_sgs_categoria_indicador_s", CommandType.StoredProcedure)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RetornaStatusCategoria() As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@tipo", SqlDbType.VarChar, "Categoria")}

            Return dal.GetDataSet("st_sgs_status_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub InsereCategoria(objCategoria As MODEL.CategoriaIndicador)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@dc_categoria", SqlDbType.VarChar, objCategoria.dc_categoria), _
                     dal.CriarParametro("@cd_status", SqlDbType.Int, objCategoria.cd_status), _
                     dal.CriarParametro("@no_userid", SqlDbType.VarChar, objCategoria.no_userid)}

            dal.ExecuteNonQuery("st_sgs_categoria_indicador_i", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ALteraCategoria(objCategoria As MODEL.CategoriaIndicador)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_categoria", SqlDbType.Int, objCategoria.cd_categoria), _
                     dal.CriarParametro("@dc_categoria", SqlDbType.VarChar, objCategoria.dc_categoria), _
                     dal.CriarParametro("@cd_status", SqlDbType.Int, objCategoria.cd_status), _
                     dal.CriarParametro("@no_userid", SqlDbType.VarChar, objCategoria.no_userid)}

            dal.ExecuteNonQuery("st_sgs_categoria_indicador_u", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ExcluirCategoria(codCategoria As Integer)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_categoria", SqlDbType.Int, codCategoria)}

            dal.ExecuteNonQuery("st_sgs_categoria_indicador_d", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
