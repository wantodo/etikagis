Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sessionUser") = "" Or Session("sessionUser") = Nothing Then
            Response.Redirect("~/UI/frmLogin.aspx")

        End If

        Dim objConteudoBLL As New BLL.ConteudoBLL
        Dim dt As DataTable

        dt = objConteudoBLL.ListaConteudo(Session("codEmpresa")).Tables(0)
        lblConteudo.Text = dt.Rows(0)("tx_conteudo_home")
    End Sub

End Class