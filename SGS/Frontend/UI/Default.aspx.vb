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

        carrega_prazo()
    End Sub

    Private Sub carrega_prazo()
        Dim objRepresentanteBLL As New BLL.RepresentanteBLL
        Dim dt As DataTable

        dt = objRepresentanteBLL.RetornaRepresentante(Session("codRepresentante")).Tables(0)

        If dt.Rows(0)("Prazo").ToString <> "" Then
            lblDataPrazo.Text = "Você tem até " & dt.Rows(0)("Prazo").ToString & " para responder"
        Else
            lblDataPrazo.Text = ""
        End If


        If lblDataPrazo.Text = "" Then
            lblDataPrazo.Visible = False
        Else
            lblDataPrazo.Visible = True
        End If
    End Sub
End Class