using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    public class CommentDaoEntityFramework : GenericDaoEntityFramework<Comment, Int64>, ICommentDao
    {
        public List<Comment> FindComentsByProductId(long productId, int starIndex, int size)
        {
            List<Comment> commentList = new List<Comment>();

            DbSet<Comment> commentDb = Context.Set<Comment>();

            var result = (from c in commentDb where c.productId == productId select c).OrderByDescending(x => x.createDate);
            commentList = result.Skip(starIndex).Take(size).ToList();
            return commentList;
        }

        public Comment FindComment(long userId, long productId)
        {
            Comment comment = null;

            #region Option 1: Using Linq.

            DbSet<Comment> commentDb = Context.Set<Comment>();

            var result = (from c in commentDb
                          where c.productId == productId &&
      c.userId == userId
                          select c);
            comment = result.FirstOrDefault();

            #endregion Option 1: Using Linq.

            return comment;
        }
    }
}
