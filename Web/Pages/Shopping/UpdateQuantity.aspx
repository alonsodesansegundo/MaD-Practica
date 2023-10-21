<%@ Page Language="C#" MasterPageFile="~/MyMasterPage.Master" AutoEventWireup="true"
    CodeBehind="UpdateQuantity.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping.UpdateQuantity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        /* Estilo de las columnas */
        .column {
            float: left;
            margin: 0 auto;
            width: 45.00%;
            box-sizing: border-box;
        }

        /* Estilo de la fila que contiene las columnas */
        .row::after {
            content: "";
            clear: both;
            display: table;
        }

        /* Estilo de la fila con padding */
        .row {
            padding: 0 350px;
        }
    </style>

    <div id="form" class="container mt-4; w-50">
        <div class="card">
            <div class="card-header">
                <center>
                    <h3>
                        <asp:Label CssClass="form-label" Style="font-size: 100%;" ID="lblUpdateQuantityTitle" runat="server"
                            meta:resourcekey="lblUpdateQuantityTitleResource1" Font-Bold="True"></asp:Label>
                    </h3>

                </center>
            </div>

            <div class="card-body">
                <form id="UpdateQuantityForm" method="post" runat="server">
                    <div class="row">
                        <div class="column">
                            <h2>
                                <asp:Label ID="lblName" Font-Bold="True" runat="server"></asp:Label>
                            </h2>
                            <asp:Label ID="lblStockText" Style="font-size: 120%;" runat="server" meta:resourcekey="lblStockText"></asp:Label>
                            <asp:Label ID="lblStock" Style="font-size: 120%;" runat="server"></asp:Label>
                        </div>

                        <div class="column">
                            <br />


                            <center>
                                <asp:Label ID="lblTienes" Style="font-size: 120%;" runat="server" meta:resourcekey="lblTienes"></asp:Label>
                                <span class="label"> 
                                    <asp:Label ID="lblStockActual" runat="server" style="font-size: 120%;"></asp:Label>
                                </span>
                                <asp:Label ID="lblProducto" Style="font-size: 120%;" runat="server" meta:resourcekey="lblProducto"></asp:Label>

                                <br />
                                <br />
                                <asp:DropDownList ID="comboQuantity" CssClass="form-control" runat="server" AutoPostBack="False"
                                    Width="100px">
                                </asp:DropDownList>

                                <asp:Button ID="btnUpdateQuantity" runat="server"
                                    OnClick="BtnUpdateQuantity_Click" meta:resourcekey="btnUpdateQuantity" />
                            </center>
                            <br />
                        </div>
                    </div>

                </form>
            </div>
        </div>
    </div>
</asp:Content>
