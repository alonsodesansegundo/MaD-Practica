<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.Master" AutoEventWireup="true" CodeBehind="InternalError.aspx.cs"
    Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Errors.InternalError" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <asp:Label CssClass="form-label" ID="lblErrorTitle" runat="server" Font-Bold="True" meta:resourcekey="lblErrorTitle"></asp:Label>
    </center>
    &nbsp;
    <br />
    <br />
    <asp:Label ID="lblRetryLater" runat="server" meta:resourcekey="lblRetryLater"></asp:Label>
    <br />
    <br />
</asp:Content>
