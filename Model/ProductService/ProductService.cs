using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.BookDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.MovieDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService.ExceptionsProduct;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Ninject;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public class ProductService : IProductService
    {
        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Inject]
        public ICategoryDao CategoryDao { private get; set; }

        [Inject]
        public IMovieDao MovieDao { private get; set; }

        [Inject]
        public IBookDao BookDao { private get; set; }

        [Inject]
        public IUserDao UserDao { private get; set; }

        /// <summary>
        /// [FUNC-3] Updates a product.
        /// </summary>
        /// <param name="product"> The product updated. </param>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="NotPermittedUpdateProductException"/>
        public void UpdateProduct(long userId, Product product)
        {
            var p = ProductDao.Find(product.productId);

            if (p == null)
                throw new InstanceNotFoundException(product.productId, typeof(Product).FullName);

            User user = UserDao.Find(userId);

            if (user == null)
                throw new InstanceNotFoundException(user.userId, typeof(User).FullName);

            if (!user.admin)
                throw new NotPermittedUpdateProductException(user.userId, p.productId);

            ProductDao.Update(product);
        }


        public List<Category> FindAllCategories()
        {
            return new List<Category>(CategoryDao.GetAllElements());
        }

        /// <exception cref="InstanceNotFoundException"/>
        public Product FindProduct(long productId)
        {
            return ProductDao.Find(productId);
        }


        /// <exception cref="InstanceNotFoundException"/>
        public ProductBlock FindProducts(string keywords, int startIndex, int size)
        {
            List<Product> products = ProductDao.FindProducts(keywords, startIndex, size + 1);
            var productsSummary = new List<ProductSummary>();
            products.ForEach(product =>
            {
                var prpSum = new ProductSummary();
                prpSum.Id = product.productId;
                prpSum.Category = product.Category.categoryName;
                prpSum.Name = product.name;
                prpSum.CreateDate = product.createDate;
                prpSum.Price = product.price;
                prpSum.Stock = product.stock;

                productsSummary.Add(prpSum);
            });
            bool existMoreItems = (productsSummary.Count == size + 1);
            if (existMoreItems)
                productsSummary.RemoveAt(size);
            return new ProductBlock(productsSummary, existMoreItems);
        }


        /// <exception cref="InstanceNotFoundException"/>
        public ProductBlock FindProducts(string keywords, long categoryId, int startIndex, int size)
        {
            List<Product> products = ProductDao.FindProducts(keywords, categoryId, startIndex, size + 1);
            var productsSummary = new List<ProductSummary>();
            products.ForEach(product =>
            {
                var prpSum = new ProductSummary();
                prpSum.Id = product.productId;
                prpSum.Category = product.Category.categoryName;
                prpSum.Name = product.name;
                prpSum.CreateDate = product.createDate;
                prpSum.Price = product.price;
                prpSum.Stock = product.stock;

                productsSummary.Add(prpSum);
            });
            bool existMoreItems = (productsSummary.Count == size + 1);
            if (existMoreItems)
                productsSummary.RemoveAt(size);
            return new ProductBlock(productsSummary, existMoreItems);
        }

        /// <summary>Finds the products by tag.</summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <returns> Product's list </returns>
        public ProductBlock FindProductsByTags(string tagName, int startIndex, int size)
        {
            List<Product> products = ProductDao.FindProductsByTag(tagName, startIndex, size + 1);
            var productsSummary = new List<ProductSummary>();
            products.ForEach(product =>
            {
                var prpSum = new ProductSummary();
                prpSum.Id = product.productId;

                /*
                if (product.Category != null)
                    prpSum.Category = product.Category.categoryName;
                else
                {
                    
                    Category category = CategoryDao.Find(product.categoryId);
                    prpSum.Category = category.categoryName;
                }*/


                prpSum.Category = product.Category.categoryName;
                prpSum.Name = product.name;
                prpSum.CreateDate = product.createDate;
                prpSum.Price = product.price;
                prpSum.Stock = product.stock;

                productsSummary.Add(prpSum);
            });

            bool existMoreItems = (productsSummary.Count == size + 1);

            if (existMoreItems)
                productsSummary.RemoveAt(size);

            return new ProductBlock(productsSummary, existMoreItems);
        }

    }
}
