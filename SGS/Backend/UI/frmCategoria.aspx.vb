Public Class frmCategoria
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sessionUser") = "" Or Session("sessionUser") = Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

        desabilitaCampos()

        If Not IsPostBack Then
            carregarGridCategoria()
            carrega_cmbStatus()

            If Not Request.QueryString.Item("editar") Is Nothing Then
                If Request.QueryString("editar").ToString = "1" Then

                    txtCodigo.Text = Request.QueryString("codigo").ToString
                    txtDescricao.Text = Request.QueryString("descricao").ToString
                    cmbStatus.SelectedValue = Request.QueryString("codstatus").ToString
                    'cmbStatus.SelectedItem.Value = Request.QueryString("status").ToString

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
                    txtDescricao.Text = Request.QueryString("descricao").ToString
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
        End If
    End Sub

    Private Sub carregarGridCategoria()
        Dim objCategoriaBLL As New BLL.CategoriaBLL
        Dim ds As DataSet
        Dim dt As DataTable
        Dim dv As DataView

        ds = objCategoriaBLL.ListaCategoria
        dv = ds.Tables(0).DefaultView
        dt = ds.Tables(0)
        gridCategoria.DataSource = dt

        If cmbFiltro.Text = "Código" Then
            dv.RowFilter = "Código = '" & txtFiltro.Text & "'"
        End If

        If cmbFiltro.Text = "Descrição" Then
            dv.RowFilter = "[Descrição] like '%" & txtFiltro.Text & "%'"
        End If

        'If cmbFiltro.Text = "Cod. Status" Then
        '    dv.RowFilter = "[Cod. Status] = '" & txtFiltro.Text & "'"
        'End If

        If cmbFiltro.Text = "Status" Then
            dv.RowFilter = "[Status] like '%" & txtFiltro.Text & "%'"
        End If

        gridCategoria.DataBind()


    End Sub

    Private Sub carrega_cmbStatus()
        Dim objCategoriaBLL As New BLL.CategoriaBLL

        cmbStatus.DataTextField = "dc_status"
        cmbStatus.DataValueField = "cd_status"
        cmbStatus.DataSource = objCategoriaBLL.RetornaStatusCategoria.Tables(0)
        cmbStatus.DataBind()
    End Sub

    Private Sub gridCategoria_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridCategoria.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = ""
            e.Row.Cells(1).Text = ""
            e.Row.Cells(4).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = "<a href='frmCategoria.aspx?editar=1&codigo=" & e.Row.Cells(2).Text & "&descricao=" & e.Row.Cells(3).Text & "&codstatus=" & e.Row.Cells(4).Text & "&status=" & e.Row.Cells(5).Text & "'><img src='../imagens/edit.png'></a>"
            e.Row.Cells(1).Text = "<a href='frmCategoria.aspx?excluir=1&codigo=" & e.Row.Cells(2).Text & "&descricao=" & e.Row.Cells(3).Text & "&codstatus=" & e.Row.Cells(4).Text & "&status=" & e.Row.Cells(5).Text & "'><img src='../imagens/delete.png'></a>"
            e.Row.Cells(4).Visible = False
        End If
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

    Private Sub limpaCampos()
        txtCodigo.Text = ""
        carrega_cmbStatus()
        txtDescricao.Text = ""
        pnlMsg.Visible = False
    End Sub

    Protected Sub btnGravar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnGravar.Click
        Dim objcategoria As New MODEL.CategoriaIndicador
        Dim objCategoriaBLL As New BLL.CategoriaBLL

        If txtDescricao.Text = "" Or cmbStatus.SelectedItem.Text = "" Or cmbStatus.SelectedItem.Text = "<Selecione>" Then
            lblMsg.Text = "Os campos com * são de preenchimento obrigatório!"
            lblMsg.ForeColor = Drawing.Color.Red
            pnlMsg.Visible = True
            Exit Sub
        Else
            pnlMsg.Visible = False
        End If


        With objcategoria
            If txtCodigo.Text <> "" Then
                .cd_categoria = txtCodigo.Text
            End If

            .dc_categoria = txtDescricao.Text
            .cd_status = cmbStatus.SelectedValue
            .no_userid = Session("sessionUser")
        End With

        If btnNovo.Enabled = False Or txtCodigo.Text = "" Then
            objCategoriaBLL.InsereCategoria(objcategoria)
            carregarGridCategoria()
            btnCancelar_Click(sender, e)

            lblMsg.Text = "Categoria cadastrda com sucesso!"
            lblMsg.ForeColor = Drawing.Color.LightGreen
            pnlMsg.Visible = True
        Else
            objCategoriaBLL.ALteraCategoria(objcategoria)
            carregarGridCategoria()

            btnCancelar_Click(sender, e)

            lblMsg.Text = "Categoria alterada com sucesso!"
            lblMsg.ForeColor = Drawing.Color.LightGreen
            pnlMsg.Visible = True
        End If
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
        Dim objCategoriaBLL As New BLL.CategoriaBLL

        objCategoriaBLL.ExcluirCategoria(CInt(Request.QueryString("codigo").ToString))
        carregarGridCategoria()
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

    Protected Sub btnConsultar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnConsultar.Click
        carregarGridCategoria()
    End Sub

    Private Sub habilitaCampos()
        cmbStatus.Enabled = True
        txtDescricao.Enabled = True
    End Sub

    Private Sub desabilitaCampos()
        cmbStatus.Enabled = False
        txtDescricao.Enabled = False
    End Sub


End Class