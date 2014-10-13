Public Class frmEmpresa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sessionUser") = "" Or Session("sessionUser") = Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

        desabilitaCampos()

        If Not IsPostBack Then
            carregaGridEmpresa()
            carrega_cmbStatus()

            If Not Request.QueryString.Item("editar") Is Nothing Then
                If Request.QueryString("editar").ToString = "1" Then

                    txtCodigo.Text = Request.QueryString("codigo").ToString
                    txtRazaoSocial.Text = Request.QueryString("razaosocial").ToString
                    txtNomeFantasia.Text = Request.QueryString("nomefantasia").ToString
                    txtCNPJ.Text = Request.QueryString("cnpj").ToString
                    txtIE.Text = Request.QueryString("ie").ToString
                    txtIM.Text = Request.QueryString("im").ToString
                    txtEmail.Text = Request.QueryString("email").ToString
                    cmbStatus.SelectedValue = Request.QueryString("cd_status").ToString

                    txtNome.Text = Request.QueryString("nome").ToString
                    txtDepartamento.Text = Request.QueryString("departamento").ToString

                    txtCEP.Text = Request.QueryString("cep").ToString
                    txtEndereco.Text = Request.QueryString("endereco").ToString
                    txtCidade.Text = Request.QueryString("cidade").ToString
                    txtUF.Text = Request.QueryString("uf").ToString
                    txtTelefone.Text = Request.QueryString("telefone").ToString

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
                    txtRazaoSocial.Text = Request.QueryString("razaosocial").ToString
                    txtNomeFantasia.Text = Request.QueryString("nomefantasia").ToString
                    txtCNPJ.Text = Request.QueryString("cnpj").ToString
                    txtIE.Text = Request.QueryString("ie").ToString
                    txtIM.Text = Request.QueryString("im").ToString
                    txtEmail.Text = Request.QueryString("email").ToString
                    cmbStatus.SelectedValue = Request.QueryString("cd_status").ToString

                    txtNome.Text = Request.QueryString("nome").ToString
                    txtDepartamento.Text = Request.QueryString("departamento").ToString

                    txtCEP.Text = Request.QueryString("cep").ToString
                    txtEndereco.Text = Request.QueryString("endereco").ToString
                    txtCidade.Text = Request.QueryString("cidade").ToString
                    txtUF.Text = Request.QueryString("uf").ToString
                    txtTelefone.Text = Request.QueryString("telefone").ToString

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

    Private Sub carrega_cmbStatus()
        Dim objEmpresaBLL As New BLL.EmpresaBLL

        cmbStatus.DataTextField = "dc_status"
        cmbStatus.DataValueField = "cd_status"
        cmbStatus.DataSource = objEmpresaBLL.RetornaStatusEmpresa.Tables(0)
        cmbStatus.DataBind()
    End Sub

    Private Sub carregaGridEmpresa()
        Dim objEmpresaBLL As New BLL.EmpresaBLL
        Dim ds As DataSet
        Dim dt As DataTable
        Dim dv As DataView

        ds = objEmpresaBLL.ListaEmpresa()
        dv = ds.Tables(0).DefaultView
        dt = ds.Tables(0)
        gridEmpresa.DataSource = dt

        If cmbFiltro.Text = "Código" Then
            dv.RowFilter = "Código = '" & txtFiltro.Text & "'"
        End If

        If cmbFiltro.Text = "Razão social" Then
            dv.RowFilter = "[Razão social] like '%" & txtFiltro.Text & "%'"
        End If

        If cmbFiltro.Text = "Nome fantasia" Then
            dv.RowFilter = "[Nome fantasia] like '%" & txtFiltro.Text & "%'"
        End If

        If cmbFiltro.Text = "CNPJ" Then
            dv.RowFilter = "CNPJ = '" & txtFiltro.Text & "'"
        End If

        If cmbFiltro.Text = "Nome cobrança" Then
            dv.RowFilter = "[Nome cobrança] like '%" & txtFiltro.Text & "%'"
        End If

        If cmbFiltro.Text = "Departamento" Then
            dv.RowFilter = "[Departamento] like '%" & txtFiltro.Text & "%'"
        End If

        gridEmpresa.DataBind()
    End Sub

    Private Sub gridEmpresa_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridEmpresa.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = ""
            e.Row.Cells(1).Text = ""
            e.Row.Cells(16).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = "<a href='frmEmpresa.aspx?editar=1&codigo=" & e.Row.Cells(2).Text & "&razaosocial=" & e.Row.Cells(3).Text & "&nomefantasia=" & e.Row.Cells(4).Text & "&cnpj=" & e.Row.Cells(5).Text & "&ie=" & e.Row.Cells(6).Text & "&im=" & e.Row.Cells(7).Text & "&nome=" & e.Row.Cells(8).Text & "&departamento=" & e.Row.Cells(9).Text & "&cep=" & e.Row.Cells(10).Text & "&endereco=" & e.Row.Cells(11).Text & "&cidade=" & e.Row.Cells(12).Text & "&uf=" & e.Row.Cells(13).Text & "&telefone=" & e.Row.Cells(14).Text & "&email=" & e.Row.Cells(15).Text & "&cd_status=" & e.Row.Cells(16).Text & "&status=" & e.Row.Cells(17).Text & "'><img src='../imagens/edit.png'></a>"
            e.Row.Cells(1).Text = "<a href='frmEmpresa.aspx?excluir=1&codigo=" & e.Row.Cells(2).Text & "&razaosocial=" & e.Row.Cells(3).Text & "&nomefantasia=" & e.Row.Cells(4).Text & "&cnpj=" & e.Row.Cells(5).Text & "&ie=" & e.Row.Cells(6).Text & "&im=" & e.Row.Cells(7).Text & "&nome=" & e.Row.Cells(8).Text & "&departamento=" & e.Row.Cells(9).Text & "&cep=" & e.Row.Cells(10).Text & "&endereco=" & e.Row.Cells(11).Text & "&cidade=" & e.Row.Cells(12).Text & "&uf=" & e.Row.Cells(13).Text & "&telefone=" & e.Row.Cells(14).Text & "&email=" & e.Row.Cells(15).Text & "&cd_status=" & e.Row.Cells(16).Text & "&status=" & e.Row.Cells(17).Text & "'><img src='../imagens/delete.png'></a>"
            e.Row.Cells(16).Visible = False
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
        txtRazaoSocial.Text = ""
        txtNomeFantasia.Text = ""
        txtCNPJ.Text = ""
        txtIE.Text = ""
        txtIM.Text = ""
        txtEmail.Text = ""
        carrega_cmbStatus()
        txtCEP.Text = ""
        txtEndereco.Text = ""
        txtCidade.Text = ""
        txtUF.Text = ""
        txtTelefone.Text = ""
        txtNome.Text = ""
        txtDepartamento.Text = ""
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
        Dim objEmpresa As New MODEL.Empresa
        Dim objEmpresaBLL As New BLL.EmpresaBLL

        If txtRazaoSocial.Text = "" Or txtCNPJ.Text = "" Or cmbStatus.SelectedItem.Text = "" Or cmbStatus.SelectedItem.Text = "<Selecione>" Then
            lblMsg.Text = "Os campos com * são de preenchimento obrigatório!"
            lblMsg.ForeColor = Drawing.Color.Red
            pnlMsg.Visible = True
            Exit Sub
        Else
            pnlMsg.Visible = False
        End If

        With objEmpresa
            If txtCodigo.Text <> "" Then
                .cd_empresa = txtCodigo.Text
            End If
            .dc_razao_social = txtRazaoSocial.Text
            .dc_nome_fantasia = txtNomeFantasia.Text
            .nm_cnpj = txtCNPJ.Text
            .nm_inscricao_estadual = txtIE.Text
            .nm_inscricao_municipal = txtIM.Text
            .dc_email = txtEmail.Text
            .nome_cobranca = txtNome.Text
            .departamento = txtDepartamento.Text
            .cd_status = cmbStatus.SelectedValue
            .nm_cep_cobranca = txtCEP.Text
            .dc_endereco_cobranca = txtEndereco.Text
            .dc_cidade_cobranca = txtCidade.Text
            .dc_uf_cobranca = txtUF.Text
            .nm_telefone = txtTelefone.Text
            .no_userid = Session("sessionUser")
        End With


        If btnNovo.Enabled = False Or txtCodigo.Text = "" Then
            objEmpresaBLL.InsereEmpresa(objEmpresa)
            carregaGridEmpresa()
            btnCancelar_Click(sender, e)

            lblMsg.Text = "Empresa cadastrda com sucesso!"
            lblMsg.ForeColor = Drawing.Color.LightGreen
            pnlMsg.Visible = True
        Else
            objEmpresaBLL.ALteraEmpresa(objEmpresa)
            carregaGridEmpresa()

            btnCancelar_Click(sender, e)

            lblMsg.Text = "Empresa alterada com sucesso!"
            lblMsg.ForeColor = Drawing.Color.LightGreen
            pnlMsg.Visible = True
        End If


    End Sub

    Protected Sub btnSim_Click(sender As Object, e As EventArgs) Handles btnSim.Click
        Dim objEmpresaBLL As New BLL.EmpresaBLL

        objEmpresaBLL.ExcluirEmpresa(CInt(Request.QueryString("codigo").ToString))
        carregaGridEmpresa()
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
        carregaGridEmpresa()
    End Sub

    Private Sub habilitaCampos()
        txtRazaoSocial.Enabled = True
        txtNomeFantasia.Enabled = True
        txtCNPJ.Enabled = True
        txtIE.Enabled = True
        txtIM.Enabled = True
        txtEmail.Enabled = True
        cmbStatus.Enabled = True
        txtCEP.Enabled = True
        txtEndereco.Enabled = True
        txtCidade.Enabled = True
        txtUF.Enabled = True
        txtTelefone.Enabled = True
        txtNome.Enabled = True
        txtDepartamento.Enabled = True
    End Sub

    Private Sub desabilitaCampos()
        txtRazaoSocial.Enabled = False
        txtNomeFantasia.Enabled = False
        txtCNPJ.Enabled = False
        txtIE.Enabled = False
        txtIM.Enabled = False
        txtEmail.Enabled = False
        cmbStatus.Enabled = False
        txtCEP.Enabled = False
        txtEndereco.Enabled = False
        txtCidade.Enabled = False
        txtUF.Enabled = False
        txtTelefone.Enabled = False
        txtNome.Enabled = False
        txtDepartamento.Enabled = False
    End Sub
End Class