Public Class frmRelStatusQuestionarios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sessionUser") = "" Or Session("sessionUser") = Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

        If Not IsPostBack Then            
            carrega_cmbEmpresa()
            carrega_cmbStatus()
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

    Protected Sub cmbEmpresa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEmpresa.SelectedIndexChanged
        carrega_cmbArea()
        carrega_cmbAspecto()

        carregagridQuestao()

        cmbArea.Enabled = True
        cmbIndicador.Enabled = True
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

    Protected Sub cmbArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbArea.SelectedIndexChanged
        carregagridQuestao()
    End Sub

    Private Sub carrega_cmbAspecto()
        Dim objAspectoBLL As New BLL.AspectoBLL
        Dim lista As New ListItem

        cmbAspecto.DataTextField = "Descrição"
        cmbAspecto.DataValueField = "Código"
        cmbAspecto.DataSource = objAspectoBLL.ListaAspecto.Tables(0)
        cmbAspecto.DataBind()

        lista.Text = "<Selecione>"
        lista.Value = 0
        cmbAspecto.Items.Insert(0, lista)
    End Sub

    Protected Sub cmbAspecto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAspecto.SelectedIndexChanged
        carrega_cmbIndicador()
    End Sub

    Private Sub carrega_cmbIndicador()
        Dim objIndicadorBLL As New BLL.IndicadorBLL
        Dim lista As New ListItem

        cmbIndicador.DataTextField = "Indicador"
        cmbIndicador.DataValueField = "Código"
        cmbIndicador.DataSource = objIndicadorBLL.RetornaIndicador(cmbAspecto.SelectedValue, 0).Tables(0)
        cmbIndicador.DataBind()

        lista.Text = "<Selecione>"
        lista.Value = 0
        cmbIndicador.Items.Insert(0, lista)
    End Sub

    Protected Sub cmbIndicador_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbIndicador.SelectedIndexChanged
        carregagridQuestao()
    End Sub

    Private Sub carrega_cmbStatus()
        Dim objStatusBLL As New BLL.StatusBLL
        Dim lista As New ListItem

        cmbStatus.DataTextField = "Status"
        cmbStatus.DataValueField = "Código"
        cmbStatus.DataSource = objStatusBLL.ListaStatus.Tables(0)
        cmbStatus.DataBind()

        lista.Text = "<Selecione>"
        lista.Value = 0
        cmbStatus.Items.Insert(0, lista)
    End Sub

    Protected Sub cmbStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStatus.SelectedIndexChanged
        carregagridQuestao()
    End Sub

    Private Sub carregagridQuestao()
        Dim objQuestionarioBLL As New BLL.QuestionarioBLL
        Dim parametros() As String = {"cd_empresa", "cd_area", "cd_indicador", "cd_status"}
        Dim ds As DataSet
        Dim dt As DataTable
        Dim dv As DataView

        If cmbEmpresa.SelectedValue <> "" Then
            parametros(0) = cmbEmpresa.SelectedValue
        Else
            parametros(0) = 0
        End If
        If cmbArea.SelectedValue <> "" Then
            parametros(1) = cmbArea.SelectedValue
        Else
            parametros(1) = 0
        End If
        If cmbIndicador.SelectedValue <> "" Then
            parametros(2) = cmbIndicador.SelectedValue
        Else
            parametros(2) = 0
        End If
        If cmbStatus.SelectedValue <> "" Then
            parametros(3) = cmbStatus.SelectedValue
        Else
            parametros(3) = 0
        End If
        

        ds = objQuestionarioBLL.RetornaStatusQuestionario(parametros)
        dv = ds.Tables(0).DefaultView
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            divLegenda.Visible = True
        Else
            divLegenda.Visible = False
        End If

        gridQuestao.DataSource = dt

        'If cmbEmpresa.SelectedValue <> 0 Then
        '    dv.RowFilter = "[Cod. Empresa] = '" & cmbEmpresa.SelectedValue & "'"
        'End If

        'If cmbArea.SelectedValue <> 0 Then
        '    dv.RowFilter = "[Area] = '" & cmbArea.SelectedValue & "'"
        'End If

        'If cmbIndicador.SelectedValue <> 0 Then
        '    dv.RowFilter = "[Indicador] = '" & cmbIndicador.SelectedValue & "'"
        'End If

        'If cmbStatus.SelectedValue <> 0 Then
        '    dv.RowFilter = "[Cod. Status] = '" & cmbStatus.SelectedValue & "'"
        'End If

        gridQuestao.DataBind()
    End Sub

    Private Sub gridQuestao_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridQuestao.RowDataBound
        Dim temp As String

        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = ""
            e.Row.Cells(1).Visible = False
            e.Row.Cells(7).Visible = False

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
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

            e.Row.Cells(1).Visible = False
            e.Row.Cells(7).Visible = False

        End If
    End Sub

End Class