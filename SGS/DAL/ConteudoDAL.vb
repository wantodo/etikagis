Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class ConteudoDAL
    Public Function ListaConteudo(codEmpresa As Integer) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_empresa", SqlDbType.Int, codEmpresa)}

            Return dal.GetDataSet("st_sgs_conteudo_home_front_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub AlteraConteudo(codEmpresa As Integer, descricao As String)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_empresa", SqlDbType.Int, codEmpresa), _
                     dal.CriarParametro("@tx_conteudo_home", SqlDbType.VarChar, descricao)}

            dal.ExecuteNonQuery("st_sgs_conteudo_home_front_u", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
