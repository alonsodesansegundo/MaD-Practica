using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    public class OrderBlock
    { 
        public List<OrderDetails> Orders { get; private set; }

        public bool ExistMoreItems { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="OrderBlock" /> class.</summary>
        /// <param name="Orders">The orders.</param>
        /// <param name="ExistMoreItems">if set to <c>true</c> [exist more items].</param>
        public OrderBlock(List<OrderDetails> Orders, bool ExistMoreItems)
        {
            this.Orders = Orders;
            this.ExistMoreItems = ExistMoreItems;
        }
    }
}

