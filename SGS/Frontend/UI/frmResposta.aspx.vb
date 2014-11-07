Public Class frmResposta
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sessionUser") = "" Or Session("sessionUser") = Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

        If Not IsPostBack Then

            carregagridQuestao()


            If Session("codPerfil") = 2 Then
                frameFiltro.Visible = True
                carrega_cmbArea()
                panelBotoes.Visible = False
                btnGravaItem.Visible = False
            End If

            If Not Request.QueryString.Item("editar") Is Nothing Then
                If Request.QueryString("editar").ToString = "1" Then

                    habilitaEdicao()

                    lblCodQuestionario.Text = Request.QueryString("codQuestionario").ToString

                    lblOrdem.Text = Request.QueryString("ordem").ToString
                    lblQuestao.Text = Request.QueryString("questao").ToString

                    If Trim(Request.QueryString("retorno").ToString) <> " " Then
                        frameRetorno.Visible = True
                        lblRetorno.Text = Request.QueryString("retorno").ToString
                    Else
                        frameRetorno.Visible = False
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

                        carrega_gridItemResposta(lblCodQuestionario.Text)
                    End If
                End If
            End If


            If Not Request.QueryString.Item("pesquisar") Is Nothing Then
                If Request.QueryString("pesquisar").ToString = "1" Then

                    habilitaPesquisa()

                    If Request.QueryString("codarea").ToString <> 0 Then
                        cmbArea.SelectedValue = Request.QueryString("codarea").ToString
                    End If

                    carregagridQuestao()

                    lblCodQuestionario.Text = Request.QueryString("codQuestionario").ToString

                    lblOrdem.Text = Request.QueryString("ordem").ToString
                    lblQuestao.Text = Request.QueryString("questao").ToString

                    If Trim(Request.QueryString("retorno").ToString) <> " " Then
                        frameRetorno.Visible = True
                        lblRetorno.Text = Request.QueryString("retorno").ToString
                    Else
                        frameRetorno.Visible = False
                    End If

                    If Request.QueryString("tipo").ToString.Equals("I") Then
                        frameResposta.Visible = True
                        frameItem.Visible = False

                        carrega_resposta(Request.QueryString("codQuestionario").ToString)

                    ElseIf Request.QueryString("tipo").ToString.Equals("Q") Then
                        frameResposta.Visible = False
                        frameItem.Visible = True                        

                        carrega_gridItemResposta(lblCodQuestionario.Text)

                    End If
                End If
            End If

            If Not Request.QueryString.Item("excluir") Is Nothing Then
                If Request.QueryString("excluir").ToString = "1" Then

                    lblCodQuestionario.Text = Request.QueryString("codQuestionario").ToString

                    lblOrdem.Text = Request.QueryString("ordem").ToString
                    lblQuestao.Text = Request.QueryString("questao").ToString

                    If Trim(Request.QueryString("retorno").ToString) <> " " Then
                        frameRetorno.Visible = True
                        lblRetorno.Text = Request.QueryString("retorno").ToString
                    Else
                        frameRetorno.Visible = False
                    End If

                    If Not Request.QueryString.Item("coditem") Is Nothing Then                        
                        pnlMsg.Visible = False

                        pnlExcluirItem.Visible = True
                        pnlExcluirItem.Focus()

                        frameQuestao.Visible = True
                        frameItem.Visible = True
                        carrega_gridItemResposta(lblCodQuestionario.Text)
                        Exit Sub
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub carrega_gridItemResposta(codQuestionario As Integer)
        Dim objRespostaBLL As New BLL.RespostaBLL
        Dim ds As DataSet
        Dim dt As DataTable
        Dim dv As DataView

        ds = objRespostaBLL.ListaItemRespostaTelaResposta(codQuestionario)
        dv = ds.Tables(0).DefaultView
        dt = ds.Tables(0)
        gridItemResposta.DataSource = dt
        gridItemResposta.DataBind()
    End Sub

    Private Sub gridItemResposta_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridItemResposta.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 0 To e.Row.Cells.Count - 1
                e.Row.Cells(i).Text = "<input type='text' name='txtItemResposta" & i & "' id='txtItemResposta" & i & "' size='20px' runat='server'>"
            Next
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
        If cmbArea.SelectedValue <> "" And cmbArea.SelectedValue <> "-1" Then
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

            If Session("codPerfil") = 2 Then
                e.Row.Cells(10).Visible = False
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then

            If Session("codPerfil") = 1 Then
                temp = e.Row.Cells(5).Text

                e.Row.Cells(5).Text = "<div style='width:610px; white-space:pre-wrap;'>" & temp & "</div>"

                Select Case e.Row.Cells(7).Text
                    Case 4 'Aguardando Resposta 
                        e.Row.Cells(0).Text = "<img src='../imagens/Flag-Red.png'>"
                        e.Row.Cells(1).Text = "<a href='frmResposta.aspx?editar=1&codQuestionario=" & e.Row.Cells(2).Text & "&ordem=" & e.Row.Cells(3).Text & "&codQuestao=" & e.Row.Cells(4).Text & "&questao=" & temp & "&tipo=" & e.Row.Cells(6).Text & "&codStatus=" & e.Row.Cells(7).Text & "&retorno=" & e.Row.Cells(8).Text & "&codarea=" & 0 & "'><img src='../imagens/edit.png'></a>"
                    Case 5 'Gravado
                        e.Row.Cells(0).Text = "<img src='../imagens/Flag-Blue.png'>"
                        e.Row.Cells(1).Text = "<a href='frmResposta.aspx?editar=1&codQuestionario=" & e.Row.Cells(2).Text & "&ordem=" & e.Row.Cells(3).Text & "&codQuestao=" & e.Row.Cells(4).Text & "&questao=" & temp & "&tipo=" & e.Row.Cells(6).Text & "&codStatus=" & e.Row.Cells(7).Text & "&retorno=" & e.Row.Cells(8).Text & "&codarea=" & 0 & "'><img src='../imagens/edit.png'></a>"
                    Case 6 'Respondido
                        e.Row.Cells(0).Text = "<img src='../imagens/Flag-Green.png'>"
                        e.Row.Cells(1).Text = "<a href='frmResposta.aspx?pesquisar=1&codQuestionario=" & e.Row.Cells(2).Text & "&ordem=" & e.Row.Cells(3).Text & "&codQuestao=" & e.Row.Cells(4).Text & "&questao=" & temp & "&tipo=" & e.Row.Cells(6).Text & "&codStatus=" & e.Row.Cells(7).Text & "&retorno=" & e.Row.Cells(8).Text & "&codarea=" & 0 & "'><img src='../imagens/find.ico'></a>"
                    Case 7 'Devolvido
                        e.Row.Cells(0).Text = "<img src='../imagens/Flag-Yellow.png'>"
                        e.Row.Cells(1).Text = "<a href='frmResposta.aspx?editar=1&codQuestionario=" & e.Row.Cells(2).Text & "&ordem=" & e.Row.Cells(3).Text & "&codQuestao=" & e.Row.Cells(4).Text & "&questao=" & temp & "&tipo=" & e.Row.Cells(6).Text & "&codStatus=" & e.Row.Cells(7).Text & "&retorno=" & e.Row.Cells(8).Text & "&codarea=" & 0 & "'><img src='../imagens/edit.png'></a>"
                    Case 8 'Finalizado
                        e.Row.Cells(0).Text = "<img src='../imagens/Flag-Checkered.png'>"
                        e.Row.Cells(1).Text = "<a href='frmResposta.aspx?pesquisar=1&codQuestionario=" & e.Row.Cells(2).Text & "&ordem=" & e.Row.Cells(3).Text & "&codQuestao=" & e.Row.Cells(4).Text & "&questao=" & temp & "&tipo=" & e.Row.Cells(6).Text & "&codStatus=" & e.Row.Cells(7).Text & "&retorno=" & e.Row.Cells(8).Text & e.Row.Cells(8).Text & "&codarea=" & 0 & "'><img src='../imagens/find.ico'></a>"
                End Select
            Else
                e.Row.Cells(10).Visible = False

                temp = e.Row.Cells(5).Text

                e.Row.Cells(5).Text = "<div style='width:510px; white-space:pre-wrap;'>" & temp & "</div>"

                Select Case e.Row.Cells(7).Text
                    Case 4 'Aguardando Resposta 
                        e.Row.Cells(0).Text = "<img src='../imagens/Flag-Red.png'>"
                    Case 5 'Gravado
                        e.Row.Cells(0).Text = "<img src='../imagens/Flag-Blue.png'>"
                    Case 6 'Respondido
                        e.Row.Cells(0).Text = "<img src='../imagens/Flag-Green.png'>"
                    Case 7 'Devolvido
                        e.Row.Cells(0).Text = "<img src='../imagens/Flag-Yellow.png'>"
                    Case 8 'Finalizado
                        e.Row.Cells(0).Text = "<img src='../imagens/Flag-Checkered.png'>"
                End Select

                e.Row.Cells(1).Text = "<a href='frmResposta.aspx?pesquisar=1&codQuestionario=" & e.Row.Cells(2).Text & "&ordem=" & e.Row.Cells(3).Text & "&codQuestao=" & e.Row.Cells(4).Text & "&questao=" & temp & "&tipo=" & e.Row.Cells(6).Text & "&codStatus=" & e.Row.Cells(7).Text & "&retorno=" & e.Row.Cells(8).Text & "&area=" & e.Row.Cells(9).Text & "&codarea=" & e.Row.Cells(10).Text & "'><img src='../imagens/find.ico'></a>"
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
        Dim tx As TextBox

        For i = 0 To gridItemResposta.Rows(0).Cells.Count - 1
            With objItemResposta
                'If lblCodigoItem.Text <> "" Then
                '    .cd_item_resposta = lblCodigoItem.Text
                'End If

                .questionario.cd_questionario = lblCodQuestionario.Text
                .itemQuestao.dc_item_questao = gridItemResposta.HeaderRow.Cells(i).Text
                tx = gridItemResposta.Rows(0).Cells(i).FindControl("txtItemResposta" & i)
                .dc_resposta_item = tx.Text
                .no_userid = Session("sessionUser")
            End With

            If objRespostaBLL.InsereItemResposta(objItemResposta) Then
                lblMsg.Text = "Item de Resposta cadastrado com sucesso!"
                lblMsg.ForeColor = Drawing.Color.LightGreen
                pnlMsg.Visible = True
            End If
        Next
        

        'If lblCodigoItem.Text = "" Then
        'If objRespostaBLL.InsereItemResposta(objItemResposta) Then
        '        lblMsg.Text = "Item de Resposta cadastrado com sucesso!"
        '        lblMsg.ForeColor = Drawing.Color.LightGreen
        '        pnlMsg.Visible = True
        '    End If
        'Else
        '    If objRespostaBLL.AlteraItemResposta(objItemResposta) Then
        '        lblMsg.Text = "Item de Resposta alterado com sucesso!"
        '        lblMsg.ForeColor = Drawing.Color.LightGreen
        '        pnlMsg.Visible = True
        '    End If
        'End If

        'carrega_gridItemResposta(lblCodQuestionario.Text)

    End Sub

    Protected Sub btnNaoItem_Click(sender As Object, e As EventArgs) Handles btnNaoItem.Click
        pnlExcluirItem.Visible = False
        gridItemResposta.Focus()
    End Sub

    Protected Sub btnSimItem_Click(sender As Object, e As EventArgs) Handles btnSimItem.Click
        Dim objItemRespostaBLL As New BLL.RespostaBLL

        'objItemRespostaBLL.ExcluirItemQuestao(CInt(lblCodigoItem.Text))
        carrega_gridItemResposta(lblCodQuestionario.Text)
        pnlExcluirItem.Visible = False        
        gridItemResposta.Focus()
    End Sub

    Protected Sub btnFinalizar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnFinalizar.Click
        If gridQuestao.Rows.Count <= 0 Then
            Exit Sub
        End If

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

            For i = 0 To gridQuestao.Rows.Count - 1
                If gridQuestao.Rows(i).Cells(7).Text = 5 Then
                    objQuestionarioBLL.AlteraQuestionario(gridQuestao.Rows(i).Cells(2).Text, 6)
                End If
            Next

            lblMsg.Text = "Questionário finalizado com sucesso!"
            lblMsg.ForeColor = Drawing.Color.LightGreen
            pnlMsg.Visible = True
        Else
            lblMsg.Text = "Não foi possível finalizar o questionário."
            lblMsg.ForeColor = Drawing.Color.Red
            pnlMsg.Visible = True
        End If

        limpaCampos()        
    End Sub

    Protected Sub btnNovo_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnNovo.Click
        limpaCampos()        

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
        lblRetorno.Text = ""

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
        lista.Value = -1
        cmbArea.Items.Insert(0, lista)
        'lista.Text = "Todos"
        'lista.Value = 999999
        'cmbArea.Items.Insert(1, lista)

    End Sub

    Protected Sub cmbArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbArea.SelectedIndexChanged
        carregagridQuestao()
    End Sub
End Class