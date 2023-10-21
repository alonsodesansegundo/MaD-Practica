<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.Master" AutoEventWireup="true" CodeBehind="MyOrders.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.MyOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <asp:Label ID="lblOrders" runat="server" meta:resourcekey="txtMyOrders" Font-Bold="True"></asp:Label>
    </center>
    <form id="MyOrders" method="POST" runat="server">
        <div>
            <center>

                <asp:Label ID="lblNoOrders" meta:resourcekey="notOrders" runat="server"></asp:Label>
                <br />
                <asp:GridView ID="gvOrders" runat="server" CssClass="products"
                    CellPading="300" HorizontalAlign="Center"
                    AutoGenerateColumns="False"
                    ControlStyle-VerticalAlign="middle">
                    <Columns>
                        <asp:HyperLinkField DataTextField="name" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center"
                            DataNavigateUrlFormatString="~/Pages/User/ShowOrderDetails.aspx?orderId={0}"
                            DataNavigateUrlFields="orderId" />
                        <asp:BoundField ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center"
                            DataField="creationDate" DataFormatString="{0:dd/MM/yyyy hh:mm}" />
                        <asp:BoundField ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center"
                            DataField="totalPrice" />
                    </Columns>
                </asp:GridView>
                <br />
                <div class="previousNextLinks">
                    <span class="previousLink">
                        <asp:HyperLink ID="lnkPrevious" runat="server" Visible="False" meta:resourcekey="txtPrevious" />
                    </span>
                    <span class="nextLink">
                        <asp:HyperLink ID="lnkNext" runat="server" Visible="False" meta:resourcekey="txtNext" />
                    </span>
                </div>
            </center>
        </div>
    </form>
</asp:Content>
