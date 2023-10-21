using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.OrderDao
{
    public class OrderDaoEntityFramework : GenericDaoEntityFramework<Order, Int64>, IOrderDao
    {    
        public List<Order> FindByUserId(long userId, int startIndex, int size)
        {
            DbSet<Order> orderDb = Context.Set<Order>();

            List<Order> orderList = new List<Order>();
            var result = (from o in orderDb where o.userId == userId select o).OrderByDescending(
                x => x.orderDate);
            //orderList = result.ToList<Order>();
            orderList = result.Skip(startIndex).Take(size).ToList();

            return orderList;
        }
    }
}
