using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public class ProductBlock
    {
        public List<ProductSummary> Products { get; private set; }

        public bool ExistMoreItems { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ProductBlock" /> class.</summary>
        /// <param name="Products">The products.</param>
        /// <param name="ExistMoreItems">if set to <c>true</c> [exist more items].</param>
        public ProductBlock(List<ProductSummary> Products, bool ExistMoreItems)
        {
            this.Products = Products;
            this.ExistMoreItems = ExistMoreItems;
        }

    }
}