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

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Dim sb1 As New System.Text.StringBuilder
        Dim label1 As New Label



        'Aqui vai a lógica do html do relatório
        sb1.Append("<div id='teste'>Hellow word!!!</div>")




        label1.Text = sb1.ToString
        sb1.Remove(0, sb1.Length)

        Response.Clear()
        Response.Charset = ""
        Response.ContentEncoding = System.Text.Encoding.UTF8
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application /msword.doc"
        Response.AddHeader("content-disposition", "attachment; filename =" & "Relatório.doc")
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