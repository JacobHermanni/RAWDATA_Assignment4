using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Data.SqlTypes;

namespace Assignment4
{
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
}
