﻿<%@ Page Title="GIS - Cadastro de Representante" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="frmRepresentante.aspx.vb" Inherits="Backend.frmRepresentante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style3
        {
            width: 429px;
        }
    </style>
    <script language="JavaScript" type="text/javascript" src="../Scripts/MascaraValidacao.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="800" height="100%">								
				<tr>
					<td colspan="2">
						<nav id="navToolBar">
							<ul>
                                <li><asp:ImageButton id="btnNovo" runat="server" ImageUrl="~/Imagens/add.ico" 
                                        style="margin-top:5px;" EnableTheming="True" ToolTip="Novo" /> </li>
								<li><asp:ImageButton id="btnGravar" runat="server" 
                                        ImageUrl="~/Imagens/save_disabled.png" Enabled="False" ToolTip="Gravar" /></li>
								<li><asp:ImageButton id="btnCancelar" runat="server" 
                                        ImageUrl="~/Imagens/no_disabled.png" Enabled="False" ToolTip="Cancelar" /></li>
								<li><asp:Label ID="Label1" runat="server" Text="Filtro:" />
                                    <asp:DropDownList id="cmbFiltro" runat="server" Width="180px" >
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>Código</asp:ListItem>
                                        <asp:ListItem>Nome</asp:ListItem>
                                        <asp:ListItem>Empresa</asp:ListItem>
                                        <asp:ListItem>Perfil</asp:ListItem>
                                        <asp:ListItem>Status</asp:ListItem>
                                    </asp:DropDownList>
								    <asp:TextBox runat="server" id="txtFiltro" Width="171px" />                                	
									    <asp:ImageButton id="btnConsultar" runat="server" 
                                        ImageUrl="~/Imagens/find.ico" ToolTip="Consultar" />
								</li>					
								
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
							<legend style="color:#B0C4DE;">Dados Gerais</legend>
							    <table>
								    <tr>
									    <td>Código</td>
                                        <td>
                                            <asp:TextBox ID="txtCodigo" runat="server" Enabled="False" Width="103px"></asp:TextBox>
                                        </td>
								    </tr>

                                    <tr>
									    <td>Nome* </td> <td style="margin-left: 40px">
                                            <asp:TextBox ID="txtNome" runat="server" MaxLength="200" Width="386px"></asp:TextBox>
                                            </td>
								        </tr>	

                                    <tr>
									    <td>Empresa*</td> <td>                         
                                        <asp:DropDownList ID="cmbEmpresa" runat="server" Width="202px">
                                        </asp:DropDownList>
                                        </td>
								    </tr>

                                    <tr>
									    <td>Perfil*</td> <td>                         
                                        <asp:DropDownList ID="cmbPerfil" runat="server" Width="202px">
                                        </asp:DropDownList>
                                        </td>
								    </tr>

                                    <tr>
									    <td>Cargo</td> <td>
                                        <asp:TextBox ID="txtCargo" runat="server" MaxLength="200" Width="386px"></asp:TextBox>
                                        </td>
								    </tr>

                                    <tr>
									    <td>Area</td> <td>
                                        <asp:TextBox ID="txtArea" runat="server" MaxLength="200" Width="386px"></asp:TextBox>
                                        </td>
								    </tr>	
                                    
                                    <tr>
									    <td>Recebe email?*</td> <td>                         
                                        <asp:DropDownList ID="cmbRecebeEmail" runat="server" Width="202px">
                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                        </asp:DropDownList>
                                        </td>
								    </tr>							    
							    </table>
                            </fieldset>
                            <fieldset class="frame">	
							    <legend style="color:#B0C4DE;">Dados Pessoais</legend>
                                    <table>                                        
                                							
								        <tr>
									        <td>Email*</td> <td>
                                            <asp:TextBox ID="txtEmail" runat="server" Width="386px" MaxLength="200"></asp:TextBox>
                                            </td>									
								        </tr>                                																    

								        <tr>
										    <td>Telefone</td> <td>
                                            <asp:TextBox ID="txtTelefone" runat="server" MaxLength="20" Width="179px" onkeypress="MascaraTelefone(ctl00$MainContent$txtTelefone);"></asp:TextBox>
                                            </td>
									    </tr>	

                                        <tr>
										    <td>Usuário</td> <td>
                                            <asp:TextBox ID="txtUsuario" runat="server" MaxLength="20" Width="179px"></asp:TextBox>
                                            </td>
									    </tr>

                                        <tr>
										    <td>Senha</td> <td>
                                            <asp:TextBox ID="txtSenha" runat="server" MaxLength="20" Width="179px"></asp:TextBox>
                                            </td>
									    </tr>
                                                                                                                                                                                    
                                        <tr>
									        <td>Status*</td> <td>                         
                                            <asp:DropDownList ID="cmbStatus" runat="server" Width="202px">
                                            </asp:DropDownList>
                                            </td>
								        </tr>
                                    </table>                           								
							</fieldset>
                            
					</td>
				</tr>
            </table>
			
            
            <table  border="0" cellpadding="0" cellspacing="0" width="800" height="100%">	
                <tr>
                    <td colspan="2">
                        <asp:Panel runat="server" ID="pnlExcluir" Visible="false" Height="44px">
                            <table width="99%" style="background-color:#FFDAB9; height: 41px; border:1px solid #CD3333; margin-left:10px;">
                                <tr>
                                    <td class="style3">
                                        <span style="margin-left:200px; font-size:medium; color:#CD3333;">Deseja excluir esse registro?</span>
                                    </td> 
                                    <td>
                                        <asp:Button ID="btnSim" runat="server" Text="SIM" Width="76px" />
                                        <asp:Button ID="btnNao" runat="server" Text="NÃO" Width="76px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>

				<tr>
					<td colspan="2">
                        <asp:Panel ID="Panel1"  runat="server" ScrollBars="Horizontal" Width="900px" 
                            style="margin-left:10px; margin-top:20px;" Height="224px">
						    <asp:GridView ID="gridRepresentante" runat="server" 
                                style="margin-top: 0px; margin-left:auto; margin-right:auto;" CellPadding="4" ForeColor="#333333" 
                                Font-Size="Small">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" 
                                    Height="30px" Wrap="False" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E3EAEB" Wrap="False" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                            </asp:GridView>
                        </asp:Panel>
					</td>
				</tr>

                <tr>
					<td valign="top" height="100%">
								
					</td>					
				</tr>				
			</table>
            
</asp:Content>