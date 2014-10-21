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

                    lblCodQuestionario.Text = Request.QueryString("codQuestionario").ToString
                    lblQuestao.Text = Request.QueryString("questao").ToString

                    If Request.QueryString("tipo").ToString.Equals("I") Then
                        frameResposta.Visible = True
                        frameItem.Visible = False
                    ElseIf Request.QueryString("tipo").ToString.Equals("Q") Then
                        frameResposta.Visible = False
                        frameItem.Visible = True
                        carrega_gridItemQuestao(Request.QueryString("codQuestao").ToString)
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub carrega_gridItemQuestao(codQuestao As Integer)
        Dim objQuestaoBLL As New BLL.QuestaoBLL
        Dim ds As DataSet
        Dim dt As DataTable
        Dim dv As DataView

        ds = objQuestaoBLL.ListaItemQuestao(codQuestao)
        dv = ds.Tables(0).DefaultView
        dt = ds.Tables(0)
        gridItemQuestao.DataSource = dt

        gridItemQuestao.DataBind()
    End Sub

    Private Sub carregagridQuestao()
        Dim objRespostaBLL As New BLL.RespostaBLL
        Dim parametros() As String = {"cd_acesso", "cd_usuario", "cd_empresa"}
        Dim ds As DataSet
        Dim dt As DataTable

        parametros(0) = Session("acesso")
        parametros(1) = Session("codUsuario")
        parametros(2) = Session("codEmpresa")

        ds = objRespostaBLL.ListaResposta(parametros)
        dt = ds.Tables(0)

        gridQuestao.DataSource = dt

        gridQuestao.DataBind()
    End Sub

    Private Sub gridQuestao_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridQuestao.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = ""
            e.Row.Cells(1).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(5).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = "<a href='frmResposta.aspx?editar=1&codQuestionario=" & e.Row.Cells(1).Text & "&ordem=" & e.Row.Cells(2).Text & "&codQuestao=" & e.Row.Cells(3).Text & "&questao=" & e.Row.Cells(4).Text & "&tipo=" & e.Row.Cells(5).Text & "'><img src='../imagens/edit.png'></a>"
            e.Row.Cells(1).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(5).Visible = False
        End If
    End Sub

    Private Sub gridItemQuestao_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridItemQuestao.RowDataBound
        Dim lb As Label
        Dim tx As TextBox

        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False

            lb = e.Row.Cells(0).FindControl("lblItem")
            lb.Text = e.Row.Cells(3).Text

            tx = e.Row.Cells(0).FindControl("txtResposta")
            'tx.Text = e.Row.Cells(2).Text


        End If

    End Sub

    Private Sub btnGravar_Click(sender As Object, e As System.EventArgs) Handles btnGravar.Click        
        Dim tx As TextBox
        Dim objRespostaBLL As New BLL.RespostaBLL
        Dim objResposta As New MODEL.Resposta

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
                .dc_resposta = txtResposta.Text
                .questionario.cd_questionario = lblCodQuestionario.Text                
                .no_userid = Session("sessionUser")
            End With

            objRespostaBLL.InsereResposta(objResposta)

        Else
            For i As Integer = 0 To gridItemQuestao.Rows.Count - 1

                tx = gridItemQuestao.Rows(i).Cells(0).FindControl("txtResposta")

                With objResposta
                    .dc_resposta = tx.Text
                    .questionario.cd_questionario = lblCodQuestionario.Text
                    .item.cd_item_questao = gridItemQuestao.Rows(i).Cells(2).Text
                    .no_userid = Session("sessionUser")
                End With

                objRespostaBLL.InsereResposta(objResposta)
            Next
        End If

        lblMsg.Text = "Resposta cadastrada com sucesso!"
        lblMsg.ForeColor = Drawing.Color.LightGreen
        pnlMsg.Visible = True
        btnCancelar_Click(sender, e)

    End Sub

    Protected Sub btnFinalizar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnFinalizar.Click
       
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
        frameQuestao.Visible = False
        frameResposta.Visible = False
        frameItem.Visible = False
        'carregagridQuestao()
    End Sub

    Private Sub habilitaEdicao()        
        limpaCampos()
        frameQuestao.Visible = True

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

    Private Sub habilitaCampos()

    End Sub

    Private Sub desabilitaCampos()

    End Sub

End Class