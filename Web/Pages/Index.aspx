<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.Master" AutoEventWireup="true"
    CodeBehind="Index.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <asp:DropDownList ID="ddCategories" runat="server">
        </asp:DropDownList>
        <asp:TextBox ID="txtBusqueda" runat="server"></asp:TextBox>
        <asp:Button ID="btnBuscar" runat="server"
            Text="<%$ Resources:Common,txtBuscar %>" OnClick="btnBuscar_Click" />
    </form>
    <br />
    <br />
    <br />
    <asp:Localize ID="lclContent" runat="server" meta:resourcekey="txtContenido" />
    <br />
    <br />
    <br />

</asp:Content>
