Imports Microsoft.Office
Imports Microsoft.Office.Interop.Word
Imports System.IO

Public Class RelatorioQuestionario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sessionUser") = "" Or Session("sessionUser") = Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

        If Not IsPostBack Then
            carrega_cmbEmpresa()
            carrega_cmbCompetencia()
            pnlMsg.Visible = False
        End If


    End Sub

    Private Sub carrega_cmbCompetencia()
        Dim StartDate, EndDate As Date

        StartDate = New Date(2014, 12, 31)
        EndDate = New Date(2050, 12, 31)

        While StartDate <= EndDate
            cmbCompetencia.Items.Add(StartDate.Year.ToString())
            StartDate = StartDate.AddYears(1)
        End While
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

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Dim sb1 As New System.Text.StringBuilder
        Dim label1 As New Label
        Dim objRelatorioBLL As New BLL.RelatorioBLL
        Dim objRespostaBLL As New BLL.RespostaBLL
        Dim dtQuestao As System.Data.DataTable
        Dim dtResposta As System.Data.DataTable
        Dim i As Integer
        Dim j As Integer
        Dim col As Integer
        Dim categoria As String



        If cmbEmpresa.SelectedValue = 0 Then
            lblMsg.Text = "Informe a empresa!"
            lblMsg.ForeColor = Drawing.Color.Red
            pnlMsg.Visible = True
            Exit Sub
        End If


        dtQuestao = objRelatorioBLL.RelatorioQuestao(cmbEmpresa.SelectedValue, cmbCompetencia.SelectedValue).Tables(0)

        If dtQuestao.Rows.Count <= 0 Then
            lblMsg.Text = "Nenhuma questão foi finalizada para essa empresa!"
            lblMsg.ForeColor = Drawing.Color.Red
            pnlMsg.Visible = True
            Exit Sub
        End If

        'Aqui vai a lógica do html do relatório

        categoria = dtQuestao.Rows(0)("dc_categoria")

        sb1.Append("<br><div class='divCategoria'>" & categoria & "</div>")

        For i = 0 To dtQuestao.Rows.Count - 1
            If dtQuestao.Rows(i)("dc_categoria") <> categoria Then
                sb1.Append("<br><div class='divCategoria'>" & dtQuestao.Rows(i)("dc_categoria") & "</div>")
                categoria = dtQuestao.Rows(i)("dc_categoria")
            End If

            'Monta cabeçalho da questão
            sb1.Append("<div class='divQuestao'><b>" & dtQuestao.Rows(i)("row") & ".&nbsp;" & dtQuestao.Rows(i)("dc_questao") & "&nbsp;(" & dtQuestao.Rows(i)("dc_area") & ")" & "</b><br>")

            'Monta resposta dentro da mesma div da questao
            If dtQuestao.Rows(i)("xx_tipo") = "I" Then
                dtResposta = objRespostaBLL.RetornaResposta(dtQuestao.Rows(i)("cd_questionario")).Tables(0)
                sb1.Append("<span class='resposta'>" & dtResposta.Rows(0)("Resposta") & "</span></div>")
            End If

            If dtQuestao.Rows(i)("xx_tipo") = "Q" Then
                dtResposta = objRelatorioBLL.RelatorioQuestaoItem(dtQuestao.Rows(i)("cd_questionario")).Tables(0)

                If Not (dtResposta.Columns.Count = 1 And dtResposta.Rows.Count = 1) Then
                    sb1.Append("<table class='tblItem'>")
                Else
                    sb1.Append("<table>")
                End If


                'Monta linha dos titulos das colunas

                If Not (dtResposta.Columns.Count = 1 And dtResposta.Rows.Count = 1) Then
                    sb1.Append("    <tr>")
                    For col = 0 To dtResposta.Columns.Count - 1
                        sb1.Append("        <td class='colunaItemTitulo'>" & dtResposta.Columns(col).ColumnName & "</td>")
                    Next
                    sb1.Append("    </tr>")
                End If


                'Monta linhas com valores
                For j = 0 To dtResposta.Rows.Count - 1
                    sb1.Append("    <tr>")
                    For col = 0 To dtResposta.Columns.Count - 1
                        If dtResposta.Columns.Count = 1 And dtResposta.Rows.Count = 1 Then
                            sb1.Append("        <td class='colunaListaItem'>" & dtResposta.Columns(col).ColumnName & ":</td>")
                            sb1.Append("        <td class='colunaListaValor'>" & dtResposta.Rows(j)(col) & "</td>")
                        Else
                            sb1.Append("        <td class='colunaItemValor'>" & dtResposta.Rows(j)(col) & "</td>")
                        End If

                    Next
                    sb1.Append("    </tr>")
                Next
                sb1.Append("</table></div><br>")

            End If
        Next


        'Aqui vai a lógica do html do relatório
        'sb1.Append("<div id='teste'>Hellow word!!!</div>")




        label1.Text = sb1.ToString
        sb1.Remove(0, sb1.Length)

        Response.Clear()
        Response.Charset = ""
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("iso-8859-1")
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application /msword.doc"
        Response.AddHeader("content-disposition", "attachment; filename =" & "Relatorio.doc")
        Dim sw As New System.IO.StringWriter
        Dim htw As New HtmlTextWriter(sw)
        label1.RenderControl(htw)
        Dim fi As FileInfo = New FileInfo(Server.MapPath("../Styles/Site.css"))
        Dim sb As New System.Text.StringBuilder
        Dim sr As StreamReader = fi.OpenText()

        Do While (sr.Peek() >= 0)
            sb.Append(sr.ReadLine())
        Loop
        sr.Close()

        Response.Write("<html> <head> <style type ='text/css'>" & sb.ToString() & "</style> <head>" & sw.ToString() & "</ html> ")
        sw = Nothing
        htw = Nothing
        Response.Flush()
        Response.End()



    End Sub


    Protected Sub cmbEmpresa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEmpresa.SelectedIndexChanged
        carrega_cmbArea()
        cmbArea.Enabled = True
    End Sub
End Class