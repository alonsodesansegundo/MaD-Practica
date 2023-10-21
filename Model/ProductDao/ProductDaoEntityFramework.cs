using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductDao
{
    public class ProductDaoEntityFramework : GenericDaoEntityFramework<Product, Int64>, IProductDao
    {
        public ProductDetailsOrderLine FindProductDetailsOrderLineByOrderLineId(long orderLineId)
        {

            OrderLine orderLine = null;
            Product product = null;

            //obtengo la order line
            DbSet<OrderLine> orderLineDb = Context.Set<OrderLine>();

            var result = (from ol in orderLineDb where ol.orderLineId == orderLineId select ol);
            orderLine = result.FirstOrDefault();

            if (orderLine == null)
                throw new InstanceNotFoundException(orderLineId,
                    typeof(OrderLine).FullName);

            //obtengo el producto
            DbSet<Product> productDb = Context.Set<Product>();

            var result2 = (from p in productDb where p.productId == orderLine.productId select p);
            product = result2.FirstOrDefault();

            if (product == null)
                throw new InstanceNotFoundException(orderLine.productId,
                    typeof(Product).FullName);

            //creo el ProductDetailsOrderLine
            ProductDetailsOrderLine productDetailsOrderLine = new ProductDetailsOrderLine(product.name,
                orderLine.quantity, orderLine.productPrice);

            return productDetailsOrderLine;
        }

        public List<Product> FindProducts(string name, int startIndex, int size)
        {
            List<Product> productList = null;

            #region Option 1: Using Linq.

            DbSet<Product> productDb = Context.Set<Product>();

            var result = (from p in productDb where p.name.Contains(@name) orderby p.createDate descending select p);
            productList = result.Skip(startIndex).Take(size).ToList();

            #endregion Option 1: Using Linq.

            if (productList.Count == 0)
                throw new InstanceNotFoundException(name,
                    typeof(Product).FullName);

            return productList;
        }

        public List<Product> FindProducts(string keywords, long categoryId, int startIndex, int size)
        {
            List<Product> productList = null;

            DbSet<Product> productDb = Context.Set<Product>();

            var result = (from p in productDb where p.name.Contains(keywords) && p.categoryId == 
                          categoryId orderby p.createDate descending select p);
            productList = result.Skip(startIndex).Take(size).ToList();


            if (productList.Count == 0)
                throw new InstanceNotFoundException(keywords,
                    typeof(Product).FullName);

            return productList;
        }

        /// <summary>Finds the products by tag.</summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <returns> Product's list </returns>
        public List<Product> FindProductsByTag(string tagName, int startIndex, int size)
        {
            DbSet<Product> product = Context.Set<Product>();
            DbSet<Comment> comment = Context.Set<Comment>();

            var result = (from p in product
                          join c in comment on p.productId equals c.productId
                          where c.Tags.Any(t => t.name == tagName)
                          orderby p.name descending
                          select p).Skip(startIndex).Take(size).ToList();

            return result;
        }
    }
}