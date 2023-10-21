<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.Master" AutoEventWireup="true" CodeBehind="ManageCreditCards.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.ManageCreditCards" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <asp:Label ID="lblDatos" runat="server" meta:resourcekey="txtMyCards" Font-Bold="True"></asp:Label>
    </center>
    <form id="ManageCreditCards" method="POST" runat="server">
        <div>
            <asp:GridView ID="gvCreditCards" runat="server" CssClass="cards"
                GridLines="None" AutoGenerateColumns="False" CellPadding="15"
                OnRowCommand="OnRowDeleting" OnRowDeleting="gvCreditCards_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="creditType" />
                    <asp:BoundField DataField="number" />
                    <asp:BoundField DataField="expirationDate" DataFormatString="{0:MM/yy}" />
                    <asp:CheckBoxField DataField="defaultCard" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDefault" runat="server"
                                CssClass="btn btn-outline-success"
                                meta:resourcekey="txtDef" CommandName="SelectDefault"
                                ControlStyle-CssClass="btn black-btn"
                                CommandArgument='<%# Bind("creditCardId") %>'></asp:Button>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnId" runat="server"
                                CssClass="btn btn-outline-success"
                                meta:resourcekey="txtDel" CommandName="Delete"
                                ControlStyle-CssClass="btn black-btn"
                                CommandArgument='<%# Bind("creditCardId") %>'></asp:Button><i class='fas fa-trash'></i>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblEmpty" runat="server" meta:resourcekey="txtNotCC"></asp:Label>
            <asp:Label ID="lblNotDelete" runat="server" meta:resourcekey="txtNotDel"></asp:Label>
        </div>
    </form>
</asp:Content>
