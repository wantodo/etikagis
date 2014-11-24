Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim menuItens As MenuItemCollection
        'Dim adminItem As New MenuItem

        'menuItens = NavigationMenu.Items

        'For Each menuItem As MenuItem In menuItens
        '    adminItem = menuItem
        'Next menuItem

        'menuItens.RemoveAt(0)




        If NavigationMenu.Page.Title.ToString = "GIS - Cadastro Empresa" Then
            lblTitulo.Text = "Cadastro de Empresa"
        End If

        If NavigationMenu.Page.Title.ToString = "GIS - Cadastro de Representante" Then
            lblTitulo.Text = "Cadastro de Representante"
        End If

        If NavigationMenu.Page.Title.ToString = "GIS - Cadastro de Categoria" Then
            lblTitulo.Text = "Cadastro de Categoria"
        End If

        If NavigationMenu.Page.Title.ToString = "GIS - Cadastro de Subcategoria" Then
            lblTitulo.Text = "Cadastro de Subcategoria"
        End If

        If NavigationMenu.Page.Title.ToString = "GIS - Cadastro de Aspecto" Then
            lblTitulo.Text = "Cadastro de Aspecto"
        End If

        If NavigationMenu.Page.Title.ToString = "GIS - Cadastro de Indicador" Then
            lblTitulo.Text = "Cadastro de Indicador"
        End If

        If NavigationMenu.Page.Title.ToString = "GIS - Cadastro de Questão" Then
            lblTitulo.Text = "Cadastro de Questão"
        End If

        If NavigationMenu.Page.Title.ToString = "GIS - Questionario" Then
            lblTitulo.Text = "Questionario"
        End If

        If NavigationMenu.Page.Title.ToString = "GIS - Analise de Resposta" Then
            lblTitulo.Text = "Analise de Resposta"
        End If

        If NavigationMenu.Page.Title.ToString = "GIS - Relatório Final" Then
            lblTitulo.Text = "Relatório Final"
        End If



        Select Case Session("acesso")
            Case 1  ' Admin
                Exit Sub
            Case 2 ' Cadastro
                NavigationMenu.Items(1).Enabled = False
                NavigationMenu.Items(3).Enabled = False
            Case 3 ' Operação
                NavigationMenu.Items(1).Enabled = False
                NavigationMenu.Items(2).Enabled = False
            Case 4 ' Relatório
                NavigationMenu.Items(1).Enabled = False
                NavigationMenu.Items(2).Enabled = False
                NavigationMenu.Items(3).Enabled = False
            Case 5 ' Analista
                NavigationMenu.Items(1).Enabled = False
            Case Else
                NavigationMenu.Items(1).Enabled = False
                NavigationMenu.Items(2).Enabled = False
                NavigationMenu.Items(3).Enabled = False
                NavigationMenu.Items(4).Enabled = False
        End Select
        'NavigationMenu.Items(2).Enabled = False


    End Sub

 
End Class