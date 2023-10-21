using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Web;
using System.Globalization;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Product
{
    public partial class UpdateProduct : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserDetails userDetails =
                    SessionManager.FindUserProfileDetails(Context);
            if (!userDetails.Admin)
                Response.Redirect(
                    Response.ApplyAppPathModifier("~/Pages/Errors/InternalError.aspx"));
            lblMensaje.Visible = false;

            if (!IsPostBack)
            {
                long productId = Convert.ToInt64(Request.Params.Get("Id"));

                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IProductService productService = iocManager.Resolve<IProductService>();

                var product = productService.FindProduct(productId);

                txtNombre.Text = product.name;
                txtDescripcion.Text = product.description;
                txtPrecio.Text = product.price.ToString();
                txtFecha.Text = product.createDate.ToString("dd/MM/yyyy");
                txtStock.Text = product.stock.ToString();

                if (product.categoryId == 2)
                {
                    Model.Movie movie = (Model.Movie)product;
                    lblTitulo.Visible = true;
                    txtTitulo.Text = movie.title;
                    txtTitulo.Visible = true;

                    lblDuracion.Visible = true;
                    txtDuracion.Text = movie.runtime.ToString();
                    txtDuracion.Visible = true;

                    lblEstreno.Visible = true;
                    txtEstreno.Text = movie.creationDate.ToString("dd/MM/yyyy");
                    txtEstreno.Visible = true;

                }
                else
                {
                    Model.Book book = (Model.Book)product;
                    lblEditorial.Visible = true;
                    txtEditorial.Text = book.editorial;
                    txtEditorial.Visible = true;

                    lblISBN.Visible = true;
                    txtISBN.Text = book.ISBN;
                    txtISBN.Visible = true;

                    lblEdicion.Visible = true;
                    txtEdicion.Text = book.edition.ToString();
                    txtEdicion.Visible = true;

                    lblPg.Visible = true;
                    txtPg.Text = book.pages.ToString();
                    txtPg.Visible = true;

                    lblPub.Visible = true;
                    txtPub.Text = book.publicationDate.ToString("dd/MM/yyyy");
                    txtPub.Visible = true;
                }
            }
        }
        private void UpdateProd(long userId, Model.Product product)
        {
            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService productService = iocManager.Resolve<IProductService>();

            /* Get Account Data */
            try
            {
                productService.UpdateProduct(userId, product);
                lblMensaje.Text = GetLocalResourceObject("txtCorrectUpdateP").ToString();
            }
            catch (Exception e)
            {
                lblMensaje.Text = GetLocalResourceObject("txtErrorUpdateP").ToString();
            }
            lblMensaje.Visible = true;
        }

        protected void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            long userId = SessionManager.GetUserSession(Context).UserId;
            long productId = Convert.ToInt64(Request.Params.Get("Id"));

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService productService = iocManager.Resolve<IProductService>();
            var product = productService.FindProduct(productId);

            if (product.categoryId == 2)
            {
                Model.Movie movie = (Model.Movie)product;
                movie.name = txtNombre.Text;
                movie.description = txtDescripcion.Text;
                movie.price = Convert.ToDecimal(txtPrecio.Text);
                DateTime fecha, estreno;
                if (DateTime.TryParseExact(txtFecha.Text, "dd/MM/yyyy", 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                {
                    movie.createDate = fecha;
                }
                movie.stock = Convert.ToInt32(txtStock.Text);
                movie.title = txtTitulo.Text;
                movie.runtime = TimeSpan.Parse(txtDuracion.Text);
                if (DateTime.TryParseExact(txtEstreno.Text, "dd/MM/yyyy", 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out estreno))
                {
                    movie.creationDate = estreno;
                }

                UpdateProd(userId, movie);
            }
            else
            {
                Model.Book book = (Model.Book)product;
                book.name = txtNombre.Text;
                book.description = txtDescripcion.Text;
                book.price = Convert.ToDecimal(txtPrecio.Text);
                DateTime fecha, publishing;
                if (DateTime.TryParseExact(txtFecha.Text, "dd/MM/yyyy", 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                {
                    book.createDate = fecha;
                }
                book.stock = Convert.ToInt32(txtStock.Text);
                book.editorial = txtEditorial.Text;
                book.ISBN = txtISBN.Text;
                book.edition = Convert.ToInt32(txtEdicion.Text);
                book.pages = Convert.ToInt32(txtPg.Text);
                if (DateTime.TryParseExact(txtPub.Text, "dd/MM/yyyy", 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out publishing))
                {
                    book.publicationDate = publishing;
                }
                UpdateProd(userId, book);
            }

            Response.Redirect(
                Response.ApplyAppPathModifier("~/Pages/Product/ShowProductDetails.aspx" + "?id=" + productId));
        }
    }
}