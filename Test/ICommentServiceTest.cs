using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    /// <summary>
    /// Unit test CommentService
    /// </summary>
    [TestClass]
    public class ICommentServiceTest
    {
        #region Variables test
        private const string loginName = "arceus";
        private const string password = "pikachu";
        private const string firstName = "Ash ";
        private const string lastName = "Ketchum";
        private const string email = "ash_ketchum@udc.es";
        private const string language = "es";
        private const string country = "ES";
        private const string postalAddress = "Pueblo Paleta, Kanto";
        private const bool admin = true;
        #endregion Variables test

        private static IKernel kernel;
        private static IUserService userService;
        private static IShoppingService shoppingService;
        private static ICommentService commentService;
        private static IUserDao userDao;
        private static IProductDao productDao;
        private static ICommentDao commentDao;
        private static ITagDao tagDao;

        private TransactionScope transaction;
        public TestContext TestContext { get; set; }

        private UserDetails CreateUserDetails()
        {
            return new UserDetails(firstName, lastName,
                email, postalAddress, country, language, admin);
        }

        #region Additional test attributes
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            userDao = kernel.Get<IUserDao>();
            productDao = kernel.Get<IProductDao>();
            commentDao = kernel.Get<ICommentDao>();
            tagDao = kernel.Get<ITagDao>();

            userService = kernel.Get<IUserService>();
            shoppingService = kernel.Get<IShoppingService>();
            commentService = kernel.Get<ICommentService>();
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateCommentException))]
        public void RegisterCommentDuplicate()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());
                Product p1 = new Product();
                p1 = productDao.Find(1);

                long commentId = commentService.AddComment(userId, p1.productId, "Añadido comentario 1", new List<string>());
                long commentId_2 = commentService.AddComment(userId, p1.productId, "Añadido comentario 2", new List<string>());
            }
        }
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void RegisterCommentNotExistentUserId()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());
                Product p1 = new Product();
                p1 = productDao.Find(1);

                long commentId = commentService.AddComment(-10, p1.productId, "Añadido comentario 1", new List<String>() { "test" });
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void RegisterCommentNotExistentProductId()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());

                long commentId = commentService.AddComment(userId, -100, "Añadido comentario 1", new List<String>() { "test" });
            }
        }

        [TestMethod]
        public void UpdateCommentOk()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());
                Product p1 = new Product();
                p1 = productDao.Find(1);

                long commentId = commentService.AddComment(userId, p1.productId, "Añadido comentario 1", new List<string>());
                long commentId_2 = commentService.UpdateComment(userId, commentId, "Actualizando comentario 1", new List<string>());
                var comment = commentDao.Find(commentId_2);

                //Check data
                Assert.AreEqual(commentId, comment.commentId);
                Assert.AreEqual(p1.productId, comment.productId);
                Assert.AreEqual("Actualizando comentario 1", comment.commentText);
                Assert.AreEqual(userId, comment.userId);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void UpdateCommentNotExistentUserId()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());
                Product p1 = new Product();
                p1 = productDao.Find(1);

                long commentId = commentService.AddComment(userId, p1.productId, "Añadido comentario 1", new List<String>() { "test" });
                long commentId_2 = commentService.UpdateComment(-100, commentId, "Actualizando comentario 1", new List<String>() { "test" });
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void UpdateCommentNotExistentCommentId()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());
                Product p1 = new Product();
                p1 = productDao.Find(1);

                long commentId = commentService.AddComment(userId, p1.productId, "Añadido comentario 1", new List<String>() { "test" });
                long commentId_2 = commentService.UpdateComment(userId, -100, "Actualizando comentario 1", new List<String>() { "test" });
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NotPermittedUpdateCommentException))]
        public void UpdateCommentNotPermitted()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());
                var userId_2 = userService.RegisterUser("elnano", password, CreateUserDetails());
                Product p1 = new Product();
                p1 = productDao.Find(1);

                long commentId = commentService.AddComment(userId, p1.productId, "Añadido comentario 1", new List<string>());
                long commentId_2 = commentService.UpdateComment(userId_2, commentId, "Actualizando comentario 1", new List<string>());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void RemoveCommentOk()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());
                Product p1 = new Product();
                p1 = productDao.Find(1);

                long commentId = commentService.AddComment(userId, p1.productId, "Añadido comentario 1", new List<String>() { "test" });
                long commentId_2 = commentService.RemoveComment(userId, commentId);

                //Check data
                Assert.AreEqual(commentId, commentId_2);

                var comment = commentDao.Find(commentId_2);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void RemoveCommentNotExistentUserId()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());
                Product p1 = new Product();
                p1 = productDao.Find(1);

                var tag = new Tag();
                tag.name = "test";

                long commentId = commentService.AddComment(userId, p1.productId, "Añadido comentario 1", new List<String>() { "test" });
                long commentId_2 = commentService.RemoveComment(-100, commentId);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void RemoveCommentNotExistentCommentId()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());
                Product p1 = new Product();
                p1 = productDao.Find(1);

                var tag = new Tag();
                tag.name = "test";

                long commentId = commentService.AddComment(userId, p1.productId, "Añadido comentario 1", new List<String>() { "test" });
                long commentId_2 = commentService.RemoveComment(userId, -100);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NotPermittedRemoveCommentException))]
        public void RemoveCommentNotPermitted()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());
                var userId_2 = userService.RegisterUser("piqué", password, CreateUserDetails());
                Product p1 = new Product();
                p1 = productDao.Find(1);

                var tag = new Tag();
                tag.name = "test";

                long commentId = commentService.AddComment(userId, p1.productId, "Añadido comentario 1", new List<String>());
                long commentId_2 = commentService.RemoveComment(userId_2, commentId);
            }
        }

        /// <summary>
        /// A test for find comments of product, when product exists and it have got commment's
        /// </summary>
        [TestMethod]
        public void FindCommentsOfProductOk()
        {
            using (var scope = new TransactionScope())
            {
                //registro 2 usuarios
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());
                var userId_2 = userService.RegisterUser("paco", password, CreateUserDetails());

                //obtengo producto
                Product p1 = new Product();
                p1 = productDao.Find(1);

                long commentId = commentService.AddComment(userId, p1.productId, "Añadido comentario 1", new List<string>());
                long commentId_2 = commentService.AddComment(userId_2, p1.productId, "Añadido comentario de Paco", new List<string>());

                Comment comment = commentDao.Find(commentId);
                Comment comment_2 = commentDao.Find(commentId_2);
                //obtengo los comentarios de p1
                var lista = commentService.FindComentsByProductId(p1.productId, 0, 2);

                //chequeo
                Assert.AreEqual("paco", lista.Comments[0].authorLogin);
                Assert.AreEqual(comment_2.createDate, lista.Comments[0].insertDate);
                Assert.AreEqual(comment_2.commentText, lista.Comments[0].commentText);

                Assert.AreEqual(loginName, lista.Comments[1].authorLogin);
                Assert.AreEqual(comment.createDate, lista.Comments[1].insertDate);
                Assert.AreEqual(comment.commentText, lista.Comments[1].commentText);

            }
        }

        /// <summary>
        /// A test for find comments of product, when product doesn't exists
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindCommentsOfProductWithNotExistProduct()
        {
            using (var scope = new TransactionScope())
            {
                var lista = commentService.FindComentsByProductId(-123, 0, 2);
            }
        }

        /// <summary>
        /// A test for find comments of product, when product haven't got comments
        /// </summary>
        [TestMethod]
        public void FindCommentsOfProductWithNotComments()
        {
            using (var scope = new TransactionScope())
            {
                //obtengo producto
                Product p1 = new Product();
                p1 = productDao.Find(1);

                var lista = commentService.FindComentsByProductId(p1.productId, 0, 1);
                Assert.AreEqual(0, lista.Comments.Count);
            }
        }

        [TestMethod]
        public void AddTagToComment()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());
                Product p1 = new Product();
                p1 = productDao.Find(1);

                var tagList = new List<string> { "novedad" };
                long commentId = commentService.AddComment(userId, p1.productId, "Añadido comentario 1", tagList);
                Comment comment = commentDao.Find(commentId);

                Assert.AreEqual(1, comment.Tags.Count);

                int i = 0;
                foreach (Tag t in comment.Tags)
                {
                    Assert.IsTrue(t.name.Equals(tagList[i]));
                    i++;
                }

            }
        }

        [TestMethod]
        public void AddTagListToComment()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());
                Product p1 = new Product();
                p1 = productDao.Find(1);

                var tagList = new List<string> { "novedad", "oferta", "terror" };

                long commentId = commentService.AddComment(userId, p1.productId, "Añadido comentario 1", tagList);
                Comment comment = commentDao.Find(commentId);

                int i = 0;
                foreach (Tag t in comment.Tags)
                {
                    Assert.IsTrue(t.name.Equals(tagList[i]));
                    i++;
                }
            }
        }

        [TestMethod]
        public void UpdateTagToComment()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());
                Product p1 = new Product();
                p1 = productDao.Find(1);

                var tagList = new List<string> { "novedad", "oferta", "terror" };
                var new_tagList = new List<string> { "oferta", "terror" };

                long commentId = commentService.AddComment(userId, p1.productId, "Añadido comentario 1", new_tagList);

                long commentId2 = commentService.UpdateComment(userId, commentId, "Actualizando comentario 1", tagList);
                Comment comment = commentDao.Find(commentId2);

                Assert.AreEqual(tagList.Count, comment.Tags.Count);

                int i = 0;
                foreach (Tag t in comment.Tags)
                {
                    Assert.IsTrue(t.name.Equals(tagList[i]));
                    i++;
                }
            }
        }

        [TestMethod]
        public void TestGetAllTags()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());
                Product p1 = new Product();
                p1 = productDao.Find(1);

                //Create a Tag
                var testTag = new Tag();
                var tag = "misterio";
                testTag.name = tag;
                tagDao.Create(testTag);

                //Create an comment without tag
                var new_tag = new List<String> { "novedad", "oferta", "terror" };
                long commentId = commentService.AddComment(userId, p1.productId, "Añadido comentario 1", new_tag);
                Comment comment = commentDao.Find(commentId);

                var listTag = new List<String>();
                listTag.Add("novedad");
                listTag.Add("oferta");
                listTag.Add("terror");
                listTag.Add(tag);
                var lista = commentService.GetAllTags();

                Assert.AreEqual(listTag.ToString(), lista.ToString());
            }
        }

        //DESCOMENTAR ESTE TEST CUANDO FUNCIONE LO DE OBTENER TAGS DE UN COMENTARIO
        /*[TestMethod]
        public void DeleteTagToComment()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());
                Product p1 = new Product();
                p1 = productDao.Find(1);

                var tagList = new List<string> { "novedad", "oferta", "terror" };
                long commentId = commentService.RegisterComment(userId, p1.productId, "Añadido comentario 1", tagList);
                Comment comment = commentDao.Find(commentId);

                commentService.DeleteTagFromComment(comment.commentId, "novedad");

                Assert.AreEqual(2, comment.Tags.Count);
            }
        }*/
        [TestMethod]
        public void TestGetTagsByUse()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());

                Product p1 = new Product();
                p1 = productDao.Find(1);

                var tagList = new List<string> { "novedad", "oferta", "terror" };
                long commentId = commentService.AddComment(userId, p1.productId, "Añadido comentario 1", tagList);

                Product p2 = new Product();
                p2 = productDao.Find(2);

                var tagList2 = new List<string> { "novedad", "terror", "misterio" };
                long commentId2 = commentService.AddComment(userId, p2.productId, "Añadido comentario 1", tagList2);

                Product p3 = new Product();
                p3 = productDao.Find(3);

                var tagList3 = new List<string> { "novedad", "misterio", "pesimo" };
                long commentId3 = commentService.AddComment(userId, p3.productId, "Añadido comentario 1", tagList2);

                var usage = commentService.GetTagsByUse2();
                var uses_tag = new List<String>();

                foreach (var tag in usage)
                {
                    Console.WriteLine(tag.name);
                    uses_tag.Add(tag.name);
                }

                var expected = new List<String> { "novedad", "misterio", "terror", "oferta", "pesimo" };

                Assert.AreEqual(expected.ToString(), uses_tag.ToString());
            }
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

    }
}
