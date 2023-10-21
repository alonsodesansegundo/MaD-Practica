using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Product
{
    public partial class ShowProducts : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            this.gvProducts.Columns[0].HeaderText = GetLocalResourceObject("txtNombre").ToString();
            this.gvProducts.Columns[1].HeaderText = GetLocalResourceObject("txtPrecio").ToString();
            this.gvProducts.Columns[2].HeaderText = GetLocalResourceObject("txtCategoria").ToString();
            this.gvProducts.Columns[3].HeaderText = GetLocalResourceObject("txtStock").ToString();
            this.gvProducts.Columns[4].HeaderText = GetLocalResourceObject("txtFechaCreacion").ToString();                        

            this.lnkPrevious.Text= GetLocalResourceObject("txtPrevious").ToString();
            this.lnkNext.Text = GetLocalResourceObject("txtNext").ToString();

            this.lblNotProducts.Text= GetLocalResourceObject("txtNotProducts").ToString();
            if (!IsPostBack) {

                string keywords = Request.Params.Get("keywords").ToString();
                string category = Request.Params.Get("category").ToString();

                loadDesplegable(category);

                int startIndex, count;
                lnkNext.Visible = false;
                lnkPrevious.Visible = false;
                lblNotProducts.Visible = false;

                var tag = Request.QueryString["tag"];

                /* Get Start Index */
                try
                {
                    startIndex = Int32.Parse(Request.Params.Get("startIndex"));
                }
                catch (ArgumentNullException)
                {
                    startIndex = 0;
                }

                /* Get Count */
                try
                {
                    count = Int32.Parse(Request.Params.Get("count"));
                }
                catch (ArgumentNullException)
                {
                    count = Settings.Default.ShowProductsResult;
                    
                }
                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IProductService productService = iocManager.Resolve<IProductService>();

                /* Get Products Info */
                ProductBlock productBlock = null;

                if (tag==null)
                {
                    // Busqueda sin categoria
                    if (category == ddCategories.Items[0].Text)
                    {
                        try
                        {
                            productBlock = productService.FindProducts(keywords, startIndex, count);
                        }
                        catch (InstanceNotFoundException)
                        {
                            lblNotProducts.Visible = true;
                            return;
                        }

                    }
                    else
                    {  // Filtrar por categoria
                        try
                        {
                            productBlock = productService.FindProducts(keywords, ddCategories.SelectedIndex, startIndex, count);
                        }
                        catch (InstanceNotFoundException)
                        {
                            lblNotProducts.Visible = true;
                            return;
                        }
                    }

                } else
                {//Buscamos por tag
                    try
                    {
                        productBlock = productService.FindProductsByTags(tag, startIndex, count);
                    }
                    catch (InstanceNotFoundException)
                    {
                        lblNotProducts.Visible = true;
                        return;
                    }
                }

                if (productBlock != null)
                {
                    this.gvProducts.DataSource = productBlock.Products;
                    this.gvProducts.DataBind();
                }

                /* "Previous" link */
                if ((startIndex - count) >= 0)
                {
                    String url = "/Pages/Product/ShowProducts.aspx" +
                        "?keywords=" + keywords + "&category=" + category +
                        "&startIndex=" + (startIndex - count) + "&count=" + count;

                    this.lnkPrevious.NavigateUrl =
                        Response.ApplyAppPathModifier(url);
                    this.lnkPrevious.Visible = true;
                }

                /* "Next" link */
                if (productBlock.ExistMoreItems)
                {
                    String url = "/Pages/Product/ShowProducts.aspx" +
                        "?keywords=" + keywords + "&category=" + category +
                        "&startIndex=" + (startIndex + count) + "&count=" + count;

                    this.lnkNext.NavigateUrl = Response.ApplyAppPathModifier(url);
                    this.lnkNext.Visible = true;
                }
            }
        }

        private void loadDesplegable(string category)
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
            ddCategories.SelectedValue = category;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string keywords = this.txtBusqueda.Text;
            string category = this.ddCategories.SelectedValue;

            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Product/ShowProducts.aspx"
                    + "?keywords=" + keywords + "&category=" + category));
        }


        protected void BtnAddProductClick(object sender, GridViewCommandEventArgs e)
        {
            string stringProductId = e.CommandArgument.ToString();
            long productId = long.Parse(stringProductId);

            string keywords = this.txtBusqueda.Text;
            string category = this.ddCategories.SelectedValue;

            /* controlar excepciones que se pueden dar a la hora de añadir un producto al carrito */
            try
            {
                SessionManager.AddProductFromShoppingCart(Context, productId, 1);

                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Product/ShowProducts.aspx"
                    + "?keywords=" + keywords + "&category=" + category));
                // Response.Redirect(Response.
                //      ApplyAppPathModifier(Page.Request.Url.ToString()));

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