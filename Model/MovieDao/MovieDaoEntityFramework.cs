using Es.Udc.DotNet.ModelUtil.Dao;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.MovieDao
{
    public class MovieDaoEntityFramework : GenericDaoEntityFramework<Movie, Int64>, IMovieDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public MovieDaoEntityFramework()
        {
        }

        #endregion Public Constructors
    }
}