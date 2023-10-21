using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions
{
    /// <summary>
    ///  Exception class for Permission Exception
    /// </summary>
    public class PermissionException : Exception
    {
        public PermissionException(long idUser)
         : base("User has not permission to do this action: Delete. idUser -> " + idUser)
        {
            this.idUser = idUser;
        }

        public long idUser { get; private set; }
    }
}