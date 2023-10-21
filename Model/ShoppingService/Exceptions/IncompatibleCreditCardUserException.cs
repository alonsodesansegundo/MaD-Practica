using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions
{
    public class IncompatibleCreditCardUserException : Exception
    {
        /// <summary>
        /// Stores the UserId of the exception
        /// </summary>
        /// <value>The user id.</value>
        public long UserId { get; private set; }

        /// <summary>
        /// Stores the CreditCardId of the exception
        /// </summary>
        /// <value>The credit card id.</value>
        public long CreditCardId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="IncompatibleCreditCardUserException"/> class.
        /// </summary>
        /// <param name="userId"><c>userId</c>The userId that causes the error.</param>
        /// <param name="creditCardId"><c>creditCardId</c>The creditCardId that causes the error.</param>
        public IncompatibleCreditCardUserException(long userId, long creditCardId)
            : base("The credit card " + creditCardId + " it's not from the user " + userId)
        {
            this.UserId = userId;
            this.CreditCardId = creditCardId;
        }
    }
}
