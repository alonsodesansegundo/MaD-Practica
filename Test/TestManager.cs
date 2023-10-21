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
using System.Configuration;
using System.Data.Entity;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    class TestManager
    {
        /// <summary>
        /// Configures and populates the Ninject kernel
        /// </summary>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel()
        {
            NinjectSettings settings = new NinjectSettings() { LoadExtensions = true };

            IKernel kernel = new StandardKernel(settings);

            kernel.Bind<IUserDao>().
                To<UserDaoEntityFramework>();

            kernel.Bind<IUserService>().
                To<UserService>();

            kernel.Bind<IShoppingService>().
                To<ShoppingService>();

            kernel.Bind<ICommentService>().
                To<CommentService>();

            kernel.Bind<ICommentDao>().
               To<CommentDaoEntityFramework>();

            kernel.Bind<ITagDao>().
               To<TagDaoEntityFramework>();

            kernel.Bind<IOrderDao>().
                To<OrderDaoEntityFramework>();

            kernel.Bind<IOrderLineDao>().
                To<OrderLineDaoEntityFramework>();

            kernel.Bind<IProductDao>().
                To<ProductDaoEntityFramework>();

            kernel.Bind<IProductService>().
                To<ProductService>();

            kernel.Bind<IMovieDao>().
                To<MovieDaoEntityFramework>();

            kernel.Bind<IBookDao>().
                To<BookDaoEntityFramework>();

            kernel.Bind<ICreditCardDao>().
                To<CreditCardDaoEntityFramework>();

            kernel.Bind<ICategoryDao>().
                To<CategoryDaoEntityFramework>();

            string connectionString =
                ConfigurationManager.ConnectionStrings["ShoppingEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);

            return kernel;
        }

        /// <summary>
        /// Configures the Ninject kernel from an external module file.
        /// </summary>
        /// <param name="moduleFilename">The module filename.</param>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel(string moduleFilename)
        {
            NinjectSettings settings = new NinjectSettings() { LoadExtensions = true };
            IKernel kernel = new StandardKernel(settings);

            kernel.Load(moduleFilename);

            return kernel;
        }

        public static void ClearNInjectKernel(IKernel kernel)
        {
            kernel.Dispose();
        }
    }
}
