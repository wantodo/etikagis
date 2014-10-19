Public Class frmResposta
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sessionUser") = "" Or Session("sessionUser") = Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

        If Not IsPostBack Then
            carregagridQuestao()
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

        gridQuestao.DataBind()
    End Sub

End Class