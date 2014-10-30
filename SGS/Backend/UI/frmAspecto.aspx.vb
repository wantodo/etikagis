Public Class frmAspecto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sessionUser") = "" Or Session("sessionUser") = Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

        desabilitaCampos()

        If Not IsPostBack Then
            carregaGridAspecto()
            carrega_cmbStatus()
            carrega_cmbCategoria()
            carrega_cmbSubcategoria()

            If Not Request.QueryString.Item("editar") Is Nothing Then
                If Request.QueryString("editar").ToString = "1" Then

                    txtCodigo.Text = Request.QueryString("codigo").ToString
                    txtDescricao.Text = Request.QueryString("descricao").ToString
                    cmbCategoria.SelectedValue = Request.QueryString("codCategoria").ToString
                    cmbSubcategoria.SelectedValue = Request.QueryString("codSubcategoria").ToString
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
                    txtDescricao.Text = Request.QueryString("descricao").ToString
                    cmbCategoria.SelectedValue = Request.QueryString("codCategoria").ToString
                    cmbSubcategoria.SelectedValue = Request.QueryString("codSubcategoria").ToString
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

    Private Sub carregaGridAspecto()
        Dim objAspectoBLL As New BLL.AspectoBLL
        Dim ds As DataSet
        Dim dt As DataTable
        Dim dv As DataView

        ds = objAspectoBLL.ListaAspecto
        dv = ds.Tables(0).DefaultView
        dt = ds.Tables(0)
        gridAspecto.DataSource = dt

        If cmbFiltro.Text = "Código" Then
            dv.RowFilter = "Código = '" & txtFiltro.Text & "'"
        End If

        If cmbFiltro.Text = "Descrição" Then
            dv.RowFilter = "[Descrição] like '%" & txtFiltro.Text & "%'"
        End If

        'If cmbFiltro.Text = "Cod. Categoria" Then
        '    dv.RowFilter = "[Cod. Cetegoria] = '" & txtFiltro.Text & "'"
        'End If

        If cmbFiltro.Text = "Categoria" Then
            dv.RowFilter = "[Categoria] like '%" & txtFiltro.Text & "%'"
        End If

        'If cmbFiltro.Text = "Cod. Status" Then
        '    dv.RowFilter = "[Cod. Status] = '" & txtFiltro.Text & "'"
        'End If

        If cmbFiltro.Text = "Status" Then
            dv.RowFilter = "[Status] like '%" & txtFiltro.Text & "%'"
        End If

        gridAspecto.DataBind()
    End Sub

    Private Sub carrega_cmbStatus()
        Dim objAspectoBLL As New BLL.AspectoBLL

        cmbStatus.DataTextField = "dc_status"
        cmbStatus.DataValueField = "cd_status"
        cmbStatus.DataSource = objAspectoBLL.RetornaStatusAspecto.Tables(0)
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

    Private Sub carrega_cmbSubcategoria()
        Dim objSubCategoriaBLL As New BLL.SubCategoriaBLL
        Dim lista As New ListItem

        cmbSubcategoria.DataTextField = "SubCategoria"
        cmbSubcategoria.DataValueField = "Código"
        cmbSubcategoria.DataSource = objSubCategoriaBLL.ListaSubCategoria.Tables(0)
        cmbSubcategoria.DataBind()

        lista.Text = "<Selecione>"
        lista.Value = 0
        cmbSubcategoria.Items.Insert(0, lista)
    End Sub

    Private Sub limpaCampos()
        txtCodigo.Text = ""
        txtDescricao.Text = ""
        carrega_cmbCategoria()
        carrega_cmbSubcategoria()
        carrega_cmbStatus()
        pnlMsg.Visible = False
    End Sub

    Private Sub gridAspecto_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridAspecto.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = ""
            e.Row.Cells(1).Text = ""
            e.Row.Cells(4).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(8).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = "<a href='frmAspecto.aspx?editar=1&codigo=" & e.Row.Cells(2).Text & "&descricao=" & e.Row.Cells(3).Text & "&codCategoria=" & e.Row.Cells(4).Text & "&categoria=" & e.Row.Cells(5).Text & "&codSubcategoria=" & e.Row.Cells(6).Text & "&subcategoria=" & e.Row.Cells(7).Text & "&codStatus=" & e.Row.Cells(8).Text & "&status=" & e.Row.Cells(9).Text & "'><img src='../imagens/edit.png'></a>"
            e.Row.Cells(1).Text = "<a href='frmAspecto.aspx?excluir=1&codigo=" & e.Row.Cells(2).Text & "&descricao=" & e.Row.Cells(3).Text & "&codCategoria=" & e.Row.Cells(4).Text & "&categoria=" & e.Row.Cells(5).Text & "&codSubcategoria=" & e.Row.Cells(6).Text & "&subcategoria=" & e.Row.Cells(7).Text & "&codStatus=" & e.Row.Cells(8).Text & "&status=" & e.Row.Cells(9).Text & "'><img src='../imagens/delete.png'></a>"
            e.Row.Cells(4).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(8).Visible = False
        End If
    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnConsultar.Click
        carregaGridAspecto()
    End Sub

    Protected Sub btnSim_Click(sender As Object, e As EventArgs) Handles btnSim.Click
        Dim objAspectoBLL As New BLL.AspectoBLL

        objAspectoBLL.ExcluirAspecto(CInt(Request.QueryString("codigo").ToString))
        carregaGridAspecto()
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

    Protected Sub btnGravar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnGravar.Click
        Dim objAspecto As New MODEL.Aspecto
        Dim objAspectoBLL As New BLL.AspectoBLL

        If txtDescricao.Text = "" Or cmbCategoria.SelectedItem.Text = "" Or cmbCategoria.SelectedItem.Text = "<Selecione>" Or _
            cmbStatus.SelectedItem.Text = "" Or cmbStatus.SelectedItem.Text = "<Selecione>" Then

            lblMsg.Text = "Os campos com * são de preenchimento obrigatório!"
            lblMsg.ForeColor = Drawing.Color.Red
            pnlMsg.Visible = True
            Exit Sub
        Else
            pnlMsg.Visible = False
        End If

        With objAspecto
            If txtCodigo.Text <> "" Then
                .cd_aspecto = txtCodigo.Text
            End If

            .dc_aspecto = txtDescricao.Text
            .cd_categoria = cmbCategoria.SelectedValue
            .subcategoria.cd_subcategoria = cmbSubCategoria.SelectedValue
            .cd_status = cmbStatus.SelectedValue
            .no_userid = Session("sessionUser")
        End With

        If btnNovo.Enabled = False Or txtCodigo.Text = "" Then
            objAspectoBLL.InsereAspecto(objAspecto)
            carregaGridAspecto()
            btnCancelar_Click(sender, e)

            lblMsg.Text = "Aspecto cadastrado com sucesso!"
            lblMsg.ForeColor = Drawing.Color.LightGreen
            pnlMsg.Visible = True
        Else
            objAspectoBLL.ALteraAspecto(objAspecto)
            carregaGridAspecto()

            btnCancelar_Click(sender, e)

            lblMsg.Text = "Aspecto alterado com sucesso!"
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

    Private Sub habilitaCampos()
        txtDescricao.Enabled = True
        cmbCategoria.Enabled = True
        cmbSubcategoria.Enabled = True
        cmbStatus.Enabled = True
    End Sub

    Private Sub desabilitaCampos()
        txtDescricao.Enabled = False
        cmbCategoria.Enabled = False
        cmbSubcategoria.Enabled = False
        cmbStatus.Enabled = False
    End Sub
End Class