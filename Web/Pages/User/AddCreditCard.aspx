<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.Master" AutoEventWireup="true" CodeBehind="AddCreditCard.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.AddCreditCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <asp:Label ID="lblAdd" runat="server" meta:resourcekey="txtAddCC" Font-Bold="True"></asp:Label>
    </center>

    <div id="form">
        <form id="UpdateUserProfileForm" method="POST" runat="server">
            <div class="field">
                <span class="label">
                    <asp:Label CssClass="form-label" ID="lblTypeCC" runat="server" Width="135px" meta:resourcekey="txtTypeCC"></asp:Label>
                </span><span class="entry">
                    <asp:DropDownList ID="ddTypeCC" runat="server"></asp:DropDownList>
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Label ID="Label1" CssClass="form-label" runat="server" meta:resourcekey="txtNumCC"></asp:Label>
                </span><span class="entry">
                    <asp:TextBox ID="txtNumCC" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:Label ID="lblErrorNum" runat="server" Style="position: relative" meta:resourcekey="txtErrorNum"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvNumCC" runat="server"
                        ControlToValidate="txtNumCC" />
                </span>

            </div>

            <div class="field">
                <span class="label">
                    <asp:Label ID="Label2" CssClass="form-label" runat="server" meta:resourcekey="txtCodeCC"></asp:Label>
                </span><span class="entry">
                    <asp:TextBox ID="txtCodeCC" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:Label ID="lblErrorCode" runat="server" Style="position: relative" meta:resourcekey="txtErrorCode"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvCodeCC" runat="server"
                        ControlToValidate="txtCodeCC" />
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Label ID="Label3" CssClass="form-label" runat="server" meta:resourcekey="txtDateCC"></asp:Label>
                </span><span class="entry">
                    <asp:TextBox ID="txtDateCC" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:Label ID="lblErrorDate" runat="server" Style="position: relative" meta:resourcekey="txtErrorDate"></asp:Label>
                    <asp:Label ID="lblErrorDate2" runat="server" Style="position: relative" meta:resourcekey="txtErrorDate2"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvDateCC" runat="server"
                        ControlToValidate="txtDateCC" />
                </span>
            </div>
            <br />
            <br />

            <center>
                <asp:Label ID="lblDuplicateCard" runat="server" Style="position: relative" meta:resourcekey="txtErrorAdd"></asp:Label>
                <br />
                <asp:Button CssClass="btn btn-primary" ID="btnAddCC" runat="server" meta:resourcekey="txtAddCC" OnClick="btnAddCC_Click" />
            </center>
            <br />
        </form>
    </div>
</asp:Content>
