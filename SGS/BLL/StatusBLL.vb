
Public Class StatusBLL

    Function ListaStatus() As DataSet
        Dim obj As New DAL.StatusDAL

        Return obj.ListaStatus
    End Function

    Sub InsereStatus(objStatus As MODEL.Status)
        Dim obj As New DAL.StatusDAL

        obj.InsereStatus(objStatus)
    End Sub

    Sub AlteraStatus(objStatus As MODEL.Status)
        Dim obj As New DAL.StatusDAL

        obj.AlteraStatus(objStatus)
    End Sub

    Sub ExcluiStatus(codStatus As Integer)
        Dim obj As New DAL.StatusDAL

        obj.ExcluiStatus(codStatus)
    End Sub

End Class
