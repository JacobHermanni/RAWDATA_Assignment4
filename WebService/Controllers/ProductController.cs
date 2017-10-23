using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL;


namespace WebService.Controllers
{
    [Route("/api/products")]
    public class ProductController : Controller
    {
        private IDataService _dataService;

        public ProductController(IDataService dataService)
        {
            _dataService = dataService;
        }


        [HttpGet("{name}")]
        public IActionResult GetProduct(string name)
        {
            var product = _dataService.GetProductByName(name)
                .FirstOrDefault(x => x.Name == name);
            if (product == null) return NotFound();
            return Ok(product);
        }


        //GET api/products/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            var product = _dataService.GetProduct(id);
            return product;
        }

        //api/products/name/<substring>
        [HttpGet("/name/{substring}")]
        public List<Product> Get(string substring)
        {
            var product = _dataService.GetProductByName(substring);
            return product;
        }

    }
}
