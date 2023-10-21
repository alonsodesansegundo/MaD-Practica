<%@ Page Language="C#" MasterPageFile="~/MyMasterPage.Master" AutoEventWireup="true" CodeBehind="UpdateProduct.aspx.cs"
    Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Product.UpdateProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="form" class="container mt-4">
        <div class="card">
            <div class="card-header">
                <!-- Titulo de la página -->
                <center>
                    <asp:Label CssClass="form-label" ID="LabelRegTitle" runat="server" Font-Bold="True" meta:resourcekey="LabelRegTitleResource3"></asp:Label>
                </center>
            </div>

            <form id="RegisterForm" method="POST" runat="server">

                <div class="field">
                    <span class="label">
                        <asp:Label CssClass="form-label" ID="lblNombre" runat="server" Width="135px" meta:resourcekey="txtNombreP"></asp:Label>
                    </span><span class="entry">
                        <asp:TextBox CssClass="form-control" ID="txtNombre" Width="100px" Columns="16" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                            ErrorMessage="Campo Obligatorio (resumen)"
                            Style="margin-left: 15px"
                            Display="Dynamic"
                            Text="<%$Resources: Common, validatorFields %>"
                            ControlToValidate="txtNombre" />
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Label CssClass="form-label" ID="lblDescripcion" runat="server" Width="135px" meta:resourcekey="txtDescripcion"></asp:Label>
                    </span><span class="entry">
                        <asp:TextBox CssClass="form-control" ID="txtDescripcion" Width="100px" Columns="16" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server"
                            ErrorMessage="Campo Obligatorio (resumen)"
                            Style="margin-left: 15px"
                            Display="Dynamic"
                            Text="<%$Resources: Common, validatorFields %>"
                            ControlToValidate="txtDescripcion" />
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Label CssClass="form-label" ID="lblPrecio" runat="server" Width="135px" meta:resourcekey="txtPrecio"></asp:Label>
                    </span><span class="entry">
                        <asp:TextBox CssClass="form-control" ID="txtPrecio" Width="100px" Columns="16" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPrecio" runat="server"
                            ErrorMessage="Campo Obligatorio (resumen)"
                            Style="margin-left: 15px"
                            Display="Dynamic"
                            Text="<%$Resources: Common, validatorFields %>"
                            ControlToValidate="txtPrecio" />
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Label CssClass="form-label" ID="lblFecha" runat="server" Width="135px" meta:resourcekey="txtFecha"></asp:Label>
                    </span><span class="entry">
                        <asp:TextBox CssClass="form-control" ID="txtFecha" Width="100px" Columns="16" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFecha" runat="server"
                            ErrorMessage="Campo Obligatorio (resumen)"
                            Style="margin-left: 15px"
                            Display="Dynamic"
                            Text="<%$Resources: Common, validatorFields %>"
                            ControlToValidate="txtFecha" />
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Label CssClass="form-label" ID="lblStock" runat="server" Width="135px" meta:resourcekey="txtStock"></asp:Label>
                    </span><span class="entry">
                        <asp:TextBox CssClass="form-control" ID="txtStock" Width="100px" Columns="16" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvStock" runat="server"
                            ErrorMessage="Campo Obligatorio (resumen)"
                            Style="margin-left: 15px"
                            Display="Dynamic"
                            Text="<%$Resources: Common, validatorFields %>"
                            ControlToValidate="txtStock" />
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Label CssClass="form-label" ID="lblEditorial" runat="server" Width="135px" meta:resourcekey="txtEditorial" Visible="false"></asp:Label>
                    </span><span class="entry">
                        <asp:TextBox CssClass="form-control" ID="txtEditorial" Width="100px" Columns="16" runat="server" Visible="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEditorial" runat="server"
                            ErrorMessage="Campo Obligatorio (resumen)"
                            Style="margin-left: 15px"
                            Display="Dynamic"
                            Text="<%$Resources: Common, validatorFields %>"
                            ControlToValidate="txtEditorial" />
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Label CssClass="form-label" ID="lblTitulo" runat="server" Width="135px" meta:resourcekey="txtTitulo" Visible="false"></asp:Label>
                    </span><span class="entry">
                        <asp:TextBox CssClass="form-control" ID="txtTitulo" Width="100px" Columns="16" runat="server" Visible="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTitulo" runat="server"
                            ErrorMessage="Campo Obligatorio (resumen)"
                            Style="margin-left: 15px"
                            Display="Dynamic"
                            Text="<%$Resources: Common, validatorFields %>"
                            ControlToValidate="txtTitulo" />
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Label CssClass="form-label" ID="lblISBN" runat="server" Width="135px" meta:resourcekey="txtISBN" Visible="false"></asp:Label>
                    </span><span class="entry">
                        <asp:TextBox CssClass="form-control" ID="txtISBN" Width="100px" Columns="16" runat="server" Visible="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvISBN" runat="server"
                            ErrorMessage="Campo Obligatorio (resumen)"
                            Style="margin-left: 15px"
                            Display="Dynamic"
                            Text="<%$Resources: Common, validatorFields %>"
                            ControlToValidate="txtISBN" />
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Label CssClass="form-label" ID="lblDuracion" runat="server" Width="135px" meta:resourcekey="txtDuracion" Visible="false"></asp:Label>
                    </span><span class="entry">
                        <asp:TextBox CssClass="form-control" ID="txtDuracion" Width="100px" Columns="16" runat="server" Visible="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvDuracion" runat="server"
                            ErrorMessage="Campo Obligatorio (resumen)"
                            Style="margin-left: 15px"
                            Display="Dynamic"
                            Text="<%$Resources: Common, validatorFields %>"
                            ControlToValidate="txtDuracion" />
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Label CssClass="form-label" ID="lblEdicion" runat="server" Width="135px" meta:resourcekey="txtEdicion" Visible="false"></asp:Label>
                    </span><span class="entry">
                        <asp:TextBox CssClass="form-control" ID="txtEdicion" Width="100px" Columns="16" runat="server" Visible="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEdicion" runat="server"
                            ErrorMessage="Campo Obligatorio (resumen)"
                            Style="margin-left: 15px"
                            Display="Dynamic"
                            Text="<%$Resources: Common, validatorFields %>"
                            ControlToValidate="txtEdicion" />
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Label CssClass="form-label" ID="lblEstreno" runat="server" Width="135px" meta:resourcekey="txtEstreno" Visible="false"></asp:Label>
                    </span><span class="entry">
                        <asp:TextBox CssClass="form-control" ID="txtEstreno" Width="100px" Columns="16" runat="server" Visible="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEstreno" runat="server"
                            ErrorMessage="Campo Obligatorio (resumen)"
                            Style="margin-left: 15px"
                            Display="Dynamic"
                            Text="<%$Resources: Common, validatorFields %>"
                            ControlToValidate="txtEstreno" />
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Label CssClass="form-label" ID="lblPg" runat="server" Width="135px" meta:resourcekey="txtPg" Visible="false"></asp:Label>
                    </span><span class="entry">
                        <asp:TextBox CssClass="form-control" ID="txtPg" Width="100px" Columns="16" runat="server" Visible="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPg" runat="server"
                            ErrorMessage="Campo Obligatorio (resumen)"
                            Style="margin-left: 15px"
                            Display="Dynamic"
                            Text="<%$Resources: Common, validatorFields %>"
                            ControlToValidate="txtPg" />
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Label CssClass="form-label" ID="lblPub" runat="server" Width="135px" meta:resourcekey="txtPub" Visible="false"></asp:Label>
                    </span><span class="entry">
                        <asp:TextBox CssClass="form-control" ID="txtPub" Width="100px" Columns="16" runat="server" Visible="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPub" runat="server"
                            ErrorMessage="Campo Obligatorio (resumen)"
                            Style="margin-left: 15px"
                            Display="Dynamic"
                            Text="<%$Resources: Common, validatorFields %>"
                            ControlToValidate="txtPub" />
                    </span>
                    <br />
                    <div>
                        <center>
                            <asp:Button CssClass="btn btn-primary" ID="Button1" runat="server" meta:resourcekey="txtUpdateP" OnClick="btnUpdateProduct_Click" />
                        </center>
                    </div>
                    <br />

                    <div>
                        <center>
                            <asp:Label ID="lblMensaje" runat="server"></asp:Label>

                        </center>
                    </div>

                    <br />
                </div>
            </form>
        </div>
    </div>
</asp:Content>
