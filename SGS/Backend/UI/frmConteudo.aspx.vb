Public Class frmConteudo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sessionUser") = "" Or Session("sessionUser") = Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

        If Not IsPostBack Then
            carrega_cmbEmpresa()
            pnlMsg.Visible = False
        End If
    End Sub

    Private Sub carrega_cmbEmpresa()
        Dim objEmpresaBLL As New BLL.EmpresaBLL
        Dim lista As New ListItem

        cmbEmpresa.DataTextField = "Razão Social"
        cmbEmpresa.DataValueField = "Código"
        cmbEmpresa.DataSource = objEmpresaBLL.ListaEmpresa.Tables(0)
        cmbEmpresa.DataBind()

        lista.Text = "<Selecione>"
        lista.Value = 0
        cmbEmpresa.Items.Insert(0, lista)
    End Sub

    Private Sub carregaConteudoHomeFront()
        Dim objConteudoBLL As New BLL.ConteudoBLL
        Dim dt As DataTable

        If cmbEmpresa.SelectedValue = 0 Then
            txtDescricao.Text = ""
            Exit Sub
        End If

        dt = objConteudoBLL.ListaConteudo(cmbEmpresa.SelectedValue).Tables(0)

        txtDescricao.Text = dt.Rows(0)("tx_conteudo_home")

    End Sub

    Protected Sub cmbEmpresa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEmpresa.SelectedIndexChanged
        pnlMsg.Visible = False
        carregaConteudoHomeFront()
    End Sub

    Protected Sub btnGravar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnGravar.Click
        Dim objConteudoBLL As New BLL.ConteudoBLL

        objConteudoBLL.AlteraConteudo(cmbEmpresa.SelectedValue, txtDescricao.Text)

        lblMsg.Text = "Conteúdo gravado com sucesso!"
        lblMsg.ForeColor = Drawing.Color.LightGreen
        pnlMsg.Visible = True
    End Sub
End Class