using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao
{
    public class OrderLineDaoEntityFramework : GenericDaoEntityFramework<OrderLine, Int64>, IOrderLineDao
    {
        public List<OrderLine> FindOrderLinesByOrderId(long orderId)
        {
            List<OrderLine> sol = null;

            DbSet<OrderLine> orderLineDb = Context.Set<OrderLine>();

            var result = (from ol in orderLineDb where ol.orderId == orderId select ol);

            sol = result.ToList<OrderLine>();

            return sol;

        }
    }
}
