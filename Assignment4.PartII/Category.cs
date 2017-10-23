using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Assignment4
{
    public class Category
    {
        [Key]
        [Column("categoryid")]

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }
}
