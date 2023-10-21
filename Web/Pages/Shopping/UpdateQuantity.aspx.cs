using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping
{
    public partial class UpdateQuantity : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IProductService productService = iocManager.Resolve<IProductService>();

                string stringProductId = Request.QueryString["product"].ToString();
                long productId = long.Parse(stringProductId);

                var product = productService.FindProduct(productId);
                lblName.Text = product.name.ToString();
                lblStock.Text = product.stock.ToString();

                Model.ShoppingService.ShoppingCart shoppingCart = SessionManager.GetShoppingCart(Context);

                int aux = shoppingCart.shoppingCartItems.Count;
                for (int i = 0; i < aux; i++)
                {
                    ShoppingCartItem item = shoppingCart.shoppingCartItems.ElementAt(i);

                    if (item.product.productId == product.productId)
                    {
                        lblStockActual.Text = item.quantity.ToString();
                    }
                }               

                // Crear un array de valores que se agregarán al DropDownList
                string[] valores = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};

                // Recorrer el array y agregar cada valor al DropDownList
                foreach (string valor in valores)
                {
                    comboQuantity.Items.Add(valor);
                }
            }    
        }

        protected void BtnUpdateQuantity_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    string stringProductId = Request.QueryString["product"].ToString();
                    long productId = long.Parse(stringProductId);

                    SessionManager.UpdateQuantityShoppingCart(Context, productId, int.Parse(comboQuantity.Text));

                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Shopping/ShoppingCart.aspx"));
                }
                catch (NotStockEnough)
                {
                    string mensaje = GetLocalResourceObject("lblUpdateQuantityError").ToString();
                    string script = "alert(\"" + mensaje + "\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                          "ServerControlScript", script, true);
                    return;
                }         
   
            }
        }

    }
}