using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    public class CategoryDaoEntityFramework : GenericDaoEntityFramework<Category, Int64>, ICategoryDao
    {
        /// <summary>
        /// Find a Category by name
        /// </summary>
        /// <param name="category">name</param>
        /// <returns>The Category Name</returns>
        /// <exception cref="InstanceNotFoundException"/>
        public long FindByName(string category)
        {
            long categoryId = -1;

            DbSet<Category> categoryDb = Context.Set<Category>();

            var result = (from p in categoryDb where p.categoryName == category select p.categoryId);
            categoryId = result.FirstOrDefault();


            if (categoryId == -1)
                throw new InstanceNotFoundException(categoryId,
                    typeof(Category).FullName);

            return categoryId;
        }
    }
}
