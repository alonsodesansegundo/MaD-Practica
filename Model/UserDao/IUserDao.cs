using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserDao
{
    public interface IUserDao : IGenericDao<User, Int64>
    {
        /// <summary>
        /// Finds a UserProfile by loginName
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>The UserProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        User FindByLoginName(String loginName);

        /// <summary>
        /// Finds a Comment by userId and productId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns>A bool represents if exists one comment from the productId by userId</returns>
        bool ExistsCommentByUserIdAndProductId(long userId, long productId);

        /// <summary>
        /// Finds a Comment by userId and productId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="commentId"></param>
        /// <returns>A bool represents if exists one comment by userId</returns>
        bool FindCommentByUserId(long userId, long commentId);
    }
}