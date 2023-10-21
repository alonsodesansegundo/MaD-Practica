<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.Master" AutoEventWireup="true" CodeBehind="ShowOrderDetails.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.ShowOrderDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <asp:Label CssClass="form-label" ID="lblOrderDetails" runat="server"
            Font-Bold="True" meta:resourcekey="txtDetails"></asp:Label>
    </center>
    <br />
    <div id="form" class="container mt-4">
        <div class="card">
            <center>
                <div class="card-body">
                    <form id="RegisterForm" method="POST" runat="server">
                        <asp:GridView ID="gvOrderDetails" runat="server" CssClass="products"
                            CellPading="300" HorizontalAlign="Center"
                            AutoGenerateColumns="False"
                            ControlStyle-VerticalAlign="middle">
                            <Columns>
                                <asp:BoundField ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center" DataField="name" />
                                <asp:BoundField ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" DataField="quantity" />
                                <asp:BoundField ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" DataField="unitPrice" />
                            </Columns>
                        </asp:GridView>
                    </form>
                </div>
            </center>
        </div>
    </div>
    <br />
</asp:Content>
