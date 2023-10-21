using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService.Exceptions
{
    /// <summary>
    /// Public <c>ModelException</c> which captures the repeat comments by userId of the same productId
    /// </summary>
    [Serializable]
    public class DuplicateCommentException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="DuplicateCommentException"/> class.
        /// </summary>
        /// <param name="userId"><c>user id</c> that causes the error.</param>
        /// <param name="productId"><c>product id</c> that causes the error.</param>
        public DuplicateCommentException(long userId, long productId)
            : base("It already exists one comment to the product: " + productId + " by user: " + userId)
        {
            this.UserId = userId;
            this.ProductId = productId;
        }

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
    }
}
