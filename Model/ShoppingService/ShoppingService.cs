using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    public class ShoppingService : IShoppingService
    {
        [Inject]
        public IUserDao UserDao { private get; set; }
        [Inject]
        public ICreditCardDao CreditCardDao { private get; set; }
        [Inject]
        public IOrderDao OrderDao { private get; set; }
        [Inject]
        public IOrderLineDao OrderLineDao { private get; set; }
        [Inject]
        public IProductDao ProductDao { private get; set; }

        /// [FUNC-5] Visualizar carrito
        /// <exception cref="PermissionException"></exception>
        public List<ShoppingCartItem> VisualizeShoppingCart(ShoppingCart shoppingCart)
        {
            List<ShoppingCartItem> listItems = shoppingCart.shoppingCartItems;

            return listItems;
        }

        /// [FUNC-5] Añadir productos al carrito
        /// <param name="shoppingCart">the shopping cart</param>
        /// <param name="productId">the product id</param>
        /// <param name="quantity">the quantity of product</param>
        /// <exception cref="InstanceNotFoundException"></exception>
        /// <exception cref="NotStockEnough"></exception>
        public ShoppingCart AddProductFromShoppingCart(ShoppingCart shoppingCart, long productId, int quantity)
        {
            var product = ProductDao.Find(productId);

            if (product == null)
            {
                throw new InstanceNotFoundException(productId, typeof(Product).FullName);
            }

            if (product.stock < quantity)
            {
                throw new NotStockEnough(productId);
            }

            int aux = shoppingCart.shoppingCartItems.Count;
            ShoppingCartItem newItem = new ShoppingCartItem(product, quantity);

            bool encontrado = false;

            for (int i = 0; i < aux; i++)
            {
                ShoppingCartItem item = shoppingCart.shoppingCartItems.ElementAt(i);
                if (item.product.productId == product.productId)
                {
                    var price = item.product.price * item.quantity;
                    shoppingCart.TotalPrice -= price;

                    item.quantity += quantity;
                    item.productPriceActual = product.price * item.quantity;
                    shoppingCart.TotalPrice += item.productPriceActual;
                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
            {
                //Modificando el precio total del carrito
                newItem.productPriceActual = product.price * quantity;
                shoppingCart.shoppingCartItems.Add(newItem);
                shoppingCart.TotalPrice += newItem.productPriceActual;
            }

            //Modificando el stock, del producto segun la cantidad que vamos a añadir al carrito.
            product.stock -= quantity;
            //ProductDao.Update(product);

            return shoppingCart;
        }

        /// [FUNC-5] Eliminar productos del carrito
        /// <param name="shoppingCart"></param>
        /// <param name="productId"></param>
        /// <returns> Shopping Cart</returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public ShoppingCart RemoveItemFromShoppingCart(ShoppingCart shoppingCart, long productId)
        {
            var product = ProductDao.Find(productId);

            if (product == null)
            {
                throw new InstanceNotFoundException(productId, typeof(Product).FullName);
            }

            int aux = shoppingCart.shoppingCartItems.Count;
            for (int i = 0; i < aux; i++)
            {
                ShoppingCartItem item = shoppingCart.shoppingCartItems.ElementAt(i);

                if (item.product.productId == product.productId)
                {
                    shoppingCart.TotalPrice -= (item.quantity * item.product.price);
                    product.stock += item.quantity;
                    shoppingCart.shoppingCartItems.Remove(item);
                    aux = shoppingCart.shoppingCartItems.Count;
                }
            }

            return shoppingCart;
        }

        /// <summary>
        /// Remove Item from Shopping Cart Not Empty Before Logout
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <exception cref="InstanceNotFoundException"></exception>
        public ShoppingCart RemoveItemsFromShoppingCartNotEmpty(ShoppingCart shoppingCart)
        {
            int aux = shoppingCart.shoppingCartItems.Count;
            for (int i = aux - 1; i >= 0; i--)
            {
                ShoppingCartItem item = shoppingCart.shoppingCartItems.ElementAt(i);
                var product = item.product;

                shoppingCart.TotalPrice -= (item.quantity * item.product.price);
                product.stock += item.quantity;
                shoppingCart.shoppingCartItems.Remove(item);
              //  aux = shoppingCart.shoppingCartItems.Count;
                
            }

            return shoppingCart;
        }


        /// [FUNC-5] Modificar el número de unidades que desea adquirir de un producto.
        /// <param name="shoppingCart"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <exception cref="InstanceNotFoundException"></exception>
        /// <exception cref="NotStockEnough"></exception>
        public ShoppingCart UpdateQuantityShoppingCart(ShoppingCart shoppingCart, long productId, int quantity)
        {
            var product = ProductDao.Find(productId);

            if (product == null)
            {
                throw new InstanceNotFoundException(productId, typeof(Product).FullName);
            }

            int aux = shoppingCart.shoppingCartItems.Count;
            for (int i = 0; i < aux; i++)
            {
                ShoppingCartItem item = shoppingCart.shoppingCartItems.ElementAt(i);

                if (item.product.productId == product.productId)
                {
                    //Añadimos al stock la cantidad del producto vieja
                    //Así comprobamos realmente cuanto stock hay
                    product.stock += item.quantity;

                    if (product.stock < quantity)
                    {
                        throw new NotStockEnough(productId);
                    }

                    var price = item.product.price * item.quantity;
                    shoppingCart.TotalPrice -= price;

                    item.quantity = quantity;
                    item.productPriceActual = product.price * item.quantity;
                    shoppingCart.TotalPrice += item.productPriceActual;

                    product.stock -= item.quantity;

                }                

            }

            return shoppingCart;
        }

        /// [FUNC-5] Indicar si un producto se quiere envuelto para regalo
        /// <param name="shoppingCart"></param>
        /// <param name="productId"></param>
        /// <param name="isGift"></param>
        ///<exception cref="InstanceNotFoundException"></exception>
        public ShoppingCart IsGiftProductShoppingCart(ShoppingCart shoppingCart, long productId, bool isGift)
        {
            var product = ProductDao.Find(productId);

            if (product == null)
            {
                throw new InstanceNotFoundException(productId, typeof(Product).FullName);
            }

            int aux = shoppingCart.shoppingCartItems.Count;
            for (int i = 0; i < aux; i++)
            {
                ShoppingCartItem item = shoppingCart.shoppingCartItems.ElementAt(i);

                if (item.product.productId == product.productId)
                {
                    item.isGiftProduct = isGift;
                }
            }

            return shoppingCart;
        }

        /// <exception cref="ShoppingCartEmptyException"/>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncompatibleCreditCardUserException"/>
        public long RegisterOrder(ShoppingCart shoppingCart, long userId, 
            long creditCardId, string postalAddress, string orderName)
        {
            // si el carrito no tiene elementos
            if(shoppingCart.shoppingCartItems.Count==0) throw new ShoppingCartEmptyException(userId);

            // si el usuario no existe 
            User user = null;
            try
            {
                user = UserDao.Find(userId);
            }
            catch (InstanceNotFoundException e)
            {
                throw e;
            }

            //si la tarjeta de credito no existe
            CreditCard creditCard = null;
            try
            {
                creditCard = CreditCardDao.Find(creditCardId);
            }
            catch (InstanceNotFoundException e)
            {
                throw e;
            }

            //si la tarjeta de credito no pertenece al usuario
            if (creditCard.userId != user.userId) 
                throw new IncompatibleCreditCardUserException(userId, creditCardId);

            //si hemos llegado hasta aqui es que es posible realizar el pedido

            //creamos el pedido para poder añadir las lineas de pedido posteriormente
            Order order = new Order();
            order.creditCardId = creditCardId;
            order.orderName = orderName;
            order.postalAddressOrder = postalAddress;
            order.userId = userId;
            order.orderDate = DateTime.Now;
            order.totalPrice = shoppingCart.TotalPrice;

            OrderDao.Create(order);

            //creamos las lineas de pedido

            OrderLine orderLine = null;
            foreach (ShoppingCartItem actual in shoppingCart.shoppingCartItems) {
                orderLine = new OrderLine();
                orderLine.orderId = order.orderId;
                orderLine.productId = actual.product.productId;
                orderLine.productPrice = actual.product.price;
                orderLine.isGift = actual.isGiftProduct;
                orderLine.quantity = actual.quantity;

                OrderLineDao.Create(orderLine);
            }

            //vacío el carrito de usuario
            shoppingCart.shoppingCartItems.Clear();
            shoppingCart.TotalPrice = 0;
            return order.orderId;
            //total price
            //createDate
        }

        /// <exception cref="InstanceNotFoundException"/>
        public OrderBlock FindOrdersByUserId(long userId, int startIndex, int size)
        {
            User user = null;
            user = UserDao.Find(userId);

            if (user == null)
                throw new InstanceNotFoundException(userId, typeof(User).FullName);

            List<Order> listOrders = OrderDao.FindByUserId(userId, startIndex, size + 1);
            var ordersDetails = new List<OrderDetails>();
            listOrders.ForEach(order =>
            {
                var orderDetails = new OrderDetails(order.orderId,
                    order.orderDate, order.orderName, order.totalPrice);
                ordersDetails.Add(orderDetails);
            });

            bool existMoreItems = (listOrders.Count == size + 1);

            if (existMoreItems)
                ordersDetails.RemoveAt(size);

            return new OrderBlock(ordersDetails, existMoreItems);
        }

        public List<OrderLine> FindOrderLinesByOrderId(long orderId)
        {
            Order order = null;
            order = OrderDao.Find(orderId);

            if (order == null)
                throw new InstanceNotFoundException(orderId, typeof(Order).FullName);

            List<OrderLine> listOrderLines = OrderLineDao.FindOrderLinesByOrderId(orderId);

            return listOrderLines;
        }

        //[FUNC-7] Ver los productos (nombre, cantidad y precio unitario en el momento de la compra) relativos a un producto

        /// <summary>
        /// Find the product details from the order line
        /// </summary>
        /// <param name="orderLineId"> The order line id. </param>
        /// <exception cref="InstanceNotFoundException"/>
        public ProductDetailsOrderLine FindProductDetailsOrderLineByOrderLineId(long orderLineId)
        {
            OrderLine orderLine = null;

            orderLine = OrderLineDao.Find(orderLineId);

            if (orderLine == null)
                throw new InstanceNotFoundException(orderLineId, typeof(OrderLine).FullName);

            ProductDetailsOrderLine productDetailsOrderLine = ProductDao.FindProductDetailsOrderLineByOrderLineId(orderLineId);

            return productDetailsOrderLine;
        }


    }
}
