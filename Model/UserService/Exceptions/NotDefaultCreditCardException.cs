using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions
{
    class NotDefaultCreditCardException : Exception
    {
        /// <summary>
        /// Stores the UserId of the exception
        /// </summary>
        /// <value>The user id.</value>
        public long UserId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="NotDefaultCreditCardException"/> class.
        /// </summary>
        /// <param name="userId"><c>userId</c>The userId that causes the error.</param>
        public NotDefaultCreditCardException(long userId)
            : base("You must have at least one credit card by default. Caused by userId: " + userId)
        {
            this.UserId = userId;
        }
    }
}