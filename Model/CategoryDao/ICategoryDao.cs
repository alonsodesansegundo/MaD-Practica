using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    public interface ICategoryDao : IGenericDao<Category, Int64>
    {
        /// <summary>
        /// Find a Category by name
        /// </summary>
        /// <param name="category">name</param>
        /// <returns>The categoryId</returns>
        /// <exception cref="InstanceNotFoundException"/>
        long FindByName(string category);
    }
}
