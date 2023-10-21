using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.BookDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    /// <summary>
    /// Descripción resumida de IShoppingServiceTest
    /// </summary>
    [TestClass]
    public class IShoppingServiceTest
    {
        #region Variables test
        private const string loginName = "pokemon";
        private const string password = "pikachu";
        private const string firstName = "Ash ";
        private const string lastName = "Ketchum";
        private const string email = "ash_ketchum@udc.es";
        private const string language = "es";
        private const string country = "ES";
        private const string postalAddress = "Pueblo Paleta, Kanto";
        private const bool admin = true;
        private const long NON_EXISTENT_ID = (long)-1;
        #endregion Variables test

        #region Variables conf test
        private static IKernel kernel;
        private static IUserService userService;
        private static IShoppingService shoppingService;
        private static IProductDao productDao;
        private static IOrderDao orderDao;
        private static IOrderLineDao orderLineDao;

        #endregion Variables conf test

        public IShoppingServiceTest()
        { }

        private TransactionScope transaction;

        public TestContext TestContext { get; set; }

        #region Additional test attributes
        //Use ClassInitialize to run code before running the first test in the class

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            userService = kernel.Get<IUserService>();
            shoppingService = kernel.Get<IShoppingService>();
            productDao = kernel.Get<IProductDao>();
            orderDao = kernel.Get<IOrderDao>();
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


        private ShoppingCart createShoppingCart()
        {

            List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();
            Product p1 = new Product();
            p1 = productDao.Find(1);

            Product p2 = new Product();
            p2 = productDao.Find(2);

            Product p3 = new Product();
            p3 = productDao.Find(3);

            ShoppingCartItem aux1 = new ShoppingCartItem();
            aux1.isGiftProduct = false;
            aux1.quantity = 1;
            aux1.product = p1;

            ShoppingCartItem aux2 = new ShoppingCartItem();
            aux2.isGiftProduct = false;
            aux2.quantity = 1;
            aux2.product = p2;

            ShoppingCartItem aux3 = new ShoppingCartItem();
            aux3.isGiftProduct = false;
            aux3.quantity = 1;
            aux3.product = p3;

            shoppingCartItems.Add(aux1);
            shoppingCartItems.Add(aux2);
            shoppingCartItems.Add(aux3);
            return new ShoppingCart(shoppingCartItems);
        }
        private UserDetails createUserDetails()
        {
            return new UserDetails(firstName, lastName, email, postalAddress, country, language);
        }
        private long registerUser(string login, string password, UserDetails userDetails)
        {
            return userService.RegisterUser(login, password, userDetails);
        }

        private long registerCreditCard(long userId)
        {
            CreditCard creditCard = new CreditCard();
            creditCard.creditType = "credito";
            creditCard.number = 123456789;
            creditCard.verificationCode = 000;
            creditCard.expirationDate = DateTime.Now.AddDays(10);
            creditCard.defaultCard = true;

            return userService.RegisterCreditCard(userId, creditCard);
        }

        #region Conjunto de pruebas del servicio de shopping
        [TestMethod]
        public void RegisterOrderCorrect()
        {
            using (var scope = new TransactionScope())
            {
                ShoppingCart shoppingCart = createShoppingCart();

                UserDetails userDetails = createUserDetails();
                long userId = registerUser(loginName, password, userDetails);

                long creditCardId = registerCreditCard(userId);

                long orderId = shoppingService.RegisterOrder(shoppingCart, userId, creditCardId,
                    postalAddress, "pedido 1");

                var orderFind = orderDao.Find(orderId);

                Assert.AreEqual(orderId, orderFind.orderId);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ShoppingCartEmptyException))]
        public void RegisterOrderWithShoppingCartEmptyException()
        {
            using (var scope = new TransactionScope())
            {
                ShoppingCart shoppingCart = new ShoppingCart();

                UserDetails userDetails = createUserDetails();
                long userId = registerUser("pepe", password, userDetails);

                long creditCardId = registerCreditCard(userId);

                long orderId = shoppingService.RegisterOrder(shoppingCart, userId, creditCardId,
                    postalAddress, "pedido exception shopping cart empty");
            }
        }

        [ExpectedException(typeof(IncompatibleCreditCardUserException))]
        [TestMethod]
        public void RegisterOrderWithIncompatibleCreditCardUserException()
        {
            using (var scope = new TransactionScope())
            {
                ShoppingCart shoppingCart = createShoppingCart();

                UserDetails userDetails = createUserDetails();
                long userId = registerUser("manolito14", password, userDetails);

                long userId_2 = registerUser("elnano33", password, userDetails);

                long creditCardId = registerCreditCard(userId_2);

                long orderId = shoppingService.RegisterOrder(shoppingCart, userId, creditCardId,
                    postalAddress, "pedido 1");
            }
        }

        [ExpectedException(typeof(InstanceNotFoundException))]
        [TestMethod]
        public void RegisterOrderWithNotExistUser()
        {
            using (var scope = new TransactionScope())
            {
                ShoppingCart shoppingCart = createShoppingCart();

                long orderId = shoppingService.RegisterOrder(shoppingCart, -1, 1,
                    postalAddress, "pedido 1");
            }
        }

        [ExpectedException(typeof(InstanceNotFoundException))]
        [TestMethod]
        public void RegisterOrderWithNotExistCreditCard()
        {
            using (var scope = new TransactionScope())
            {
                ShoppingCart shoppingCart = createShoppingCart();

                UserDetails userDetails = createUserDetails();
                long userId = registerUser("amaia", password, userDetails);

                long orderId = shoppingService.RegisterOrder(shoppingCart, userId, -1,
                    postalAddress, "pedido 1");
            }
        }

        //TEST DE LA FUNC-5 --> Añadir producto al carrito
        [TestMethod]
        public void AddProductFromShoppingCart()
        {
            using (var scope = new TransactionScope())
            {
                UserDetails user = new UserDetails(firstName, lastName, email, postalAddress, country, language);
                long userId = userService.RegisterUser(loginName, password, user);

                List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();
                ShoppingCart shoppingCart = new ShoppingCart(shoppingCartItems);

                Product p1 = new Product();
                p1 = productDao.Find(1);

                Product p2 = new Product();
                p2 = productDao.Find(2);

                shoppingService.AddProductFromShoppingCart(shoppingCart, p1.productId, 1);
                shoppingService.AddProductFromShoppingCart(shoppingCart, p2.productId, 1);

                Product p3 = new Product();
                p3 = productDao.Find(3);

                ShoppingCart newShoppingCart = shoppingService.AddProductFromShoppingCart(shoppingCart, p3.productId, 1);

                Assert.AreEqual(3, newShoppingCart.shoppingCartItems.Count);
                Assert.AreEqual(p3, newShoppingCart.shoppingCartItems[2].product);
                Assert.AreEqual(1, newShoppingCart.shoppingCartItems[2].quantity);
                //Por defecto tiene que salir a false el isGiftProduct
                Assert.AreEqual(false, newShoppingCart.shoppingCartItems[2].isGiftProduct);
                Assert.AreEqual((decimal)21.88, newShoppingCart.TotalPrice);

            }
        }

        [TestMethod]
        [ExpectedException(typeof(NotStockEnough))]
        public void AddProductNotStockEnoughFromShoppingCart()
        {
            using (var scope = new TransactionScope())
            {
                UserDetails user = new UserDetails(firstName, lastName, email, postalAddress, country, language);
                long userId = userService.RegisterUser(loginName, password, user);

                List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();
                ShoppingCart shoppingCart = new ShoppingCart(shoppingCartItems);


                Product p1 = productDao.Find(1);
                shoppingService.AddProductFromShoppingCart(shoppingCart, p1.productId, 1);


                Product p2 = productDao.Find(4);
                shoppingService.AddProductFromShoppingCart(shoppingCart, p2.productId, 1);
                //Modificamos el stock para poder comprobar la excepción, de que no podemos actualizar
                //la cantidad del producto en carrito si no hay stock
                p2.stock = 0;

                ShoppingCart newShoppingCart =
                    shoppingService.AddProductFromShoppingCart(shoppingCart, p2.productId, 1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void AddProductFromShoppingCartNotExistProduct()
        {
            using (var scope = new TransactionScope())
            {
                UserDetails user = new UserDetails(firstName, lastName, email, postalAddress, country, language);
                long userId = userService.RegisterUser(loginName, password, user);

                List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();
                ShoppingCart shoppingCart = new ShoppingCart(shoppingCartItems);

                Product p1 = productDao.Find(1);

                shoppingService.AddProductFromShoppingCart(shoppingCart, p1.productId, 1);

                ShoppingCart newShoppingCart = shoppingService.AddProductFromShoppingCart(shoppingCart, NON_EXISTENT_ID, 1);
            }
        }

        [TestMethod]
        public void VisualizeShoppingCart()
        {
            using (var scope = new TransactionScope())
            {
                UserDetails user = new UserDetails(firstName, lastName, email, postalAddress, country, language, admin);
                long userId = userService.RegisterUser(loginName, password, user);

                List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();
                ShoppingCart shoppingCart = new ShoppingCart(shoppingCartItems);

                Product p1 = productDao.Find(1);
                Product p2 = productDao.Find(4);

                shoppingService.AddProductFromShoppingCart(shoppingCart, p1.productId, 1);
                shoppingService.AddProductFromShoppingCart(shoppingCart, p2.productId, 1);

                List<ShoppingCartItem> items = shoppingService.VisualizeShoppingCart(shoppingCart);

                Assert.AreEqual(2, items.Count);

                Assert.AreEqual(p1, items[0].product);
                Assert.AreEqual(1, items[0].quantity);
                Assert.AreEqual(false, items[0].isGiftProduct);

                Assert.AreEqual(p2, items[1].product);
                Assert.AreEqual(1, items[1].quantity);
                Assert.AreEqual(false, items[1].isGiftProduct);
            }
        }

        [TestMethod]
        public void NotEmptyShoppingCart()
        {
            using (var scope = new TransactionScope())
            {
                UserDetails user = new UserDetails(firstName, lastName, email, postalAddress, country, language, admin);
                long userId = userService.RegisterUser(loginName, password, user);

                List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();
                ShoppingCart shoppingCart = new ShoppingCart(shoppingCartItems);

                Product p1 = productDao.Find(1);
                Product p2 = productDao.Find(4);

                shoppingService.AddProductFromShoppingCart(shoppingCart, p1.productId, 1);
                shoppingService.AddProductFromShoppingCart(shoppingCart, p2.productId, 1);

                List<ShoppingCartItem> items = shoppingService.VisualizeShoppingCart(shoppingCart);

                Assert.AreEqual(2, items.Count);

                Assert.AreEqual(p1, items[0].product);
                Assert.AreEqual(1, items[0].quantity);
                Assert.AreEqual(false, items[0].isGiftProduct);

                Assert.AreEqual(p2, items[1].product);
                Assert.AreEqual(1, items[1].quantity);
                Assert.AreEqual(false, items[1].isGiftProduct);

                shoppingService.RemoveItemsFromShoppingCartNotEmpty(shoppingCart);
                Assert.AreEqual(0, items.Count);
            }
        }

        //TEST DE LA FUNC-5 --> Eliminar producto del carrito
        [TestMethod]
        public void RemoveItemFromShoppingCart()
        {
            using (var scope = new TransactionScope())
            {
                UserDetails user = new UserDetails(firstName, lastName, email, postalAddress, country, language);
                long userId = userService.RegisterUser(loginName, password, user);

                List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();
                ShoppingCart shoppingCart = new ShoppingCart(shoppingCartItems);

                Product p1 = productDao.Find(1);
                shoppingService.AddProductFromShoppingCart(shoppingCart, p1.productId, 1);

                Product p2 = productDao.Find(2);
                shoppingService.AddProductFromShoppingCart(shoppingCart, p2.productId, 1);

                ShoppingCart newShoppingCart = shoppingService.RemoveItemFromShoppingCart(shoppingCart, p2.productId);

                Assert.AreEqual(1, newShoppingCart.shoppingCartItems.Count);

                Assert.AreEqual(p1, newShoppingCart.shoppingCartItems[0].product);
                Assert.AreEqual(1, newShoppingCart.shoppingCartItems[0].quantity);
                //Por defecto tiene que salir a false el isGiftProduct
                Assert.AreEqual(false, newShoppingCart.shoppingCartItems[0].isGiftProduct);
                Assert.AreEqual((decimal)3.99, newShoppingCart.TotalPrice);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void RemoveItemNotExistFromShoppingCart()
        {
            using (var scope = new TransactionScope())
            {
                UserDetails user = new UserDetails(firstName, lastName, email, postalAddress, country, language);
                long userId = userService.RegisterUser(loginName, password, user);

                List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();
                ShoppingCart shoppingCart = new ShoppingCart(shoppingCartItems);

                Product p1 = productDao.Find(1);
                shoppingService.AddProductFromShoppingCart(shoppingCart, p1.productId, 1);

                Product p2 = productDao.Find(2);
                shoppingService.AddProductFromShoppingCart(shoppingCart, p2.productId, 1);

                ShoppingCart newShoppingCart = shoppingService.RemoveItemFromShoppingCart(shoppingCart, NON_EXISTENT_ID);

            }
        }

        //TEST DE LA FUNC-5 --> Actualizar cantidad de un producto en el carrito
        [TestMethod]
        public void UpdateQuantityShoppingCart()
        {
            using (var scope = new TransactionScope())
            {
                UserDetails user = new UserDetails(firstName, lastName, email, postalAddress, country, language);
                long userId = userService.RegisterUser(loginName, password, user);

                List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();
                ShoppingCart shoppingCart = new ShoppingCart(shoppingCartItems);

                Product p1 = productDao.Find(1);
                shoppingService.AddProductFromShoppingCart(shoppingCart, p1.productId, 1);

                Product p2 = productDao.Find(4);
                shoppingService.AddProductFromShoppingCart(shoppingCart, p2.productId, 1);

                ShoppingCart newShoppingCart = shoppingService.UpdateQuantityShoppingCart(shoppingCart, p2.productId, 2);

                Assert.AreEqual(2, newShoppingCart.shoppingCartItems.Count);
                Assert.AreEqual(p2, newShoppingCart.shoppingCartItems[1].product);
                Assert.AreEqual(2, newShoppingCart.shoppingCartItems[1].quantity);
                //Por defecto tiene que salir a false el isGiftProduct
                Assert.AreEqual(false, newShoppingCart.shoppingCartItems[1].isGiftProduct);
                Assert.AreEqual((decimal)41.89, newShoppingCart.TotalPrice);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void UpdateQuantityShoppingCartNotExistProduct()
        {
            using (var scope = new TransactionScope())
            {
                UserDetails user = new UserDetails(firstName, lastName, email, postalAddress, country, language);
                long userId = userService.RegisterUser(loginName, password, user);

                List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();
                ShoppingCart shoppingCart = new ShoppingCart(shoppingCartItems);

                Product p1 = productDao.Find(1);
                shoppingService.AddProductFromShoppingCart(shoppingCart, p1.productId, 1);

                ShoppingCart newShoppingCart = shoppingService.UpdateQuantityShoppingCart(shoppingCart, NON_EXISTENT_ID, 2);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NotStockEnough))]
        public void UpdateQuantityShoppingCartNotStock()
        {
            using (var scope = new TransactionScope())
            {
                UserDetails user = new UserDetails(firstName, lastName, email, postalAddress, country, language);
                long userId = userService.RegisterUser(loginName, password, user);

                List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();
                ShoppingCart shoppingCart = new ShoppingCart(shoppingCartItems);

                Product p1 = productDao.Find(1);
                shoppingService.AddProductFromShoppingCart(shoppingCart, p1.productId, 1);
                //Modificamos el stock para poder comprobar la excepción, de que no podemos actualizar
                //la cantidad del producto en carrito si no hay stock
                p1.stock = 0;

                ShoppingCart newShoppingCart = shoppingService.UpdateQuantityShoppingCart(shoppingCart, p1.productId, 2);
            }
        }

        //TEST DE LA FUNC-5 --> Selecionar si queremos  un producto envuelto para regalo
        [TestMethod]
        public void IsGiftProductShoppingCart()
        {
            using (var scope = new TransactionScope())
            {
                UserDetails user = new UserDetails(firstName, lastName, email, postalAddress, country, language);
                long userId = userService.RegisterUser(loginName, password, user);

                List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();
                ShoppingCart shoppingCart = new ShoppingCart(shoppingCartItems);

                Product p1 = productDao.Find(1);
                shoppingService.AddProductFromShoppingCart(shoppingCart, p1.productId, 1);

                Product p2 = productDao.Find(4);
                shoppingService.AddProductFromShoppingCart(shoppingCart, p2.productId, 1);

                ShoppingCart newShoppingCart = shoppingService.IsGiftProductShoppingCart(shoppingCart, p1.productId, true);

                Assert.AreEqual(2, newShoppingCart.shoppingCartItems.Count);
                Assert.AreEqual(p1, newShoppingCart.shoppingCartItems[0].product);
                Assert.AreEqual(1, newShoppingCart.shoppingCartItems[0].quantity);
                Assert.AreEqual(true, newShoppingCart.shoppingCartItems[0].isGiftProduct);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void IsGiftProductShoppingCartNotExistProduct()
        {
            using (var scope = new TransactionScope())
            {
                UserDetails user = new UserDetails(firstName, lastName, email, postalAddress, country, language);
                long userId = userService.RegisterUser(loginName, password, user);

                List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();
                ShoppingCart shoppingCart = new ShoppingCart(shoppingCartItems);

                Product p1 = productDao.Find(1);
                shoppingService.AddProductFromShoppingCart(shoppingCart, p1.productId, 1);

                ShoppingCart newShoppingCart = shoppingService.IsGiftProductShoppingCart(shoppingCart, NON_EXISTENT_ID, true);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void VisualizeOrderWithNotExistUser()
        {
            using (var scope = new TransactionScope())
            {
                OrderBlock orderBlockObteined = shoppingService.FindOrdersByUserId(-1, 0, 5);
            }
        }

        [TestMethod]
        public void VisualizeOrder()
        {
            using (var scope = new TransactionScope())
            {
                // datos usuario
                UserDetails userDetails = createUserDetails();
                long userId = registerUser(loginName, password, userDetails);
                long creditCardId = registerCreditCard(userId);

                //pedido 1
                ShoppingCart shoppingCart = createShoppingCart();
                long orderId_1 = shoppingService.RegisterOrder(shoppingCart, userId, creditCardId,
                    postalAddress, "pedido 1");
                Order order_1 = orderDao.Find(orderId_1);
                List<OrderDetails> orders = new List<OrderDetails>();
                OrderDetails orderDetails_1 = new OrderDetails(orderId_1, order_1.orderDate,
                    order_1.orderName, order_1.totalPrice);
                orders.Add(orderDetails_1);

                //pedido 2
                List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();
                Product p1 = new Product();
                p1 = productDao.Find(1);
                ShoppingCartItem aux1 = new ShoppingCartItem();
                aux1.isGiftProduct = false;
                aux1.quantity = 1;
                aux1.product = p1;
                shoppingCartItems.Add(aux1);
                shoppingCart = new ShoppingCart(shoppingCartItems, aux1.productPriceActual * aux1.quantity);
                long orderId_2 = shoppingService.RegisterOrder(shoppingCart, userId, creditCardId,
                    postalAddress, "pedido 2");
                Order order_2 = orderDao.Find(orderId_2);
                OrderDetails orderDetails_2 = new OrderDetails(orderId_2, order_2.orderDate,
                    order_2.orderName, order_2.totalPrice);
                orders.Add(orderDetails_2);

                //comparación
                OrderBlock orderBlock = new OrderBlock(orders, false);
                OrderBlock orderBlockObteined = shoppingService.FindOrdersByUserId(userId, 0, 5);
                Assert.AreEqual(order_2.orderName, orderBlockObteined.Orders[0].name);
                Assert.AreEqual(order_2.orderDate, orderBlockObteined.Orders[0].creationDate);
                Assert.AreEqual(order_2.totalPrice, orderBlockObteined.Orders[0].totalPrice);

                Assert.AreEqual(order_1.orderName, orderBlockObteined.Orders[1].name);
                Assert.AreEqual(order_1.orderDate, orderBlockObteined.Orders[1].creationDate);
                Assert.AreEqual(order_1.totalPrice, orderBlockObteined.Orders[1].totalPrice);
            }
        }
        [TestMethod]
        public void VisualizeOrderLines()
        {
            using (var scope = new TransactionScope())
            {
                // datos usuario
                UserDetails userDetails = createUserDetails();
                long userId = registerUser(loginName, password, userDetails);
                long creditCardId = registerCreditCard(userId);

                //pedido 1
                ShoppingCart shoppingCart = createShoppingCart();
                long orderId_1 = shoppingService.RegisterOrder(shoppingCart, userId, creditCardId,
                    postalAddress, "pedido 1");
                Order order_1 = orderDao.Find(orderId_1);
                List<OrderDetails> orders = new List<OrderDetails>();
                OrderDetails orderDetails_1 = new OrderDetails(orderId_1, order_1.orderDate,
                    order_1.orderName, order_1.totalPrice);
                orders.Add(orderDetails_1);

                List<OrderLine> orderLines = shoppingService.FindOrderLinesByOrderId(orderId_1);
                Product p1 = new Product();
                p1 = productDao.Find(1);
                ShoppingCartItem aux1 = new ShoppingCartItem();
                aux1.isGiftProduct = false;
                aux1.quantity = 1;
                aux1.product = p1;

                Product p2 = new Product();
                p2 = productDao.Find(2);
                ShoppingCartItem aux2 = new ShoppingCartItem();
                aux2.isGiftProduct = false;
                aux2.quantity = 1;
                aux2.product = p2;

                Product p3 = new Product();
                p3 = productDao.Find(3);
                ShoppingCartItem aux3 = new ShoppingCartItem();
                aux3.isGiftProduct = false;
                aux3.quantity = 1;
                aux3.product = p3;

                Assert.AreEqual(aux1.product, orderLines[0].Product);
                Assert.AreEqual(aux1.quantity, orderLines[0].quantity);
                Assert.AreEqual(aux1.isGiftProduct, orderLines[0].isGift);

                Assert.AreEqual(aux2.product, orderLines[1].Product);
                Assert.AreEqual(aux2.quantity, orderLines[1].quantity);
                Assert.AreEqual(aux2.isGiftProduct, orderLines[1].isGift);

                Assert.AreEqual(aux3.product, orderLines[2].Product);
                Assert.AreEqual(aux3.quantity, orderLines[2].quantity);
                Assert.AreEqual(aux3.isGiftProduct, orderLines[2].isGift);
            }
        }
        #endregion Conjunto de pruebas del servicio de shopping
    }
}
