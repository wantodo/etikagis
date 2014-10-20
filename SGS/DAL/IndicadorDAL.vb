Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class IndicadorDAL
    Public Function ListaIndicador() As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)

            Return dal.GetDataSet("st_sgs_indicador_s", CommandType.StoredProcedure)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RetornaIndicador(cd_aspecto As Integer, cd_indicador As Integer) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_aspecto", SqlDbType.VarChar, cd_aspecto), _
                     dal.CriarParametro("@cd_indicador", SqlDbType.VarChar, cd_indicador)}

            Return dal.GetDataSet("st_sgs_indicador_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RetornaStatusIndicador() As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@tipo", SqlDbType.VarChar, "Indicador")}

            Return dal.GetDataSet("st_sgs_status_s", CommandType.StoredProcedure, param)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub InsereIndicador(objIndicador As MODEL.Indicador)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@nm_indicador", SqlDbType.VarChar, objIndicador.nm_indicador), _
                     dal.CriarParametro("@dc_indicador", SqlDbType.VarChar, objIndicador.dc_indicador), _
                     dal.CriarParametro("@cd_aspecto", SqlDbType.Int, objIndicador.aspecto.cd_aspecto), _
                     dal.CriarParametro("@cd_status", SqlDbType.Int, objIndicador.cd_status), _
                     dal.CriarParametro("@no_userid", SqlDbType.VarChar, objIndicador.no_userid)}

            dal.ExecuteNonQuery("st_sgs_indicador_i", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ALteraIndicador(objIndicador As MODEL.Indicador)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_indicador", SqlDbType.Int, objIndicador.cd_indicador), _
                     dal.CriarParametro("@nm_indicador", SqlDbType.VarChar, objIndicador.nm_indicador), _
                     dal.CriarParametro("@dc_indicador", SqlDbType.VarChar, objIndicador.dc_indicador), _
                     dal.CriarParametro("@cd_aspecto", SqlDbType.Int, objIndicador.aspecto.cd_aspecto), _
                     dal.CriarParametro("@cd_status", SqlDbType.Int, objIndicador.cd_status), _
                     dal.CriarParametro("@no_userid", SqlDbType.VarChar, objIndicador.no_userid)}

            dal.ExecuteNonQuery("st_sgs_indicador_u", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ExcluirIndicador(codIndicador As Integer)
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter

            param = {dal.CriarParametro("@cd_indicador", SqlDbType.Int, codIndicador)}

            dal.ExecuteNonQuery("st_sgs_indicador_d", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
