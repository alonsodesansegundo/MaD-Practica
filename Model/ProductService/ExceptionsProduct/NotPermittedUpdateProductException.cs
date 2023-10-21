using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService.ExceptionsProduct
{
    /// <summary>
    /// Public <c>ModelException</c> which captures when a not admin user try to update a product
    /// </summary>
    [Serializable]
    public class NotPermittedUpdateProductException : Exception
    {
        /// <summary>
        /// Stores the user id of the exception
        /// </summary>
        /// <value>The id of the user.</value>
        public long UserId { get; private set; }

        /// <summary>
        /// Stores the product id of the exception
        /// </summary>
        /// <value>The id of the product.</value>
        public long ProductId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="NotPermittedUpdateProductException"/> class.
        /// </summary>
        /// <param name="userId"><c>userId</c> that causes the error.</param>
        /// <param name="productId"><c>productId</c> that causes the error.</param>
        public NotPermittedUpdateProductException(long userId, long productId)
            : base("The not admin user: " + userId + " try to update the product " + productId)
        {
            this.UserId = userId;
            this.ProductId = productId;
        }
    }
}