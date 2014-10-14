<%@ Page Title="Home Page" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="Default.aspx.vb" Inherits="Frontend._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Bem Vindo ao Questionario!
    </h2>
    <p>
        <asp:LinkButton ID="lnkResposta" runat="server" PostBackUrl="~/UI/frmResposta.aspx">Responder Questionario</asp:LinkButton>
    </p>
</asp:Content>
