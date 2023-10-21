using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /* Vaciamos el carrito antes de cerrar sesión */
            SessionManager.RemoveItemsFromShoppingCartNotEmpty(Context);       

            SessionManager.Logout(Context);

            Response.Redirect("~/Pages/Index.aspx");
        }
    }
}