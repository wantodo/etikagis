
Public Class LoginBLL

    Public Function Logar(usuario As MODEL.Usuario, tipoAcesso As Integer) As DataSet
        Dim obj As New DAL.LoginDAL

        Return obj.Logar(usuario, tipoAcesso)
    End Function


End Class
