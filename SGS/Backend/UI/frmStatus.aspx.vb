Public Class frmStatus
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sessionUser") = "" Or Session("sessionUser") = Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

        desabilitaCampos()

        If Not IsPostBack Then
            carregaGridStatus()

            If Not Request.QueryString.Item("editar") Is Nothing Then
                If Request.QueryString("editar").ToString = "1" Then

                    txtCodigo.Text = Request.QueryString("codigo").ToString
                    txtStatus.Text = Request.QueryString("status").ToString
                    cmbTipo.SelectedItem.Text = Request.QueryString("tipo").ToString

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
                    txtStatus.Text = Request.QueryString("status").ToString
                    cmbTipo.SelectedItem.Text = Request.QueryString("tipo").ToString

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

    Private Sub carregaGridStatus()
        Dim objStatusBLL As New BLL.StatusBLL
        Dim ds As DataSet
        Dim dt As DataTable
        Dim dv As DataView

        ds = objStatusBLL.ListaStatus()
        dv = ds.Tables(0).DefaultView
        dt = ds.Tables(0)

        gridStatus.DataSource = dt

        If cmbFiltro.Text = "Código" Then
            dv.RowFilter = "Código = '" & txtFiltro.Text & "'"
        End If

        If cmbFiltro.Text = "Usuário" Then
            dv.RowFilter = "[Status] like '%" & txtFiltro.Text & "%'"
        End If

        If cmbFiltro.Text = "Tipo" Then
            dv.RowFilter = "[Tipo] = '" & txtFiltro.Text & "'"
        End If

        gridStatus.DataBind()
    End Sub

    Protected Sub btnGravar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnGravar.Click
        Dim objStatus As New MODEL.Status
        Dim objStatusBLL As New BLL.StatusBLL

        If txtStatus.Text = "" Then
            lblMsg.Text = "Os campos com * são de preenchimento obrigatório!"
            lblMsg.ForeColor = Drawing.Color.Red
            pnlMsg.Visible = True
            habilitaCampos()
            Exit Sub
        Else
            pnlMsg.Visible = False
        End If

        With objStatus
            If txtCodigo.Text <> "" Then
                .cd_status = txtCodigo.Text
            End If
            .dc_status = txtStatus.Text
            .dc_tipo = cmbTipo.SelectedValue
        End With

        If btnNovo.Enabled = False Or txtCodigo.Text = "" Then
            objStatusBLL.InsereStatus(objStatus)
            carregaGridStatus()
            btnCancelar_Click(sender, e)

            lblMsg.Text = "Status cadastrado com sucesso!"
            lblMsg.ForeColor = Drawing.Color.LightGreen
            pnlMsg.Visible = True
        Else
            objStatusBLL.AlteraStatus(objStatus)
            carregaGridStatus()

            btnCancelar_Click(sender, e)

            lblMsg.Text = "Status alterado com sucesso!"
            lblMsg.ForeColor = Drawing.Color.LightGreen
            pnlMsg.Visible = True
        End If
    End Sub

    Protected Sub btnSim_Click(sender As Object, e As EventArgs) Handles btnSim.Click
        Dim objStatusBLL As New BLL.StatusBLL

        objStatusBLL.ExcluiStatus(CInt(Request.QueryString("codigo").ToString))
        carregaGridStatus()
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
        carregaGridStatus()
    End Sub

    Private Sub gridStatus_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridStatus.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = ""
            e.Row.Cells(1).Text = ""
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = "<a href='frmStatus.aspx?editar=1&codigo=" & e.Row.Cells(2).Text & "&status=" & e.Row.Cells(3).Text & "&tipo=" & e.Row.Cells(4).Text & "'><img src='../imagens/edit.png'></a>"
            e.Row.Cells(1).Text = "<a href='frmStatus.aspx?excluir=1&codigo=" & e.Row.Cells(2).Text & "&status=" & e.Row.Cells(3).Text & "&tipo=" & e.Row.Cells(4).Text & "'><img src='../imagens/delete.png'></a>"
        End If
    End Sub

    Private Sub limpaCampos()
        txtCodigo.Text = ""
        txtStatus.Text = ""
        cmbTipo.Enabled = True
        pnlMsg.Visible = False
    End Sub

    Private Sub habilitaCampos()
        txtStatus.Enabled = True
        cmbTipo.Enabled = True
    End Sub

    Private Sub desabilitaCampos()
        txtStatus.Enabled = False
        cmbTipo.Enabled = False
    End Sub

End Class