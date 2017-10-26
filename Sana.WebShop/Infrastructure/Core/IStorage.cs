using System.Collections.Generic;

namespace Sana.WebShop.Infrastructure.Core
{
    public interface IStorage
    {
        void Create(object entity);

        List<T> List<T>();
    }
}
