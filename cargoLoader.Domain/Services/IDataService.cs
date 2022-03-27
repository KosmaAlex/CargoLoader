using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Services
{
    public interface IDataService<T>
    {
        Task Create(T entity);
        Task<T> Update(int id, T entity);
        Task<bool> Delete(int id);
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
    }
}
