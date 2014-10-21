Public Class frmUsuario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sessionUser") = "" Or Session("sessionUser") = Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

        desabilitaCampos()

        If Not IsPostBack Then
            carregaGridUsuario()
            carrega_cmbAcesso()
            carrega_cmbStatus()

            If Not Request.QueryString.Item("editar") Is Nothing Then
                If Request.QueryString("editar").ToString = "1" Then

                    txtCodigo.Text = Request.QueryString("codigo").ToString
                    txtUsuario.Text = Request.QueryString("usuario").ToString
                    txtEmail.Text = Request.QueryString("email").ToString
                    'cmbStatus.SelectedItem.Text = Request.QueryString("status").ToString
                    cmbStatus.SelectedValue = Request.QueryString("codstatus").ToString
                    'cmbAcesso.SelectedItem.Text = Request.QueryString("acesso").ToString
                    cmbAcesso.SelectedValue = Request.QueryString("codacesso").ToString

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
                    txtUsuario.Text = Request.QueryString("usuario").ToString
                    txtEmail.Text = Request.QueryString("email").ToString
                    'cmbStatus.SelectedItem.Text = Request.QueryString("status").ToString
                    cmbStatus.SelectedValue = Request.QueryString("codstatus").ToString
                    'cmbAcesso.SelectedItem.Text = Request.QueryString("acesso").ToString
                    cmbAcesso.SelectedValue = Request.QueryString("codacesso").ToString

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

    Protected Sub btnGravar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnGravar.Click
        Dim objUsuario As New MODEL.Usuario
        Dim objUsuarioBLL As New BLL.UsuarioBLL

        If String.Equals(txtSenha.Text, txtConfirmaSenha.Text) = False Then
            lblMsg.Text = "Os campos senha estão diferentes!"
            lblMsg.ForeColor = Drawing.Color.Red
            pnlMsg.Visible = True
            habilitaCampos()
            Exit Sub
        End If

        If txtUsuario.Text = "" Or txtSenha.Text = "" Or cmbAcesso.SelectedItem.Text = "<Selecione>" Then
            lblMsg.Text = "Os campos com * são de preenchimento obrigatório!"
            lblMsg.ForeColor = Drawing.Color.Red
            pnlMsg.Visible = True
            habilitaCampos()
            Exit Sub
        Else
            pnlMsg.Visible = False
        End If

        With objUsuario
            If txtCodigo.Text <> "" Then
                .codigo = txtCodigo.Text
            End If
            .nomeUsuario = txtUsuario.Text
            .senha = txtSenha.Text
            .email = txtEmail.Text
            .status = cmbStatus.SelectedValue
            .acesso = cmbAcesso.SelectedValue
        End With

        If btnNovo.Enabled = False Or txtCodigo.Text = "" Then
            objUsuarioBLL.InsereUsuario(objUsuario)
            carregaGridUsuario()
            btnCancelar_Click(sender, e)

            lblMsg.Text = "Usuário cadastrado com sucesso!"
            lblMsg.ForeColor = Drawing.Color.LightGreen
            pnlMsg.Visible = True
        Else
            objUsuarioBLL.AlteraUsuario(objUsuario)
            carregaGridUsuario()

            btnCancelar_Click(sender, e)

            lblMsg.Text = "Usuário alterado com sucesso!"
            lblMsg.ForeColor = Drawing.Color.LightGreen
            pnlMsg.Visible = True
        End If
    End Sub

    Protected Sub btnSim_Click(sender As Object, e As EventArgs) Handles btnSim.Click
        Dim objUsuarioBLL As New BLL.UsuarioBLL

        objUsuarioBLL.ExcluiUsuario(CInt(Request.QueryString("codigo").ToString))
        carregaGridUsuario()
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
        carregaGridUsuario()
        gridPerfil.Focus()
    End Sub

    Private Sub limpaCampos()
        txtCodigo.Text = ""
        txtUsuario.Text = ""
        txtSenha.Text = ""
        txtConfirmaSenha.Text = ""
        txtEmail.Text = ""
        carrega_cmbAcesso()
        carrega_cmbStatus()
        pnlMsg.Visible = False
    End Sub

    Private Sub habilitaCampos()
        txtUsuario.Enabled = True
        txtSenha.Enabled = True
        txtConfirmaSenha.Enabled = True
        txtEmail.Enabled = True
        cmbAcesso.Enabled = True
        cmbStatus.Enabled = True
    End Sub

    Private Sub desabilitaCampos()
        txtUsuario.Enabled = False
        txtSenha.Enabled = False
        txtConfirmaSenha.Enabled = False
        txtEmail.Enabled = False
        cmbAcesso.Enabled = False
        cmbStatus.Enabled = False
    End Sub

    Private Sub carregaGridUsuario()
        Dim objUsuarioBLL As New BLL.UsuarioBLL
        Dim ds As DataSet
        Dim dt As DataTable
        Dim dv As DataView

        ds = objUsuarioBLL.ListaUsuario()
        dv = ds.Tables(0).DefaultView
        dt = ds.Tables(0)

        gridPerfil.DataSource = dt

        If cmbFiltro.Text = "Código" Then
            dv.RowFilter = "Código = '" & txtFiltro.Text & "'"
        End If

        If cmbFiltro.Text = "Usuário" Then
            dv.RowFilter = "[Usuário] like '%" & txtFiltro.Text & "%'"
        End If

        If cmbFiltro.Text = "Acesso" Then
            dv.RowFilter = "[Acesso] = '" & txtFiltro.Text & "'"
        End If

        If cmbFiltro.Text = "Status" Then
            dv.RowFilter = "[Status] like '%" & txtFiltro.Text & "%'"
        End If

        gridPerfil.DataBind()
    End Sub

    Private Sub carrega_cmbAcesso()
        Dim objAcessoBLL As New BLL.AcessoBLL

        cmbAcesso.DataTextField = "dc_acesso"
        cmbAcesso.DataValueField = "cd_acesso"
        cmbAcesso.DataSource = objAcessoBLL.ListaAcessos().Tables(0)
        cmbAcesso.DataBind()
    End Sub

    Private Sub carrega_cmbStatus()
        Dim objUsuarioBLL As New BLL.UsuarioBLL

        cmbStatus.DataTextField = "dc_status"
        cmbStatus.DataValueField = "cd_status"
        cmbStatus.DataSource = objUsuarioBLL.RetornaStatusUsuario.Tables(0)
        cmbStatus.DataBind()
    End Sub

    Private Sub gridPerfil_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridPerfil.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = ""
            e.Row.Cells(1).Text = ""
            e.Row.Cells(6).Visible = False
            e.Row.Cells(8).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = "<a href='frmUsuario.aspx?editar=1&codigo=" & e.Row.Cells(2).Text & "&usuario=" & e.Row.Cells(3).Text & "&email=" & e.Row.Cells(4).Text & "&acesso=" & e.Row.Cells(5).Text & "&codacesso=" & e.Row.Cells(6).Text & "&status=" & e.Row.Cells(7).Text & "&codstatus=" & e.Row.Cells(8).Text & "'><img src='../imagens/edit.png'></a>"
            e.Row.Cells(1).Text = "<a href='frmUsuario.aspx?excluir=1&codigo=" & e.Row.Cells(2).Text & "&usuario=" & e.Row.Cells(3).Text & "&email=" & e.Row.Cells(4).Text & "&acesso=" & e.Row.Cells(5).Text & "&codacesso=" & e.Row.Cells(6).Text & "&status=" & e.Row.Cells(7).Text & "&codstatus=" & e.Row.Cells(8).Text & "'><img src='../imagens/delete.png'></a>"
            e.Row.Cells(6).Visible = False
            e.Row.Cells(8).Visible = False
        End If
    End Sub

End Class