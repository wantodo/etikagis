
Public Class RepresentanteBLL

    Function RetornaRepresentante(codRepresentante As Integer) As Object
        Dim obj As New DAL.RepresentanteDAL

        Return obj.RetornaRepresentante(codRepresentante)
    End Function

    Function ListaRepresentante() As Object
        Dim obj As New DAL.RepresentanteDAL

        Return obj.ListaRepresentante()
    End Function

    Function RetornaStatusRepresentante() As Object
        Dim obj As New DAL.RepresentanteDAL

        Return obj.RetornaStatusRepresentante
    End Function

    Sub ExcluiRepresentante(codRepresentante As Integer)
        Dim obj As New DAL.RepresentanteDAL

        obj.ExcluiRepresentante(codRepresentante)
    End Sub

    Sub InsereRepresentante(objRepresentante As MODEL.Representante)
        Dim obj As New DAL.RepresentanteDAL

        obj.InsereRepresentante(objRepresentante)
    End Sub

    Sub AlteraRepresentante(objRepresentante As MODEL.Representante)
        Dim obj As New DAL.RepresentanteDAL

        obj.AlteraRepresentante(objRepresentante)
    End Sub

    Public Function ListaArea(cd_empresa As Integer) As DataSet
        Dim obj As New DAL.RepresentanteDAL

        Return obj.ListaArea(cd_empresa)
    End Function

End Class
