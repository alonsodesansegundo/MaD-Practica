using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions
{
    public class ShoppingCartEmptyException : Exception
    {
        /// <summary>
        /// Stores the UserId of the exception
        /// </summary>
        /// <value>The user id.</value>
        public long UserId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ShoppingCartEmptyException"/> class.
        /// </summary>
        /// <param name="userId"><c>userId</c>The userId that causes the error.</param>
        public ShoppingCartEmptyException(long userId)
            : base("The shopping cart of the user " + userId + " is empty, so we can't register the order")
        {
            this.UserId = userId;
        }

    }
}
