using BusinessObjects;
using DataAccess;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        public Product AddProduct(Product Product) => ProductDAO.Instance.AddProduct(Product);

        public IEnumerable<Product> GetAll() => ProductDAO.Instance.GetAll();   

        public Product GetByID(string id) => ProductDAO.Instance.GetByID(id);   

        public Product RemoveProduct(string id) => ProductDAO.Instance.RemoveProduct(id);

        public Product UpdateProduct(Product Product) => ProductDAO.Instance.UpdateProduct(Product);
    }
}
