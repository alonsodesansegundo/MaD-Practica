using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;
using Ninject;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public class UserService : IUserService
    {
        [Inject]
        public IUserDao UserDao { private get; set; }
        [Inject]
        public ICreditCardDao CreditCardDao { private get; set; }

        public long RegisterCreditCard(long userId, CreditCard creditCard)
        {
            try
            {
                CreditCardDao.FindByNumber(userId, creditCard.number);

                throw new DuplicateInstanceException(creditCard.number,
                    typeof(CreditCard).FullName);
            }
            catch (InstanceNotFoundException)
            {
                CreditCard newcreditCard = new CreditCard();
                List<CreditCard> creditCards = CreditCardDao.FindByUserId(userId);

                newcreditCard.userId = userId;
                newcreditCard.creditType = creditCard.creditType;
                newcreditCard.number = creditCard.number;
                newcreditCard.verificationCode = creditCard.verificationCode;
                newcreditCard.expirationDate = creditCard.expirationDate;
                //si es la primera tarjeta que añado, es la tarjeta por defecto
                if (creditCards.Count == 0)
                    newcreditCard.defaultCard = true;
                else
                    newcreditCard.defaultCard = false;

                CreditCardDao.Create(newcreditCard);

                return newcreditCard.creditCardId;
            }
        }

        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public long RegisterUser(string loginName, string clearPassword,
            UserDetails userDetails)
        {
            try
            {
                UserDao.FindByLoginName(loginName);

                throw new DuplicateInstanceException(loginName,
                    typeof(User).FullName);
            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                User user = new User();

                user.loginName = loginName;
                user.password = encryptedPassword;
                user.firstName = userDetails.FirstName;
                user.lastName = userDetails.LastName;
                user.email = userDetails.Email;
                user.postalAddress = userDetails.PostalAddress;
                user.country = userDetails.Country;
                user.language = userDetails.Language;
                user.admin = userDetails.Admin;

                UserDao.Create(user);

                return user.userId;
            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        public CreditCard GetDefaultCreditCard(long userId)
        {
            CreditCard creditCard = null;
            creditCard = CreditCardDao.FindDefaultCreditCardByUserId(userId);
            if (creditCard == null)
                throw new InstanceNotFoundException(userId,
                    typeof(CreditCard).FullName);
            return creditCard;
        }

        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void ChangePassword(long userProfileId, string oldClearPassword,
            string newClearPassword)
        {
            User user = UserDao.Find(userProfileId);
            String storedPassword = user.password;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldClearPassword,
                 storedPassword))
            {
                throw new IncorrectPasswordException(user.loginName);
            }

            user.password = PasswordEncrypter.Crypt(newClearPassword);

            UserDao.Update(user);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void UpdateUserDetails(long userId, UserDetails userDetails)
        {
            User user = UserDao.Find(userId);

            user.firstName = userDetails.FirstName;
            user.lastName = userDetails.LastName;
            user.email = userDetails.Email;
            user.postalAddress = userDetails.PostalAddress;
            user.country = userDetails.Country;
            user.language = userDetails.Language;
            //user.admin = userDetails.Admin;
            UserDao.Update(user);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public void UpdateUserCreditCard(long creditCardId, CreditCard userCreditCard)
        {
            CreditCard creditCard = CreditCardDao.Find(creditCardId);
            creditCard.creditType = userCreditCard.creditType;
            creditCard.number = userCreditCard.number;
            creditCard.verificationCode = userCreditCard.verificationCode;
            creditCard.expirationDate = userCreditCard.expirationDate;

            //si antes no era la tarjeta por defecto
            if (!userCreditCard.defaultCard)
            {
                List<CreditCard> creditCards = CreditCardDao.FindByUserId(creditCard.userId);
                foreach (CreditCard c in creditCards)
                {
                    c.defaultCard = false;
                    CreditCardDao.Update(c);
                }
                creditCard.defaultCard = true;
            }
            CreditCardDao.Update(creditCard);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public UserDetails FindUserDetails(long userId)
        {
            User user = UserDao.Find(userId);

            UserDetails userProfileDetails = new UserDetails(user.firstName, user.lastName,
                user.email, user.postalAddress, user.country, user.language, user.admin);

            return userProfileDetails;
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public CreditCard FindUserCreditCard(long creditCardId)
        {
            CreditCard creditCard = CreditCardDao.Find(creditCardId);

            return creditCard;
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public List<CreditCard> FindCreditCards(long userId)
        {
            List<CreditCard> creditCards = CreditCardDao.FindByUserId(userId);

            return creditCards;
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void RemoveCard(long cardId)
        {
            CreditCardDao.Remove(cardId);
        }


        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        [Transactional]
        public LoginResult Login(string loginName, string password, bool passwordIsEncrypted)
        {
            User user =
                UserDao.FindByLoginName(loginName);

            String storedPassword = user.password;

            if (passwordIsEncrypted)
            {
                if (!password.Equals(storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }
            else
            {
                if (!PasswordEncrypter.IsClearPasswordCorrect(password,
                        storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }

            return new LoginResult(user.userId, user.firstName, user.lastName,
                storedPassword, user.language, user.country, user.postalAddress);
        }

    }
}
