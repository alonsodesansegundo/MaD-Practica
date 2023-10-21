using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao
{
    public interface IOrderLineDao : IGenericDao<OrderLine, Int64>
    {
        /// <summary>
        /// Get the order lines of an order id
        /// </summary>
        /// <param name="orderId">The order id</param>
        List<OrderLine> FindOrderLinesByOrderId(long orderId);
    }
}
