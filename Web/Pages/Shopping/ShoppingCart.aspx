<%@ Page Language="C#" MasterPageFile="~/MyMasterPage.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs"
    Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping.ShoppingCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div id="form" class="container mt-4">
            <div class="card">
                <div class="card-header">
                    <center>
                        <asp:Label CssClass="form-label" ID="lblShoppingCartTitle" runat="server"
                            Font-Bold="True"
                            meta:resourcekey="lblShoppingCartTitleResource1">
                        </asp:Label>
                    </center>
                </div>
                <center>
                    <br />
                    <asp:GridView ID="gvShoppingCartItems" runat="server" CssClass="products"
                        GridLines="None" AutoGenerateColumns="False"
                        OnRowCommand="BtnClick" CellPadding="10"
                        OnRowDataBound="GridView1_RowDataBound">
                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="Black" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnRemove" runat="server"
                                        CssClass="btn btn-primary"
                                        CommandArgument='<%# Bind("Product.productId") %>'
                                        CommandName="Remove"
                                        meta:resourcekey="btnRemove"></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Product.name" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="productPriceActual" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="isGiftProduct" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="quantity" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnAdd" runat="server"
                                        CssClass="btn btn-primary"
                                        meta:resourcekey="btnAdd"
                                        CommandArgument='<%# Bind("Product.productId") %>'
                                        CommandName="Add"></asp:Button>

                                    <asp:Button ID="btnGift" runat="server"
                                        CssClass="btn btn-primary"
                                        meta:resourcekey="btnGift"
                                        CommandArgument='<%# Bind("Product.productId") %>'
                                        CommandName="IsGift"
                                        Visible='<%# Eval("isGiftProduct").ToString() == "False"  %>'></asp:Button>

                                    <asp:Button ID="Button1" runat="server"
                                        CssClass="btn btn-primary"
                                        meta:resourcekey="Button1"
                                        CommandArgument='<%# Bind("Product.productId") %>'
                                        CommandName="NotIsGift"
                                        Visible='<%# Eval("isGiftProduct").ToString() == "True"%>'></asp:Button>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
       
                    <div>
                        <center>
                            <asp:Label ID="lblTextEmptyCart" runat="server" Visible="true" meta:resourcekey="lblTextEmptyCart"></asp:Label>
                        </center>
                    </div>
                    <br />
                    <div>
                        <center>
                            <asp:Label ID="lblTextPrice" runat="server" meta:resourcekey="lblTextPrice"></asp:Label>
                            <asp:Label ID="lblTotalPrice" runat="server"></asp:Label>
                        </center>
                    </div>
                    <br />
                    <div>
                        <center>
                            <asp:Button ID="btnDoOrder" runat="server" meta:resourcekey="btnDoOrder" OnClick="btnDoOrder_Click" />

                        </center>
                    </div>
                    <br />
                </center>
            </div>
        </div>
    </form>
</asp:Content>
