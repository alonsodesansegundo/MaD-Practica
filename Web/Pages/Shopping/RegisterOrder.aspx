<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.Master" AutoEventWireup="true" CodeBehind="RegisterOrder.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping.RegisterOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <asp:Label ID="lblDatos" runat="server" meta:resourcekey="lblDoOrder" Font-Bold="True"></asp:Label>
    </center>
    <div id="form">
        <form id="RegisterOrder" method="POST" runat="server">
            <div class="field">
                <span class="label">
                    <asp:Label CssClass="form-label" ID="lblNum" runat="server"
                        Width="135px" meta:resourcekey="txtNumCC"></asp:Label>
                </span><span class="entry">
                    <asp:DropDownList ID="ddNumberCC" AutoPostBack="true" runat="server">
                    </asp:DropDownList>
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Label CssClass="form-label" ID="lblFechCC" runat="server"
                        Width="135px" meta:resourcekey="txtFechCC"></asp:Label>
                </span><span class="entry">
                    <asp:Label CssClass="form-label" ID="lblFechCC_2" runat="server"
                        Width="135px"></asp:Label>&nbsp;</span></div>

            <div class="field">
                <span class="label">
                    <asp:Label CssClass="form-label" ID="Label1" runat="server"
                        Width="135px" meta:resourcekey="txtCodCC"></asp:Label>
                </span><span class="entry">
                    <asp:TextBox ID="txtCodeCC" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:Label CssClass="form-label" ID="lblErrorCode" runat="server"
                        Width="135px" meta:resourcekey="txtIncorrectCode"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvCodeCC" runat="server"
                        ControlToValidate="txtCodeCC" />
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Label CssClass="form-label" ID="Label2" runat="server"
                        Width="135px" meta:resourcekey="txtDirCC"></asp:Label>
                </span><span class="entry">
                    <asp:TextBox ID="txtDirOrder" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDirOrder" runat="server"
                        ControlToValidate="txtDirOrder" />
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Label CssClass="form-label" ID="Label3" runat="server"
                        Width="135px" meta:resourcekey="txtNomOrder"></asp:Label>
                </span><span class="entry">
                    <asp:TextBox ID="txtNameOrder" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNomOrder" runat="server"
                        ControlToValidate="txtNameOrder" />
                </span>
            </div>
            <br />
            <br />
            <center>
                <asp:Button CssClass="btn btn-primary" ID="btnDoOrder" runat="server" meta:resourcekey="txtDoOrder" OnClick="btnDoOrder_Click" />
            </center>
            <br />
        </form>
    </div>
</asp:Content>

