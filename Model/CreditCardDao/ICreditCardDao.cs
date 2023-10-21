using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao
{
    public interface ICreditCardDao : IGenericDao<CreditCard, Int64> 
    {
        List<CreditCard> FindByUserId(long userId);
        /// <summary>
        /// Finds a CreditCard by number
        /// </summary>
        /// <param name="number">number</param>
        /// <returns>The CreditCard</returns>
        /// <exception cref="InstanceNotFoundException"/>
        CreditCard FindByNumber(long userId, long number);

        /// <summary>
        /// Finds a default Credit Card by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>The user's credit card default</returns>
        CreditCard FindDefaultCreditCardByUserId(long userId);
    }
}
