
Public Class AcessoDAL

    Function ListaAcessos() As Object
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)

            Return dal.GetDataSet("st_sgs_acessos_s", CommandType.StoredProcedure)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
