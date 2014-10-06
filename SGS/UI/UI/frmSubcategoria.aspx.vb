Public Class frmSubcategoria
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sessionUser") = "" Or Session("sessionUser") = Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

        desabilitaCampos()

        If Not IsPostBack Then
            carregaGridSubCategoria()
            carrega_cmbStatus()
            carrega_cmbCategoria()
        End If

        If Not Request.QueryString.Item("editar") Is Nothing Then
            If Request.QueryString("editar").ToString = "1" Then

                txtCodigo.Text = Request.QueryString("codigo").ToString
                txtSubCategoria.Text = Request.QueryString("subcategoria").ToString
                cmbCategoria.SelectedValue = Request.QueryString("codcategoria").ToString
                cmbStatus.SelectedValue = Request.QueryString("codstatus").ToString

                habilitaCampos()

                btnGravar.Enabled = True
                btnGravar.ImageUrl = "../imagens/save.ico"

                btnCancelar.Enabled = True
                btnCancelar.ImageUrl = "../imagens/no.ico"

                cmbFiltro.Enabled = False
                txtFiltro.Enabled = False

                btnConsultar.Enabled = False
                btnConsultar.ImageUrl = "../imagens/find_disabled.png"

                pnlMsg.Visible = False
            End If
        End If

        If Not Request.QueryString.Item("excluir") Is Nothing Then
            If Request.QueryString("excluir").ToString = "1" Then
                txtCodigo.Text = Request.QueryString("codigo").ToString
                txtSubCategoria.Text = Request.QueryString("subcategoria").ToString
                cmbCategoria.SelectedValue = Request.QueryString("codcategoria").ToString
                cmbStatus.SelectedValue = Request.QueryString("codstatus").ToString

                desabilitaCampos()

                btnNovo.Enabled = False
                btnNovo.ImageUrl = "../imagens/add_disabled.png"

                btnGravar.Enabled = False
                btnGravar.ImageUrl = "../imagens/save_disabled.png"

                btnCancelar.Enabled = False
                btnCancelar.ImageUrl = "../imagens/no_disabled.png"

                cmbFiltro.Enabled = False
                txtFiltro.Enabled = False

                btnConsultar.Enabled = False
                btnConsultar.ImageUrl = "../imagens/find_disabled.png"

                pnlMsg.Visible = False

                pnlExcluir.Visible = True
                pnlExcluir.Focus()
            End If
        End If

    End Sub

    Private Sub carregaGridSubCategoria()
        Dim objSubCategoriaBLL As New BLL.SubCategoriaBLL
        Dim ds As DataSet
        Dim dt As DataTable
        Dim dv As DataView

        ds = objSubCategoriaBLL.ListaSubCategoria()
        dv = ds.Tables(0).DefaultView
        dt = ds.Tables(0)

        gridSubCategoria.DataSource = dt

        If cmbFiltro.Text = "Código" Then
            dv.RowFilter = "Código = '" & txtFiltro.Text & "'"
        End If

        If cmbFiltro.Text = "Perfil" Then
            dv.RowFilter = "[Perfil] like '%" & txtFiltro.Text & "%'"
        End If

        If cmbFiltro.Text = "Status" Then
            dv.RowFilter = "[Status] like '%" & txtFiltro.Text & "%'"
        End If

        gridSubCategoria.DataBind()
    End Sub

    Private Sub gridSubCategoria_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridSubCategoria.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = ""
            e.Row.Cells(1).Text = ""
            e.Row.Cells(5).Visible = False
            e.Row.Cells(7).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = "<a href='frmSubCategoria.aspx?editar=1&codigo=" & e.Row.Cells(2).Text & "&subcategoria=" & e.Row.Cells(3).Text & "&categoria=" & e.Row.Cells(4).Text & "&codcategoria=" & e.Row.Cells(5).Text & "&status=" & e.Row.Cells(6).Text & "&codstatus=" & e.Row.Cells(7).Text & "'><img src='../imagens/edit.png'></a>"
            e.Row.Cells(1).Text = "<a href='frmSubCategoria.aspx?excluir=1&codigo=" & e.Row.Cells(2).Text & "&subcategoria=" & e.Row.Cells(3).Text & "&categoria=" & e.Row.Cells(4).Text & "&codcategoria=" & e.Row.Cells(5).Text & "&status=" & e.Row.Cells(6).Text & "&codstatus=" & e.Row.Cells(7).Text & "'><img src='../imagens/delete.png'></a>"
            e.Row.Cells(5).Visible = False
            e.Row.Cells(7).Visible = False
        End If
    End Sub

    Private Sub carrega_cmbStatus()
        Dim objPerfilRepresentanteBLL As New BLL.PerfilRepresentanteBLL

        cmbStatus.DataTextField = "dc_status"
        cmbStatus.DataValueField = "cd_status"
        cmbStatus.DataSource = objPerfilRepresentanteBLL.RetornaStatusPerfil.Tables(0)
        cmbStatus.DataBind()
    End Sub

    Private Sub carrega_cmbCategoria()
        Dim objCategoriaBLL As New BLL.CategoriaBLL
        Dim lista As New ListItem


        cmbCategoria.DataTextField = "Descrição"
        cmbCategoria.DataValueField = "Código"
        cmbCategoria.DataSource = objCategoriaBLL.ListaCategoria.Tables(0)
        cmbCategoria.DataBind()

        lista.Text = "<Selecione>"
        lista.Value = 0
        cmbCategoria.Items.Insert(0, lista)
    End Sub

    Protected Sub btnNovo_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnNovo.Click
        limpaCampos()
        habilitaCampos()

        btnNovo.Enabled = False
        btnNovo.ImageUrl = "../imagens/add_disabled.png"

        btnGravar.Enabled = True
        btnGravar.ImageUrl = "../imagens/save.ico"

        btnCancelar.Enabled = True
        btnCancelar.ImageUrl = "../imagens/no.ico"

        cmbFiltro.Enabled = False
        txtFiltro.Enabled = False

        btnConsultar.Enabled = False
        btnConsultar.ImageUrl = "../imagens/find_disabled.png"

        pnlMsg.Visible = False
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCancelar.Click
        limpaCampos()
        desabilitaCampos()

        btnNovo.Enabled = True
        btnNovo.ImageUrl = "../imagens/add.ico"

        btnGravar.Enabled = False
        btnGravar.ImageUrl = "../imagens/save_disabled.png"

        btnCancelar.Enabled = False
        btnCancelar.ImageUrl = "../imagens/no_disabled.png"

        cmbFiltro.Enabled = True
        txtFiltro.Enabled = True

        btnConsultar.Enabled = True
        btnConsultar.ImageUrl = "../imagens/find.ico"
    End Sub

    Protected Sub btnSim_Click(sender As Object, e As EventArgs) Handles btnSim.Click
        Dim objSubCategoriaBLL As New BLL.SubCategoriaBLL

        objSubCategoriaBLL.ExcluiSubCategoria(CInt(Request.QueryString("codigo").ToString))
        carregaGridSubCategoria()
        DataBind()
        pnlExcluir.Visible = False

        limpaCampos()
        desabilitaCampos()

        btnNovo.Enabled = True
        btnNovo.ImageUrl = "../imagens/add.ico"

        btnGravar.Enabled = False
        btnGravar.ImageUrl = "../imagens/save_disabled.png"

        btnCancelar.Enabled = False
        btnCancelar.ImageUrl = "../imagens/no_disabled.png"

        cmbFiltro.Enabled = True
        txtFiltro.Enabled = True

        btnConsultar.Enabled = True
        btnConsultar.ImageUrl = "../imagens/find.ico"
    End Sub

    Protected Sub btnNao_Click(sender As Object, e As EventArgs) Handles btnNao.Click
        pnlExcluir.Visible = False
        limpaCampos()
        desabilitaCampos()

        btnNovo.Enabled = True
        btnNovo.ImageUrl = "../imagens/add.ico"

        btnGravar.Enabled = False
        btnGravar.ImageUrl = "../imagens/save_disabled.png"

        btnCancelar.Enabled = False
        btnCancelar.ImageUrl = "../imagens/no_disabled.png"

        cmbFiltro.Enabled = True
        txtFiltro.Enabled = True

        btnConsultar.Enabled = True
        btnConsultar.ImageUrl = "../imagens/find.ico"
    End Sub

    Protected Sub btnGravar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnGravar.Click
        Dim objSubCategoria As New MODEL.SubCategoria
        Dim objSubCategoriaBLL As New BLL.SubCategoriaBLL

        If txtSubCategoria.Text = "" Or cmbCategoria.SelectedItem.Text = "" Or cmbCategoria.SelectedItem.Text = "<Selecione>" Or cmbStatus.SelectedItem.Text = "" Or cmbStatus.SelectedItem.Text = "<Selecione>" Then
            lblMsg.Text = "Os campos com * são de preenchimento obrigatório!"
            lblMsg.ForeColor = Drawing.Color.Red
            pnlMsg.Visible = True
            habilitaCampos()
            Exit Sub
        Else
            pnlMsg.Visible = False
        End If

        With objSubCategoria
            If txtCodigo.Text <> "" Then
                .cd_subcategoria = txtCodigo.Text
            End If
            .dc_subcategoria = txtSubCategoria.Text
            .categoria.cd_categoria = cmbCategoria.SelectedValue
            .cd_status = cmbStatus.SelectedValue
            .no_userid = Session("sessionUser")
        End With

        If btnNovo.Enabled = False Or txtCodigo.Text = "" Then
            objSubCategoriaBLL.InsereSubCategoria(objSubCategoria)
            carregaGridSubCategoria()
            btnCancelar_Click(sender, e)

            lblMsg.Text = "SubCategoria cadastrada com sucesso!"
            lblMsg.ForeColor = Drawing.Color.LightGreen
            pnlMsg.Visible = True
        Else
            objSubCategoriaBLL.AlteraSubCategoria(objSubCategoria)
            carregaGridSubCategoria()

            btnCancelar_Click(sender, e)

            lblMsg.Text = "SubCategoria alterada com sucesso!"
            lblMsg.ForeColor = Drawing.Color.LightGreen
            pnlMsg.Visible = True
        End If
    End Sub

    Private Sub limpaCampos()
        txtCodigo.Text = ""
        txtSubCategoria.Text = ""
        carrega_cmbCategoria()
        carrega_cmbStatus()
        pnlMsg.Visible = False
    End Sub

    Private Sub habilitaCampos()
        txtSubCategoria.Enabled = True
        cmbCategoria.Enabled = True
        cmbStatus.Enabled = True
    End Sub

    Private Sub desabilitaCampos()
        txtSubCategoria.Enabled = False
        cmbCategoria.Enabled = False
        cmbStatus.Enabled = False
    End Sub

End Class