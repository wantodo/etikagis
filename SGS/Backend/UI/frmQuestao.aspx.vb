Public Class frmQuestaoIndicador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sessionUser") = "" Or Session("sessionUser") = Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

     
        If Not IsPostBack Then
            carrega_gridQuestao()
            carrega_cmbEmpresa()
            carrega_cmbCategoria()
            carrega_cmbStatus()
            carrega_cmbCompetencia()
            desabilitaCampos()

            If Not Request.QueryString.Item("editar") Is Nothing Then
                If Request.QueryString("editar").ToString = "1" Then

                    habilitaCampos()

                    txtCodigo.Text = Request.QueryString("codigo").ToString
                    cmbEmpresa.SelectedValue = Request.QueryString("codempresa").ToString
                    cmbCategoria.SelectedValue = Request.QueryString("codcategoria").ToString

                    If Request.QueryString("tipo").ToString = "I" Then
                        rdbIndicador.Checked = True
                        rdbNovaQustao.Checked = False
                        indicador()
                        rdbIndicador.Enabled = False
                        rdbNovaQustao.Enabled = False
                        carrega_cmbSubcategoria()
                        If Not Request.QueryString.Item("codsubcategoria") Is Nothing Then
                            cmbSubcatedoria.SelectedValue = Request.QueryString.Item("codsubcategoria").ToString
                        End If
                        carrega_cmbAspecto()
                        cmbAspecto.SelectedValue = Request.QueryString.Item("codaspecto").ToString
                        carrega_cmbIndicador()
                        cmbIndicador.SelectedValue = Request.QueryString.Item("codindicador").ToString

                        cmbStatus.SelectedValue = Request.QueryString("codstatus").ToString
                        txtQuestao.Text = Request.QueryString("questao").ToString
                    End If

                    If Request.QueryString("tipo").ToString = "Q" Then
                        rdbNovaQustao.Checked = True
                        rdbIndicador.Checked = False
                        pergunta()
                        rdbIndicador.Enabled = False
                        rdbNovaQustao.Enabled = False

                        If Not Request.QueryString.Item("coditem") Is Nothing Then
                            lblCodigoItem.Text = Request.QueryString.Item("coditem").ToString
                            txtItem.Text = Request.QueryString.Item("item").ToString
                        End If

                        cmbStatus.SelectedValue = Request.QueryString("codstatus").ToString
                        txtQuestao.Text = Request.QueryString("questao").ToString

                        carrega_gridItemQuestao()
                    End If

                    'cmbStatus.SelectedValue = Request.QueryString("codstatus").ToString
                    'txtQuestao.Text = Request.QueryString("questao").ToString

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
                    cmbEmpresa.SelectedValue = Request.QueryString("codempresa").ToString
                    cmbCategoria.SelectedValue = Request.QueryString("codcategoria").ToString

                    If Request.QueryString("tipo").ToString = "I" Then
                        rdbIndicador.Checked = True
                        rdbNovaQustao.Checked = False
                        indicador()
                        rdbIndicador.Enabled = False
                        rdbNovaQustao.Enabled = False
                        carrega_cmbSubcategoria()
                        If Not Request.QueryString.Item("codsubcategoria") Is Nothing Then
                            cmbSubcatedoria.SelectedValue = Request.QueryString.Item("codsubcategoria").ToString
                        End If
                        carrega_cmbAspecto()
                        cmbAspecto.SelectedValue = Request.QueryString.Item("codaspecto").ToString
                        carrega_cmbIndicador()
                        cmbIndicador.SelectedValue = Request.QueryString.Item("codindicador").ToString

                        cmbStatus.SelectedValue = Request.QueryString("codstatus").ToString
                        txtQuestao.Text = Request.QueryString("questao").ToString
                    End If

                    If Request.QueryString("tipo").ToString = "Q" Then
                        rdbNovaQustao.Checked = True
                        rdbIndicador.Checked = False
                        pergunta()
                        rdbIndicador.Enabled = False
                        rdbNovaQustao.Enabled = False

                        If Not Request.QueryString.Item("coditem") Is Nothing Then
                            lblCodigoItem.Text = Request.QueryString.Item("coditem").ToString
                            txtItem.Text = Request.QueryString.Item("item").ToString

                            pnlMsg.Visible = False

                            pnlExcluirItem.Visible = True
                            pnlExcluirItem.Focus()

                            cmbStatus.SelectedValue = Request.QueryString("codstatus").ToString
                            txtQuestao.Text = Request.QueryString("questao").ToString

                            carrega_gridItemQuestao()

                            habilitaCampos()

                            rdbNovaQustao.Checked = True
                            rdbIndicador.Checked = False
                            pergunta()
                            rdbIndicador.Enabled = False
                            rdbNovaQustao.Enabled = False

                            Exit Sub
                        End If

                        cmbStatus.SelectedValue = Request.QueryString("codstatus").ToString
                        txtQuestao.Text = Request.QueryString("questao").ToString

                        carrega_gridItemQuestao()
                    End If

                    'desabilitaCampos()

                    If gridItemQuestao.Rows.Count > 0 Then
                        lblMsg.Text = "Não é possível excluir essa questão, existem itens relacionados."
                        lblMsg.ForeColor = Drawing.Color.Red
                        pnlMsg.Visible = True

                        habilitaCampos()

                        rdbNovaQustao.Checked = True
                        rdbIndicador.Checked = False
                        pergunta()
                        rdbIndicador.Enabled = False
                        rdbNovaQustao.Enabled = False

                        btnGravar.Enabled = True
                        btnGravar.ImageUrl = "../imagens/save.ico"

                        btnCancelar.Enabled = True
                        btnCancelar.ImageUrl = "../imagens/no.ico"

                        cmbFiltro.Enabled = False
                        txtFiltro.Enabled = False

                        btnConsultar.Enabled = False
                        btnConsultar.ImageUrl = "../imagens/find_disabled.png"

                        Exit Sub
                    Else
                        txtQuestao.Enabled = False

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

                        pnlGridItem.Visible = False

                        pnlExcluir.Visible = True
                        pnlExcluir.Focus()
                    End If

                   
                End If
            End If
        End If

    End Sub

    Private Sub carrega_gridQuestao()
        Dim objQuestaoBLL As New BLL.QuestaoBLL
        Dim ds As DataSet
        Dim dt As DataTable
        Dim dv As DataView

        ds = objQuestaoBLL.ListaQuestao(0)
        dv = ds.Tables(0).DefaultView
        dt = ds.Tables(0)
        gridQuestao.DataSource = dt

        If cmbFiltro.Text = "Código" Then
            dv.RowFilter = "Código = '" & txtFiltro.Text & "'"
        End If

        If cmbFiltro.Text = "Questão" Then
            dv.RowFilter = "[Questão] like '%" & txtFiltro.Text & "%'"
        End If

        If cmbFiltro.Text = "Categoria" Then
            dv.RowFilter = "[Categoria] like '%" & txtFiltro.Text & "%'"
        End If

        If cmbFiltro.Text = "Indicador" Then
            dv.RowFilter = "[Indicador] = '" & txtFiltro.Text & "'"
        End If

        If cmbFiltro.Text = "Empresa" Then
            dv.RowFilter = "[Empresa] like '%" & txtFiltro.Text & "%'"
        End If

        If cmbFiltro.Text = "Status" Then
            dv.RowFilter = "[Status] like '%" & txtFiltro.Text & "%'"
        End If

        If cmbFiltro.Text = "Competência" Then
            dv.RowFilter = "[Ano] like '%" & txtFiltro.Text & "%'"
        End If

        gridQuestao.DataBind()
    End Sub

    Private Sub carrega_cmbSubcategoria()
        Dim objQuestaoBLL As New BLL.QuestaoBLL
        Dim lista As New ListItem

        cmbSubcatedoria.Items.Clear()

        cmbSubcatedoria.DataTextField = "SubCategoria"
        cmbSubcatedoria.DataValueField = "Código"
        cmbSubcatedoria.DataSource = objQuestaoBLL.RetornaSubcategoria(cmbCategoria.SelectedValue).Tables(0)
        cmbSubcatedoria.DataBind()

        lista.Text = "<Selecione>"
        lista.Value = 0
        cmbSubcatedoria.Items.Insert(0, lista)
    End Sub

    Private Sub carrega_cmbAspecto()
        Dim objQuestaoBLL As New BLL.QuestaoBLL
        Dim lista As New ListItem

        cmbAspecto.Items.Clear()

        cmbAspecto.DataTextField = "Descrição"
        cmbAspecto.DataValueField = "Código"
        cmbAspecto.DataSource = objQuestaoBLL.RetornaAspecto(cmbCategoria.SelectedValue).Tables(0)
        cmbAspecto.DataBind()

        lista.Text = "<Selecione>"
        lista.Value = 0
        cmbAspecto.Items.Insert(0, lista)
    End Sub

    Private Sub carrega_cmbIndicador()
        Dim objIndicadorBLL As New BLL.IndicadorBLL
        Dim lista As New ListItem

        cmbIndicador.Items.Clear()

        cmbIndicador.DataTextField = "Indicador"
        cmbIndicador.DataValueField = "Código"
        cmbIndicador.DataSource = objIndicadorBLL.RetornaIndicador(cmbAspecto.SelectedValue, 0).Tables(0)
        cmbIndicador.DataBind()

        lista.Text = "<Selecione>"
        lista.Value = 0
        cmbIndicador.Items.Insert(0, lista)
    End Sub

    Private Sub carrega_descricao_indicador()
        Dim objIndicadorBLL As New BLL.IndicadorBLL
        Dim dt As DataTable

        dt = objIndicadorBLL.RetornaIndicador(cmbAspecto.SelectedValue, cmbIndicador.SelectedValue).Tables(0)

        txtQuestao.Text = dt.Rows(0)("Descrição").ToString

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

    Private Sub carrega_cmbCompetencia()
        Dim StartDate, EndDate As Date

        StartDate = New Date(2014, 12, 31)
        EndDate = New Date(2050, 12, 31)

        While StartDate <= EndDate
            cmbCompetencia.Items.Add(StartDate.Year.ToString())
            StartDate = StartDate.AddYears(1)
        End While
    End Sub

    Private Sub carrega_cmbStatus()
        Dim objCategoriaBLL As New BLL.CategoriaBLL

        cmbStatus.DataTextField = "dc_status"
        cmbStatus.DataValueField = "cd_status"
        cmbStatus.DataSource = objCategoriaBLL.RetornaStatusCategoria.Tables(0)
        cmbStatus.DataBind()
    End Sub

    Private Sub habilitaCampos()
        cmbEmpresa.Enabled = True
        cmbCategoria.Enabled = True
        cmbCompetencia.Enabled = True
        rdbIndicador.Checked = True
        rdbIndicador.Enabled = True
        rdbNovaQustao.Checked = False
        rdbNovaQustao.Enabled = True
        cmbSubcatedoria.Enabled = True
        cmbAspecto.Enabled = True
        cmbIndicador.Enabled = True
        lblSubcategoria.Visible = True
        lblAspecto.Visible = True
        lblIndicador.Visible = True
        cmbSubcatedoria.Visible = True
        cmbAspecto.Visible = True
        cmbIndicador.Visible = True
        cmbStatus.Enabled = True
        txtQuestao.Enabled = False
        frameItem.Visible = False
    End Sub

    Private Sub desabilitaCampos()
        cmbEmpresa.Enabled = False
        cmbCategoria.Enabled = False
        cmbCompetencia.Enabled = False
        rdbIndicador.Checked = True
        rdbIndicador.Enabled = False
        rdbNovaQustao.Checked = False
        rdbNovaQustao.Enabled = False
        cmbSubcatedoria.Enabled = False
        cmbAspecto.Enabled = False
        cmbIndicador.Enabled = False
        cmbSubcatedoria.Visible = True
        lblSubcategoria.Visible = True
        lblAspecto.Visible = True
        lblIndicador.Visible = True
        cmbAspecto.Visible = True
        cmbIndicador.Visible = True
        cmbStatus.Enabled = False
        txtQuestao.Enabled = False
        frameItem.Visible = False
        gridItemQuestao.DataSource = Nothing
        gridItemQuestao.DataBind()
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

        gridItemQuestao.DataSource = Nothing
        gridItemQuestao.DataBind()

        pnlMsg.Visible = False
    End Sub

    Protected Sub cmbCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCategoria.SelectedIndexChanged
        If rdbIndicador.Checked Then
            carrega_cmbSubcategoria()
            carrega_cmbAspecto()
        End If
    End Sub


    Private Sub limpaCampos()
        txtCodigo.Text = ""
        carrega_cmbEmpresa()
        carrega_cmbCategoria()
        cmbIndicador.Items.Clear()
        carrega_cmbStatus()
        txtQuestao.Text = ""
        txtItem.Text = ""
        lblCodigoItem.Text = ""
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

    Protected Sub btnGravaItem_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnGravaItem.Click
        Dim objItemQuestao As New MODEL.ItemQuestao
        Dim objQuestaoBLL As New BLL.QuestaoBLL
        Dim dt As DataTable

        'If txtItem.Text = "" Then
        '    lblMsg.Text = "Informe o item!"
        '    lblMsg.ForeColor = Drawing.Color.Red
        '    pnlMsg.Visible = True
        '    Exit Sub
        'Else
        '    pnlMsg.Visible = False
        'End If

        If txtCodigo.Text = "" Then
            btnGravar_Click(sender, e)
            gridQuestao.DataBind()
            dt = gridQuestao.DataSource
            txtCodigo.Text = dt.Rows(dt.Rows.Count - 1)(2)
            txtCodigo.AutoPostBack = True
        End If

        With objItemQuestao
            If lblCodigoItem.Text <> "" Then
                .cd_item_questao = lblCodigoItem.Text
            End If

            .questao.cd_questao = txtCodigo.Text
            .dc_item_questao = txtItem.Text
            .no_userid = Session("sessionUser")
        End With

        If lblCodigoItem.Text = "" Then
            objQuestaoBLL.InsereItemQuestao(objItemQuestao)
            carrega_gridItemQuestao()
            lblCodigoItem.Text = ""
            txtItem.Text = ""
            'btnCancelar_Click(sender, e)
        Else
            objQuestaoBLL.AlteraItemQuestao(objItemQuestao)
            carrega_gridItemQuestao()
            lblCodigoItem.Text = ""
            txtItem.Text = ""
        End If

    End Sub

    Private Sub carrega_gridItemQuestao()
        Dim objQuestaoBLL As New BLL.QuestaoBLL
        Dim ds As DataSet
        Dim dt As DataTable
        Dim dv As DataView

        ds = objQuestaoBLL.ListaItemQuestao(txtCodigo.Text)
        dv = ds.Tables(0).DefaultView
        dt = ds.Tables(0)
        gridItemQuestao.DataSource = dt
        gridItemQuestao.DataBind()
    End Sub

    Protected Sub rdbIndicador_CheckedChanged(sender As Object, e As EventArgs) Handles rdbIndicador.CheckedChanged
        indicador()
    End Sub

    Protected Sub rdbNovaQustao_CheckedChanged(sender As Object, e As EventArgs) Handles rdbNovaQustao.CheckedChanged
        pergunta()
    End Sub

    Private Sub indicador()
        lblSubcategoria.Visible = True
        lblAspecto.Visible = True
        lblIndicador.Visible = True
        cmbSubcatedoria.Visible = True
        cmbAspecto.Visible = True
        cmbIndicador.Visible = True
        txtQuestao.Enabled = False
        frameItem.Visible = False
        carrega_cmbSubcategoria()
    End Sub

    Private Sub pergunta()
        lblSubcategoria.Visible = False
        lblAspecto.Visible = False
        lblIndicador.Visible = False
        cmbSubcatedoria.Visible = False
        cmbAspecto.Visible = False
        cmbIndicador.Visible = False
        txtQuestao.Enabled = True
        txtQuestao.Text = ""
        frameItem.Visible = True
    End Sub

    Protected Sub cmbAspecto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAspecto.SelectedIndexChanged
        carrega_cmbIndicador()
    End Sub

    Protected Sub cmbIndicador_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbIndicador.SelectedIndexChanged
        carrega_descricao_indicador()
    End Sub

    Protected Sub btnGravar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnGravar.Click
        Dim objQuestao As New MODEL.Questao
        Dim objQuestaoBLL As New BLL.QuestaoBLL

       

        If cmbEmpresa.SelectedItem.Text = "" Or cmbEmpresa.SelectedItem.Text = "<Selecione>" Or _
              cmbCategoria.SelectedItem.Text = "" Or cmbCategoria.SelectedItem.Text = "<Selecione>" Or cmbCompetencia.SelectedItem.Text = "" Then

            lblMsg.Text = "Os campos com * são de preenchimento obrigatório!"
            lblMsg.ForeColor = Drawing.Color.Red
            pnlMsg.Visible = True
            Exit Sub
        Else
            pnlMsg.Visible = False
        End If

        If rdbIndicador.Checked Then
            If cmbAspecto.SelectedItem.Text = "" Or cmbAspecto.SelectedItem.Text = "<Selecione>" Then
                lblMsg.Text = "Os campos com * são de preenchimento obrigatório!"
                lblMsg.ForeColor = Drawing.Color.Red
                pnlMsg.Visible = True
                Exit Sub
            Else
                pnlMsg.Visible = False
            End If

            If cmbIndicador.SelectedItem.Text = "" OrElse cmbIndicador.SelectedItem.Text = "<Selecione>" Or _
               cmbStatus.SelectedItem.Text = "" Or cmbStatus.SelectedItem.Text = "<Selecione>" Or _
               txtQuestao.Text = "" Then

                lblMsg.Text = "Os campos com * são de preenchimento obrigatório!"
                lblMsg.ForeColor = Drawing.Color.Red
                pnlMsg.Visible = True
                Exit Sub
            Else
                pnlMsg.Visible = False
            End If
        Else
            If cmbStatus.SelectedItem.Text = "" Or cmbStatus.SelectedItem.Text = "<Selecione>" Or txtQuestao.Text = "" Then

                lblMsg.Text = "Os campos com * são de preenchimento obrigatório!"
                lblMsg.ForeColor = Drawing.Color.Red
                pnlMsg.Visible = True
                Exit Sub
            End If
        End If


        With objQuestao
            If txtCodigo.Text <> "" Then
                .cd_questao = txtCodigo.Text
            End If

            .empresa.cd_empresa = cmbEmpresa.SelectedValue
            .categoria.cd_categoria = cmbCategoria.SelectedValue

            If rdbIndicador.Checked Then
                .xx_tipo = "I"
                .indicador.cd_indicador = cmbIndicador.SelectedValue
            Else
                .xx_tipo = "Q"
            End If

            .cd_status = cmbStatus.SelectedValue
            .dc_questao = txtQuestao.Text
            .no_userid = Session("sessionUser")
            .dt_competencia = cmbCompetencia.SelectedValue
        End With

        If (btnNovo.Enabled = False And txtCodigo.Text = "") Or txtCodigo.Text = "" Then
            objQuestaoBLL.InsereQuestao(objQuestao)
            carrega_gridQuestao()

            If txtItem.Text = "" Then
                btnCancelar_Click(sender, e)
            End If

            lblMsg.Text = "Questão cadastrda com sucesso!"
            lblMsg.ForeColor = Drawing.Color.LightGreen
            pnlMsg.Visible = True
        Else
            objQuestaoBLL.AlteraQuestao(objQuestao)
            carrega_gridQuestao()

            btnCancelar_Click(sender, e)

            lblMsg.Text = "Questão alterada com sucesso!"
            lblMsg.ForeColor = Drawing.Color.LightGreen
            pnlMsg.Visible = True
        End If

    End Sub

    Private Sub gridQuestao_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridQuestao.RowDataBound
        Dim temp As String

        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = ""
            e.Row.Cells(1).Text = ""
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(8).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(12).Visible = False
            e.Row.Cells(13).Visible = False
            e.Row.Cells(14).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            temp = e.Row.Cells(3).Text
            e.Row.Cells(3).Text = "<div style='width:323px; white-space:pre-wrap;'>" & temp & "</div>"

            e.Row.Cells(0).Text = "<a href='frmQuestao.aspx?editar=1&codigo=" & e.Row.Cells(2).Text & "&questao=" & temp & "&codcategoria=" & e.Row.Cells(4).Text & "&categoria=" & e.Row.Cells(5).Text & "&codindicador=" & e.Row.Cells(6).Text & "&indicador=" & e.Row.Cells(7).Text & "&codempresa=" & e.Row.Cells(8).Text & "&empresa=" & e.Row.Cells(9).Text & "&codstatus=" & e.Row.Cells(10).Text & "&status=" & e.Row.Cells(11).Text & "&tipo=" & e.Row.Cells(12).Text & "&codaspecto=" & e.Row.Cells(13).Text & "&codsubcategoria=" & e.Row.Cells(14).Text & "'><img src='../imagens/edit.png'></a>"
            e.Row.Cells(1).Text = "<a href='frmQuestao.aspx?excluir=1&codigo=" & e.Row.Cells(2).Text & "&questao=" & temp & "&codcategoria=" & e.Row.Cells(4).Text & "&categoria=" & e.Row.Cells(5).Text & "&codindicador=" & e.Row.Cells(6).Text & "&indicador=" & e.Row.Cells(7).Text & "&codempresa=" & e.Row.Cells(8).Text & "&empresa=" & e.Row.Cells(9).Text & "&codstatus=" & e.Row.Cells(10).Text & "&status=" & e.Row.Cells(11).Text & "&tipo=" & e.Row.Cells(12).Text & "&codaspecto=" & e.Row.Cells(13).Text & "&codsubcategoria=" & e.Row.Cells(14).Text & "'><img src='../imagens/delete.png'></a>"

            If e.Row.Cells(7).Text = "" Or e.Row.Cells(7).Text = "&nbsp;" Then
                e.Row.Cells(7).Text = "P-" & e.Row.Cells(2).Text
            End If

            e.Row.Cells(4).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(8).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(12).Visible = False
            e.Row.Cells(13).Visible = False
            e.Row.Cells(14).Visible = False
        End If
    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnConsultar.Click
        pnlMsg.Visible = False
        carrega_gridQuestao()
        gridQuestao.Focus()
    End Sub

    Private Sub gridItemQuestao_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridItemQuestao.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = ""
            e.Row.Cells(1).Text = ""

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = "<a href='frmQuestao.aspx?editar=1&codigo=" & txtCodigo.Text & "&questao=" & txtQuestao.Text & "&codcategoria=" & cmbCategoria.SelectedValue & "&categoria=" & cmbCategoria.SelectedItem.Text & "&codindicador=" & "" & "&indicador=" & "" & "&codempresa=" & cmbEmpresa.SelectedValue & "&empresa=" & cmbEmpresa.SelectedItem.Text & "&codstatus=" & cmbStatus.SelectedValue & "&status=" & "" & "&tipo=Q" & "&codaspecto=" & "" & "&codsubcategoria=" & "" & "&coditem=" & e.Row.Cells(2).Text & "&item=" & e.Row.Cells(3).Text & "'><img src='../imagens/edit.png'></a>"
            e.Row.Cells(1).Text = "<a href='frmQuestao.aspx?excluir=1&codigo=" & txtCodigo.Text & "&questao=" & txtQuestao.Text & "&codcategoria=" & cmbCategoria.SelectedValue & "&categoria=" & cmbCategoria.SelectedItem.Text & "&codindicador=" & "" & "&indicador=" & "" & "&codempresa=" & cmbEmpresa.SelectedValue & "&empresa=" & cmbEmpresa.SelectedItem.Text & "&codstatus=" & cmbStatus.SelectedValue & "&status=" & "" & "&tipo=Q" & "&codaspecto=" & "" & "&codsubcategoria=" & "" & "&coditem=" & e.Row.Cells(2).Text & "&item=" & e.Row.Cells(3).Text & "'><img src='../imagens/delete.png'></a>"

        End If
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

    Protected Sub btnNaoItem_Click(sender As Object, e As EventArgs) Handles btnNaoItem.Click
        lblCodigoItem.Text = ""
        txtItem.Text = ""
        pnlExcluirItem.Visible = False
        gridItemQuestao.Focus()
    End Sub

    Protected Sub btnSimItem_Click(sender As Object, e As EventArgs) Handles btnSimItem.Click
        Dim objItemQuestaoBLL As New BLL.QuestaoBLL

        objItemQuestaoBLL.ExcluirItemQuestao(CInt(lblCodigoItem.Text))
        carrega_gridItemQuestao()
        pnlExcluirItem.Visible = False
        lblCodigoItem.Text = ""
        txtItem.Text = ""
        gridItemQuestao.Focus()
    End Sub

    Protected Sub btnSim_Click(sender As Object, e As EventArgs) Handles btnSim.Click
        Dim objQuestaoBLL As New BLL.QuestaoBLL

        objQuestaoBLL.ExcluirQuestao(CInt(Request.QueryString("codigo").ToString))
        carrega_gridQuestao()
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
End Class