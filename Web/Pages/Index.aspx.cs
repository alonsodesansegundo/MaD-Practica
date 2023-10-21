using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class Index : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["redirectPostLogin"] != null)
                    Session.Remove("redirectPostLogin");

                if (Context.Session["toProductDetails"] == null)
                    Session.Add("toProductDetails", false);

                if (Session["toProductDetails"] != null) {
                    Session.Remove("toProductDetails");
                    Session.Remove("txtComment");
                    Session.Remove("productId");
                }

                /* Si no se ha creado el carrito, lo creo vacío */
                if (Context.Session["shoppingCart"] == null)
                    SessionManager.CreateShoppingCartEmpty(Context);

                loadDesplegable();
            }
        }
        private void loadDesplegable()
        {
            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService productService = iocManager.Resolve<IProductService>();
            /* Damos datos al desplegable */
            List<string> listCategories = new List<string>();
            listCategories.Add(GetGlobalResourceObject("Common", "txtCategorias").ToString());
            List<Category> categories = productService.FindAllCategories();
            foreach (Category c in categories)
                listCategories.Add(c.categoryName);
            ddCategories.DataSource = listCategories;
            ddCategories.DataBind();
            // valor por defecto
            ddCategories.SelectedValue = ddCategories.Items[0].Text;
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string keywords = this.txtBusqueda.Text;
            string category = this.ddCategories.SelectedValue;

            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Product/ShowProducts.aspx"
                    + "?keywords=" + keywords + "&category=" + category));
        }
    }
}