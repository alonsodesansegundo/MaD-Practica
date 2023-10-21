using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Product
{
    public partial class ShowProductDetails : SpecificCulturePage
    {
        private static int currentPage;
        private static List<CommentDetails> comments;
        static private List<string> tags;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblInvalidTag.Visible = false;
            labelsDetailsVisualice();

            //Comments part
            currentPage = 0;
            VisualizeComments();
            long productId = Convert.ToInt64(Request.Params.Get("Id"));

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService productService = iocManager.Resolve<IProductService>();

            var product = productService.FindProduct(productId);

            /* Información común de los productos */
            Name.Text = product.name.ToUpper();
            Description.Text = product.description;
            CreateDate.Text = product.createDate.ToString("dd/MM/yyyy");
            Price.Text = product.price.ToString();
            Stock.Text = product.stock.ToString();


            if (product.categoryId == 2)
            {
                /* Información especifica de Movie */
                Model.Movie movie = (Model.Movie)product;

                TitleMovie.Text = movie.title;
                TitleMovie.Visible = true;
                lblTitleMovie.Visible = true;

                Runtime.Text = movie.runtime.ToString();
                Runtime.Visible = true;
                lblRuntime.Visible = true;

                CreationDate.Text = movie.creationDate.ToString("dd/MM/yyyy");
                CreationDate.Visible = true;
                lblCreationDate.Visible = true;
            }
            else
            {
                /* Información especifica de Book */
                Model.Book book = (Model.Book)product;
                
                Editorial.Text = book.editorial;
                Editorial.Visible = true;
                lblEditorial.Visible = true;

                ISBN.Text = book.ISBN;
                ISBN.Visible = true;
                lblISBN.Visible = true;

                Edition.Text = book.edition.ToString();
                Edition.Visible = true;
                lblEdition.Visible = true;


                Pages.Text = book.pages.ToString();
                Pages.Visible = true;
                lblPages.Visible = true;


                Publication.Text = book.publicationDate.ToString("dd/MM/yyyy");
                Publication.Visible = true;
                lblPublication.Visible = true;

            }

            /* Comprobamos que el usuario sea administrador sino ocultamos el botón de editar producto */
            try
            {
                long userId = SessionManager.GetUserSession(Context).UserId;
                /* Get the Service */
                IUserService userService = iocManager.Resolve<IUserService>();
                UserDetails user = userService.FindUserDetails(userId);
                if (user.Admin)
                {
                    //si el usuario es admin
                    btnEdit.Visible = true;
                }
                else
                {
                    //el usuario está autenticado y no es admin
                    btnEdit.Visible = false;
                }
                getCommentIfExists(userId, productId);
            }
            catch (NullReferenceException)
            {
                //el usuario no está autenticado
                btnEdit.Visible = false;
                btnUpdateComment.Visible = false;
                btnRemoveComment.Visible = false;
            }

            if (!IsPostBack)
            {
                /* Parte de tags */
                tags = new List<string>();

                RepeaterAddTags.DataSource = tags;
                RepeaterAddTags.DataBind();
            }



        }
        
        private void labelsDetailsVisualice()
        {            
            lblTitleMovie.Visible = false;            
            lblRuntime.Visible = false;
            lblCreationDate.Visible = false;
            lblEditorial.Visible = false;
            lblISBN.Visible = false;
            lblEdition.Visible = false;
            lblPages.Visible = false;
            lblPublication.Visible = false;

        }
         

        private void getCommentIfExists(long userId, long productId)
        {
            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICommentService commentService = iocManager.Resolve<ICommentService>();
            Comment comment = commentService.GetComment(userId, productId);

            if (comment != null)
            {
                //si el usuario ya tiene comentario
                btnAddComment.Visible = false;
                if (!IsPostBack)
                    txtComment.Text = comment.commentText;
            }
            else
            {
                if (Session["txtComment"] != null)
                {
                    txtComment.Text = Session["txtComment"].ToString();
                    Session.Remove("txtComment");
                    Session.Remove("productId");
                }
                btnUpdateComment.Visible = false;
                btnRemoveComment.Visible = false;
            }

        }

        protected void BtnAddProductClick(object sender, EventArgs e)
        {
            long productId = Convert.ToInt64(Request.Params.Get("Id"));

            //controlar excepciones que se pueden dar a la hora de añadir un producto al carrito
            try
            {
                SessionManager.AddProductFromShoppingCart(Context, productId, 1);
          
                Response.Redirect(
                    Response.ApplyAppPathModifier("~/Pages/Product/ShowProductDetails.aspx"
                    + "?id=" + productId));
            }
            catch (NotStockEnough)
            {
                btnAddProduct.Enabled = false;
            }
        }

        protected void BtnEditProductClick(object sender, EventArgs e)
        {
            long productId = Convert.ToInt64(Request.Params.Get("Id"));
            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService productService = iocManager.Resolve<IProductService>();

            var product = productService.FindProduct(productId);

            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Product/UpdateProduct.aspx"
                + "?id=" + productId + "&category=" + product.categoryId));

        }

        protected void btnAddComment_Click(object sender, EventArgs e)
        {
            //si el usuario está autenticado
            try
            {
                long userId = SessionManager.GetUserSession(Context).UserId;

                //si está autenticado
                //le dejo añadir un comentario
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                ICommentService commentService = iocManager.Resolve<ICommentService>();
                long productId = Convert.ToInt64(Request.Params.Get("Id"));
                //List<string> tags = new List<string>();
                commentService.AddComment(userId, productId, txtComment.Text, tags);

                btnRemoveComment.Visible = true;
                btnUpdateComment.Visible = true;
                btnAddComment.Visible = false;
                VisualizeComments();
            }
            catch (NullReferenceException)
            {
                //si el usuario no está autenticado
                //lo mando a autenticarse y luego lo traigo de vuelta aqui
                Session.Add("toProductDetails", true);
                long productId = Convert.ToInt64(Request.Params.Get("Id"));
                Session.Add("productId", productId);
                Session.Add("txtComment", txtComment.Text);
                //lo redirigo a la pag de iniciar sesión
                Server.Transfer(Response.ApplyAppPathModifier("~/Pages/User/Authentication.aspx"));
            }

        }

        protected void btnUpdateComment_Click1(object sender, EventArgs e)
        {
            long userId = SessionManager.GetUserSession(Context).UserId;
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICommentService commentService = iocManager.Resolve<ICommentService>();
            //List<string> tags = new List<string>();
            long productId = Convert.ToInt64(Request.Params.Get("Id"));
            Comment comment = commentService.GetComment(userId, productId);
            commentService.UpdateComment(userId, comment.commentId, txtComment.Text, tags);
            VisualizeComments();
        }

        protected void btnRemoveComment_Click1(object sender, EventArgs e)
        {
            long userId = SessionManager.GetUserSession(Context).UserId;
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICommentService commentService = iocManager.Resolve<ICommentService>();
            long productId = Convert.ToInt64(Request.Params.Get("Id"));
            Comment comment = commentService.GetComment(userId, productId);

            commentService.RemoveComment(userId, comment.commentId);
            Response.Redirect(Response.
            ApplyAppPathModifier(Page.Request.Url.ToString()));
            btnUpdateComment.Visible = false;
            btnRemoveComment.Visible = false;
            btnAddComment.Visible = true;
            txtComment.Text = "";
            VisualizeComments();
        }

        private void VisualizeComments() {
            long productId = Convert.ToInt64(Request.Params.Get("Id"));

            btnPreviousPage.Visible = currentPage != 0;

            int size = Settings.Default.ShowCommentsResult;
            var newComments = CommentManager.FindComentsDetailsByProductIdOrderByCreateDateDesc(productId, currentPage,size);
            comments = newComments.Comments;
            if (comments.Count == 0)
                lblNotComments.Visible = true;
            else
                lblNotComments.Visible = false;


            RepeaterComments.DataSource = comments;
            RepeaterComments.DataBind();
            Check_more_comments(newComments.ExistMoreItems);
        }

        private void Check_more_comments(bool moreItems)
        {
            btnNextPage.Visible = moreItems;
        }

        protected void btnPreviousPage_Click(object sender, EventArgs e)
        {
            currentPage--;
            if (currentPage < 0)
                currentPage = 0;
            VisualizeComments();
        }

        protected void btnNextPage_Click(object sender, EventArgs e)
        {
            currentPage++;
            VisualizeComments();
        }

        protected void btnDeleteTag_Click(object sender, EventArgs e)
        {
            var buttonClicked = (HtmlButton)sender;

            var tag = buttonClicked.Attributes["tag"];

            /* Eliminamos los tags de la lista */
            tags.RemoveAll(x => x == tag);
            RepeaterAddTags.DataSource = tags;
            RepeaterAddTags.DataBind();

            /* Lo borramos de los comentarios */
            long userId = SessionManager.GetUserSession(Context).UserId;
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICommentService commentService = iocManager.Resolve<ICommentService>();
            long productId = Convert.ToInt64(Request.Params.Get("Id"));
            Comment comment = commentService.GetComment(userId, productId);

            CommentManager.DeleteTagFromComment(comment.commentId, tag);
        }

        protected void btnAddTag_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var newTag = txtbNewTag.Text;

                if (newTag.Trim() == "")
                {
                    lblInvalidTag.Visible = true;

                    return;
                }

                lblInvalidTag.Visible = false;

                tags.Add(newTag);

               // txtbNewTag.Text = "";
                RepeaterAddTags.DataSource = tags;
                RepeaterAddTags.DataBind();
            }
        }

    }
}