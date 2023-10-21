using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService.Exceptions
{
    /// <summary>
    /// Public <c>ModelException</c> which captures when user try to remove comment that is not theirs
    /// </summary>
    [Serializable]
    public class NotPermittedRemoveCommentException : Exception
    {
        /// <summary>
        /// Stores the user id of the exception
        /// </summary>
        /// <value>The id of the user.</value>
        public long UserId { get; private set; }

        /// <summary>
        /// Stores the comment id of the exception
        /// </summary>
        /// <value>The id of the comment.</value>
        public long CommentId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="NotPermittedRemoveCommentException"/> class.
        /// </summary>
        /// <param name="userId"><c>userId</c> that causes the error.</param>
        /// <param name="commentId"><c>commentId</c> that causes the error.</param>
        public NotPermittedRemoveCommentException(long userId, long commentId)
            : base("The user: " + userId + " try to remove the comment " + commentId + " when it is not theirs")
        {
            this.UserId = userId;
            this.CommentId = commentId;
        }
    }
}