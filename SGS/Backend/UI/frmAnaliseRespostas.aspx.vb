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

                    cmbStatus.Enabled = True
                    txtRetorno.Enabled = True

                    cmbEmpresa.SelectedValue = Request.QueryString("cd_empresa").ToString
                    carrega_cmbArea()
                    cmbArea.SelectedValue = Request.QueryString("cd_representante").ToString
                    carregaGridQuestao()

                    txtCabecalho.Text = Request.QueryString("dc_questao").ToString

                    If Request.QueryString("xx_tipo").ToString = "I" Then
                        carrega_resposta(Request.QueryString("cd_questionario").ToString)
                        pnlGridItem.Visible = False
                        lblItem.Visible = False

                        If Request.QueryString("cd_status").ToString = 7 Or Request.QueryString("cd_status").ToString = 8 Then
                            cmbStatus.SelectedValue = Request.QueryString("cd_status").ToString
                        Else
                            cmbStatus.SelectedValue = 0
                        End If

                        txtRetorno.Text = Request.QueryString("dc_retorno").ToString
                    Else
                        txtResposta.Visible = False
                        lblResposta.Visible = False
                        lblItem.Visible = True
                        gridItemQuestao.Visible = True
                        carrega_gridItemQuestao(Request.QueryString("cd_questionario").ToString)
                    End If

                    btnGravar.Enabled = True
                    btnCancelar.Enabled = True
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

    Private Sub carrega_resposta(codQuestionario As Integer)
        Dim objRespostaBLL As New BLL.RespostaBLL
        Dim dt As DataTable

        dt = objRespostaBLL.RetornaResposta(codQuestionario).Tables(0)

        txtResposta.Text = dt.Rows(0)("Resposta").ToString
        Session("codResposta") = dt.Rows(0)("codResposta").ToString

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
        gridQuestao.Focus()
    End Sub

    Private Sub gridQuestao_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridQuestao.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = ""
            e.Row.Cells(1).Text = ""
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
            e.Row.Cells(9).Visible = False
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

            e.Row.Cells(1).Text = "<a href='frmAnaliseRespostas.aspx?editar=1&cd_questionario=" & e.Row.Cells(2).Text & "&cd_questao=" & e.Row.Cells(4).Text & "&nm_indicador=" & e.Row.Cells(5).Text & "&dc_questao=" & e.Row.Cells(6).Text & "&xx_tipo=" & e.Row.Cells(7).Text & "&cd_status=" & e.Row.Cells(8).Text & "&dc_retorno=" & e.Row.Cells(9).Text & "&cd_empresa=" & e.Row.Cells(10).Text & "&cd_representante=" & e.Row.Cells(11).Text & "'><img src='../imagens/edit.png'></a>"
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
            e.Row.Cells(9).Visible = False
        End If
    End Sub

    Private Sub gridItemQuestao_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridItemQuestao.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Visible = False
            e.Row.Cells(3).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Visible = False
            e.Row.Cells(3).Visible = False
        End If
    End Sub

End Class