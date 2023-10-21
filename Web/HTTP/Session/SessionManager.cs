using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Util;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;

namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session
{
    public class SessionManager 
    {
        public static readonly String LOCALE_SESSION_ATTRIBUTE = "locale";

        public static readonly String USER_SESSION_ATTRIBUTE =
               "userSession";
        public static readonly String SHOPPING_CART_ATTRIBUTE =
               "shoppingCart";

        private static IUserService userService;

        private static IShoppingService shoppingService;

        public IUserService UserService
        {
            set { userService = value; }
        }

        public IShoppingService ShoppingService
        {
            set { shoppingService = value; }
        }

        static SessionManager()
        {
            IIoCManager iocManager =
                (IIoCManager)HttpContext.Current.Application["managerIoC"];

            userService = iocManager.Resolve<IUserService>();
            shoppingService = iocManager.Resolve<IShoppingService>();
        }

        /// <summary>
        /// Changes the user's password
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="oldClearPassword">The old password in clear text</param>
        /// <param name="newClearPassword">The new password in clear text</param>
        /// <exception cref="IncorrectPasswordException"/>
        public static void ChangePassword(HttpContext context,
               String oldClearPassword, String newClearPassword)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            userService.ChangePassword(userSession.UserId,
                oldClearPassword, newClearPassword);

            /* Remove cookies. */
            CookiesManager.RemoveCookies(context);
        }

        /// <summary>
        /// Find all the credit cards of a user.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        public static List<CreditCard> FindCreditCards(HttpContext context)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];
            /* Register credit card. */
            List<CreditCard> cards = userService.FindCreditCards(userSession.UserId);

            return cards;
        }

        /// <summary>
        /// Registers the credit card.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="creditCard">The credit card details.</param>
        /// <exception cref="DuplicateInstanceException"/>
        public static void RegisterCreditCard(HttpContext context,
            CreditCard creditCard)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];
            /* Register credit card. */
            long cardId = userService.RegisterCreditCard(userSession.UserId, creditCard);

            FormsAuthentication.SetAuthCookie("", false);
        }


        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="loginName">Username</param>
        /// <param name="clearPassword">Password in clear text</param>
        /// <param name="userProfileDetails">The user profile details.</param>
        /// <exception cref="DuplicateInstanceException"/>
        public static void RegisterUser(HttpContext context,
            String loginName, String clearPassword,
            UserDetails userDetails)
        {
            /* Register user. */
            long usrId = userService.RegisterUser(loginName, clearPassword,
                userDetails);

            /* Insert necessary objects in the session. */
            UserSession userSession = new UserSession();
            userSession.UserId = usrId;
            userSession.FirstName = userDetails.FirstName;

            Locale locale = new Locale(userDetails.Language,
                userDetails.Country);

            UpdateSessionForAuthenticatedUser(context, userSession, locale);

            FormsAuthentication.SetAuthCookie(loginName, false);
        }

        /// <summary>
        /// Login method. Authenticates an user in the current context.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="loginName">Username</param>
        /// <param name="clearPassword">Password in clear text</param>
        /// <param name="rememberMyPassword">Remember password to the next logins</param>
        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        public static void Login(HttpContext context, String loginName,
           String clearPassword, Boolean rememberMyPassword)
        {
            /* Try to login, and if successful, update session with the necessary
             * objects for an authenticated user. */
            LoginResult loginResult = DoLogin(context, loginName,
                clearPassword, false, rememberMyPassword);

            /* Add cookies if requested. */
            if (rememberMyPassword)
            {
                CookiesManager.LeaveCookies(context, loginName,
                    loginResult.EncryptedPassword);
            }
        }

        /// <summary>
        /// Tries to log in with the corresponding method of
        /// <c>UserService</c>, and if successful, inserts in the
        /// session the necessary objects for an authenticated user.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="loginName">Username</param>
        /// <param name="password">User Password</param>
        /// <param name="passwordIsEncrypted">Password is either encrypted or
        /// in clear text</param>
        /// <param name="rememberMyPassword">Remember password to the next
        /// logins</param>
        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        private static LoginResult DoLogin(HttpContext context,
             String loginName, String password, Boolean passwordIsEncrypted,
             Boolean rememberMyPassword)
        {
            LoginResult loginResult =
                userService.Login(loginName, password,
                    passwordIsEncrypted);

            /* Insert necessary objects in the session. */

            UserSession userSession = new UserSession();
            userSession.UserId = loginResult.UserId;
            userSession.FirstName = loginResult.FirstName;
            userSession.PostalAddress = loginResult.PostalAddress;

            try
            {
                userSession.DefaultCreditCard = userService.GetDefaultCreditCard(loginResult.UserId);
            }
            catch (InstanceNotFoundException)
            {
                userSession.DefaultCreditCard = null;
            }

            Locale locale =
                new Locale(loginResult.Language, loginResult.Country);

            UpdateSessionForAuthenticatedUser(context, userSession, locale);

            return loginResult;
        }

        /// <summary>
        /// Updates the user profile details.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="userProfileDetails">The user profile details.</param>
        public static void UpdateUserProfileDetails(HttpContext context,
            UserDetails userProfileDetails)
        {
            /* Update user's profile details. */

            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            userService.UpdateUserDetails(userSession.UserId,
                userProfileDetails);

            /* Update user's session objects. */

            Locale locale = new Locale(userProfileDetails.Language,
                userProfileDetails.Country);

            userSession.FirstName = userProfileDetails.FirstName;
            userSession.PostalAddress = userProfileDetails.PostalAddress;
            try
            {
                userSession.DefaultCreditCard = userService.GetDefaultCreditCard(userSession.UserId);
            }
            catch (InstanceNotFoundException e) {
                userSession.DefaultCreditCard = null;
            }

            UpdateSessionForAuthenticatedUser(context, userSession, locale);
        }

        /// <summary>
        /// Finds the user profile with the id stored in the session.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static UserDetails FindUserProfileDetails(HttpContext context)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            UserDetails userProfileDetails =
                userService.FindUserDetails(userSession.UserId);

            return userProfileDetails;
        }
        public static void CreateShoppingCartEmpty(HttpContext context)
        {
            context.Session.Add(SHOPPING_CART_ATTRIBUTE, new ShoppingCart());
        }

        /// <summary>
        /// Gets the user info stored in the session.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static ShoppingCart GetShoppingCart(HttpContext context)
        {
            return (ShoppingCart)context.Session[SHOPPING_CART_ATTRIBUTE];
        }

        public static List<ShoppingCartItem> VisualizeShoppingCart(HttpContext context)
        {
            return shoppingService.VisualizeShoppingCart((ShoppingCart)context.Session[SHOPPING_CART_ATTRIBUTE]);
        }

        public static ShoppingCart RemoveItemFromShoppingCart(HttpContext context, long productId)
        {
            return shoppingService.RemoveItemFromShoppingCart((ShoppingCart)context.Session[SHOPPING_CART_ATTRIBUTE], productId);
        }


        public static ShoppingCart RemoveItemsFromShoppingCartNotEmpty(HttpContext context)
        {
            return shoppingService.RemoveItemsFromShoppingCartNotEmpty((ShoppingCart)context.Session[SHOPPING_CART_ATTRIBUTE]);
        }


        public static void AddProductFromShoppingCart(HttpContext context, long productId, int quantity)
        {
            shoppingService.AddProductFromShoppingCart((ShoppingCart)context.Session[SHOPPING_CART_ATTRIBUTE],
                productId, quantity);
        }

        public static ShoppingCart IsGiftProductShoppingCart(HttpContext context, long productId, bool isGift)
        {
            return shoppingService.IsGiftProductShoppingCart((ShoppingCart)context.Session[SHOPPING_CART_ATTRIBUTE], productId, isGift);
        }

        public static ShoppingCart UpdateQuantityShoppingCart(HttpContext context, long productId, int quantity)
        {
            return shoppingService.UpdateQuantityShoppingCart((ShoppingCart)context.Session[SHOPPING_CART_ATTRIBUTE], productId, quantity);
        }

        /// <summary>
        /// Updates the session values for an previously authenticated user
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="userSession">The user data stored in session.</param>
        /// <param name="locale">The locale info.</param>
        private static void UpdateSessionForAuthenticatedUser(
            HttpContext context, UserSession userSession, Locale locale)
        {
            /* Insert objects in session. */
            context.Session.Add(USER_SESSION_ATTRIBUTE, userSession);
            context.Session.Add(LOCALE_SESSION_ATTRIBUTE, locale);
        }

        /// <summary>
        /// Determine if a user is authenticated
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <returns>
        /// 	<c>true</c> if is user authenticated
        ///     <c>false</c> otherwise
        /// </returns>
        public static Boolean IsUserAuthenticated(HttpContext context)
        {
            if (context.Session == null)
                return false;

            return (context.Session[USER_SESSION_ATTRIBUTE] != null);
        }

        public static Locale GetLocale(HttpContext context)
        {
            Locale locale =
                (Locale)context.Session[LOCALE_SESSION_ATTRIBUTE];

            return locale;
        }

        /// <summary>
        /// Gets the user info stored in the session.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static UserSession GetUserSession(HttpContext context)
        {
            if (IsUserAuthenticated(context))
                return (UserSession)context.Session[USER_SESSION_ATTRIBUTE];
            else
                return null;
        }

        /// <sumary>
        /// Guarantees that the session will have the necessary objects if the
        /// user has been authenticated or had selected "remember my password"
        /// in the past.
        /// </sumary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        public static void TouchSession(HttpContext context)
        {
            /* Check if "UserSession" object is in the session. */
            UserSession userSession = null;

            if (context.Session != null)
            {
                userSession =
                    (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

                // If userSession object is in the session, nothing should be doing.
                if (userSession != null)
                {
                    return;
                }
            }

            /*
             * The user had not been authenticated or his/her session has
             * expired. We need to check if the user has selected "remember my
             * password" in the last login (login name and password will come
             * as cookies). If so, we reconstruct user's session objects.
             */
            UpdateSessionFromCookies(context);
        }

        /// <summary>
        /// Tries to login (inserting necessary objects in the session) by using
        /// cookies (if present).
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        private static void UpdateSessionFromCookies(HttpContext context)
        {
            HttpRequest request = context.Request;
            if (request.Cookies == null)
            {
                return;
            }

            /*
             * Check if the login name and the encrypted password come as
             * cookies.
             */
            String loginName = CookiesManager.GetLoginName(context);
            String encryptedPassword = CookiesManager.GetEncryptedPassword(context);

            if ((loginName == null) || (encryptedPassword == null))
            {
                return;
            }

            /* If loginName and encryptedPassword have valid values (the user selected "remember
             * my password" option) try to login, and if successful, update session with the
             * necessary objects for an authenticated user.
             */
            try
            {
                DoLogin(context, loginName, encryptedPassword, true, true);

                /* Authentication Ticket. */
                FormsAuthentication.SetAuthCookie(loginName, true);
            }
            catch (Exception)
            { // Incorrect loginName or encryptedPassword
                return;
            }
        }

        /// <summary>
        /// Destroys the session, and removes the cookies if the user had
        /// selected "remember my password".
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        public static void Logout(HttpContext context)
        {
            /* Remove cookies. */
            CookiesManager.RemoveCookies(context);

            /* Invalidate session. */
            context.Session.Abandon();

            /* Invalidate Authentication Ticket */
            FormsAuthentication.SignOut();
        }

    }
}