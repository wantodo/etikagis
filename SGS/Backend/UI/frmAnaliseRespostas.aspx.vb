Public Class frmAnaliseRespostas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sessionUser") = "" Or Session("sessionUser") = Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

        If Not IsPostBack Then
            carrega_cmbEmpresa()

            If Not Request.QueryString.Item("editar") Is Nothing Then
                If Request.QueryString("editar").ToString = "1" Then

                    habilitaEdicao()

                    lblCodQuestionario.Text = Request.QueryString("ordem").ToString
                    lblQuestao.Text = Request.QueryString("questao").ToString

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

    Private Sub carrega_cmbEmpresa()
        Dim objEmpresaBLL As New BLL.EmpresaBLL
        Dim lista As New ListItem

        cmbEmpresa.DataTextField = "Razão Social"
        cmbEmpresa.DataValueField = "Código"
        cmbEmpresa.DataSource = objEmpresaBLL.ListaEmpresa.Tables(0)
        cmbEmpresa.DataBind()

        lista.Text = "<Selecione>"
        lista.Value = 0
        cmbEmpresa.Items.Insert(0, lista)
    End Sub

    Private Sub carrega_cmbArea()
        Dim objRepresentanteBLL As New BLL.RepresentanteBLL
        Dim lista As New ListItem

        cmbArea.DataTextField = "Area"
        cmbArea.DataValueField = "Cod. Representante"
        cmbArea.DataSource = objRepresentanteBLL.ListaArea(cmbEmpresa.SelectedValue).Tables(0)
        cmbArea.DataBind()

        lista.Text = "<Selecione>"
        lista.Value = 0
        cmbArea.Items.Insert(0, lista)
    End Sub

    Protected Sub cmbEmpresa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEmpresa.SelectedIndexChanged
        carrega_cmbArea()
        cmbArea.Enabled = True
    End Sub

    Private Sub carregaGridQuestao()
        Dim objAnaliseQuestaoBLL As New BLL.AnaliseQuestaoBLL
        Dim ds As DataSet
        Dim dt As DataTable
        Dim dv As DataView

        ds = objAnaliseQuestaoBLL.ListaAnaliseQuestao(cmbArea.SelectedValue)
        dv = ds.Tables(0).DefaultView
        dt = ds.Tables(0)
        gridQuestao.DataSource = dt

        gridQuestao.DataBind()
    End Sub

    Protected Sub cmbArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbArea.SelectedIndexChanged
        carregaGridQuestao()
    End Sub

    Private Sub gridQuestao_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridQuestao.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = ""
            e.Row.Cells(1).Text = ""
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(8).Text = 6 Then
                e.Row.Cells(0).Text = "<img src='../imagens/Flag_vermelha.png'>"
            End If

            If e.Row.Cells(8).Text = 7 Then
                e.Row.Cells(0).Text = "<img src='../imagens/Flag_amarela.png'>"
            End If

            If e.Row.Cells(8).Text = 8 Then
                e.Row.Cells(0).Text = "<img src='../imagens/Flag_verde.png'>"
            End If

            If e.Row.Cells(8).Text = 9 Then
                e.Row.Cells(0).Text = "<img src='../imagens/Flag_azul.png'>"
            End If

            e.Row.Cells(1).Text = "<a href='frmAnaliseRespostas.aspx?editar=1&cd_questionario=" & e.Row.Cells(2).Text & "&cd_questao=" & e.Row.Cells(4).Text & "&nm_indicador=" & e.Row.Cells(5).Text & "&dc_questao=" & e.Row.Cells(6).Text & "&xx_tipo=" & e.Row.Cells(7).Text & "&cd_status=" & e.Row.Cells(8).Text & "'><img src='../imagens/edit.png'></a>"
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
        End If
    End Sub

End Class