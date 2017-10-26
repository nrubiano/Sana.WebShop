using System;
using System.Collections.Generic;
using System.Linq;

namespace Sana.WebShop.Infrastructure.Core
{
    public class MemoryStorage : IStorage
    {
        private static readonly List<object> _storage = new List<object>();
        
        public void Create(object entity)
        {
            _storage.Add(entity);
        }

        public List<T> List<T>()
        {
            return _storage.OfType<T>().ToList();
        }
    }
}