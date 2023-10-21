using Es.Udc.DotNet.ModelUtil.Dao;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.BookDao
{
    public class BookDaoEntityFramework : GenericDaoEntityFramework<Book, Int64>, IBookDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public BookDaoEntityFramework()
        {
        }

        #endregion Public Constructors
    }
}