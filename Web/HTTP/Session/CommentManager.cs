using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using System.Collections.Generic;
using System.Web;

namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session
{
    public class CommentManager
    {
        private static ICommentService commentService;

        private static IProductService productService;

        public ICommentService CommentService
        {
            set { CommentService = value; }
        }
        public IProductService ProductService
        {
            set { ProductService = value; }
        }

        static CommentManager()
        {
            IIoCManager iocManager =
                (IIoCManager)HttpContext.Current.Application["managerIoC"];

            commentService = iocManager.Resolve<ICommentService>();
            productService = iocManager.Resolve<IProductService>();
        }

        public static CommentBlock FindComentsDetailsByProductIdOrderByCreateDateDesc(long productId, int page, int size)
        {
            return commentService.FindComentsByProductId(productId, page * size, size);
        }

        public static void DeleteTagFromComment(long commentId, string tagName)
        {
             commentService.DeleteTagFromComment(commentId, tagName);
        }

        public static List<TagDetails> GetTagsByUse()
        {
            return commentService.GetTagsByUse2();
        }

    }
}