Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class IndicadorBLL
    Public Function ListaIndicador() As DataSet
        Dim obj As New DAL.IndicadorDAL

        Return obj.ListaIndicador
    End Function

    Public Function RetornaStatusIndicador() As DataSet
        Dim obj As New DAL.IndicadorDAL

        Return obj.RetornaStatusIndicador
    End Function

    Public Sub InsereIndicador(objIndicador As MODEL.Indicador)
        Dim obj As New DAL.IndicadorDAL

        obj.InsereIndicador(objIndicador)
    End Sub

    Public Sub ALteraIndicador(objIndicador As MODEL.Indicador)
        Dim obj As New DAL.IndicadorDAL

        obj.ALteraIndicador(objIndicador)
    End Sub

    Public Sub ExcluirIndicador(codIndicador As Integer)
        Dim obj As New DAL.IndicadorDAL

        obj.ExcluirIndicador(codIndicador)
    End Sub

    Public Function RetornaIndicador(cd_aspecto As Integer, cd_indicador As Integer) As DataSet
        Dim obj As New DAL.IndicadorDAL

        Return obj.RetornaIndicador(cd_aspecto, cd_indicador)
    End Function

End Class
