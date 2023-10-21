using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.BookDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.MovieDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.Util.IoC
{
    internal class IoCManagerNinject : IIoCManager
    {
        private static IKernel kernel;
        private static NinjectSettings settings;

        public void Configure()
        {
            settings = new NinjectSettings() { LoadExtensions = true };
            kernel = new StandardKernel(settings);

            /*** BookDao ***/
            kernel.Bind<IBookDao>().
                To<BookDaoEntityFramework>();

            /*** CategoryDao ***/
            kernel.Bind<ICategoryDao>().
                To<CategoryDaoEntityFramework>();

            /*** CommentDao ***/
            kernel.Bind<ICommentDao>().
                To<CommentDaoEntityFramework>();

            /*** CommentService ***/
            kernel.Bind<ICommentService>()
                .To<CommentService>();

            /*** CreditCardDao ***/
            kernel.Bind<ICreditCardDao>().
                To<CreditCardDaoEntityFramework>();

            /*** MovieDao ***/
            kernel.Bind<IMovieDao>().
                To<MovieDaoEntityFramework>();

            /*** OrderDao ***/
            kernel.Bind<IOrderDao>().
                To<OrderDaoEntityFramework>();

            /*** OrderLineDao ***/
            kernel.Bind<IOrderLineDao>().
                To<OrderLineDaoEntityFramework>();

            /*** ProductDao ***/
            kernel.Bind<IProductDao>().
                To<ProductDaoEntityFramework>();

            /*** ProductService ***/
            kernel.Bind<IProductService>()
                .To<ProductService>();

            /*** ShoppingService ***/
            kernel.Bind<IShoppingService>()
                .To<ShoppingService>();

            /*** TagDao ***/
            kernel.Bind<ITagDao>().
                To<TagDaoEntityFramework>();

            /*** UserDao ***/
            kernel.Bind<IUserDao>().
                To<UserDaoEntityFramework>();

            /*** UserService ***/
            kernel.Bind<IUserService>()
                .To<UserService>();

            /*** DbContext ***/
            string connectionString =
                    ConfigurationManager.ConnectionStrings["ShoppingEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                    ToSelf().
                    InSingletonScope().
                    WithConstructorArgument("nameOrConnectionString", connectionString);
        }

        public T Resolve<T>()
        {
            return kernel.Get<T>();
        }
    }
}