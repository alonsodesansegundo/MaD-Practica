using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao
{
    public class CreditCardDaoEntityFramework : GenericDaoEntityFramework<CreditCard, Int64>, ICreditCardDao
    {
        #region Public Constructors
        /// <summary>
        /// Public Constructor
        /// </summary>
        public CreditCardDaoEntityFramework()
        {
        }
        #endregion Public Constructors

        #region ICreditCardDao Members. Specific Operations

        /// <summary>
        /// Finds a Credit Cards by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>The user's credit card list</returns>
        public List<CreditCard> FindByUserId(long userId)
        {
            List<CreditCard> creditCards = null;
            DbSet<CreditCard> creditCardsDb = Context.Set<CreditCard>();

            var result = (from cc in creditCardsDb where cc.userId == userId select cc);

            creditCards = result.ToList<CreditCard>();

            return creditCards;
        }

        /// <summary>
        /// Finds a default Credit Card by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>The user's credit card default</returns>
        public CreditCard FindDefaultCreditCardByUserId(long userId)
        {
            CreditCard creditCard = null;
            DbSet<CreditCard> creditCardsDb = Context.Set<CreditCard>();

            var result = (from cc in creditCardsDb where cc.userId == userId && cc.defaultCard == true select cc);

            creditCard = result.FirstOrDefault();

            return creditCard;
        }

        public CreditCard FindByNumber(long userId, long number)
        {
            CreditCard creditCard = null;

            #region Option 1: Using Linq.

            DbSet<CreditCard> creditCardDb = Context.Set<CreditCard>();

            var result = (from u in creditCardDb where u.userId == userId && u.number == number select u);
            creditCard = result.FirstOrDefault();

            #endregion Option 1: Using Linq.

            if (creditCard == null)
                throw new InstanceNotFoundException(number,
                    typeof(CreditCard).FullName);

            return creditCard;

        }
        #endregion ICreditCardDao Members

    }
}