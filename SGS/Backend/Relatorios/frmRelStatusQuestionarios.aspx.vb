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
        carrega_cmbIndicador()

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

    Private Sub carrega_cmbIndicador()
        Dim objIndicadorBLL As New BLL.IndicadorBLL
        Dim lista As New ListItem

        cmbIndicador.DataTextField = "Indicador"
        cmbIndicador.DataValueField = "Código"
        cmbIndicador.DataSource = objIndicadorBLL.ListaIndicador.Tables(0)
        cmbIndicador.DataBind()

        lista.Text = "<Selecione>"
        lista.Value = 0
        cmbIndicador.Items.Insert(0, lista)
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

    Private Sub carregagridQuestao()
        Dim objQuestionarioBLL As New BLL.QuestionarioBLL
        Dim parametros() As String = {"cd_empresa", "cd_area", "cd_indicador", "cd_status"}
        Dim ds As DataSet
        Dim dt As DataTable

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
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            divLegenda.Visible = True
        Else
            divLegenda.Visible = False
        End If

        gridQuestao.DataSource = dt

        gridQuestao.DataBind()
    End Sub

End Class