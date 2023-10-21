using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions
{
    public class NotStockEnough : Exception
    {

        /// <summary>
        /// Stores the ProductId of the exception
        /// </summary>
        /// <value>The product id.</value>
        public long ProductId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="NotStockEnough"/> class.
        /// </summary>
        /// <param name="productId"><c>userId</c>The productId that causes the error.</param>
        public NotStockEnough(long productId)
            : base("The quantity of products to buy is greater than the stock we have. Caused by productId: " + productId)
        {
            this.ProductId = productId;
        }
    }
}