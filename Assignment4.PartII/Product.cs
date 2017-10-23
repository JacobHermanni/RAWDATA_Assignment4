using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Assignment4
{
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
}
