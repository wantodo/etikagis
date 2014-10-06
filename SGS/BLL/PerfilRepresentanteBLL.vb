
Public Class PerfilRepresentanteBLL

    Function RetornaStatusPerfil() As Object
        Dim obj As New DAL.PerfilRepresentanteDAL

        Return obj.RetornaStatusPerfil
    End Function

    Sub InserePerfil(objPerfil As MODEL.PerfilRepresentante)
        Dim obj As New DAL.PerfilRepresentanteDAL

        obj.InserePerfil(objPerfil)
    End Sub

    Sub AlteraPerfil(objPerfil As MODEL.PerfilRepresentante)
        Dim obj As New DAL.PerfilRepresentanteDAL

        obj.AlteraPerfil(objPerfil)
    End Sub

    Sub ExcluiPerfil(codPerfil As Integer)
        Dim obj As New DAL.PerfilRepresentanteDAL

        obj.ExcluiPerfil(codPerfil)
    End Sub

    Function ListaPerfilRepresentante() As Object
        Dim obj As New DAL.PerfilRepresentanteDAL

        Return obj.ListaPerfilRepresentante()
    End Function

End Class
