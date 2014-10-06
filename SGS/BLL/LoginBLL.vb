
Public Class LoginBLL

    Public Function Logar(usuario As MODEL.Usuario) As DataSet
        Dim obj As New DAL.LoginDAL

        Return obj.Logar(usuario)
    End Function


End Class
