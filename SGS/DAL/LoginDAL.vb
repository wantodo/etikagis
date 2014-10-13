﻿Imports System.Data.SqlClient

Public Class LoginDAL

    Public Function Logar(usuario As MODEL.Usuario, tipoAcesso As Integer) As DataSet
        Try
            Dim dal As New BDDAL(COMUM.strConexao, True)
            Dim param() As SqlParameter = { _
                                            dal.CriarParametro("@pvchUsuario", SqlDbType.VarChar, usuario.nomeUsuario), _
                                            dal.CriarParametro("@pvchSenha", SqlDbType.VarChar, usuario.senha), _
                                            dal.CriarParametro("@pintTipoAcesso", SqlDbType.Int, tipoAcesso)}

            Return dal.GetDataSet("st_sgs_usuario_v", CommandType.StoredProcedure, param)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
