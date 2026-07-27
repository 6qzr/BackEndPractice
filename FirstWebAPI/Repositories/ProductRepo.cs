using FirstWebAPI.Models;

namespace FirstWebAPI.Repositories
{
    public class ProductRepo
    {
        private ECommerceContext context;

        public ProductRepo(ECommerceContext context)
        {
            this.context = context;
        }

        public List<Product> GetAllProducts()
        {
            return context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return context.Products.FirstOrDefault(p => p.productId == id);
        }

        public void Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public void Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }
    }
}
