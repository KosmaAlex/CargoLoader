using CargoLoader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Services
{
    public interface IDataService
    {
        Task Create<T>(T entity) where T : DomainObject;
        Task<T> Update<T>(int id, T entity) where T : DomainObject;
        Task<bool> Delete<T>(int id) where T : DomainObject;
        Task<T> Get<T>(int id) where T : DomainObject;
        Task<IEnumerable<T>> GetAll<T>() where T : DomainObject; 
        bool Contains<T>(T entity, IEqualityComparer<T> comparer) where T : DomainObject;
    }
}
