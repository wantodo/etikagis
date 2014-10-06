Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class SubCategoriaDAL

    Function ListaSubCategoria() As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)

            Return dal.GetDataSet("st_sgs_subcategoria_s", CommandType.StoredProcedure)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub ExcluiSubCategoria(codSubCategoria As Integer)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_subcategoria", SqlDbType.Int, codSubCategoria)}

            dal.ExecuteNonQuery("st_sgs_subcategoria_d", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub InsereSubCategoria(objSubCategoria As MODEL.SubCategoria)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@dc_subcategoria", SqlDbType.VarChar, objSubCategoria.dc_subcategoria), _
                     dal.CriarParametro("@cd_categoria", SqlDbType.Int, objSubCategoria.categoria.cd_categoria), _
                     dal.CriarParametro("@cd_status", SqlDbType.Int, objSubCategoria.cd_status), _
                     dal.CriarParametro("@no_userid_cadastro", SqlDbType.VarChar, objSubCategoria.no_userid)}

            dal.ExecuteNonQuery("st_sgs_subcategoria_i", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub AlteraSubCategoria(objSubCategoria As MODEL.SubCategoria)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_subcategoria", SqlDbType.Int, objSubCategoria.cd_subcategoria), _
                     dal.CriarParametro("@dc_subcategoria", SqlDbType.VarChar, objSubCategoria.dc_subcategoria), _
                     dal.CriarParametro("@cd_categoria", SqlDbType.Int, objSubCategoria.categoria.cd_categoria), _
                     dal.CriarParametro("@cd_status", SqlDbType.Int, objSubCategoria.cd_status), _
                     dal.CriarParametro("@no_userid", SqlDbType.VarChar, objSubCategoria.no_userid)}

            dal.ExecuteNonQuery("st_sgs_subcategoria_u", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
