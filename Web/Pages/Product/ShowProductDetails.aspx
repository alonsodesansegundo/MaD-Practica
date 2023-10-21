<%@ Page Language="C#" MasterPageFile="~/MyMasterPage.Master" AutoEventWireup="true" CodeBehind="ShowProductDetails.aspx.cs"
    Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Product.ShowProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        /* Estilo de las columnas */
        .column {
            float: left;
            width: 33.00%;
            padding: 10px;
            box-sizing: border-box;
        }

        /* Estilo de la fila que contiene las columnas */
        .row::after {
            content: "";
            clear: both;
            display: table;
        }

        .badge {
            display: inline-block;
            padding: 0.25em 0.4em;
            font-size: 75%;
            font-weight: 700;
            line-height: 1;
            text-align: center;
            white-space: nowrap;
            vertical-align: baseline;
            border-radius: 10rem;
        }

        .badge-primary {
            color: #fff;
            background-color: #007bff;
        }
    </style>

    <form id="form2" runat="server">
        <div class="row">
            <div class="column">
                <center>
                    <h1>
                        <asp:Label ID="Name" runat="server" Font-Bold="true"></asp:Label>
                    </h1>
                    <br />
                    <asp:Label ID="CreateDate" runat="server"></asp:Label>
                    <br />
                </center>
                <asp:Label ID="Description" runat="server"></asp:Label>
                <br />
                <br />
                <h3>
                    <asp:Label ID="lblPrice" runat="server" meta:resourcekey="price"></asp:Label>
                    <b>
                        <asp:Label ID="Price" runat="server"></asp:Label>
                        €
                    </b>
                </h3>

                <br />
                <asp:Button ID="btnAddProduct" runat="server"
                    CssClass="btn btn-outline-success"
                    OnClick="BtnAddProductClick"
                    CommandArgument='<%# Bind("Id") %>' meta:resourcekey="btnAddProduct"></asp:Button>
                <asp:Button ID="btnEdit" runat="server"
                    CssClass="btn btn-outline-success"
                    OnClick="BtnEditProductClick" meta:resourcekey="btnEditProduct"></asp:Button>

                <br />

                <b>
                    <asp:Label ID="lblStock" runat="server" meta:resourcekey="unitsStock"></asp:Label>
                    <asp:Label ID="Stock" runat="server"></asp:Label>
                </b>
                <br />
                <br />
                <br />

            </div>
            <div class="column">
                <!-- Añado espacios para que quede centrado el texto en las dos columnas -->
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:Label ID="Detalles" runat="server" meta:resourcekey="extraInfo"></asp:Label>
                <br />
                <!-- Texto en detalle de la parte de book -->
                <div>
                    <b>
                        <asp:Label ID="lblEditorial" runat="server" meta:resourcekey="editorial"></asp:Label>
                    </b>
                    <asp:Label ID="Editorial" runat="server" Visible="false"></asp:Label>
                </div>

                <div>
                    <b>
                        <asp:Label ID="lblISBN" runat="server" meta:resourcekey="isbn"></asp:Label>
                    </b>
                    <asp:Label ID="ISBN" runat="server" Visible="false"></asp:Label>
                </div>

                <div>

                    <b>
                        <asp:Label ID="lblEdition" runat="server" meta:resourcekey="edicion"></asp:Label>
                    </b>
                    <asp:Label ID="Edition" runat="server" Visible="false"></asp:Label>
                </div>

                <div>
                    <b>
                        <asp:Label ID="lblPages" runat="server" meta:resourcekey="pags"></asp:Label>
                    </b>
                    <asp:Label ID="Pages" runat="server" Visible="false"></asp:Label><br />
                </div>

                <div>
                    <b>
                        <asp:Label ID="lblPublication" runat="server" meta:resourcekey="publicacion"></asp:Label>
                    </b>
                    <asp:Label ID="Publication" runat="server" Visible="false"></asp:Label><br />
                </div>

                <!-- Texto en detalle de la parte de movie -->
                <div>
                    <b>
                        <asp:Label ID="lblTitleMovie" runat="server" meta:resourcekey="title"></asp:Label>
                    </b>
                    <asp:Label ID="TitleMovie" runat="server" Visible="false"></asp:Label><br />
                </div>

                <div>
                    <b>
                        <asp:Label ID="lblRuntime" runat="server" meta:resourcekey="duration"></asp:Label>
                    </b>
                    <asp:Label ID="Runtime" runat="server" Visible="false"></asp:Label><br />
                </div>

                <div>
                    <b>
                        <asp:Label ID="lblCreationDate" runat="server" meta:resourcekey="estreno"></asp:Label>
                    </b>
                    <asp:Label ID="CreationDate" runat="server" Visible="false"></asp:Label><br />
                </div>

            </div>
            <div class="column">
                <h2>
                    <asp:Label ID="Label1" runat="server" meta:resourcekey="txtComments"></asp:Label>
                </h2>
                <asp:Label ID="lblNotComments" runat="server" Visible="false" meta:resourcekey="lblNotComments"></asp:Label>

                <div style="height: 100px; width: 100px;">
                    <asp:Repeater ID="RepeaterComments" runat="server">
                        <ItemTemplate>
                            <asp:Label CssClass="link-primary" runat="server">
                                <%# DataBinder.Eval(Container.DataItem, "authorLogin") %>
                            </asp:Label>:
                            <asp:Label ID="lblCommentario" runat="server">
                                <%# DataBinder.Eval(Container.DataItem, "commentText") %>
                            </asp:Label>
                            <asp:Label ID="lblUploadDate" runat="server" ForeColor="#666666" Font-Size="Small">
                                <%# String.Format("{0:dd/MM/yyyy HH:mm}", 
                                    DataBinder.Eval(Container.DataItem, "insertDate")) %>
                            </asp:Label>
                            <asp:Label ID="lblTagsList" runat="server" ForeColor="Blue" Font-Size="Small">
                                <%# string.Join(", ", ((List<string>)DataBinder.Eval(Container.DataItem, "tags"))) %>
                            </asp:Label>
                            <br />
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
                <center>
                    <div>
                        <asp:Button ID="btnPreviousPage" runat="server" meta:resourcekey="txtPrevious" OnClick="btnPreviousPage_Click"></asp:Button>
                        <asp:Button ID="btnNextPage" runat="server" meta:resourcekey="txtNext" OnClick="btnNextPage_Click"></asp:Button>
                    </div>
                </center>
                <br />
                <br />
                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine"></asp:TextBox>

                <!--Repeater for the tags-->
                <div class="input-group mt-3 mb-5" runat="server" id="divInputTag">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtbNewTag"></asp:TextBox>
                    <button runat="server" class="btn btn-primary" id="addTagButton" onserverclick="btnAddTag_Click">
                        <i class="fas fa-plus">+</i>
                    </button>
                    <asp:Label ID="lblInvalidTag" runat="server" meta:resourcekey="lblInvalidTag"></asp:Label>
                </div>

                <asp:Label CssClass="form-label" runat="server" meta:resourcekey="lblResourceTag">Tags</asp:Label>
                <asp:Repeater ID="RepeaterAddTags" runat="server">
                    <ItemTemplate>
                        <span class="badge badge-primary">
                            <asp:Label runat="server" ID="lblTagPill" Text='<%# Container.DataItem.ToString() %>'>
                            </asp:Label>
                            <button type="button" class="btn-close" aria-label="Close" runat="server" onserverclick="btnDeleteTag_Click" tag='<%# Container.DataItem.ToString() %>'>x</button>
                        </span>
                    </ItemTemplate>
                </asp:Repeater>

                <div>
                    <asp:Button ID="btnAddComment" runat="server"
                        CssClass="btn btn-outline-success"
                        meta:resourcekey="txtAddComment" OnClick="btnAddComment_Click"></asp:Button>

                    <asp:Button ID="btnUpdateComment" runat="server"
                        CssClass="btn btn-outline-success"
                        meta:resourcekey="txtUpdateComment" OnClick="btnUpdateComment_Click1"></asp:Button>
                    <asp:Button ID="btnRemoveComment" runat="server"
                        CssClass="btn btn-outline-success"
                        meta:resourcekey="txtRemoveComment" OnClick="btnRemoveComment_Click1"></asp:Button>
                </div>
            </div>
        </div>
    </form>

</asp:Content>
