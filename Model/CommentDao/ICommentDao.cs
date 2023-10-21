using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    public interface ICommentDao : IGenericDao<Comment, Int64>
    {
        /// <summary>
        /// Find the comment's of the productId
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <returns>Comment's list of productId</returns>
        List<Comment> FindComentsByProductId(long productId, int starIndex, int size);


        /// <summary>
        /// Find a comment by userid and product id
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="productId">product id</param>
        /// <returns>The comment</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Comment FindComment(long userId, long productId);
    }
}

