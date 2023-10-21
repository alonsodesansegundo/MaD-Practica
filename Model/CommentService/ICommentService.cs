using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public interface ICommentService
    {
        /// <summary>Registers a new comment.</summary>
        /// <param name="userId">The user id.</param>
        /// <param name="productId">The product id.</param>
        /// <param name="text">The comment text.</param>
        /// <param name="tags">Tag's list</param>
        /// <exception cref="InstanceNotFoundException"></exception>
        /// <exception cref="DuplicateCommentException"></exception>
        [Transactional]
        long AddComment(long userId, long productId, string text, List<string> tags);

        /// <summary>
        /// Adds the tag to comment.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <exception cref="InstanceNotFoundException"></exception>
        Tag AddTagToComment(string tagName);

        /// <summary>
        /// Update the tag to comment.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="tagName">Name of the tag.</param>
        /// <exception cref="InstanceNotFoundException"></exception>
        Tag UpdateTagToComment(long commentId, string tagName);

        /// <summary>Update a comment.</summary>
        /// <param name="userId">The user id.</param>
        /// <param name="commentId">The comment id.</param>
        /// <param name="text">The comment text.</param>
        /// <param name="tags">Tag's list</param>
        /// <exception cref="InstanceNotFoundException"></exception>
        /// <exception cref="NotPermittedUpdateCommentException"></exception>
        [Transactional]
        long UpdateComment(long userId, long commentId, string text, List<string> tags);

        /// <summary>
        /// Remove a comment.
        /// </summary>
        /// <param name="userId"> The user id. </param>
        /// <param name="commentId"> The comment id. </param>
        /// <exception cref="InstanceNotFoundException"/> 
        /// <exception cref="NotPermittedRemoveCommentException"/>
        [Transactional] 
        long RemoveComment(long userId, long commentId);

        /// <summary>
        /// Deletes the tag from comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="tagName">Name of the tag.</param>
        /// <exception cref="InstanceNotFoundException"></exception>
        void DeleteTagFromComment(long commentId, String tagName);

        /// <summary>Find the comments of the product</summary>
        /// <param name="productId">The product id.</param>
        /// <param name="startIndex"></param>
        /// <param name="size"></param>
        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        CommentBlock FindComentsByProductId(long productId, int startIndex, int size);

        /// <summary>
        /// Gets all tags created
        /// </summary>
        /// <returns>List with all tags created</returns>
        List<String> GetAllTags();

        /// <summary>
        /// Gets the tags by uses
        /// </summary>
        /// <returns>A list of TagDetails ordered by the uses of the tags</returns>
        List<TagDetails> GetTagsByUse();

        List<TagDetails> GetTagsByUse2();

        /// <summary>
        /// Gets the comment of user in product details
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="productId">The product id.</param>
        /// <returns>The comment</returns>
        [Transactional]
        Comment GetComment(long userId, long productId);
    }
}
