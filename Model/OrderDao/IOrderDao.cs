using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.OrderDao
{
    public interface IOrderDao : IGenericDao<Order, Int64>
    {
        /// <summary>
        /// Finds order's by userId
        /// </summary>
        /// <param name="userId">name</param>
        /// <param name="startIndex">The start index</param>
        /// <param name="size">The size</param>
        /// <returns>The list of order's</returns>
        List<Order> FindByUserId(long userId, int startIndex, int size);
    }
}
