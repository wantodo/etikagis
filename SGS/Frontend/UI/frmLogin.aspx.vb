Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Web.Security

Public Class frmLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'teste
    End Sub

    Protected Sub Login1_Authenticate(sender As Object, e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles Login1.Authenticate
        Dim objLoginBLL As New BLL.LoginBLL
        Dim objRepresentanteBLL As New BLL.RepresentanteBLL
        Dim usuario As New MODEL.Usuario
        Dim representante As New MODEL.Representante
        Dim dtUsuario As DataTable
        Dim dtRepresentante As DataTable

        usuario.nomeUsuario = Login1.UserName
        usuario.senha = Login1.Password


        dtUsuario = objLoginBLL.Logar(usuario, 2).Tables(0)

        usuario.codigo = dtUsuario.Rows(0)("cd_usuario")

        Select Case usuario.codigo
            Case -1
                Login1.FailureText = "Usuario e senha não estão corretos."
                Exit Select
            Case Else
                usuario.acesso = dtUsuario.Rows(0)("cd_acesso")
                Session("codEmpresa") = dtUsuario.Rows(0)("cd_empresa")

                Session("sessionUser") = usuario.nomeUsuario
                Session("codUsuario") = usuario.codigo                

                dtRepresentante = objRepresentanteBLL.RetornaRepresentante(, usuario.codigo).Tables(0)
                representante.cd_representante = dtRepresentante.Rows(0)("Código")
                representante.no_representante = dtRepresentante.Rows(0)("Nome")
                representante.dc_email = dtRepresentante.Rows(0)("email")
                representante.dc_area = dtRepresentante.Rows(0)("area")
                representante.perfil.cd_perfil_representante = dtRepresentante.Rows(0)("Cod. Perfil")

                Session("codRepresentante") = representante.cd_representante
                Session("email") = representante.dc_email
                Session("area") = representante.dc_area
                Session("nome") = representante.no_representante
                Session("codPerfil") = representante.perfil.cd_perfil_representante

                FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet)
                Exit Select
        End Select


    End Sub

End Class