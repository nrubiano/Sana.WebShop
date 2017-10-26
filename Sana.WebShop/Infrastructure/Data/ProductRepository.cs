using System.Collections.Generic;
using Ninject;
using Sana.WebShop.Infrastructure.Core;
using Sana.WebShop.Models;

namespace Sana.WebShop.Infrastructure.Data
{
    public class ProductRepository : IRepository<Product>
    {
        public string Storage { get; set; }

        private IStorage _storage;

        private readonly IKernel _kernel;

        public ProductRepository(IKernel kernel)
        {
            _kernel = kernel;
        }
        
        public void Create(Product entity)
        {
            SetupStorage();

            _storage.Create(entity);
        }

        public IList<Product> List()
        {
            SetupStorage();

            return _storage.List<Product>();
        }

        private void SetupStorage()
        {
            _storage = _kernel.Get<IStorage>(string.IsNullOrEmpty(Storage) ? "Memory" : Storage);
        }
    }
}