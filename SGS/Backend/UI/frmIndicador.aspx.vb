Public Class frmIndicador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sessionUser") = "" Or Session("sessionUser") = Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

        desabilitaCampos()

        If Not IsPostBack Then
            carregarGridIndicador()
            carrega_cmbAspecto()
            carrega_cmbStatus()

            If Not Request.QueryString.Item("editar") Is Nothing Then
                If Request.QueryString("editar").ToString = "1" Then

                    txtCodigo.Text = Request.QueryString("codigo").ToString
                    txtIndicador.Text = Request.QueryString("Indicador").ToString
                    txtDescricao.Text = Request.QueryString("descricao").ToString
                    cmbAspecto.SelectedValue = Request.QueryString("cd_aspecto").ToString
                    cmbStatus.SelectedValue = Request.QueryString("cd_status").ToString

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
                    txtIndicador.Text = Request.QueryString("Indicador").ToString
                    txtDescricao.Text = Request.QueryString("descricao").ToString
                    cmbAspecto.SelectedValue = Request.QueryString("cd_aspecto").ToString
                    cmbStatus.SelectedValue = Request.QueryString("cd_status").ToString

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

    Private Sub carrega_cmbAspecto()
        Dim objAspectoBLL As New BLL.AspectoBLL

        cmbAspecto.DataTextField = "Descrição"
        cmbAspecto.DataValueField = "Código"
        cmbAspecto.DataSource = objAspectoBLL.ListaAspecto.Tables(0)
        cmbAspecto.DataBind()

        cmbAspecto.Items.Add(New ListItem("<Selecione>", 0))
        cmbAspecto.SelectedValue = 0
    End Sub

    Private Sub carrega_cmbStatus()
        Dim objIndicadorBLL As New BLL.IndicadorBLL

        cmbStatus.DataTextField = "dc_status"
        cmbStatus.DataValueField = "cd_status"
        cmbStatus.DataSource = objIndicadorBLL.RetornaStatusIndicador.Tables(0)
        cmbStatus.DataBind()
    End Sub

    Private Sub carregarGridIndicador()
        Dim objIndicadorBLL As New BLL.IndicadorBLL
        Dim ds As DataSet
        Dim dt As DataTable
        Dim dv As DataView

        ds = objIndicadorBLL.ListaIndicador
        dv = ds.Tables(0).DefaultView
        dt = ds.Tables(0)
        gridIndicador.DataSource = dt

        If cmbFiltro.Text = "Código" Then
            dv.RowFilter = "Código = '" & txtFiltro.Text & "'"
        End If

        If cmbFiltro.Text = "Indicador" Then
            dv.RowFilter = "Indicador like '%" & txtFiltro.Text & "%'"
        End If

        If cmbFiltro.Text = "Descrição" Then
            dv.RowFilter = "[Descrição] like '%" & txtFiltro.Text & "%'"
        End If

        If cmbFiltro.Text = "Aspecto" Then
            dv.RowFilter = "[Aspecto] like '%" & txtFiltro.Text & "%'"
        End If

        If cmbFiltro.Text = "Status" Then
            dv.RowFilter = "[Status] like '%" & txtFiltro.Text & "%'"
        End If

        gridIndicador.DataBind()


    End Sub

    Private Sub habilitaCampos()
        txtIndicador.Enabled = True
        txtDescricao.Enabled = True
        cmbAspecto.Enabled = True
        cmbStatus.Enabled = True
    End Sub

    Private Sub desabilitaCampos()
        txtIndicador.Enabled = False
        txtDescricao.Enabled = False
        cmbAspecto.Enabled = False
        cmbStatus.Enabled = False
    End Sub

    Private Sub gridIndicador_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridIndicador.RowDataBound
        Dim temp As String
        Dim temp2 As String

        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = ""
            e.Row.Cells(1).Text = ""
            e.Row.Cells(5).Visible = False
            e.Row.Cells(7).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            temp = e.Row.Cells(4).Text
            e.Row.Cells(4).Text = "<div style='width:350px; white-space:pre-wrap;'>" & temp & "</div>"

            temp2 = e.Row.Cells(6).Text
            e.Row.Cells(6).Text = "<div style='width:275px; white-space:pre-wrap;'>" & temp2 & "</div>"

            e.Row.Cells(0).Text = "<a href='frmIndicador.aspx?editar=1&codigo=" & e.Row.Cells(2).Text & "&indicador=" & e.Row.Cells(3).Text & "&descricao=" & temp & "&cd_aspecto=" & e.Row.Cells(5).Text & "&aspecto=" & temp2 & "&cd_status=" & e.Row.Cells(7).Text & "&status=" & e.Row.Cells(8).Text & "'><img src='../imagens/edit.png'></a>"
            e.Row.Cells(1).Text = "<a href='frmIndicador.aspx?excluir=1&codigo=" & e.Row.Cells(2).Text & "&indicador=" & e.Row.Cells(3).Text & "&descricao=" & temp & "&cd_aspecto=" & e.Row.Cells(5).Text & "&aspecto=" & temp2 & "&cd_status=" & e.Row.Cells(7).Text & "&status=" & e.Row.Cells(8).Text & "'><img src='../imagens/delete.png'></a>"
            e.Row.Cells(4).Wrap = True
            e.Row.Cells(6).Wrap = True
            e.Row.Cells(5).Visible = False
            e.Row.Cells(7).Visible = False
        End If
    End Sub

    Private Sub limpaCampos()
        txtCodigo.Text = ""
        txtIndicador.Text = ""
        txtDescricao.Text = ""
        carrega_cmbAspecto()
        carrega_cmbStatus()
        pnlMsg.Visible = False
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
        Dim objIndicador As New MODEL.Indicador
        Dim objIndicadorBLL As New BLL.IndicadorBLL

        If txtIndicador.Text = "" Or txtDescricao.Text = "" Or cmbAspecto.SelectedItem.Text = "" Or cmbAspecto.SelectedItem.Text = "<Selecione>" Or _
           cmbStatus.SelectedItem.Text = "" Or cmbStatus.SelectedItem.Text = "<Selecione>" Then

            lblMsg.Text = "Os campos com * são de preenchimento obrigatório!"
            lblMsg.ForeColor = Drawing.Color.Red
            pnlMsg.Visible = True
            Exit Sub
        Else
            pnlMsg.Visible = False
        End If

        With objIndicador
            If txtCodigo.Text <> "" Then
                .cd_indicador = txtCodigo.Text
            End If

            .nm_indicador = txtIndicador.Text
            .dc_indicador = txtDescricao.Text
            .aspecto.cd_aspecto = cmbAspecto.SelectedValue
            .cd_status = cmbStatus.SelectedValue
        End With

        If btnNovo.Enabled = False Or txtCodigo.Text = "" Then
            objIndicadorBLL.InsereIndicador(objIndicador)
            carregarGridIndicador()
            btnCancelar_Click(sender, e)

            lblMsg.Text = "Indicador cadastrado com sucesso!"
            lblMsg.ForeColor = Drawing.Color.LightGreen
            pnlMsg.Visible = True
        Else
            objIndicadorBLL.ALteraIndicador(objIndicador)
            carregarGridIndicador()

            btnCancelar_Click(sender, e)

            lblMsg.Text = "Indicador alterado com sucesso!"
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
        Dim objIndicadorBLL As New BLL.IndicadorBLL

        objIndicadorBLL.ExcluirIndicador(CInt(Request.QueryString("codigo").ToString))
        carregarGridIndicador()
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
        carregarGridIndicador()
        gridIndicador.Focus()
    End Sub
End Class