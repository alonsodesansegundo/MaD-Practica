using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    /// <summary>
    /// VO Class which contains the user shopping cart
    /// </summary>
    [Serializable()]
    public class ShoppingCart
    {
        public List<ShoppingCartItem> shoppingCartItems { get; set; }

        public decimal TotalPrice { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ShoppingCart" /> class.</summary>
        public ShoppingCart()
        {
            shoppingCartItems = new List<ShoppingCartItem>();
        }
        /// <summary>Initializes a new instance of the <see cref="ShoppingCart" /> class.</summary>
        /// <param name="user">The user.</param>
        /// <param name="shoppingCartItems">The shopping cart items.</param>
        public ShoppingCart(List<ShoppingCartItem> shoppingCartItems)
        {
            this.shoppingCartItems = shoppingCartItems;
        }

        /// <summary>Initializes a new instance of the <see cref="ShoppingCart" /> class.</summary>
        /// <param name="user">The user.</param>
        /// <param name="shoppingCartItems">The shopping cart items.</param>
        /// <param name="TotalPrice"></param>
        public ShoppingCart(List<ShoppingCartItem> shoppingCartItems, decimal TotalPrice)
        {
            this.shoppingCartItems = shoppingCartItems;
            this.TotalPrice = TotalPrice;
        }

        /// <summary>Determines whether the specified <see cref="System.Object" />, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return obj is ShoppingCart cart &&
                   EqualityComparer<List<ShoppingCartItem>>.Default.Equals(shoppingCartItems, cart.shoppingCartItems);
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            int hashCode = 2084075499;
            hashCode = hashCode * -1521134295 + EqualityComparer<List<ShoppingCartItem>>.Default.GetHashCode(shoppingCartItems);
            return hashCode;
        }
    }
}