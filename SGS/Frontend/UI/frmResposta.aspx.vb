Public Class frmResposta
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sessionUser") = "" Or Session("sessionUser") = Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

        'desabilitaCampos()


        If Not IsPostBack Then

            carregagridQuestao()


            If Session("codPerfil") = 2 Then
                frameFiltro.Visible = True
                carrega_cmbArea()
                panelBotoes.Visible = False
            End If

            If Not Request.QueryString.Item("editar") Is Nothing Then
                If Request.QueryString("editar").ToString = "1" Then

                    habilitaEdicao()

                    lblCodQuestionario.Text = Request.QueryString("codQuestionario").ToString

                    lblOrdem.Text = Request.QueryString("ordem").ToString
                    lblQuestao.Text = Request.QueryString("questao").ToString

                    If Request.QueryString("retorno").ToString <> "" Then
                        frameRetorno.Visible = True
                        lblRetorno.Text = Request.QueryString("retorno").ToString
                    End If

                    If Request.QueryString("tipo").ToString.Equals("I") Then
                        frameResposta.Visible = True
                        frameItem.Visible = False

                        If Request.QueryString("codStatus").ToString <> 4 Then
                            carrega_resposta(Request.QueryString("codQuestionario").ToString)
                        End If

                    ElseIf Request.QueryString("tipo").ToString.Equals("Q") Then
                        frameResposta.Visible = False
                        frameItem.Visible = True
                        'carrega_gridItemQuestao(Request.QueryString("codQuestionario").ToString)
                        carrega_cmbItemQuestao(lblCodQuestionario.Text)

                        carrega_gridItemResposta(lblCodQuestionario.Text)

                        If Not Request.QueryString.Item("coditem") Is Nothing Then
                            lblCodigoItem.Text = Request.QueryString.Item("coditem").ToString
                            txtRespostaItem.Text = Request.QueryString.Item("respostaitem").ToString
                            cmbItemQuestao.SelectedValue = Request.QueryString.Item("coditemresposta").ToString
                        End If
                    End If
                End If
            End If


            If Not Request.QueryString.Item("pesquisar") Is Nothing Then
                If Request.QueryString("pesquisar").ToString = "1" Then

                    habilitaPesquisa()

                    If Request.QueryString("area").ToString <> 0 Then
                        cmbArea.SelectedValue = Request.QueryString("area").ToString
                    End If

                    carregagridQuestao()

                    lblCodQuestionario.Text = Request.QueryString("codQuestionario").ToString

                    lblOrdem.Text = Request.QueryString("ordem").ToString
                    lblQuestao.Text = Request.QueryString("questao").ToString

                    If Request.QueryString("retorno").ToString <> " " Then
                        frameRetorno.Visible = True
                        lblRetorno.Text = Request.QueryString("retorno").ToString
                    End If

                    If Request.QueryString("tipo").ToString.Equals("I") Then
                        frameResposta.Visible = True
                        frameItem.Visible = False

                        carrega_resposta(Request.QueryString("codQuestionario").ToString)

                    ElseIf Request.QueryString("tipo").ToString.Equals("Q") Then
                        frameResposta.Visible = False
                        frameItem.Visible = True
                        'carrega_gridItemQuestao(Request.QueryString("codQuestionario").ToString)
                        carrega_cmbItemQuestao(lblCodQuestionario.Text)

                        carrega_gridItemResposta(lblCodQuestionario.Text)

                        If Not Request.QueryString.Item("coditem") Is Nothing Then
                            lblCodigoItem.Text = Request.QueryString.Item("coditem").ToString
                            txtRespostaItem.Text = Request.QueryString.Item("respostaitem").ToString
                            cmbItemQuestao.SelectedValue = Request.QueryString.Item("coditemresposta").ToString
                        End If
                    End If
                End If
            End If

            If Not Request.QueryString.Item("excluir") Is Nothing Then
                If Request.QueryString("excluir").ToString = "1" Then

                    lblCodQuestionario.Text = Request.QueryString("codQuestionario").ToString

                    lblOrdem.Text = Request.QueryString("ordem").ToString
                    lblQuestao.Text = Request.QueryString("questao").ToString

                    If Request.QueryString("retorno").ToString <> "" Then
                        frameRetorno.Visible = True
                        lblRetorno.Text = Request.QueryString("retorno").ToString
                    End If

                    carrega_cmbItemQuestao(lblCodQuestionario.Text)

                    If Not Request.QueryString.Item("coditem") Is Nothing Then
                        lblCodigoItem.Text = Request.QueryString.Item("coditem").ToString
                        txtRespostaItem.Text = Request.QueryString.Item("respostaitem").ToString
                        cmbItemQuestao.SelectedValue = Request.QueryString.Item("coditemresposta").ToString

                        pnlMsg.Visible = False

                        pnlExcluirItem.Visible = True
                        pnlExcluirItem.Focus()

                        frameQuestao.Visible = True
                        frameItem.Visible = True
                        carrega_gridItemResposta(lblCodQuestionario.Text)

                        habilitaCampos()

                        Exit Sub
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub carrega_cmbItemQuestao(codQuestionario As Integer)
        Dim objQuestaoBLL As New BLL.QuestaoBLL
        Dim lista As New ListItem

        cmbItemQuestao.DataTextField = "Item"
        cmbItemQuestao.DataValueField = "Codigo"
        cmbItemQuestao.DataSource = objQuestaoBLL.ListaItemQuestao(0, codQuestionario).Tables(0)
        cmbItemQuestao.DataBind()

        lista.Text = "<Selecione>"
        lista.Value = 0
        cmbItemQuestao.Items.Insert(0, lista)
    End Sub

    Private Sub carrega_gridItemResposta(codQuestionario As Integer)
        Dim objRespostaBLL As New BLL.RespostaBLL
        Dim ds As DataSet
        Dim dt As DataTable
        Dim dv As DataView

        ds = objRespostaBLL.ListaItemResposta(codQuestionario)
        dv = ds.Tables(0).DefaultView
        dt = ds.Tables(0)
        gridItemResposta.DataSource = dt
        gridItemResposta.DataBind()
    End Sub

    Private Sub gridItemResposta_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridItemResposta.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = ""
            e.Row.Cells(1).Text = ""
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(6).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = "<a href='frmResposta.aspx?editar=1&coditem=" & e.Row.Cells(2).Text & "&item=" & e.Row.Cells(3).Text & "&coditemresposta=" & e.Row.Cells(4).Text & "&respostaitem=" & e.Row.Cells(5).Text & "&codQuestionario=" & e.Row.Cells(6).Text & "&ordem=" & lblOrdem.Text & "&questao=" & lblQuestao.Text & "&retorno=" & lblRetorno.Text & "&tipo=Q" & "'><img src='../imagens/edit.png'></a>"
            e.Row.Cells(1).Text = "<a href='frmResposta.aspx?excluir=1&coditem=" & e.Row.Cells(2).Text & "&item=" & e.Row.Cells(3).Text & "&coditemresposta=" & e.Row.Cells(4).Text & "&respostaitem=" & e.Row.Cells(5).Text & "&codQuestionario=" & e.Row.Cells(6).Text & "&ordem=" & lblOrdem.Text & "&questao=" & lblQuestao.Text & "&retorno=" & lblRetorno.Text & "&tipo=Q" & "'><img src='../imagens/delete.png'></a>"
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(6).Visible = False
        End If

    End Sub

    Private Sub carregagridQuestao()
        Dim objQuestionarioBLL As New BLL.QuestionarioBLL
        Dim parametros() As String = {"cd_perfil", "cd_usuario", "cd_empresa", "cd_representante"}
        Dim ds As DataSet
        Dim dt As DataTable

        parametros(0) = Session("codPerfil")
        parametros(1) = Session("codUsuario")
        parametros(2) = Session("codEmpresa")
        If cmbArea.SelectedValue <> "" Then
            parametros(3) = cmbArea.SelectedValue
        Else
            parametros(3) = 0
        End If

        ds = objQuestionarioBLL.RetornaQuestionarioRepresentante(parametros)
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            divLegenda.Visible = True
        Else
            divLegenda.Visible = False
        End If

        gridQuestao.DataSource = dt

        gridQuestao.DataBind()
    End Sub

    Private Sub gridQuestao_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridQuestao.RowDataBound
        Dim temp As String

        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = ""
            e.Row.Cells(1).Text = ""
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(7).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            temp = e.Row.Cells(5).Text

            e.Row.Cells(5).Text = "<div style='width:610px; white-space:pre-wrap;'>" & temp & "</div>"

            If Session("codPerfil") = 1 Then
                Select Case e.Row.Cells(7).Text
                    Case 4 'Aguardando Resposta 
                        e.Row.Cells(0).Text = "<img src='../imagens/Flag_vermelha.png'>"
                        e.Row.Cells(1).Text = "<a href='frmResposta.aspx?editar=1&codQuestionario=" & e.Row.Cells(2).Text & "&ordem=" & e.Row.Cells(3).Text & "&codQuestao=" & e.Row.Cells(4).Text & "&questao=" & temp & "&tipo=" & e.Row.Cells(6).Text & "&codStatus=" & e.Row.Cells(7).Text & "&retorno=" & "&area=" & 0 & "'><img src='../imagens/edit.png'></a>"
                    Case 7 'Aguardando Retorno
                        e.Row.Cells(0).Text = "<img src='../imagens/Flag_vermelha.png'>"
                        e.Row.Cells(1).Text = "<a href='frmResposta.aspx?editar=1&codQuestionario=" & e.Row.Cells(2).Text & "&ordem=" & e.Row.Cells(3).Text & "&codQuestao=" & e.Row.Cells(4).Text & "&questao=" & temp & "&tipo=" & e.Row.Cells(6).Text & "&codStatus=" & e.Row.Cells(7).Text & "&retorno=" & "&area=" & 0 & "'><img src='../imagens/edit.png'></a>"
                    Case 5 'Respondido
                        e.Row.Cells(0).Text = "<img src='../imagens/Flag_verde.png'>"
                        e.Row.Cells(1).Text = "<a href='frmResposta.aspx?editar=1&codQuestionario=" & e.Row.Cells(2).Text & "&ordem=" & e.Row.Cells(3).Text & "&codQuestao=" & e.Row.Cells(4).Text & "&questao=" & temp & "&tipo=" & e.Row.Cells(6).Text & "&codStatus=" & e.Row.Cells(7).Text & "&retorno=" & "&area=" & 0 & "'><img src='../imagens/edit.png'></a>"
                    Case 6 'Aguardando Analise
                        e.Row.Cells(0).Text = "<img src='../imagens/Flag_amarela.png'>"
                        e.Row.Cells(1).Text = "<a href='frmResposta.aspx?pesquisar=1&codQuestionario=" & e.Row.Cells(2).Text & "&ordem=" & e.Row.Cells(3).Text & "&codQuestao=" & e.Row.Cells(4).Text & "&questao=" & temp & "&tipo=" & e.Row.Cells(6).Text & "&codStatus=" & e.Row.Cells(7).Text & "&retorno=" & "&area=" & 0 & "'><img src='../imagens/find.ico'></a>"
                    Case 9 'Finalizado
                        e.Row.Cells(0).Text = "<img src='../imagens/Flag_azul.png'>"
                        e.Row.Cells(1).Text = "<a href='frmResposta.aspx?pesquisar=1&codQuestionario=" & e.Row.Cells(2).Text & "&ordem=" & e.Row.Cells(3).Text & "&codQuestao=" & e.Row.Cells(4).Text & "&questao=" & temp & "&tipo=" & e.Row.Cells(6).Text & "&codStatus=" & e.Row.Cells(7).Text & "&retorno=" & e.Row.Cells(8).Text & "&area=" & 0 & "'><img src='../imagens/find.ico'></a>"
                End Select
            Else
                Select Case e.Row.Cells(7).Text
                    Case 4 'Aguardando Resposta 
                        e.Row.Cells(0).Text = "<img src='../imagens/Flag_vermelha.png'>"
                    Case 7 'Aguardando Retorno
                        e.Row.Cells(0).Text = "<img src='../imagens/Flag_vermelha.png'>"
                    Case 5 'Respondido
                        e.Row.Cells(0).Text = "<img src='../imagens/Flag_verde.png'>"
                    Case 6 'Aguardando Analise
                        e.Row.Cells(0).Text = "<img src='../imagens/Flag_amarela.png'>"
                    Case 9 'Finalizado
                        e.Row.Cells(0).Text = "<img src='../imagens/Flag_azul.png'>"
                End Select

                e.Row.Cells(1).Text = "<a href='frmResposta.aspx?pesquisar=1&codQuestionario=" & e.Row.Cells(2).Text & "&ordem=" & e.Row.Cells(3).Text & "&codQuestao=" & e.Row.Cells(4).Text & "&questao=" & temp & "&tipo=" & e.Row.Cells(6).Text & "&codStatus=" & e.Row.Cells(7).Text & "&retorno=" & e.Row.Cells(8).Text & "&area=" & cmbArea.SelectedValue & "'><img src='../imagens/find.ico'></a>"
            End If

            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(7).Visible = False
        End If
    End Sub

    Private Sub btnGravar_Click(sender As Object, e As System.EventArgs) Handles btnGravar.Click
        Dim objRespostaBLL As New BLL.RespostaBLL
        Dim objQuestionarioBLL As New BLL.QuestionarioBLL
        Dim objResposta As New MODEL.Resposta
        Dim respondido As Boolean

        If txtResposta.Visible = True And txtResposta.Text = "" Then

            lblMsg.Text = "Os campos com * são de preenchimento obrigatório!"
            lblMsg.ForeColor = Drawing.Color.Red
            pnlMsg.Visible = True
            Exit Sub
        Else
            pnlMsg.Visible = False
        End If

        If txtResposta.Visible = True Then

            With objResposta
                If lblCodigoResposta.Text <> "" Then
                    .cd_resposta = lblCodigoResposta.Text
                End If

                .dc_resposta = txtResposta.Text
                .questionario.cd_questionario = Request.QueryString("codQuestionario").ToString
                .no_userid = Session("sessionUser")
            End With

            If Request.QueryString("codStatus").ToString = 4 Then
                If objRespostaBLL.InsereResposta(objResposta) Then
                    objQuestionarioBLL.AlteraQuestionario(Request.QueryString("codQuestionario").ToString, 5)

                    lblMsg.Text = "Resposta cadastrada com sucesso!"
                    lblMsg.ForeColor = Drawing.Color.LightGreen
                    pnlMsg.Visible = True
                    btnCancelar_Click(sender, e)
                End If
            ElseIf Request.QueryString("codStatus").ToString = 5 Then
                If objRespostaBLL.AlteraResposta(objResposta) Then
                    lblMsg.Text = "Resposta alterada com sucesso!"
                    lblMsg.ForeColor = Drawing.Color.LightGreen
                    pnlMsg.Visible = True
                    btnCancelar_Click(sender, e)
                End If
            End If
        Else
            If gridItemResposta.Rows.Count > 0 Then
                objQuestionarioBLL.AlteraQuestionario(Request.QueryString("codQuestionario").ToString, 5)

                lblMsg.Text = "Resposta cadastrada com sucesso!"
                lblMsg.ForeColor = Drawing.Color.LightGreen
                pnlMsg.Visible = True
                btnCancelar_Click(sender, e)
            Else
                lblMsg.Text = "O questionário deve ser todo respondido!"
                lblMsg.ForeColor = Drawing.Color.Red
                pnlMsg.Visible = True
            End If
        End If

            For i = 0 To gridQuestao.Rows.Count - 1
                If gridQuestao.Rows(i).Cells(7).Text = 4 Or gridQuestao.Rows(i).Cells(7).Text = 7 Then
                    respondido = False
                    Exit For
                Else
                    respondido = True
                End If
            Next

            If respondido Then
                pnlMsg.Visible = False

                pnlFinalizar.Visible = True
            End If

    End Sub

    Protected Sub btnGravaItem_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnGravaItem.Click
        Dim objItemResposta As New MODEL.ItemResposta
        Dim objRespostaBLL As New BLL.RespostaBLL
        Dim objQuestionarioBLL As New BLL.QuestionarioBLL
        Dim dt As DataTable
        Dim respondido As Boolean

        With objItemResposta
            If lblCodigoItem.Text <> "" Then
                .cd_item_resposta = lblCodigoItem.Text
            End If

            .questionario.cd_questionario = lblCodQuestionario.Text
            .itemQuestao.cd_item_questao = cmbItemQuestao.SelectedValue
            .dc_resposta_item = txtRespostaItem.Text
            .no_userid = Session("sessionUser")
        End With

        If lblCodigoItem.Text = "" Then
            If objRespostaBLL.InsereItemResposta(objItemResposta) Then                
                lblMsg.Text = "Item de Resposta cadastrado com sucesso!"
                lblMsg.ForeColor = Drawing.Color.LightGreen
                pnlMsg.Visible = True
            End If
        Else
            If objRespostaBLL.AlteraItemResposta(objItemResposta) Then                
                lblMsg.Text = "Item de Resposta alterado com sucesso!"
                lblMsg.ForeColor = Drawing.Color.LightGreen
                pnlMsg.Visible = True
            End If
        End If

        txtRespostaItem.Text = ""
        carrega_gridItemResposta(lblCodQuestionario.Text)

        For i = 0 To gridQuestao.Rows.Count - 1
            If gridQuestao.Rows(i).Cells(7).Text = 4 Or gridQuestao.Rows(i).Cells(7).Text = 7 Then
                respondido = False
                Exit For
            Else
                respondido = True
            End If
        Next

        If respondido Then
            pnlMsg.Visible = False

            pnlFinalizar.Visible = True
        End If

    End Sub

    Protected Sub btnNaoItem_Click(sender As Object, e As EventArgs) Handles btnNaoItem.Click
        lblCodigoItem.Text = ""
        txtRespostaItem.Text = ""
        carrega_cmbItemQuestao(lblCodQuestionario.Text)
        pnlExcluirItem.Visible = False
        gridItemResposta.Focus()
    End Sub

    Protected Sub btnSimItem_Click(sender As Object, e As EventArgs) Handles btnSimItem.Click
        Dim objItemRespostaBLL As New BLL.RespostaBLL

        objItemRespostaBLL.ExcluirItemQuestao(CInt(lblCodigoItem.Text))        
        carrega_gridItemResposta(lblCodQuestionario.Text)
        pnlExcluirItem.Visible = False
        lblCodigoItem.Text = ""
        txtRespostaItem.Text = ""
        gridItemResposta.Focus()
    End Sub

    Protected Sub btnFinalizar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnFinalizar.Click
        If gridQuestao.Rows.Count <= 0 Then
            Exit Sub
        End If

        'For i = 0 To gridQuestao.Rows.Count - 1
        '    If gridQuestao.Rows(i).Cells(7).Text = 4 Or gridQuestao.Rows(i).Cells(7).Text = 7 Then

        '        lblMsg.Text = "O questionário deve ser todo respondido!"
        '        lblMsg.ForeColor = Drawing.Color.Red
        '        pnlMsg.Visible = True
        '        Exit Sub
        '    Else
        '        pnlMsg.Visible = False

        '        pnlFinalizar.Visible = True
        '    End If
        'Next

        pnlMsg.Visible = False

        pnlFinalizar.Visible = True

    End Sub

    Protected Sub btnNao_Click(sender As Object, e As EventArgs) Handles btnNao.Click
        pnlMsg.Visible = False
        pnlFinalizar.Visible = False
    End Sub

    Protected Sub btnSim_Click(sender As Object, e As EventArgs) Handles btnSim.Click
        Dim objQuestionario As New MODEL.Questionario
        Dim objQuestionarioBLL As New BLL.QuestionarioBLL

        pnlFinalizar.Visible = False

        objQuestionario.representante.cd_representante = Session("codRepresentante")
        objQuestionario.representante.dc_email = Session("email")
        objQuestionario.representante.no_representante = Session("nome")
        objQuestionario.representante.dc_area = Session("area")

        If objQuestionarioBLL.EnviaEmailQuestionarioRespondido(objQuestionario) Then

            objQuestionarioBLL.AlteraQuestionario(0, 6, objQuestionario.representante.cd_representante)

            lblMsg.Text = "Questionário finalizado com sucesso!"
            lblMsg.ForeColor = Drawing.Color.LightGreen
            pnlMsg.Visible = True
        Else
            lblMsg.Text = "Não foi possível finalizar o questionário."
            lblMsg.ForeColor = Drawing.Color.Red
            pnlMsg.Visible = True
        End If

        limpaCampos()
        desabilitaCampos()
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

        btnFinalizar.Enabled = True
        btnFinalizar.ImageUrl = "../imagens/accept.png"

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

        btnFinalizar.Enabled = True
        btnFinalizar.ImageUrl = "../imagens/accept_disabled.png"
    End Sub

    Private Sub limpaCampos()
        lblQuestao.Text = ""
        txtResposta.Text = ""
        txtRespostaItem.Text = ""
        lblRetorno.Text = ""
        'cmbItemQuestao.SelectedValue = 0
        frameQuestao.Visible = False
        frameRetorno.Visible = False
        frameResposta.Visible = False
        frameItem.Visible = False
        carregagridQuestao()
    End Sub

    Private Sub habilitaEdicao()
        limpaCampos()
        frameQuestao.Visible = True

        txtResposta.Enabled = True

        btnNovo.Enabled = False
        btnNovo.ImageUrl = "../imagens/add_disabled.png"

        btnGravar.Enabled = True
        btnGravar.ImageUrl = "../imagens/save.ico"

        btnCancelar.Enabled = True
        btnCancelar.ImageUrl = "../imagens/no.ico"

        btnFinalizar.Enabled = True
        btnFinalizar.ImageUrl = "../imagens/accept.png"

        pnlMsg.Visible = False
    End Sub

    Private Sub habilitaPesquisa()
        limpaCampos()
        frameQuestao.Visible = True

        txtResposta.Enabled = False

        btnNovo.Enabled = False
        btnNovo.ImageUrl = "../imagens/add_disabled.png"

        btnCancelar.Enabled = True
        btnCancelar.ImageUrl = "../imagens/no.ico"

        pnlMsg.Visible = False
    End Sub

    Private Sub habilitaCampos()

    End Sub

    Private Sub desabilitaCampos()
        'frameItem.Visible = False
    End Sub

    Private Sub carrega_resposta(codQuestionario As Integer)
        Dim objRespostaBLL As New BLL.RespostaBLL
        Dim dt As DataTable

        dt = objRespostaBLL.RetornaResposta(codQuestionario).Tables(0)

        txtResposta.Text = dt.Rows(0)("Resposta").ToString
        lblCodigoResposta.Text = dt.Rows(0)("codResposta").ToString

    End Sub

    Private Sub carrega_cmbArea()
        Dim objRepresentanteBLL As New BLL.RepresentanteBLL
        Dim lista As New ListItem

        cmbArea.DataTextField = "Area"
        cmbArea.DataValueField = "Cod. Representante"
        cmbArea.DataSource = objRepresentanteBLL.ListaArea(Session("codEmpresa")).Tables(0)
        cmbArea.DataBind()

        lista.Text = "<Selecione>"
        lista.Value = 0
        cmbArea.Items.Insert(0, lista)
    End Sub

    Protected Sub cmbArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbArea.SelectedIndexChanged
        carregagridQuestao()
    End Sub
End Class