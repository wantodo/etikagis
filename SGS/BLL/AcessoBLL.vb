
Public Class AcessoBLL

    Function ListaAcessos() As Object
        Dim obj As New DAL.AcessoDAL

        Return obj.ListaAcessos
    End Function

End Class
