using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping
{
    public partial class ShoppingCart : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["redirectPostLogin"] != null)
                Session.Remove("redirectPostLogin");

            if (Session["toProductDetails"] != null)
            {
                Session.Remove("toProductDetails");
                Session.Remove("txtComment");
                Session.Remove("productId");
            }
            if (!IsPostBack)
            {
                List<ShoppingCartItem> lista = SessionManager.VisualizeShoppingCart(Context);

                this.gvShoppingCartItems.DataSource = lista;
                this.gvShoppingCartItems.Columns[1].HeaderText = GetLocalResourceObject("nameProduct").ToString();
                this.gvShoppingCartItems.Columns[2].HeaderText = GetLocalResourceObject("productPriceActual").ToString();
                this.gvShoppingCartItems.Columns[3].HeaderText = GetLocalResourceObject("isGiftProduct").ToString();
                this.gvShoppingCartItems.Columns[4].HeaderText = GetLocalResourceObject("quantity").ToString();

                this.gvShoppingCartItems.DataBind();

                Model.ShoppingService.ShoppingCart shoppingCart = SessionManager.GetShoppingCart(Context);
                if (lista.Count != 0)
                {
                    this.lblTextEmptyCart.Visible = false;
                }
                else
                {
                    this.btnDoOrder.Enabled = false;
                }

                this.lblTotalPrice.Text = shoppingCart.TotalPrice.ToString() + " €";
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Checking the RowType of the Row  
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[3].Text == "True")
                {
                    e.Row.Cells[3].Text = GetLocalResourceObject("GiftProduct").ToString(); ;
                }
                if (e.Row.Cells[3].Text == "False")
                {
                    e.Row.Cells[3].Text = GetLocalResourceObject("NotGiftProduct").ToString();
                }
            }
        }

        protected void BtnClick(object sender, GridViewCommandEventArgs e)
        {
            string stringProductId = e.CommandArgument.ToString();
            long productId = long.Parse(stringProductId);

            if (e.CommandName == "Remove")
            {
                SessionManager.RemoveItemFromShoppingCart(Context, productId);
                Server.Transfer(Response.ApplyAppPathModifier("~/Pages/Shopping/ShoppingCart.aspx"));
            }
            else if (e.CommandName == "Add")
            {
                //Redirigimos al formulario para añadir cantidad al carrito
                Server.Transfer("~/Pages/Shopping/UpdateQuantity.aspx?product=" + stringProductId);
            }
            else if (e.CommandName == "IsGift")
            {
                //Añadir producto para regalo
                SessionManager.IsGiftProductShoppingCart(Context, productId, true);
                //Actualizamos la página
                Server.Transfer(Response.ApplyAppPathModifier("~/Pages/Shopping/ShoppingCart.aspx"));
            }
            else if (e.CommandName == "NotIsGift")
            {
                //Añadir producto para regalo
                SessionManager.IsGiftProductShoppingCart(Context, productId, false);
                //Actualizamos la página
                Server.Transfer(Response.ApplyAppPathModifier("~/Pages/Shopping/ShoppingCart.aspx"));
            }
        }

        protected void btnDoOrder_Click(object sender, EventArgs e)
        {
            if (SessionManager.GetUserSession(Context)==null)
            {
                Session["redirectPostLogin"] = "true";
                Response.Redirect("~/Pages/User/Authentication.aspx");
            }
            else {
                Response.Redirect("~/Pages/Shopping/RegisterOrder.aspx");
            }
        }
    }
}