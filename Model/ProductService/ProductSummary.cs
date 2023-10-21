using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public class ProductSummary
    {
        private long id;
        public long Id {
            get { return id; }
            set { id = value; } 
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string category;
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        private DateTime createDate;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        private int stock;
        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        public override bool Equals(object obj)
        {
            return obj is ProductSummary summary &&
                   id == summary.id &&
                   name == summary.name &&
                   category == summary.category &&
                   createDate == summary.createDate &&
                   price == summary.price;
        }
        public override int GetHashCode()
        {
            int hashCode = 1012081786;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(category);
            hashCode = hashCode * -1521134295 + createDate.GetHashCode();
            hashCode = hashCode * -1521134295 + price.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            string strProductSummary;
            strProductSummary =
                "[ Id = " + id + " | " +
                "Name = " + name + " | " +
                "Category = " + category + " | " +
                "CreateDate = " + createDate + " | " +
                "Stock = " + stock + " | " +
                "Price = " + price + " ]";
            return strProductSummary;
        }
    }
}