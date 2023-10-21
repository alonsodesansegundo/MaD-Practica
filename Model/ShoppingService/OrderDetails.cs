using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    public class OrderDetails
    {
        public long orderId { get; set; }
        public DateTime creationDate { get; set; }

        public string name { get; set; }

        public decimal totalPrice { get; set; }

        public OrderDetails(long orderId, DateTime creationDate, string name, decimal totalPrice)
        {
            this.orderId = orderId;
            this.creationDate = creationDate;
            this.name = name;
            this.totalPrice = totalPrice;
        }
    }
}
