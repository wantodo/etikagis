<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="frmConteudo.aspx.vb" Inherits="Backend.frmConteudo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style3
        {
            width: 429px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="800" height="100%">
				
				
				<tr>
					<td colspan="2">
						<nav id="navToolBar">
							<ul style="margin-left:50%;">
                                
								<li><asp:ImageButton id="btnGravar" runat="server" 
                                        ImageUrl="~/Imagens/save_disabled.png" Enabled="False" style="margin-top:7px;" /></li>													
								
							</ul>
						</nav>
					</td>					
				</tr>

                <tr>
                    <td colspan="2">
                        <asp:Panel runat="server" ID="pnlMsg" Visible=False 
                            style="width: 64%; margin-left: 25%; margin-top: 10px; background-color:#FFFACD; height: 34px; border:1px solid #FFE4B5;" 
                            HorizontalAlign="Center">
                            <p style="margin-top:7px;">
                                <asp:Label runat="server" ID="lblMsg" Font-Bold="True" 
                                    Font-Size="Small" ForeColor="Black" /></p>
                        </asp:panel>
                    </td>
                </tr>			
				<tr>
					<td colspan="2">
                        <fieldset class="frame">	
							<legend style="color:#B0C4DE;">HOME - FRONTEND</legend>
							    <table>
								    <tr>
									    <td>Empresa*</td> <td>                         
                                        <asp:DropDownList ID="cmbEmpresa" runat="server" Width="404px">
                                        </asp:DropDownList>
                                        </td>
								    </tr>

                                    <tr>
									    <td>Descrição* </td> <td style="margin-left: 40px">
                                        <asp:TextBox ID="txtDescricao" runat="server" MaxLength="8000" Width="404px" 
                                            Height="144px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
								    </tr>
							    </table>                            								
							</fieldset>                            
					</td>
				</tr>
            </table>
</asp:Content>
