
Public Class UsuarioBLL

    Function RetornaStatusUsuario() As Object
        Dim obj As New DAL.UsuarioDAL

        Return obj.RetornaStatusUsuario
    End Function

    Function ListaUsuario() As Object
        Dim obj As New DAL.UsuarioDAL

        Return obj.ListaUsuario()
    End Function

    Sub ExcluiUsuario(codUsuario As Integer)
        Dim obj As New DAL.UsuarioDAL

        obj.ExcluiUsuario(codUsuario)
    End Sub

    Sub AlteraUsuario(objUsuario As MODEL.Usuario)
        Dim obj As New DAL.UsuarioDAL

        obj.AlteraUsuario(objUsuario)
    End Sub

    Sub InsereUsuario(objUsuario As MODEL.Usuario)
        Dim obj As New DAL.UsuarioDAL

        obj.InsereUsuario(objUsuario)
    End Sub

End Class
