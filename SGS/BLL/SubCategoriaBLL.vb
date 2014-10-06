
Public Class SubCategoriaBLL

    Function ListaSubCategoria() As DataSet
        Dim obj As New DAL.SubCategoriaDAL

        Return obj.ListaSubCategoria()
    End Function

    Sub ExcluiSubCategoria(codSubCategoria As Integer)
        Dim obj As New DAL.SubCategoriaDAL

        obj.ExcluiSubCategoria(codSubCategoria)
    End Sub

    Sub InsereSubCategoria(objSubCategoria As MODEL.SubCategoria)
        Dim obj As New DAL.SubCategoriaDAL

        obj.InsereSubCategoria(objSubCategoria)
    End Sub

    Sub AlteraSubCategoria(objSubCategoria As MODEL.SubCategoria)
        Dim obj As New DAL.SubCategoriaDAL

        obj.AlteraSubCategoria(objSubCategoria)
    End Sub

End Class
