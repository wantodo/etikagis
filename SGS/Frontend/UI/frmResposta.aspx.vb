Public Class frmResposta
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sessionUser") = "" Or Session("sessionUser") = Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

        If Not IsPostBack Then
            carregagridQuestao()

            If Not Request.QueryString.Item("editar") Is Nothing Then
                If Request.QueryString("editar").ToString = "1" Then
                    lblQuestao.Text = Request.QueryString("questao").ToString

                    If Request.QueryString("tipo").ToString.Equals("I") Then
                        frameResposta.Visible = True
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub carregagridQuestao()
        Dim objRespostaBLL As New BLL.RespostaBLL
        Dim objResposta As New MODEL.Resposta
        Dim ds As DataSet
        Dim dt As DataTable

        objResposta.cd_acesso = Session("acesso")
        objResposta.cd_usuario = Session("codUsuario")
        objResposta.cd_empresa = Session("codEmpresa")

        ds = objRespostaBLL.ListaResposta(objResposta)
        dt = ds.Tables(0)

        gridQuestao.DataSource = dt

        gridQuestao.DataBind()
    End Sub

    Private Sub gridQuestao_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridQuestao.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = ""
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = "<a href='frmResposta.aspx?editar=1&ordem=" & e.Row.Cells(1).Text & "&codQuestao=" & e.Row.Cells(2).Text & "&questao=" & e.Row.Cells(3).Text & "&tipo=" & e.Row.Cells(4).Text & "'><img src='../imagens/edit.png'></a>"
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
        End If
    End Sub
End Class