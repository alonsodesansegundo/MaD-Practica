using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    /// <summary>
    /// VO Class which contains the items of shopping cart
    /// </summary>
    [Serializable()]
    public class ShoppingCartItem
    {
        public Product product { get; set; }

        public int quantity { get; set; }

        public bool isGiftProduct { get; set; }

        public decimal productPriceActual { get; set; }

        public ShoppingCartItem(Product product)
        {
            this.product = product;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartItem"/> class.
        /// </summary>
        public ShoppingCartItem() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartItem"/> class.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="quantity">The quantity of product.</param>
        public ShoppingCartItem(Product product, int quantity)
        {
            this.product = product;
            this.quantity = quantity;
            this.isGiftProduct = false;
        }

        /// <summary>Initializes a new instance of the <see cref="ShoppingCartItem" /> class.</summary>
        /// <param name="product">The product.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="isGiftProduct">if set to <c>true</c> [is gift product].</param>
        public ShoppingCartItem(Product product, int quantity, bool isGiftProduct)
        {
            this.product = product;
            this.quantity = quantity;
            this.isGiftProduct = isGiftProduct;
        }

        /// <summary>Initializes a new instance of the <see cref="ShoppingCartItem" /> class.</summary>
        /// <param name="product">The product.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="isGiftProduct">if set to <c>true</c> [is gift product].</param>
        /// <param name="productPriceActual"></param>
        public ShoppingCartItem(Product product, int quantity, bool isGiftProduct, decimal productPriceActual)
        {
            this.product = product;
            this.quantity = quantity;
            this.isGiftProduct = isGiftProduct;
            this.productPriceActual = productPriceActual;
        }

        public override bool Equals(object obj)
        {
            return obj is ShoppingCartItem item &&
                   EqualityComparer<Product>.Default.Equals(product, item.product) &&
                   quantity == item.quantity &&
                   isGiftProduct == item.isGiftProduct;
        }

        public override int GetHashCode()
        {
            int hashCode = -1669317934;
            hashCode = hashCode * -1521134295 + EqualityComparer<Product>.Default.GetHashCode(product);
            hashCode = hashCode * -1521134295 + quantity.GetHashCode();
            hashCode = hashCode * -1521134295 + isGiftProduct.GetHashCode();
            return hashCode;
        }

        public override String ToString()
        {
            String strShoppingCartItem;

            strShoppingCartItem =
                "[ product = " + product + " | " +
                "quantity = " + quantity + " | " +
                "isGiftProduct = " + isGiftProduct + " ]";
            return strShoppingCartItem;
        }

    }
}