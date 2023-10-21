<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <asp:Label ID="lblChangePw" runat="server" meta:resourcekey="txtChangePw" Font-Bold="True"></asp:Label>
    </center>
    <form id="form">
        <form id="UpdatePassword" method="post" runat="server">
            <div class="field">
                <span class="label">
                    <asp:Label ID="Label1" CssClass="form-label" runat="server" meta:resourcekey="txtPwOld"></asp:Label>
                </span><span class="entry">
                    <asp:TextBox ID="txtOldPw" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPasswordActual" runat="server"
                        ControlToValidate="txtOldPw" />
                    <asp:Label ID="lblOldPasswordError" runat="server" ForeColor="Red" Visible="False" 
                        meta:resourcekey="lblOldPasswordError"></asp:Label>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Label ID="Label2" CssClass="form-label" runat="server" meta:resourcekey="txtPwNew"></asp:Label>
                </span><span class="entry">
                    <asp:TextBox ID="txtNewPw" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNuevaPassword" runat="server" ControlToValidate="txtNewPw" />
                    <asp:CompareValidator ID="cvPasswordsMatch" runat="server"
                        ControlToValidate="txtOldPw"
                        ControlToCompare="txtNewPw"
                        Operator="NotEqual" />
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Label ID="Label4" CssClass="form-label" runat="server" meta:resourcekey="txtPwRepeat"></asp:Label>
                </span><span class="entry">
                    <asp:TextBox ID="txtRepeatPw" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvConfirmacionPassword" runat="server"
                        ControlToValidate="txtRepeatPw" />
                    <asp:CompareValidator ID="cvConfirmacionPassword" runat="server"
                        ControlToValidate="txtNewPw" ControlToCompare="txtRepeatPw" />
                </span>
            </div>
            <br />
            <br />
            <center>
                <asp:Button CssClass="btn btn-primary" ID="btnUpdatePw" runat="server" meta:resourcekey="txtChangePw" OnClick="btnUpdatePw_Click" />
            </center>
            <br />
        </form>
    </form>
</asp:Content>
