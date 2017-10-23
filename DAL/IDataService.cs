using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDataService
    {
        List<Category> GetCategories();
        Category CreateCategory(string name, string description);
        Product GetProduct(int id);
        bool DeleteCategory(int id);
        List<Product> GetProductByName(string substring);

        Category GetCategory(int id);
    }
}
