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




        If NavigationMenu.Page.Title.ToString = "Empresas" Then
            lblTitulo.Text = "Cadastro de Empresa"
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