<%@ Page Language="C#" MasterPageFile="~/MyMasterPage.Master" AutoEventWireup="true"
    CodeBehind="Authentication.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.Authentication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <asp:Label ID="Label1" Font-Bold="True" runat="server" meta:resourcekey="LabelTitleAuth"></asp:Label>
    </center>
    <div class="card-body">
        <form id="AuthenticationForm" method="POST" runat="server">
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclLogin" runat="server" meta:resourcekey="lclLogin" />
                    <asp:Label CssClass="form-label" runat="server" Width="100px" Columns="16" meta:resourcekey="LabelResource1"></asp:Label>
                </span><span class="entry">
                    <asp:TextBox CssClass="form-control" ID="txtbLogin" runat="server" Width="100px" Colums="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLogin" runat="server"
                        Style="margin-left: 15px"
                        Display="Dynamic"
                        Text="<%$Resources: Common, validatorFields %>"
                        ControlToValidate="txtbLogin"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblLoginError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblLoginError">                        
                    </asp:Label>             
                                               
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclPassword" runat="server" meta:resourcekey="lclPassword" />
                    <asp:Label CssClass="form-label" runat="server" Width="100px" Columns="16" meta:resourcekey="LabelResource2"></asp:Label>
                </span><span class="entry">
                    <asp:TextBox CssClass="form-control" TextMode="Password" ID="txtPassword" runat="server" Width="100px" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" Style="margin-left: 15px"
                        Display="Dynamic"
                        Text="<%$Resources: Common, validatorFields %>"
                        ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblPasswordError" runat="server" ForeColor="Red" Style="position: relative; margin-left: 15px" Visible="False" meta:resourcekey="lblPasswordError" />
                </span>
            </div>

            <div class="checkbox">
                <asp:Label ID="labCheckbox" runat="server"></asp:Label>
                <asp:CheckBox ID="checkRememberPassword" runat="server" meta:resourcekey="RememberPassword" />
            </div>
            <br>
            <center>
                <asp:Button CssClass="btn btn-primary" ID="btnLogin" runat="server"
                    OnClick="BtnLogin_Click" meta:resourcekey="btnLogin" />
            </center>
            <br />
            <center>
                <span class="label">
                    <asp:Label ID="labRegister" runat="server" meta:resourcekey="labRegisterResource1"></asp:Label>
                    <asp:HyperLink ID="lnkRegister" runat="server" NavigateUrl="~/Pages/User/Register.aspx" meta:resourcekey="lnkRegister" />
                </span>
            </center>
            <br />

        </form>

    </div>
</asp:Content>
