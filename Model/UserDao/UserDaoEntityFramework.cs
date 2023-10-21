using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserDao
{
    public class UserDaoEntityFramework : GenericDaoEntityFramework<User, Int64>, IUserDao

    {
        #region Public Constructors
        /// <summary>
        /// Public Constructor
        /// </summary>
        public UserDaoEntityFramework()
        {
        }
        #endregion Public Constructors

        #region IUserProfileDao Members. Specific Operations

        /// <summary>
        /// Finds a UserProfile by his loginName
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public User FindByLoginName(string loginName)
        {
            User user = null;

            #region Option 1: Using Linq.

            DbSet<User> userDb = Context.Set<User>();

            var result = (from u in userDb where u.loginName == loginName select u);
            user = result.FirstOrDefault();

            #endregion Option 1: Using Linq.

            if (user == null)
                throw new InstanceNotFoundException(loginName,
                    typeof(User).FullName);

            return user;
        }

        /// <summary>
        /// Finds a Comment by userId and productId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns>A bool represents if exists one comment from the productId by userId</returns>
        public bool ExistsCommentByUserIdAndProductId(long userId, long productId)
        {
            //compruebo si existe el comentario
            Comment comment = null;

            DbSet<Comment> commentDb = Context.Set<Comment>();

            var result = (from c in commentDb where c.userId == userId && c.productId == productId select c);
            comment = result.FirstOrDefault();

            if (comment != null)
                return true;
            return false;
        }

        /// <summary>
        /// Finds a Comment by userId and productId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="commentId"></param>
        /// <returns>A bool represents if exists one comment by userId</returns>
        public bool FindCommentByUserId(long userId, long commentId)
        {
            Comment comment = null;
            DbSet<Comment> commentDb = Context.Set<Comment>();
            var result = (from c in commentDb where c.userId == userId && c.commentId == commentId select c);
            comment = result.FirstOrDefault();

            if (comment != null)
                return true;
            return false;
        }

        #endregion IUserProfileDao Members
    }
}
