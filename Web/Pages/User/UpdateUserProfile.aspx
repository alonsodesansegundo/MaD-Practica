<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.Master"
    AutoEventWireup="true" CodeBehind="UpdateUserProfile.aspx.cs"
    Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.UpdateUserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <asp:Label ID="lblDatos" runat="server" meta:resourcekey="txtUpdate" Font-Bold="True"></asp:Label>
    </center>
    <div id="form">
        <form id="UpdateUserProfileForm" method="POST" runat="server">
            <div class="field">
                <span class="label">
                    <asp:Label CssClass="form-label" ID="lblNombre" runat="server" Width="135px" meta:resourcekey="txtNombre"></asp:Label>
                </span><span class="entry">
                    <asp:TextBox CssClass="form-control" ID="txtNombre" Width="100px" Columns="16" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvName" runat="server"
                        ErrorMessage="Campo Obligatorio (resumen)"
                        Style="margin-left: 15px"
                        Display="Dynamic"
                        Text="<%$Resources: Common, validatorFields %>"
                        ControlToValidate="txtNombre" />
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Label CssClass="form-label" ID="lblApellidos" runat="server" Width="135px" meta:resourcekey="txtApellidos"></asp:Label>
                </span><span class="entry">
                    <asp:TextBox CssClass="form-control" ID="txtApellidos" Width="100px" Columns="16" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server"
                        ErrorMessage="Campo Obligatorio (resumen)"
                        Style="margin-left: 15px"
                        Display="Dynamic"
                        Text="<%$Resources: Common, validatorFields %>"
                        ControlToValidate="txtApellidos" />
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Label CssClass="form-label" ID="lblDir" runat="server" Width="135px" meta:resourcekey="txtDir"></asp:Label>
                </span><span class="entry">
                    <asp:TextBox CssClass="form-control" ID="txtDir" Width="100px" Columns="16" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rdfDir" runat="server"
                        ErrorMessage="Campo Obligatorio (resumen)"
                        Style="margin-left: 15px"
                        Display="Dynamic"
                        Text="<%$Resources: Common, validatorFields %>"
                        ControlToValidate="txtDir" />
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Label CssClass="form-label" ID="lblEmail" runat="server" Width="135px" meta:resourcekey="txtEmail"></asp:Label>
                </span><span class="entry">
                    <asp:TextBox CssClass="form-control" ID="txtEmail" Width="100px" Columns="16" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                        ErrorMessage="Campo Obligatorio (resumen)"
                        Style="margin-left: 15px"
                        Display="Dynamic"
                        Text="<%$Resources: Common, validatorFields %>"
                        ControlToValidate="txtEmail" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        ControlToValidate="txtEmail" Display="Dynamic"
                        Text="Sintaxis de email incorrecta"
                        ForeColor="Red"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        meta:resourcekey="revEmail"></asp:RegularExpressionValidator>
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Label CssClass="form-label" ID="lblPais" runat="server" Width="135px" meta:resourcekey="txtPais"></asp:Label>
                </span><span class="entry">
                    <asp:DropDownList ID="comboPais" CssClass="form-control" runat="server" Width="100px"
                        meta:resourcekey="comboCountryResource1">
                    </asp:DropDownList>
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Label CssClass="form-label" ID="lblIdioma" runat="server" Width="135px" meta:resourcekey="txtIdioma"></asp:Label>
                </span><span class="entry">
                    <asp:DropDownList ID="comboIdioma" CssClass="form-control" runat="server" AutoPostBack="True"
                        Width="100px" meta:resourcekey="comboLanguageResource1"
                        OnSelectedIndexChanged="ComboLanguageSelectedIndexChanged">
                    </asp:DropDownList>
                </span>
            </div>

            <div class="field">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Pages/User/ChangePassword.aspx" Width="150px"
                    meta:resourcekey="txtChangePw" Font-Bold="True" Font-Italic="False"
                    Font-Overline="False" Font-Underline="False" ForeColor="Black" />
            </div>

            <div class="field">
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Pages/User/AddCreditCard.aspx" Width="150px"
                    meta:resourcekey="txtAddCC" Font-Bold="True" Font-Italic="False"
                    Font-Overline="False" Font-Underline="False" ForeColor="Black" />
            </div>

            <div class="field">
                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Pages/User/ManageCreditCards.aspx" Width="150px"
                    meta:resourcekey="txtManageCC" Font-Bold="True" Font-Italic="False"
                    Font-Overline="False" Font-Underline="False" ForeColor="Black"/>
            </div>

            <center>
                <asp:Button CssClass="btn btn-primary" ID="btnUpdateProfile" runat="server" meta:resourcekey="txtUpdate" OnClick="btnUpdateProfile_Click" />
            </center>
            <br />
            <div>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </div>


        </form>
    </div>
</asp:Content>
