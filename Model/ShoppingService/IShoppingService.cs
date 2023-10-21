using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    public interface IShoppingService
    {
        /// <summary>
        /// Visualizes the shopping cart.
        /// </summary>
        /// <param name="shoppingCart">The shopping cart.</param>
        /// <returns> List of shopping cart items </returns>
        /// <exception cref="Exceptions.PermissionException"></exception>
        [Transactional]
        List<ShoppingCartItem> VisualizeShoppingCart(ShoppingCart shoppingCart);

        /// <summary>
        /// Add Product from ShoppingCart
        /// </summary>
        /// <param name="shoppingCart">the shopping cart</param>
        /// <param name="productId">the product id</param>
        /// <param name="quantity">the quantity of product</param>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="Exceptions.NotStockEnough"></exception>
        [Transactional]
        ShoppingCart AddProductFromShoppingCart(ShoppingCart shoppingCart, long productId, int quantity);

        /// <summary>
        /// Remove Item from Shopping Cart
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <param name="productId"></param>
        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        ShoppingCart RemoveItemFromShoppingCart(ShoppingCart shoppingCart, long productId);

        /// <summary>Updates the quantity shopping cart.</summary>
        /// <param name="shoppingCart">The shopping cart.</param>
        /// <param name="productId">The product identifier.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns> Shopping Cart </returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        /// <exception cref="Exceptions.NotStockEnough"></exception>
        [Transactional]
        ShoppingCart UpdateQuantityShoppingCart(ShoppingCart shoppingCart, long productId, int quantity);

        /// <summary> Indicate if user want a product gift-wrapped</summary>
        /// <param name="shoppingCart">The shopping cart.</param>
        /// <param name="productId">The product identifier.</param>
        /// <param name="isGift">if set to <c>true</c> [is gift].</param>
        /// <returns>ShoppingCart</returns>
        ///<exception cref="InstanceNotFoundException"></exception>
        /// <exception cref="PermissionException"></exception>
        [Transactional]
        ShoppingCart IsGiftProductShoppingCart(ShoppingCart shoppingCart, long productId, bool isGift);

        /// <summary>
        /// [FUNC-6] Registers a new order.
        /// </summary>
        /// <param name="shoppingCart"> The shopping cart. </param>
        /// <param name="creditCardId"> The credit card id. </param>
        /// <param name="postalAddress"> The postal address of the order. </param>
        /// <param name="orderName"> The name of the order. </param>
        /// <exception cref="ShoppingCartEmptyException"/>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncompatibleCreditCardUserException"/>
        [Transactional]
        long RegisterOrder(ShoppingCart shoppingCart, long userId,
            long creditCardId, string postalAddress, string orderName);

        /// <summary>
        /// [FUNC-7] Find the order's from userId
        /// </summary>
        /// <param name="userId"> The user id. </param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        OrderBlock FindOrdersByUserId(long userId, int startIndex, int size);

        /// Find the order's lines from orderId
        /// </summary>
        /// <param name="orderId"> The order id. </param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        List<OrderLine> FindOrderLinesByOrderId(long orderId);


        /// <summary>
        /// Find the product details from the order line
        /// </summary>
        /// <param name="orderLineId"> The order line id. </param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        ProductDetailsOrderLine FindProductDetailsOrderLineByOrderLineId(long orderLineId);

        /// <summary>
        /// Remove Item from Shopping Cart Not Empty Before Logout
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        ShoppingCart RemoveItemsFromShoppingCartNotEmpty(ShoppingCart shoppingCart);

    }
}