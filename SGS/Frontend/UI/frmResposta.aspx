<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="frmResposta.aspx.vb" Inherits="Frontend.frmResposta" %>

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
							<ul style="margin-left:45%;">
                                <li><asp:ImageButton id="btnNovo" runat="server" ImageUrl="~/Imagens/add.ico" 
                                        style="margin-top:5px;" EnableTheming="True" /> </li>
								<li><asp:ImageButton id="btnGravar" runat="server" 
                                        ImageUrl="~/Imagens/save_disabled.png" Enabled="False" /></li>
								<li><asp:ImageButton id="btnCancelar" runat="server" 
                                        ImageUrl="~/Imagens/no_disabled.png" Enabled="False" /></li>
								<li><asp:ImageButton id="btnFinalizar" runat="server" 
                                        ImageUrl="~/Imagens/accept_disabled.png" Enabled="False" /></li>				
								
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
                        <asp:Panel ID="Panel1"  runat="server" ScrollBars="Vertical"  
                            style="margin-left:10px; margin-top:20px; max-height:366px; max-width:900px;">
						    <asp:GridView ID="gridQuestao" runat="server" 
                                style="margin-top: 0px; margin-left:auto; margin-right:auto;" CellPadding="4" ForeColor="#333333" 
                                Font-Size="Small" Height="215px">
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
					<td colspan="2">
                        <fieldset id="frameQuestao" runat="server" visible="false" class="frame">	
							<legend style="color:#B0C4DE;">Questão</legend>
							    <table>
								    <tr>	
                                        <td>
                                            <asp:Label ID="lblCodQuestionario" runat="server" Font-Bold="True" />
                                        </td>								    
                                        <td>
                                            <asp:Label ID="lblQuestao" runat="server" Font-Bold="True" />
                                        </td>
								    </tr>                                                                          			    	                                   
							    </table>                            								
							</fieldset>                            
					</td>
				</tr>

                <tr>
					<td colspan="2">
                        <fieldset id="frameResposta" runat="server" visible="false" class="frame">	
							<legend style="color:#B0C4DE;">Resposta</legend>
							    <table>                                    
                                    <tr>									    
                                        <td>
                                            <asp:TextBox ID="txtResposta" runat="server" MaxLength="8000" Width="439px" 
                                                        Height="137px" TextMode="MultiLine" style="margin-left:15px;"></asp:TextBox>
                                        </td>
								    </tr>			    	                                   
							    </table>                            								
						</fieldset>                            
					</td>
				</tr>

                <tr>
					<td colspan="2">                      
                        <fieldset id="frameItem" runat="server" visible="false" class="frame">	
						    <legend style="color:#B0C4DE;">Item Questão</legend>
							<table>                                           
                                <tr>
                                    <td colspan="3">
                                        <asp:Panel ID="pnlGridItem"  runat="server" ScrollBars="Auto"                                                 
                                            style="margin-left:0px; margin-top:0px; max-height:2    00px; max-width:465px;">
                                                    
                                                    <asp:GridView ID="gridItemQuestao" runat="server" 
                                                        style="margin-top: 0px; margin-left:auto; margin-right:auto;" CellPadding="4" ForeColor="#333333" 
                                                        Font-Size="Small">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Item">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItem" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Resposta">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtResposta" runat="server" Width="250px"/>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
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
					<td valign="top" height="100%">
								
					</td>					
				</tr>				
			</table>
</asp:Content>
