using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Assignment4
{
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
        //{
            //get
            //{
            //    var dataService = new DataService();

            //    var orderDetails = dataService.GetOrderDetailsByOrderId(Id);

            //    return orderDetails;
            //}
            //set { OrderDetails = value; }
        //}

        public string ShipName { get; set; }

        public string ShipCity { get; set; }

        public string ShipPostalCode { get; set; }

        public string ShipCountry { get; set; }

    }
}
