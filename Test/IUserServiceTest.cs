using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    /// <summary>
    /// Unit test UserService
    /// </summary>
    [TestClass]
    public class IUserServiceTest
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
        private const long NON_EXISTENT_USER_ID = -1;

        private const string creditType = "bitcoin";
        private const long number = 666;
        private const int verificationCode = 123;
        private DateTime expirationDate = new DateTime(2025, 02, 25);
        private DateTime expirationDate2 = new DateTime(2027, 02, 25);
        private const bool defaultCard = true;

        #endregion Variables test

        private static IKernel kernel;
        private static IUserService userService;
        private static IUserDao userDao;
        private static ICreditCardDao creditCardDao;

        public IUserServiceTest()
        { }

        private TransactionScope transaction;

        public TestContext TestContext { get; set; }

        private UserDetails CreateUserDetails()
        {
            return new UserDetails(firstName, lastName,
                email, postalAddress, country, language, admin);
        }

        private CreditCard CreateUserCreditCard()
        {
            CreditCard creditCard = new CreditCard();
            creditCard.creditType = creditType;
            creditCard.number = number;
            creditCard.verificationCode = verificationCode;
            creditCard.expirationDate = expirationDate;
            creditCard.defaultCard = defaultCard;
            return creditCard;
        }

        /// <summary>
        /// Tests the register user.
        /// </summary>
        [TestMethod]
        public void RegisterUserTest()
        {
            using (var scope = new TransactionScope())
            {

                var createUserDetails = CreateUserDetails();
                //Register an user
                var userId = userService.RegisterUser(loginName, password, createUserDetails);

                //Find user
                var user = userDao.Find(userId);

                //Check data
                Assert.AreEqual(userId, user.userId);
                Assert.AreEqual(loginName, user.loginName);
                Assert.AreEqual(PasswordEncrypter.Crypt(password), user.password);
                Assert.AreEqual(firstName, user.firstName);
                Assert.AreEqual(lastName, user.lastName);
                Assert.AreEqual(postalAddress, user.postalAddress);
                Assert.AreEqual(email, user.email);
                Assert.AreEqual(language, user.language);
                Assert.AreEqual(country, user.country);
            }
        }

        /// <summary>
        /// A test for registering a user that already exists in the database
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void RegisterDuplicatedUserTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                userService.RegisterUser(loginName, password, CreateUserDetails());

                // Register the same user
                userService.RegisterUser(loginName, password, CreateUserDetails());
            }
        }

        /// <summary>
        /// A test for FindUserDetails
        /// </summary>
        [TestMethod]
        public void FindUserProfileDetailsTest()
        {
            using (var scope = new TransactionScope())
            {
                var expected = CreateUserDetails();
                var userId = userService.RegisterUser(loginName, password, expected);
                var obtained = userService.FindUserDetails(userId);

                // Check data
                Assert.AreEqual(expected, obtained);
            }
        }

        /// <summary>
        /// A test for FindUserDetails when the user does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindUserDetailsForNonExistingUserTest()
        {
            using (var scope = new TransactionScope())
            {
                userService.FindUserDetails(NON_EXISTENT_USER_ID);
            }
        }

        /// <summary>
        /// A test for UpdateUserDetails
        /// </summary>
        [TestMethod]
        public void UpdateUserDetailsTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user and update profile details
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());

                var expected = new UserDetails(firstName + "X", lastName + "X",
                email + "X", postalAddress + "X", "XX", "XX", true);

                userService.UpdateUserDetails(userId, expected);

                var obtained = userService.FindUserDetails(userId);

                // Check changes
                Assert.AreEqual(expected, obtained);
            }
        }

        /// <summary>
        /// A test for UpdateUserDetails when the user does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void UpdateUserDetailsForNonExistingUserTest()
        {
            using (var scope = new TransactionScope())
            {
                userService.UpdateUserDetails(NON_EXISTENT_USER_ID, CreateUserDetails());
            }
        }

        /// <summary>
        /// A test for ChangePassword
        /// </summary>
        [TestMethod]
        public void ChangePasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());

                // Change password
                var newClearPassword = password + "X";
                userService.ChangePassword(userId, password, newClearPassword);

                // Try to login with the new password. If the login is correct, then the password
                // was successfully changed.
                userService.Login(loginName, newClearPassword, false);
            }
        }

        /// <summary>
        /// A test for ChangePassword entering a wrong old password
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void ChangePasswordWithIncorrectPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());

                // Change password
                var newClearPassword = password + "X";
                userService.ChangePassword(userId, password + "Y", newClearPassword);
            }
        }

        /// <summary>
        /// A test for ChangePassword when the user does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ChangePasswordForNonExistingUserTest()
        {
            using (var scope = new TransactionScope())
            {
                userService.ChangePassword(NON_EXISTENT_USER_ID, password, password + "X");
            }
        }

        /// <summary>
        /// Tests the register credit card.
        /// </summary>
        [TestMethod]
        public void RegisterCreditCardTest()
        {
            using (var scope = new TransactionScope())
            {
                //Register an user
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());

                //Register a credit card
                var creditCardId = userService.RegisterCreditCard(userId, CreateUserCreditCard());

                //Find creditCard
                var creditCard = creditCardDao.Find(creditCardId);

                //Check data
                Assert.AreEqual(userId, creditCard.userId);
                Assert.AreEqual(creditType, creditCard.creditType);
                Assert.AreEqual(number, creditCard.number);
                Assert.AreEqual(verificationCode, creditCard.verificationCode);
                Assert.AreEqual(expirationDate, creditCard.expirationDate);
                Assert.AreEqual(defaultCard, creditCard.defaultCard);
            }
        }

        /// <summary>
        /// A test for registering a credit card that already exists in the database
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void RegisterDuplicatedCreditCardTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());

                //Register a credit card
                userService.RegisterCreditCard(userId, CreateUserCreditCard());

                //Register the same credit card
                userService.RegisterCreditCard(userId, CreateUserCreditCard());
            }
        }

        /// <summary>
        /// A test for UpdateUserCreditCard
        /// </summary>
        [TestMethod]
        public void UpdateUserCreditCardTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user and update user credit card
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());

                var creditCardId = userService.RegisterCreditCard(userId, CreateUserCreditCard());

                var expected = new CreditCard();
                expected.creditCardId = creditCardId;
                expected.userId = userId;
                expected.creditType = creditType + "X";
                expected.number = number + 1;
                expected.verificationCode = verificationCode + 1;
                expected.expirationDate = expirationDate2;
                expected.defaultCard = defaultCard;

                userService.UpdateUserCreditCard(creditCardId, expected);

                var obtained = userService.FindUserCreditCard(creditCardId);

                // Check changes
                Assert.AreEqual(expected, obtained);
            }
        }

        /// <summary>
        /// A test for UpdateUserCreditCard when the credit card does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void UpdateUserCreditCardForNonExistingUserTest()
        {
            using (var scope = new TransactionScope())
            {
                userService.UpdateUserCreditCard(NON_EXISTENT_USER_ID, CreateUserCreditCard());
            }
        }

        /* FUNC-2. Login */

        ///// <summary>
        /////A test for Login with clear password
        /////</summary>
        [TestMethod]
        public void LoginClearPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());

                var expected = new LoginResult(userId, firstName, lastName,
                    PasswordEncrypter.Crypt(password), language, country, postalAddress);

                // Login with clear password
                var actual = userService.Login(loginName, password, false);

                // Check data
                Assert.AreEqual(expected, actual);
            }
        }

        ///// <summary>
        /////A test for Login with encrypted password
        /////</summary>
        [TestMethod]
        public void LoginEncryptedPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());

                var expected = new LoginResult(userId, firstName, lastName,
                    PasswordEncrypter.Crypt(password), language, country, postalAddress);

                // Login with encrypted password
                var obtained = userService.Login(loginName, PasswordEncrypter.Crypt(password), true);

                // Check data
                Assert.AreEqual(expected, obtained);
            }
        }

        ///// <summary>
        /////A test for Login with incorrect password
        /////</summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void LoginIncorrectPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.RegisterUser(loginName, password, CreateUserDetails());

                // Login with incorrect (clear) password
                var actual = userService.Login(loginName, password + "X", false);
            }
        }

        ///// <summary>
        /////A test for Login with a non-existing user
        /////</summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void LoginNonExistingUserTest()
        {
            using (var scope = new TransactionScope())
            {
                // Login for a user that has not been registered
                var actual = userService.Login("asdfghj", password, false);
            }
        }

        #region Additional test attributes
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            userDao = kernel.Get<IUserDao>();
            userService = kernel.Get<IUserService>();
            creditCardDao = kernel.Get<ICreditCardDao>();

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