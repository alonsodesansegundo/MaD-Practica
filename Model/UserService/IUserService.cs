using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public interface IUserService 
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="loginName"> Name of the login. </param>
        /// <param name="clearPassword"> The clear password. </param>
        /// <param name="userDetails"> The user profile details. </param>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        long RegisterUser(String loginName, String clearPassword,
            UserDetails userDetails);

        /// <summary>
        /// Get the default credit card of user id.
        /// </summary>
        /// <param name="userId"> The id of the user. </param>
        /// <returns> The default credit card </returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        CreditCard GetDefaultCreditCard(long userId);

        /// <summary>
        /// Registers a new credit card.
        /// </summary>
        /// <param name="userId"> The user id. </param>
        /// <param name="userCreditCard"> The user credit card. </param>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        long RegisterCreditCard(long userId, CreditCard creditCard);

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userProfileId"> The user profile id. </param>
        /// <param name="oldClearPassword"> The old clear password. </param>
        /// <param name="newClearPassword"> The new clear password. </param>
        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        void ChangePassword(long userProfileId, String oldClearPassword,
            String newClearPassword);

        /// <summary>
        /// Finds the user profile details.
        /// </summary>
        /// <param name="userProfileId"> The user profile id. </param>
        /// <returns> The user profile details </returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        UserDetails FindUserDetails(long userId);

        /// <summary>
        /// List credit card by id.
        /// </summary>
        /// <param name="creditCardId"> The user id. </param>
        [Transactional]
        CreditCard FindUserCreditCard(long creditCardId);

        /// <summary>
        /// Updates the user profile details.
        /// </summary>
        /// <param name="userId"> The user profile id. </param>
        /// <param name="userDetails"> The user profile details. </param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void UpdateUserDetails(long userId, UserDetails userDetails);

        /// <summary>
        /// Updates the user profile details.
        /// </summary>
        /// <param name="creditCardId"> The credit card id. </param>
        /// <param name="userCreditCard"> The user credit card. </param>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="NotDefaultCreditCardException"/>
        [Transactional]
        void UpdateUserCreditCard(long creditCardId, CreditCard userCreditCard);

        /// <summary>
        /// List all credit cards of a user.
        /// </summary>
        /// <param name="userId"> The user id. </param>
        [Transactional]
        List<CreditCard> FindCreditCards(long userId);

        /// <summary>
        /// Remove a credit card.
        /// </summary>
        /// <param name="cardId"> The card id. </param>
        [Transactional]
        void RemoveCard(long cardId);


        /// <summary>
        /// Logins the specified login name.
        /// </summary>
        /// <param name="loginName"> Name of the login. </param>
        /// <param name="password"> The password. </param>
        /// <param name="passwordIsEncrypted"> if set to <c> true </c> [password is encrypted]. </param>
        /// <returns> LoginResult </returns>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        [Transactional]
        LoginResult Login(String loginName, String password,
            Boolean passwordIsEncrypted);

    }
}
