using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL;
using Newtonsoft.Json;

namespace WebService.Controllers
{

    [Route("/api/categories")]
    public class CategoryController : Controller
    {
        private IDataService _dataService;

        public CategoryController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET api/categories/5 etc.
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _dataService.GetCategory(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpGet()]
        public IActionResult GetCategory()
        {
            var categories = _dataService.GetCategories();
            if (categories == null) return NotFound();
            return Ok(categories);
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody]Category sentCategory)
        {
            var category = _dataService.CreateCategory(sentCategory.Name, sentCategory.Description);
            return Created("http://localhost:5001/api/categories/" + category.Id, category);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int catId)
        {
            var delete = _dataService.DeleteCategory(catId);
            if (delete) return Ok();
            return NotFound();
        }
    }
}








