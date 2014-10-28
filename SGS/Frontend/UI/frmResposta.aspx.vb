Public Class frmResposta
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sessionUser") = "" Or Session("sessionUser") = Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

        desabilitaCampos()

        If Not IsPostBack Then
            carregagridQuestao()

            If Not Request.QueryString.Item("editar") Is Nothing Then
                If Request.QueryString("editar").ToString = "1" Then

                    habilitaEdicao()

                    lblCodQuestionario.Text = Request.QueryString("ordem").ToString
                    lblQuestao.Text = Request.QueryString("questao").ToString

                    If Request.QueryString("retorno").ToString <> " " Then
                        frameRetorno.Visible = True
                        lblRetorno.Text = Request.QueryString("retorno").ToString
                    End If

                    If Request.QueryString("tipo").ToString.Equals("I") Then
                        frameResposta.Visible = True
                        frameItem.Visible = False

                        If Request.QueryString("codStatus").ToString = 5 Then
                            carrega_resposta(Request.QueryString("codQuestionario").ToString)
                        End If

                    ElseIf Request.QueryString("tipo").ToString.Equals("Q") Then
                        frameResposta.Visible = False
                        frameItem.Visible = True
                        carrega_gridItemQuestao(Request.QueryString("codQuestionario").ToString)
                    End If
                End If
            End If


            If Not Request.QueryString.Item("pesquisar") Is Nothing Then
                If Request.QueryString("pesquisar").ToString = "1" Then

                    habilitaPesquisa()

                    lblCodQuestionario.Text = Request.QueryString("ordem").ToString
                    lblQuestao.Text = Request.QueryString("questao").ToString

                    If Request.QueryString("retorno").ToString <> " " Then
                        frameRetorno.Visible = True
                        lblRetorno.Text = Request.QueryString("retorno").ToString
                    End If

                    If Request.QueryString("tipo").ToString.Equals("I") Then
                        frameResposta.Visible = True
                        frameItem.Visible = False

                        If Request.QueryString("codStatus").ToString = 5 Then
                            carrega_resposta(Request.QueryString("codQuestionario").ToString)
                        End If

                    ElseIf Request.QueryString("tipo").ToString.Equals("Q") Then
                        frameResposta.Visible = False
                        frameItem.Visible = True
                        carrega_gridItemQuestao(Request.QueryString("codQuestionario").ToString)
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub carrega_gridItemQuestao(codQuestionario As Integer)
        Dim objRespostaBLL As New BLL.RespostaBLL
        Dim ds As DataSet
        Dim dt As DataTable
        Dim dv As DataView

        ds = objRespostaBLL.ListaItemResposta(codQuestionario)
        dv = ds.Tables(0).DefaultView
        dt = ds.Tables(0)
        gridItemQuestao.DataSource = dt

        gridItemQuestao.DataBind()
    End Sub

    Private Sub gridItemQuestao_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridItemQuestao.RowDataBound
        Dim lb As Label
        Dim tx As TextBox

        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False

            lb = e.Row.Cells(0).FindControl("lblItem")
            lb.Text = e.Row.Cells(3).Text

            If e.Row.Cells(4).Text <> "&nbsp;" Then
                tx = e.Row.Cells(0).FindControl("txtResposta")
                tx.Text = e.Row.Cells(4).Text
                If Not Request.QueryString.Item("pesquisar") Is Nothing Then
                    tx.Enabled = False
                End If
            End If


        End If

    End Sub

    Private Sub carregagridQuestao()
        Dim objQuestionarioBLL As New BLL.QuestionarioBLL
        Dim parametros() As String = {"cd_acesso", "cd_usuario", "cd_empresa"}
        Dim ds As DataSet
        Dim dt As DataTable

        parametros(0) = Session("acesso")
        parametros(1) = Session("codUsuario")
        parametros(2) = Session("codEmpresa")

        ds = objQuestionarioBLL.RetornaQuestionarioRepresentante(parametros)
        dt = ds.Tables(0)

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

            Select Case e.Row.Cells(7).Text
                Case 4 Or 7 'Aguardando Resposta ou Aguardando Retorno
                    e.Row.Cells(0).Text = "<img src='../imagens/Flag_vermelha.png'>"
                    e.Row.Cells(1).Text = "<a href='frmResposta.aspx?editar=1&codQuestionario=" & e.Row.Cells(2).Text & "&ordem=" & e.Row.Cells(3).Text & "&codQuestao=" & e.Row.Cells(4).Text & "&questao=" & temp & "&tipo=" & e.Row.Cells(6).Text & "&codStatus=" & e.Row.Cells(7).Text & "&retorno=" & e.Row.Cells(8).Text & "'><img src='../imagens/edit.png'></a>"
                Case 5 'Respondido
                    e.Row.Cells(0).Text = "<img src='../imagens/Flag_verde.png'>"
                    e.Row.Cells(1).Text = "<a href='frmResposta.aspx?editar=1&codQuestionario=" & e.Row.Cells(2).Text & "&ordem=" & e.Row.Cells(3).Text & "&codQuestao=" & e.Row.Cells(4).Text & "&questao=" & temp & "&tipo=" & e.Row.Cells(6).Text & "&codStatus=" & e.Row.Cells(7).Text & "&retorno=" & e.Row.Cells(8).Text & "'><img src='../imagens/edit.png'></a>"
                Case 6 'Aguardando Analise
                    e.Row.Cells(0).Text = "<img src='../imagens/Flag_amarela.png'>"
                    e.Row.Cells(1).Text = "<a href='frmResposta.aspx?pesquisar=1&codQuestionario=" & e.Row.Cells(2).Text & "&ordem=" & e.Row.Cells(3).Text & "&codQuestao=" & e.Row.Cells(4).Text & "&questao=" & temp & "&tipo=" & e.Row.Cells(6).Text & "&codStatus=" & e.Row.Cells(7).Text & "&retorno=" & e.Row.Cells(8).Text & "'><img src='../imagens/find.ico'></a>"
                Case 9 'Finalizado
                    e.Row.Cells(0).Text = "<img src='../imagens/Flag_azul.png'>"
                    e.Row.Cells(1).Text = "<a href='frmResposta.aspx?pesquisar=1&codQuestionario=" & e.Row.Cells(2).Text & "&ordem=" & e.Row.Cells(3).Text & "&codQuestao=" & e.Row.Cells(4).Text & "&questao=" & temp & "&tipo=" & e.Row.Cells(6).Text & "&codStatus=" & e.Row.Cells(7).Text & "&retorno=" & e.Row.Cells(8).Text & "'><img src='../imagens/find.ico'></a>"
            End Select

            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(7).Visible = False
        End If
    End Sub

    Private Sub btnGravar_Click(sender As Object, e As System.EventArgs) Handles btnGravar.Click        
        Dim tx As TextBox
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
                If Session("codResposta") <> "" Then
                    .cd_resposta = Session("codResposta")
                End If

                .dc_resposta = txtResposta.Text
                .questionario.cd_questionario = Request.QueryString("codQuestionario").ToString
                .no_userid = Session("sessionUser")
            End With

            If Request.QueryString("codStatus").ToString = 4 Then
                If objRespostaBLL.InsereResposta(objResposta) Then
                    objQuestionarioBLL.AlteraQuestionario(0, Request.QueryString("codQuestionario").ToString, 5)

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
            For i As Integer = 0 To gridItemQuestao.Rows.Count - 1

                tx = gridItemQuestao.Rows(i).Cells(0).FindControl("txtResposta")

                If tx.Text = "" Then
                    lblMsg.Text = "O preenchimento dos itens é obrigatório!"
                    lblMsg.ForeColor = Drawing.Color.Red
                    pnlMsg.Visible = True
                    Exit Sub
                End If

                With objResposta
                    If gridItemQuestao.Rows(i).Cells(5).Text <> "" Then
                        .cd_resposta = gridItemQuestao.Rows(i).Cells(5).Text
                    End If
                    .dc_resposta = tx.Text
                    .questionario.cd_questionario = Request.QueryString("codQuestionario").ToString
                    .item.cd_item_questao = gridItemQuestao.Rows(i).Cells(2).Text
                    .no_userid = Session("sessionUser")
                End With

                If Request.QueryString("codStatus").ToString = 4 Then
                    If objRespostaBLL.InsereResposta(objResposta) Then
                        objQuestionarioBLL.AlteraQuestionario(0, Request.QueryString("codQuestionario").ToString, 5)

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
            Next
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

    Protected Sub btnFinalizar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnFinalizar.Click
        If gridQuestao.Rows.Count <= 0 Then
            Exit Sub
        End If

        For i = 0 To gridQuestao.Rows.Count - 1
            If gridQuestao.Rows(i).Cells(7).Text = 4 Or gridQuestao.Rows(i).Cells(7).Text = 7 Then

                lblMsg.Text = "O questionário deve ser todo respondido!"
                lblMsg.ForeColor = Drawing.Color.Red
                pnlMsg.Visible = True
                Exit Sub
            Else
                pnlMsg.Visible = False

                pnlFinalizar.Visible = True
            End If
        Next
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

            objQuestionarioBLL.AlteraQuestionario(objQuestionario.representante.cd_representante, 0, 6)

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

    Private Sub habilitaCampos()

    End Sub

    Private Sub desabilitaCampos()

    End Sub

    Private Sub carrega_resposta(codQuestionario As Integer)
        Dim objRespostaBLL As New BLL.RespostaBLL
        Dim dt As DataTable

        dt = objRespostaBLL.RetornaResposta(codQuestionario).Tables(0)

        txtResposta.Text = dt.Rows(0)("Resposta").ToString
        Session("codResposta") = dt.Rows(0)("codResposta").ToString

    End Sub



End Class