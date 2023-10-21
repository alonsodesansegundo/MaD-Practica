using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductDao
{
    public interface IProductDao : IGenericDao<Product, Int64>
    {

        /// <summary>
        /// Find Products by name and category
        /// </summary>
        /// <param name="keywords">The keywords</param>
        /// <param name="startIndex">The index of the first image to return (starting in 0)</param>
        /// <param name="size">The maximum number of images to return</param>
        /// <returns>List of Products</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Product> FindProducts(string keywords, int startIndex, int size);

        /// <summary>
        /// Find Products by name and category
        /// </summary>
        /// <param name="keywords">The keywords</param>
        /// <param name="categoryId">The categoryId</param>
        /// <param name="startIndex">The index of the first image to return (starting in 0)</param>
        /// <param name="size">The maximum number of images to return</param>
        /// <returns>List of Products</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Product> FindProducts(string keywords, long categoryId, int startIndex, int size);

        /// <summary>Finds the products by tag.</summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <returns>Product's list</returns>
        List<Product> FindProductsByTag(string tagName, int startIndex, int size);


        /// <summary>
        /// Find a Product details order line by order line id
        /// </summary>
        /// <param name="orderLineId">The order line id</param>
        /// <returns>The Product Details Order Line</returns>
        ProductDetailsOrderLine FindProductDetailsOrderLineByOrderLineId(long orderLineId);
    }
}