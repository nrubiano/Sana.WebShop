using System.Collections.Generic;

namespace Sana.WebShop.Infrastructure.Data
{
    public interface IRepository<T>
    {
        string Storage { get; set; }

        void Create(T entity);

        //Whe should use some pagination pattern
        IList<T> List();
    }
}