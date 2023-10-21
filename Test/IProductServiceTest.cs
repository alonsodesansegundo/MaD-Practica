using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.BookDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.MovieDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService.ExceptionsProduct;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    /// <summary>
    /// Descripción resumida de IProductServiceTest
    /// </summary>
    [TestClass]
    public class IProductServiceTest
    {

        #region Variables test

        // atributos de book
        private const long categoryIdB = 1;
        private const string nameB = "libro";
        private const decimal priceB = 2;
        private DateTime dateB = new DateTime(2001, 06, 18);
        private const int stockB = 1;
        private const string descriptionB = "el libro trata sobre";
        private const string editorial = "Testing";
        private const string ISBN = "952-74-58-06047-1";
        private const int edition = 20;
        private const int pages = 23;
        private DateTime publicationDate = new DateTime(1419, 05, 15);

        private const long productIdBook = 8;
        private const long productIdBook2 = 9;

        //para creación de usuarios
        private const string loginName = "pokemon";
        private const string password = "pikachu";
        private const string firstName = "Ash ";
        private const string lastName = "Ketchum";
        private const string email = "ash_ketchum@udc.es";
        private const string language = "es";
        private const string country = "ES";
        private const string postalAddress = "Pueblo Paleta, Kanto";
        private const bool admin = true;

        private const string NON_EXISTENT_PRODUCT_NAME = "wwwwwwww";

        // atributos de movie
        private const long productIdMovie = 7;

        private const long categoryIdM = 2;
        private const string nameM = "película";
        private const decimal priceM = 2;
        private DateTime dateM = new DateTime(2021, 02, 25);
        private const int stockM = 1;
        private const string descriptionM = "la película trata sobre";
        private const string title = "La película test";
        private TimeSpan runtime = new TimeSpan(1, 30, 0);
        private DateTime creationDate = new DateTime(1999, 09, 19);

        #endregion Variables test

        #region Variables conf test
        private static IKernel kernel;
        private static IProductService productService;
        private static IUserService userService;
        private static IProductDao productDao;
        private static IMovieDao movieDao;
        private static IBookDao bookDao;
        private static ICategoryDao categoryDao;
        private static ICommentDao commentDao;
        private static ICommentService commentService;
        #endregion Variables conf test

        public IProductServiceTest()
        { }

        private TransactionScope transaction;

        public TestContext TestContext { get; set; }

        #region Additional test attributes
        //Use ClassInitialize to run code before running the first test in the class

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            productService = kernel.Get<IProductService>();
            userService = kernel.Get<IUserService>();
            productDao = kernel.Get<IProductDao>();
            categoryDao = kernel.Get<ICategoryDao>();
            bookDao = kernel.Get<IBookDao>();
            movieDao = kernel.Get<IMovieDao>();
            commentDao = kernel.Get<ICommentDao>();
            commentService = kernel.Get<ICommentService>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize]
        public void MyTestInitialize()
        {
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void MyTestCleanup()
        {
        }

        #endregion Additional test attributes


        #region Conjunto de pruebas del servicio de producto

        private Book createBook(long productId)
        {
            Book book = bookDao.Find(productId);
            book.categoryId = categoryIdB;
            book.name = nameB + "test";
            book.price = priceB + 1;
            book.createDate = dateB;
            book.stock = stockB - 1;
            book.description = descriptionB + "como testear bien";
            book.editorial = editorial + " book";
            book.ISBN = ISBN;
            book.edition = edition + 3;
            book.pages = pages + 7;
            book.publicationDate = publicationDate;
            return book;
        }

        private Movie createMovie(long productId)
        {
            Movie movie = movieDao.Find(productId);
            movie.categoryId = categoryIdM;
            movie.name = nameM + "test";
            movie.price = priceM + 1;
            movie.createDate = dateM;
            movie.stock = stockM - 1;
            movie.description = descriptionM + "como testear bien";
            movie.title = title + " 2";
            movie.runtime = runtime;
            movie.creationDate = creationDate;
            return movie;
        }

        /// <summary>
        /// A test for UpdateProduct
        /// </summary>
        [TestMethod]
        public void UpdateProductTest()
        {
            using (var scope = new TransactionScope())
            {
                Book expectedB = createBook(productIdBook);
                Movie expectedM = createMovie(productIdMovie);

                UserDetails details = new UserDetails(firstName, lastName, email, postalAddress, country, language, admin);
                var userId = userService.RegisterUser(loginName, password, details);

                productService.UpdateProduct(userId, (Product)expectedB);
                productService.UpdateProduct(userId, (Product)expectedM);

                var obtainedB = productDao.Find(expectedB.productId);
                var obtainedM = productDao.Find(expectedM.productId);

                // Check changes
                Assert.AreEqual(expectedB, obtainedB);
                Assert.AreEqual(expectedM, obtainedM);
            }
        }

        /// <summary>
        /// A test for UpdateProducts when the product does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void UpdateProductForNonExistingProductTest()
        {
            using (var scope = new TransactionScope())
            {
                Book book = new Book();
                UserDetails details = new UserDetails(firstName, lastName, email, postalAddress, country, language, false);
                var userId = userService.RegisterUser(loginName, password, details);

                productService.UpdateProduct(userId, (Product)book);
            }
        }

        /// <summary>
        /// A test for UpdateProducts when the not admin user try to update the product
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotPermittedUpdateProductException))]
        public void UpdateProductForNonAdminUserTest()
        {
            using (var scope = new TransactionScope())
            {
                Book book = createBook(productIdBook);

                UserDetails details = new UserDetails(firstName, lastName, email, postalAddress, country, language, false);
                var userId = userService.RegisterUser(loginName, password, details);

                productService.UpdateProduct(userId, (Product)book);
            }
        }




        [TestMethod]
        public void FindProducts_TestLibro()
        {
            using (var scope = new TransactionScope())
            {
                Product p1 = productDao.Find(productIdBook);
                ProductSummary ps1 = new ProductSummary();
                ps1.Id = p1.productId;
                ps1.Name = p1.name;
                ps1.Price = p1.price;
                ps1.Stock = p1.stock;
                ps1.CreateDate = p1.createDate;
                ps1.Category = p1.Category.categoryName;

                Product p2 = productDao.Find(productIdBook2);
                ProductSummary ps2 = new ProductSummary();
                ps2.Id = p2.productId;
                ps2.Name = p2.name;
                ps2.Price = p2.price;
                ps2.Stock = p2.stock;
                ps2.CreateDate = p2.createDate;
                ps2.Category = p2.Category.categoryName;

                var obtainedProducts = productService.FindProducts("lIBro", 0, 4);
                Assert.AreEqual(ps1, obtainedProducts.Products[0]);
                Assert.AreEqual(ps2, obtainedProducts.Products[1]);
            }
        }

        [TestMethod]
        public void FindProducts_TestPeli()
        {
            using (var scope = new TransactionScope())
            {
                Product p1 = productDao.Find(productIdMovie);
                ProductSummary ps1 = new ProductSummary();
                ps1.Id = p1.productId;
                ps1.Name = p1.name;
                ps1.Price = p1.price;
                ps1.Stock = p1.stock;
                ps1.CreateDate = p1.createDate;
                ps1.Category = p1.Category.categoryName;

                var obtainedProducts = productService.FindProducts("peLícUlA", 0, 4);
                Assert.AreEqual(ps1, obtainedProducts.Products[0]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindProducts_NotExistProduct()
        {
            using (var scope = new TransactionScope())
            {
                var obtainedProducts = productService.FindProducts("fdavcxcbsnbzvczdfavxc", 0, 4);
            }
        }

        [TestMethod]
        public void FindProducts_TestWithCategoryBook()
        {
            using (var scope = new TransactionScope())
            {
                long firstProduct = 2;
                long secondProduct = 4;

                Product p1 = productDao.Find(firstProduct);
                ProductSummary ps1 = new ProductSummary();
                ps1.Id = p1.productId;
                ps1.Name = p1.name;
                ps1.Price = p1.price;
                ps1.Stock = p1.stock;
                ps1.CreateDate = p1.createDate;
                ps1.Category = p1.Category.categoryName;

                Product p2 = productDao.Find(secondProduct);
                ProductSummary ps2 = new ProductSummary();
                ps2.Id = p2.productId;
                ps2.Name = p2.name;
                ps2.Price = p2.price;
                ps2.Stock = p2.stock;
                ps2.CreateDate = p2.createDate;
                ps2.Category = p2.Category.categoryName;

                var obtainedProducts = productService.FindProducts("", 1, 0, 2);
                Assert.AreEqual(ps1, obtainedProducts.Products[0]);
                Assert.AreEqual(ps2, obtainedProducts.Products[1]);
            }
        }

        [TestMethod]
        public void FindProducts_TestWithCategoryMovie()
        {
            using (var scope = new TransactionScope())
            {
                long firstProduct = 7;
                long secondProduct = 3;

                Product p1 = productDao.Find(firstProduct);
                ProductSummary ps1 = new ProductSummary();
                ps1.Id = p1.productId;
                ps1.Name = p1.name;
                ps1.Price = p1.price;
                ps1.Stock = p1.stock;
                ps1.CreateDate = p1.createDate;
                ps1.Category = p1.Category.categoryName;

                Product p2 = productDao.Find(secondProduct);
                ProductSummary ps2 = new ProductSummary();
                ps2.Id = p2.productId;
                ps2.Name = p2.name;
                ps2.Price = p2.price;
                ps2.Stock = p2.stock;
                ps2.CreateDate = p2.createDate;
                ps2.Category = p2.Category.categoryName;

                var obtainedProducts = productService.FindProducts("", 2, 0, 2);
                Assert.AreEqual(ps1, obtainedProducts.Products[0]);
                Assert.AreEqual(ps2, obtainedProducts.Products[1]);
            }
        }

        [TestMethod]
        public void FindProducts_TestWithKeywordsAndCategory()
        {
            using (var scope = new TransactionScope())
            {
                long firstProduct = 5;
                long size = 1;

                Product p1 = productDao.Find(firstProduct);
                ProductSummary ps1 = new ProductSummary();
                ps1.Id = p1.productId;
                ps1.Name = p1.name;
                ps1.Price = p1.price;
                ps1.Stock = p1.stock;
                ps1.CreateDate = p1.createDate;
                ps1.Category = p1.Category.categoryName;

                var obtainedProducts = productService.FindProducts("rEsPlaNdoR", 2, 0, 2);
                Assert.AreEqual(ps1, obtainedProducts.Products[0]);
                Assert.AreEqual(size, obtainedProducts.Products.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindProduct_NotExistId()
        {
            using (var scope = new TransactionScope())
            {
                long productId = -1;
                var obtainedProducts = productService.FindProduct(productId);
            }
        }

        [TestMethod]
        public void FindProduct_ExistId()
        {
            using (var scope = new TransactionScope())
            {
                long productId = 1;
                Product p1 = productDao.Find(1);
                var obtainedProduct = productService.FindProduct(productId);
                Assert.AreEqual(p1, obtainedProduct);
            }
        }

        [TestMethod]
        public void FindAllCategories()
        {
            using (var scope = new TransactionScope())
            {
                Category c1 = categoryDao.Find(1);
                Category c2 = categoryDao.Find(2);
                long size = 2;

                var obtainedCategories = productService.FindAllCategories();
                Assert.AreEqual(c1, obtainedCategories[0]);
                Assert.AreEqual(c2, obtainedCategories[1]);
                Assert.AreEqual(size, obtainedCategories.Count);
            }
        }

        [TestMethod]
        public void FindProductByTagTest()
        {
            using (var scope = new TransactionScope())
            {                
                //Register user
                var login = "Test";
                var userDetails = new UserDetails("Test", "Test", "test@test.com", "Estocolmo", "es", "ES", true);
                var id = userService.RegisterUser(login, "Test", userDetails);

                Product p1 = new Product();
                p1 = productDao.Find(1);

                Product p2 = new Product();
                p2 = productDao.Find(2);

                Product p3 = new Product();
                p3 = productDao.Find(3);

                var new_tag = new List<String> { "novedad", "oferta", "terror" };
                var new_tag2 = new List<String> { "novedad", "Tag1", "Tag2" };

                long commentId = commentService.AddComment(id, p1.productId, "Añadido comentario test", new_tag);
                long commentId2 = commentService.AddComment(id, p2.productId, "Añadido comentario test 2", new_tag2);
                long commentId3 = commentService.AddComment(id, p3.productId, "Añadido comentario test 3", new_tag2);

                var productList = productService.FindProductsByTags("novedad", 0, 3);
                Assert.AreEqual(3, productList.Products.Count);

                var productList2 = productService.FindProductsByTags("Tag1", 0, 3);
                Assert.AreEqual(2, productList2.Products.Count);
            }
        }



        #endregion Conjunto de pruebas del servicio de producto
    }
}
