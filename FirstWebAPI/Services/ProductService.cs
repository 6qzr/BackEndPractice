using FirstWebAPI.Models;
using FirstWebAPI.Repositories;

namespace FirstWebAPI.Services
{
    public class ProductService
    {
        private ProductRepo repo;

        public ProductService(ProductRepo repo)
        {
            this.repo = repo;
        }

        public List<Product> GetAllProducts()
        {
            return repo.GetAllProducts();
        }

        public Product GetProductById(int id)
        {
            return repo.GetProductById(id);
        }

        public int Create(Product product)
        {

            repo.Add(product);
            return product.productId;
        }

        public bool UpdatePrice(int productId, decimal newPrice)
        {
            Product product = repo.GetProductById(productId);
            if (product == null)
            {
                return false;
            }

            product.price = newPrice;
            repo.Update();
            return true;

        }

        public bool Delete(int productId)
        {
            Product product = repo.GetProductById(productId);
            if (product == null)
            {
                return false;
            }

            repo.Delete(product);
            return true;
        }
    }
}
