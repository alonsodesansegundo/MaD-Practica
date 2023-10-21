<%@ Page Language="C#" MasterPageFile="~/MyMasterPage.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs"
    Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.Register" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <div id="form" class="container mt-4">
        <div class="card">
            <div class="card-header">
                <!-- Titulo de la página -->
                <center>
                    <asp:Label CssClass="form-label" ID="LabelRegTitle" runat="server" Font-Bold="True" meta:resourcekey="LabelRegTitleResource1"></asp:Label>
                </center>
            </div>

            <form id="RegisterForm" method="POST" runat="server">

                <div class="field">
                    <span class="label">
                        <asp:Label CssClass="form-label" ID="lblLogin" runat="server" Width="135px" meta:resourcekey="txtLogin"></asp:Label>
                    </span><span class="entry">
                        <asp:TextBox CssClass="form-control" ID="txtLogin" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:Label ID="lblErrorLogin" runat="server" meta:resourcekey="txtObligatorio"></asp:Label>
                        <asp:Label ID="lblRepeatLogin" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="txtLoginRepetido"></asp:Label>
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Label CssClass="form-label" ID="lblPassword" runat="server" Width="135px" meta:resourcekey="txtPassword"></asp:Label>
                    </span><span class="entry">
                        <asp:TextBox CssClass="form-control" ID="txtPassword" runat="server" TextMode="Password" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                            ErrorMessage="Campo Obligatorio (resumen)"
                            Style="margin-left: 15px"
                            Display="Dynamic"
                            Text="<%$Resources: Common, validatorFields %>"
                            ControlToValidate="txtPassword" />
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Label CssClass="form-label" ID="lblRepeatPassword" runat="server" Width="135px" meta:resourcekey="txtRepeatPassword"></asp:Label>
                    </span><span class="entry">
                        <asp:TextBox CssClass="form-control" ID="txtRetypePassword" runat="server" TextMode="Password" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRetypePassword" runat="server" ControlToValidate="txtRetypePassword"
                            Display="Dynamic" Text="<%$ Resources:Common, validatorFields %>" />
                        <asp:CompareValidator ID="cvPasswordCheck" runat="server" ControlToCompare="txtPassword"
                            ControlToValidate="txtRetypePassword" Text="No coinciden las contraseñas" meta:resourcekey="cvPasswordCheck"></asp:CompareValidator>
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Label CssClass="form-label" ID="lblNombre" runat="server" Width="135px" meta:resourcekey="txtNombre"></asp:Label>
                    </span><span class="entry">

                        <asp:TextBox CssClass="form-control" ID="txtNombre" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server"
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
                        <asp:TextBox CssClass="form-control" ID="txtDir" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="lblErrorDir" runat="server"
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
                        <asp:TextBox CssClass="form-control" ID="txtEmail" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtDir"
                            Display="Dynamic" Text="<%$ Resources:Common, validatorFields %>" />
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            Text="Sintaxis de email incorrecta"
                            Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            meta:resourcekey="revEmail" ForeColor="#FF3300">
                        </asp:RegularExpressionValidator>
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Label CssClass="form-label" ID="lblPais" runat="server" Width="135px" meta:resourcekey="txtPais"></asp:Label>
                    </span><span class="entry">
                        <asp:DropDownList ID="comboCountry" CssClass="form-control" runat="server" Width="100px"
                            meta:resourcekey="comboCountryResource1">
                        </asp:DropDownList>
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Label CssClass="form-label" ID="lblIdioma" runat="server" Width="135px" meta:resourcekey="txtIdioma"></asp:Label>
                    </span><span class="entry">
                        <asp:DropDownList ID="comboLanguage" CssClass="form-control" runat="server" AutoPostBack="True"
                            Width="100px" meta:resourcekey="comboLanguageResource1"
                            OnSelectedIndexChanged="ComboLanguageSelectedIndexChanged">
                        </asp:DropDownList>
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Label CssClass="form-label" ID="lblUsuario" runat="server" Width="135px" meta:resourcekey="txtUsuario"></asp:Label>
                    </span><span class="entry">
                        <asp:DropDownList ID="comboUser" CssClass="form-control" runat="server" AutoPostBack="False"
                            Width="100px">
                        </asp:DropDownList>
                    </span>
                </div>

                <div class="button">
                    <asp:Button CssClass="btn btn-primary" ID="btnRegister" runat="server" meta:resourcekey="txtCrear" OnClick="btnRegister_Click" />
                </div>
                <br />

            </form>

        </div>
    </div>
</asp:Content>
