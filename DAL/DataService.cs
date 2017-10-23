using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL
{

    // --------------------------- Db: --------------------------- //
    class NorthwindContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseMySql(
                "server=localhost;" +
                "database=northwind;" +
                "uid=root;" +
                "pwd=root;"
            );

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .Property(x => x.Name)
                .HasColumnName("categoryname");

            modelBuilder.Entity<Product>()
                .Property(x => x.Name)
                .HasColumnName("productname");

            modelBuilder.Entity<OrderDetails>()
                .Property(x => x.OrderId)
                .HasColumnName("orderid");

        }
    }

    // --------------------------- Db-Classes: --------------------------- //
    public class Category
    {
        [Key]
        [Column("categoryid")]

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }

    public class Order
    {
        [Key]
        [Column("orderid")]
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        [Column("OrderDate")]
        public DateTime Date { get; set; }

        [Column("RequiredDate")]
        public DateTime Required { get; set; }

        [Column("ShippedDate")]
        public Nullable<DateTime> ShippedDate { get; set; }

        public Double Freight { get; set; }

        public OrderDetails OrderDetails { get; set; }

        public string ShipName { get; set; }

        public string ShipCity { get; set; }

        public string ShipPostalCode { get; set; }

        public string ShipCountry { get; set; }

    }

    public class OrderDetails
    {

        [Key]
        [Column("orderid")]

        public int OrderId { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public double Discount { get; set; }

        public double UnitPrice { get; set; }

        public Order Order
        {
            get
            {
                var dataService = new DataService();

                var order = dataService.GetOrder(OrderId);

                return order;
            }
            set { Order = value; }
        }

        public Product Product
        {
            get
            {
                var dataService = new DataService();

                var product = dataService.GetProduct(ProductId);

                return product;
            }
            set { Product = value; }
        }

    }

    public class Product
    {
        [Key]
        [Column("productid")]

        public int Id { get; set; }

        public string Name { get; set; }

        public double UnitPrice { get; set; }

        [Column("quantityunit")]
        public string QuantityPerUnit { get; set; }

        public int UnitsInStock { get; set; }

        public int CategoryId { get; set; }

        public Category Category
        {
            get
            {
                var dataService = new DataService();

                var category = dataService.GetCategory(CategoryId);

                return category;
            }
            set { Category = value; }
        }
    }


   
    // --------------------------- Db-Service: --------------------------- //
    public class DataService : IDataService
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
