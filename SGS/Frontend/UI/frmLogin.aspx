<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmLogin.aspx.vb" Inherits="Frontend.frmLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

<style>
    .Centraliza
{
    width: 940px;
    margin-left:auto;
    margin-right:auto;
    left: 50%;
    top: 230px;
}
</style>
    
</head>
<body style="background-repeat: no-repeat; background-position:top;">
    
    <form id="form1" runat="server">
        <table style="width: 100%" class="Centraliza">
            <tr>
                <td>
                        <div style="background-image:url('../Imagens/login_bg.png'); width:100%; background-repeat:no-repeat; background-position:center; margin-left:0px; margin-top:15%; height: 283px;">
						    <br>
                            
                            <div style="margin-top: 0px; margin-left:auto; margin-right:auto; position:inherit; top: 0px; left: 0px; width: 424px;">
                            <asp:Login ID="Login1" runat="server" 
                                FailureText="Erro! Nome de Usuário ou senha incorretos!" LoginButtonText="Entrar" 
                                PasswordLabelText="Senha &nbsp;" PasswordRequiredErrorMessage="Digite sua senha!" 
                                RememberMeText="Lembrar meus dados." 
                                TitleText="SGS - Sistema de Gestão Sustentável" UserNameLabelText="Usuário &nbsp;" 
                                UserNameRequiredErrorMessage="Digite seu nome de usuário!" Height="204px" 
                                style="margin-top: 0px; margin-left:0px; position:inherit; top: 0px; left: 0px;" 
                                Font-Names="Arial" Font-Overline="False" Width="391px">
                                <LoginButtonStyle Width="80px" />
                                <TextBoxStyle Width="250px" Font-Bold="True" />  
                                <TitleTextStyle Font-Bold="True" Font-Names="Comic Sans MS" Font-Size="Larger" 
                                    Font-Strikeout="False" ForeColor="#009933" />
                            </asp:Login> 
                           </div>
                            
                        </div>				
				</td>
            </tr>
        </table>
		    
		

    
    </form>
    
</body>
</html>
