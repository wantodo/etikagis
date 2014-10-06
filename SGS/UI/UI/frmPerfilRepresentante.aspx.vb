Public Class frmPerfil
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sessionUser") = "" Or Session("sessionUser") = Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

        desabilitaCampos()

        If Not IsPostBack Then
            carregaGridPerfilRepresentante()
            carrega_cmbStatus()

            If Not Request.QueryString.Item("editar") Is Nothing Then
                If Request.QueryString("editar").ToString = "1" Then

                    txtCodigo.Text = Request.QueryString("codigo").ToString
                    txtPerfil.Text = Request.QueryString("perfil").ToString
                    'cmbStatus.SelectedItem.Text = Request.QueryString("status").ToString
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
                    txtPerfil.Text = Request.QueryString("perfil").ToString
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

    Private Sub carregaGridPerfilRepresentante()
        Dim objPerfilRepresentanteBLL As New BLL.PerfilRepresentanteBLL
        Dim ds As DataSet
        Dim dt As DataTable
        Dim dv As DataView

        ds = objPerfilRepresentanteBLL.ListaPerfilRepresentante()
        dv = ds.Tables(0).DefaultView
        dt = ds.Tables(0)

        gridPerfil.DataSource = dt

        If cmbFiltro.Text = "Código" Then
            dv.RowFilter = "Código = '" & txtFiltro.Text & "'"
        End If

        If cmbFiltro.Text = "Perfil" Then
            dv.RowFilter = "[Perfil] like '%" & txtFiltro.Text & "%'"
        End If

        If cmbFiltro.Text = "Status" Then
            dv.RowFilter = "[Status] like '%" & txtFiltro.Text & "%'"
        End If

        gridPerfil.DataBind()
    End Sub

    Private Sub carrega_cmbStatus()
        Dim objPerfilRepresentanteBLL As New BLL.PerfilRepresentanteBLL

        cmbStatus.DataTextField = "dc_status"
        cmbStatus.DataValueField = "cd_status"
        cmbStatus.DataSource = objPerfilRepresentanteBLL.RetornaStatusPerfil.Tables(0)
        cmbStatus.DataBind()
    End Sub

    Private Sub gridPerfil_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridPerfil.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = ""
            e.Row.Cells(1).Text = ""
            e.Row.Cells(5).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = "<a href='frmPerfilRepresentante.aspx?editar=1&codigo=" & e.Row.Cells(2).Text & "&perfil=" & e.Row.Cells(3).Text & "&status=" & e.Row.Cells(4).Text & "&codstatus=" & e.Row.Cells(5).Text & "'><img src='../imagens/edit.png'></a>"
            e.Row.Cells(1).Text = "<a href='frmPerfilRepresentante.aspx?excluir=1&codigo=" & e.Row.Cells(2).Text & "&perfil=" & e.Row.Cells(3).Text & "&status=" & e.Row.Cells(4).Text & "&codstatus=" & e.Row.Cells(5).Text & "'><img src='../imagens/delete.png'></a>"
            e.Row.Cells(5).Visible = False
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

    Protected Sub btnGravar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnGravar.Click
        Dim objPerfil As New MODEL.PerfilRepresentante
        Dim objPerfilBLL As New BLL.PerfilRepresentanteBLL

        If txtPerfil.Text = "" Or cmbStatus.SelectedItem.Text = "" Or cmbStatus.SelectedItem.Text = "<Selecione>" Then
            lblMsg.Text = "Os campos com * são de preenchimento obrigatório!"
            lblMsg.ForeColor = Drawing.Color.Red
            pnlMsg.Visible = True
            habilitaCampos()
            Exit Sub
        Else
            pnlMsg.Visible = False
        End If

        With objPerfil
            If txtCodigo.Text <> "" Then
                .cd_perfil_representante = txtCodigo.Text
            End If
            .dc_perfil_representante = txtPerfil.Text
            .cd_status = cmbStatus.SelectedValue

        End With

        If btnNovo.Enabled = False Or txtCodigo.Text = "" Then
            objPerfilBLL.InserePerfil(objPerfil)
            carregaGridPerfilRepresentante()
            btnCancelar_Click(sender, e)

            lblMsg.Text = "Perfil cadastrado com sucesso!"
            lblMsg.ForeColor = Drawing.Color.LightGreen
            pnlMsg.Visible = True
        Else
            objPerfilBLL.ALteraPerfil(objPerfil)
            carregaGridPerfilRepresentante()

            btnCancelar_Click(sender, e)

            lblMsg.Text = "Perfil alterado com sucesso!"
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
        Dim objPerfilRepresentanteBLL As New BLL.PerfilRepresentanteBLL

        objPerfilRepresentanteBLL.ExcluiPerfil(CInt(Request.QueryString("codigo").ToString))
        carregaGridPerfilRepresentante()
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
        carregaGridPerfilRepresentante()
    End Sub

    Private Sub limpaCampos()
        txtCodigo.Text = ""
        txtPerfil.Text = ""
        carrega_cmbStatus()
        pnlMsg.Visible = False
    End Sub

    Private Sub habilitaCampos()
        txtPerfil.Enabled = True
        cmbStatus.Enabled = True
    End Sub

    Private Sub desabilitaCampos()
        txtPerfil.Enabled = False
        cmbStatus.Enabled = False
    End Sub
End Class