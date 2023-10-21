using Es.Udc.DotNet.ModelUtil.Transactions;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public interface IProductService
    {
        /// <summary>
        /// [FUNC-3] Updates a product.
        /// </summary>
        /// <param name="product"> The product updated. </param>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="ExceptionsProduct.NotPermittedUpdateProductException"/>
        [Transactional]
        void UpdateProduct(long userId, Product product);

        /// <summary>
        /// [FUNC-4] Find products according to name.
        /// </summary>
        /// <param name="keywords"> The keywords to search. </param>
        /// <param name="startIndex"> The startIndex. </param>
        /// <param name="size"> The size. </param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        ProductBlock FindProducts(string keywords, int startIndex, int size);

        /// <summary>
        /// [FUNC-4] Find products according to name.
        /// </summary>
        /// <param name="keywords"> The keywords to search. </param>
        /// <param name="categoryId"> The categoryId to search. </param>
        /// <param name="startIndex"> The startIndex. </param>
        /// <param name="size"> The size. </param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        ProductBlock FindProducts(string keywords, long categoryId, int startIndex, int size);

        /// <summary>
        /// Find all categories of products
        /// </summary>
        [Transactional]
        List<Category> FindAllCategories();

        /// <summary>
        /// [FUNC-4] Find products according to productId.
        /// </summary>
        /// <param name="productId"> The productId. </param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        Product FindProduct(long productId);

        /// <summary>Finds the products by tag.</summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <param name="startIndex"></param>
        /// <param name="size"></param>
        /// <returns>Product's list</returns>
        [Transactional]
        ProductBlock FindProductsByTags(string tagName, int startIndex, int size);

    }
}
