<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.Master" AutoEventWireup="true" CodeBehind="ShowProducts.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Product.ShowProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <asp:DropDownList ID="ddCategories" runat="server">
        </asp:DropDownList>
        <asp:TextBox ID="txtBusqueda" runat="server"></asp:TextBox>
        <asp:Button ID="btnBuscar" runat="server"
            Text="<%$ Resources:Common,txtBuscar %>" OnClick="btnBuscar_Click" />
        <center>
            <asp:GridView ID="gvProducts" runat="server" GridLines="None"
                AutoGenerateColumns="False" CellPadding="15" OnRowCommand="BtnAddProductClick">
                <Columns>
                    <asp:HyperLinkField DataTextField="Name"
                        DataNavigateUrlFormatString="~/Pages/Product/ShowProductDetails.aspx?id={0}"
                        DataNavigateUrlFields="Id" />
                    <asp:BoundField DataField="Price" />
                    <asp:BoundField DataField="Category" />
                    <asp:BoundField DataField="Stock" />
                    <asp:BoundField DataField="CreateDate" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnId" runat="server"
                                CssClass="btn btn-outline-success"
                                meta:resourcekey="addProduct"
                                CommandArgument='<%# Bind("Id") %>'></asp:Button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:Label ID="lblNotProducts" runat="server" Text="Label"></asp:Label>
            <asp:HyperLink ID="lnkPrevious" runat="server">HyperLink</asp:HyperLink>
            <asp:HyperLink ID="lnkNext" runat="server">HyperLink</asp:HyperLink>
        </center>
    </form>
</asp:Content>
