using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Assignment4
{
    public class DataService
    {

        // --- GetProduct_ValidId_ReturnsProductWithCategory() --- //
        public Product GetProduct(int prodId)
        {
            using (var db = new NorthwindContext())
            {
                var Products = db.Products.Where(x => x.Id == prodId);
                if (Products.Any())
                {
                    return Products.First();
                }
            }
            return null;
        }

        // Korrekt
        public List<Product> GetProductByName(string substring)
        {
            List<Product> matchingProducts = null;

            using (var db = new NorthwindContext())
            {
                matchingProducts = db.Products.Where(x => x.Name.Contains(substring)).ToList();
                if (matchingProducts.Any())
                {

                    return matchingProducts.ToList();
                }
            }
            // return list of tuples even if null. Null should be checked 
            return null;
        }

        public List<Product> GetProductByCategory(int catId)
        {
            List<Product> matchingProducts = null;

            using (var db = new NorthwindContext())
            {
                matchingProducts = db.Products.Where(x => x.CategoryId == catId).ToList();
                if (matchingProducts.Any())
                {
                    return matchingProducts.ToList();
                }
            } 
            return null;
        }


        // --- public void GetOrder_ValidId_ReturnsCompleteOrder() --- //
        public Order GetOrder(int orderId)
        {
            var db = new NorthwindContext();
            var test = db.Orders.FirstOrDefault(
                x => x.Id == orderId);
            return test;
        }

        // --- public void GetOrders() --- //
        public List<Order> GetOrders()
        {
            using (var db = new NorthwindContext())
            {
                var orders = db.Orders.ToList();

                return orders;
            }
        }

        public List<OrderDetails> GetOrderDetailsByOrderId(int orderId)
        {
            using (var db = new NorthwindContext())
            {
                List<OrderDetails> OrderDetails = (List<OrderDetails>)db.OrderDetails.Where(x => x.OrderId == orderId).ToList();
                if (OrderDetails.Any())
                {
                    return OrderDetails;
                }
            }
            return null;
        }

        public List<OrderDetails> GetOrderDetailsByProductId(int productId)
        {
            using (var db = new NorthwindContext())
            {
                List<OrderDetails> OrderDetails = (List<OrderDetails>)db.OrderDetails.Where(x => x.ProductId == productId).ToList();
                if (OrderDetails.Any())
                {
                    return OrderDetails;
                }
            }
            return null;
        }


        // --- GetCategory_ValidId_ReturnsCategoryObject() --- //
        public Category GetCategory(int catId)
        {
            using (var db = new NorthwindContext())
            {
                List<Category> Categories = (List<Category>)db.Categories.Where(x => x.Id == catId).ToList();
                if (Categories.Any())
                {
                    return Categories[0];
                }
            }
            return null;
        }

        public List<Category> GetCategories()
        {
            using (var db = new NorthwindContext())
            {
                var categories = db.Categories;

                return categories.ToList();
            }
        }


        public Category CreateCategory(String name, String description)
        {
            using (var db = new NorthwindContext())
            {
                var category = new Category
                {
                    Name = name,
                    Description = description
                };

                db.Categories.Add(category);

                db.SaveChanges();

                {
                    return category;
                }
            }
        }


        //Færdig
        public bool UpdateCategory(int id, string name, string description)
        {
            using (var db = new NorthwindContext())
            {
                var category = db.Categories.FirstOrDefault(x => x.Id == id);

                if (category != null)
                {
                    category.Name = name;
                    category.Description = description;
                    db.SaveChanges();
                    return true;
                }
                

            }

            return false;
        }
        //Færdig
        public bool DeleteCategory(int id)
        {

            using (var db = new NorthwindContext())
            {
                var category = db.Categories.FirstOrDefault(x => x.Id == id);

                if (category != null)
                {
                    db.Categories.Remove(category);
                    db.SaveChanges();
                    return true;
                }

            }

            return false;
        }




    }
}
