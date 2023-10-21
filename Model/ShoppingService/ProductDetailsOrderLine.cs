using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    /// <summary>
    /// VO Class which contains the details of the product order line
    /// </summary>
    [Serializable()]
    public class ProductDetailsOrderLine
    {
        public string name { get; set; }

        public int quantity { get; set; }

        public decimal unitPrice { get; set; }

        public ProductDetailsOrderLine(string name, int quantity, decimal unitPrice)
        {
            this.name = name;
            this.quantity = quantity;
            this.unitPrice = unitPrice;
        }
    }
}
